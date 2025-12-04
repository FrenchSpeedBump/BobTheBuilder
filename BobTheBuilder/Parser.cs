namespace BobTheBuilder
{
    public class Parser
    {
        private readonly CommandWords commandWords = new();

        public Command? GetCommand(string inputLine)
        {
            string[] words = inputLine.Split();

            if (words.Length == 0 || !commandWords.IsValidCommand(words[0]))
            {
                return null;
            }
            if (words.Length == 3)
            {
                return new Command(words[0], words[1], words[2]);
            }
            if (words.Length == 2)
            {
                return new Command(words[0], words[1]);
            }
            if (words.Length == 4)
            {
                return new Command(words[0], words[1], words[2], words[3]);
            }

            return new Command(words[0]);
        }
    }

}
