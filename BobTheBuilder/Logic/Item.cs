namespace BobTheBuilder
{
    public abstract class ShopInventoryContents
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        protected ShopInventoryContents(string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
    public class Item : ShopInventoryContents
    {
        public string? Effect { get; set; } //what does it affect
        public double Discount { get; set; } //by how much

        public Item(string name, string description, double price, string effect, double discount) : base(name, description, price)
        {
            this.Effect = effect;
            this.Discount = discount;
        }
        public Item(string name, string description, double price) : base(name, description, price)
        {
            this.Effect = null;
            this.Discount = 1;
        }
    }

    public class Material : ShopInventoryContents
    {
        public double Sustainability;
        public double Quality;
        public Material(string name, string description, double sustainability, double quality, double price) : base(name, description, price)
        {
            this.Sustainability = sustainability;
            this.Quality = quality;
        }
    }

}
