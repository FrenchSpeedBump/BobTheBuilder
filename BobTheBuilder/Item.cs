
namespace BobTheBuilder
{
    public class Item
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; set; }
        public Item(string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
