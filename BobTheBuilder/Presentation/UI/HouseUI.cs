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
        public string setFoundation(int foundation)
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
                return NoFoundation;
            }
        }
        public string getFoundation()
        {
            return ASCIIFoundation;
        }
        public string setWalls(int walls)
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
            else
            {
                return WoodShinglesWall;
            }
        }
        public string getWalls()
        {
            return ASCIIWalls;
        }
        public string setRoof(int roof)
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
                return " \r\n \r\n \r\n \r\n \n";
            }
        }
        public string getRoof()
        {
            return ASCIIRoof;
        }
        /*
        public void setFoundation(int foundation)
        {
            if (foundation == 0)
            {
                ASCIIFoundation = NoFoundation;
            }
            else if (foundation == 1)
            {
                ASCIIFoundation = WoodFoundation;
            }
            else if (foundation == 2)
            {
                ASCIIFoundation = ConcreteFoundation;
            }
            else if (foundation == 3)
            {
                ASCIIFoundation = BrickFoundation;
            }
            else
            {
                ASCIIFoundation = NoFoundation;
            }
        }
        public string getFoundation() 
        { 
            return ASCIIFoundation; 
        }
        public void setWalls(int walls)
        {
            if (walls == 0)
            {
                ASCIIWalls = "\n";
            }
            else if (walls == 1)
            {
                ASCIIWalls = WoodWall;
            }
            else if (walls == 2)
            {
                ASCIIWalls = ConcreteWall;
            }
            else if (walls == 3)
            {
                ASCIIWalls = BrickWall;
            }
            else
            {
                ASCIIWalls = WoodShinglesWall;
            }
        }
        public string getWalls()
        {
            return ASCIIWalls;
        }
        public void setRoof(int roof)
        {
            if (roof == 0)
            {
                ASCIIRoof = "\n";
            }
            else if (roof == 1)
            {
                ASCIIRoof = WoodRoof;
            }
            else if (roof == 2)
            {
                ASCIIRoof = ConcreteRoof;
            }
            else if (roof == 3)
            {
                ASCIIRoof = TyleRoof;
            }
            else
            {
                ASCIIRoof = "\n";
            }
        }
        public string getRoof()
        {
            return ASCIIRoof;
        }
        */
    }
}
