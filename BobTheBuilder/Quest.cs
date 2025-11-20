
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace BobTheBuilder
{
    public class Quest
    {
        public string shortDescription { get; private set; }

        public string longDescription { get; private set; }

        public bool isCompleted { get; private set; }

        public int phase { get; private set; }

        List<ShopInventoryContents> requirements = new();
        public int price { get; private set; }


        public Quest(string shortDescription, string longDescription, List<ShopInventoryContents>? requirements , int phase, int price) 
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
        }

        public bool checkRequirements(List<ShopInventoryContents> inventory)
        {

            List<ShopInventoryContents> tempList = new List<ShopInventoryContents>();
            tempList = requirements;

            foreach (ShopInventoryContents req in tempList)
            {

                foreach (ShopInventoryContents item in inventory)
                {
                }
            }

            return false;
        }
    }
}
