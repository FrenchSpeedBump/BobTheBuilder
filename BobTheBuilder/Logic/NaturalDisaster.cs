namespace BobTheBuilder
{
    // Hazard categories the game can test for.
    public enum HazardType
    {
        Flood,
        Fire,
        Wind,
        Earthquake
    }

    // Represents a single disaster with its target parts and the resistance needed to survive.
    public class NaturalDisaster
    {
        public string Name { get; }                       // Display name, e.g., "Flood".
        public HazardType Hazard { get; }                 // Which hazard type this disaster uses.
        public List<string> AffectedParts { get; }        // House parts to evaluate (e.g., "walls", "roof").
        public double Threshold { get; }                  // Required resistance (0-1) to pass the check.

        public NaturalDisaster(string name, HazardType hazard, IEnumerable<string> parts, double threshold)
        {
            Name = name;                                  // Store the display name.
            Hazard = hazard;                              // Store the hazard type.
            AffectedParts = new List<string>(parts);      // Copy the list of parts we will check.
            Threshold = threshold;                        // Store the pass/fail cutoff.
        }
    }

    // Runs disaster rolls on a schedule and checks the house materials against hazard thresholds.
    public class DisasterManager
    {
        private readonly List<NaturalDisaster> _disasters;                           // Pool of possible disasters.
        private readonly int _intervalDays;                                          // How often (in days) we attempt a roll.
        private readonly double _baseChance;                                         // Probability of a disaster on a roll (0-1).

        public DisasterManager()
        {
            _intervalDays = 1;                                                       // Check every day.
            _baseChance = 0.2;                                                       // 20% chance to trigger each day.

            // Define the disasters we support and which parts they stress.
            _disasters = new List<NaturalDisaster>
            {
                new NaturalDisaster("Flood", HazardType.Flood, new[]{ "foundation", "floors", "walls" }, 0.6),
                new NaturalDisaster("Wildfire", HazardType.Fire, new[]{ "walls", "roof" }, 0.6),
                new NaturalDisaster("Hurricane", HazardType.Wind, new[]{ "walls", "roof" }, 0.7),
                new NaturalDisaster("Earthquake", HazardType.Earthquake, new[]{ "foundation", "floors", "walls", "roof" }, 0.7)
            };
        }

        // Call this once per day. It rolls for a disaster based on the schedule and reports results via log.
        // Returns false if the house collapses (e.g., missing material on an affected part).
        public bool TryTrigger(int day, House house, Random rng, Action<string> log)
        {
            if (house.UsedMaterials.Count == 0) return true;                         // Do nothing until at least one part is built.
            if (day % _intervalDays != 0) return true;                               // Only roll on interval days.
            if (rng.NextDouble() > _baseChance) return true;                         // Skip if the random roll misses.

            NaturalDisaster disaster = _disasters[rng.Next(_disasters.Count)];       // Pick a random disaster.
            log($"A {disaster.Name} hit the area!");                                 // Announce the event.

            foreach (string part in disaster.AffectedParts)                          // Evaluate each affected house part.
            {
                house.UsedMaterials.TryGetValue(part, out var material);             // Look up the material for this part.
                if (material == null)
                {
                    log($"- {part} had no material and collapsed. Your house is no longer safe.");
                    return false;                                                    // Game over if a required part is missing.
                }

                double resistance = GetResistance(material, disaster.Hazard);        // Get the resistance score.
                bool survived = resistance >= disaster.Threshold;                    // Pass/fail against the threshold.

                if (survived)
                {
                    log($"- {part} held up thanks to {material?.Name ?? "no material"} (resistance {resistance:0.00}).");
                }
                else
                {
                    log($"- {part} was damaged; {material?.Name ?? "no material"} had only {resistance:0.00} vs required {disaster.Threshold:0.00}.");
                    house.UsedMaterials.Remove(part);                                // Simple approach: damaged part loses its material; player must rebuild.
                }
            }
            return true;                                                            // House survived this disaster.
        }

        // Looks up a material's resistance to a hazard; falls back to sustainability if not explicitly mapped.
        private double GetResistance(Material? material, HazardType hazard)
        {
            if (material == null) return 0;                                          // No material means no protection.
            return hazard switch
            {
                HazardType.Flood => material.FloodResist,
                HazardType.Fire => material.FireResist,
                HazardType.Wind => material.WindResist,
                HazardType.Earthquake => material.QuakeResist,
                _ => material.Sustainability
            };
        }
    }
}
