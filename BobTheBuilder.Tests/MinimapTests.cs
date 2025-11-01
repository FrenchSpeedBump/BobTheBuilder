namespace BobTheBuilder.Tests
{
    public class MinimapTests
    {
        [Fact]
        public void Minimap_CanBeInitialized()
        {
            // Arrange & Act
            var minimap = new Minimap();

            // Assert
            Assert.NotNull(minimap);
        }

        [Fact]
        public void MapRooms_MapsSingleRoom()
        {
            // Arrange
            var minimap = new Minimap();
            var house = new Room("House", "Your house");

            // Act
            minimap.MapRooms(house);

            // Assert - No exception should be thrown
            Assert.NotNull(minimap);
        }

        [Fact]
        public void MapRooms_MapsConnectedRooms()
        {
            // Arrange
            var minimap = new Minimap();
            var house = new Room("House", "Your house");
            var street = new Room("Street", "A street");
            house.SetExit("west", street);
            street.SetExit("east", house);

            // Act
            minimap.MapRooms(house);

            // Assert - No exception should be thrown
            Assert.NotNull(minimap);
        }

        [Fact]
        public void GetDirectionTo_SameRoom_ReturnsYouAreHere()
        {
            // Arrange
            var minimap = new Minimap();
            var house = new Room("House", "Your house");
            minimap.MapRooms(house);

            // Act
            var direction = minimap.GetDirectionTo(house, house);

            // Assert
            Assert.Equal("You are here", direction);
        }

        [Fact]
        public void GetDirectionTo_UnmappedRoom_ReturnsUnknownLocation()
        {
            // Arrange
            var minimap = new Minimap();
            var house = new Room("House", "Your house");
            var unmappedRoom = new Room("Unknown", "Not mapped");

            // Act
            var direction = minimap.GetDirectionTo(house, unmappedRoom);

            // Assert
            Assert.Equal("Unknown location", direction);
        }

        [Fact]
        public void GetDirectionTo_NorthRoom_ReturnsNorthDirection()
        {
            // Arrange
            var minimap = new Minimap();
            var center = new Room("Center", "Center room");
            var north = new Room("North", "North room");
            center.SetExit("north", north);
            north.SetExit("south", center);
            minimap.MapRooms(center);

            // Act
            var direction = minimap.GetDirectionTo(center, north);

            // Assert
            Assert.Contains("north", direction.ToLower());
        }

        [Fact]
        public void GetDirectionTo_SouthRoom_ReturnsSouthDirection()
        {
            // Arrange
            var minimap = new Minimap();
            var center = new Room("Center", "Center room");
            var south = new Room("South", "South room");
            center.SetExit("south", south);
            south.SetExit("north", center);
            minimap.MapRooms(center);

            // Act
            var direction = minimap.GetDirectionTo(center, south);

            // Assert
            Assert.Contains("south", direction.ToLower());
        }

        [Fact]
        public void GetDirectionTo_EastRoom_ReturnsEastDirection()
        {
            // Arrange
            var minimap = new Minimap();
            var center = new Room("Center", "Center room");
            var east = new Room("East", "East room");
            center.SetExit("east", east);
            east.SetExit("west", center);
            minimap.MapRooms(center);

            // Act
            var direction = minimap.GetDirectionTo(center, east);

            // Assert
            Assert.Contains("east", direction.ToLower());
        }

        [Fact]
        public void GetDirectionTo_WestRoom_ReturnsWestDirection()
        {
            // Arrange
            var minimap = new Minimap();
            var center = new Room("Center", "Center room");
            var west = new Room("West", "West room");
            center.SetExit("west", west);
            west.SetExit("east", center);
            minimap.MapRooms(center);

            // Act
            var direction = minimap.GetDirectionTo(center, west);

            // Assert
            Assert.Contains("west", direction.ToLower());
        }

        [Fact]
        public void MapRooms_ComplexNetwork_MapsAllRooms()
        {
            // Arrange
            var minimap = new Minimap();
            var house = new Room("House", "Your house");
            var street1 = new Room("Street_1", "First street");
            var streetMain = new Room("Street_Main", "Main street");
            var streetNorth = new Room("Street_North", "North street");
            
            house.SetExit("west", street1);
            street1.SetExit("east", house);
            street1.SetExit("west", streetMain);
            streetMain.SetExit("east", street1);
            streetMain.SetExit("west", streetNorth);
            streetNorth.SetExit("east", streetMain);

            // Act
            minimap.MapRooms(house);

            // Assert - No exception should be thrown, all rooms should be mapped
            Assert.NotNull(minimap);
        }

        [Fact]
        public void GetDirectionTo_MultipleStepsAway_ReturnsCorrectDirection()
        {
            // Arrange
            var minimap = new Minimap();
            var room1 = new Room("Room1", "First room");
            var room2 = new Room("Room2", "Second room");
            var room3 = new Room("Room3", "Third room");
            
            room1.SetExit("east", room2);
            room2.SetExit("west", room1);
            room2.SetExit("east", room3);
            room3.SetExit("west", room2);
            
            minimap.MapRooms(room1);

            // Act
            var direction = minimap.GetDirectionTo(room1, room3);

            // Assert
            Assert.Contains("east", direction.ToLower());
        }
    }
}
