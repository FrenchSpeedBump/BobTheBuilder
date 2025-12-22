namespace BobTheBuilder.Logic
{
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
            dayInterval = 1;
            disasterChance = 1;
        }

        public bool DisasterStruck(House house, int day)
        {
            Random rng = new Random();
            int rand = rng.Next(0, 4);
            NaturalDisaster disaster = disasters[rand];
            if (house.foundation == 0 && house.walls == 0 && house.roof == 0)
            {
                return true;
            }
            if (day % dayInterval != 0)
            {
                return true;
            }
            double chance = rng.NextDouble();
            if (chance > disasterChance)
            {
                return true;
            }
            Console.WriteLine("Oh No! A disaster struck!\n========\n{0}\n========", disaster.name);
            
            if (house.foundation > 0)
            {
                if (disaster.affectFoundation > house.foundationHP)
                {
                    house.foundationHP -= disaster.affectFoundation - house.foundationHP;
                    Console.WriteLine("Foundation damage! Foundation durability reduced to {0:F2}", house.foundationHP);
                    if(house.foundationHP < 0)
                    {
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Foundation resisted the disaster! (HP: {0:F2})", house.foundationHP);
                }
            }
            if (house.walls > 0)
            {
                if (disaster.affectWalls > house.wallsHP)
                {
                    house.wallsHP -= disaster.affectWalls - house.wallsHP;
                    Console.WriteLine("Wall damage! Wall durability reduced to {0:F2}", house.wallsHP);
                    if (house.wallsHP < 0)
                    {
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Walls resisted the disaster! (HP: {0:F2})", house.wallsHP);
                }
            }
            if (house.roof > 0)
            {
                if (disaster.affectRoof > house.roofHP)
                {
                    house.roofHP -= disaster.affectRoof - house.roofHP;
                    Console.WriteLine("Roof damage! Roof durability reduced to {0:F2}", house.roofHP);
                    if (house.roofHP < 0)
                    {
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Roof resisted the disaster! (HP: {0:F2})", house.roofHP);
                }
            }
            
            Console.WriteLine("Consider buying an item to repair your house.");
            return true;
        }
    }
}
