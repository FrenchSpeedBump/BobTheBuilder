namespace BobTheBuilder
{
    public class ConstructionBuilding : Shop
    {
        List<Quest> quests = new List<Quest>();
        public List<Quest> currentQuests = new List<Quest>();

        public ConstructionBuilding(string shortDesc, string longDesc, List<Quest> quests) : base(shortDesc, longDesc)
        {
            this.quests = quests;
        }
        
        public List<Quest> GetQuests()
        {
            return quests;
        }

        public List<Quest> GetQuestByPhase(int phase)
        {
            currentQuests.Clear();

            foreach (Quest quest in quests)
            {
                if (quest.Phase == phase)
                {
                    currentQuests.Add(quest);
                }
            }
            return new List<Quest>(currentQuests);
        }
        public bool AcceptQuest(int questId, int phase, Player player)
        {
            currentQuests.Clear();

            foreach (Quest quest in quests)
            {
                if (quest.Phase == phase)
                {
                    currentQuests.Add(quest);
                }
            }
            if (questId < 0 || questId >= currentQuests.Count)
            {
                return false;
            }
            Quest myQuest = currentQuests[questId];
            if (myQuest.CheckRequirements(player.Inventory))
            {
                myQuest.IsCompleted = true;
            }

            return myQuest.CheckRequirements(player.Inventory);

        }
        public void QuestItemRemover(int questId, Player player)
        {
            Quest myQuest = currentQuests[questId];
            foreach (Material req in myQuest.Requirements)
            {
                foreach (ShopInventoryContents item in player.Inventory)
                {
                    if (item.Name == req.Name)
                    {
                        player.RemoveItem(item);
                        break;
                    }
                }
            }
        }
        public void MoneyDeduction(int questId, Bank bank)
        {
            Quest myQuest = currentQuests[questId];
            bank.accountBalance -= myQuest.Price;
        }
        public Quest GetQuestInfo(int questId, int phase)
        {
            Quest myQuest = currentQuests[questId];
            return myQuest;
        }
    }
}
