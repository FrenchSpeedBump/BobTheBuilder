namespace BobTheBuilder.Tests
{
    public class CommandTests
    {
        [Fact]
        public void Command_WithOnlyName_InitializesCorrectly()
        {
            // Arrange & Act
            var command = new Command("look");

            // Assert
            Assert.Equal("look", command.Name);
            Assert.Null(command.SecondWord);
            Assert.Null(command.ThirdWord);
        }

        [Fact]
        public void Command_WithNameAndSecondWord_InitializesCorrectly()
        {
            // Arrange & Act
            var command = new Command("goto", "House");

            // Assert
            Assert.Equal("goto", command.Name);
            Assert.Equal("House", command.SecondWord);
            Assert.Null(command.ThirdWord);
        }

        [Fact]
        public void Command_WithAllThreeWords_InitializesCorrectly()
        {
            // Arrange & Act
            var command = new Command("gointo", "Best", "Build");

            // Assert
            Assert.Equal("gointo", command.Name);
            Assert.Equal("Best", command.SecondWord);
            Assert.Equal("Build", command.ThirdWord);
        }

        [Fact]
        public void Command_DirectionCommands_InitializeCorrectly()
        {
            // Arrange & Act
            var north = new Command("north");
            var south = new Command("south");
            var east = new Command("east");
            var west = new Command("west");

            // Assert
            Assert.Equal("north", north.Name);
            Assert.Equal("south", south.Name);
            Assert.Equal("east", east.Name);
            Assert.Equal("west", west.Name);
        }

        [Fact]
        public void Command_BuyCommand_WithItemName()
        {
            // Arrange & Act
            var command = new Command("buy", "Hammer");

            // Assert
            Assert.Equal("buy", command.Name);
            Assert.Equal("Hammer", command.SecondWord);
        }
    }
}
