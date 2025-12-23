namespace BobTheBuilder
{

    public static class GameUI 
    
    {
    public static void PrintWelcomeImage()
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
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⢠⣾⡟⣿⣿⣶⠀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠋⠁⠤⠠⣭⣿⣿⣧⢠⡌⠻⠂⠘⢿⡿⠟⢋⣤⣤⣤⣄⠈⠿⢣⠘⣿ \n");
            
        }


    public static void PrintWelcome()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n================================");
        Console.WriteLine("  WELCOME TO BOB THE BUILDER");
        Console.WriteLine("================================\n");
        Console.ResetColor();
        Console.WriteLine("You just bought a plot of land. Build your dream house!");
        Console.WriteLine();
        PrintHelp();
        Console.WriteLine();
    }

    public static void PrintHelp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("MOVEMENT:");
            Console.ResetColor();
            Console.WriteLine("  north, south, east, west  - Navigate between rooms");
            Console.WriteLine("  back                      - Return to previous room");
            Console.WriteLine("  gointo <name>             - Enter neighboring room by name");
            Console.WriteLine("  travel <name>             - Travel to discovered room (requires car)");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("ACTIONS:");
            Console.ResetColor();
            Console.WriteLine("  look                      - View room details");
            Console.WriteLine("  buy <item>                - Purchase item in shop");
            Console.WriteLine("  accept <number>           - Accept quest in construction building");
            Console.WriteLine("  repair                    - Repair house (at construction building)");
            Console.WriteLine("  sleep                     - End day and start next");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("INFORMATION:");
            Console.ResetColor();
            Console.WriteLine("  inventory                 - View your items");
            Console.WriteLine("  map                       - Show minimap");
            Console.WriteLine("  account                   - Check bank account (at bank)");
            Console.WriteLine("  loan <amount>             - Take out loan (at bank)");
            Console.WriteLine("  help                      - Show this message");
            Console.WriteLine("  quit                      - Exit game");
        }

        public static void LoadingBar()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("=================================LOADING=================================");
            string bar = "";
            for (int i = 0; i < 73; i++)
            {
                Console.Write("\r{0}", bar);
                bar += "█";
                Thread.Sleep(50);
            }
            Console.ForegroundColor = ConsoleColor.White;

        }

    }
    
}
    