using System; //why do we need these?
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
            if(amount < 1000)
            {
                totalDebt += amount + amount*0.025;
                monthlyRepayment = totalDebt / 2;
                accountBalance += amount;
            }
            else if(amount <5000)
            {
                totalDebt += amount + amount * 0.05;
                monthlyRepayment = totalDebt / 4;
                accountBalance += amount;
            }
            else if (amount <= 8000)
            {
                totalDebt += amount + amount * 0.08;
                monthlyRepayment = totalDebt / 6;
                accountBalance += amount;
            }
            else
            {
                Console.WriteLine("This is too big of an ammount.");
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
        public void AddItem(ShopInventoryContents contents)
        {
            Player.Inventory.Add(contents);
        }

        public void RemoveItem(ShopInventoryContents contents)
        {
            Player.Inventory.Remove(contents);
        }
        public void BuyItem(ShopInventoryContents contents)
        {
            if (accountBalance >= contents.Price)
            {
                accountBalance -= contents.Price;
                AddItem(contents);
                Console.WriteLine($"Bought {contents.Name} for {contents.Price}.");
            }
            else
            {
                Console.WriteLine("Not enough money.");
            }
        }
    }
}
