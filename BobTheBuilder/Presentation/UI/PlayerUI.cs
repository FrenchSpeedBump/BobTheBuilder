namespace BobTheBuilder
{
    public static class PlayerUI
    {
        public static void DisplayInventory(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n--- Your Inventory ---\n");
            Console.ResetColor();
            
            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("  Your inventory is empty");
            }
            else
            {
                foreach (var contents in player.Inventory)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"  {contents.Name}");
                    Console.ResetColor();
                    Console.WriteLine($" - {contents.Description}");
                    Console.WriteLine($"    Value: ${contents.Price}");
                }
            }
            Console.WriteLine();
        }
    }
}