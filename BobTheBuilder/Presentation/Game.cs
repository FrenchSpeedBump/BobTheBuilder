namespace BobTheBuilder
{
    public class Game
    {
        private Minimap minimap = new();
        private Room? currentRoom;
        private Room? previousRoom;
        private List<Room> discoveredRooms = new();
        private Bank bank = null!;
        public House house = null!;
        private Logic.NaturalDisasters disasterEvent = new();


        public Game()
        {
            GameInit gameInit = new GameInit();

            // get all rooms, items and materials
            Dictionary<string, Room> rooms = gameInit.CreateRooms();
            List<(string shopShortDescription, Item item)> items = gameInit.CreateItems();
            List<(string shopShortDescription, Material material)> materials = gameInit.CreateMaterials();

            // wire up starting room and bank using the same normalization
            if (rooms.TryGetValue(GameInit.Normalize("House"), out Room? houseRoom))
            {
                currentRoom = houseRoom;
                houseRoom.discovered = true;
                minimap.MapRooms(houseRoom);
                house = (House)rooms["House"];
            }

            if (rooms.TryGetValue(GameInit.Normalize("Bank"), out Room? bankRoom) && bankRoom is Bank b)
            {
                bank = b;
            }

            // place items/materials into shops using existing helpers
            foreach (var (shopShort, item) in items)
                AssignItem(shopShort, item);

            foreach (var (shopShort, material) in materials)
                AssignMaterial(shopShort, material);
        }
        

        public void Play()
        {
            Parser parser = new();

            Player player = new();

            Statistics stats = new();

            // UI features now handled through GameUI static class, which should contain all UI related methods

            GameUI.PrintWelcomeImage();
            GameUI.LoadingBar();
            Console.Clear();
            GameUI.PrintWelcome();
            
            int day = 1;
            int phase = 1;
            bool built_today;
            while (day<10)
            {
                built_today = false;
                bool continuePlaying = true;
                bank?.calculateRepayment();
                Console.WriteLine("==============|Day {0}|==============",day);
                Console.WriteLine("===================================");
                Console.WriteLine("Money = " + bank!.getBalance()+bank.currency);
                Console.WriteLine("===================================");
                StatisticsUI.DisplayStats(stats, day);

                if(!disasterEvent.disasterStruck(house, day))
                {
                    Console.WriteLine("Unfortunatelly your house did not survive the disaster.\n===================\nGAME OVER\n===================");
                }
                while (continuePlaying)
                {
                    
                    Console.WriteLine(currentRoom?.ShortDescription);
                    Console.Write("> ");

                    string? input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Please enter a command.");
                        continue;
                    }

                    Command? command = parser.GetCommand(input);

                    if (command == null)
                    {
                        Console.WriteLine("I don't know that command.");
                        continue;
                    }

                    switch (command.Name)
                    {
                        case "look":
                            Console.WriteLine(currentRoom?.LongDescription);
                            if (currentRoom is Shop lookShop)
                            {
                                ShopUI.DisplayInventory(lookShop);
                            }
                            if (currentRoom is ConstructionBuilding consBuilding)
                            {
                                List<Quest> quests = consBuilding.GetQuestByPhase(phase);
                                ConstructionUI.DisplayQuests(quests);
                            }

                            if (currentRoom is House)
                            {
                                showHouse();
                            }
                            break;

                        case "accept":
                            if (currentRoom is ConstructionBuilding consBuildingAccept)
                            {
                                if(!built_today)
                                {
                                    if (command.SecondWord == null)
                                    {
                                        Console.WriteLine("Accept which quest?");
                                        break;
                                    }
                                    else
                                    {
                                        if (consBuildingAccept.AcceptQuest(Convert.ToInt32(command.SecondWord), phase, player))
                                        {
                                            built_today = true;
                                            if (phase == 1)//phase for foundation
                                            {
                                                Quest quest = consBuildingAccept.GetQuestInfo(Convert.ToInt32(command.SecondWord), phase);
                                                string name = quest.requirements[0].Name;
                                                double quality = quest.requirements[0].Quality;
                                                house.foundationHP = quality;
                                                if (name == "Wood")
                                                {
                                                    house.foundation = 1;
                                                }
                                                else if (name == "Concrete")
                                                {
                                                    house.foundation = 2;
                                                }
                                                else if (name == "Brick")
                                                {
                                                    house.foundation = 3;
                                                }
                                                else//other
                                                {
                                                    house.foundation = 4;
                                                }

                                            }
                                            if (phase == 2)//phase for walls
                                            {
                                                Quest quest = consBuildingAccept.GetQuestInfo(Convert.ToInt32(command.SecondWord), phase);
                                                string name = quest.requirements[0].Name;
                                                double quality = quest.requirements[0].Quality;
                                                house.wallsHP = quality;
                                                if (name == "Wood")
                                                {
                                                    house.walls = 1;
                                                }
                                                else if (name == "Concrete")
                                                {
                                                    house.walls = 2;
                                                }
                                                else if (name == "Brick")
                                                {
                                                    house.walls = 3;
                                                }
                                                else if (name == "WoodShingles")
                                                {
                                                    house.walls = 4;
                                                }
                                                else//other
                                                {
                                                    house.walls = 5;
                                                }
                                            }
                                            if (phase == 3)//phase for roof
                                            {
                                                Quest quest = consBuildingAccept.GetQuestInfo(Convert.ToInt32(command.SecondWord), phase);
                                                string name = quest.requirements[0].Name;
                                                double quality = quest.requirements[0].Quality;
                                                house.roofHP = quality;
                                                if (name == "Wood")
                                                {
                                                    house.roof = 1;
                                                }
                                                else if (name == "Concrete")
                                                {
                                                    house.roof = 2;
                                                }
                                                else if (name == "Tyle")
                                                {
                                                    house.roof = 3;
                                                }
                                                else//other
                                                {
                                                    house.foundation = 4;
                                                }
                                            }
                                            Console.WriteLine("Quest completed!");
                                            consBuildingAccept.QuestItemRemover(Convert.ToInt32(command.SecondWord), player);
                                            consBuildingAccept.MoneyDeduction(Convert.ToInt32(command.SecondWord), bank);
                                            stats.RecordQuestCompletion(consBuildingAccept.GetQuestInfo(Convert.ToInt32(command.SecondWord), phase));
                                            house.RecordMaterials(consBuildingAccept.GetQuestInfo(Convert.ToInt32(command.SecondWord), phase));
                                            phase++;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Could not complete quest.");

                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("You need to wait for the construction team to finish. Maybe come back tomorrow to start a new quest.");
                                }
                            } else
                            {
                                Console.WriteLine("You can only accept quests in a construction building.");
                            }
                            break;

                        case "back":
                            if (previousRoom == null)
                                Console.WriteLine("You can't go back from here!");
                            else
                                currentRoom = previousRoom;
                            break;

                        case "north":
                        case "south":
                        case "east":
                        case "west":
                            if (currentRoom != null && !discoveredRooms.Contains(currentRoom))
                            {
                                discoveredRooms.Add(currentRoom);
                            }
                            Move(command.Name);
                            break;

                        case "quit":
                            day = 10000;
                            continuePlaying = false;
                            break;
                        case "sleep":
                            Console.WriteLine("You went home to sleep.");
                            continuePlaying = false;
                            currentRoom = house;
                            break;
                        case "help":
                            GameUI.PrintHelp();
                            break;

                        case "map":
                            if (currentRoom != null)
                                MinimapUI.DisplayMinimap(minimap, currentRoom, minimap.roomPositions);
                            break;
                            
                        case "travel":
                            if (player.Has("Car"))
                            {
                                if (command.SecondWord == null)
                                {
                                    Console.WriteLine("Travel where?");
                                    break;
                                }
                                var targetTravel = FindRoomByName(command.SecondWord, command.ThirdWord, command.FourthWord);
                                if (targetTravel != null)
                                {
                                    if (currentRoom != null && !discoveredRooms.Contains(currentRoom))
                                    {
                                        discoveredRooms.Add(currentRoom);
                                    }
                                    Travel(targetTravel);
                                }
                                else
                                    Console.WriteLine("Unknown room");
                                break;
                            }
                            Console.WriteLine("You can't travel yet. You need to buy a car first.");
                            break;

                        case "gointo"://now you can enter a neighbouring room by typing in it's name without knowing the direction
                            if (command.SecondWord != null)
                            {
                                string direction = IsNeighbour(command.SecondWord);
                                if (command.SecondWord == null)
                                {
                                Console.WriteLine("You need to specify where to go");
                                }
                                else if (direction != "")
                                {
                                Move(direction);
                                }
                                else
                                {
                                Console.WriteLine("I can't see that room anywhere!");
                                }
                            }
                            break;

                        case "loan"://loan money
                            if (currentRoom != bank)
                            {
                                Console.WriteLine("I can only do this in a bank.");
                                break;
                            }
                            if (command.SecondWord == null)
                            {
                                Console.WriteLine("How much would you like to loan?");
                            }
                            if (!bank!.takeLoan(Convert.ToDouble(command.SecondWord)))
                            {
                                Console.WriteLine("Loan request denied.");
                            }
                            break;
                        case "account"://display account info
                            if (currentRoom != bank)
                            {
                                Console.WriteLine("I can only do this in a bank.");
                                break;
                            }
                            Console.WriteLine("Account information:");
                            Console.WriteLine("Account balance: " + bank!.getBalance()+bank.currency);
                            Console.WriteLine("Total debt: " + bank!.getTotalDebt() + bank.currency);
                            Console.WriteLine("Monthly repayment: " + bank!.getMonthlyRepayment() + bank.currency);
                            break;
                        case "inventory": // Show player inventory
                            PlayerUI.DisplayInventory(player);
                            break;

                        case "buy"://buy stuff
                            if (command.SecondWord == null)
                            {
                                Console.WriteLine("Buy what?");
                                break;
                            }
                            if (currentRoom is Shop buyShop)
                            {
                                ShopInventoryContents? contentsToBuy = buyShop.GetContents(command.SecondWord);
                                if (contentsToBuy != null)
                                {
                                    Room? otherShop = FindRoomByName("Bob's", "Materials", null);
                                    if (buyShop.ShortDescription == "Bob's Materials" && player.BuyMaterial(contentsToBuy, bank))
                                    {
                                        Console.WriteLine($"Bought {contentsToBuy.Name} for {contentsToBuy.Price}.");
                                    }
                                    else if(otherShop is Shop materialShop && buyShop.ShortDescription == "Magic Tool Shop")
                                    {
                                        if (contentsToBuy is Item tool && player.BuyItem(tool, bank, materialShop))
                                        {
                                            Console.WriteLine($"Bought {contentsToBuy.Name} for {contentsToBuy.Price}.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Not enough money.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Item not found.");
                                }
                            }
                            break;
                        case "work":
                            continuePlaying = false;
                            double amount = 200;
                            Console.WriteLine("Today you went to work and earned {0}{1}.",amount,bank.currency);
                            bank.addMoney(amount);
                            Thread.Sleep(3000);
                            break;
                        default:
                            Console.WriteLine("I don't know what command.");
                            break;
                    }
                    
                }
                day++;
                    Console.Clear();
            }
            

            Console.WriteLine("Thank you for playing Bob the Builder!");
        }

        private void Move(string direction)
        {
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];
                if (!currentRoom!.discovered)
                    currentRoom!.discovered=true;
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }
        private string IsNeighbour(string room)
        {
            foreach (var direction in currentRoom!.Exits)
            {
                if (Normalize(direction.Value.ShortDescription) == Normalize(room))
                {
                    return direction.Key;
                }
            }
            return "";
        }
        private void Travel(Room targetRoom)
        {
            if (discoveredRooms.Contains(targetRoom))
            {
                previousRoom = currentRoom;
                currentRoom = targetRoom;
            }
            else
            {
                Console.WriteLine("Targeted location not yet discovered.");
            }
        }
        
        private Room? FindRoomByName(string? secondWord, string? thirdWord, string? fourthWord)//BFS for finding target room
        {
            string target = Normalize(secondWord) + Normalize(thirdWord) + Normalize(fourthWord);

            if (target == "north" || target == "east" || target == "south" || target == "west")//if just direction going to direction
            {
                Move(target);
                return null;
            }

            var visited = new HashSet<Room>();
            var queue = new Queue<Room>();

            queue.Enqueue(currentRoom!);
            visited.Add(currentRoom!);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (Normalize(current.ShortDescription) == target)
                    return current;

                foreach (var neighbor in current.Exits.Values)
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
            return null;
        }

        private static string Normalize(string? s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            // remove common separators/punctuation that users might type differently
            return s.Trim()
                    .Replace(" ", "")
                    .Replace("_", "")
                    .Replace("-", "")
                    .Replace("'", "")
                    .Replace("\"", "")
                    .Replace(".", "")
                    .Replace(",", "")
                    .ToLowerInvariant();
        }

        private void AssignItem(string shopShortDescription, Item items)
        {
            var room = FindRoomByName(shopShortDescription, null, null);
            if (room is Shop shop)
            {
                shop.AddContents(items);
            }
            else
            {
                Console.WriteLine($"Shop '{shopShortDescription}' not found to add item '{items.Name}'.");
            }
        }
        private void AssignMaterial(string shopShortDescription, Material material)
        {
            var room = FindRoomByName(shopShortDescription, null, null);
            if (room is Shop shop)
            {
                shop.AddContents(material);
            }
            else
            {
                Console.WriteLine($"Shop '{shopShortDescription}' not found to add material '{material.Name}'.");
            }
        }

        private void showHouse()
        {
            Presentation.UI.HouseUI my = new Presentation.UI.HouseUI();
            Console.Write(my.setRoof(house.roof));
            Console.Write(my.setWalls(house.walls));
            Console.Write(my.setFoundation(house.foundation));
        }
        

    }
}