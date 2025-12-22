namespace BobTheBuilder
{
    public static class ShopUI
    {
        public static void DisplayInventory(Shop shop)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n--- {shop.ShortDescription} Inventory ---\n");
            Console.ResetColor();
            
            bool hasItems = false;
            foreach (Item item in shop.Inventory.Values.OfType<Item>())
            {
                hasItems = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  {item.Name}");
                Console.ResetColor();
                Console.WriteLine($" - {item.Description}");
                Console.WriteLine($"    Price: ${item.Price} | Effect: {item.Effect} x{item.Discount}");
            }
            
            foreach (Material material in shop.Inventory.Values.OfType<Material>())
            {
                hasItems = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  {material.Name}");
                Console.ResetColor();
                Console.WriteLine($" - {material.Description}");
                Console.WriteLine($"    Price: ${material.Price} | Sustainability: {material.Sustainability.ToString("F2")}");
            }
            
            if (!hasItems)
            {
                Console.WriteLine("  No items available");
            }
            Console.WriteLine();
        }
    }
}