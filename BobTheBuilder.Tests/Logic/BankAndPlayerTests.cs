
namespace BobTheBuilder.Tests.Logic;

public class BankTests
{
    [Test]
    public void AddMoney_ShouldIncreaseBalance()
    {
        Bank bank = new Bank("Bank", "desc");

        bank.AddMoney(500);

        Assert.That(bank.GetBalance(), Is.EqualTo(3500));
    }

    [Test]
    public void TakeMoney_ShouldFailWhenInsufficientFunds()
    {
        var bank = new Bank("Bank", "desc");

        var success = bank.takeMoney(5000);

        Assert.Multiple(() =>
        {
            Assert.That(success, Is.False);
            Assert.That(bank.GetBalance(), Is.EqualTo(3000));
        });
    }

    [Test]
    public void TakeLoan_ShouldApplyInterestAndRepayment()
    {
        Bank bank = new Bank("Bank", "desc");

        bool success = bank.TakeLoan(800);

        Assert.Multiple(() =>
        {
            Assert.That(success, Is.True);
            Assert.That(bank.GetBalance(), Is.EqualTo(3800));
            Assert.That(bank.totalDebt, Is.EqualTo(820));
            Assert.That(bank.monthlyRepayment, Is.EqualTo(820.0 / 3).Within(0.01));
        });
    }

    [Test]
    public void CalculateRepayment_ShouldReduceDebtAndBalance()
    {
        Bank bank = new Bank("Bank", "desc");
        bank.TakeLoan(2000);

        bank.CalculateRepayment();

        Assert.Multiple(() =>
        {
            Assert.That(bank.totalDebt, Is.EqualTo(1750));
            Assert.That(bank.GetBalance(), Is.EqualTo(4650));
        });
    }
}

public class PlayerTests
{
    [Test]
    public void BuyItem_ShouldApplyDiscountToMaterialsShop()
    {
        var player = new Player();
        var bank = new Bank("Bank", "desc");
        var materialsShop = new Shop("Bob's Materials", "desc");
        var wood = new Material("Wood", "desc", 0.5, 0.5, 100);
        materialsShop.AddContents(wood);
        var coupon = new Item("Coupon", "desc", 10, "Wood", 0.5);

        var success = player.BuyItem(coupon, bank, materialsShop);

        Assert.Multiple(() =>
        {
            Assert.That(success, Is.True);
            Assert.That(bank.GetBalance(), Is.EqualTo(2990));
            Assert.That(materialsShop.Inventory["Wood"].Price, Is.EqualTo(50));
            Assert.That(player.Inventory, Does.Contain(coupon));
        });
    }

    [Test]
    public void BuyMaterial_ShouldAddToInventoryAndChargeBank()
    {
        var player = new Player();
        var bank = new Bank("Bank", "desc");
        var material = new Material("Concrete", "desc", 0.6, 0.6, 25);

        var success = player.BuyMaterial(material, bank);

        Assert.Multiple(() =>
        {
            Assert.That(success, Is.True);
            Assert.That(bank.GetBalance(), Is.EqualTo(2975));
            Assert.That(player.Has("Concrete"), Is.True);
        });
    }

    [Test]
    public void Has_ShouldReturnFalseForMissingItems()
    {
        var player = new Player();

        Assert.That(player.Has("Car"), Is.False);
    }
}