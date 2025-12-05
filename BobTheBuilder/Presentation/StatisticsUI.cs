namespace BobTheBuilder
{
    public static class StatisticsUI
    {
        public static void DisplayStats(Statistics statistics, int day)
        {
            Console.WriteLine("==============|Statistics for Day " + day + "|==============");
            Console.WriteLine("Average Sustainability: " + statistics.GetAverageSustainability());
            Console.WriteLine("Average Quality: " + statistics.GetAverageQuality());
            Console.WriteLine("===================================");
        }
    }
}