namespace BobTheBuilder
{
    public class Player//can inventory be a separate class?
    {
        public string Name = "Bob";
        public static List<ShopInventoryContents> Inventory = new List<ShopInventoryContents>();
        
        public void DisplayInventory() // Displays only items bcs you can't get materials to your inventory yet. If we want to implement buying materials we just delete the condition in "buy"
        {
            Console.WriteLine($"Inventory for {Name}:");
            foreach (var contents in Inventory)
            {
                Console.WriteLine($" - {contents.Name}: {contents.Description} Price: {contents.Price}");
            }
        }
    }
}
