namespace BobTheBuilder.Tests
{
    public class RoomTests
    {
        [Fact]
        public void Room_InitializedWithCorrectValues()
        {
            // Arrange & Act
            var room = new Room("House", "This is where we are going to build a house");

            // Assert
            Assert.Equal("House", room.ShortDescription);
            Assert.Equal("This is where we are going to build a house", room.LongDescription);
            Assert.Empty(room.Exits);
        }

        [Fact]
        public void SetExit_AddsSingleExit()
        {
            // Arrange
            var room1 = new Room("House", "Your house");
            var room2 = new Room("Street", "A street");

            // Act
            room1.SetExit("west", room2);

            // Assert
            Assert.Single(room1.Exits);
            Assert.Equal(room2, room1.Exits["west"]);
        }

        [Fact]
        public void SetExit_WithNullRoom_DoesNotAddExit()
        {
            // Arrange
            var room = new Room("House", "Your house");

            // Act
            room.SetExit("west", null);

            // Assert
            Assert.Empty(room.Exits);
        }

        [Fact]
        public void SetExits_AddsMultipleExits()
        {
            // Arrange
            var center = new Room("Center", "Center room");
            var north = new Room("North", "North room");
            var east = new Room("East", "East room");
            var south = new Room("South", "South room");
            var west = new Room("West", "West room");

            // Act
            center.SetExits(north, east, south, west);

            // Assert
            Assert.Equal(4, center.Exits.Count);
            Assert.Equal(north, center.Exits["north"]);
            Assert.Equal(east, center.Exits["east"]);
            Assert.Equal(south, center.Exits["south"]);
            Assert.Equal(west, center.Exits["west"]);
        }

        [Fact]
        public void SetExits_WithSomeNullRooms_OnlyAddsNonNullExits()
        {
            // Arrange
            var center = new Room("Center", "Center room");
            var north = new Room("North", "North room");
            var south = new Room("South", "South room");

            // Act
            center.SetExits(north, null, south, null);

            // Assert
            Assert.Equal(2, center.Exits.Count);
            Assert.Equal(north, center.Exits["north"]);
            Assert.Equal(south, center.Exits["south"]);
        }

        [Fact]
        public void SetExit_OverwritesExistingExit()
        {
            // Arrange
            var room1 = new Room("House", "Your house");
            var room2 = new Room("Street1", "First street");
            var room3 = new Room("Street2", "Second street");
            room1.SetExit("west", room2);

            // Act
            room1.SetExit("west", room3);

            // Assert
            Assert.Single(room1.Exits);
            Assert.Equal(room3, room1.Exits["west"]);
        }

        [Fact]
        public void Exits_AreBidirectional_WhenSetCorrectly()
        {
            // Arrange
            var room1 = new Room("House", "Your house");
            var room2 = new Room("Street", "A street");

            // Act
            room1.SetExit("west", room2);
            room2.SetExit("east", room1);

            // Assert
            Assert.Equal(room2, room1.Exits["west"]);
            Assert.Equal(room1, room2.Exits["east"]);
        }
    }
}
