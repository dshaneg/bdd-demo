using BDDDemo.Library.Models;
using Reqnroll;

namespace BDDDemo.Tests.StepDefinitions;

[Binding]
public class BankAccountSteps
{
    private BankAccount? _account;
    private bool _withdrawSuccess;
    private string? _withdrawMessage;

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

    [When(@"I (?:attempt to )?withdraw \$(.*)")]
    public void WhenIAttemptToWithdraw(decimal amount)
    {
        if (_account == null)
        {
            throw new InvalidOperationException("Bank account has not been initialized.");
        }

        (_withdrawSuccess, _withdrawMessage) = _account.TryWithdraw(amount);
    }

    [Then(@"my account balance should be \$(.*)")]
    public void ThenMyAccountBalanceShouldBe(decimal expectedBalance)
    {
        Assert.NotNull(_account);
        Assert.Equal(expectedBalance, _account.Balance);
    }

    [Then(@"my account balance should remain \$(.*)")]
    public void ThenMyAccountBalanceShouldRemain(decimal expectedBalance)
    {
        Assert.NotNull(_account);
        Assert.Equal(expectedBalance, _account.Balance);
    }

    [Then(@"I should see an error ""(.*)""")]
    public void ThenIShouldSeeAnError(string expectedError)
    {
        Assert.NotNull(_account);
        Assert.False(_withdrawSuccess);
        Assert.Equal(expectedError, _withdrawMessage);
    }
}