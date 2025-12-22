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
            while (day<21)
            {
                built_today = false;
                bool continuePlaying = true;
                bank.CalculateRepayment();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n==============|Day {0}|==============",day);
                Console.ResetColor();
                Console.WriteLine("Balance: " + bank.GetBalance() + bank.currency);
                Console.WriteLine("===================================");
                StatisticsUI.DisplayStats(stats, day);

                Logic.DisasterResult disasterResult = disasterEvent.DisasterStruck(house, day);
                
                if(!disasterResult.HouseSurvived)
                {
                    stats.RecordNaturalDisasterHappening(true);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n================================");
                    Console.WriteLine("       GAME OVER");
                    Console.WriteLine("================================");
                    Console.ResetColor();
                    Console.WriteLine("Your house did not survive the disaster.\n");
                    day = 10000;
                    break;
                }
                if(disasterResult.DisasterOccurred)
                {
                    stats.RecordNaturalDisasterHappening(true);
                    Presentation.UI.NaturalDisastersUI.DisplayDisasterAnnouncement(disasterResult.DisasterName!);
                    
                    // Check quality vs disaster damage for each part
                    if (house.foundation > 0)
                    {
                        if (house.foundationQuality < disasterResult.FoundationDamage)
                        {
                            Presentation.UI.NaturalDisastersUI.DisplayDamageReport("Foundation", house.foundationHP);
                        }
                        else
                        {
                            Presentation.UI.NaturalDisastersUI.DisplayBlockedMessage("Foundation", house.foundationQuality, house.foundationHP);
                        }
                    }
                    if (house.walls > 0)
                    {
                        if (house.wallsQuality < disasterResult.WallsDamage)
                        {
                            Presentation.UI.NaturalDisastersUI.DisplayDamageReport("Walls", house.wallsHP);
                        }
                        else
                        {
                            Presentation.UI.NaturalDisastersUI.DisplayBlockedMessage("Walls", house.wallsQuality, house.wallsHP);
                        }
                    }
                    if (house.roof > 0)
                    {
                        if (house.roofQuality < disasterResult.RoofDamage)
                        {
                            Presentation.UI.NaturalDisastersUI.DisplayDamageReport("Roof", house.roofHP);
                        }
                        else
                        {
                            Presentation.UI.NaturalDisastersUI.DisplayBlockedMessage("Roof", house.roofQuality, house.roofHP);
                        }
                    }
                    Presentation.UI.NaturalDisastersUI.DisplayRepairSuggestion();
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("I don't know that command.");
                        Console.ResetColor();
                        continue;
                    }

                    switch (command.Name)
                    {
                        case "look":
                            Console.WriteLine(currentRoom?.LongDescription);
                            if (currentRoom is ConstructionBuilding consBuilding)
                            {
                                List<Quest> quests = consBuilding.GetQuestByPhase(phase);
                                ConstructionUI.DisplayQuests(quests);
                            }
                            else if (currentRoom is Shop lookShop)
                            {
                                ShopUI.DisplayInventory(lookShop);
                            }

                            if (currentRoom is House)
                            {
                                Presentation.UI.HouseUI.DisplayHouse(house);
                            }
                            break;

                        case "accept":
                            if (currentRoom is ConstructionBuilding consBuildingAccept)
                            {
                                if(!built_today)
                                {
                                    if (command.SecondWord == null)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Accept which quest?");
                                        Console.ResetColor();
                                        break;
                                    }
                                    else
                                    {
                                        int questId = Convert.ToInt32(command.SecondWord) - 1; // Convert from 1-based to 0-based index
                                        if (consBuildingAccept.AcceptQuest(questId, phase, player))
                                        {
                                            Quest quest = consBuildingAccept.GetQuestInfo(questId, phase);
                                            
                                            // Check payment first before completing quest
                                            if (!consBuildingAccept.MoneyDeduction(questId, bank))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Not enough money to pay for this quest!");
                                                Console.ResetColor();
                                            }
                                            else
                                            {
                                                built_today = true;
                                                house.BuildPart(phase, quest);
                                                
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("\n✓ Quest completed!\n");
                                                Console.ResetColor();
                                                consBuildingAccept.QuestItemRemover(questId, player);
                                                stats.RecordQuestCompletion(quest);
                                                house.RecordMaterials(quest);
                                                phase++;
                                            }
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Could not complete quest.\n");
                                            Console.ResetColor();
                                        }
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("You need to wait for the construction team to finish. Maybe come back tomorrow to start a new quest.");
                                    Console.ResetColor();
                                }
                            } else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("You can only accept quests in a construction building.");
                                Console.ResetColor();
                            }
                            break;

                        case "back":
                            if (previousRoom == null)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("You can't go back from here!");
                                Console.ResetColor();
                            }
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
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("You went home to sleep.\n");
                            Console.ResetColor();
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
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Travel where?");
                                    Console.ResetColor();
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
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Unknown room");
                                    Console.ResetColor();
                                }
                                break;
                            }
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("You can't travel yet. You need to buy a car first.");
                            Console.ResetColor();
                            break;

                        case "gointo"://now you can enter a neighbouring room by typing in it's name without knowing the direction
                            if (command.SecondWord == null)
                                {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("You need to specify where to go");
                                Console.ResetColor();
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
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("I can't see that room anywhere!");
                                Console.ResetColor();
                                }
                            }
                            break;

                        case "loan"://loan money
                            if (currentRoom != bank)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("I can only do this in a bank.");
                                Console.ResetColor();
                                break;
                            }
                            if (command.SecondWord == null)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("How much would you like to loan?");
                                Console.ResetColor();
                                break;
                            }
                            if (!bank.TakeLoan(Convert.ToDouble(command.SecondWord)))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Loan request denied.");
                                Console.ResetColor();
                            }
                            break;
                        case "account"://display account info
                            if (currentRoom != bank)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("I can only do this in a bank.");
                                Console.ResetColor();
                                break;
                            }
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\n--- Account Information ---");
                            Console.ResetColor();
                            Console.WriteLine("Balance: " + bank.GetBalance() + bank.currency);
                            Console.WriteLine("Total Debt: " + bank.GetTotalDebt() + bank.currency);
                            Console.WriteLine("Monthly Repayment: " + bank.GetMonthlyRepayment() + bank.currency);
                            Console.WriteLine();
                            break;
                        case "inventory": // Show player inventory
                            PlayerUI.DisplayInventory(player);
                            break;

                        case "buy"://buy stuff
                            if (command.SecondWord == null)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Buy what?");
                                Console.ResetColor();
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
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"Bought {contentsToBuy.Name} for ${contentsToBuy.Price}.\n");
                                        Console.ResetColor();
                                    }
                                    else if(otherShop is Shop materialShop && buyShop.ShortDescription == "Magic Tool Shop")
                                    {
                                        if (contentsToBuy is Item tool && player.BuyItem(tool, bank, materialShop))
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine($"Bought {contentsToBuy.Name} for ${contentsToBuy.Price}.\n");
                                            Console.ResetColor();
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Not enough money.");
                                        Console.ResetColor();
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Item not found.");
                                    Console.ResetColor();
                                }
                            }
                            else 
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("You can't buy that here.");
                                Console.ResetColor();
                            }
                            break;
                        case "work":
                            continuePlaying = false;
                            double amount = 600;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("You earned {0}{1} from work today.\n",amount,bank.currency);
                            Console.ResetColor();
                            bank.AddMoney(amount);
                            Thread.Sleep(3000);
                            break;
                        case "repair":
                            if (currentRoom is ConstructionBuilding)
                            {
                                double repairCost = 0;
                                int repairsNeeded = 0;
                                
                                if (house.foundation > 0 && house.foundationHP < 100)
                                {
                                    double foundationRepair = (100 - house.foundationHP) * 2;
                                    repairCost += foundationRepair;
                                    repairsNeeded++;
                                }
                                if (house.walls > 0 && house.wallsHP < 100)
                                {
                                    double wallsRepair = (100 - house.wallsHP) * 2;
                                    repairCost += wallsRepair;
                                    repairsNeeded++;
                                }
                                if (house.roof > 0 && house.roofHP < 100)
                                {
                                    double roofRepair = (100 - house.roofHP) * 2;
                                    repairCost += roofRepair;
                                    repairsNeeded++;
                                }
                                
                                if (repairsNeeded == 0)
                                {
                                    Console.WriteLine("Your house is in perfect condition!");
                                }
                                else if (bank.GetBalance() >= repairCost)
                                {
                                    bank.AddMoney(-repairCost);
                                    if (house.foundation > 0 && house.foundationHP < 100) house.foundationHP = 100;
                                    if (house.walls > 0 && house.wallsHP < 100) house.wallsHP = 100;
                                    if (house.roof > 0 && house.roofHP < 100) house.roofHP = 100;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("House fully repaired for ${0:F2}!\n", repairCost);
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Not enough money. Repair cost: ${0:F2}", repairCost);
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("You can only repair at a construction building.");
                                Console.ResetColor();
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
            

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n================================");
            Console.WriteLine("  Thank you for playing!");
            Console.WriteLine("================================\n");
            Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You can't go {direction}!");
                Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Targeted location not yet discovered.");
                Console.ResetColor();
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
        

    }
}