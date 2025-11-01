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

        public Game()
        {
            CreateRooms();
            CreateItems();
            CreateMaterials();
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
            Shop? cons_1 = new("Best Build", "Constructions");
            insideOfficeRooms.Add(cons_1);
            Shop? cons_2 = new("Big Build", "Constructions");
            insideOfficeRooms.Add(cons_2);
            Shop? cons_3 = new("Small Build", "Constructions");
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
            Item? hammer = new("Hammer", "A sturdy hammer for building.", 0, 10);
            Item? nails = new("Nails", "A box of small nails.", 0, 5);

            // Assign items to their respective shops
            // Move this into Play() loop eventually
            AssignItem("Magic Tool Shop", hammer);
            AssignItem("Magic Tool Shop", nails);
        }

        private void CreateMaterials()
        {
            Material? wood = new("Wood", "A sturdy piece of wood.", 0.8, 0);
            Material? bricks = new("Bricks", "A stack of red bricks.", 0.6, 0);
            Material? concrete = new("Concrete", "A heavy block of concrete.", 0.4, 0);
            Material? glass = new("Glass", "A transparent sheet of glass.", 0.5, 0);

            // Also move this to Play() loop eventually
            AssignMaterial("Bob's Materials", wood);
            AssignMaterial("Bob's Materials", bricks);
            AssignMaterial("Bob's Materials", concrete);
            AssignMaterial("Bob's Materials", glass);
        }

        public void Play()
        {
            Parser parser = new();

            Player player = new();

            PrintWelcome();

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
                        var target = FindRoomByName(command.SecondWord);
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
                        var targetTravel = FindRoomByName(command.SecondWord);
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
                            if (contentsToBuy != null && contentsToBuy is Item) // checks whether the item is available for purchase for example if you try to purhcase material it will not work
                            {
                                player.BuyItem(contentsToBuy);
                                buyShop.RemoveContents(contentsToBuy); // Remove the item from the shop inventory (also works only for items not materials)
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
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }

        private Room? FindRoomByName(string name, string? name_2 = null)
        {
            if (name_2 == null)
            {
                return allRooms.Find(room => room.ShortDescription.Replace(" ", "").Replace("_", "").Equals(name.Replace(" ", "").Replace("_", ""), StringComparison.OrdinalIgnoreCase));
            }
            return insideOfficeRooms.Find(room => room.ShortDescription.Equals($"{name} {name_2}", StringComparison.OrdinalIgnoreCase));
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
    }
}