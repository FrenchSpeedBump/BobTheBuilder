
namespace BobTheBuilder


{
    
    public class ConstructionBuilding : Shop
    {

        List<Quest> quests = new List<Quest>();

        List<Quest> currentQuests = new List<Quest>();

        
        public ConstructionBuilding(string shortDesc, string longDesc, List<Quest> quests) : base(shortDesc, longDesc)
        {
            this.quests = quests;
        }
        
        public List<Quest> GetQuests()
        {
            return quests;
        }

        public void GetQuestByPhase(int phase)
        {
            currentQuests.Clear();

            foreach (Quest quest in quests)
            {
                if (quest.phase == phase)
                {
                    currentQuests.Add(quest);
                    Console.WriteLine(quest.shortDescription+ " " + quest.longDescription+ " "+ quest.price);
                }
            }
        }

        public bool AcceptQuest(int questId)
        {
            Quest myQuest = currentQuests[questId];

            return myQuest.checkRequirements(Player.Inventory);

        }
    }
}
