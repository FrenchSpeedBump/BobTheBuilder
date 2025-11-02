namespace BobTheBuilder
{
    /// <summary>
    /// Tracks player statistics throughout the game for final scoring and ranking.
    /// </summary>
    public class GameStatistics
    {
        // Financial statistics
        public double TotalSpent { get; private set; } = 0;
        public double TotalInterestPaid { get; private set; } = 0;
        public double TotalIncome { get; private set; } = 0;
        
        // Construction quality statistics
        private List<double> sustainabilityScores = new List<double>();
        private List<double> qualityScores = new List<double>();
        
        // Quest tracking
        public List<string> CompanyChoices { get; private set; } = new List<string>();
        public Dictionary<string, int> MaterialsUsed { get; private set; } = new Dictionary<string, int>();
        
        // Chapter timing
        public int FoundationTurns { get; set; } = 0;
        public int WallsTurns { get; set; } = 0;
        public int RoofTurns { get; set; } = 0;
        
        /// <summary>
        /// Record a completed quest's financial impact.
        /// </summary>
        public void RecordQuestCost(double totalCost)
        {
            TotalSpent += totalCost;
        }
        
        /// <summary>
        /// Record a quest's quality ratings.
        /// </summary>
        public void RecordQuestQuality(double sustainability, double quality, string companyName)
        {
            sustainabilityScores.Add(sustainability);
            qualityScores.Add(quality);
            CompanyChoices.Add(companyName);
        }
        
        /// <summary>
        /// Record materials used in a quest.
        /// </summary>
        public void RecordMaterialsUsed(Dictionary<string, int> materials)
        {
            foreach (var material in materials)
            {
                if (MaterialsUsed.ContainsKey(material.Key))
                {
                    MaterialsUsed[material.Key] += material.Value;
                }
                else
                {
                    MaterialsUsed[material.Key] = material.Value;
                }
            }
        }
        
        /// <summary>
        /// Record interest payment.
        /// </summary>
        public void RecordInterest(double interest)
        {
            TotalInterestPaid += interest;
        }
        
        /// <summary>
        /// Record monthly income.
        /// </summary>
        public void RecordIncome(double income)
        {
            TotalIncome += income;
        }
        
        /// <summary>
        /// Calculate average sustainability score.
        /// </summary>
        public double GetAverageSustainability()
        {
            if (sustainabilityScores.Count == 0) return 0;
            return Math.Round(sustainabilityScores.Average(), 2);
        }
        
        /// <summary>
        /// Calculate average quality score.
        /// </summary>
        public double GetAverageQuality()
        {
            if (qualityScores.Count == 0) return 0;
            return Math.Round(qualityScores.Average(), 2);
        }
        
        /// <summary>
        /// Get total turns taken.
        /// </summary>
        public int GetTotalTurns()
        {
            return FoundationTurns + WallsTurns + RoofTurns;
        }
        
        /// <summary>
        /// Calculate overall game score (0-100).
        /// </summary>
        public double CalculateOverallScore()
        {
            // Scoring formula:
            // 40% Sustainability (0-10 → 0-40 points)
            // 40% Quality (0-10 → 0-40 points)
            // 20% Financial efficiency (bonus for staying under budget, penalty for excessive debt)
            
            double sustainabilityPoints = (GetAverageSustainability() / 10.0) * 40.0;
            double qualityPoints = (GetAverageQuality() / 10.0) * 40.0;
            
            // Financial efficiency: Start with 20 points, lose points for high spending
            // Ideal spending ~$1000-1500, penalty for going over $2000
            double financialPoints = 20.0;
            if (TotalSpent > 2000)
            {
                financialPoints -= Math.Min(10, (TotalSpent - 2000) / 100);
            }
            // Bonus for finishing with money left over
            if (TotalSpent < 1500)
            {
                financialPoints += Math.Min(5, (1500 - TotalSpent) / 100);
            }
            
            double totalScore = sustainabilityPoints + qualityPoints + financialPoints;
            return Math.Round(Math.Max(0, Math.Min(100, totalScore)), 1);
        }
        
        /// <summary>
        /// Get rank based on score.
        /// </summary>
        public string GetRank()
        {
            double score = CalculateOverallScore();
            
            if (score >= 90) return "S - Master Builder";
            if (score >= 80) return "A - Excellent Builder";
            if (score >= 70) return "B - Great Builder";
            if (score >= 60) return "C - Good Builder";
            if (score >= 50) return "D - Average Builder";
            return "F - Needs Improvement";
        }
        
        /// <summary>
        /// Display comprehensive statistics.
        /// </summary>
        public void DisplayStatistics(Player player)
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    📊 GAME STATISTICS                      ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            
            // Financial Summary
            Console.WriteLine("║ 💰 FINANCIAL SUMMARY                                       ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ Total Spent:        ${TotalSpent.ToString("F2").PadLeft(36)} ║");
            Console.WriteLine($"║ Total Income:       ${TotalIncome.ToString("F2").PadLeft(36)} ║");
            Console.WriteLine($"║ Interest Paid:      ${TotalInterestPaid.ToString("F2").PadLeft(36)} ║");
            Console.WriteLine($"║ Final Balance:      ${player.Money.ToString("F2").PadLeft(36)} ║");
            Console.WriteLine($"║ Final Loan:         ${player.CurrentLoan.ToString("F2").PadLeft(36)} ║");
            
            // Construction Quality
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 🏗️  CONSTRUCTION QUALITY                                   ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ Sustainability:     {GetAverageSustainability()}/10 {GetStarRating(GetAverageSustainability())}");
            Console.WriteLine($"║ Quality:            {GetAverageQuality()}/10 {GetStarRating(GetAverageQuality())}");
            
            // Company Choices
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 🏢 COMPANY PREFERENCES                                     ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            var companyCounts = CompanyChoices.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            foreach (var company in companyCounts.OrderByDescending(c => c.Value))
            {
                string line = $"║ {company.Key.PadRight(40)} {company.Value.ToString().PadLeft(2)} quests    ║";
                Console.WriteLine(line);
            }
            
            // Time Statistics
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ ⏱️  TIME STATISTICS                                        ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ Foundation Chapter: {FoundationTurns.ToString().PadLeft(2)} turns                            ║");
            Console.WriteLine($"║ Walls Chapter:      {WallsTurns.ToString().PadLeft(2)} turns                            ║");
            Console.WriteLine($"║ Roof Chapter:       {RoofTurns.ToString().PadLeft(2)} turns                            ║");
            Console.WriteLine($"║ Total Time:         {GetTotalTurns().ToString().PadLeft(2)} turns                            ║");
            
            // Final Score
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 🏆 FINAL SCORE                                             ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ Overall Score:      {CalculateOverallScore().ToString("F1").PadLeft(37)}/100 ║");
            Console.WriteLine($"║ Rank:               {GetRank().PadRight(41)}║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
        }
        
        /// <summary>
        /// Helper method to display star ratings.
        /// </summary>
        private string GetStarRating(double rating)
        {
            int stars = (int)Math.Round(rating / 2); // Convert 0-10 to 0-5 stars
            return new string('⭐', stars).PadRight(10) + "║";
        }
    }
}
