namespace BobTheBuilder
{
    public class House : Room
    {
        public Dictionary<string, Material> UsedMaterials { get; } = new Dictionary<string, Material>();
        public House(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {
        }
    }
}
