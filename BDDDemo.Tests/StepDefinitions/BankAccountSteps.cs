using BDDDemo.Library.Models;
using Xunit;
using Reqnroll;

namespace BDDDemo.Tests.StepDefinitions;

[Binding]
public class BankAccountSteps
{
    private BankAccount? _account;

    [Given(@"I have a bank account with a balance of \$(.*)")]
    public void GivenIHaveABankAccountWithABalanceOf(decimal initialBalance)
    {
        _account = new BankAccount(initialBalance);
    }

    [When(@"I deposit \$(.*)")]
    public void WhenIDeposit(decimal amount)
    {
        _account?.Deposit(amount);
    }

    [Then(@"my account balance should be \$(.*)")]
    public void ThenMyAccountBalanceShouldBe(decimal expectedBalance)
    {
        Assert.NotNull(_account);
        Assert.Equal(expectedBalance, _account.Balance);
    }
}