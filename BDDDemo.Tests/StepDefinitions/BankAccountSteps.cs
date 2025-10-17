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

    [When(@"I perform the following transactions:")]
    public void WhenIPerformTheFollowingTransactions(Table table)
    {
        if (_account == null)
        {
            throw new InvalidOperationException("Bank account has not been initialized.");
        }

        foreach (var row in table.Rows)
        {
            var type = row["Type"];
            var amount = decimal.Parse(row["Amount"]);

            switch(row["Type"].ToLower())
            {
                case "deposit":
                    _account.Deposit(amount);
                    break;
                case "withdraw":
                    // the test currently does not care about the result of each withdrawal
                    _account.TryWithdraw(amount);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown transaction type: {type}");
            }
        }
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