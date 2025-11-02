namespace BobTheBuilder
{
    /// <summary>
    /// Bank system for managing player loans and interest.
    /// </summary>
    public static class Bank
    {
        // Interest rate per turn (5% = 0.05)
        public const double InterestRate = 0.05;
        
        // Minimum loan amount
        public const double MinLoanAmount = 50;
        
        // Maximum total loan (credit limit)
        public const double MaxTotalLoan = 1000;

        /// <summary>
        /// Calculate interest on the current loan.
        /// </summary>
        /// <param name="currentLoan">Current loan balance</param>
        /// <returns>Interest amount to add</returns>
        public static double CalculateInterest(double currentLoan)
        {
            if (currentLoan <= 0)
                return 0;
            
            return Math.Round(currentLoan * InterestRate, 2);
        }

        /// <summary>
        /// Check if player can take out additional loan.
        /// </summary>
        /// <param name="currentLoan">Current loan balance</param>
        /// <param name="requestedAmount">Amount player wants to borrow</param>
        /// <returns>True if loan is allowed</returns>
        public static bool CanTakeLoan(double currentLoan, double requestedAmount)
        {
            if (requestedAmount < MinLoanAmount)
                return false;
            
            if (currentLoan + requestedAmount > MaxTotalLoan)
                return false;
            
            return true;
        }

        /// <summary>
        /// Get available credit remaining.
        /// </summary>
        public static double GetAvailableCredit(double currentLoan)
        {
            return Math.Max(0, MaxTotalLoan - currentLoan);
        }
    }
}
