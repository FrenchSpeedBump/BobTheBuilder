namespace BobTheBuilder
{
    public class Minimap
    {
        public Dictionary<Room, (int x, int y)> roomPositions = new();
        public Room? currentRoom;

        public void MapRooms(Room startRoom) //USING BFS IT LOOPS THROUGH ALL THE ROOMS
        {

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
    }
}