using System.Reflection;

namespace BobTheBuilder.Tests.Presentation;

public class GameTests
{
    [Test]
    public void Constructor_ShouldStartAtHouse()
    {
        var game = new Game();
        var currentRoom = (Room?)typeof(Game).GetField("currentRoom", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(game);

        Assert.Multiple(() =>
        {
            Assert.That(game.house, Is.Not.Null);
            Assert.That(currentRoom?.ShortDescription, Is.EqualTo("House"));
            Assert.That(game.house.discovered, Is.True);
        });
    }

    [Test]
    public void FindRoomByName_ShouldReturnRequestedRoom()
    {
        var game = new Game();
        var method = typeof(Game).GetMethod("FindRoomByName", BindingFlags.NonPublic | BindingFlags.Instance)!;

        var shop = method.Invoke(game, new object?[] { "Bob's", "Materials", null }) as Room;

        Assert.That(shop?.ShortDescription, Is.EqualTo("Bob's Materials"));
    }

    [Test]
    public void Shops_ShouldContainSeededInventory()
    {
        var game = new Game();
        var method = typeof(Game).GetMethod("FindRoomByName", BindingFlags.NonPublic | BindingFlags.Instance)!;

        var magicShop = method.Invoke(game, new object?[] { "Magic", "Tool", "Shop" }) as Shop;
        var materialShop = method.Invoke(game, new object?[] { "Bob's", "Materials", null }) as Shop;

        Assert.Multiple(() =>
        {
            Assert.That(magicShop?.Inventory.ContainsKey("Hammer"), Is.True);
            Assert.That(materialShop?.Inventory.ContainsKey("Wood"), Is.True);
        });
    }
}

public class ProgramTests
{
    [Test]
    public void Main_ShouldExistAndBeParameterless()
    {
        var method = typeof(Program).GetMethod("Main", BindingFlags.Public | BindingFlags.Static);

        Assert.Multiple(() =>
        {
            Assert.That(method, Is.Not.Null);
            Assert.That(method!.GetParameters(), Is.Empty);
            Assert.That(method.ReturnType, Is.EqualTo(typeof(void)));
        });
    }
}