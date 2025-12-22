namespace BobTheBuilder
{
    public static class ConstructionUI
    {
        public static void DisplayQuests(List<Quest> quests)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n=== Available Quests ===\n");
            Console.ResetColor();
            int questNumber = 1;
            foreach (Quest quest in quests)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Quest " + questNumber + ": ");
                Console.ResetColor();
                Console.WriteLine(quest.ShortDescription);
                Console.WriteLine("  " + quest.LongDescription);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("  Price: ");
                Console.ResetColor();
                Console.WriteLine("$" + quest.Price);
                Console.WriteLine("  Quality: " + quest.GetAverageQuality().ToString("F2") + " | Sustainability: " + quest.GetAverageSustainability().ToString("F2"));
                
                if (quest.Requirements.Count == 0)
                {
                    Console.WriteLine("  Materials: None");
                }
                else
                {
                    Console.Write("  Materials: ");
                    foreach (Material req in quest.Requirements)
                    {
                        Console.Write(req.Name + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                questNumber++;
            }
        }
    }
}