namespace BobTheBuilder
{
    public class Minimap
    {
        private Dictionary<Room, (int x, int y)> roomPositions = new();
        private Room? currentRoom;

        public void MapRooms(Room startRoom)
            {
//USING FIFO ALGO IT LOOPS THROUGH ALL THE ROOMS
                var visited = new HashSet<Room>();
                var queue = new Queue<(Room room, int x, int y)>();
        
                queue.Enqueue((startRoom, 0, 0));
                visited.Add(startRoom);
        
                while (queue.Count > 0)
                    {
                        var (room, x, y) = queue.Dequeue();
                        roomPositions[room] = (x, y);
            
                        foreach (var exit in room.Exits)
                            {
                                if (!visited.Contains(exit.Value))
                                    {
                                        visited.Add(exit.Value);
                                        var (newX, newY) = GetNewPosition(x, y, exit.Key);
                                        queue.Enqueue((exit.Value, newX, newY));
                                    }
                            }
                    }
            }

            private (int, int) GetNewPosition(int x, int y, string direction)
            {
                return direction switch
                {
                    "north" => (x, y - 1),
                    "south" => (x, y + 1),
                    "east" => (x + 1, y),
                    "west" => (x - 1, y),
                    _ => (x, y)
                };
            }

            public void Display(Room currentRoom)
            {
                this.currentRoom = currentRoom;
        
                int minX = roomPositions.Values.Min(p => p.x);
                int maxX = roomPositions.Values.Max(p => p.x);
                int minY = roomPositions.Values.Min(p => p.y);
                int maxY = roomPositions.Values.Max(p => p.y);
        
                Console.WriteLine("\n=== MAP ===");
                for (int y = minY; y <= maxY; y++)
                {
                    for (int x = minX; x <= maxX; x++)
                        {
                            var room = roomPositions.FirstOrDefault(kvp => kvp.Value == (x, y)).Key;
                
                            if (room == currentRoom)
                                Console.Write(" @ ");  // Player position
                            else if (room != null)
                                Console.Write(" · ");  // Visited room
                            else
                                Console.Write("   ");  // Empty space
                        }
                    Console.WriteLine();
                }
                Console.WriteLine("===========\n");
            }
        
//THIS IS LIKE 95% AI SO IDK IF WE SHOULD IMPLEMENT IT
            public string GetDirectionTo(Room fromRoom, Room targetRoom)
                {
                    if (fromRoom == null || !roomPositions.ContainsKey(fromRoom) || !roomPositions.ContainsKey(targetRoom))
                        return "Unknown location";

                    var (curX, curY) = roomPositions[fromRoom];
                    var (tarX, tarY) = roomPositions[targetRoom];
        
                    if (curX == tarX && curY == tarY)
                        return "You are here";
        
                    var directions = new List<string>();
                    int deltaX = tarX - curX;
                    int deltaY = tarY - curY;
        
                    // Add directions with counts
                    if (deltaY < 0) directions.Add($"north {Math.Abs(deltaY)}");
                    if (deltaY > 0) directions.Add($"south {Math.Abs(deltaY)}");
                    if (deltaX > 0) directions.Add($"east {Math.Abs(deltaX)}");
                    if (deltaX < 0) directions.Add($"west {Math.Abs(deltaX)}");
        
                    return directions.Count > 0 ? string.Join(", then ", directions) : "You are here";
                }
    }
}