namespace BobTheBuilder
{
    public class Statistics
    {
        private List<double> sustainabilityScores = new();
        private List<double> qualityScores = new();
        private double totalMoneySpent = 0;
        private int questsCompleted = 0;
    
        // Called when a quest is completed
        public void RecordQuestCompletion(Quest quest)
        {
            sustainabilityScores.Add(GetQuestSustainability(quest));
            qualityScores.Add(GetQuestQuality(quest));
            totalMoneySpent += GetQuestTotalPrice(quest);
            questsCompleted++;
        }
    
        // Getters for calculated stats
        public double GetAverageSustainability()
        {
            if (questsCompleted > 0 && sustainabilityScores.Count > 0)
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
                scores.Add(material.Sustainability);
            }
            return scores.Average();
        }
        public double GetQuestQuality(Quest quest)
        {
            List<double> scores = new();
            foreach(Material material in quest.requirements)
            {
                scores.Add(material.Quality);
            }
            return scores.Average();
        }
        public double GetQuestTotalPrice(Quest quest)
        {
            List<double> prices = new();
            foreach(Material material in quest.requirements)
            {
                prices.Add(material.Price);
            }
            return prices.Sum()+quest.price;
        }
    }
}