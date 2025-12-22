namespace BobTheBuilder
{
    public class Player//can inventory be a separate class?
    {
        public string Name = "Bob";
        public List<ShopInventoryContents> Inventory = new List<ShopInventoryContents>();

        public void AddItem(ShopInventoryContents contents)
        {
            Inventory.Add(contents);
        }

        public void RemoveItem(ShopInventoryContents contents)
        {
            Inventory.Remove(contents);
        }
        public bool BuyItem(Item contents, Bank bank, Shop materials)
        {
            if (bank.GetBalance() >= contents.Price)
            {
                if(contents.Name!=null && contents.Effect!=null)
                {
                    ShopInventoryContents? item = materials.GetContents(contents.Effect);
                    if (item != null)
                    {
                        item.Price *= contents.Discount;
                    }
                }
                if (!bank.takeMoney(contents.Price))
                {
                    return false;
                }
                AddItem(contents);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool BuyMaterial(ShopInventoryContents contents, Bank bank)
        {
            if (bank.GetBalance() >= contents.Price)
            {
                if (!bank.takeMoney(contents.Price))
                {
                    return false;
                }
                AddItem(contents);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Has(string item)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if(Inventory[i].Name== item)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
