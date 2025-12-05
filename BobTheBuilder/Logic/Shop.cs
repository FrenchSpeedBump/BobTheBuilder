namespace BobTheBuilder
{
    //Shop class with inventory variable, which will hold multiple item objects of type Item 
    public class Shop : Room
    {
        public Dictionary<string, ShopInventoryContents> Inventory { get; set; } = new();
        public Shop(string shortDesc, string longDesc) : base(shortDesc, longDesc) // Made shop class inherit from Room, added inventory and relevant methods
        {
        }
        public void AddContents(ShopInventoryContents contents) // Both these methods support both items and materials
        {
            Inventory[contents.Name] = contents;
        }
        public void RemoveContents(ShopInventoryContents contents) // Both these methods support both items and materials
        {
            Inventory.Remove(contents.Name);
        }

        public void DisplayInventory()
        {
            Console.WriteLine($"Inventory for {ShortDescription}:");
            foreach (Item item in Inventory.Values.OfType<Item>())
            {
                Console.WriteLine($" - {item.Name}: {item.Description} Price: {item.Price}");
            }
            foreach (Material material in Inventory.Values.OfType<Material>())
            {
                Console.WriteLine($" - {material.Name}: {material.Description} Sustainability: {material.Sustainability} Price: {material.Price}");
            }
        }
        public ShopInventoryContents? GetContents(string contentsName) // Used in "buy" command
        {
            Inventory.TryGetValue(contentsName, out var contents);
            return contents;
        }
    }
}
