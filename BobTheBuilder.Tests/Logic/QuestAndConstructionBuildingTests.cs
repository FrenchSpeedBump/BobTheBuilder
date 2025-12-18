

namespace BobTheBuilder.Tests.Logic;

public class QuestTests
{
    [Test]
    public void CheckRequirements_ShouldSucceedWhenInventoryMatches()
    {
        var requirements = new List<Material>
        {
            new("Wood", "desc", 0.5, 0.5, 10),
            new("Concrete", "desc", 0.6, 0.6, 15)
        };
        var quest = new Quest("Test", "desc", requirements, 1, 100);
        var inventory = new List<ShopInventoryContents>
        {
            new Material("Wood", "desc", 0.5, 0.5, 10),
            new Material("Concrete", "desc", 0.6, 0.6, 15)
        };

        var result = quest.checkRequirements(inventory);

        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckRequirements_ShouldFailWhenMissingMaterial()
    {
        var requirements = new List<Material> { new("Wood", "desc", 0.5, 0.5, 10) };
        var quest = new Quest("Test", "desc", requirements, 1, 100);
        var inventory = new List<ShopInventoryContents>();

        var result = quest.checkRequirements(inventory);

        Assert.That(result, Is.False);
    }
}

public class ConstructionBuildingTests
{
    private static Quest CreateQuest(int phase, string materialName = "Wood", int price = 100)
    {
        var requirements = new List<Material>
        {
            new(materialName, "desc", 0.5, 0.5, 10)
        };
        return new Quest($"Quest {phase}", "desc", requirements, phase, price);
    }

    [Test]
    public void GetQuestByPhase_ShouldFilterCorrectly()
    {
        var quests = new List<Quest> { CreateQuest(1), CreateQuest(2) };
        var building = new ConstructionBuilding("Best Build", "desc", quests);

        var phaseOne = building.GetQuestByPhase(1);

        Assert.Multiple(() =>
        {
            Assert.That(phaseOne, Has.Count.EqualTo(1));
            Assert.That(phaseOne[0].phase, Is.EqualTo(1));
        });
    }

    [Test]
    public void AcceptQuest_ShouldValidateRequirements()
    {
        var quests = new List<Quest> { CreateQuest(1) };
        var building = new ConstructionBuilding("Best Build", "desc", quests);
        var player = new Player();
        player.AddItem(new Material("Wood", "desc", 0.5, 0.5, 10));

        var accepted = building.AcceptQuest(0, 1, player);

        Assert.Multiple(() =>
        {
            Assert.That(accepted, Is.True);
            Assert.That(quests[0].isCompleted, Is.True);
        });
    }

    [Test]
    public void QuestItemRemover_ShouldConsumePlayerMaterials()
    {
        var quests = new List<Quest> { CreateQuest(1) };
        var building = new ConstructionBuilding("Best Build", "desc", quests);
        var player = new Player();
        player.AddItem(new Material("Wood", "desc", 0.5, 0.5, 10));

        building.AcceptQuest(0, 1, player);
        building.QuestItemRemover(0, player);

        Assert.That(player.Has("Wood"), Is.False);
    }

    [Test]
    public void MoneyDeduction_ShouldSubtractQuestPrice()
    {
        var quests = new List<Quest> { CreateQuest(1, price: 250) };
        var building = new ConstructionBuilding("Best Build", "desc", quests);
        var bank = new Bank("Bank", "desc");
        var player = new Player();
        player.AddItem(new Material("Wood", "desc", 0.5, 0.5, 10));

        building.AcceptQuest(0, 1, player);
        building.MoneyDeduction(0, bank);

        Assert.That(bank.accountBalance, Is.EqualTo(49750));
    }

    [Test]
    public void GetQuestInfo_ShouldReturnQuestFromCurrentList()
    {
        var quests = new List<Quest> { CreateQuest(1) };
        var building = new ConstructionBuilding("Best Build", "desc", quests);
        var player = new Player();
        player.AddItem(new Material("Wood", "desc", 0.5, 0.5, 10));

        building.AcceptQuest(0, 1, player);
        var info = building.GetQuestInfo(0, 1);

        Assert.That(info.shortDescription, Is.EqualTo("Quest 1"));
    }
}