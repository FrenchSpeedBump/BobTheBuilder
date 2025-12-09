namespace BobTheBuilder
{
    public class House : Room
    {
        int foundation {  get; set; }
        int walls {  get; set; }
        int roof {  get; set; }
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
            List<Material> materials = new();
            foreach(Material material in quest.requirements)
            {
                materials.Add(material);
            }
            return materials;
        }
    }
}
