
namespace BobTheBuilder
{
    //Shop class with inventory variable, which will hold multiple item objects of type Item 
    public class Shop
    {
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set; }
        public Room? Exit { get; private set; }
        public Dictionary<string, Item> Inventory { get; private set; } = new();
        public Shop(string shortDesc, string longDesc)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
        }
        //no direction is set for shop exit since we only have 1 exit, ideally we would have an exit command that simply returns to previous room
        public void SetExit(Room exit)
        {
            Exit = exit;
        }
        public void AddItem(Item item)
        {
            Inventory[item.Name] = item;
        }
        public void RemoveItem(Item item)
        {
            Inventory.Remove(item.Name);
        }

    }
}
