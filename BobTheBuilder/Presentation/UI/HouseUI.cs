namespace BobTheBuilder.Presentation.UI
{
    public static class HouseUI
    {
        //foundations
        static string NoFoundation = "≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈\n";
        static string WoodFoundation = "≈≈≈≈≈≈≈╔═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╗≈≈≈≈≈≈≈\r\n       ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║       \r\n       ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║       \r\n       ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║       \n";
        static string ConcreteFoundation = "≈≈≈≈≈≈≈███████████████████████████████████≈≈≈≈≈≈≈\r\n       ███████████████████████████████████       \n";
        static string BrickFoundation = "≈≈≈≈≈≈≈▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒≈≈≈≈≈≈≈\r\n       ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒       \n";

        //walls
        static string WoodWall = "       ()________________________________) \r\n       ()________________________________)\r\n       ()________________________________)\r\n       ()___╔═╦═╗_____╔═══╗_____╔═╦═╗____)\r\n       ()___╠═╬═╣_____║░░░║_____╠═╬═╣____)\r\n       ()___╚═╩═╝_____║▄  ║_____╚═╩═╝____)\r\n       ()_____________║   ║______________)\r\n       ()_____________║   ║______________)\n";
        static string ConcreteWall = "       ███████████████████████████████████\r\n       ███████████████████████████████████\r\n       █████╔═╦═╗█████╔═══╗█████╔═╦═╗█████\r\n       █████╠═╬═╣█████║░░░║█████╠═╬═╣█████\r\n       █████╚═╩═╝█████║▄  ║█████╚═╩═╝█████\r\n       ███████████████║   ║███████████████\r\n       ███████████████║   ║███████████████\n";
        static string BrickWall = "       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓\r\n       ▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓\r\n       ▓▒▒▒▒╔═╦═╗▒▒▒▒▒╔═══╗▒▒▒▒▒╔═╦═╗▒▒▒▒▓\r\n       ▓▒▒▒▒╠═╬═╣▒▒▒▒▒║░░░║▒▒▒▒▒╠═╬═╣▒▒▒▒▓\r\n       ▓▒▒▒▒╚═╩═╝▒▒▒▒▒║▄  ║▒▒▒▒▒╚═╩═╝▒▒▒▒▓\r\n       ▓▒▒▒▒▓▓▓▓▓▒▒▒▒▒║   ║▒▒▒▒▒▓▓▓▓▓▒▒▒▒▓\r\n       ▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒║   ║▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓\n";
        static string WoodShinglesWall = "       █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌\r\n       █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌\r\n       █▀▀▀▀╔═╦═╗▀▀▀▀▀╔═══╗▀▀▀▀▀╔═╦═╗▀▀▀▀▌\r\n       █▀▀▀▀╠═╬═╣▀▀▀▀▀║░░░║▀▀▀▀▀╠═╬═╣▀▀▀▀▌\r\n       █▀▀▀▀╚═╩═╝▀▀▀▀▀║▄  ║▀▀▀▀▀╚═╩═╝▀▀▀▀▌\r\n       █▀▀▀▀▀▀▀▀▀▀▀▀▀▀║   ║▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌\r\n       █▀▀▀▀▀▀▀▀▀▀▀▀▀▀║   ║▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌\n";

        //roofs
        static string WoodRoof = "           ()-=-=-=-=-=-=-=-=-=-=-=-=)           \r\n          ()-=-=-=-=-=-=-=-=-=-=-=-=-=)          \r\n         ()-=-=-=-=-=-=-=-=-=-=-=-=-=-=)         \r\n        ()-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=)        \r\n       ()-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=)       \n";
        static string ConcreteRoof = "       █                                 █       \r\n       ███████████████████████████████████       \n";
        static string TyleRoof = "               nununununununununun               \r\n             nununununununununununun             \r\n           nununununununununununununun           \r\n         nununununununununununununununun         \r\n       nununununununununununununnununununn       \n";

        public static string SetFoundation(int foundation)
        {
            if (foundation == 0)
            {
                return NoFoundation;
            }
            else if (foundation == 1)
            {
                return WoodFoundation;
            }
            else if (foundation == 2)
            {
                return ConcreteFoundation;
            }
            else if (foundation == 3)
            {
                return BrickFoundation;
            }
            else
            {
                return ConcreteFoundation;
            }
        }
        public static string SetWalls(int walls)
        {
            if (walls == 0)
            {
                return " \r\n \r\n \r\n \r\n \r\n \r\n \n";
            }
            else if (walls == 1)
            {
                return WoodWall;
            }
            else if (walls == 2)
            {
                return ConcreteWall;
            }
            else if (walls == 3)
            {
                return BrickWall;
            }
            else if (walls == 4)
            {
                return WoodShinglesWall;
            }
            return ConcreteWall;
        }
        public static string SetRoof(int roof)
        {
            if (roof == 0)
            {
                return " \r\n \r\n \r\n \r\n \n";
            }
            else if (roof == 1)
            {
                return WoodRoof;
            }
            else if (roof == 2)
            {
                return ConcreteRoof;
            }
            else if (roof == 3)
            {
                return TyleRoof;
            }
            else
            {
                return TyleRoof;
            }
        }

        public static void DisplayHouse(House house)
        {
            Console.Write(SetRoof(house.roof));
            Console.Write(SetWalls(house.walls));
            Console.Write(SetFoundation(house.foundation));
            
            if (house.UsedMaterials.Count > 0)
            {
                double totalQuality = 0;
                double totalSustainability = 0;
                
                foreach (Material material in house.UsedMaterials)
                {
                    totalQuality += material.Quality;
                    totalSustainability += material.Sustainability;
                }
                
                double avgQuality = totalQuality / house.UsedMaterials.Count;
                double avgSustainability = totalSustainability / house.UsedMaterials.Count;
                
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n--- House Statistics ---");
                Console.ResetColor();
                Console.WriteLine("Average Quality: " + avgQuality.ToString("F2"));
                Console.WriteLine("Average Sustainability: " + avgSustainability.ToString("F2"));
                
                // Show HP for built parts
                if (house.foundation > 0)
                {
                    Console.ForegroundColor = house.foundationHP < 50 ? ConsoleColor.Red : ConsoleColor.White;
                    Console.WriteLine("Foundation HP: {0:F1}%", house.foundationHP);
                    Console.ResetColor();
                }
                if (house.walls > 0)
                {
                    Console.ForegroundColor = house.wallsHP < 50 ? ConsoleColor.Red : ConsoleColor.White;
                    Console.WriteLine("Walls HP: {0:F1}%", house.wallsHP);
                    Console.ResetColor();
                }
                if (house.roof > 0)
                {
                    Console.ForegroundColor = house.roofHP < 50 ? ConsoleColor.Red : ConsoleColor.White;
                    Console.WriteLine("Roof HP: {0:F1}%", house.roofHP);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
}
