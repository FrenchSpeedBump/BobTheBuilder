namespace BobTheBuilder.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void Player_InitializedWithCorrectValues()
        {
            // Arrange & Act
            var player = new Player();

            // Assert
            Assert.Equal("Bob", player.Name);
            Assert.Equal(100, player.Money);
            Assert.Empty(player.Inventory);
        }

        [Fact]
        public void AddItem_AddsItemToInventory()
        {
            // Arrange
            var player = new Player();
            var item = new Item("Hammer", "A sturdy hammer", 0, 10);

            // Act
            player.AddItem(item);

            // Assert
            Assert.Single(player.Inventory);
            Assert.Contains(item, player.Inventory);
        }

        [Fact]
        public void RemoveItem_RemovesItemFromInventory()
        {
            // Arrange
            var player = new Player();
            var item = new Item("Hammer", "A sturdy hammer", 0, 10);
            player.AddItem(item);

            // Act
            player.RemoveItem(item);

            // Assert
            Assert.Empty(player.Inventory);
        }

        [Fact]
        public void BuyItem_WithEnoughMoney_SuccessfullyBuysItem()
        {
            // Arrange
            var player = new Player();
            var item = new Item("Hammer", "A sturdy hammer", 0, 10);

            // Act
            player.BuyItem(item);

            // Assert
            Assert.Equal(90, player.Money);
            Assert.Single(player.Inventory);
            Assert.Contains(item, player.Inventory);
        }

        [Fact]
        public void BuyItem_WithInsufficientMoney_DoesNotBuyItem()
        {
            // Arrange
            var player = new Player();
            var expensiveItem = new Item("Gold Hammer", "A very expensive hammer", 0, 150);

            // Act
            player.BuyItem(expensiveItem);

            // Assert
            Assert.Equal(100, player.Money); // Money unchanged
            Assert.Empty(player.Inventory); // Item not added
        }

        [Fact]
        public void BuyItem_WithExactMoney_SuccessfullyBuysItem()
        {
            // Arrange
            var player = new Player();
            var item = new Item("Expensive Item", "Costs all the money", 0, 100);

            // Act
            player.BuyItem(item);

            // Assert
            Assert.Equal(0, player.Money);
            Assert.Single(player.Inventory);
        }

        [Fact]
        public void AddItem_MultipleItems_AddsAllItems()
        {
            // Arrange
            var player = new Player();
            var hammer = new Item("Hammer", "A sturdy hammer", 0, 10);
            var nails = new Item("Nails", "A box of nails", 0, 5);

            // Act
            player.AddItem(hammer);
            player.AddItem(nails);

            // Assert
            Assert.Equal(2, player.Inventory.Count);
            Assert.Contains(hammer, player.Inventory);
            Assert.Contains(nails, player.Inventory);
        }
    }
}
