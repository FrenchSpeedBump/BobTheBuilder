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

        public void GetQuestByPhase(int phase)
        {
            currentQuests.Clear();

            foreach (Quest quest in quests)
            {
                if (quest.phase == phase)
                {
                    currentQuests.Add(quest);
                    Console.WriteLine(quest.shortDescription+ " " + quest.longDescription+ " "+ quest.price);
                    Console.WriteLine("Requirements:");
                    if (quest.requirements.Count > 0)
                    {
                        foreach (Material req in quest.requirements)
                        {
                            Console.Write(req.Name + " ");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("No requirements.");
                    }
                }
            }
        }

        public bool AcceptQuest(int questId, int phase, Player player)
        {
            currentQuests.Clear();
            foreach (Quest quest in quests) // Ensures that only quests matching the current phase are considered, without this you couldn't accept quest before looking
            {
                if (quest.phase == phase)
                {
                    currentQuests.Add(quest);
                }
            }
            if (questId < 0 || questId >= currentQuests.Count)
            {
                Console.WriteLine("Invalid quest ID.");
                return false;
            }
            Quest myQuest = currentQuests[questId];
            if (myQuest.checkRequirements(player.Inventory))
            {
                myQuest.isCompleted = true;
            }

            return myQuest.checkRequirements(player.Inventory);

        }
        public void QuestItemRemover(int questId, Player player)
        {
            Quest myQuest = currentQuests[questId];
            foreach (Material req in myQuest.requirements)
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
            bank.accountBalance -= myQuest.price;
        }
        public Quest GetQuestInfo(int questId, int phase)
        {
            foreach (Quest quest in quests)
            {
                if (quest.phase == phase)
                {
                    currentQuests.Add(quest);
                }
            }
            Quest myQuest = currentQuests[questId];
            return myQuest;
        }
    }
}
