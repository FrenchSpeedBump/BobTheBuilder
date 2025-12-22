namespace BobTheBuilder
{
    public static class MinimapUI
    {
        public static void DisplayMinimap(Minimap minimap, Room currentRoom, Dictionary<Room, (int x, int y)> roomPositions)
        {
            int minX = roomPositions.Values.Min(p => p.x);
            int maxX = roomPositions.Values.Max(p => p.x);
            int minY = roomPositions.Values.Min(p => p.y);
            int maxY = roomPositions.Values.Max(p => p.y);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n========== MAP ==========");
            Console.ResetColor();
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var room = roomPositions.FirstOrDefault(kvp => kvp.Value == (x, y)).Key;
                    if(room==null)
                    {
                        Console.Write("     ");  // 5 spaces to match room width
                    }
                    else
                    {
                        if (room == currentRoom)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("[{0}]", room.ShortDescription.Substring(0, 3));  // Player position (5 chars)
                            Console.ResetColor();
                        }
                        else
                        {
                            if (room.discovered)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(" {0} ", room.ShortDescription.Substring(0, 3));  // Visited room (5 chars)
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write(" ??? ");  // Undiscovered (5 chars)
                                Console.ResetColor();
                            }
                                
                        }
                            
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("========================\n");
            Console.ResetColor();
        }
    }
}