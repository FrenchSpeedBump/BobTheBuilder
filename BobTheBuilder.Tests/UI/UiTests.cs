
using BobTheBuilder.Presentation.UI;
using BobTheBuilder.Tests.Helpers;

namespace BobTheBuilder.Tests.UI;

public class HouseUITests
{
    [Test]
    public void SetFoundation_ShouldReturnAsciiForConcrete()
    {
        string ascii = HouseUI.SetFoundation(2);

        Assert.That(ascii, Does.Contain("████"));
    }

    [Test]
    public void SetWalls_ShouldReturnAsciiForWood()
    {
        string ascii = HouseUI.SetWalls(1);

        Assert.That(ascii, Does.Contain("╔═╦═╗"));
    }

    [Test]
    public void SetRoof_ShouldReturnAsciiForTyle()
    {
        string ascii = HouseUI.SetRoof(3);

        Assert.That(ascii, Does.Contain("nununun"));
    }
}

public class GameUITests
{
    [Test]
    public void PrintWelcome_ShouldIncludeTitle()
    {
        var output = ConsoleTestHelper.CaptureOutput(GameUI.PrintWelcome);

        Assert.That(output, Does.Contain("WELCOME TO BOB THE BUILDER"));
    }

    [Test]
    public void PrintHelp_ShouldListNavigationCommand()
    {
        var output = ConsoleTestHelper.CaptureOutput(GameUI.PrintHelp);

        Assert.That(output, Does.Contain("Navigate"));
    }

    [Test]
    public void PrintWelcomeImage_ShouldEmitAsciiArt()
    {
        var output = ConsoleTestHelper.CaptureOutput(GameUI.PrintWelcomeImage);

        Assert.That(output, Does.Contain("⣿"));
    }
}

public class GeneralUiOutputTests
{
    [Test]
    public void ShopUI_ShouldListItemsAndMaterials()
    {
        var shop = new Shop("Bob's Materials", "desc");
        shop.AddContents(new Item("Hammer", "desc", 20));
        shop.AddContents(new Material("Wood", "desc", 0.5, 0.5, 10));

        var output = ConsoleTestHelper.CaptureOutput(() => ShopUI.DisplayInventory(shop));

        Assert.Multiple(() =>
        {
            Assert.That(output, Does.Contain("Hammer"));
            Assert.That(output, Does.Contain("Wood"));
        });
    }

    [Test]
    public void PlayerUI_ShouldListInventoryEntries()
    {
        var player = new Player();
        player.AddItem(new Item("Car", "desc", 2000));

        var output = ConsoleTestHelper.CaptureOutput(() => PlayerUI.DisplayInventory(player));

        Assert.That(output, Does.Contain("Your Inventory"));
    }

    [Test]
    public void StatisticsUI_ShouldPrintDayHeader()
    {
        var statistics = new Statistics();
        var quest = new Quest("Quest", "desc", new List<Material>
        {
            new("Wood", "desc", 0.5, 0.5, 10)
        }, 1, 50);
        statistics.RecordQuestCompletion(quest);

        var output = ConsoleTestHelper.CaptureOutput(() => StatisticsUI.DisplayStats(statistics, 2));

        Assert.That(output, Does.Contain("Day 2"));
    }

    [Test]
    public void ConstructionUI_ShouldDisplayRequirements()
    {
        var quests = new List<Quest>
        {
            new("Quest", "desc", new List<Material>
            {
                new("Wood", "desc", 0.5, 0.5, 10)
            }, 1, 50)
        };

        var output = ConsoleTestHelper.CaptureOutput(() => ConstructionUI.DisplayQuests(quests));

        Assert.That(output, Does.Contain("Materials"));
    }
}