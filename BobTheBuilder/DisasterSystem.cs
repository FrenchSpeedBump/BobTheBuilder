namespace BobTheBuilder
{
    /// <summary>
    /// Represents a disaster event that can occur after chapter completion.
    /// </summary>
    public class Disaster
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double BaseCost { get; set; }
        public Dictionary<string, int> RequiredMaterials { get; set; }
        
        public Disaster(string name, string description, double baseCost, Dictionary<string, int> materials)
        {
            Name = name;
            Description = description;
            BaseCost = baseCost;
            RequiredMaterials = materials;
        }
    }
    
    /// <summary>
    /// Manages disaster events that occur after chapter completion.
    /// </summary>
    public static class DisasterSystem
    {
        private static Random random = new Random();
        
        /// <summary>
        /// Trigger a disaster based on completed chapter and quality score.
        /// Returns null if no disaster occurs (based on quality).
        /// </summary>
        public static Disaster? TriggerDisaster(QuestChapter chapter, double averageQuality)
        {
            // Higher quality = lower chance of disaster
            // Quality 8-10: 10% chance
            // Quality 5-7: 30% chance
            // Quality 0-4: 60% chance
            
            double disasterChance;
            if (averageQuality >= 8.0)
                disasterChance = 0.1;
            else if (averageQuality >= 5.0)
                disasterChance = 0.3;
            else
                disasterChance = 0.6;
            
            // Roll for disaster
            if (random.NextDouble() > disasterChance)
            {
                return null; // No disaster!
            }
            
            // Select disaster based on chapter
            return GenerateDisaster(chapter, averageQuality);
        }
        
        /// <summary>
        /// Generate a specific disaster for the chapter.
        /// </summary>
        private static Disaster GenerateDisaster(QuestChapter chapter, double averageQuality)
        {
            List<Disaster> possibleDisasters = chapter switch
            {
                QuestChapter.Foundation => GetFoundationDisasters(),
                QuestChapter.Walls => GetWallsDisasters(),
                QuestChapter.Roof => GetRoofDisasters(),
                _ => new List<Disaster>()
            };
            
            // Pick random disaster from list
            Disaster disaster = possibleDisasters[random.Next(possibleDisasters.Count)];
            
            // Scale damage based on quality (lower quality = higher cost)
            double qualityMultiplier = 1.0 + (10.0 - averageQuality) / 10.0; // 1.0 to 2.0x
            disaster.BaseCost = Math.Round(disaster.BaseCost * qualityMultiplier, 2);
            
            return disaster;
        }
        
        private static List<Disaster> GetFoundationDisasters()
        {
            return new List<Disaster>
            {
                new Disaster(
                    "Foundation Cracks",
                    "Severe cracks have appeared in the foundation due to improper settling. This needs immediate repair!",
                    120,
                    new Dictionary<string, int> { { "Concrete", 3 }, { "Waterproofing", 1 } }
                ),
                new Disaster(
                    "Water Seepage",
                    "Water is seeping through the foundation walls. The waterproofing has failed!",
                    80,
                    new Dictionary<string, int> { { "Waterproofing", 2 } }
                ),
                new Disaster(
                    "Ground Settling",
                    "The ground has settled unevenly, causing structural stress. Foundation reinforcement needed.",
                    150,
                    new Dictionary<string, int> { { "Concrete", 4 }, { "Steel", 2 } }
                )
            };
        }
        
        private static List<Disaster> GetWallsDisasters()
        {
            return new List<Disaster>
            {
                new Disaster(
                    "Wall Cracks",
                    "Large cracks have formed in the walls. The structure needs reinforcement!",
                    100,
                    new Dictionary<string, int> { { "Bricks", 4 }, { "Drywall", 2 } }
                ),
                new Disaster(
                    "Insulation Failure",
                    "Poor insulation installation has left gaps. Energy costs will be high unless fixed!",
                    90,
                    new Dictionary<string, int> { { "Insulation", 3 } }
                ),
                new Disaster(
                    "Water Damage",
                    "Water has leaked into the walls, causing damage to drywall and structure.",
                    130,
                    new Dictionary<string, int> { { "Drywall", 4 }, { "Wood", 2 }, { "Paint", 1 } }
                ),
                new Disaster(
                    "Structural Weakness",
                    "Walls are showing signs of structural weakness. Steel reinforcement needed.",
                    140,
                    new Dictionary<string, int> { { "Steel", 3 }, { "Concrete", 2 } }
                )
            };
        }
        
        private static List<Disaster> GetRoofDisasters()
        {
            return new List<Disaster>
            {
                new Disaster(
                    "Roof Leak",
                    "Heavy rain has revealed multiple leaks in the roof. Immediate repairs needed!",
                    110,
                    new Dictionary<string, int> { { "RoofTiles", 5 }, { "Waterproofing", 2 } }
                ),
                new Disaster(
                    "Wind Damage",
                    "Strong winds have damaged the roof structure. Several tiles are missing or broken.",
                    95,
                    new Dictionary<string, int> { { "RoofTiles", 6 }, { "Wood", 1 } }
                ),
                new Disaster(
                    "Poor Ventilation",
                    "Inadequate ventilation has caused moisture buildup, damaging roof materials.",
                    85,
                    new Dictionary<string, int> { { "Wood", 2 }, { "RoofTiles", 3 } }
                ),
                new Disaster(
                    "Structural Sagging",
                    "The roof is sagging due to inadequate support. Major structural repairs required!",
                    160,
                    new Dictionary<string, int> { { "Wood", 4 }, { "Steel", 2 }, { "RoofTiles", 3 } }
                )
            };
        }
        
        /// <summary>
        /// Display disaster event to player.
        /// </summary>
        public static void DisplayDisaster(Disaster disaster)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    ⚠️  DISASTER! ⚠️                        ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ {disaster.Name.PadRight(58)} ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            
            // Word wrap description
            var words = disaster.Description.Split(' ');
            string line = "║ ";
            foreach (var word in words)
            {
                if (line.Length + word.Length + 1 > 60)
                {
                    Console.WriteLine(line.PadRight(60) + "║");
                    line = "║ " + word + " ";
                }
                else
                {
                    line += word + " ";
                }
            }
            if (line.Length > 2)
            {
                Console.WriteLine(line.PadRight(60) + "║");
            }
            
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 🔧 REPAIR REQUIREMENTS:                                    ║");
            Console.WriteLine($"║   Labor Cost: ${disaster.BaseCost}                                   ║");
            
            if (disaster.RequiredMaterials.Count > 0)
            {
                Console.WriteLine("║   Materials Needed:                                        ║");
                foreach (var material in disaster.RequiredMaterials)
                {
                    Console.WriteLine($"║     • {material.Value}x {material.Key.PadRight(40)} ║");
                }
            }
            
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine("\n💡 Visit Bob's Materials to buy required materials!");
            Console.WriteLine("   Then use 'repair' command to fix the damage.");
        }
    }
}
