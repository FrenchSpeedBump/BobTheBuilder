namespace BobTheBuilder
{
    public class Bank : Room
    {
        public double accountBalance;
        public double totalDebt;
        public double monthlyRepayment;
        public char currency='$';

        public Bank(string shortDesc,string longDesc)
            : base(shortDesc, longDesc)
        {
            accountBalance = 50000;//here set initial money amount
            totalDebt = 0;
            monthlyRepayment = 0;
        }
        public void addMoney(double amount)
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
        public double getBalance() 
        {
            return accountBalance;
        }
        public double getTotalDebt() 
        { 
            return totalDebt;
        }
        public bool takeLoan(double amount)
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
        public double getMonthlyRepayment()
        {
            return monthlyRepayment;
        }
        public void calculateRepayment()
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
