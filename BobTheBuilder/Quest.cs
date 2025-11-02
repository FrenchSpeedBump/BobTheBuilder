namespace BobTheBuilder
{
    /// <summary>
    /// Represents a single subquest in the house-building game.
    /// Each quest is part of a chapter (Foundation, Walls, or Roof).
    /// </summary>
    public class Quest
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public QuestChapter Chapter { get; }
        public Dictionary<string, int> BaseMaterials { get; } // Base material requirements for this quest
        public double BaseServiceCost { get; } // Base service cost before company modifiers
        public bool IsCompleted { get; set; }
        public QuestOption? ChosenOption { get; set; } // The company offer the player selected

        public Quest(string id, string name, string description, QuestChapter chapter, 
                     Dictionary<string, int> baseMaterials, double baseServiceCost)
        {
            Id = id;
            Name = name;
            Description = description;
            Chapter = chapter;
            BaseMaterials = baseMaterials;
            BaseServiceCost = baseServiceCost;
            IsCompleted = false;
            ChosenOption = null;
        }

        /// <summary>
        /// Mark this quest as completed with the chosen company option.
        /// </summary>
        public void Complete(QuestOption chosenOption)
        {
            IsCompleted = true;
            ChosenOption = chosenOption;
        }
    }

    /// <summary>
    /// The three main chapters of house construction.
    /// Each chapter contains multiple subquests.
    /// </summary>
    public enum QuestChapter
    {
        Foundation,
        Walls,
        Roof
    }
}
