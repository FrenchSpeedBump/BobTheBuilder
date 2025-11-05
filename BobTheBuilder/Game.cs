namespace BobTheBuilder
{
    public class Game
    {
        private Minimap minimap = new();
        private Room? currentRoom;
        private Room? previousRoom;
        private List<Room> discoveredRooms = new();
        private Bank? bank;

        public Game()
        {
            CreateRooms();
        }
        
        private void CreateRooms()
        {
  
            Room? house = new("House", "This is where we are going to build a house :)");
            Room? street_1 = new("Street_1", "A street leading to our house going into the city. Nothing but nature around us.");
            Room? street_main = new("Street_Main", "The main street ouf our town. The street has an office building on one side and a bank on the other"); 
            Room? street_north = new("Street_North", "The north street has two shops: Bob's Materials and the Magic Tool Shop. Funily enoguh, the street doesn't face north.");
            Room? office = new("Office Building", "When you step into this old office building you are met with three construction company signs: Best Build, Big Build and Small Build");         
            bank = new("Bank", "A nice empty building where at the other side of the room a young teller is smiling at you");
            Room? materials = new("Bob's Materials", "Rows and rows of construction materials");  
            Room? tools = new("Magic Tool Shop", "A nice old man is very happy to tell you everything about a hammer");   
            Room? cons_1 = new("Best Build", "Constructions");
            Room? cons_2 = new("Big Build", "Constructions");
            Room? cons_3 = new("Small Build", "Constructions");
            Room? forest = new("Forest", "Just a bunch of trees and bushes. Nothing to do here.");

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
                Console.WriteLine("===================================");
                Console.WriteLine("Money = "+bank.getBalance());
                Console.WriteLine("===================================");
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
                            
                    case "goto"://maybe change name so its not similar to gointo
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

                    case "gointo"://now you can enter a neighbouring room by typing in it's name without knowing the direction
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
                        break;

                    case "loan":
                        if(currentRoom != bank)
                        {
                            Console.WriteLine("I can only do this in a bank.");
                        }
                        if (command.SecondWord == null)
                        {
                            Console.WriteLine("How much would you like to loan?");
                        }
                        bank.takeLoan(Convert.ToDouble(command.SecondWord));
                        break;
                    case "account":
                        if (currentRoom != bank)
                        {
                            Console.WriteLine("I can only do this in a bank.");
                        }
                        Console.WriteLine("Account information:");
                        Console.WriteLine("Account balace: "+bank.getBalance());
                        Console.WriteLine("Total debt:" + bank.getTotalDebt());
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
        private string IsNeighbour(string room)
        {
            foreach (var direction in currentRoom?.Exits)
            {
                if (direction.Value.ShortDescription == room)
                {
                    return direction.Key;
                }
            }
            return "";
        }
        private void Travel(Room targetRoom)//add discovered trait to rooms
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


        private static void PrintWelcome()//this needs to be moved to a separate file and just be called 
        {
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠛⣛⣩⣭⣭⣭⣭⣭⣭⡄⢲⣤⣤⣭⣙⠛⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠟⢉⣤⣴⣿⣿⣿⣿⣿⣿⠛⠟⠋⢠⣾⣿⣿⣿⣿⣷⡀⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⠟⠉⠭⠭⢍⠉⠀⠐⣶⣄⠙⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢋⣠⠞⢋⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣶⣌⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⠟⠟⡋⠀⠐⠒⣾⣾⣇⠘⠀⣾⣿⡆⠀⠟⠛⣛⠛⠿⣿⣿⣿⣿⣿⣿⣿⡿⠋⣤⡿⢋⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⡈⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⠀⡿⣿⣗⣠⡄⣻⣿⣿⣿⣿⣿⣿⣷⠀⢶⣾⣿⣷⢀⣿⣿⣿⣿⣿⣿⠟⣠⣾⣿⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⡄⠃⠸⣿⣿⣿⣿⣿⣿⣿⡿⢻⣿⣦⣥⣼⣿⣿⠋⢠⣿⣿⣿⣿⣿⠃⣴⣿⣿⡿⠿⠿⠿⠿⠿⠿⠿⠿⣶⣶⣦⣬⣭⣛⡛⠛⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣷⡄⠱⠙⣿⣿⣿⣿⣿⠃⣴⣾⣿⣿⣿⣿⡿⠁⢀⣿⣿⣿⣿⡿⠁⡼⠟⠋⣀⣤⣤⣤⣤⣤⣀⣀⡀⠀⠀⠀⠀⠉⠙⠛⠛⠿⢶⣦⣌⡙⠻⢿⣿⣿⣿⣿⣿⣿⣿⡆⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣆⠁⡈⠻⣿⣿⣿⣶⣿⣿⣿⡿⢛⡝⢡⣴⣿⡿⠿⠛⠛⣁⠌⠀⣰⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣶⣶⣶⣤⣄⡀⠀⠀⠈⠉⠙⠳⢶⣌⡙⠻⣿⣿⣿⣿⣿⡆⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣷⣌⠀⠋⢹⣿⣿⣿⣿⣿⡆⢋⣰⣷⠏⣠⣄⣀⡀⠈⠀⠀⢰⣿⣿⣿⣿⣿⣿⡿⠛⠁⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣤⣤⣀⡀⠀⠉⠛⠶⣌⡻⣿⣿⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⠀⠀⣿⣿⣿⣿⣿⣿⡇⠈⣻⠃⣰⣿⣿⡟⣿⡶⠀⠀⣿⣿⣿⣿⣿⣿⣿⣄⠀⢀⣸⣿⣿⣿⣿⣿⠟⣫⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⣄⠀⠈⠙⢮⣻⣿⠇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⡿⠀⠰⢿⣿⣿⡿⠿⠿⠇⠀⠛⡀⠃⢸⣿⣇⠘⠃⢠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠃⣠⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠁⠀⢩⣿⣿⣿⣿⣄⠙⣿⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⡿⠟⠃⠀⠐⠈⣹⣧⣴⣶⡆⠀⠃⠸⣷⣄⠐⢹⣿⡗⠐⠚⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢁⣤⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⣤⣤⣾⣿⣿⣿⣿⣿⡆⠹⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⡿⠋⠁⠀⠀⠀⢠⣠⠀⠙⣿⣿⣿⣿⡀⠀⠀⣿⣿⣷⣦⠉⢣⠀⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⣤⣤⣾⣿⣿⣿⣿⣿⡆⠹⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⡄⠀⣿⣄⠀⠈⠀⠀⠀⠘⠛⣋⣉⠀⢴⣆⢸⣿⣿⣿⣷⣄⠀⠀⢘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⣤⣀⣀⣈⣙⣓⣂⣉⣹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡗⠀⠀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣷⡀⠛⠿⠀⢀⣤⣶⣿⡄⠘⣿⣿⣧⠈⢢⡀⢻⣿⣿⣿⣿⣿⣆⡀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠙⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠀⣴⣷⣄⠙⢿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣇⠈⠀⠀⠸⣿⣿⣿⣷⡀⠙⠛⢋⣠⡈⠱⡀⢿⣿⣿⣿⣿⣿⣷⡈⠻⣿⣿⣿⣿⣿⣿⣿⣿⠻⣦⡀⠈⠛⠛⠛⠿⠿⠟⢛⣻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⢠⣾⣿⣿⣿⣷⢸⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣆⢂⠀⠀⠹⡿⠛⠋⠀⢸⣿⣿⣿⣿⠀⠑⠈⢿⣿⣿⣿⣿⣿⣿⡄⠈⠏⠙⢿⣿⣿⣿⣿⣦⡈⠻⢷⣦⣤⣤⣤⣴⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⢀⣾⣿⣿⣿⣿⣿⠈⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⡄⢀⠀⣤⠀⢰⡞⠇⠈⢿⣿⠿⣯⣄⣤⣤⠀⠬⣉⠻⠟⠁⠀⠀⠐⠂⠀⠀⠻⢿⣿⣿⣿⣿⣷⣶⣬⣭⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠈⢹⣿⣿⣿⠟⢁⣴⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣷⡀⢣⣼⡀⠈⠁⠀⣀⣈⣀⡀⠻⠿⢿⣿⣦⠀⠀⠑⠀⠀⢀⣀⣻⣶⣆⠀⠀⠀⠈⠙⠻⠿⢿⣿⣟⣻⠿⠿⢿⣿⣿⣿⣿⣿⡿⠟⠋⠁⠈⠀⠠⠤⢂⣠⣤⣤⣤⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣆⠛⠗⠀⢰⣿⣿⣿⠟⠀⣰⣦⡤⠀⣄⣀⣤⡄⠀⠀⢸⣿⣿⣿⣯⣀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠉⠙⠒⠘⠛⠛⢉⣈⣡⡀⠤⣤⣰⠒⠀⡀⠘⠻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣧⡐⠠⡈⠙⠛⠃⠀⠚⠛⠉⠀⣴⣿⣿⣿⡇⢤⠀⢸⣿⣿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠸⠿⠿⠿⣷⠀⡄⢸⣿⣿⣿⣿⣿⠀⠙⠛⠃⠀⠄⠙⠒⣦⣬⡙⠻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡙⠂⠀⠘⣿⣷⣶⡦⠀⠉⠁⠀⢿⠃⠀⠀⢸⣿⣿⣿⣿⣿⣿⠀⣿⣿⣿⡇⠀⢷⣶⣶⠘⠈⡇⢸⣿⣿⣿⣿⣏⠀⠀⣾⣿⣶⣦⡀⢹⣿⣿⣯⡰⢤⡉⠛⠿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⣤⡀⢄⡉⠛⢁⡀⠀⠀⠀⢸⡄⠀⠀⢸⣿⣿⣿⣿⣿⡟⠀⣿⣿⣿⡇⠀⠺⠛⠞⠀⠀⠀⣼⣿⣿⣿⣿⣿⠀⠰⣿⣿⣿⣿⠁⣄⠉⠻⣿⣿⣤⣬⡀⠐⠲⠍⣉⠛⢻⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡈⠢⡈⠑⠆⠀⠐⠿⠃⠀⠀⠘⣿⣿⣿⣿⣿⣿⣄⡈⠙⠋⠀⠠⠶⠶⠆⠀⠀⣠⣿⣿⣿⣿⣿⣿⠀⠀⠈⠉⠻⠿⢰⠿⠁⠠⢈⡉⠻⢿⠟⠀⣴⣷⠄⠘⢠⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣤⣉⠐⠤⠀⠀⣠⣄⣚⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⣻⣷⣶⣦⣶⣿⣶⣿⣿⣿⣿⣿⣿⠿⠿⣿⣀⣀⡀⠀⣶⡄⠀⠀⣰⣿⣿⣦⣄⠀⠚⠋⢁⣄⡄⢸⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⢠⣾⡟⣿⣿⣶⠀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠋⠁⠤⠠⣭⣿⣿⣧⢠⡌⠻⠂⠘⢿⡿⠟⢋⣤⣤⣤⣄⠈⠿⢣⠘⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⡇⢹⣿⡿⠀⠈⠻⠛⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣤⣤⣄⠀⠀⡇⠁⣿⣿⢸⣿⣶⣤⡄⠀⠀⠠⣿⣿⣿⣿⡟⢠⡆⢸⠀⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠇⣼⣿⡁⢠⣶⡄⢠⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠇⠀⡇⢸⢻⣿⢸⣿⣿⡟⢁⡴⠀⢀⣤⣉⠙⠋⣠⡿⠃⠸⠀⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⢿⣿⣇⠀⣤⡄⠘⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⠀⢠⠀⣀⣇⣀⠸⠿⠀⢿⡟⠀⠾⠃⠠⢿⣿⣿⠇⣸⣿⣷⠀⠀⣰⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠁⣸⣿⡿⠀⣿⡇⠀⠀⠀⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠀⣿⠀⣿⠀⠙⠋⠙⠀⠀⠀⠀⣴⣶⣤⣄⡉⠁⠠⣿⣿⣇⡌⢠⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠀⣰⣾⣿⠀⣿⣿⣿⣿⣿⠋⠁⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠀⣰⣾⣿⠀⣿⣿⣿⣿⣿⠋⠁⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣾⡟⠋⠁⠀⣿⣿⣿⣿⡇⡐⢀⣽⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⠙⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⠀⠃⣸⣿⣿⣿⡘⠋⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠋⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡌⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠀⢾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠸⠟⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⡘⠀⠀⠈⢻⣿⣿⣿⣿⣿⣿⣿⡟⠛⠻⠿⠏⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡷⢀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠇⠀⠀⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣾⠓⣤⠄⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠈⢀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠇⣨⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡷⠀⠀⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣆⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⠠⠿⠛⠻⠿⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠐⠀⠂⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠟⠋⡀⢉⠛⠻⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢋⢡⠤⠶⣶⣦⣶⣶⣶⣶⣤⣤⣤⣤⣤⣭⣭⠉⠉⢀⣼⣴⠰⠛⠛⠿⠿⠛⠓⠒⢉⣉⣥⣤⣴⣶⣿⣿⣿⣿⣷⣶⠒⢬⡙⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢁⠘⠀⣀⣤⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠃⢨⣿⣿⢠⠀⠀⠀⠀⠀⠀⠀⠉⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣄⠘⠆⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢁⠂⠀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠀⠀⠀⠉⠁⠈⠀⠀⠒⠀⠀⣠⣴⡆⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⢸⠀⠻⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠃⣼⠀⠰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢛⣁⣠⣴⣶⡆⠀⠀⢰⣶⣦⣤⣄⣀⣀⠉⠙⠛⠻⠿⠿⠿⠿⢿⣿⣿⣿⣿⣿⣿⣿⠯⠤⠀⢹⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⢆⣤⣤⣄⣀⣈⣉⣉⣉⣉⣉⡉⠙⠋⠉⣁⣠⣾⣿⣿⣿⠿⠛⠐⢁⠀⠄⠙⠛⠻⠿⠿⠿⣿⣿⣷⣶⣦⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣶⣿⠃⢸⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣏⠀⠈⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠟⣛⠉⠀⣐⣤⣶⣿⣿⣷⣦⣥⣄⣒⣤⣀⠀⠨⠉⠛⠻⠿⠿⠿⠿⠿⠿⠿⠿⠿⠟⠛⠛⠋⠐⣸⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⣀⠀⠤⠈⢉⣉⠛⠛⠛⠛⣉⣉⠉⠩⠄⠀⢂⣉⣥⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣶⣦⣴⣶⣶⣖⡒⠒⠒⠒⢒⣦⣤⣉⣤⣴⣾⣿⣿⣿⣿⣿⣿⣿⣿");
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣶⡶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣿⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿ \n");
            Console.WriteLine("                 ||================================||");
            Console.WriteLine("                 ||   Welcome to Bob the Builder!  ||");
            Console.WriteLine("                 ||================================||\n");
            Console.WriteLine("Bob the Builder is a new, incredibly boring base building game.");
            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("You just bought a plot of land. Now you have to build your dream house.                           Room Connections:");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
            Console.WriteLine("Type 'map' to show the minimap.");
            Console.WriteLine("Type 'goto' to plot a direction to another room.");
            Console.WriteLine("Type 'gointo' to enter a neighbouring room by it's name.");
            Console.WriteLine("Type 'travel' to travel to any discovered room by it's name.");
        }

        private Room? FindRoomByName(string name)//BFS for finding target room
        {
            string target = name.Replace(" ", "").Replace("_", "").Replace("-", "").ToLowerInvariant();//simplifying target

            if (target=="north" || target=="east" || target=="south" || target=="south")//if just direction going to direction
            {
                Move(target);
                return null;
            }

            var visited = new HashSet<Room>();
            var queue = new Queue<Room>();

            queue.Enqueue(currentRoom);
            visited.Add(currentRoom);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.ShortDescription.Replace(" ", "").Replace("_", "").Replace("-", "").ToLowerInvariant() == target)
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
            return null; // Not found
        }
    }
}