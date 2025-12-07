namespace BobTheBuilder
{
    public class Quest
    {
        public string shortDescription { get; }

        public string longDescription { get; }

        public bool isCompleted { get; set; }

        public int phase { get; }

        public List<Material> requirements { get; set; } = new();
        public List<string> miniQuests { get; set; } = new();
        public int price { get; }

        public Quest(string shortDescription, string longDescription, List<Material>? requirements , int phase, int price, List<string>? miniQuests = null) 
        {
            this.shortDescription = shortDescription;
            this.longDescription = longDescription;
            isCompleted = false;
            if (requirements != null)
            {
                this.requirements = requirements;
            }
            if (miniQuests != null)
            {
                this.miniQuests = miniQuests;
            }
            this.phase = phase;
            this.price = price;
        }

        public bool checkRequirements(List<ShopInventoryContents> inventory)
        {

            List<Material> tempList = new List<Material>(requirements);

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
