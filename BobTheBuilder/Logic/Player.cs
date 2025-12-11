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
            if (bank.accountBalance >= contents.Price)
            {
                if(contents.Name!=null)
                {
                    ShopInventoryContents? item = materials.GetContents(contents.efect);
                    if (item != null)
                    {
                        item.Price *= contents.discount;
                    }
                }
                bank.accountBalance -= contents.Price;
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
            if (bank.accountBalance >= contents.Price)
            {
                bank.accountBalance -= contents.Price;
                AddItem(contents);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
