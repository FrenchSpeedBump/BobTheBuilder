namespace BobTheBuilder
{
    public class Bank : Room
    {
        private double accountBalance;
        public double totalDebt;
        public double monthlyRepayment;
        public char currency='$';

        public Bank(string shortDesc,string longDesc)
            : base(shortDesc, longDesc)
        {
            accountBalance = 3000;//here set initial money amount - forces strategic planning
            totalDebt = 0;
            monthlyRepayment = 0;
        }
        public void AddMoney(double amount)
        {
            accountBalance += amount;
        }
        public bool takeMoney(double amount)
        {
            if (amount < 0 || amount>accountBalance) 
            {
                return false;//card declined
            }
            accountBalance -= amount;
            return true;
        }
        public double GetBalance() 
        {
            return accountBalance;
        }
        public double GetTotalDebt() 
        { 
            return totalDebt;
        }
        public bool TakeLoan(double amount)
        {
            if(amount < 1000)
            {
                totalDebt += amount + amount*0.025;
                monthlyRepayment = totalDebt / 2;
                accountBalance += amount;
                return true;
            }
            else if(amount <5000)
            {
                totalDebt += amount + amount * 0.05;
                monthlyRepayment = totalDebt / 4;
                accountBalance += amount;
                return true;
            }
            else if (amount <= 8000)
            {
                totalDebt += amount + amount * 0.08;
                monthlyRepayment = totalDebt / 6;
                accountBalance += amount;
                return true;
            }
            else
            {
                return false;
            }

        }
        public double GetMonthlyRepayment()
        {
            return monthlyRepayment;
        }
        public void CalculateRepayment()
        {
            if (totalDebt == 0)
            {
                monthlyRepayment = 0;
            }
            else
            {
                accountBalance -= monthlyRepayment;
                totalDebt -= monthlyRepayment;
            }

        }
    }
}
