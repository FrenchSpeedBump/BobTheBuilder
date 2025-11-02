namespace BobTheBuilder
{
    /// <summary>
    /// Manages all quests in the game, tracks progress, and handles quest completion.
    /// Contains all 12 hardcoded subquests across 3 chapters: Foundation, Walls, and Roof.
    /// </summary>
    public class QuestManager
    {
        private List<Quest> allQuests;
        private int currentQuestIndex;

        public Quest ActiveQuest => allQuests[currentQuestIndex];
        public bool IsGameComplete => currentQuestIndex >= allQuests.Count;

        public QuestManager()
        {
            allQuests = CreateAllQuests();
            currentQuestIndex = 0;
        }

        /// <summary>
        /// Creates all 12 quests for the game with realistic construction progression.
        /// Quests are organized into 3 chapters with sustainability-focused options.
        /// </summary>
        private List<Quest> CreateAllQuests()
        {
            var quests = new List<Quest>();

            // ═══════════════════════════════════════════════════════════════
            // CHAPTER 1: FOUNDATION (4 subquests)
            // Foundation is critical for disaster resistance and sustainability
            // ═══════════════════════════════════════════════════════════════

            // F1: Site Excavation - Preparing the ground
            quests.Add(new Quest(
                "F1",
                "Foundation - Site Excavation",
                "Clear and excavate the land. Proper excavation prevents future settling issues.",
                QuestChapter.Foundation,
                new Dictionary<string, int>
                {
                    { "Concrete", 2 },
                    { "Wood", 1 }
                },
                100 // Base service cost
            ));

            // F2: Foundation Pouring - The main structural base
            quests.Add(new Quest(
                "F2",
                "Foundation - Concrete Pouring",
                "Pour reinforced concrete foundation. Quality concrete is crucial for house stability.",
                QuestChapter.Foundation,
                new Dictionary<string, int>
                {
                    { "Concrete", 6 },
                    { "Steel", 2 }
                },
                120
            ));

            // F3: Waterproofing - Preventing moisture damage
            quests.Add(new Quest(
                "F3",
                "Foundation - Waterproofing",
                "Apply waterproofing to prevent moisture damage and ensure longevity.",
                QuestChapter.Foundation,
                new Dictionary<string, int>
                {
                    { "Waterproofing", 4 },
                    { "Concrete", 1 }
                },
                90
            ));

            // F4: Foundation Insulation - Energy efficiency starts here
            quests.Add(new Quest(
                "F4",
                "Foundation - Insulation",
                "Install foundation insulation for energy efficiency and sustainable heating.",
                QuestChapter.Foundation,
                new Dictionary<string, int>
                {
                    { "Insulation", 5 },
                    { "Wood", 2 }
                },
                110
            ));

            // ═══════════════════════════════════════════════════════════════
            // CHAPTER 2: WALLS (5 subquests)
            // Walls define structure, insulation, and sustainability of the home
            // ═══════════════════════════════════════════════════════════════

            // W1: Wall Framing - The skeleton of the house
            quests.Add(new Quest(
                "W1",
                "Walls - Framing",
                "Build the structural frame for walls. Use sustainable wood when possible.",
                QuestChapter.Walls,
                new Dictionary<string, int>
                {
                    { "Wood", 8 },
                    { "Steel", 2 }
                },
                130
            ));

            // W2: Wall Construction - Building the actual walls
            quests.Add(new Quest(
                "W2",
                "Walls - Construction",
                "Construct the main walls using bricks or eco-friendly alternatives.",
                QuestChapter.Walls,
                new Dictionary<string, int>
                {
                    { "Bricks", 10 },
                    { "Concrete", 3 },
                    { "Wood", 2 }
                },
                150
            ));

            // W3: Wall Insulation - Energy efficiency in walls
            quests.Add(new Quest(
                "W3",
                "Walls - Insulation",
                "Install wall insulation to reduce energy consumption and improve comfort.",
                QuestChapter.Walls,
                new Dictionary<string, int>
                {
                    { "Insulation", 8 },
                    { "Wood", 3 }
                },
                120
            ));

            // W4: Exterior Finish - Weather protection and aesthetics
            quests.Add(new Quest(
                "W4",
                "Walls - Exterior Finish",
                "Apply exterior finishing for weather protection and sustainability.",
                QuestChapter.Walls,
                new Dictionary<string, int>
                {
                    { "Paint", 6 },
                    { "Waterproofing", 3 },
                    { "Wood", 2 }
                },
                140
            ));

            // W5: Interior Drywall - Internal finishing
            quests.Add(new Quest(
                "W5",
                "Walls - Interior Drywall",
                "Install interior drywall for a finished look and better insulation.",
                QuestChapter.Walls,
                new Dictionary<string, int>
                {
                    { "Drywall", 12 },
                    { "Wood", 2 }
                },
                110
            ));

            // ═══════════════════════════════════════════════════════════════
            // CHAPTER 3: ROOF (3 subquests)
            // Roof protects everything below and impacts energy efficiency
            // ═══════════════════════════════════════════════════════════════

            // R1: Roof Framing - Support structure for the roof
            quests.Add(new Quest(
                "R1",
                "Roof - Framing",
                "Build the roof frame structure. Strong framing protects against storms.",
                QuestChapter.Roof,
                new Dictionary<string, int>
                {
                    { "Wood", 10 },
                    { "Steel", 3 }
                },
                160
            ));

            // R2: Roof Covering - The actual protective layer
            quests.Add(new Quest(
                "R2",
                "Roof - Covering",
                "Install roof tiles or sustainable roofing materials for weather protection.",
                QuestChapter.Roof,
                new Dictionary<string, int>
                {
                    { "RoofTiles", 15 },
                    { "Waterproofing", 4 },
                    { "Wood", 2 }
                },
                180
            ));

            // R3: Roof Insulation & Gutters - Final touches for sustainability
            quests.Add(new Quest(
                "R3",
                "Roof - Insulation & Drainage",
                "Install roof insulation and gutters for energy efficiency and water management.",
                QuestChapter.Roof,
                new Dictionary<string, int>
                {
                    { "Insulation", 6 },
                    { "Steel", 2 },
                    { "Wood", 3 }
                },
                170
            ));

            return quests;
        }

        /// <summary>
        /// Complete the current quest and advance to the next one.
        /// </summary>
        public void CompleteCurrentQuest(QuestOption chosenOption)
        {
            if (!IsGameComplete)
            {
                allQuests[currentQuestIndex].Complete(chosenOption);
                currentQuestIndex++;
            }
        }

        /// <summary>
        /// Check if a chapter has just been completed (useful for triggering disasters).
        /// </summary>
        public bool IsChapterJustCompleted()
        {
            if (currentQuestIndex == 0) return false;
            
            Quest previousQuest = allQuests[currentQuestIndex - 1];
            Quest? currentQuest = IsGameComplete ? null : allQuests[currentQuestIndex];
            
            // Chapter is complete if previous quest was completed and current quest is from different chapter (or game is complete)
            return previousQuest.IsCompleted && 
                   (IsGameComplete || currentQuest!.Chapter != previousQuest.Chapter);
        }

        /// <summary>
        /// Get which chapter was just completed.
        /// </summary>
        public QuestChapter? GetCompletedChapter()
        {
            if (currentQuestIndex == 0) return null;
            return allQuests[currentQuestIndex - 1].Chapter;
        }

        /// <summary>
        /// Get all completed quests.
        /// </summary>
        public List<Quest> GetCompletedQuests()
        {
            return allQuests.Where(q => q.IsCompleted).ToList();
        }

        /// <summary>
        /// Get all quests from a specific chapter.
        /// </summary>
        public List<Quest> GetQuestsInChapter(QuestChapter chapter)
        {
            return allQuests.Where(q => q.Chapter == chapter).ToList();
        }

        /// <summary>
        /// Get total number of quests.
        /// </summary>
        public int TotalQuests => allQuests.Count;

        /// <summary>
        /// Get current progress (number of completed quests).
        /// </summary>
        public int CompletedQuestCount => allQuests.Count(q => q.IsCompleted);
    }
}
