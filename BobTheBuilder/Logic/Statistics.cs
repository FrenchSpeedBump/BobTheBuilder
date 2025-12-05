namespace BobTheBuilder
{
    public class Statistics
    {
        private List<double> sustainabilityScores = new();
        private List<int> qualityScores = new();
        private double totalMoneySpent = 0;
        private int questsCompleted = 0;
    
        // Called when a quest is completed
        public void RecordQuestCompletion(Quest quest)
        {
            sustainabilityScores.Add(GetQuestSustainability(quest));
            //qualityScores.Add(qualityScore);
            //totalMoneySpent += cost;
            questsCompleted++;
        }
    
        // Getters for calculated stats
        public double GetAverageSustainability()
        {
            if (questsCompleted > 0)
            {
                return sustainabilityScores.Average();
            }
            else
            {
                return 0;
            }
        }
    
        public double GetAverageQuality()
        {
            if (questsCompleted > 0)
            {
                return qualityScores.Average();
            }
            else
            {
                return 0;
            }
        }
    
        public double GetTotalMoneySpent() => totalMoneySpent;
        
        public int GetQuestsCompleted() => questsCompleted;

        public double GetQuestSustainability(Quest quest)
        {
            List<double> scores = new();
            foreach(Material material in quest.requirements)
            {
                scores.Add(material.sustainability);
            }
            return scores.Average();
        }
    }
}