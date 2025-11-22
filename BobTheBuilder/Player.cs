namespace BobTheBuilder
{
    public class Player//can inventory be a separate class?
    {
        public string Name = "Bob";
        public static List<ShopInventoryContents> Inventory = new List<ShopInventoryContents>();
        
        public void DisplayInventory()
        {
            Console.WriteLine($"Inventory for {Name}:");
            foreach (var contents in Inventory)
            {
                Console.WriteLine($" - {contents.Name}: {contents.Description} Price: {contents.Price}");
            }
        }

        public void AddItem(ShopInventoryContents contents)
        {
            Player.Inventory.Add(contents);
        }

        public void RemoveItem(ShopInventoryContents contents)
        {
            Player.Inventory.Remove(contents);
        }
        public void BuyItem(ShopInventoryContents contents)
        {
            if (Bank.accountBalance >= contents.Price)
            {
                Bank.accountBalance -= contents.Price;
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
