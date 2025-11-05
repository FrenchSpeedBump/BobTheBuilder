namespace BobTheBuilder.Tests
{
    public class ShopTests
    {
        [Fact]
        public void Shop_InitializedWithCorrectValues()
        {
            // Arrange & Act
            var shop = new Shop("Magic Tool Shop", "A shop selling tools");

            // Assert
            Assert.Equal("Magic Tool Shop", shop.ShortDescription);
            Assert.Equal("A shop selling tools", shop.LongDescription);
            Assert.Empty(shop.Inventory);
        }

        [Fact]
        public void Shop_InheritsFromRoom()
        {
            // Arrange & Act
            var shop = new Shop("Magic Tool Shop", "A shop selling tools");

            // Assert
            Assert.IsAssignableFrom<Room>(shop);
        }

        [Fact]
        public void AddContents_AddsItemToInventory()
        {
            // Arrange
            var shop = new Shop("Magic Tool Shop", "A shop selling tools");
            var item = new Item("Hammer", "A sturdy hammer", 0, 10);

            // Act
            shop.AddContents(item);

            // Assert
            Assert.Single(shop.Inventory);
            Assert.Equal(item, shop.Inventory["Hammer"]);
        }

        [Fact]
        public void AddContents_AddsMaterialToInventory()
        {
            // Arrange
            var shop = new Shop("Bob's Materials", "A materials shop");
            var material = new Material("Wood", "A piece of wood", 0.8, 0);

            // Act
            shop.AddContents(material);

            // Assert
            Assert.Single(shop.Inventory);
            Assert.Equal(material, shop.Inventory["Wood"]);
        }

        [Fact]
        public void RemoveContents_RemovesItemFromInventory()
        {
            // Arrange
            var shop = new Shop("Magic Tool Shop", "A shop selling tools");
            var item = new Item("Hammer", "A sturdy hammer", 0, 10);
            shop.AddContents(item);

            // Act
            shop.RemoveContents(item);

            // Assert
            Assert.Empty(shop.Inventory);
        }

        [Fact]
        public void GetContents_ReturnsCorrectItem()
        {
            // Arrange
            var shop = new Shop("Magic Tool Shop", "A shop selling tools");
            var hammer = new Item("Hammer", "A sturdy hammer", 0, 10);
            shop.AddContents(hammer);

            // Act
            var result = shop.GetContents("Hammer");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(hammer, result);
        }

        [Fact]
        public void GetContents_ReturnsNullForNonExistentItem()
        {
            // Arrange
            var shop = new Shop("Magic Tool Shop", "A shop selling tools");

            // Act
            var result = shop.GetContents("NonExistent");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AddContents_MultipleItems_AddsAllItems()
        {
            // Arrange
            var shop = new Shop("Magic Tool Shop", "A shop selling tools");
            var hammer = new Item("Hammer", "A sturdy hammer", 0, 10);
            var nails = new Item("Nails", "A box of nails", 0, 5);

            // Act
            shop.AddContents(hammer);
            shop.AddContents(nails);

            // Assert
            Assert.Equal(2, shop.Inventory.Count);
            Assert.Equal(hammer, shop.Inventory["Hammer"]);
            Assert.Equal(nails, shop.Inventory["Nails"]);
        }

        [Fact]
        public void AddContents_MixedItemsAndMaterials_AddsBoth()
        {
            // Arrange
            var shop = new Shop("General Store", "A store with everything");
            var hammer = new Item("Hammer", "A sturdy hammer", 0, 10);
            var wood = new Material("Wood", "A piece of wood", 0.8, 0);

            // Act
            shop.AddContents(hammer);
            shop.AddContents(wood);

            // Assert
            Assert.Equal(2, shop.Inventory.Count);
            Assert.Equal(hammer, shop.Inventory["Hammer"]);
            Assert.Equal(wood, shop.Inventory["Wood"]);
        }
    }
}
