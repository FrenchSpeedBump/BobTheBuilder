namespace BobTheBuilder
{
    public class Quest
    {
        public string shortDescription { get; private set; }

        public string longDescription { get; private set; }

        public bool isCompleted { get; set; }

        public int phase { get; private set; }

        public List<string> requirements {get; private set; } = new();
        public int price { get; private set; }
        public string? buildsPart { get; private set; }


        public Quest(string shortDescription, string longDescription, List<string>? requirements , int phase, int price, string? buildsPart = null) 
        {
            this.shortDescription = shortDescription;
            this.longDescription = longDescription;
            this.isCompleted = false;
            if (requirements != null)
            {
                this.requirements = requirements;
            }
            this.phase = phase;
            this.price = price;
            this.buildsPart = buildsPart;
        }

        public bool checkRequirements(List<ShopInventoryContents> inventory)
        {

            List<string> tempList = new List<string>(requirements);

            foreach (ShopInventoryContents item in inventory)
            {
                foreach (string req in tempList)
                {
                    if (item.Name == req)
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
