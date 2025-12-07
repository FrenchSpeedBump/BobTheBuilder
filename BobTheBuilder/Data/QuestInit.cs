namespace BobTheBuilder
{
    public class QuestInit
    {

        List<Quest> questsCons1 = new();

        List<Quest> questsCons2 = new();

        List<Quest> questsCons3 = new();
    

        public List<Quest> GetQuestsCons1()
        {
            // Company 1: cheaper, least sustainable
            var reqFoundation = new List<Material> { new Material("Cheap Gravel", "Low-cost gravel for backfill and base.", 0.2, 0.4, 20) };
            var reqFloor = new List<Material> { new Material("Pine Wood Planks", "Economical wood planks for flooring.", 0.3, 0.4, 30) };
            var reqWalls = new List<Material> { new Material("Low-grade Bricks", "Inexpensive bricks with lower sustainability.", 0.25, 0.4, 40) };
            var reqRoofs = new List<Material> { new Material("Asphalt Shingles", "Cheap roofing shingles.", 0.3, 0.4, 35) };

            questsCons1.Add(new Quest("Dig Foundation", "You will have to pay the construction office to dig.", reqFoundation, 1, 100));
            questsCons1.Add(new Quest("Floor", "Install the building floor.", reqFloor, 2, 150));
            questsCons1.Add(new Quest("Walls", "Build the walls.", reqWalls, 3, 200));
            questsCons1.Add(new Quest("Roofs", "Install the roof.", reqRoofs, 4, 180));
            
            return questsCons1;
        }

        public List<Quest> GetQuestsCons2()
        {
            // Company 2: average cost, average sustainability
            var reqFoundation = new List<Material> { new Material("Standard Gravel", "Balanced cost/quality gravel.", 0.6, 0.6, 50) };
            var reqFloor = new List<Material> { new Material("Engineered Wood", "Durable engineered flooring.", 0.6, 0.6, 80) };
            var reqWalls = new List<Material> { new Material("Standard Bricks", "Common bricks with moderate sustainability.", 0.6, 0.6, 100) };
            var reqRoofs = new List<Material> { new Material("Metal Roofing", "Long-lasting metal roofing.", 0.6, 0.7, 120) };

            questsCons2.Add(new Quest("Dig Foundation", "You will have to pay the construction office to dig.", reqFoundation, 1, 150));
            questsCons2.Add(new Quest("Floor", "Install the building floor.", reqFloor, 2, 250));
            questsCons2.Add(new Quest("Walls", "Build the walls.", reqWalls, 3, 350));
            questsCons2.Add(new Quest("Roofs", "Install the roof.", reqRoofs, 4, 300));

            return questsCons2;
        }

        public List<Quest> GetQuestsCons3()
        {
            // Company 3: expensive but sustainable
            var reqFoundation = new List<Material> { new Material("Recycled Aggregate", "High-sustainability recycled aggregate for foundations.", 0.9, 0.9, 120) };
            var reqFloor = new List<Material> { new Material("Bamboo Composite", "Sustainable, high-quality flooring material.", 0.95, 0.9, 200) };
            var reqWalls = new List<Material> { new Material("Insulated Eco Blocks", "High-efficiency sustainable wall blocks.", 0.9, 0.95, 300) };
            var reqRoofs = new List<Material> { new Material("Solar Tiles", "Sustainable and energy-generating roofing.", 0.95, 0.9, 500) };

            questsCons3.Add(new Quest("Dig Foundation", "You will have to pay the construction office to dig.", reqFoundation, 1, 250));
            questsCons3.Add(new Quest("Floor", "Install the building floor.", reqFloor, 2, 400));
            questsCons3.Add(new Quest("Walls", "Build the walls.", reqWalls, 3, 600));
            questsCons3.Add(new Quest("Roofs", "Install the roof.", reqRoofs, 4, 500));

            return questsCons3;
        }
    }
}
