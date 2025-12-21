
using BobTheBuilder.Tests.Helpers;

namespace BobTheBuilder.Tests.UI;

public class MinimapUiTests
{
    [Test]
    public void DisplayMinimap_ShouldHighlightCurrentRoom()
    {
        var minimap = new Minimap();
        var house = new Room("House", "desc") { discovered = true };
        var bank = new Room("Bank", "desc") { discovered = true };

        minimap.roomPositions[house] = (0, 0);
        minimap.roomPositions[bank] = (1, 0);

        var output = ConsoleTestHelper.CaptureOutput(() => MinimapUI.DisplayMinimap(minimap, house, minimap.roomPositions));

        Assert.Multiple(() =>
        {
            Assert.That(output, Does.Contain("[Hou]"));
            Assert.That(output, Does.Contain(" Ban "));
        });
    }
}