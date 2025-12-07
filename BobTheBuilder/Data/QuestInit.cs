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
            List<Material> reqFoundation = new List<Material> { new Material("Cheap Gravel", "Low-cost gravel for backfill and base.", 0.2, 0.4, 20) };
            List<Material> reqFloor = new List<Material> { new Material("Pine Wood Planks", "Economical wood planks for flooring.", 0.3, 0.4, 30) };
            List<Material> reqWalls = new List<Material> { new Material("Low-grade Bricks", "Inexpensive bricks with lower sustainability.", 0.25, 0.4, 40) };
            List<Material> reqRoofs = new List<Material> { new Material("Asphalt Shingles", "Cheap roofing shingles.", 0.3, 0.4, 35) };

            List<string> miniFoundation = new List<string> { "Digging", "Building foundation", "Setting up foundation" };
            List<string> miniFloor = new List<string> { "Prepare subfloor", "Lay floor structure", "Finish floor" };
            List<string> miniWalls = new List<string> { "Frame walls", "Lay bricks/blocks", "Finish walls" };
            List<string> miniRoofs = new List<string> { "Install trusses", "Lay roofing material", "Roof finish & inspection" };

            questsCons1.Add(new Quest("Dig Foundation", "You will have to pay the construction office to dig.", reqFoundation, 1, 100, miniFoundation));
            questsCons1.Add(new Quest("Floor", "Install the building floor.", reqFloor, 2, 150, miniFloor));
            questsCons1.Add(new Quest("Walls", "Build the walls.", reqWalls, 3, 200, miniWalls));
            questsCons1.Add(new Quest("Roofs", "Install the roof.", reqRoofs, 4, 180, miniRoofs));
            
            return questsCons1;
        }

        public List<Quest> GetQuestsCons2()
        {
            // Company 2: average cost, average sustainability
            List<Material> reqFoundation = new List<Material> { new Material("Standard Gravel", "Balanced cost/quality gravel.", 0.6, 0.6, 50) };
            List<Material> reqFloor = new List<Material> { new Material("Engineered Wood", "Durable engineered flooring.", 0.6, 0.6, 80) };
            List<Material> reqWalls = new List<Material> { new Material("Standard Bricks", "Common bricks with moderate sustainability.", 0.6, 0.6, 100) };
            List<Material> reqRoofs = new List<Material> { new Material("Metal Roofing", "Long-lasting metal roofing.", 0.6, 0.7, 120) };

            List<string> miniFoundation = new List<string> { "Survey site", "Excavate", "Prepare foundation bed" };
            List<string> miniFloor = new List<string> { "Install joists", "Lay flooring panels", "Seal & finish floor" };
            List<string> miniWalls = new List<string> { "Erect frames", "Install insulation", "Apply exterior finish" };
            List<string> miniRoofs = new List<string> { "Place trusses", "Install sheathing", "Add roofing finish" };

            questsCons2.Add(new Quest("Dig Foundation", "You will have to pay the construction office to dig.", reqFoundation, 1, 150, miniFoundation));
            questsCons2.Add(new Quest("Floor", "Install the building floor.", reqFloor, 2, 250, miniFloor));
            questsCons2.Add(new Quest("Walls", "Build the walls.", reqWalls, 3, 350, miniWalls));
            questsCons2.Add(new Quest("Roofs", "Install the roof.", reqRoofs, 4, 300, miniRoofs));

            return questsCons2;
        }

        public List<Quest> GetQuestsCons3()
        {
            // Company 3: expensive but sustainable
            List<Material> reqFoundation = new List<Material> { new Material("Recycled Aggregate", "High-sustainability recycled aggregate for foundations.", 0.9, 0.9, 120) };
            List<Material> reqFloor = new List<Material> { new Material("Bamboo Composite", "Sustainable, high-quality flooring material.", 0.95, 0.9, 200) };
            List<Material> reqWalls = new List<Material> { new Material("Insulated Eco Blocks", "High-efficiency sustainable wall blocks.", 0.9, 0.95, 300) };
            List<Material> reqRoofs = new List<Material> { new Material("Solar Tiles", "Sustainable and energy-generating roofing.", 0.95, 0.9, 500) };

            List<string> miniFoundation = new List<string> { "Site protection & prep", "Precision excavation", "Install recycled footings" };
            List<string> miniFloor = new List<string> { "Install eco subfloor", "Lay bamboo composite", "Finish with low-VOC sealant" };
            List<string> miniWalls = new List<string> { "Construct eco-block walls", "Add high-efficiency insulation", "Apply sustainable cladding" };
            List<string> miniRoofs = new List<string> { "Place sustainable trusses", "Install solar tiles", "System test & inspection" };

            questsCons3.Add(new Quest("Dig Foundation", "You will have to pay the construction office to dig.", reqFoundation, 1, 250, miniFoundation));
            questsCons3.Add(new Quest("Floor", "Install the building floor.", reqFloor, 2, 400, miniFloor));
            questsCons3.Add(new Quest("Walls", "Build the walls.", reqWalls, 3, 600, miniWalls));
            questsCons3.Add(new Quest("Roofs", "Install the roof.", reqRoofs, 4, 500, miniRoofs));

            return questsCons3;
        }
    }
}
