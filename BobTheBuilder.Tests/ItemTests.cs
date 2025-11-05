namespace BobTheBuilder.Tests
{
    public class ItemTests
    {
        [Fact]
        public void Item_InitializedWithCorrectValues()
        {
            // Arrange & Act
            var item = new Item("Hammer", "A sturdy hammer", 0.5, 10);

            // Assert
            Assert.Equal("Hammer", item.Name);
            Assert.Equal("A sturdy hammer", item.Description);
            Assert.Equal(0.5, item.Sustainability);
            Assert.Equal(10, item.Price);
        }

        [Fact]
        public void Material_InitializedWithCorrectValues()
        {
            // Arrange & Act
            var material = new Material("Wood", "A piece of wood", 0.8, 15);

            // Assert
            Assert.Equal("Wood", material.Name);
            Assert.Equal("A piece of wood", material.Description);
            Assert.Equal(0.8, material.Sustainability);
            Assert.Equal(15, material.Price);
        }

        [Fact]
        public void Item_InheritsFromShopInventoryContents()
        {
            // Arrange & Act
            var item = new Item("Hammer", "A sturdy hammer", 0, 10);

            // Assert
            Assert.IsAssignableFrom<ShopInventoryContents>(item);
        }

        [Fact]
        public void Material_InheritsFromShopInventoryContents()
        {
            // Arrange & Act
            var material = new Material("Wood", "A piece of wood", 0.8, 0);

            // Assert
            Assert.IsAssignableFrom<ShopInventoryContents>(material);
        }

        [Fact]
        public void Item_WithZeroPrice_InitializesCorrectly()
        {
            // Arrange & Act
            var item = new Item("Free Hammer", "A free hammer", 0, 0);

            // Assert
            Assert.Equal(0, item.Price);
        }

        [Fact]
        public void Material_WithZeroSustainability_InitializesCorrectly()
        {
            // Arrange & Act
            var material = new Material("Plastic", "Non-sustainable material", 0, 5);

            // Assert
            Assert.Equal(0, material.Sustainability);
        }
    }
}
