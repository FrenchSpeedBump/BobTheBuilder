

namespace BobTheBuilder.Tests.Logic;

public class StatisticsTests
{
    private static Quest CreateQuest()
    {
        var requirements = new List<Material>
        {
            new("Wood", "desc", 0.5, 0.6, 10),
            new("Concrete", "desc", 0.7, 0.8, 20)
        };
        return new Quest("Quest", "desc", requirements, 1, 100);
    }

    [Test]
    public void RecordQuestCompletion_ShouldAggregateStats()
    {
        var stats = new Statistics();
        var quest = CreateQuest();

        stats.RecordQuestCompletion(quest);

        Assert.Multiple(() =>
        {
            Assert.That(stats.GetQuestsCompleted(), Is.EqualTo(1));
            Assert.That(stats.GetAverageSustainability(), Is.EqualTo(0.6));
            Assert.That(stats.GetAverageQuality(), Is.EqualTo(0.7));
            Assert.That(stats.GetTotalMoneySpent(), Is.EqualTo(130));
        });
    }

    [Test]
    public void GetQuestTotals_ShouldReflectRequirementValues()
    {
        var quest = CreateQuest();
        var stats = new Statistics();

        Assert.Multiple(() =>
        {
            Assert.That(stats.GetQuestSustainability(quest), Is.EqualTo(0.6));
            Assert.That(stats.GetQuestQuality(quest), Is.EqualTo(0.7));
            Assert.That(stats.GetQuestTotalPrice(quest), Is.EqualTo(130));
        });
    }
}

public class HouseTests
{
    [Test]
    public void RecordMaterials_ShouldTrackQuestMaterials()
    {
        var house = new House("House", "desc");
        var quest = new Quest("Quest", "desc", new List<Material>
        {
            new("Wood", "desc", 0.5, 0.5, 10)
        }, 1, 50);

        house.RecordMaterials(quest);

        Assert.That(house.UsedMaterials.Count, Is.EqualTo(1));
    }

    [Test]
    public void GetMaterials_ShouldReturnRequirementCopies()
    {
        var quest = new Quest("Quest", "desc", new List<Material>
        {
            new("Wood", "desc", 0.5, 0.5, 10),
            new("Concrete", "desc", 0.6, 0.6, 20)
        }, 1, 50);
        var house = new House("House", "desc");

        var materials = house.GetMaterials(quest);

        Assert.That(materials, Has.Count.EqualTo(2));
    }
}