
namespace BobTheBuilder.Tests.Logic;

public class BankTests
{
    [Test]
    public void AddMoney_ShouldIncreaseBalance()
    {
        var bank = new Bank("Bank", "desc");

        bank.addMoney(500);

        Assert.That(bank.accountBalance, Is.EqualTo(50500));
    }

    [Test]
    public void TakeMoney_ShouldFailWhenInsufficientFunds()
    {
        var bank = new Bank("Bank", "desc");

        var success = bank.takeMoney(60000);

        Assert.Multiple(() =>
        {
            Assert.That(success, Is.False);
            Assert.That(bank.accountBalance, Is.EqualTo(50000));
        });
    }

    [Test]
    public void TakeLoan_ShouldApplyInterestAndRepayment()
    {
        var bank = new Bank("Bank", "desc");

        var success = bank.takeLoan(800);

        Assert.Multiple(() =>
        {
            Assert.That(success, Is.True);
            Assert.That(bank.accountBalance, Is.EqualTo(50800));
            Assert.That(bank.totalDebt, Is.EqualTo(820));
            Assert.That(bank.monthlyRepayment, Is.EqualTo(410));
        });
    }

    [Test]
    public void CalculateRepayment_ShouldReduceDebtAndBalance()
    {
        var bank = new Bank("Bank", "desc");
        bank.takeLoan(2000);

        bank.calculateRepayment();

        Assert.Multiple(() =>
        {
            Assert.That(bank.totalDebt, Is.EqualTo(1575));
            Assert.That(bank.accountBalance, Is.EqualTo(51475));
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
            Assert.That(bank.accountBalance, Is.EqualTo(49990));
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
            Assert.That(bank.accountBalance, Is.EqualTo(49975));
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