namespace BobTheBuilder
{
    public static class PlayerUI
    {
        public static void DisplayInventory(Player player)
        {
            Console.WriteLine($"Inventory for {player.Name}:");
            foreach (var contents in player.Inventory)
            {
                Console.WriteLine($" - {contents.Name}: {contents.Description} Price: {contents.Price}");
            }
        }
    }
}