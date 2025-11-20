using System.Collections.Generic;

namespace BobTheBuilder
{
    public class GameInit
    {
        // Return every room created keyed by a normalized short description
        public Dictionary<string, Room> CreateRooms()
        {
            QuestInit questInit = new QuestInit();
            var rooms = new Dictionary<string, Room>(StringComparer.OrdinalIgnoreCase);


            House house = new("House", "This is where we are going to build a house :)");
            Room street_1 = new("Street_1", "A street leading to our house going into the city. Nothing but nature around us.");
            Room street_main = new("Street_Main", "The main street ouf our town. The street has an office building on one side and a bank on the other");
            Room street_north = new("Street_North", "The north street has two shops: Bob's Materials and the Magic Tool Shop. Funily enoguh, the street doesn't face north.");
            Room alley = new("Alley", "A small alley between two buildings. Nothing to see here.");
            Room office = new("Office Building", "When you step into this old office building you are met with three construction company signs: Best Build, Big Build and Small Build");
            Bank bank = new("Bank", "A nice empty building where at the other side of the room a young teller is smiling at you");
            Shop materials = new("Bob's Materials", "Rows and rows of construction materials");
            Shop tools = new("Magic Tool Shop", "A nice old man is very happy to tell you everything about a hammer");
            ConstructionBuilding cons_1 = new("Best Build", "Constructions", questInit.GetQuestsCons1());
            ConstructionBuilding cons_2 = new("Big Build", "Constructions", questInit.GetQuestsCons2());
            ConstructionBuilding cons_3 = new("Small Build", "Constructions", questInit.GetQuestsCons3());
            Room forest = new("Forest", "Just a bunch of trees and bushes. Nothing to do here.");

            // road network
            house.SetExit("west", street_1);
            street_1.SetExits(forest, house, null, street_main);
            street_main.SetExits(alley, street_1, bank, street_north);
            alley.SetExits(office, null, street_main, null);
            street_north.SetExits(tools, street_main, materials, null);
            forest.SetExits(null, null, street_1, office);
            office.SetExits(cons_2, cons_3, alley, cons_1);
            bank.SetExits(street_main, null, null, materials);
            materials.SetExits(street_north, bank, null, null);
            tools.SetExits(null, office, street_north, null);
            cons_1.SetExit("east", office);
            cons_2.SetExit("south", office);
            cons_3.SetExit("west", office);

            // add quests to construction buildings



            // add every created room into the returned dictionary
            AddRoom(rooms, house);
            AddRoom(rooms, street_1);
            AddRoom(rooms, street_main);
            AddRoom(rooms, street_north);
            AddRoom(rooms, office);
            AddRoom(rooms, bank);
            AddRoom(rooms, materials);
            AddRoom(rooms, tools);
            AddRoom(rooms, cons_1);
            AddRoom(rooms, cons_2);
            AddRoom(rooms, cons_3);
            AddRoom(rooms, forest);

            return rooms;
        }

        // Return every item created along with the target shop short description
        public List<(string shopShortDescription, Item item)> CreateItems()
        {
            var list = new List<(string, Item)>();
            Item hammer = new("Hammer", "A sturdy hammer for building.", 0, 10);
            Item nails = new("Nails", "A box of small nails.", 0, 5);

            list.Add(("Magic Tool Shop", hammer));
            list.Add(("Magic Tool Shop", nails));
            return list;
        }

        // Return every material created along with the target shop short description
        public List<(string shopShortDescription, Material material)> CreateMaterials()
        {
            var list = new List<(string, Material)>();
            Material wood = new("Wood", "A sturdy piece of wood.", 0.8, 20);
            Material bricks = new("Bricks", "A stack of red bricks.", 0.6, 15);
            Material concrete = new("Concrete", "A heavy block of concrete.", 0.4, 10);
            Material glass = new("Glass", "A transparent sheet of glass.", 0.5, 5);

            list.Add(("Bob's Materials", wood));
            list.Add(("Bob's Materials", bricks));
            list.Add(("Bob's Materials", concrete));
            list.Add(("Bob's Materials", glass));
            return list;
        }

        // helper to add a room into dictionary with normalized key
        private void AddRoom(Dictionary<string, Room> dict, Room? r)
        {
            if (r == null) return;
            dict[Normalize(r.ShortDescription)] = r;
        }

        // make Normalize public so callers can look up keys the same way
        public static string Normalize(string? s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            return s.Trim()
                    .Replace(" ", "")
                    .Replace("_", "")
                    .Replace("-", "")
                    .Replace("'", "")
                    .Replace("\"", "")
                    .Replace(".", "")
                    .Replace(",", "")
                    .ToLowerInvariant();
        }
    }
}
