namespace BobTheBuilder
{
    public class Game
    {
        private Minimap minimap = new();
        private Room? currentRoom;
        private Room? previousRoom;
        private List<Room> allRooms = new();
        private List<Room> discoveredRooms = new();
        private List<Room> insideOfficeRooms = new();
        
        // Quest system
        private QuestManager? questManager;
        private List<ConstructionCompany> companies = new();
        private Dictionary<string, Material> allMaterials = new(); // Store for offer generation
        
        // Statistics tracking
        private GameStatistics statistics = new();

        public Game()
        {
            // Step 1: Create materials (populates allMaterials dictionary, but don't add to shops yet)
            CreateMaterialsData();
            
            // Step 2: Create companies (needs allMaterials dictionary)
            CreateCompanies();
            
            // Step 3: Initialize quest manager
            questManager = new QuestManager();
            
            // Step 4: Create rooms (needs companies and allMaterials for ConstructionOffices)
            CreateRooms();
            
            // Step 5: Add items and materials to shops (needs shops to exist)
            CreateItems();
            AssignMaterialsToShops();
        }
        
        /// <summary>
        /// Create the three construction companies with their characteristics.
        /// </summary>
        private void CreateCompanies()
        {
            companies.Add(new ConstructionCompany("Best Build", "Balanced Quality", CompanyType.BestBuild));
            companies.Add(new ConstructionCompany("Big Build", "Fast & Cheap", CompanyType.BigBuild));
            companies.Add(new ConstructionCompany("Small Build", "Eco-Friendly", CompanyType.SmallBuild));
        }

        private void CreateRooms()
        {

            House? house = new("House", "This is where we are going to build a house :)");
            allRooms.Add(house);
            Room? street_1 = new("Street_1", "A street leading to our house going into the city. Nothing but nature around us.");
            allRooms.Add(street_1);
            Room? street_main = new("Street_Main", "The main street ouf our town. The street has an office building on one side and a bank on the other");
            allRooms.Add(street_main);
            Room? street_north = new("Street_North", "The north street has two shops: Bob's Materials and the Magic Tool Shop. Funily enoguh, the street doesn't face north.");
            allRooms.Add(street_north);
            Room? office = new("Office Building", "When you step into this old office building you are met with three construction company signs: Best Build, Big Build and Small Build");
            allRooms.Add(office);
            Room? bank = new("Bank", "A nice empty building where at the other side of the room a young teller is smiling at you");
            allRooms.Add(bank);
            Shop? materials = new("Bob's Materials", "Rows and rows of construction materials");
            allRooms.Add(materials);
            Shop? tools = new("Magic Tool Shop", "A nice old man is very happy to tell you everything about a hammer");
            allRooms.Add(tools);
            
            // Create construction offices with their respective companies
            ConstructionOffice? cons_1 = new("Best Build", "A well-organized office with blueprints on the walls.", companies[0], allMaterials);
            insideOfficeRooms.Add(cons_1);
            ConstructionOffice? cons_2 = new("Big Build", "A busy office with workers coming and going.", companies[1], allMaterials);
            insideOfficeRooms.Add(cons_2);
            ConstructionOffice? cons_3 = new("Small Build", "A cozy office decorated with plants and eco-certifications.", companies[2], allMaterials);
            insideOfficeRooms.Add(cons_3);
            
            Room? forest = new("Forest", "Just a bunch of trees and bushes. Nothing to do here.");
            allRooms.Add(forest);

            //north east south west
            //rooms should be connected in a way that if you go west you go back by going east

            //road network
            house.SetExit("west", street_1);
            street_1.SetExits(forest, house, null, street_main);
            street_main.SetExits(office, street_1, bank, street_north);
            street_north.SetExits(tools, street_main, materials, null);

            //street 1(next to the house)
            forest.SetExits(null, null, street_1, office);

            //street_main
            office.SetExits(null, forest, street_main, tools);
            // in the office the gointo(atr) command will let us enter a construction office untill then they will be used as normal rooms
            /*office.SetExits(null, cons_3, street_main, cons_1);
            office.SetExit("north", cons_2);*/

            bank.SetExits(street_main, null, null, materials);

            //street_north
            materials.SetExits(street_north, bank, null, null);
            tools.SetExits(null, office, street_north, null);

            //office so it works before gointo is implemented
            /*cons_1.SetExit("east", office);
            cons_2.SetExit("south", office);
            cons_3.SetExit("west", office);*/

            currentRoom = house;
            minimap.MapRooms(house);

        }

        private void CreateItems()
        {
            Item? hammer = new("Hammer", "A sturdy hammer for building.", 0, 0.8, 10);
            Item? nails = new("Nails", "A box of small nails.", 0, 0.5, 5);

            // Assign items to their respective shops
            // Move this into Play() loop eventually
            AssignItem("Magic Tool Shop", hammer);
            AssignItem("Magic Tool Shop", nails);
        }

        private void CreateMaterialsData()
        {
            // Basic construction materials
            Material? wood = new("Wood", "A sturdy piece of wood.", 0.8, 0.6, 15);
            Material? bricks = new("Bricks", "A stack of red bricks.", 0.6, 0.7, 20);
            Material? concrete = new("Concrete", "A heavy block of concrete.", 0.4, 0.8, 25);
            Material? glass = new("Glass", "A transparent sheet of glass.", 0.5, 0.4, 30);
            Material? insulation = new("Insulation", "Eco-friendly insulation material.", 0.7, 0.5, 35);
            
            // Advanced materials for specialized tasks
            Material? steel = new("Steel", "Strong steel beams for reinforcement.", 0.3, 0.9, 40);
            Material? roofTiles = new("RoofTiles", "Durable ceramic roof tiles.", 0.5, 0.7, 28);
            Material? waterproofing = new("Waterproofing", "Waterproofing membrane.", 0.6, 0.6, 32);
            Material? paint = new("Paint", "Eco-friendly exterior paint.", 0.7, 0.3, 18);
            Material? drywall = new("Drywall", "Interior drywall panels.", 0.5, 0.5, 22);

            // Store in dictionary for offer generation
            allMaterials[wood.Name] = wood;
            allMaterials[bricks.Name] = bricks;
            allMaterials[concrete.Name] = concrete;
            allMaterials[glass.Name] = glass;
            allMaterials[insulation.Name] = insulation;
            allMaterials[steel.Name] = steel;
            allMaterials[roofTiles.Name] = roofTiles;
            allMaterials[waterproofing.Name] = waterproofing;
            allMaterials[paint.Name] = paint;
            allMaterials[drywall.Name] = drywall;
        }

        private void AssignMaterialsToShops()
        {
            // Assign all materials to Bob's Materials shop
            foreach (var material in allMaterials.Values)
            {
                AssignMaterial("Bob's Materials", material);
            }
        }

        public void Play()
        {
            Parser parser = new();

            Player player = new();

            // Initialize all construction offices with the first quest
            UpdateAllOffices();

            PrintWelcome();
            
            // Display the first quest
            if (questManager != null)
            {
                Console.WriteLine("\n📋 Your first quest is ready!");
                DisplayActiveQuest(questManager.ActiveQuest);
            }

            bool continuePlaying = true;
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

                switch(command.Name)
                {
                    case "look":
                        Console.WriteLine(currentRoom?.LongDescription);
                        if (currentRoom is Shop lookShop)
                        {
                            lookShop.DisplayInventory();
                        }
                        else if (currentRoom is ConstructionOffice constructionOffice)
                        {
                            // Display quest offers in construction office
                            constructionOffice.DisplayOffers(player);
                        }
                        
                        // Show active disaster warning if present
                        if (player.ActiveDisaster != null)
                        {
                            Console.WriteLine($"\n⚠️  ACTIVE DISASTER: {player.ActiveDisaster.Name}");
                            Console.WriteLine($"   Use 'repair' command to fix it!");
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
                        PrintHelp();
                        break;
                    
                    case "map":
                        if (currentRoom != null)
                            minimap.Display(currentRoom);
                        break;
                            
                    case "goto":
                        if (command.SecondWord == null)
                        {
                            Console.WriteLine("Go where?");
                            break;
                        }
                        var target = FindRoomByName(command.SecondWord, command.ThirdWord);
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
                        var targetTravel = FindRoomByName(command.SecondWord, command.ThirdWord);
                        if (targetTravel != null)
                        {
                            Travel(targetTravel);
                        }
                        else
                            Console.WriteLine("Unknown room");
                        break;

                    case "gointo":
                        if (currentRoom != null && !currentRoom.ShortDescription.Equals("Office Building"))
                        {
                            Console.WriteLine("You can only go into the Office Building.");
                            break;
                        }
                        if (command.SecondWord == null || command.ThirdWord == null)
                        {
                            Console.WriteLine("Go into what?");
                            break;
                        }
                        else
                        {
                            var targetRoom = FindRoomByName(command.SecondWord, command.ThirdWord);
                            if (targetRoom != null)
                            {
                                GoInto(targetRoom);
                            }
                            else
                            {
                                Console.WriteLine("Unknown room");
                            }
                            break;
                        }

                    case "inventory": // Show player inventory
                        player.DisplayInventory(); // Displays only items bcs you can't get materials to your inventory yet. If we want to implement buying materials we just delete the condition in "buy"
                        break;

                    case "buy":
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
                                // Now supports both items AND materials!
                                player.BuyItem(contentsToBuy);
                                // Note: We don't remove materials from shop (unlimited stock)
                                if (contentsToBuy is Item)
                                {
                                    buyShop.RemoveContents(contentsToBuy); // Remove items only
                                }
                            }
                            else
                            {
                                Console.WriteLine("Item/Material not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You can only buy things in shops.");
                        }
                        break;

                    case "quest":
                        // Display active quest information
                        if (questManager != null)
                        {
                            DisplayActiveQuest(questManager.ActiveQuest);
                        }
                        break;

                    case "accept":
                        // Accept a quest offer (only works in ConstructionOffice)
                        if (currentRoom is ConstructionOffice office)
                        {
                            int optionNumber = 1; // Default to option 1
                            
                            // Check if player specified an option number
                            if (command.SecondWord != null && int.TryParse(command.SecondWord, out int parsedOption))
                            {
                                optionNumber = parsedOption;
                            }
                            
                            AcceptQuestOffer(office, player, optionNumber);
                        }
                        else
                        {
                            Console.WriteLine("You can only accept quest offers in construction company offices.");
                        }
                        break;

                    case "loan":
                        // Take out a loan from the bank
                        if (currentRoom?.ShortDescription == "Bank")
                        {
                            if (command.SecondWord == null || !double.TryParse(command.SecondWord, out double loanAmount))
                            {
                                Console.WriteLine("Please specify an amount. Usage: loan <amount>");
                                Console.WriteLine($"Available credit: ${Bank.GetAvailableCredit(player.CurrentLoan)}");
                                break;
                            }
                            
                            TakeLoan(player, loanAmount);
                        }
                        else
                        {
                            Console.WriteLine("You can only take out loans at the Bank.");
                        }
                        break;

                    case "repay":
                        // Repay loan
                        if (currentRoom?.ShortDescription == "Bank")
                        {
                            if (command.SecondWord == null || !double.TryParse(command.SecondWord, out double repayAmount))
                            {
                                Console.WriteLine("Please specify an amount. Usage: repay <amount>");
                                Console.WriteLine($"Current loan: ${player.CurrentLoan}");
                                break;
                            }
                            
                            RepayLoan(player, repayAmount);
                        }
                        else
                        {
                            Console.WriteLine("You can only repay loans at the Bank.");
                        }
                        break;

                    case "stats":
                        // Display current game statistics
                        statistics.DisplayStatistics(player);
                        break;

                    case "repair":
                        // Repair disaster damage
                        if (player.ActiveDisaster != null)
                        {
                            RepairDisaster(player);
                        }
                        else
                        {
                            Console.WriteLine("There's nothing that needs repairing right now.");
                        }
                        break;

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing Bob the Builder!");
        }

        private void Move(string direction)
        {
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
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

        private void GoInto(Room targetConstruction)
        {
            previousRoom = currentRoom;
            currentRoom = targetConstruction;
        }


        private static void PrintWelcome()
        {
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠛⣛⣩⣭⣭⣭⣭⣭⣭⡄⢲⣤⣤⣭⣙⠛⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠟⢉⣤⣴⣿⣿⣿⣿⣿⣿⠛⠟⠋⢠⣾⣿⣿⣿⣿⣷⡀⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⠟⠉⠭⠭⢍⠉⠀⠐⣶⣄⠙⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢋⣠⠞⢋⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣶⣌⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⠟⠟⡋⠀⠐⠒⣾⣾⣇⠘⠀⣾⣿⡆⠀⠟⠛⣛⠛⠿⣿⣿⣿⣿⣿⣿⣿⡿⠋⣤⡿⢋⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⡈⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⠀⡿⣿⣗⣠⡄⣻⣿⣿⣿⣿⣿⣿⣷⠀⢶⣾⣿⣷⢀⣿⣿⣿⣿⣿⣿⠟⣠⣾⣿⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⡄⠃⠸⣿⣿⣿⣿⣿⣿⣿⡿⢻⣿⣦⣥⣼⣿⣿⠋⢠⣿⣿⣿⣿⣿⠃⣴⣿⣿⡿⠿⠿⠿⠿⠿⠿⠿⠿⣶⣶⣦⣬⣭⣛⡛⠛⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣷⡄⠱⠙⣿⣿⣿⣿⣿⠃⣴⣾⣿⣿⣿⣿⡿⠁⢀⣿⣿⣿⣿⡿⠁⡼⠟⠋⣀⣤⣤⣤⣤⣤⣀⣀⡀⠀⠀⠀⠀⠉⠙⠛⠛⠿⢶⣦⣌⡙⠻⢿⣿⣿⣿⣿⣿⣿⣿⡆⠹⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣆⠁⡈⠻⣿⣿⣿⣶⣿⣿⣿⡿⢛⡝⢡⣴⣿⡿⠿⠛⠛⣁⠌⠀⣰⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣶⣶⣶⣤⣄⡀⠀⠀⠈⠉⠙⠳⢶⣌⡙⠻⣿⣿⣿⣿⣿⡆⢻⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣷⣌⠀⠋⢹⣿⣿⣿⣿⣿⡆⢋⣰⣷⠏⣠⣄⣀⡀⠈⠀⠀⢰⣿⣿⣿⣿⣿⣿⡿⠛⠁⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣤⣤⣀⡀⠀⠉⠛⠶⣌⡻⣿⣿⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⠀⠀⣿⣿⣿⣿⣿⣿⡇⠈⣻⠃⣰⣿⣿⡟⣿⡶⠀⠀⣿⣿⣿⣿⣿⣿⣿⣄⠀⢀⣸⣿⣿⣿⣿⣿⠟⣫⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⣄⠀⠈⠙⢮⣻⣿⠇⢸⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⡿⠀⠰⢿⣿⣿⡿⠿⠿⠇⠀⠛⡀⠃⢸⣿⣇⠘⠃⢠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠃⣠⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠁⠀⢩⣿⣿⣿⣿⣄⠙⣿⢸⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⡿⠟⠃⠀⠐⠈⣹⣧⣴⣶⡆⠀⠃⠸⣷⣄⠐⢹⣿⡗⠐⠚⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢁⣤⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⣤⣤⣾⣿⣿⣿⣿⣿⡆⠹⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⡿⠋⠁⠀⠀⠀⢠⣠⠀⠙⣿⣿⣿⣿⡀⠀⠀⣿⣿⣷⣦⠉⢣⠀⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⣤⣤⣾⣿⣿⣿⣿⣿⡆⠹⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⡄⠀⣿⣄⠀⠈⠀⠀⠀⠘⠛⣋⣉⠀⢴⣆⢸⣿⣿⣿⣷⣄⠀⠀⢘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⣤⣀⣀⣈⣙⣓⣂⣉⣹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡗⠀⠀⢿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣷⡀⠛⠿⠀⢀⣤⣶⣿⡄⠘⣿⣿⣧⠈⢢⡀⢻⣿⣿⣿⣿⣿⣆⡀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠙⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠀⣴⣷⣄⠙⢿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣇⠈⠀⠀⠸⣿⣿⣿⣷⡀⠙⠛⢋⣠⡈⠱⡀⢿⣿⣿⣿⣿⣿⣷⡈⠻⣿⣿⣿⣿⣿⣿⣿⣿⠻⣦⡀⠈⠛⠛⠛⠿⠿⠟⢛⣻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⢠⣾⣿⣿⣿⣷⢸⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣆⢂⠀⠀⠹⡿⠛⠋⠀⢸⣿⣿⣿⣿⠀⠑⠈⢿⣿⣿⣿⣿⣿⣿⡄⠈⠏⠙⢿⣿⣿⣿⣿⣦⡈⠻⢷⣦⣤⣤⣤⣴⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⢀⣾⣿⣿⣿⣿⣿⠈⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⡄⢀⠀⣤⠀⢰⡞⠇⠈⢿⣿⠿⣯⣄⣤⣤⠀⠬⣉⠻⠟⠁⠀⠀⠐⠂⠀⠀⠻⢿⣿⣿⣿⣿⣷⣶⣬⣭⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠈⢹⣿⣿⣿⠟⢁⣴⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣷⡀⢣⣼⡀⠈⠁⠀⣀⣈⣀⡀⠻⠿⢿⣿⣦⠀⠀⠑⠀⠀⢀⣀⣻⣶⣆⠀⠀⠀⠈⠙⠻⠿⢿⣿⣟⣻⠿⠿⢿⣿⣿⣿⣿⣿⡿⠟⠋⠁⠈⠀⠠⠤⢂⣠⣤⣤⣤⣴⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣆⠛⠗⠀⢰⣿⣿⣿⠟⠀⣰⣦⡤⠀⣄⣀⣤⡄⠀⠀⢸⣿⣿⣿⣯⣀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠉⠙⠒⠘⠛⠛⢉⣈⣡⡀⠤⣤⣰⠒⠀⡀⠘⠻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣧⡐⠠⡈⠙⠛⠃⠀⠚⠛⠉⠀⣴⣿⣿⣿⡇⢤⠀⢸⣿⣿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠸⠿⠿⠿⣷⠀⡄⢸⣿⣿⣿⣿⣿⠀⠙⠛⠃⠀⠄⠙⠒⣦⣬⡙⠻⢿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡙⠂⠀⠘⣿⣷⣶⡦⠀⠉⠁⠀⢿⠃⠀⠀⢸⣿⣿⣿⣿⣿⣿⠀⣿⣿⣿⡇⠀⢷⣶⣶⠘⠈⡇⢸⣿⣿⣿⣿⣏⠀⠀⣾⣿⣶⣦⡀⢹⣿⣿⣯⡰⢤⡉⠛⠿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⣤⡀⢄⡉⠛⢁⡀⠀⠀⠀⢸⡄⠀⠀⢸⣿⣿⣿⣿⣿⡟⠀⣿⣿⣿⡇⠀⠺⠛⠞⠀⠀⠀⣼⣿⣿⣿⣿⣿⠀⠰⣿⣿⣿⣿⠁⣄⠉⠻⣿⣿⣤⣬⡀⠐⠲⠍⣉⠛⢻");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡈⠢⡈⠑⠆⠀⠐⠿⠃⠀⠀⠘⣿⣿⣿⣿⣿⣿⣄⡈⠙⠋⠀⠠⠶⠶⠆⠀⠀⣠⣿⣿⣿⣿⣿⣿⠀⠀⠈⠉⠻⠿⢰⠿⠁⠠⢈⡉⠻⢿⠟⠀⣴⣷⠄⠘⢠");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣤⣉⠐⠤⠀⠀⣠⣄⣚⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⣻⣷⣶⣦⣶⣿⣶⣿⣿⣿⣿⣿⣿⠿⠿⣿⣀⣀⡀⠀⣶⡄⠀⠀⣰⣿⣿⣦⣄⠀⠚⠋⢁⣄⡄⢸");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⢠⣾⡟⣿⣿⣶⠀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠋⠁⠤⠠⣭⣿⣿⣧⢠⡌⠻⠂⠘⢿⡿⠟⢋⣤⣤⣤⣄⠈⠿⢣⠘");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⡇⢹⣿⡿⠀⠈⠻⠛⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣤⣤⣄⠀⠀⡇⠁⣿⣿⢸⣿⣶⣤⡄⠀⠀⠠⣿⣿⣿⣿⡟⢠⡆⢸⠀");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠇⣼⣿⡁⢠⣶⡄⢠⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠇⠀⡇⢸⢻⣿⢸⣿⣿⡟⢁⡴⠀⢀⣤⣉⠙⠋⣠⡿⠃⠸⠀");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⢿⣿⣇⠀⣤⡄⠘⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⠀⢠⠀⣀⣇⣀⠸⠿⠀⢿⡟⠀⠾⠃⠠⢿⣿⣿⠇⣸⣿⣷⠀⠀⣰");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠁⣸⣿⡿⠀⣿⡇⠀⠀⠀⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠀⣿⠀⣿⠀⠙⠋⠙⠀⠀⠀⠀⣴⣶⣤⣄⡉⠁⠠⣿⣿⣇⡌⢠⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠀⣰⣾⣿⠀⣿⣿⣿⣿⣿⠋⠁⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠀⣰⣾⣿⠀⣿⣿⣿⣿⣿⠋⠁⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣾⡟⠋⠁⠀⣿⣿⣿⣿⡇⡐⢀⣽⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⠙⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⠀⠃⣸⣿⣿⣿⡘⠋⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠋⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡌⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠀⢾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠸⠟⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⡘⠀⠀⠈⢻⣿⣿⣿⣿⣿⣿⣿⡟⠛⠻⠿⠏⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡷⢀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠇⠀⠀⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣾⠓⣤⠄⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠈⢀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠇⣨⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡷⠀⠀⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣆⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⠠⠿⠛⠻⠿⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠐⠀⠂⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠟⠋⡀⢉⠛⠻⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢋⢡⠤⠶⣶⣦⣶⣶⣶⣶⣤⣤⣤⣤⣤⣭⣭⠉⠉⢀⣼⣴⠰⠛⠛⠿⠿⠛⠓⠒⢉⣉⣥⣤⣴⣶⣿⣿⣿⣿⣷⣶⠒⢬⡙⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢁⠘⠀⣀⣤⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠃⢨⣿⣿⢠⠀⠀⠀⠀⠀⠀⠀⠉⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣄⠘⠆⢻⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢁⠂⠀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠀⠀⠀⠉⠁⠈⠀⠀⠒⠀⠀⣠⣴⡆⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⢸⠀⠻⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠃⣼⠀⠰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢛⣁⣠⣴⣶⡆⠀⠀⢰⣶⣦⣤⣄⣀⣀⠉⠙⠛⠻⠿⠿⠿⠿⢿⣿⣿⣿⣿⣿⣿⣿⠯⠤⠀⢹⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⢆⣤⣤⣄⣀⣈⣉⣉⣉⣉⣉⡉⠙⠋⠉⣁⣠⣾⣿⣿⣿⠿⠛⠐⢁⠀⠄⠙⠛⠻⠿⠿⠿⣿⣿⣷⣶⣦⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣶⣿⠃⢸⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣏⠀⠈⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠟⣛⠉⠀⣐⣤⣶⣿⣿⣷⣦⣥⣄⣒⣤⣀⠀⠨⠉⠛⠻⠿⠿⠿⠿⠿⠿⠿⠿⠿⠟⠛⠛⠋⠐⣸⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⣀⠀⠤⠈⢉⣉⠛⠛⠛⠛⣉⣉⠉⠩⠄⠀⢂⣉⣥⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣶⣦⣴⣶⣶⣖⡒⠒⠒⠒⢒⣦⣤⣉⣤⣴⣾⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣶⡶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣿⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("Welcome to the Bob the Builder!");
            Console.WriteLine("Bob the Builder is a new, incredibly boring base building game.");
            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("You just bought a plot of land. Now you have to build your dream house.");
            Console.WriteLine();
            Console.WriteLine("=== NAVIGATION ===");
            Console.WriteLine("  'north', 'south', 'east', 'west' - Move between connected rooms");
            Console.WriteLine("  'back' - Return to previous room");
            Console.WriteLine("  'goto <room>' - Get directions/path to a discovered room");
            Console.WriteLine("  'travel <room>' - Fast travel to any discovered room");
            Console.WriteLine("  'gointo <company>' - Enter construction company office (e.g., 'gointo best build')");
            Console.WriteLine();
            Console.WriteLine("=== QUESTS & CONSTRUCTION ===");
            Console.WriteLine("  'quest' - View current active quest details");
            Console.WriteLine("  'look' - View room details, shop inventory, or quest offers");
            Console.WriteLine("  'accept' or 'accept <number>' - Accept a quest offer (in construction offices)");
            Console.WriteLine("  'repair' - Fix disaster damage (requires materials and money)");
            Console.WriteLine();
            Console.WriteLine("=== SHOPPING & INVENTORY ===");
            Console.WriteLine("  'buy <item/material>' - Purchase items or materials in shops");
            Console.WriteLine("  'inventory' - View your tools, materials, and money");
            Console.WriteLine();
            Console.WriteLine("=== BANKING ===");
            Console.WriteLine("  'loan <amount>' - Take out a loan at the Bank (5% interest per turn)");
            Console.WriteLine("  'repay <amount>' - Repay part or all of your loan at the Bank");
            Console.WriteLine();
            Console.WriteLine("=== OTHER ===");
            Console.WriteLine("  'map' - Display minimap of discovered areas");
            Console.WriteLine("  'stats' - View your current game statistics and progress");
            Console.WriteLine("  'help' - Show this help message");
            Console.WriteLine("  'quit' - Exit the game");
            Console.WriteLine();
        }

        private Room? FindRoomByName(string name, string? name_2 = null)
        {
            string searchName;
            
            if (name_2 == null)
            {
                searchName = name;
            }
            else
            {
                // Combine the two words
                searchName = $"{name} {name_2}";
            }
            
            // Remove spaces, underscores, and apostrophes for comparison
            string normalizedSearch = searchName.Replace(" ", "").Replace("_", "").Replace("'", "");
            
            // First, try to find in allRooms
            var room = allRooms.Find(r => 
                r.ShortDescription.Replace(" ", "").Replace("_", "").Replace("'", "")
                .Equals(normalizedSearch, StringComparison.OrdinalIgnoreCase));
            
            if (room != null)
                return room;
            
            // If not found, try insideOfficeRooms (for construction offices)
            return insideOfficeRooms.Find(r => 
                r.ShortDescription.Replace(" ", "").Replace("_", "").Replace("'", "")
                .Equals(normalizedSearch, StringComparison.OrdinalIgnoreCase));
        }

        private void AssignItem(string shopShortDescription, Item items) // Assign item to shop dynamically with this method, I think it will be handy later when we dig deeper into the turn based system
        {
            var room = allRooms.Find(r => r.ShortDescription.Equals(shopShortDescription, StringComparison.OrdinalIgnoreCase));
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
            var room = allRooms.Find(r => r.ShortDescription.Equals(shopShortDescription, StringComparison.OrdinalIgnoreCase));
            if (room is Shop shop)
            {
                shop.AddContents(material);
            }
            else
            {
                Console.WriteLine($"Shop '{shopShortDescription}' not found to add material '{material.Name}'.");
            }
        }

        /// <summary>
        /// Display information about the active quest.
        /// </summary>
        private void DisplayActiveQuest(Quest quest)
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  📋 ACTIVE QUEST");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
            Console.WriteLine($"\n{quest.Name}");
            Console.WriteLine($"{quest.Description}");
            Console.WriteLine($"\nChapter: {quest.Chapter}");
            Console.WriteLine($"\nVisit construction company offices to compare offers and prices!");
            Console.WriteLine();
        }

        /// <summary>
        /// Handle accepting a quest offer in a construction office.
        /// </summary>
        private void AcceptQuestOffer(ConstructionOffice office, Player player, int optionNumber)
        {
            if (questManager == null) return;

            // Check if there's an active disaster that needs repair
            if (player.ActiveDisaster != null)
            {
                Console.WriteLine("❌ You cannot start new construction work while there's disaster damage!");
                Console.WriteLine($"   Current disaster: {player.ActiveDisaster.Name}");
                Console.WriteLine("\n💡 Use the 'repair' command to fix the damage first.");
                return;
            }

            QuestOption? offer = office.GetOffer(optionNumber);
            
            if (offer == null)
            {
                Console.WriteLine($"❌ Invalid option number. Please choose 1-{office.GetAllOffers().Count}.");
                return;
            }

            // Check if player can accept (has materials and money)
            if (!office.CanAcceptOffer(player, offer, out string errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.WriteLine("\n💡 Tip: Visit Bob's Materials to buy required materials!");
                return;
            }

            // Complete the quest!
            Console.WriteLine("\n✓ Checking requirements...");
            Console.WriteLine($"✓ Materials: {offer.FormatRequiredMaterials()} ✓");
            Console.WriteLine($"✓ Service Cost: ${offer.ServiceCost} ✓");
            Console.WriteLine("\nProcessing...\n");

            // Deduct costs and materials
            player.Money -= offer.ServiceCost;
            player.ConsumeMaterials(offer.RequiredMaterials);

            // Record statistics
            double materialCost = offer.CalculateTotalMaterialCost(allMaterials);
            double totalCost = offer.ServiceCost + materialCost;
            statistics.RecordQuestCost(totalCost);
            statistics.RecordQuestQuality(offer.SustainabilityRating, offer.QualityRating, offer.CompanyName);
            statistics.RecordMaterialsUsed(offer.RequiredMaterials);

            // Complete quest
            Quest completedQuest = questManager.ActiveQuest;
            questManager.CompleteCurrentQuest(offer);

            // Display completion info
            DisplayQuestCompletion(completedQuest, offer, player);

            // Check if game is complete
            if (questManager.IsGameComplete)
            {
                DisplayGameEnd(player);
                return;
            }

            // Update all construction offices with new active quest
            UpdateAllOffices();
        }

        /// <summary>
        /// Display quest completion information.
        /// </summary>
        private void DisplayQuestCompletion(Quest completedQuest, QuestOption chosenOption, Player player)
        {
            double materialCost = chosenOption.CalculateTotalMaterialCost(allMaterials);
            double totalCost = chosenOption.ServiceCost + materialCost;

            Console.WriteLine("\n════════════════════════════════════════════════════════════════");
            Console.WriteLine("                   🎉 QUEST COMPLETED! 🎉");
            Console.WriteLine("════════════════════════════════════════════════════════════════");
            Console.WriteLine($"\n{completedQuest.Name}");
            Console.WriteLine($"✓ Contractor: {chosenOption.CompanyName}");
            Console.WriteLine($"✓ Service Cost: ${chosenOption.ServiceCost}");
            Console.WriteLine($"✓ Materials: {chosenOption.FormatRequiredMaterials()} (${materialCost})");
            Console.WriteLine($"✓ Total Spent: ${totalCost}");
            Console.WriteLine($"\n📊 Quest Stats:");
            Console.WriteLine($"   🌱 Sustainability: +{chosenOption.SustainabilityRating}/10");
            Console.WriteLine($"   🏗️  Quality: +{chosenOption.QualityRating}/10");
            
            Console.WriteLine("────────────────────────────────────────────────────────────────");
            Console.WriteLine("                      💰 TURN SUMMARY");
            Console.WriteLine("────────────────────────────────────────────────────────────────");
            Console.WriteLine($"Turn {player.CurrentTurn} → Turn {player.CurrentTurn + 1}");
            Console.WriteLine($"Monthly Income: +$30");
            
            // Apply monthly income
            player.Money += 30;
            statistics.RecordIncome(30);
            
            // Apply loan interest if player has debt
            if (player.CurrentLoan > 0)
            {
                double interest = Bank.CalculateInterest(player.CurrentLoan);
                player.CurrentLoan += interest;
                statistics.RecordInterest(interest);
                Console.WriteLine($"💳 Loan Interest ({Bank.InterestRate * 100}%): +${interest} debt");
                Console.WriteLine($"   Total Loan: ${player.CurrentLoan}");
            }
            
            player.CurrentTurn++;
            
            // Track chapter completion times
            if (questManager!.IsChapterJustCompleted())
            {
                QuestChapter? completedChapter = questManager.GetCompletedChapter();
                if (completedChapter == QuestChapter.Foundation)
                {
                    statistics.FoundationTurns = player.CurrentTurn;
                }
                else if (completedChapter == QuestChapter.Walls)
                {
                    statistics.WallsTurns = player.CurrentTurn - statistics.FoundationTurns;
                }
                else if (completedChapter == QuestChapter.Roof)
                {
                    statistics.RoofTurns = player.CurrentTurn - statistics.FoundationTurns - statistics.WallsTurns;
                }
                
                // Trigger potential disaster based on chapter quality
                if (completedChapter.HasValue)
                {
                    Disaster? disaster = DisasterSystem.TriggerDisaster(completedChapter.Value, statistics.GetAverageQuality());
                    if (disaster != null)
                    {
                        player.ActiveDisaster = disaster;
                    }
                    else
                    {
                        // No disaster! Good quality work paid off
                        Console.WriteLine($"\n🎉 Chapter Complete! No issues detected.");
                        Console.WriteLine($"   Your quality work on {completedChapter.Value} has paid off!");
                    }
                }
            }
            
            Console.WriteLine($"New Balance: ${player.Money}");

            if (!questManager!.IsGameComplete)
            {
                Quest nextQuest = questManager.ActiveQuest;
                Console.WriteLine("\n────────────────────────────────────────────────────────────────");
                Console.WriteLine("                   📋 NEW QUEST ACTIVATED");
                Console.WriteLine("────────────────────────────────────────────────────────────────");
                Console.WriteLine($"\n{nextQuest.Name}");
                Console.WriteLine($"{nextQuest.Description}");
                Console.WriteLine($"\nVisit construction companies to compare offers!");
            }
            
            Console.WriteLine("\n════════════════════════════════════════════════════════════════");
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            
            // Display disaster if one occurred
            if (player.ActiveDisaster != null)
            {
                DisasterSystem.DisplayDisaster(player.ActiveDisaster);
                Console.WriteLine("\n⚠️  WARNING: You cannot proceed with new quests until disaster is repaired!");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Update all construction offices with the current active quest.
        /// </summary>
        private void UpdateAllOffices()
        {
            if (questManager == null) return;

            foreach (var room in insideOfficeRooms)
            {
                if (room is ConstructionOffice office)
                {
                    office.UpdateOffers(questManager.ActiveQuest);
                }
            }
        }

        /// <summary>
        /// Player takes out a loan from the bank.
        /// </summary>
        private void TakeLoan(Player player, double amount)
        {
            // Validate loan amount
            if (amount < Bank.MinLoanAmount)
            {
                Console.WriteLine($"❌ Minimum loan amount is ${Bank.MinLoanAmount}.");
                return;
            }

            if (!Bank.CanTakeLoan(player.CurrentLoan, amount))
            {
                Console.WriteLine($"❌ Loan denied! Your credit limit is ${Bank.MaxTotalLoan}.");
                Console.WriteLine($"   Current loan: ${player.CurrentLoan}");
                Console.WriteLine($"   Available credit: ${Bank.GetAvailableCredit(player.CurrentLoan)}");
                return;
            }

            // Grant loan
            player.CurrentLoan += amount;
            player.Money += amount;

            Console.WriteLine("\n✅ Loan approved!");
            Console.WriteLine("────────────────────────────────────────────");
            Console.WriteLine($"Loan amount: ${amount}");
            Console.WriteLine($"Interest rate: {Bank.InterestRate * 100}% per turn");
            Console.WriteLine($"Total loan: ${player.CurrentLoan}");
            Console.WriteLine($"Available credit: ${Bank.GetAvailableCredit(player.CurrentLoan)}");
            Console.WriteLine($"New balance: ${player.Money}");
            Console.WriteLine("────────────────────────────────────────────");
            Console.WriteLine($"⚠️  Interest of ${Bank.CalculateInterest(player.CurrentLoan)} will be added each turn!");
        }

        /// <summary>
        /// Player repays part or all of their loan.
        /// </summary>
        private void RepayLoan(Player player, double amount)
        {
            // Check if player has any loan
            if (player.CurrentLoan <= 0)
            {
                Console.WriteLine("You don't have any outstanding loans.");
                return;
            }

            // Check if player has enough money
            if (amount > player.Money)
            {
                Console.WriteLine($"❌ You don't have enough money. Your balance: ${player.Money}");
                return;
            }

            // Can't repay more than the loan amount
            if (amount > player.CurrentLoan)
            {
                Console.WriteLine($"You only owe ${player.CurrentLoan}. Repaying that amount instead.");
                amount = player.CurrentLoan;
            }

            // Process repayment
            player.CurrentLoan -= amount;
            player.Money -= amount;

            Console.WriteLine("\n✅ Loan repayment successful!");
            Console.WriteLine("────────────────────────────────────────────");
            Console.WriteLine($"Repaid: ${amount}");
            Console.WriteLine($"Remaining loan: ${player.CurrentLoan}");
            Console.WriteLine($"New balance: ${player.Money}");
            Console.WriteLine("────────────────────────────────────────────");

            if (player.CurrentLoan == 0)
            {
                Console.WriteLine("🎉 Congratulations! You've paid off your loan!");
            }
        }

        /// <summary>
        /// Repair disaster damage.
        /// </summary>
        private void RepairDisaster(Player player)
        {
            if (player.ActiveDisaster == null) return;
            
            Disaster disaster = player.ActiveDisaster;
            
            // Check if player has required materials
            if (!player.HasMaterials(disaster.RequiredMaterials))
            {
                var missing = player.GetMissingMaterials(disaster.RequiredMaterials);
                Console.WriteLine("❌ You don't have the required materials!");
                Console.WriteLine($"\nMissing materials: {missing}");
                Console.WriteLine("\n💡 Visit Bob's Materials to purchase what you need.");
                return;
            }
            
            // Check if player has enough money for labor
            if (player.Money < disaster.BaseCost)
            {
                Console.WriteLine($"❌ You don't have enough money for labor costs!");
                Console.WriteLine($"   Required: ${disaster.BaseCost}");
                Console.WriteLine($"   Your balance: ${player.Money}");
                Console.WriteLine("\n💡 Visit the Bank to take out a loan if needed.");
                return;
            }
            
            // Process repair
            Console.WriteLine("\n🔧 Starting repairs...");
            Console.WriteLine($"   Using materials: {FormatMaterialsList(disaster.RequiredMaterials)}");
            Console.WriteLine($"   Paying labor: ${disaster.BaseCost}");
            
            player.ConsumeMaterials(disaster.RequiredMaterials);
            player.Money -= disaster.BaseCost;
            
            // Record repair cost in statistics
            double materialCost = CalculateMaterialCost(disaster.RequiredMaterials);
            statistics.RecordQuestCost(disaster.BaseCost + materialCost);
            
            Console.WriteLine("\n✅ Repairs completed successfully!");
            Console.WriteLine("────────────────────────────────────────────");
            Console.WriteLine($"Total cost: ${disaster.BaseCost + materialCost}");
            Console.WriteLine($"New balance: ${player.Money}");
            Console.WriteLine("────────────────────────────────────────────");
            Console.WriteLine("\n🎉 The house is safe again! You can continue with construction.");
            
            // Clear the disaster
            player.ActiveDisaster = null;
        }
        
        /// <summary>
        /// Format materials list for display.
        /// </summary>
        private string FormatMaterialsList(Dictionary<string, int> materials)
        {
            return string.Join(", ", materials.Select(m => $"{m.Value}x {m.Key}"));
        }
        
        /// <summary>
        /// Calculate total cost of materials.
        /// </summary>
        private double CalculateMaterialCost(Dictionary<string, int> materials)
        {
            double total = 0;
            foreach (var mat in materials)
            {
                if (allMaterials.ContainsKey(mat.Key))
                {
                    total += allMaterials[mat.Key].Price * mat.Value;
                }
            }
            return total;
        }

        /// <summary>
        /// Display game ending screen.
        /// </summary>
        private void DisplayGameEnd(Player player)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  🎊 GAME COMPLETE! 🎊                      ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ Congratulations! You've completed your house!             ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            
            // Display comprehensive statistics
            statistics.DisplayStatistics(player);
            
            Console.WriteLine("\nThank you for playing Bob the Builder!");
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}