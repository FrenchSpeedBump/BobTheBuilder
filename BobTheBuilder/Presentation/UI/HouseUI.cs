namespace BobTheBuilder.Presentation.UI
{
    internal class HouseUI
    {
        //foundations
        string NoFoundation = "≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈\n";
        string WoodFoundation = "≈≈≈≈≈≈≈╔═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╦═╗≈≈≈≈≈≈≈\r\n       ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║       \r\n       ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║       \r\n       ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║ ║       \n";
        string ConcreteFoundation = "≈≈≈≈≈≈≈███████████████████████████████████≈≈≈≈≈≈≈\r\n       ███████████████████████████████████       \n";
        string BrickFoundation = "≈≈≈≈≈≈≈▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒≈≈≈≈≈≈≈\r\n       ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒       \n";

        //walls
        string WoodWall = "  ________________________________ \r\n()________________________________)\r\n()________________________________)\r\n()___╔═╦═╗_____╔═══╗_____╔═╦═╗____)\r\n()___╠═╬═╣_____║░░░║_____╠═╬═╣____)\r\n()___╚═╩═╝_____║▄  ║_____╚═╩═╝____)\r\n()_____________║   ║______________)\r\n()_____________║   ║______________)\n";
        string ConcreteWall = "███████████████████████████████████\r\n███████████████████████████████████\r\n█████╔═╦═╗█████╔═══╗█████╔═╦═╗█████\r\n█████╠═╬═╣█████║░░░║█████╠═╬═╣█████\r\n█████╚═╩═╝█████║▄  ║█████╚═╩═╝█████\r\n███████████████║   ║███████████████\r\n███████████████║   ║███████████████\n";
        string BrickWall = "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓\r\n▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓\r\n▓▒▒▒▒╔═╦═╗▒▒▒▒▒╔═══╗▒▒▒▒▒╔═╦═╗▒▒▒▒▓\r\n▓▒▒▒▒╠═╬═╣▒▒▒▒▒║░░░║▒▒▒▒▒╠═╬═╣▒▒▒▒▓\r\n▓▒▒▒▒╚═╩═╝▒▒▒▒▒║▄  ║▒▒▒▒▒╚═╩═╝▒▒▒▒▓\r\n▓▒▒▒▒▓▓▓▓▓▒▒▒▒▒║   ║▒▒▒▒▒▓▓▓▓▓▒▒▒▒▓\r\n▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒║   ║▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓\n";
        string WoodShinglesWall = "\n";
        //roofs


        string ASCIIFoundation;
        string ASCIIWalls;
        string ASCIIRoof;

        public HouseUI()
        {
            ASCIIFoundation = "";
            ASCIIWalls = "";
            ASCIIRoof = "";
        }

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
                ASCIIWalls = WoodFoundation;
            }
            else if (walls == 2)
            {
                ASCIIWalls = ConcreteFoundation;
            }
            else if (walls == 3)
            {
                ASCIIWalls = BrickFoundation;
            }
            else
            {
                ASCIIWalls = NoFoundation;
            }
        }
        public string getWalls()
        {
            return ASCIIWalls;
        }
        public void setRoof(int foundation)
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
        public string getRoof()
        {
            return ASCIIFoundation;
        }
    }
}
