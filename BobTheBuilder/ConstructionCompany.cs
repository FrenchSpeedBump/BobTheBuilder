namespace BobTheBuilder
{
    /// <summary>
    /// Represents a construction company with its unique characteristics and pricing strategy.
    /// Each company generates different offers for the same quest based on their specialty.
    /// </summary>
    public class ConstructionCompany
    {
        public string Name { get; }
        public string Specialty { get; }
        public CompanyType Type { get; }

        public ConstructionCompany(string name, string specialty, CompanyType type)
        {
            Name = name;
            Specialty = specialty;
            Type = type;
        }

        /// <summary>
        /// Generate all offers (standard + special) for a specific quest.
        /// Returns a list of 1-2 options per company.
        /// </summary>
        public List<QuestOption> GenerateOffers(Quest quest, Dictionary<string, Material> availableMaterials)
        {
            var offers = new List<QuestOption>();
            
            // 1. Always add standard offer
            offers.Add(GenerateStandardOffer(quest, availableMaterials));
            
            // 2. Check for special offer
            var specialOffer = GenerateSpecialOffer(quest, availableMaterials);
            if (specialOffer != null)
            {
                offers.Add(specialOffer);
            }
            
            return offers;
        }

        /// <summary>
        /// Generate standard offer using base materials and company multipliers.
        /// </summary>
        private QuestOption GenerateStandardOffer(Quest quest, Dictionary<string, Material> availableMaterials)
        {
            // 1. Get base materials from quest and apply company quantity multiplier
            Dictionary<string, int> materials = ApplyMaterialMultiplier(quest.BaseMaterials);
            
            // 2. Calculate sustainability from materials
            int sustainability = CalculateSustainabilityFromMaterials(materials, availableMaterials);
            
            // 3. Calculate quality from materials
            int quality = CalculateQualityFromMaterials(materials, availableMaterials);
            
            // 4. Apply company bonus to ratings
            sustainability = (int)(sustainability * GetSustainabilityBonus());
            quality = (int)(quality * GetQualityBonus());
            
            // Clamp values to 1-10 range
            sustainability = Math.Clamp(sustainability, 1, 10);
            quality = Math.Clamp(quality, 1, 10);
            
            // 5. Calculate service cost with company multiplier
            double serviceCost = quest.BaseServiceCost * GetCostMultiplier();
            
            // 6. Generate description
            string description = GenerateOfferDescription(quest);
            
            return new QuestOption(
                Name,
                description,
                serviceCost,
                materials,
                sustainability,
                quality
            );
        }

        /// <summary>
        /// Generate special company-specific offer for this quest (if available).
        /// Returns null if no special offer exists.
        /// </summary>
        private QuestOption? GenerateSpecialOffer(Quest quest, Dictionary<string, Material> availableMaterials)
        {
            // Special offers are defined per quest ID and company type
            var key = (quest.Id, Type);
            
            return key switch
            {
                // ═══════════════════════════════════════════════════════════════
                // FOUNDATION CHAPTER SPECIAL OFFERS
                // ═══════════════════════════════════════════════════════════════
                
                // F1: Site Excavation
                ("F1", CompanyType.BigBuild) => new QuestOption(
                    Name,
                    "MINIMAL EXCAVATION: Quick shallow dig. Saves money but risks future settling issues.",
                    60, // Cheap!
                    new Dictionary<string, int> { { "Concrete", 1 }, { "Wood", 1 } },
                    2, // Very low sustainability
                    3  // Low quality - risky!
                ),
                
                ("F1", CompanyType.SmallBuild) => new QuestOption(
                    Name,
                    "ECO DEEP-DIG: Proper excavation with soil analysis and sustainable practices.",
                    140, // Expensive but thorough
                    new Dictionary<string, int> { { "Concrete", 2 }, { "Wood", 3 }, { "Insulation", 1 } },
                    8, // High sustainability
                    8  // High quality
                ),
                
                // F2: Foundation Pouring
                ("F2", CompanyType.BigBuild) => new QuestOption(
                    Name,
                    "QUICK POUR: Fast concrete pour without steel reinforcement. Saves time and money.",
                    70,
                    new Dictionary<string, int> { { "Concrete", 8 } }, // No steel!
                    3,
                    4 // Weak foundation
                ),
                
                ("F2", CompanyType.BestBuild) => new QuestOption(
                    Name,
                    "REINFORCED FOUNDATION: Premium concrete with extra steel reinforcement and waterproofing.",
                    180,
                    new Dictionary<string, int> { { "Concrete", 5 }, { "Steel", 5 }, { "Waterproofing", 2 } },
                    5,
                    9 // Very strong!
                ),
                
                ("F2", CompanyType.SmallBuild) => new QuestOption(
                    Name,
                    "HYBRID WOOD-CONCRETE: Reduces concrete use with sustainable wood pilings (Venice-style!).",
                    150,
                    new Dictionary<string, int> { { "Concrete", 3 }, { "Wood", 8 }, { "Insulation", 2 } },
                    8, // Eco-friendly
                    6  // Good but not as strong as full concrete
                ),
                
                // F3: Waterproofing
                ("F3", CompanyType.BigBuild) => new QuestOption(
                    Name,
                    "BASIC COATING: Minimal waterproofing layer. Cheap but may need repairs sooner.",
                    60,
                    new Dictionary<string, int> { { "Waterproofing", 2 }, { "Concrete", 1 } },
                    4,
                    4
                ),
                
                ("F3", CompanyType.SmallBuild) => new QuestOption(
                    Name,
                    "ECO MEMBRANE SYSTEM: Advanced sustainable waterproofing with drainage system.",
                    130,
                    new Dictionary<string, int> { { "Waterproofing", 6 }, { "Insulation", 2 }, { "Wood", 2 } },
                    9,
                    8
                ),
                
                // F4: Foundation Insulation
                ("F4", CompanyType.BigBuild) => new QuestOption(
                    Name,
                    "SKIP INSULATION: Save money now. Warning: Higher energy bills forever!",
                    30, // Very cheap
                    new Dictionary<string, int> { { "Wood", 2 } }, // No insulation!
                    1, // Terrible for sustainability
                    3  // Poor energy efficiency
                ),
                
                ("F4", CompanyType.BestBuild) => new QuestOption(
                    Name,
                    "PREMIUM INSULATION: Maximum R-value insulation for optimal energy efficiency.",
                    150,
                    new Dictionary<string, int> { { "Insulation", 8 }, { "Waterproofing", 2 }, { "Wood", 2 } },
                    8,
                    8
                ),
                
                ("F4", CompanyType.SmallBuild) => new QuestOption(
                    Name,
                    "RECYCLED INSULATION: High-performance insulation from recycled materials.",
                    130,
                    new Dictionary<string, int> { { "Insulation", 7 }, { "Wood", 3 } },
                    9, // Very eco-friendly
                    7
                ),
                
                // ═══════════════════════════════════════════════════════════════
                // WALLS CHAPTER SPECIAL OFFERS
                // ═══════════════════════════════════════════════════════════════
                
                // W1: Wall Framing
                ("W1", CompanyType.BigBuild) => new QuestOption(
                    Name,
                    "METAL FRAME: All-steel framing. Strong but high carbon footprint.",
                    110,
                    new Dictionary<string, int> { { "Steel", 10 } }, // All steel, no wood
                    2, // Low sustainability
                    9  // Very strong
                ),
                
                ("W1", CompanyType.SmallBuild) => new QuestOption(
                    Name,
                    "SUSTAINABLE WOOD FRAME: Certified sustainable wood from responsibly managed forests.",
                    160,
                    new Dictionary<string, int> { { "Wood", 12 }, { "Steel", 1 } },
                    9,
                    7
                ),
                
                // W2: Wall Construction
                ("W2", CompanyType.BigBuild) => new QuestOption(
                    Name,
                    "CONCRETE BLOCKS: Fast concrete block construction. Industrial and efficient.",
                    120,
                    new Dictionary<string, int> { { "Concrete", 12 }, { "Steel", 2 } },
                    3,
                    7
                ),
                
                ("W2", CompanyType.BestBuild) => new QuestOption(
                    Name,
                    "BRICK & STONE MIX: Traditional craftsmanship with brick and stone. Very durable.",
                    190,
                    new Dictionary<string, int> { { "Bricks", 15 }, { "Concrete", 4 }, { "Wood", 3 } },
                    6,
                    9
                ),
                
                ("W2", CompanyType.SmallBuild) => new QuestOption(
                    Name,
                    "ECO-BRICKS: Compressed earth or recycled material bricks. Highly sustainable!",
                    170,
                    new Dictionary<string, int> { { "Bricks", 8 }, { "Wood", 6 }, { "Insulation", 3 } },
                    9,
                    6
                ),
                
                // W3: Wall Insulation
                ("W3", CompanyType.BigBuild) => new QuestOption(
                    Name,
                    "MINIMAL INSULATION: Basic insulation to meet minimum standards only.",
                    80,
                    new Dictionary<string, int> { { "Insulation", 4 }, { "Wood", 2 } },
                    4,
                    4
                ),
                
                ("W3", CompanyType.SmallBuild) => new QuestOption(
                    Name,
                    "NATURAL FIBER INSULATION: Hemp or wool insulation. Renewable and breathable!",
                    150,
                    new Dictionary<string, int> { { "Insulation", 10 }, { "Wood", 4 } },
                    10, // Maximum sustainability
                    7
                ),
                
                // W4: Exterior Finish
                ("W4", CompanyType.BigBuild) => new QuestOption(
                    Name,
                    "BASIC PAINT: Standard industrial paint. Gets the job done.",
                    90,
                    new Dictionary<string, int> { { "Paint", 4 }, { "Waterproofing", 2 } },
                    4,
                    5
                ),
                
                ("W4", CompanyType.SmallBuild) => new QuestOption(
                    Name,
                    "LIVING WALL SYSTEM: Green wall with plants and eco-finish. Carbon-negative!",
                    200,
                    new Dictionary<string, int> { { "Paint", 3 }, { "Waterproofing", 4 }, { "Wood", 5 }, { "Insulation", 2 } },
                    10, // Ultimate sustainability
                    7
                ),
                
                // W5: Interior Drywall
                ("W5", CompanyType.BestBuild) => new QuestOption(
                    Name,
                    "SOUNDPROOF DRYWALL: Premium acoustic drywall for better living quality.",
                    150,
                    new Dictionary<string, int> { { "Drywall", 15 }, { "Insulation", 3 }, { "Wood", 2 } },
                    6,
                    8
                ),
                
                ("W5", CompanyType.SmallBuild) => new QuestOption(
                    Name,
                    "RECYCLED DRYWALL: Made from recycled gypsum. Eco-friendly and quality.",
                    140,
                    new Dictionary<string, int> { { "Drywall", 14 }, { "Wood", 3 } },
                    9,
                    7
                ),
                
                // ═══════════════════════════════════════════════════════════════
                // ROOF CHAPTER SPECIAL OFFERS
                // ═══════════════════════════════════════════════════════════════
                
                // R1: Roof Framing
                ("R1", CompanyType.BigBuild) => new QuestOption(
                    Name,
                    "LIGHTWEIGHT FRAME: Minimal wood and steel. Cheap but less storm-resistant.",
                    110,
                    new Dictionary<string, int> { { "Wood", 6 }, { "Steel", 2 } },
                    4,
                    5
                ),
                
                ("R1", CompanyType.BestBuild) => new QuestOption(
                    Name,
                    "STORM-RESISTANT FRAME: Extra reinforcement for extreme weather protection.",
                    220,
                    new Dictionary<string, int> { { "Wood", 12 }, { "Steel", 6 } },
                    5,
                    10 // Maximum storm protection
                ),
                
                // R2: Roof Covering
                ("R2", CompanyType.BigBuild) => new QuestOption(
                    Name,
                    "STANDARD SHINGLES: Basic asphalt shingles. Cheap and functional.",
                    130,
                    new Dictionary<string, int> { { "RoofTiles", 10 }, { "Waterproofing", 2 } },
                    3,
                    5
                ),
                
                ("R2", CompanyType.BestBuild) => new QuestOption(
                    Name,
                    "PREMIUM TILES: High-end ceramic tiles. Extremely durable, lasts 50+ years.",
                    250,
                    new Dictionary<string, int> { { "RoofTiles", 20 }, { "Waterproofing", 6 }, { "Wood", 3 } },
                    5,
                    10
                ),
                
                ("R2", CompanyType.SmallBuild) => new QuestOption(
                    Name,
                    "SOLAR-READY GREEN ROOF: Vegetation layer with solar panel integration. Future-proof!",
                    280,
                    new Dictionary<string, int> { { "RoofTiles", 10 }, { "Insulation", 8 }, { "Waterproofing", 6 }, { "Wood", 4 } },
                    10, // Maximum eco rating
                    8
                ),
                
                // R3: Roof Insulation & Drainage
                ("R3", CompanyType.BigBuild) => new QuestOption(
                    Name,
                    "BASIC GUTTERS ONLY: Skip roof insulation entirely. Install minimal gutters.",
                    90,
                    new Dictionary<string, int> { { "Steel", 3 }, { "Wood", 2 } },
                    2,
                    4
                ),
                
                ("R3", CompanyType.SmallBuild) => new QuestOption(
                    Name,
                    "RAINWATER COLLECTION: Advanced system with insulation and water harvesting!",
                    230,
                    new Dictionary<string, int> { { "Insulation", 8 }, { "Steel", 4 }, { "Wood", 4 }, { "Waterproofing", 2 } },
                    10, // Eco-friendly water management
                    9
                ),
                
                _ => null // No special offer for this combination
            };
        }

        /// <summary>
        /// Apply company-specific material quantity multiplier.
        /// Big Build uses less (0.8x), Best Build standard (1.0x), Small Build uses more (1.2x).
        /// </summary>
        private Dictionary<string, int> ApplyMaterialMultiplier(Dictionary<string, int> baseMaterials)
        {
            double multiplier = Type switch
            {
                CompanyType.BigBuild => 0.8,
                CompanyType.BestBuild => 1.0,
                CompanyType.SmallBuild => 1.2,
                _ => 1.0
            };
            
            var result = new Dictionary<string, int>();
            foreach (var kvp in baseMaterials)
            {
                // Round up to ensure at least 1 material
                result[kvp.Key] = Math.Max(1, (int)Math.Ceiling(kvp.Value * multiplier));
            }
            return result;
        }

        /// <summary>
        /// Calculate sustainability rating from materials (weighted average on 1-10 scale).
        /// </summary>
        private int CalculateSustainabilityFromMaterials(Dictionary<string, int> materials, Dictionary<string, Material> availableMaterials)
        {
            double totalSustainability = 0;
            int totalQuantity = 0;
            
            foreach (var kvp in materials)
            {
                if (availableMaterials.TryGetValue(kvp.Key, out Material? material))
                {
                    totalSustainability += material.Sustainability * kvp.Value;
                    totalQuantity += kvp.Value;
                }
            }
            
            if (totalQuantity == 0) return 5; // Default if no materials
            
            // Convert 0.0-1.0 scale to 1-10 scale
            double average = totalSustainability / totalQuantity;
            return (int)Math.Round(average * 10);
        }

        /// <summary>
        /// Calculate quality rating from materials (weighted average on 1-10 scale).
        /// </summary>
        private int CalculateQualityFromMaterials(Dictionary<string, int> materials, Dictionary<string, Material> availableMaterials)
        {
            double totalQuality = 0;
            int totalQuantity = 0;
            
            foreach (var kvp in materials)
            {
                if (availableMaterials.TryGetValue(kvp.Key, out Material? material))
                {
                    totalQuality += material.Quality * kvp.Value;
                    totalQuantity += kvp.Value;
                }
            }
            
            if (totalQuantity == 0) return 5; // Default if no materials
            
            // Convert 0.0-1.0 scale to 1-10 scale
            double average = totalQuality / totalQuantity;
            return (int)Math.Round(average * 10);
        }

        /// <summary>
        /// Get company-specific cost multiplier.
        /// </summary>
        private double GetCostMultiplier()
        {
            return Type switch
            {
                CompanyType.BigBuild => 0.7,
                CompanyType.BestBuild => 1.0,
                CompanyType.SmallBuild => 1.3,
                _ => 1.0
            };
        }

        /// <summary>
        /// Get company-specific sustainability bonus/penalty.
        /// </summary>
        private double GetSustainabilityBonus()
        {
            return Type switch
            {
                CompanyType.BigBuild => 0.95,    // 5% penalty
                CompanyType.BestBuild => 1.05,   // 5% bonus
                CompanyType.SmallBuild => 1.15,  // 15% bonus
                _ => 1.0
            };
        }

        /// <summary>
        /// Get company-specific quality bonus/penalty.
        /// </summary>
        private double GetQualityBonus()
        {
            return Type switch
            {
                CompanyType.BigBuild => 0.95,    // 5% penalty
                CompanyType.BestBuild => 1.05,   // 5% bonus
                CompanyType.SmallBuild => 1.15,  // 15% bonus (eco-friendly often means durable)
                _ => 1.0
            };
        }

        /// <summary>
        /// Generate descriptive text for the offer.
        /// </summary>
        private string GenerateOfferDescription(Quest quest)
        {
            return Type switch
            {
                CompanyType.BestBuild => $"Premium service with quality materials and expert craftsmanship for {quest.Name}.",
                CompanyType.BigBuild => $"Fast and affordable solution for {quest.Name}. We get it done quickly!",
                CompanyType.SmallBuild => $"Eco-friendly and sustainable approach to {quest.Name} using green materials.",
                _ => $"Standard construction service for {quest.Name}."
            };
        }
    }

    /// <summary>
    /// The three types of construction companies, each with different characteristics.
    /// </summary>
    public enum CompanyType
    {
        BestBuild,  // Balanced, reliable
        BigBuild,   // Cheap, fast, low sustainability
        SmallBuild  // Expensive, eco-friendly
    }
}
