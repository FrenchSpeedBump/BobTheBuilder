namespace BobTheBuilder.Presentation.UI
{
    public static class NaturalDisastersUI
    {
        public static void DisplayDisasterAnnouncement(string disasterName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n================================");
            Console.WriteLine("       NATURAL DISASTER!");
            Console.WriteLine("================================");
            Console.ResetColor();
            Console.WriteLine("A {0} has struck your house!\n", disasterName);
        }

        public static void DisplayDamageReport(string partName, double currentHP)
        {
            Console.WriteLine("{0} damage! {0} HP reduced to {1:F1}%", partName, currentHP);
        }

        public static void DisplayBlockedMessage(string partName, double quality, double currentHP)
        {
            Console.WriteLine("{0} quality blocked the disaster! (Quality: {1:F2}, HP: {2:F1}%)", partName, quality, currentHP);
        }

        public static void DisplayRepairSuggestion()
        {
            Console.WriteLine("Consider repairing your house at a construction building.");
        }
    }
}
