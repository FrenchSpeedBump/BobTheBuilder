namespace BobTheBuilder
{
    /// <summary>
    /// Represents a construction company's office where players can view and accept quest offers.
    /// Each office is associated with one construction company.
    /// </summary>
    public class ConstructionOffice : Room
    {
        public ConstructionCompany Company { get; }
        private List<QuestOption> currentOffers;
        private Quest? activeQuest;
        private Dictionary<string, Material> availableMaterials;

        public ConstructionOffice(string shortDesc, string longDesc, ConstructionCompany company, Dictionary<string, Material> materials) 
            : base(shortDesc, longDesc)
        {
            Company = company;
            currentOffers = new List<QuestOption>();
            availableMaterials = materials;
        }

        /// <summary>
        /// Update the offers displayed in this office based on the active quest.
        /// </summary>
        public void UpdateOffers(Quest quest)
        {
            activeQuest = quest;
            currentOffers = Company.GenerateOffers(quest, availableMaterials);
        }

        /// <summary>
        /// Get the offer by index (1-based for user display).
        /// </summary>
        public QuestOption? GetOffer(int index)
        {
            if (index < 1 || index > currentOffers.Count)
                return null;
            
            return currentOffers[index - 1]; // Convert to 0-based index
        }

        /// <summary>
        /// Get all current offers.
        /// </summary>
        public List<QuestOption> GetAllOffers()
        {
            return currentOffers;
        }

        /// <summary>
        /// Display all offers for the active quest in a formatted manner.
        /// </summary>
        public void DisplayOffers(Player player)
        {
            if (activeQuest == null || currentOffers.Count == 0)
            {
                Console.WriteLine("No active quest available.");
                return;
            }

            Console.WriteLine("\n╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {Company.Name.ToUpper()} - OFFERS FOR: {activeQuest.Name}");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝\n");

            for (int i = 0; i < currentOffers.Count; i++)
            {
                var offer = currentOffers[i];
                int optionNumber = i + 1;
                
                // Determine offer type label
                string offerType = (i == 0) ? "STANDARD" : "SPECIAL";
                
                Console.WriteLine($"┌─ OPTION {optionNumber} - {offerType} ─────────────────────────────────");
                Console.WriteLine($"│ {offer.OfferDescription}");
                Console.WriteLine($"│");
                Console.WriteLine($"│ 💵 Service Cost: ${offer.ServiceCost}");
                Console.WriteLine($"│ 📦 Required Materials: {offer.FormatRequiredMaterials()}");
                
                // Calculate total material cost
                double materialCost = offer.CalculateTotalMaterialCost(availableMaterials);
                double totalCost = offer.ServiceCost + materialCost;
                Console.WriteLine($"│ 💰 Material Cost: ${materialCost}");
                Console.WriteLine($"│ 💸 TOTAL COST: ${totalCost}");
                Console.WriteLine($"│");
                
                // Display ratings with stars
                string sustainStars = new string('⭐', offer.SustainabilityRating);
                string qualityStars = new string('⭐', offer.QualityRating);
                Console.WriteLine($"│ 🌱 Sustainability: {sustainStars} ({offer.SustainabilityRating}/10)");
                Console.WriteLine($"│ 🏗️  Quality: {qualityStars} ({offer.QualityRating}/10)");
                Console.WriteLine($"└────────────────────────────────────────────────────────");
                Console.WriteLine();
            }

            // Display player status
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine($"💰 Your Balance: ${player.Money}");
            
            if (player.MaterialInventory.Count > 0)
            {
                Console.WriteLine($"📦 Your Materials: {string.Join(", ", player.MaterialInventory.Select(m => $"{m.Value}x {m.Key}"))}");
            }
            else
            {
                Console.WriteLine("📦 Your Materials: None");
            }
            
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("\nCommands: 'accept <number>' to accept an offer | 'back' to leave");
            Console.WriteLine();
        }

        /// <summary>
        /// Check if player can afford and has materials for a specific offer.
        /// </summary>
        public bool CanAcceptOffer(Player player, QuestOption offer, out string errorMessage)
        {
            // Check materials
            if (!player.HasMaterials(offer.RequiredMaterials))
            {
                errorMessage = $"❌ You don't have the required materials!\n   Need: {offer.FormatRequiredMaterials()}\n   Missing: {player.GetMissingMaterials(offer.RequiredMaterials)}";
                return false;
            }

            // Check money
            if (player.Money < offer.ServiceCost)
            {
                errorMessage = $"❌ Not enough money for service cost!\n   Need: ${offer.ServiceCost}\n   Have: ${player.Money}";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
