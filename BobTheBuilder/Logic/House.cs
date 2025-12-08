namespace BobTheBuilder
{
    public class House : Room
    {
        int foundation {  get; set; }
        int walls {  get; set; }
        int roof {  get; set; }
        public Dictionary<string, Material> UsedMaterials { get; } = new Dictionary<string, Material>();
        public House(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {
        }
    }
}
