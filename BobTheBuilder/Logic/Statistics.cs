namespace BobTheBuilder
{
    public class Statistics
    {
        private List<double> sustainabilityScores = new List<double>();
        private List<double> qualityScores = new List<double>();
        private double totalMoneySpent = 0;
        private int questsCompleted = 0;
        private int naturalDisasters = 0;
    
        public void RecordQuestCompletion(Quest quest)
        {
            sustainabilityScores.Add(GetQuestSustainability(quest));
            qualityScores.Add(GetQuestQuality(quest));
            totalMoneySpent += GetQuestTotalPrice(quest);
            questsCompleted++;
        }
        public void RecordNaturalDisasterHappening(bool happened)
        {
            if (happened)
            {
                naturalDisasters++;
            }
        }

        public void RecordItemPurchase(double price)
        {
            totalMoneySpent += price;
        }
    
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

        public int GetNaturalDisasters() => naturalDisasters;

        public double GetQuestSustainability(Quest quest)
        {
            List<double> scores = new List<double>();
            foreach(Material material in quest.Requirements)
            {
                scores.Add(material.Sustainability);
            }
            return scores.Average();
        }
        public double GetQuestQuality(Quest quest)
        {
            List<double> scores = new List<double>();
            foreach(Material material in quest.Requirements)
            {
                scores.Add(material.Quality);
            }
            return scores.Average();
        }
        public double GetQuestTotalPrice(Quest quest)
        {
            return quest.Price;
        }
    }
}