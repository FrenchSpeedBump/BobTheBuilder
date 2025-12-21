

namespace BobTheBuilder.Tests.Logic;

public class RoomTests
{
    [Test]
    public void SetExits_ShouldPopulateNeighbors()
    {
        var room = new Room("House", "desc");
        var north = new Room("North", "desc");
        var east = new Room("East", "desc");
        var south = new Room("South", "desc");
        var west = new Room("West", "desc");

        room.SetExits(north, east, south, west);

        Assert.Multiple(() =>
        {
            Assert.That(room.Exits["north"], Is.SameAs(north));
            Assert.That(room.Exits["east"], Is.SameAs(east));
            Assert.That(room.Exits["south"], Is.SameAs(south));
            Assert.That(room.Exits["west"], Is.SameAs(west));
        });
    }

    [Test]
    public void SetExit_ShouldIgnoreNullNeighbor()
    {
        var room = new Room("House", "desc");

        room.SetExit("north", null);

        Assert.That(room.Exits.ContainsKey("north"), Is.False);
    }
}

public class ShopTests
{
    [Test]
    public void AddContents_ShouldStoreByName()
    {
        var shop = new Shop("Shop", "desc");
        var hammer = new Item("Hammer", "desc", 20);

        shop.AddContents(hammer);

        Assert.That(shop.Inventory["Hammer"], Is.SameAs(hammer));
    }

    [Test]
    public void RemoveContents_ShouldDropEntry()
    {
        var shop = new Shop("Shop", "desc");
        var hammer = new Item("Hammer", "desc", 20);
        shop.AddContents(hammer);

        shop.RemoveContents(hammer);

        Assert.That(shop.Inventory.ContainsKey("Hammer"), Is.False);
    }

    [Test]
    public void GetContents_ShouldReturnNullWhenMissing()
    {
        var shop = new Shop("Shop", "desc");

        var result = shop.GetContents("Unknown");

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Item_ShouldPersistEffectMetadata()
    {
        var coupon = new Item("Coupon", "desc", 10, "Wood", 0.5);

        Assert.Multiple(() =>
        {
            Assert.That(coupon.efect, Is.EqualTo("Wood"));
            Assert.That(coupon.discount, Is.EqualTo(0.5));
        });
    }

    [Test]
    public void Material_ShouldPersistAttributes()
    {
        var material = new Material("Wood", "desc", 0.8, 0.9, 20);

        Assert.Multiple(() =>
        {
            Assert.That(material.Sustainability, Is.EqualTo(0.8));
            Assert.That(material.Quality, Is.EqualTo(0.9));
            Assert.That(material.Price, Is.EqualTo(20));
        });
    }
}