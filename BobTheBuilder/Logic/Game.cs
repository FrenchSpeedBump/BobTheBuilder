namespace BobTheBuilder
{
    public class Game
    {
        private Minimap minimap = new();
        private Room? currentRoom;
        private Room? previousRoom;
        private List<Room> discoveredRooms = new();
        private Bank bank = null!;
        private House? house;
        private DisasterManager disasterManager = new();
        private Random rng = new();


        public Game()
        {
            var gameInit = new GameInit();

            // get all rooms, items and materials
            var rooms = gameInit.CreateRooms();
            var items = gameInit.CreateItems();
            var materials = gameInit.CreateMaterials();

            // wire up starting room and bank using the same normalization
            if (rooms.TryGetValue(GameInit.Normalize("House"), out var houseRoom))
            {
                currentRoom = houseRoom;
                houseRoom.discovered = true;
                minimap.MapRooms(houseRoom);
                if (houseRoom is House h)
                    house = h;
            }

            if (rooms.TryGetValue(GameInit.Normalize("Bank"), out var bankRoom) && bankRoom is Bank b)
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
                bool gameOver = false;
                bank?.calculateRepayment();
                Console.WriteLine("==============|Day {0}|==============",day);
                Console.WriteLine("===================================");
                Console.WriteLine("Money = " + bank!.getBalance());
                Console.WriteLine("===================================");
                if (house != null)
                {
                    bool survived = disasterManager.TryTrigger(day, house, rng, msg => Console.WriteLine(msg), out var damagedParts);
                    if (!survived)
                    {
                        Console.WriteLine("Your house collapsed. Game over.");
                        gameOver = true;
                        continuePlaying = false;
                    }
                    else if (damagedParts.Any())
                    {
                        HandleRepairs(bank, house, damagedParts);
                    }
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
                                lookShop.DisplayInventory();
                            }
                            if (currentRoom is ConstructionBuilding consBuilding)
                            {
                                consBuilding.GetQuestByPhase(phase);
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
                                        Material? usedMaterial = consBuildingAccept.QuestItemRemover(Convert.ToInt32(command.SecondWord), player);
                                        Quest completedQuest = consBuildingAccept.currentQuests[Convert.ToInt32(command.SecondWord)];
                                        completedQuest.isCompleted = true;
                                        if (house != null && completedQuest.buildsPart != null)
                                        {
                                            house.SetBuiltPart(completedQuest.buildsPart, usedMaterial);
                                        }
                                        consBuildingAccept.MoneyDeduction(Convert.ToInt32(command.SecondWord), bank);
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
                                minimap.Display(currentRoom);
                            break;

                        case "goto"://maybe change name so its not similar to gointo
                            if (command.SecondWord == null)
                            {
                                Console.WriteLine("Go where?");
                                break;
                            }
                            var target = FindRoomByName(command.SecondWord, command.ThirdWord, command.FourthWord);
                            if (target != null && currentRoom != null)
                            {
                                Console.WriteLine($"Direction: {minimap.GetDirectionTo(currentRoom, target)}");
                            }
                            else
                                Console.WriteLine("Unknown room");
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
                            bank!.takeLoan(Convert.ToDouble(command.SecondWord));
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
                            player.DisplayInventory();
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
                                    player.BuyItem(contentsToBuy, bank);
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
                if (gameOver)
                    break;
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

        private void HandleRepairs(Bank bank, House house, List<string> damagedParts)
        {
            foreach (string part in damagedParts)
            {
                while (true)
                {
                    Console.WriteLine($"{part} was damaged. Rebuild now for $50? (yes/no)");
                    string? input = Console.ReadLine();
                    if (input == null) break;
                    string normalized = Normalize(input);
                    if (normalized == "yes" || normalized == "y")
                    {
                        if (bank.getBalance() >= 50)
                        {
                            bank.accountBalance -= 50;
                            Material repaired = new("Repaired Part", "Quick paid rebuild", 0.5, 50, 0.5, 0.5, 0.5, 0.5);
                            house.SetBuiltPart(part, repaired);
                            Console.WriteLine($"Rebuilt {part} for $50. Remaining balance: {bank.getBalance():0.00}");
                        }
                        else
                        {
                            Console.WriteLine("Not enough money to rebuild now. Consider taking a loan at the bank.");
                        }
                        break;
                    }
                    else if (normalized == "no" || normalized == "n")
                    {
                        Console.WriteLine("You chose not to rebuild. If another disaster comes your house might collapse.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please answer yes or no.");
                    }
                }
            }
        }

    }
}
