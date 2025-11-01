namespace BobTheBuilder
{
    //Shop class with inventory variable, which will hold multiple item objects of type Item 
    public class Shop : Room
    {
        public Dictionary<string, Item> Inventory { get; private set; } = new();
        public Shop(string shortDesc, string longDesc) : base(shortDesc, longDesc) // Made shop class inherit from Room, added inventory and relevant methods
        {
        }
        public void AddItem(Item item)
        {
            Inventory[item.Name] = item;
        }
        public void RemoveItem(Item item)
        {
            Inventory.Remove(item.Name);
        }

        public void DisplayInventory()
        {
            Console.WriteLine($"Inventory for {ShortDescription}:");
            foreach (var item in Inventory.Values)
            {
                Console.WriteLine($" - {item.Name}: {item.Description} Price: {item.Price}");
            }
        }
        public Item? GetItem(string itemName)
        {
            Inventory.TryGetValue(itemName, out var item);
            return item;
        }
    }
}
