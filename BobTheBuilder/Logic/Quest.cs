namespace BobTheBuilder
{
    public class Quest
    {
        public string ShortDescription { get; }

        public string LongDescription { get; }

        public bool IsCompleted { get; set; }

        public int Phase { get; }

        public List<Material> Requirements { get; set; } = new List<Material>();
        public int Price { get; }

        public Quest(string shortDescription, string longDescription, List<Material>? requirements , int phase, int price) 
        {
            this.ShortDescription = shortDescription;
            this.LongDescription = longDescription;
            IsCompleted = false;
            if (requirements != null)
            {
                this.Requirements = requirements;
            }
            this.Phase = phase;
            this.Price = price;
        }

        public bool CheckRequirements(List<ShopInventoryContents> inventory)
        {

            List<Material> tempList = new List<Material>(Requirements);

            foreach (ShopInventoryContents item in inventory)
            {
                foreach (Material req in tempList)
                {
                    if (item.Name == req.Name)
                    {
                        tempList.Remove(req);
                        break;
                    }
                }
            }
            return tempList.Count == 0;
        }
    }
}
