namespace BobTheBuilder.Presentation.UI
{
    public class HouseUI
    {
        //foundations
        string NoFoundation = "≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈\n";
        string WoodFoundation = "≈≈≈≈≈≈≈╔═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╗≈≈≈≈≈≈≈\r\n       ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║       \r\n       ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║       \r\n       ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║       \n";
        string ConcreteFoundation = "≈≈≈≈≈≈≈███████████████████████████████████≈≈≈≈≈≈≈\r\n       ███████████████████████████████████       \n";
        string BrickFoundation = "≈≈≈≈≈≈≈▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒≈≈≈≈≈≈≈\r\n       ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒       \n";

        //walls
        string WoodWall = "       ()________________________________) \r\n       ()________________________________)\r\n       ()________________________________)\r\n       ()___╔═╦═╗_____╔═══╗_____╔═╦═╗____)\r\n       ()___╠═╬═╣_____║░░░║_____╠═╬═╣____)\r\n       ()___╚═╩═╝_____║▄  ║_____╚═╩═╝____)\r\n       ()_____________║   ║______________)\r\n       ()_____________║   ║______________)\n";
        string ConcreteWall = "       ███████████████████████████████████\r\n       ███████████████████████████████████\r\n       █████╔═╦═╗█████╔═══╗█████╔═╦═╗█████\r\n       █████╠═╬═╣█████║░░░║█████╠═╬═╣█████\r\n       █████╚═╩═╝█████║▄  ║█████╚═╩═╝█████\r\n       ███████████████║   ║███████████████\r\n       ███████████████║   ║███████████████\n";
        string BrickWall = "       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓\r\n       ▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓\r\n       ▓▒▒▒▒╔═╦═╗▒▒▒▒▒╔═══╗▒▒▒▒▒╔═╦═╗▒▒▒▒▓\r\n       ▓▒▒▒▒╠═╬═╣▒▒▒▒▒║░░░║▒▒▒▒▒╠═╬═╣▒▒▒▒▓\r\n       ▓▒▒▒▒╚═╩═╝▒▒▒▒▒║▄  ║▒▒▒▒▒╚═╩═╝▒▒▒▒▓\r\n       ▓▒▒▒▒▓▓▓▓▓▒▒▒▒▒║   ║▒▒▒▒▒▓▓▓▓▓▒▒▒▒▓\r\n       ▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒║   ║▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓\n";
        string WoodShinglesWall = "       █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌\r\n       █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌\r\n       █▀▀▀▀╔═╦═╗▀▀▀▀▀╔═══╗▀▀▀▀▀╔═╦═╗▀▀▀▀▌\r\n       █▀▀▀▀╠═╬═╣▀▀▀▀▀║░░░║▀▀▀▀▀╠═╬═╣▀▀▀▀▌\r\n       █▀▀▀▀╚═╩═╝▀▀▀▀▀║▄  ║▀▀▀▀▀╚═╩═╝▀▀▀▀▌\r\n       █▀▀▀▀▀▀▀▀▀▀▀▀▀▀║   ║▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌\r\n       █▀▀▀▀▀▀▀▀▀▀▀▀▀▀║   ║▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌\n";

        //roofs
        string WoodRoof = "           ()-=-=-=-=-=-=-=-=-=-=-=-=)           \r\n          ()-=-=-=-=-=-=-=-=-=-=-=-=-=)          \r\n         ()-=-=-=-=-=-=-=-=-=-=-=-=-=-=)         \r\n        ()-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=)        \r\n       ()-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=)       \n";
        string ConcreteRoof = "       █                                 █       \r\n       ███████████████████████████████████       \n";
        string TyleRoof = "               nununununununununun               \r\n             nununununununununununun             \r\n           nununununununununununununun           \r\n         nununununununununununununununun         \r\n       nununununununununununununnununununn       \n";

        //my version
        string ASCIIFoundation;
        string ASCIIWalls;
        string ASCIIRoof;

        public HouseUI()
        {
            ASCIIFoundation = "";
            ASCIIWalls = "";
            ASCIIRoof = "";
        }
        public string SetFoundation(int foundation)
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
        public string GetFoundation()
        {
            return ASCIIFoundation;
        }
        public string SetWalls(int walls)
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
        public string GetWalls()
        {
            return ASCIIWalls;
        }
        public string SetRoof(int roof)
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
        public string GetRoof()
        {
            return ASCIIRoof;
        }

        public static void DisplayHouse(House house)
        {
            HouseUI houseUI = new HouseUI();
            Console.Write(houseUI.SetRoof(house.roof));
            Console.Write(houseUI.SetWalls(house.walls));
            Console.Write(houseUI.SetFoundation(house.foundation));
            
            // Display house stats if materials have been used
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
                Console.WriteLine();
            }
        }
    }
}
