namespace BobTheBuilder
{
    public static class StatisticsUI
    {
        public static void DisplayStats(Statistics statistics, int day)
        {
            // Don't show stats on day 1 since there's nothing to display yet
            if (day <= 1)
            {
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("============|Statistics for Day " + day + "|============");
            Console.ResetColor();
            Console.WriteLine("Average Sustainability: " + statistics.GetAverageSustainability().ToString("F2"));
            Console.WriteLine("Average Quality: " + statistics.GetAverageQuality().ToString("F2"));
            Console.WriteLine("Total Money Spent: " + statistics.GetTotalMoneySpent().ToString("F2"));
            Console.WriteLine("Quests Completed: " + statistics.GetQuestsCompleted());
            Console.WriteLine("===================================");
        }
        public static void DisplayEndStats(Statistics statistics, int day)
        {
            Console.WriteLine("============|Final Statistics|============");
            Console.WriteLine("Average Sustainability: " + statistics.GetAverageSustainability().ToString("F2"));
            Console.WriteLine("Average Quality: " + statistics.GetAverageQuality().ToString("F2"));
            Console.WriteLine("Total Money Spent: " + statistics.GetTotalMoneySpent().ToString("F2"));
            Console.WriteLine("Quests Completed: " + statistics.GetQuestsCompleted());
            Console.WriteLine("Number of Natural Disasters: " + statistics.GetNaturalDisasters());
            Console.WriteLine("===================================");
        }
    }
}