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
        public Material(string name, string description, double sustainability, double price) : base(name, description, sustainability, price)
        {
        }
    }

}
