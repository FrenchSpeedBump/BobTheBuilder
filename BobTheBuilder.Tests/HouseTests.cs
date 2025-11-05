namespace BobTheBuilder.Tests
{
    public class HouseTests
    {
        [Fact]
        public void House_InitializedWithCorrectValues()
        {
            // Arrange & Act
            var house = new House("House", "This is where we are going to build a house");

            // Assert
            Assert.Equal("House", house.ShortDescription);
            Assert.Equal("This is where we are going to build a house", house.LongDescription);
            Assert.Empty(house.UsedMaterials);
        }

        [Fact]
        public void House_InheritsFromRoom()
        {
            // Arrange & Act
            var house = new House("House", "This is where we are going to build a house");

            // Assert
            Assert.IsAssignableFrom<Room>(house);
        }

        [Fact]
        public void UsedMaterials_CanAddMaterial()
        {
            // Arrange
            var house = new House("House", "This is where we are going to build a house");
            var wood = new Material("Wood", "A piece of wood", 0.8, 0);

            // Act
            house.UsedMaterials["Wood"] = wood;

            // Assert
            Assert.Single(house.UsedMaterials);
            Assert.Equal(wood, house.UsedMaterials["Wood"]);
        }

        [Fact]
        public void UsedMaterials_CanAddMultipleMaterials()
        {
            // Arrange
            var house = new House("House", "This is where we are going to build a house");
            var wood = new Material("Wood", "A piece of wood", 0.8, 0);
            var bricks = new Material("Bricks", "A stack of bricks", 0.6, 0);
            var concrete = new Material("Concrete", "A block of concrete", 0.4, 0);

            // Act
            house.UsedMaterials["Wood"] = wood;
            house.UsedMaterials["Bricks"] = bricks;
            house.UsedMaterials["Concrete"] = concrete;

            // Assert
            Assert.Equal(3, house.UsedMaterials.Count);
            Assert.Equal(wood, house.UsedMaterials["Wood"]);
            Assert.Equal(bricks, house.UsedMaterials["Bricks"]);
            Assert.Equal(concrete, house.UsedMaterials["Concrete"]);
        }

        [Fact]
        public void House_CanHaveExits_LikeAnyRoom()
        {
            // Arrange
            var house = new House("House", "This is where we are going to build a house");
            var street = new Room("Street", "A street");

            // Act
            house.SetExit("west", street);

            // Assert
            Assert.Single(house.Exits);
            Assert.Equal(street, house.Exits["west"]);
        }
    }
}
