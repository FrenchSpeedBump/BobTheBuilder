using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobTheBuilder
{
    public class Bank : Room
    {
        double accountBalance;
        double totalDebt;
        double monthlyRepayment;

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
        public void takeLoan(double amount)
        {
            accountBalance += amount;
            totalDebt += amount;

        }
    }
}
