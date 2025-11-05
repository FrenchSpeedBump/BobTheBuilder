namespace BobTheBuilder
{
    public class Player
    {
        public string Name = "Bob";
        public double Money = 100; // This can be modified, need to still implement the banking system
        public List<Item> Inventory = new List<Item>();

        public void AddItem(ShopInventoryContents contents) // Both these methods currently only support items
        {
            if (contents is Item item)
            {
                Inventory.Add(item);
            }
        }

        public void RemoveItem(ShopInventoryContents contents) // Both these methods currently only support items
        {
            if (contents is Item item)
            {
                Inventory.Remove(item);
            }
        }

        public void DisplayInventory() // Displays only items bcs you can't get materials to your inventory yet. If we want to implement buying materials we just delete the condition in "buy"
        {
            Console.WriteLine($"Inventory for {Name}:");
            foreach (var item in Inventory)
            {
                Console.WriteLine($" - {item.Name}: {item.Description} Price: {item.Price}");
            }
        }
        public void BuyItem(ShopInventoryContents contents) // Cannot buy materials bcs there is condition when this method is called
        {
            if (Money >= contents.Price)
            {
                Money -= contents.Price;
                AddItem(contents);
                Console.WriteLine($"Bought {contents.Name} for {contents.Price}.");
            }
            else
            {
                Console.WriteLine("Not enough money.");
            }
        }
    }
}
