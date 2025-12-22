namespace BobTheBuilder
{
    public static class ConstructionUI
    {
        public static void DisplayQuests(List<Quest> quests)
        {
            int questNumber = 1;
            foreach (Quest quest in quests)
            {
                Console.WriteLine("[Quest " + questNumber + "] " + quest.ShortDescription+ " " + quest.LongDescription+ " "+ quest.Price);
                Console.WriteLine("Requirements:");
                if (quest.Requirements.Count == 0)
                {
                    Console.WriteLine("No requirements.");
                }
                else
                {
                    foreach (Material req in quest.Requirements)
                    {
                        Console.Write(req.Name + " ");
                    }
                    Console.WriteLine();
                }
                questNumber++;
            }
        }
    }
}