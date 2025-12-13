namespace BobTheBuilder
{
    public class CommandWords
    {
        public List<string> ValidCommands { get; } = new List<string> { "north", "east", "south", "west", "look", "back", "quit", "help", "map", "travel", "gointo", "inventory", "buy", "loan", "account", "accept","sleep","work" };

        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
