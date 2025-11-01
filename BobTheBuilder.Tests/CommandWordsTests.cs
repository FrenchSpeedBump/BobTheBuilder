namespace BobTheBuilder.Tests
{
    public class CommandWordsTests
    {
        [Fact]
        public void CommandWords_ContainsAllDirectionCommands()
        {
            // Arrange
            var commandWords = new CommandWords();

            // Assert
            Assert.Contains("north", commandWords.ValidCommands);
            Assert.Contains("south", commandWords.ValidCommands);
            Assert.Contains("east", commandWords.ValidCommands);
            Assert.Contains("west", commandWords.ValidCommands);
        }

        [Fact]
        public void CommandWords_ContainsAllGameCommands()
        {
            // Arrange
            var commandWords = new CommandWords();

            // Assert
            Assert.Contains("look", commandWords.ValidCommands);
            Assert.Contains("back", commandWords.ValidCommands);
            Assert.Contains("quit", commandWords.ValidCommands);
            Assert.Contains("help", commandWords.ValidCommands);
        }

        [Fact]
        public void CommandWords_ContainsNewCommands()
        {
            // Arrange
            var commandWords = new CommandWords();

            // Assert
            Assert.Contains("map", commandWords.ValidCommands);
            Assert.Contains("goto", commandWords.ValidCommands);
            Assert.Contains("travel", commandWords.ValidCommands);
            Assert.Contains("gointo", commandWords.ValidCommands);
            Assert.Contains("inventory", commandWords.ValidCommands);
            Assert.Contains("buy", commandWords.ValidCommands);
        }

        [Fact]
        public void IsValidCommand_ValidCommand_ReturnsTrue()
        {
            // Arrange
            var commandWords = new CommandWords();

            // Act & Assert
            Assert.True(commandWords.IsValidCommand("north"));
            Assert.True(commandWords.IsValidCommand("look"));
            Assert.True(commandWords.IsValidCommand("map"));
            Assert.True(commandWords.IsValidCommand("buy"));
        }

        [Fact]
        public void IsValidCommand_InvalidCommand_ReturnsFalse()
        {
            // Arrange
            var commandWords = new CommandWords();

            // Act & Assert
            Assert.False(commandWords.IsValidCommand("invalid"));
            Assert.False(commandWords.IsValidCommand("jump"));
            Assert.False(commandWords.IsValidCommand("run"));
        }

        [Fact]
        public void IsValidCommand_EmptyString_ReturnsFalse()
        {
            // Arrange
            var commandWords = new CommandWords();

            // Act & Assert
            Assert.False(commandWords.IsValidCommand(""));
        }

        [Fact]
        public void ValidCommands_HasCorrectCount()
        {
            // Arrange
            var commandWords = new CommandWords();

            // Assert
            Assert.Equal(14, commandWords.ValidCommands.Count); // 4 directions + 10 other commands
        }
    }
}
