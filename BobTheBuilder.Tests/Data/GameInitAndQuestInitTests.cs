namespace BobTheBuilder.Tests.Data;

public class GameInitTests
{
    [Test]
    public void CreateRooms_ShouldNormalizeKeys()
    {
        var init = new GameInit();

        Dictionary<string, Room> rooms = init.CreateRooms();

        string houseKey = GameInit.Normalize(" House ");
        string shopKey = GameInit.Normalize("Bob's Materials");

        Assert.Multiple(() =>
        {
            Assert.That(rooms.ContainsKey(houseKey), Is.True);
            Assert.That(rooms.ContainsKey(shopKey), Is.True);
            Assert.That(rooms[houseKey], Is.InstanceOf<House>());
        });
    }

    [Test]
    public void CreateItems_ShouldReturnPreconfiguredItems()
    {
        var init = new GameInit();

        var items = init.CreateItems();

        Assert.Multiple(() =>
        {
            Assert.That(items, Has.Count.EqualTo(3));
            Assert.That(items.Select(tuple => tuple.item.Name), Does.Contain("Hammer"));
        });
    }

    [Test]
    public void CreateMaterials_ShouldExposeQuestMaterials()
    {
        var init = new GameInit();

        var materials = init.CreateMaterials();

        Assert.That(materials.Any(tuple => tuple.material.Name == "Bamboo"), Is.True);
    }

    [Test]
    public void Normalize_ShouldStripWhitespaceAndPunctuation()
    {
        var normalized = GameInit.Normalize("  Bob's-Materials ");

        Assert.That(normalized, Is.EqualTo("bobsmaterials"));
    }
}

public class QuestInitTests
{
    [TestCaseSource(nameof(QuestFactories))]
    public void QuestSets_ShouldCoverAllPhases(Func<QuestInit, List<Quest>> factory)
    {
        QuestInit questInit = new QuestInit();

        List<Quest> quests = factory(questInit);
        List<int> phases = quests.Select(q => q.Phase).Distinct().OrderBy(p => p).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(quests, Has.Count.EqualTo(16));
            Assert.That(phases.First(), Is.EqualTo(1));
            Assert.That(phases.Last(), Is.EqualTo(16));
        });
    }

    private static IEnumerable<Func<QuestInit, List<Quest>>> QuestFactories()
    {
        yield return q => q.GetQuestsCons1();
        yield return q => q.GetQuestsCons2();
        yield return q => q.GetQuestsCons3();
    }
}