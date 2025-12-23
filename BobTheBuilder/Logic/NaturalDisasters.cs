namespace BobTheBuilder.Logic
{
    public class DisasterResult
    {
        public bool HouseSurvived { get; set; }
        public bool DisasterOccurred { get; set; }
        public string? DisasterName { get; set; }
        public double FoundationDamage { get; set; }
        public double WallsDamage { get; set; }
        public double RoofDamage { get; set; }
    }

    internal class NaturalDisaster
    {
        public string name;
        public double affectFoundation;
        public double affectWalls;
        public double affectRoof;

        public NaturalDisaster(string name, double affectFoundation, double affectWalls, double affectRoof)
        {
            this.name = name;
            this.affectFoundation = affectFoundation;
            this.affectWalls = affectWalls;
            this.affectRoof = affectRoof;
        }
    }
    public class NaturalDisasters
    {
        List<NaturalDisaster> disasters;
        int dayInterval; //how often should disasters happen
        double disasterChance; //how likely it is to happen

        public NaturalDisasters()
        {
            disasters = new List<NaturalDisaster>()
            {
                new NaturalDisaster("flood", 0.5, 0.6, 0),
                new NaturalDisaster("fire", 0.4, 0.7, 0.6),
                new NaturalDisaster("tornado", 0.4, 0.6, 0.8),
                new NaturalDisaster("earthquake", 0.7, 0.5, 0.3)
            };
            dayInterval = 4;
            disasterChance = 0.25;
        }

        public DisasterResult DisasterStruck(House house, int day)
        {
            DisasterResult result = new DisasterResult
            {
                HouseSurvived = true,
                DisasterOccurred = false,
                DisasterName = "",
                FoundationDamage = 0,
                WallsDamage = 0,
                RoofDamage = 0
            };

            Random rng = new Random();
            int rand = rng.Next(0, 4);
            NaturalDisaster disaster = disasters[rand];
            if (house.foundation == 0 && house.walls == 0 && house.roof == 0)
            {
                return result;
            }
            if (day % dayInterval != 0)
            {
                return result;
            }
            double chance = rng.NextDouble();
            if (chance > disasterChance)
            {
                return result;
            }
            result.DisasterOccurred = true;
            result.DisasterName = disaster.name;
            result.FoundationDamage = disaster.affectFoundation;
            result.WallsDamage = disaster.affectWalls;
            result.RoofDamage = disaster.affectRoof;
            
            if (house.foundation > 0)
            {
                if (house.foundationQuality < disaster.affectFoundation)
                {
                    double damage = (disaster.affectFoundation - house.foundationQuality) * 100;
                    house.foundationHP -= damage;
                    if(house.foundationHP <= 0)
                    {
                        result.HouseSurvived = false;
                        return result;
                    }
                }
            }
            if (house.walls > 0)
            {
                if (house.wallsQuality < disaster.affectWalls)
                {
                    double damage = (disaster.affectWalls - house.wallsQuality) * 100;
                    house.wallsHP -= damage;
                    if (house.wallsHP <= 0)
                    {
                        result.HouseSurvived = false;
                        return result;
                    }
                }
            }
            if (house.roof > 0)
            {
                if (house.roofQuality < disaster.affectRoof)
                {
                    double damage = (disaster.affectRoof - house.roofQuality) * 100;
                    house.roofHP -= damage;
                    if (house.roofHP <= 0)
                    {
                        result.HouseSurvived = false;
                        return result;
                    }
                }
            }
            
            return result;
        }
    }
}
