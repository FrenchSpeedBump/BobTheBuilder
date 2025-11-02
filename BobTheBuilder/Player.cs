namespace BobTheBuilder
{
    public class Player
    {
        public string Name = "Bob";
        public double Money = 200; // Starting budget (updated from 100)
        public double CurrentLoan = 0; // Track bank loans
        public int CurrentTurn = 1; // Track game turns
        public List<Item> Inventory = new List<Item>(); // Tools and items
        public Dictionary<string, int> MaterialInventory = new Dictionary<string, int>(); // Construction materials
        public Disaster? ActiveDisaster = null; // Current disaster that needs repair

        public void AddItem(ShopInventoryContents contents)
        {
            if (contents is Item item)
            {
                Inventory.Add(item);
            }
            else if (contents is Material material)
            {
                AddMaterial(material.Name);
            }
        }

        public void RemoveItem(ShopInventoryContents contents)
        {
            if (contents is Item item)
            {
                Inventory.Remove(item);
            }
        }

        /// <summary>
        /// Add a material to the player's material inventory.
        /// </summary>
        public void AddMaterial(string materialName)
        {
            if (MaterialInventory.ContainsKey(materialName))
            {
                MaterialInventory[materialName]++;
            }
            else
            {
                MaterialInventory[materialName] = 1;
            }
        }

        /// <summary>
        /// Check if player has required materials for a quest.
        /// </summary>
        public bool HasMaterials(Dictionary<string, int> requiredMaterials)
        {
            foreach (var requirement in requiredMaterials)
            {
                string materialName = requirement.Key;
                int requiredQuantity = requirement.Value;

                if (!MaterialInventory.ContainsKey(materialName) || MaterialInventory[materialName] < requiredQuantity)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Consume materials from inventory (when completing a quest).
        /// </summary>
        public void ConsumeMaterials(Dictionary<string, int> materials)
        {
            foreach (var requirement in materials)
            {
                string materialName = requirement.Key;
                int quantity = requirement.Value;

                if (MaterialInventory.ContainsKey(materialName))
                {
                    MaterialInventory[materialName] -= quantity;
                    
                    // Remove entry if quantity reaches 0
                    if (MaterialInventory[materialName] <= 0)
                    {
                        MaterialInventory.Remove(materialName);
                    }
                }
            }
        }

        /// <summary>
        /// Get a formatted string of missing materials.
        /// </summary>
        public string GetMissingMaterials(Dictionary<string, int> requiredMaterials)
        {
            var missing = new List<string>();
            
            foreach (var requirement in requiredMaterials)
            {
                string materialName = requirement.Key;
                int requiredQuantity = requirement.Value;
                int currentQuantity = MaterialInventory.GetValueOrDefault(materialName, 0);

                if (currentQuantity < requiredQuantity)
                {
                    int needed = requiredQuantity - currentQuantity;
                    missing.Add($"{needed}x {materialName}");
                }
            }
            
            return string.Join(", ", missing);
        }

        public void DisplayInventory()
        {
            Console.WriteLine($"\n=== INVENTORY FOR {Name} ===");
            
            // Display tools
            if (Inventory.Count > 0)
            {
                Console.WriteLine("\n🔨 Tools:");
                foreach (var item in Inventory)
                {
                    Console.WriteLine($"  - {item.Name}: {item.Description}");
                }
            }
            
            // Display materials
            if (MaterialInventory.Count > 0)
            {
                Console.WriteLine("\n📦 Materials:");
                foreach (var material in MaterialInventory)
                {
                    Console.WriteLine($"  - {material.Value}x {material.Key}");
                }
            }
            
            if (Inventory.Count == 0 && MaterialInventory.Count == 0)
            {
                Console.WriteLine("  (Empty)");
            }
            
            Console.WriteLine($"\n💰 Money: ${Money}");
            if (CurrentLoan > 0)
            {
                Console.WriteLine($"💳 Loan: ${CurrentLoan}");
            }
            Console.WriteLine();
        }

        public void BuyItem(ShopInventoryContents contents)
        {
            if (Money >= contents.Price)
            {
                Money -= contents.Price;
                AddItem(contents);
                Console.WriteLine($"Bought {contents.Name} for ${contents.Price}.");
                
                // Show updated balance
                Console.WriteLine($"💰 New balance: ${Money}");
            }
            else
            {
                Console.WriteLine($"❌ Not enough money. Need ${contents.Price}, have ${Money}.");
            }
        }
    }
}
