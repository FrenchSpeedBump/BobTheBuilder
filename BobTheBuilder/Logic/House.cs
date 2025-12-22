namespace BobTheBuilder
{
    public class House : Room
    {
        public int foundation {  get; set; }
        public double foundationHP { get; set; }
        public double foundationQuality { get; set; }
        public int walls {  get; set; }
        public double wallsHP { get; set; }
        public double wallsQuality { get; set; }
        public int roof {  get; set; }
        public double roofHP { get; set; }
        public double roofQuality { get; set; }


        public List<Material> UsedMaterials { get; set; } = new List<Material>();
        public House(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {

        }

        public void RecordMaterials(Quest quest)
        {
            UsedMaterials.AddRange(GetMaterials(quest));
        }

        public List<Material> GetMaterials(Quest quest)
        {
            List<Material> materials = new List<Material>();
            foreach(Material material in quest.Requirements)
            {
                materials.Add(material);
            }
            return materials;
        }

        public void BuildPart(int phase, Quest quest)
        {
            string materialName = quest.Requirements[0].Name;
            
            double totalQuality = 0;
            foreach (Material material in quest.Requirements)
            {
                totalQuality += material.Quality;
            }
            double averageQuality = totalQuality / quest.Requirements.Count;
            
            if (phase == 1) // Foundation phase
            {
                foundationHP = 100;
                foundationQuality = averageQuality;
                if (materialName == "Wood")
                {
                    foundation = 1;
                }
                else if (materialName == "Concrete")
                {
                    foundation = 2;
                }
                else if (materialName == "Bricks")
                {
                    foundation = 3;
                }
                else
                {
                    foundation = 4;
                }
            }
            else if (phase == 9) // Walls phase
            {
                wallsHP = 100;
                wallsQuality = averageQuality;
                if (materialName == "Wood")
                {
                    walls = 1;
                }
                else if (materialName == "Concrete")
                {
                    walls = 2;
                }
                else if (materialName == "Bricks")
                {
                    walls = 3;
                }
                else if (materialName == "Shingle")
                {
                    walls = 4;
                }
                else
                {
                    walls = 5;
                }
            }
            else if (phase == 13) // Roof phase
            {
                roofHP = 100;
                roofQuality = averageQuality;
                if (materialName == "Wood")
                {
                    roof = 1;
                }
                else if (materialName == "Concrete")
                {
                    roof = 2;
                }
                else if (materialName == "Shingle")
                {
                    roof = 3;
                }
                else
                {
                    roof = 4;
                }
            }
        }
    }
}
