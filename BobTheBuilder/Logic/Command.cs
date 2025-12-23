namespace BobTheBuilder
{
    public class Command
    {
        public string Name { get; } 
        public string? SecondWord { get; }
        public string? ThirdWord { get; }
        public string? FourthWord { get; }
        public Command(string name, string? secondWord = null, string? thirdWord = null, string? fourthWord = null)
        {
            Name = name;
            SecondWord = secondWord;
            ThirdWord = thirdWord;
            FourthWord = fourthWord;
        }
    }
}
