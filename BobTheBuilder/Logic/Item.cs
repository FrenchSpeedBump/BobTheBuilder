namespace BobTheBuilder
{
    public abstract class ShopInventoryContents
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Sustainability { get; set; }
        protected ShopInventoryContents(string name, string description, double sustainability, double price)
        {
            Name = name;
            Description = description;
            Sustainability = sustainability;
            Price = price;
        }
    }
/* 
    Both item and material have both price and sustainability, however, only items can be bought and sold in the current implementation.
    If we want to implement buying materials we just delete the condition in "buy" and assign a price to the material.
    We also can assign sustainability to items in the same way we do for materials.
*/
    public class Item : ShopInventoryContents
    {
        public Item(string name, string description, double sustainability, double price) : base(name, description, sustainability, price)
        {
        }
    }

    public class Material : ShopInventoryContents
    {
        public double FloodResist { get; set; }      // Resistance to flooding (0-1)
        public double FireResist { get; set; }       // Resistance to fire (0-1)
        public double WindResist { get; set; }       // Resistance to hurricanes/wind (0-1)
        public double QuakeResist { get; set; }      // Resistance to earthquakes (0-1)

        public Material(
            string name,
            string description,
            double sustainability,
            double price,
            double floodResist = 0,
            double fireResist = 0,
            double windResist = 0,
            double quakeResist = 0) : base(name, description, sustainability, price)
        {
            FloodResist = floodResist;
            FireResist = fireResist;
            WindResist = windResist;
            QuakeResist = quakeResist;
        }
    }

}
