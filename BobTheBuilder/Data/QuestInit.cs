namespace BobTheBuilder
{
    public class QuestInit
    {

        List<Quest> questsCons1 = new();

        List<Quest> questsCons2 = new();

        List<Quest> questsCons3 = new();
    

        public List<Quest> GetQuestsCons1()
        {
            // Foundation (phases 1-4)
            List<Material> reqF1 = new List<Material> {
                new Material("Wood", "Subfloor support timber.", 0.9, 0.55, 10),
                new Material("Gravel", "General gravel for site work.", 0.65, 0.65, 8),
                new Material("Wood", "Subfloor support timber.", 0.9, 0.55, 10),
            };
            List<Material> reqF2 = new List<Material> {
                new Material("Gravel", "Compaction gravel.", 0.65, 0.65, 8),
                new Material("Gravel", "Extra fill gravel.", 0.65, 0.65, 8),
                new Material("Shingle", "Cheap footing cover.", 0.6, 0.6, 12),
                new Material("Insulation", "Basic insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqF3 = new List<Material> {
                new Material("Gravel", "Slab base gravel.", 0.65, 0.65, 8),
                new Material("Shingle", "Low-cost protection layer.", 0.6, 0.6, 12),
                new Material("Shingle", "Secondary barrier.", 0.6, 0.6, 12),
                new Material("Insulation", "Basic slab insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqF4 = new List<Material> {
                new Material("Gravel", "Site dressing gravel.", 0.65, 0.65, 8),
                new Material("Shingle", "Cheap moisture barrier.", 0.6, 0.6, 12),
                new Material("Insulation", "Basic foundation insulation.", 0.55, 0.75, 20)
            };

            // Flooring (phases 5-8)
            List<Material> reqFL5 = new List<Material> {
                new Material("Gravel", "Joist base gravel.", 0.65, 0.65, 8),
                new Material("Shingle", "Basic moisture barrier.", 0.6, 0.6, 12),
                new Material("Shingle", "Secondary barrier.", 0.6, 0.6, 12)
            };
            List<Material> reqFL6 = new List<Material> {
                new Material("Gravel", "Leveling gravel.", 0.65, 0.65, 8),
                new Material("Shingle", "Subfloor barrier.", 0.6, 0.6, 12),
                new Material("Insulation", "Cheap underfloor insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqFL7 = new List<Material> {
                new Material("Shingle", "Budget flooring overlay.", 0.6, 0.6, 12),
                new Material("Shingle", "Secondary layer.", 0.6, 0.6, 12),
                new Material("Gravel", "Base fill.", 0.65, 0.65, 8)
            };
            List<Material> reqFL8 = new List<Material> {
                new Material("Shingle", "Cheap sealant layer.", 0.6, 0.6, 12),
                new Material("Gravel", "Final leveling.", 0.65, 0.65, 8),
                new Material("Insulation", "Basic underlay.", 0.55, 0.75, 20)
            };

            // Walls (phases 9-12)
            List<Material> reqW9 = new List<Material> {
                new Material("Wood", "Subfloor support timber.", 0.9, 0.55, 10),
                new Material("Shingle", "Basic wall barrier.", 0.6, 0.6, 12),
                new Material("Gravel", "Wall base fill.", 0.65, 0.65, 8),
                new Material("Insulation", "Basic wall insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqW10 = new List<Material> {
                new Material("Gravel", "Wall base fill.", 0.65, 0.65, 8),
                new Material("Shingle", "Cheap wall barrier.", 0.6, 0.6, 12),
                new Material("Shingle", "Secondary barrier.", 0.6, 0.6, 12),
                new Material("Insulation", "Basic wall insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqW11 = new List<Material> {
                new Material("Shingle", "Interior barrier.", 0.6, 0.6, 12),
                new Material("Gravel", "Wall filler.", 0.65, 0.65, 8),
                new Material("Insulation", "Basic insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqW12 = new List<Material> {
                new Material("Shingle", "Exterior cladding.", 0.6, 0.6, 12),
                new Material("Gravel", "Foundation dressing.", 0.65, 0.65, 8),
                new Material("Insulation", "Final insulation.", 0.55, 0.75, 20)
            };

            // Roofs (phases 13-16)
            List<Material> reqR13 = new List<Material> {
                new Material("Wood", "Subfloor support timber.", 0.9, 0.55, 10),
                new Material("Shingle", "Roofing under/cover (cheap).", 0.6, 0.6, 12),
                new Material("Insulation", "Roof insulation basic.", 0.55, 0.75, 20)
            };
            List<Material> reqR14 = new List<Material> {
                new Material("Shingle", "Cheap underlayment.", 0.6, 0.6, 12),
                new Material("Shingle", "Secondary layer.", 0.6, 0.6, 12),
                new Material("Gravel", "Ballast gravel.", 0.65, 0.65, 8)
            };
            List<Material> reqR15 = new List<Material> {
                new Material("Shingle", "Budget roofing.", 0.6, 0.6, 12),
                new Material("Shingle", "Secondary cover.", 0.6, 0.6, 12),
                new Material("Gravel", "Roof ballast.", 0.65, 0.65, 8),
                new Material("Insulation", "Basic attic insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqR16 = new List<Material> {
                new Material("Shingle", "Final cover.", 0.6, 0.6, 12),
                new Material("Gravel", "Edge gravel.", 0.65, 0.65, 8),
                new Material("Insulation", "Basic roof insulation.", 0.55, 0.75, 20)
            };

            questsCons1.Add(new Quest("Dig Foundation", "Digging and clearing the foundation area.", reqF1, 1, 50));
            questsCons1.Add(new Quest("Level Site", "Level the site for foundation work.", reqF2, 2, 60));
            questsCons1.Add(new Quest("Pour Concrete", "Pour economy concrete for footings.", reqF3, 3, 70));
            questsCons1.Add(new Quest("Cure Foundation", "Cure and set the foundation.", reqF4, 4, 55));

            questsCons1.Add(new Quest("Install Joists", "Install simple joists for the floor.", reqFL5, 5, 65));
            questsCons1.Add(new Quest("Lay Subfloor", "Lay subfloor panels.", reqFL6, 6, 60));
            questsCons1.Add(new Quest("Lay Flooring", "Lay pine flooring boards.", reqFL7, 7, 75));
            questsCons1.Add(new Quest("Finish Floor", "Finish and seal the floor.", reqFL8, 8, 55));

            questsCons1.Add(new Quest("Frame Walls", "Create basic wall frames.", reqW9, 9, 70));
            questsCons1.Add(new Quest("Erect Walls", "Erect low-cost wall structure.", reqW10, 10, 80));
            questsCons1.Add(new Quest("Insulate Walls", "Install basic insulation.", reqW11, 11, 60));
            questsCons1.Add(new Quest("Finish Walls", "Apply finishing touches to walls.", reqW12, 12, 65));

            questsCons1.Add(new Quest("Install Trusses", "Install simple roof trusses.", reqR13, 13, 75));
            questsCons1.Add(new Quest("Lay Underlayment", "Lay roof underlayment.", reqR14, 14, 55));
            questsCons1.Add(new Quest("Install Cover", "Install shingles as roof covering.", reqR15, 15, 70));
            questsCons1.Add(new Quest("Inspect Roof", "Final roof inspection and minor fixes.", reqR16, 16, 50));

            return questsCons1;
        }

        public List<Quest> GetQuestsCons2()
        {
            // Foundation (phases 1-4)
            List<Material> reqF1 = new List<Material> {
                new Material("Concrete", "Standard concrete mix.", 0.45, 0.9, 18),
                new Material("Gravel", "Balanced gravel for excavation.", 0.65, 0.65, 8),
                new Material("Insulation", "Foundation insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqF2 = new List<Material> {
                new Material("Bricks", "Footing bricks.", 0.7, 0.75, 15),
                new Material("Pine", "Form timber.", 0.8, 0.55, 15),
                new Material("Insulation", "Foundation insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqF3 = new List<Material> {
                new Material("Bricks", "Foundation bricks.", 0.7, 0.75, 15),
                new Material("Bricks", "Double layer bricks.", 0.7, 0.75, 15),
                new Material("Pine", "Support framing.", 0.8, 0.55, 15)
            };
            List<Material> reqF4 = new List<Material> {
                new Material("Pine", "Foundation finish timber.", 0.8, 0.55, 15),
                new Material("Bricks", "Finish bricks.", 0.7, 0.75, 15),
                new Material("Insulation", "Foundation insulation.", 0.55, 0.75, 20)
            };

            // Flooring (phases 5-8)
            List<Material> reqFL5 = new List<Material> {
                new Material("Pine", "Engineered joists.", 0.8, 0.55, 15),
                new Material("Bricks", "Joist support piers.", 0.7, 0.75, 15),
                new Material("Insulation", "Underfloor insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqFL6 = new List<Material> {
                new Material("Pine", "Subfloor panels.", 0.8, 0.55, 15),
                new Material("Pine", "Support battens.", 0.8, 0.55, 15),
                new Material("Bricks", "Foundation pads.", 0.7, 0.75, 15)
            };
            List<Material> reqFL7 = new List<Material> {
                new Material("Pine", "Flooring boards.", 0.8, 0.55, 15),
                new Material("Bricks", "Edge supports.", 0.7, 0.75, 15),
                new Material("Insulation", "Floor underlay.", 0.55, 0.75, 20)
            };
            List<Material> reqFL8 = new List<Material> {
                new Material("Pine", "Finish boards.", 0.8, 0.55, 15),
                new Material("Bricks", "Trim supports.", 0.7, 0.75, 15),
                new Material("Insulation", "Final underlay.", 0.55, 0.75, 20)
            };

            // Walls (phases 9-12)
            List<Material> reqW9 = new List<Material> {
                new Material("Concrete", "Mortar mix.", 0.45, 0.9, 18),
                new Material("Bricks", "Standard bricks.", 0.7, 0.75, 15),
                new Material("Insulation", "Wall insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqW10 = new List<Material> {
                new Material("Bricks", "Structural wall bricks.", 0.7, 0.75, 15),
                new Material("Bricks", "Double layer bricks.", 0.7, 0.75, 15),
                new Material("Insulation", "Wall insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqW11 = new List<Material> {
                new Material("Bricks", "Inner wall bricks.", 0.7, 0.75, 15),
                new Material("Pine", "Wall framing.", 0.8, 0.55, 15),
                new Material("Insulation", "Wall insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqW12 = new List<Material> {
                new Material("Bricks", "Exterior bricks.", 0.7, 0.75, 15),
                new Material("Pine", "Exterior trim.", 0.8, 0.55, 15),
                new Material("Insulation", "Final insulation.", 0.55, 0.75, 20)
            };

            // Roofs (phases 13-16)
            List<Material> reqR13 = new List<Material> {
                new Material("Concrete", "Mortar mix.", 0.45, 0.9, 18),
                new Material("Metal", "Metal trusses and framing.", 0.5, 0.85, 60),
                new Material("Insulation", "Roof insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqR14 = new List<Material> {
                new Material("Pine", "Roof sheathing.", 0.8, 0.55, 15),
                new Material("Bricks", "Chimney bricks.", 0.7, 0.75, 15),
                new Material("Insulation", "Roof insulation.", 0.55, 0.75, 20)
            };
            List<Material> reqR15 = new List<Material> {
                new Material("Pine", "Roof decking.", 0.8, 0.55, 15),
                new Material("Bricks", "Vent supports.", 0.7, 0.75, 15),
                new Material("Metal", "Roof fixings.", 0.5, 0.85, 60)
            };
            List<Material> reqR16 = new List<Material> {
                new Material("Pine", "Finish timbers.", 0.8, 0.55, 15),
                new Material("Bricks", "Final trim.", 0.7, 0.75, 15),
                new Material("Insulation", "Roof insulation.", 0.55, 0.75, 20)
            };

            questsCons2.Add(new Quest("Excavate", "Excavate the foundation with standard equipment.", reqF1, 1, 100));
            questsCons2.Add(new Quest("Install Footings", "Install standard footings.", reqF2, 2, 110));
            questsCons2.Add(new Quest("Pour Reinforced Slab", "Pour reinforced concrete slab.", reqF3, 3, 120));
            questsCons2.Add(new Quest("Seal Foundation", "Seal and protect foundation.", reqF4, 4, 105));

            questsCons2.Add(new Quest("Install Engineered Joists", "Install engineered joists.", reqFL5, 5, 115));
            questsCons2.Add(new Quest("Lay Subfloor", "Lay quality subfloor.", reqFL6, 6, 110));
            questsCons2.Add(new Quest("Lay Flooring", "Lay durable flooring boards.", reqFL7, 7, 120));
            questsCons2.Add(new Quest("Finish Floor", "Finish and seal floor.", reqFL8, 8, 105));

            questsCons2.Add(new Quest("Frame Walls", "Frame walls with standard methods.", reqW9, 9, 110));
            questsCons2.Add(new Quest("Build Walls", "Build structural brick walls.", reqW10, 10, 120));
            questsCons2.Add(new Quest("Insulate Walls", "Add wall insulation and prep.", reqW11, 11, 105));
            questsCons2.Add(new Quest("Finish Walls", "Apply exterior finishes.", reqW12, 12, 115));

            questsCons2.Add(new Quest("Install Trusses", "Place metal trusses for the roof.", reqR13, 13, 115));
            questsCons2.Add(new Quest("Lay Sheathing", "Lay quality roof sheathing.", reqR14, 14, 105));
            questsCons2.Add(new Quest("Install Skylight", "Install standard skylight/glass.", reqR15, 15, 120));
            questsCons2.Add(new Quest("Roof Inspection", "Inspect and finish roof.", reqR16, 16, 100));

            return questsCons2;
        }

        public List<Quest> GetQuestsCons3()
        {
            // Foundation (phases 1-4)
            List<Material> reqF1 = new List<Material> {
                new Material("Bricks", "High-quality brick accents.", 0.7, 0.75, 15),
                new Material("EcoBlock", "Premium eco foundation.", 0.95, 0.9, 140),
                new Material("Recycled", "Recycled aggregate.", 0.95, 0.85, 70),
                new Material("Recycled", "Secondary recycled layer.", 0.95, 0.85, 70)
            };
            List<Material> reqF2 = new List<Material> {
                new Material("Recycled", "Recycled aggregate fill.", 0.95, 0.85, 70),
                new Material("EcoBlock", "Eco foundation blocks.", 0.95, 0.9, 140),
                new Material("Recycled", "Recycled form supports.", 0.95, 0.85, 70)
            };
            List<Material> reqF3 = new List<Material> {
                new Material("EcoBlock", "Premium eco slab blocks.", 0.95, 0.9, 140),
                new Material("EcoBlock", "Secondary eco blocks.", 0.95, 0.9, 140),
                new Material("Recycled", "Recycled reinforcement.", 0.95, 0.85, 70)
            };
            List<Material> reqF4 = new List<Material> {
                new Material("Recycled", "Recycled dressing.", 0.95, 0.85, 70),
                new Material("EcoBlock", "Eco finish blocks.", 0.95, 0.9, 140),
                new Material("Recycled", "Final recycled layer.", 0.95, 0.85, 70)
            };

            // Flooring (phases 5-8)
            List<Material> reqFL5 = new List<Material> {
                new Material("EcoBlock", "Eco joist supports.", 0.95, 0.9, 140),
                new Material("Recycled", "Recycled joist base.", 0.95, 0.85, 70),
                new Material("Recycled", "Recycled underlayment.", 0.95, 0.85, 70)
            };
            List<Material> reqFL6 = new List<Material> {
                new Material("EcoBlock", "Eco subfloor blocks.", 0.95, 0.9, 140),
                new Material("EcoBlock", "Eco support pads.", 0.95, 0.9, 140),
                new Material("Recycled", "Recycled battens.", 0.95, 0.85, 70)
            };
            List<Material> reqFL7 = new List<Material> {
                new Material("EcoBlock", "Premium eco flooring.", 0.95, 0.9, 140),
                new Material("Recycled", "Recycled underlay.", 0.95, 0.85, 70),
                new Material("Recycled", "Secondary recycled layer.", 0.95, 0.85, 70)
            };
            List<Material> reqFL8 = new List<Material> {
                new Material("EcoBlock", "Eco finish blocks.", 0.95, 0.9, 140),
                new Material("Recycled", "Recycled sealant base.", 0.95, 0.85, 70),
                new Material("EcoBlock", "Eco trim blocks.", 0.95, 0.9, 140)
            };

            // Walls (phases 9-12)
            List<Material> reqW9 = new List<Material> {
                new Material("Bricks", "High-quality brick accents.", 0.7, 0.75, 15),
                new Material("EcoBlock", "High-efficiency eco blocks.", 0.95, 0.9, 140),
                new Material("EcoBlock", "Secondary eco blocks.", 0.95, 0.9, 140),
                new Material("Recycled", "Recycled wall base.", 0.95, 0.85, 70)
            };
            List<Material> reqW10 = new List<Material> {
                new Material("EcoBlock", "Structural eco blocks.", 0.95, 0.9, 140),
                new Material("EcoBlock", "Double eco block layer.", 0.95, 0.9, 140),
                new Material("Recycled", "Recycled mortar base.", 0.95, 0.85, 70)
            };
            List<Material> reqW11 = new List<Material> {
                new Material("EcoBlock", "Inner eco blocks.", 0.95, 0.9, 140),
                new Material("Recycled", "Recycled wall framing.", 0.95, 0.85, 70),
                new Material("Recycled", "Recycled insulation.", 0.95, 0.85, 70)
            };
            List<Material> reqW12 = new List<Material> {
                new Material("EcoBlock", "Exterior eco cladding.", 0.95, 0.9, 140),
                new Material("Recycled", "Recycled trim.", 0.95, 0.85, 70),
                new Material("EcoBlock", "Sustainable finish.", 0.95, 0.9, 140)
            };

            // Roofs (phases 13-16)
            List<Material> reqR13 = new List<Material> {
                new Material("Shingle", "Roofing cover.", 0.6, 0.6, 12),
                new Material("EcoBlock", "Eco roof supports.", 0.95, 0.9, 140),
                new Material("Recycled", "Recycled connectors.", 0.95, 0.85, 70),
                new Material("Recycled", "Recycled fixings.", 0.95, 0.85, 70)
            };
            List<Material> reqR14 = new List<Material> {
                new Material("Solar", "Solar panel base.", 1, 0.7, 400),
                new Material("EcoBlock", "Eco sheathing blocks.", 0.95, 0.9, 140),
                new Material("Recycled", "Recycled underlayment.", 0.95, 0.85, 70)
            };
            List<Material> reqR15 = new List<Material> {
                new Material("Solar", "Solar roof tiles.", 1, 0.7, 400),
                new Material("Solar", "Additional solar panels.", 1, 0.7, 400),
                new Material("EcoBlock", "Eco finishing.", 0.95, 0.9, 140)
            };
            List<Material> reqR16 = new List<Material> {
                new Material("Recycled", "Recycled final dressing.", 0.95, 0.85, 70),
                new Material("EcoBlock", "Eco vent blocks.", 0.95, 0.9, 140),
                new Material("Recycled", "Sustainable trim.", 0.95, 0.85, 70)
            };

            questsCons3.Add(new Quest("Site Protection", "Prepare and protect site with sustainable methods.", reqF1, 1, 150));
            questsCons3.Add(new Quest("Precision Excavation", "Excavate precisely with recycled fill.", reqF2, 2, 160));
            questsCons3.Add(new Quest("Pour Premium Concrete", "Pour high-grade sustainable concrete.", reqF3, 3, 180));
            questsCons3.Add(new Quest("Install Foundation Insulation", "Install premium insulation.", reqF4, 4, 170));

            questsCons3.Add(new Quest("Install Eco Joists", "Install bamboo/eco joists.", reqFL5, 5, 175));
            questsCons3.Add(new Quest("Lay Eco Subfloor", "Lay bamboo composite subfloor.", reqFL6, 6, 170));
            questsCons3.Add(new Quest("Lay Premium Flooring", "Lay premium bamboo flooring.", reqFL7, 7, 190));
            questsCons3.Add(new Quest("Finish Floor Eco", "Finish floor using low-VOC products.", reqFL8, 8, 165));

            questsCons3.Add(new Quest("Construct Eco Walls", "Build walls using eco blocks.", reqW9, 9, 180));
            questsCons3.Add(new Quest("Erect Eco Structure", "Erect structural eco-block walls.", reqW10, 10, 200));
            questsCons3.Add(new Quest("High-eff Insulation", "Install high-efficiency insulation.", reqW11, 11, 170));
            questsCons3.Add(new Quest("Sustainable Cladding", "Apply sustainable cladding.", reqW12, 12, 185));

            questsCons3.Add(new Quest("Place Sustainable Trusses", "Install metal trusses with eco methods.", reqR13, 13, 190));
            questsCons3.Add(new Quest("Install Solar Sheathing", "Prepare roof for solar tiles.", reqR14, 14, 165));
            questsCons3.Add(new Quest("Install Solar Tiles", "Install solar tiles as roof covering.", reqR15, 15, 200));
            questsCons3.Add(new Quest("Roof System Test", "Final roof testing and inspection.", reqR16, 16, 155));

            return questsCons3;
        }
    }
}
