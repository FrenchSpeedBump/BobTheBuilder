namespace BobTheBuilder
{
    public class Game
    {
        private Minimap minimap = new();
        private Room? currentRoom;
        private Room? previousRoom;
        private List<Room> allRooms = new();

        public Game()
        {
            CreateRooms();
        }
        
        private void CreateRooms()
        {
  
            Room? house = new("House", "This is where we are going to build a house :)");
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
            Room? materials = new("Bob's Materials", "Rows and rows of construction materials");
            allRooms.Add(materials);
            Room? tools = new("Magic Tool Shop", "A nice old man is very happy to tell you everything about a hammer");
            allRooms.Add(tools);
            Room? cons_1 = new("Best Build", "Constructions");
            allRooms.Add(cons_1);
            Room? cons_2 = new("Big Build", "Constructions");
            allRooms.Add(cons_2);
            Room? cons_3 = new("Small Build", "Constructions");
            allRooms.Add(cons_3);
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

        public void Play()
        {
            Parser parser = new();

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
                        // Find room by short description and show directions
                        var target = FindRoomByName(command.SecondWord);
                        if (target != null && currentRoom != null)
                        {
                            Console.WriteLine($"Direction: {minimap.GetDirectionTo(currentRoom, target)}");
                        }
                        else
                            Console.WriteLine("Unknown room");
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

//FINDING FUNCTION FOR "GOTO" COMMAND
        private Room? FindRoomByName(string name)
        {
            // Case-insensitive partial match
            return allRooms.Find(room => 
                room.ShortDescription.Replace(" ", "").Replace("_", "")
                .Equals(name.Replace(" ", "").Replace("_", ""), StringComparison.OrdinalIgnoreCase));
        }
    }
}