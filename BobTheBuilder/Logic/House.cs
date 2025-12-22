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
    }
}
