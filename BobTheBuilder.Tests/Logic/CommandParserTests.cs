

namespace BobTheBuilder.Tests.Logic;

public class CommandParserTests
{
    [Test]
    public void Command_ShouldExposeWords()
    {
        var command = new Command("travel", "Bob's", "Materials");

        Assert.Multiple(() =>
        {
            Assert.That(command.Name, Is.EqualTo("travel"));
            Assert.That(command.SecondWord, Is.EqualTo("Bob's"));
            Assert.That(command.ThirdWord, Is.EqualTo("Materials"));
        });
    }

    [Test]
    public void CommandWords_ShouldValidateCommandList()
    {
        var words = new CommandWords();

        Assert.Multiple(() =>
        {
            Assert.That(words.IsValidCommand("north"), Is.True);
            Assert.That(words.IsValidCommand("fly"), Is.False);
        });
    }

    [Test]
    public void Parser_ShouldReturnCommandWithArguments()
    {
        var parser = new Parser();

        var command = parser.GetCommand("travel Bob's Materials");

        Assert.Multiple(() =>
        {
            Assert.That(command, Is.Not.Null);
            Assert.That(command!.SecondWord, Is.EqualTo("Bob's"));
            Assert.That(command.ThirdWord, Is.EqualTo("Materials"));
        });
    }

    [Test]
    public void Parser_ShouldReturnNullForUnknownCommand()
    {
        var parser = new Parser();

        var command = parser.GetCommand("dance wildly");

        Assert.That(command, Is.Null);
    }
}