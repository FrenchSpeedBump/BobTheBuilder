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
            // Foundation (phases 1-4) - use 3 materials per quest
            List<Material> reqF1 = new List<Material> {
                new Material("Gravel", "Low-cost gravel for excavation.", 0.2, 0.3, 5),
                new Material("Concrete", "Economy concrete mix.", 0.25, 0.35, 8),
                new Material("Brick", "Low-cost brick.", 0.2, 0.25, 2)
            };
            List<Material> reqF2 = new List<Material> {
                new Material("Gravel", "Low-cost gravel for site leveling.", 0.2, 0.3, 5),
                new Material("Concrete", "Economy concrete for footings.", 0.25, 0.35, 10),
                new Material("Insulation", "Basic foundation insulation.", 0.2, 0.3, 6)
            };
            List<Material> reqF3 = new List<Material> {
                new Material("Concrete", "Economy concrete mix for slab.", 0.25, 0.4, 12),
                new Material("Gravel", "Backfill gravel.", 0.2, 0.3, 4),
                new Material("Insulation", "Basic curing insulation.", 0.2, 0.3, 5)
            };
            List<Material> reqF4 = new List<Material> {
                new Material("Concrete", "Concrete for curing and finishing.", 0.2, 0.25, 6),
                new Material("Insulation", "Foundation finishing insulation.", 0.2, 0.3, 4),
                new Material("Gravel", "Site tidy gravel.", 0.2, 0.25, 3)
            };

            // Flooring (phases 5-8)
            List<Material> reqFL5 = new List<Material> {
                new Material("Pine", "Cheap pine boards for joists.", 0.25, 0.35, 10),
                new Material("Wood", "Basic wood for support.", 0.2, 0.3, 8),
                new Material("Insulation", "Underfloor insulation basic.", 0.2, 0.3, 5)
            };
            List<Material> reqFL6 = new List<Material> {
                new Material("Pine", "Subfloor pine panels.", 0.25, 0.35, 12),
                new Material("Wood", "Subfloor battens.", 0.2, 0.3, 7),
                new Material("Concrete", "Small concrete pads for support.", 0.2, 0.25, 6)
            };
            List<Material> reqFL7 = new List<Material> {
                new Material("Pine", "Flooring pine planks.", 0.25, 0.35, 15),
                new Material("Wood", "Finish trims.", 0.2, 0.3, 5),
                new Material("Insulation", "Floor finishing underlay.", 0.2, 0.3, 4)
            };
            List<Material> reqFL8 = new List<Material> {
                new Material("Pine", "Finish boards.", 0.2, 0.3, 6),
                new Material("Wood", "Sealant timber.", 0.2, 0.3, 4),
                new Material("Insulation", "Final underlay.", 0.2, 0.3, 3)
            };

            // Walls (phases 9-12)
            List<Material> reqW9 = new List<Material> {
                new Material("Brick", "Low-grade bricks for walls.", 0.2, 0.35, 9),
                new Material("Bricks", "Stack of basic bricks.", 0.2, 0.35, 12),
                new Material("Concrete", "Basic mortar/concrete.", 0.2, 0.3, 6)
            };
            List<Material> reqW10 = new List<Material> {
                new Material("Brick", "Bricks for wall erection.", 0.2, 0.35, 10),
                new Material("Concrete", "Mortar mix.", 0.2, 0.3, 7),
                new Material("Insulation", "Basic wall insulation.", 0.2, 0.3, 5)
            };
            List<Material> reqW11 = new List<Material> {
                new Material("Bricks", "Bricks for finishing.", 0.2, 0.35, 9),
                new Material("Brick", "Detail bricks.", 0.2, 0.35, 6),
                new Material("Insulation", "Wall finishing insulation.", 0.2, 0.3, 4)
            };
            List<Material> reqW12 = new List<Material> {
                new Material("Bricks", "Exterior finish bricks.", 0.2, 0.35, 8),
                new Material("Wood", "Wall trims and frames.", 0.2, 0.3, 5),
                new Material("Insulation", "Final insulation touch.", 0.2, 0.3, 3)
            };

            // Roofs (phases 13-16)
            List<Material> reqR13 = new List<Material> {
                new Material("Shingle", "Cheap roofing shingles for cover.", 0.25, 0.35, 10),
                new Material("Wood", "Rafters and battens.", 0.2, 0.3, 9),
                new Material("Insulation", "Roof insulation basic.", 0.2, 0.3, 5)
            };
            List<Material> reqR14 = new List<Material> {
                new Material("Shingle", "Roof underlayment.", 0.25, 0.35, 6),
                new Material("Wood", "Under-structure for roof.", 0.2, 0.3, 7),
                new Material("Metal", "Basic fixings.", 0.25, 0.35, 8)
            };
            List<Material> reqR15 = new List<Material> {
                new Material("Shingle", "Top roofing layer.", 0.25, 0.35, 12),
                new Material("Glass", "Skylight glass basic.", 0.2, 0.25, 10),
                new Material("Wood", "Roof finishing timbers.", 0.2, 0.3, 6)
            };
            List<Material> reqR16 = new List<Material> {
                new Material("Shingle", "Final roof cover.", 0.25, 0.35, 9),
                new Material("Insulation", "Roof finishing insulation.", 0.2, 0.3, 4),
                new Material("Metal", "Fasteners and trims.", 0.25, 0.35, 6)
            };

            questsCons1.Add(new Quest("Dig Foundation", "Digging and clearing the foundation area.", reqF1, 1, 50));
            questsCons1.Add(new Quest("Level Site", "Level the site for foundation work.", reqF2, 2, 60));
            questsCons1.Add(new Quest("Pour Concrete", "Pour economy concrete for footings.", reqF3, 3, 80));
            questsCons1.Add(new Quest("Cure Foundation", "Cure and set the foundation.", reqF4, 4, 40));

            questsCons1.Add(new Quest("Install Joists", "Install simple joists for the floor.", reqFL5, 5, 70));
            questsCons1.Add(new Quest("Lay Subfloor", "Lay subfloor panels.", reqFL6, 6, 60));
            questsCons1.Add(new Quest("Lay Flooring", "Lay pine flooring boards.", reqFL7, 7, 90));
            questsCons1.Add(new Quest("Finish Floor", "Finish and seal the floor.", reqFL8, 8, 40));

            questsCons1.Add(new Quest("Frame Walls", "Create basic wall frames.", reqW9, 9, 80));
            questsCons1.Add(new Quest("Erect Walls", "Erect low-cost wall structure.", reqW10, 10, 100));
            questsCons1.Add(new Quest("Insulate Walls", "Install basic insulation.", reqW11, 11, 50));
            questsCons1.Add(new Quest("Finish Walls", "Apply finishing touches to walls.", reqW12, 12, 60));

            questsCons1.Add(new Quest("Install Trusses", "Install simple roof trusses.", reqR13, 13, 90));
            questsCons1.Add(new Quest("Lay Underlayment", "Lay roof underlayment.", reqR14, 14, 50));
            questsCons1.Add(new Quest("Install Cover", "Install shingles as roof covering.", reqR15, 15, 110));
            questsCons1.Add(new Quest("Inspect Roof", "Final roof inspection and minor fixes.", reqR16, 16, 40));

            return questsCons1;
        }

        public List<Quest> GetQuestsCons2()
        {
            // Company 2: average cost, average sustainability
            // Foundation (phases 1-4) - 3 materials each
            List<Material> reqF1 = new List<Material> {
                new Material("Gravel", "Balanced gravel for excavation.", 0.55, 0.6, 12),
                new Material("Concrete", "Standard concrete mix.", 0.6, 0.6, 18),
                new Material("Insulation", "Foundation insulation standard.", 0.5, 0.6, 10)
            };
            List<Material> reqF2 = new List<Material> {
                new Material("Concrete", "Concrete for footings.", 0.6, 0.65, 22),
                new Material("Gravel", "Compaction gravel.", 0.55, 0.6, 10),
                new Material("Metal", "Rebar/metal fixings.", 0.6, 0.7, 18)
            };
            List<Material> reqF3 = new List<Material> {
                new Material("Concrete", "Concrete with reinforcement.", 0.6, 0.65, 30),
                new Material("Metal", "Reinforcement and ties.", 0.6, 0.7, 20),
                new Material("Insulation", "Curing insulation.", 0.6, 0.6, 12)
            };
            List<Material> reqF4 = new List<Material> {
                new Material("Concrete", "Foundation seal and finish.", 0.6, 0.6, 16),
                new Material("Insulation", "Good quality foundation insulation.", 0.5, 0.6, 14),
                new Material("Gravel", "Final site dressing.", 0.55, 0.6, 8)
            };

            // Flooring (phases 5-8)
            List<Material> reqFL5 = new List<Material> {
                new Material("Pine", "Engineered joists.", 0.6, 0.6, 30),
                new Material("Wood", "Support timber.", 0.6, 0.6, 25),
                new Material("Insulation", "Underfloor insulation.", 0.55, 0.6, 18)
            };
            List<Material> reqFL6 = new List<Material> {
                new Material("Pine", "Good subfloor panels.", 0.6, 0.6, 35),
                new Material("Wood", "Subfloor battens.", 0.6, 0.6, 20),
                new Material("Concrete", "Pads for support.", 0.6, 0.6, 12)
            };
            List<Material> reqFL7 = new List<Material> {
                new Material("Pine", "Durable flooring boards.", 0.6, 0.6, 50),
                new Material("Wood", "Finish trims.", 0.6, 0.6, 15),
                new Material("Insulation", "Finish underlay.", 0.55, 0.6, 10)
            };
            List<Material> reqFL8 = new List<Material> {
                new Material("Pine", "Floor finishing materials.", 0.6, 0.6, 20),
                new Material("Wood", "Sealant timber.", 0.6, 0.6, 10),
                new Material("Insulation", "Underfloor finish.", 0.55, 0.6, 8)
            };

            // Walls (phases 9-12)
            List<Material> reqW9 = new List<Material> {
                new Material("Bricks", "Standard bricks.", 0.6, 0.6, 25),
                new Material("Brick", "Detail bricks.", 0.6, 0.6, 6),
                new Material("Concrete", "Mortar mix.", 0.6, 0.6, 12)
            };
            List<Material> reqW10 = new List<Material> {
                new Material("Bricks", "Bricks with mortar.", 0.6, 0.65, 40),
                new Material("Concrete", "Structural mortar.", 0.6, 0.65, 20),
                new Material("Insulation", "Wall insulation standard.", 0.6, 0.65, 18)
            };
            List<Material> reqW11 = new List<Material> {
                new Material("Bricks", "Bricks for insulation prep.", 0.6, 0.6, 22),
                new Material("Insulation", "Wall insulation standard.", 0.6, 0.65, 20),
                new Material("Wood", "Wall trims.", 0.6, 0.6, 10)
            };
            List<Material> reqW12 = new List<Material> {
                new Material("Bricks", "Exterior finish bricks.", 0.6, 0.6, 28),
                new Material("Wood", "Finish trims and frames.", 0.6, 0.6, 12),
                new Material("Insulation", "Final insulation.", 0.6, 0.6, 10)
            };

            // Roofs (phases 13-16)
            List<Material> reqR13 = new List<Material> {
                new Material("Metal", "Metal trusses and framing.", 0.6, 0.7, 60),
                new Material("Wood", "Quality rafters.", 0.6, 0.65, 30),
                new Material("Insulation", "Roof insulation standard.", 0.6, 0.65, 20)
            };
            List<Material> reqR14 = new List<Material> {
                new Material("Wood", "Roof sheathing materials.", 0.6, 0.6, 25),
                new Material("Shingle", "Good quality underlayment.", 0.6, 0.65, 25),
                new Material("Metal", "Fasteners and trims.", 0.6, 0.7, 18)
            };
            List<Material> reqR15 = new List<Material> {
                new Material("Shingle", "Durable roofing cover.", 0.6, 0.7, 80),
                new Material("Glass", "Skylight glass.", 0.6, 0.6, 40),
                new Material("Wood", "Finish timbers.", 0.6, 0.6, 20)
            };
            List<Material> reqR16 = new List<Material> {
                new Material("Shingle", "Roof finishing layer.", 0.6, 0.65, 35),
                new Material("Insulation", "Roof finishing insulation.", 0.6, 0.65, 18),
                new Material("Metal", "Inspection fixings.", 0.6, 0.7, 15)
            };

            questsCons2.Add(new Quest("Dig Foundation", "Excavate the foundation with standard equipment.", reqF1, 1, 120));
            questsCons2.Add(new Quest("Install Footings", "Install standard footings.", reqF2, 2, 160));
            questsCons2.Add(new Quest("Pour Reinforced Concrete", "Pour concrete with reinforcement.", reqF3, 3, 220));
            questsCons2.Add(new Quest("Seal Foundation", "Seal and insulate the foundation.", reqF4, 4, 90));

            questsCons2.Add(new Quest("Install Joists", "Install engineered joists.", reqFL5, 5, 180));
            questsCons2.Add(new Quest("Lay Subfloor", "Lay quality subfloor panels.", reqFL6, 6, 160));
            questsCons2.Add(new Quest("Lay Flooring", "Lay engineered flooring.", reqFL7, 7, 260));
            questsCons2.Add(new Quest("Finish Floor", "Finish and seal floor with standard materials.", reqFL8, 8, 110));

            questsCons2.Add(new Quest("Frame Walls", "Frame walls with standard methods.", reqW9, 9, 200));
            questsCons2.Add(new Quest("Erect Walls", "Erect brick walls.", reqW10, 10, 300));
            questsCons2.Add(new Quest("Insulate Walls", "Install mid-grade insulation.", reqW11, 11, 150));
            questsCons2.Add(new Quest("Finish Walls", "Apply external finishes.", reqW12, 12, 180));

            questsCons2.Add(new Quest("Install Trusses", "Place metal trusses for the roof.", reqR13, 13, 250));
            questsCons2.Add(new Quest("Lay Sheathing", "Lay quality roof sheathing.", reqR14, 14, 130));
            questsCons2.Add(new Quest("Install Covering", "Install mid-range roof covering.", reqR15, 15, 320));
            questsCons2.Add(new Quest("Inspect Roof", "Inspection and roof finishing.", reqR16, 16, 140));

            return questsCons2;
        }

        public List<Quest> GetQuestsCons3()
        {
            // Company 3: expensive but sustainable (uses same simple material names)
            // Foundation (phases 1-4)
            List<Material> reqF1 = new List<Material> {
                new Material("Recycled", "Recycled aggregate for excavation.", 0.9, 0.9, 30),
                new Material("Gravel", "Precision recycled gravel.", 0.9, 0.9, 18),
                new Material("Concrete", "High-grade sustainable concrete.", 0.9, 0.95, 45)
            };
            List<Material> reqF2 = new List<Material> {
                new Material("Recycled", "Precision site preparation with recycled fill.", 0.9, 0.9, 35),
                new Material("Concrete", "Premium concrete for footings.", 0.9, 0.95, 55),
                new Material("Insulation", "High-performance foundation insulation.", 0.9, 0.95, 30)
            };
            List<Material> reqF3 = new List<Material> {
                new Material("Concrete", "High-grade sustainable concrete for slab.", 0.9, 0.95, 85),
                new Material("Recycled", "Recycled aggregate for reinforcement.", 0.9, 0.9, 40),
                new Material("Metal", "High-quality reinforcement.", 0.9, 0.95, 60)
            };
            List<Material> reqF4 = new List<Material> {
                new Material("Insulation", "High-performance foundation insulation.", 0.9, 0.95, 40),
                new Material("Concrete", "Premium foundation finish.", 0.9, 0.95, 30),
                new Material("Gravel", "Final recycled dressing.", 0.9, 0.9, 15)
            };

            // Flooring (phases 5-8)
            List<Material> reqFL5 = new List<Material> {
                new Material("Bamboo", "Sustainably sourced bamboo joists.", 0.95, 0.9, 80),
                new Material("Wood", "High-quality support timber.", 0.9, 0.9, 50),
                new Material("Insulation", "Low-VOC underlay and insulation.", 0.95, 0.9, 25)
            };
            List<Material> reqFL6 = new List<Material> {
                new Material("Bamboo", "High-quality bamboo subfloor.", 0.95, 0.92, 90),
                new Material("Wood", "Eco-friendly support.", 0.9, 0.9, 45),
                new Material("Concrete", "Precision pads.", 0.9, 0.9, 20)
            };
            List<Material> reqFL7 = new List<Material> {
                new Material("Bamboo", "Premium bamboo flooring.", 0.95, 0.95, 150),
                new Material("Wood", "Finish trims.", 0.9, 0.9, 30),
                new Material("Insulation", "Low-VOC finishing underlay.", 0.95, 0.9, 20)
            };
            List<Material> reqFL8 = new List<Material> {
                new Material("Insulation", "Low-VOC underlay and insulation.", 0.95, 0.9, 25),
                new Material("Bamboo", "Finish boards.", 0.95, 0.95, 60),
                new Material("Wood", "Premium sealants.", 0.9, 0.9, 15)
            };

            // Walls (phases 9-12)
            List<Material> reqW9 = new List<Material> {
                new Material("EcoBlock", "High-efficiency eco blocks.", 0.95, 0.95, 120),
                new Material("Brick", "High-quality brick accents.", 0.9, 0.9, 18),
                new Material("Insulation", "High-performance eco insulation.", 0.95, 0.95, 40)
            };
            List<Material> reqW10 = new List<Material> {
                new Material("EcoBlock", "Eco blocks for structural walls.", 0.95, 0.95, 160),
                new Material("Concrete", "Eco mortar.", 0.9, 0.95, 40),
                new Material("Recycled", "Recycled fillers.", 0.9, 0.9, 30)
            };
            List<Material> reqW11 = new List<Material> {
                new Material("Insulation", "High-performance eco insulation.", 0.95, 0.95, 50),
                new Material("EcoBlock", "Structural eco-blocks.", 0.95, 0.95, 110),
                new Material("Wood", "Eco-friendly finishes.", 0.9, 0.9, 25)
            };
            List<Material> reqW12 = new List<Material> {
                new Material("EcoBlock", "Sustainable exterior cladding blocks.", 0.95, 0.9, 130),
                new Material("Brick", "Eco brick finishes.", 0.9, 0.9, 30),
                new Material("Insulation", "Final eco insulation.", 0.95, 0.95, 35)
            };

            // Roofs (phases 13-16)
            List<Material> reqR13 = new List<Material> {
                new Material("Metal", "Sustainable metal trusses.", 0.9, 0.95, 140),
                new Material("Wood", "Premium rafters.", 0.9, 0.95, 80),
                new Material("Recycled", "Recycled connectors and fixings.", 0.9, 0.9, 30)
            };
            List<Material> reqR14 = new List<Material> {
                new Material("Solar", "Solar-ready sheathing.", 0.95, 0.95, 200),
                new Material("Metal", "Premium sheathing supports.", 0.9, 0.95, 100),
                new Material("Insulation", "Top-tier roof insulation.", 0.95, 0.95, 60)
            };
            List<Material> reqR15 = new List<Material> {
                new Material("Solar", "Solar tiles for roof covering.", 0.95, 0.95, 450),
                new Material("Glass", "High-efficiency skylight glass.", 0.95, 0.95, 120),
                new Material("Wood", "Premium finishing timbers.", 0.9, 0.95, 60)
            };
            List<Material> reqR16 = new List<Material> {
                new Material("Insulation", "Top-tier roof insulation.", 0.95, 0.95, 120),
                new Material("Metal", "Final inspection fixings.", 0.9, 0.95, 30),
                new Material("Recycled", "Final recycled dressing.", 0.9, 0.9, 25)
            };

            questsCons3.Add(new Quest("Site Protection", "Prepare and protect site with sustainable methods.", reqF1, 1, 300));
            questsCons3.Add(new Quest("Precision Excavation", "Excavate precisely with recycled fill.", reqF2, 2, 350));
            questsCons3.Add(new Quest("Pour Premium Concrete", "Pour high-grade sustainable concrete.", reqF3, 3, 600));
            questsCons3.Add(new Quest("Install Foundation Insulation", "Install premium insulation.", reqF4, 4, 250));

            questsCons3.Add(new Quest("Install Eco Joists", "Install bamboo/eco joists.", reqFL5, 5, 420));
            questsCons3.Add(new Quest("Lay Eco Subfloor", "Lay bamboo composite subfloor.", reqFL6, 6, 460));
            questsCons3.Add(new Quest("Lay Premium Flooring", "Lay premium bamboo flooring.", reqFL7, 7, 800));
            questsCons3.Add(new Quest("Finish Floor Eco", "Finish floor using low-VOC products.", reqFL8, 8, 220));

            questsCons3.Add(new Quest("Construct Eco Walls", "Build walls using eco blocks.", reqW9, 9, 900));
            questsCons3.Add(new Quest("Erect Eco Structure", "Erect structural eco-block walls.", reqW10, 10, 1200));
            questsCons3.Add(new Quest("High-eff Insulation", "Install high-efficiency insulation.", reqW11, 11, 500));
            questsCons3.Add(new Quest("Sustainable Cladding", "Apply sustainable cladding.", reqW12, 12, 700));

            questsCons3.Add(new Quest("Place Sustainable Trusses", "Install metal trusses with eco methods.", reqR13, 13, 900));
            questsCons3.Add(new Quest("Install Solar Sheathing", "Prepare roof for solar tiles.", reqR14, 14, 1200));
            questsCons3.Add(new Quest("Install Solar Tiles", "Install solar tiles as roof covering.", reqR15, 15, 2500));
            questsCons3.Add(new Quest("Roof System Test", "Final roof testing and inspection.", reqR16, 16, 500));

            return questsCons3;
        }
    }
}
