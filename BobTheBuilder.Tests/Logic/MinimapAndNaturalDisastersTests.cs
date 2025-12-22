using System.Reflection;
using BobTheBuilder.Logic;

namespace BobTheBuilder.Tests.Logic;

public class MinimapTests
{
    [Test]
    public void MapRooms_ShouldAssignGridPositions()
    {
        var start = new Room("House", "desc");
        var north = new Room("North", "desc");
        start.SetExit("north", north);
        var minimap = new Minimap();

        minimap.MapRooms(start);

        Assert.Multiple(() =>
        {
            Assert.That(minimap.roomPositions[start], Is.EqualTo((0, 0)));
            Assert.That(minimap.roomPositions[north], Is.EqualTo((0, -1)));
        });
    }
}

public class NaturalDisastersTests
{
    [Test]
    public void DisasterStruck_ShouldReduceStructureValuesWhenHit()
    {
        var disasters = new NaturalDisasters();
        ConfigureDeterministicDisaster(disasters);
        var house = new House("House", "desc")
        {
            foundation = 1,
            walls = 1,
            roof = 1,
            foundationHP = 10,
            foundationQuality = 0.0,
            wallsHP = 10,
            wallsQuality = 0.0,
            roofHP = 10,
            roofQuality = 0.0
        };

        var result = disasters.DisasterStruck(house, 1);

        Assert.Multiple(() =>
        {
            Assert.That(result.HouseSurvived, Is.False); // House destroyed
            Assert.That(house.foundationHP, Is.LessThan(0)); // HP went negative
        });
    }

    [Test]
    public void DisasterStruck_ShouldIgnoreHouseWithHighDurability()
    {
        var disasters = new NaturalDisasters();
        var house = new House("House", "desc")
        {
            foundation = 1,
            walls = 1,
            roof = 1,
            foundationHP = 100,
            foundationQuality = 0.9,
            wallsHP = 100,
            wallsQuality = 0.9,
            roofHP = 100,
            roofQuality = 0.9
        };

        var result = disasters.DisasterStruck(house, 1);

        Assert.Multiple(() =>
        {
            Assert.That(result.HouseSurvived, Is.True);
            Assert.That(house.foundationHP, Is.EqualTo(100)); // Quality blocked damage
        });
    }

    private static void ConfigureDeterministicDisaster(NaturalDisasters disasters)
    {
        var naturalDisasterType = typeof(NaturalDisasters).Assembly
            .GetType("BobTheBuilder.Logic.NaturalDisaster", throwOnError: true)!;
        var disasterInstance = Activator.CreateInstance(naturalDisasterType, "test", 0.9, 0.9, 0.9)!;
        var listType = typeof(List<>).MakeGenericType(naturalDisasterType);
        var disasterList = Activator.CreateInstance(listType)!;
        var addMethod = listType.GetMethod("Add")!;
        for (int i = 0; i < 4; i++)
        {
            addMethod.Invoke(disasterList, new[] { disasterInstance });
        }

        typeof(NaturalDisasters).GetField("disasters", BindingFlags.NonPublic | BindingFlags.Instance)!
            .SetValue(disasters, disasterList);
    }
}