namespace BobTheBuilder
{
    public class Game
    {
        private Minimap minimap = new();
        private Room? currentRoom;
        private Room? previousRoom;
        private List<Room> discoveredRooms = new();
        private Bank bank = null!;


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

            while (day<10)
            {
                bool continuePlaying = true;
                bank?.calculateRepayment();
                Console.WriteLine("==============|Day {0}|==============",day);
                Console.WriteLine("===================================");
                Console.WriteLine("Money = " + bank!.getBalance());
                Console.WriteLine("===================================");
                StatisticsUI.DisplayStats(stats, day);


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
                            if (currentRoom is House house)
                            {
                                showHouse();
                            }
                            break;

                        case "accept":
                            if (currentRoom is ConstructionBuilding consBuildingAccept)
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
                                        Console.WriteLine("Quest completed!");
                                        consBuildingAccept.QuestItemRemover(Convert.ToInt32(command.SecondWord), player);
                                        consBuildingAccept.MoneyDeduction(Convert.ToInt32(command.SecondWord), bank);
                                        stats.RecordQuestCompletion(consBuildingAccept.GetQuestInfo(Convert.ToInt32(command.SecondWord), phase));
                                        phase++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Could not complete quest.");

                                    }
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
                            continuePlaying = false;
                            break;

                        case "help":
                            GameUI.PrintHelp();
                            break;

                        case "map":
                            if (currentRoom != null)
                                MinimapUI.DisplayMinimap(minimap, currentRoom, minimap.roomPositions);
                            break;
                            
                        case "travel":
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
                            Console.WriteLine("Account balance: " + bank!.getBalance());
                            Console.WriteLine("Total debt: " + bank!.getTotalDebt());
                            Console.WriteLine("Monthly repayment: " + bank!.getMonthlyRepayment());
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
                                    if(player.BuyItem(contentsToBuy, bank))
                                    {
                                        Console.WriteLine($"Bought {contentsToBuy.Name} for {contentsToBuy.Price}.");
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
            Console.Write(my.setRoof(3));
            Console.Write(my.setWalls(4));
            Console.Write(my.setFoundation(3));
        }
        

    }
}