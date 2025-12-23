namespace BobTheBuilder
{ 
    public class Shop : Room
    {
        public Dictionary<string, ShopInventoryContents> Inventory { get; set; } = new Dictionary<string, ShopInventoryContents>();
        public Shop(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {
        }
        public void AddContents(ShopInventoryContents contents)
        {
            Inventory[contents.Name] = contents;
        }
        public void RemoveContents(ShopInventoryContents contents)
        {
            Inventory.Remove(contents.Name);
        }

        public ShopInventoryContents? GetContents(string contentsName)
        {
            Inventory.TryGetValue(contentsName, out ShopInventoryContents? contents);
            return contents;
        }
    }
}
