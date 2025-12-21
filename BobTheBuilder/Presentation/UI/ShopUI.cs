namespace BobTheBuilder
{
    public static class ShopUI
    {
        public static void DisplayInventory(Shop shop)
        {
            Console.WriteLine($"Inventory for {shop.ShortDescription}:");
            foreach (Item item in shop.Inventory.Values.OfType<Item>())
            {
                Console.WriteLine($" - {item.Name}: {item.Description} Price: {item.Price}. It affects {item.effect} by x{item.discount}");
            }
            foreach (Material material in shop.Inventory.Values.OfType<Material>())
            {
                Console.WriteLine($" - {material.Name}: {material.Description} Sustainability: {material.Sustainability} Price: {material.Price}");
            }
        }
    }
}