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

            Console.WriteLine("\n========MAP========");
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var room = roomPositions.FirstOrDefault(kvp => kvp.Value == (x, y)).Key;
                    if(room==null)
                    {
                        Console.Write("   ");
                    }
                    else
                    {
                        if (room == currentRoom)
                            Console.Write("[{0}]", room.ShortDescription.Substring(0, 3));  // Player position
                        else
                        {
                            if (room.discovered)
                            {
                                Console.Write(" {0} ", room.ShortDescription.Substring(0, 3));  // Visited room
                            }
                            else
                            {
                                Console.Write(" ??? ");
                            }
                                
                        }
                            
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("==================\n");
        }
    }
}