namespace BobTheBuilder
{
    public class Game
    {
        private Minimap minimap = new Minimap();
        private Room? currentRoom;
        private Room? previousRoom;
        private List<Room> discoveredRooms = new List<Room>();
        private Bank bank;
        private House house;
        private Logic.NaturalDisasters disasterEvent = new Logic.NaturalDisasters();

        public House House => house;


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
            else
            {
                throw new InvalidOperationException("House room not found in game initialization.");
            }

            if (rooms.TryGetValue(GameInit.Normalize("Bank"), out Room? bankRoom) && bankRoom is Bank b)
            {
                bank = b;
            }
            else
            {
                throw new InvalidOperationException("Bank room not found in game initialization.");
            }

            // place items/materials into shops using existing helpers
            foreach ((string shopShort, Item item) in items)
                AssignItem(shopShort, item);

            foreach ((string shopShort, Material material) in materials)
                AssignMaterial(shopShort, material);
        }
        

        public void Play()
        {
            Parser parser = new Parser();
            Player player = new Player();
            Statistics stats = new Statistics();

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
                bank.CalculateRepayment();
                Console.WriteLine("==============|Day {0}|==============",day);
                Console.WriteLine("===================================");
                Console.WriteLine("Money = " + bank.GetBalance() + bank.currency);
                Console.WriteLine("===================================");
                StatisticsUI.DisplayStats(stats, day);

                if(!disasterEvent.DisasterStruck(house, day))
                {
                    Console.WriteLine("Unfortunately your house did not survive the disaster.\n===================\nGAME OVER\n===================");
                    day = 10000;
                    break;
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
                                ShowHouse();
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
                                        int questId = Convert.ToInt32(command.SecondWord);
                                        if (consBuildingAccept.AcceptQuest(questId, phase, player))
                                        {
                                            built_today = true;
                                            Quest quest = consBuildingAccept.GetQuestInfo(questId, phase);
                                            string materialName = quest.Requirements[0].Name;
                                            double quality = quest.Requirements[0].Quality;
                                            
                                            if (phase == 1)//phase for foundation
                                            {
                                                house.foundationHP = quality;
                                                if (materialName == "Wood")
                                                {
                                                    house.foundation = 1;
                                                }
                                                else if (materialName == "Concrete")
                                                {
                                                    house.foundation = 2;
                                                }
                                                else if (materialName == "Brick")
                                                {
                                                    house.foundation = 3;
                                                }
                                                else//other
                                                {
                                                    house.foundation = 4;
                                                }
                                            }
                                            else if (phase == 2)//phase for walls
                                            {
                                                house.wallsHP = quality;
                                                if (materialName == "Wood")
                                                {
                                                    house.walls = 1;
                                                }
                                                else if (materialName == "Concrete")
                                                {
                                                    house.walls = 2;
                                                }
                                                else if (materialName == "Brick")
                                                {
                                                    house.walls = 3;
                                                }
                                                else if (materialName == "WoodShingles")
                                                {
                                                    house.walls = 4;
                                                }
                                                else//other
                                                {
                                                    house.walls = 5;
                                                }
                                            }
                                            else if (phase == 3)//phase for roof
                                            {
                                                house.roofHP = quality;
                                                if (materialName == "Wood")
                                                {
                                                    house.roof = 1;
                                                }
                                                else if (materialName == "Concrete")
                                                {
                                                    house.roof = 2;
                                                }
                                                else if (materialName == "Tyle")
                                                {
                                                    house.roof = 3;
                                                }
                                                else//other
                                                {
                                                    house.roof = 4;
                                                }
                                            }
                                            Console.WriteLine("Quest completed!");
                                            consBuildingAccept.QuestItemRemover(questId, player);
                                            consBuildingAccept.MoneyDeduction(questId, bank);
                                            stats.RecordQuestCompletion(quest);
                                            house.RecordMaterials(quest);
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
                                Room? targetTravel = FindRoomByName(command.SecondWord, command.ThirdWord, command.FourthWord);
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
                            if (command.SecondWord == null)
                                {
                                Console.WriteLine("You need to specify where to go");
                                }
                            else if (command.SecondWord != null)
                            {
                                string direction = IsNeighbour(command.SecondWord);
                                if (direction != "")
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
                                break;
                            }
                            if (!bank.TakeLoan(Convert.ToDouble(command.SecondWord)))
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
                            Console.WriteLine("Account balance: " + bank.GetBalance() + bank.currency);
                            Console.WriteLine("Total debt: " + bank.GetTotalDebt() + bank.currency);
                            Console.WriteLine("Monthly repayment: " + bank.GetMonthlyRepayment() + bank.currency);
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
                            else 
                            {
                                Console.WriteLine("You can't buy that here.");
                            }
                            break;
                        case "work":
                            continuePlaying = false;
                            double amount = 200;
                            Console.WriteLine("Today you went to work and earned {0}{1}.",amount,bank.currency);
                            bank.AddMoney(amount);
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
            foreach (KeyValuePair<string, Room> direction in currentRoom!.Exits)
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

            HashSet<Room> visited = new HashSet<Room>();
            Queue<Room> queue = new Queue<Room>();

            queue.Enqueue(currentRoom!);
            visited.Add(currentRoom!);

            while (queue.Count > 0)
            {
                Room current = queue.Dequeue();

                if (Normalize(current.ShortDescription) == target)
                    return current;

                foreach (Room neighbor in current.Exits.Values)
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

        private void AssignItem(string shopShortDescription, Item item)
        {
            Room? room = FindRoomByName(shopShortDescription, null, null);
            if (room is Shop shop)
            {
                shop.AddContents(item);
            }
            else
            {
                Console.WriteLine($"Shop '{shopShortDescription}' not found to add item '{item.Name}'.");
            }
        }
        private void AssignMaterial(string shopShortDescription, Material material)
        {
            Room? room = FindRoomByName(shopShortDescription, null, null);
            if (room is Shop shop)
            {
                shop.AddContents(material);
            }
            else
            {
                Console.WriteLine($"Shop '{shopShortDescription}' not found to add material '{material.Name}'.");
            }
        }

        private void ShowHouse()
        {
            Presentation.UI.HouseUI houseUI = new Presentation.UI.HouseUI();
            Console.Write(houseUI.SetRoof(house.roof));
            Console.Write(houseUI.SetWalls(house.walls));
            Console.Write(houseUI.SetFoundation(house.foundation));
        }
        

    }
}