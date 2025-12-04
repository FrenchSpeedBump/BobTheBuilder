namespace BobTheBuilder
{
    public class Player//can inventory be a separate class?
    {
        public string Name = "Bob";
        public List<ShopInventoryContents> Inventory = new List<ShopInventoryContents>();
        
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
            Inventory.Add(contents);
        }

        public void RemoveItem(ShopInventoryContents contents)
        {
            Inventory.Remove(contents);
        }
        public void BuyItem(ShopInventoryContents contents, Bank bank)
        {
            if (bank.accountBalance >= contents.Price)
            {
                bank.accountBalance -= contents.Price;
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
