using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobTheBuilder
{
    public class Command
    {
        public string Name { get; }
        public string? SecondWord { get; } // this might be used for future expansions, such as "take apple".
        public string? ThirdWord { get; } // used for entering construction building
        public string? FourthWord { get; } // used for entering construction building

        public Command(string name, string? secondWord = null, string? thirdWord = null, string? fourthWord = null)
        {
            Name = name;
            SecondWord = secondWord;
            ThirdWord = thirdWord;
            FourthWord = fourthWord;
        }
    }
}
