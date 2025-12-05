namespace BobTheBuilder
{
    public static class ConstructionUI
    {
        public static void DisplayQuests(List<Quest> quests)
        {
            foreach (Quest quest in quests)
            {
                Console.WriteLine(quest.shortDescription+ " " + quest.longDescription+ " "+ quest.price);
                Console.WriteLine("Requirements:");
                if (quest.requirements.Count < 0)
                {
                    Console.WriteLine("No requirements.");
                }
                else
                {
                    foreach (Material req in quest.requirements)
                    {
                        Console.Write(req.Name + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}