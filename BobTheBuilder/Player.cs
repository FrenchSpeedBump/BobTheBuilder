namespace BobTheBuilder
{
    public class Player
    {
        public string Name = "Bob";
        public double Money = 100; // This can be modified, need to still implement the banking system
        public List<Item> Inventory = new List<Item>();

        public void AddItem(Item item)
        {
            Inventory.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Inventory.Remove(item);
        }

        public void DisplayInventory()
        {
            Console.WriteLine($"Inventory for {Name}:");
            foreach (var item in Inventory)
            {
                Console.WriteLine($" - {item.Name}: {item.Description} Price: {item.Price}");
            }
        }
        public void BuyItem(Item item)
        {
            if (Money >= item.Price)
            {
                Money -= item.Price;
                AddItem(item);
                Console.WriteLine($"Bought {item.Name} for {item.Price}.");
            }
            else
            {
                Console.WriteLine("Not enough money.");
            }
        }
    }
}
