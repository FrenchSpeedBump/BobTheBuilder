namespace BobTheBuilder
{
    /// <summary>
    /// Represents a construction company's offer for completing a specific quest.
    /// Each company provides different costs, materials, sustainability, and quality.
    /// </summary>
    public class QuestOption
    {
        public string CompanyName { get; }
        public string OfferDescription { get; }
        public double ServiceCost { get; } // The labor/service fee (not including materials)
        public Dictionary<string, int> RequiredMaterials { get; } // e.g., {"Concrete": 3, "Wood": 2}
        public int SustainabilityRating { get; } // 1-10 scale
        public int QualityRating { get; } // 1-10 scale (affects disaster resistance)

        public QuestOption(
            string companyName,
            string offerDescription,
            double serviceCost,
            Dictionary<string, int> requiredMaterials,
            int sustainabilityRating,
            int qualityRating)
        {
            CompanyName = companyName;
            OfferDescription = offerDescription;
            ServiceCost = serviceCost;
            RequiredMaterials = requiredMaterials;
            SustainabilityRating = sustainabilityRating;
            QualityRating = qualityRating;
        }

        /// <summary>
        /// Calculate the total cost including materials.
        /// Requires access to material prices (will be calculated when displaying offer).
        /// </summary>
        public double CalculateTotalMaterialCost(Dictionary<string, Material> availableMaterials)
        {
            double totalMaterialCost = 0;
            foreach (var requirement in RequiredMaterials)
            {
                string materialName = requirement.Key;
                int quantity = requirement.Value;

                // Find the material in available materials
                if (availableMaterials.TryGetValue(materialName, out Material? material))
                {
                    totalMaterialCost += material.Price * quantity;
                }
            }
            return totalMaterialCost;
        }

        /// <summary>
        /// Format the required materials as a readable string.
        /// Example: "3x Concrete, 2x Wood"
        /// </summary>
        public string FormatRequiredMaterials()
        {
            return string.Join(", ", RequiredMaterials.Select(kvp => $"{kvp.Value}x {kvp.Key}"));
        }
    }
}
