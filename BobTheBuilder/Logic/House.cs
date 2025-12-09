namespace BobTheBuilder
{
    public class House : Room
    {
        public Material? Foundation { get; private set; }
        public Material? Floors { get; private set; }
        public Material? Walls { get; private set; }
        public Material? Roof { get; private set; }

        public House(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {
        }

        public bool HasAnyBuiltParts()
        {
            return Foundation != null || Floors != null || Walls != null || Roof != null;
        }

        public void SetBuiltPart(string? partName, Material? material)
        {
            if (material == null || string.IsNullOrWhiteSpace(partName))
                return;

            string part = Normalize(partName);
            switch (part)
            {
                case "foundation":
                    Foundation = material;
                    break;
                case "floor":
                case "floors":
                    Floors = material;
                    break;
                case "wall":
                case "walls":
                    Walls = material;
                    break;
                case "roof":
                    Roof = material;
                    break;
            }
        }

        public Material? GetMaterialForPart(string partName)
        {
            string part = Normalize(partName);
            return part switch
            {
                "foundation" => Foundation,
                "floor" or "floors" => Floors,
                "wall" or "walls" => Walls,
                "roof" => Roof,
                _ => null
            };
        }

        public void DamagePart(string partName)
        {
            string part = Normalize(partName);
            switch (part)
            {
                case "foundation":
                    Foundation = null;
                    break;
                case "floor":
                case "floors":
                    Floors = null;
                    break;
                case "wall":
                case "walls":
                    Walls = null;
                    break;
                case "roof":
                    Roof = null;
                    break;
            }
        }

        private static string Normalize(string value)
        {
            return value.Trim().Replace(" ", "").Replace("_", "").Replace("-", "").ToLowerInvariant();
        }
    }
}
