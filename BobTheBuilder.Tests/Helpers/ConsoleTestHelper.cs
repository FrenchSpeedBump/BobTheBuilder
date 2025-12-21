namespace BobTheBuilder.Tests.Helpers;

internal static class ConsoleTestHelper
{
    public static string CaptureOutput(Action action)
    {
        var originalOut = Console.Out;
        try
        {
            using StringWriter writer = new();
            Console.SetOut(writer);
            action();
            writer.Flush();
            return writer.ToString();
        }
        finally
        {
            Console.SetOut(originalOut);
        }
    }
}