using BDDDemo.Library.Models;
using Reqnroll;

namespace BDDDemo.Tests.StepDefinitions;

[Binding]
public class BankAccountSteps
{
    private ScenarioContext _scenarioContext;
    private bool _withdrawSuccess;
    private string? _withdrawMessage;

    public BankAccountSteps(ScenarioContext context)
    {
        _scenarioContext = context;
    }

    [When(@"I deposit \$(.*)")]
    public void WhenIDeposit(decimal amount)
    {
        var account = _scenarioContext.Get<BankAccount>("BankAccount");
        account?.Deposit(amount);
    }

    [When(@"I (?:attempt to )?withdraw \$(.*)")]
    public void WhenIAttemptToWithdraw(decimal amount)
    {
        var account = _scenarioContext.Get<BankAccount>("BankAccount")
            ?? throw new InvalidOperationException("Bank account has not been initialized.");

        (_withdrawSuccess, _withdrawMessage) = account.TryWithdraw(amount);
    }

    [When(@"I perform the following transactions:")]
    public void WhenIPerformTheFollowingTransactions(Table table)
    {
        var account = _scenarioContext.Get<BankAccount>("BankAccount")
            ?? throw new InvalidOperationException("Bank account has not been initialized.");

        foreach (var row in table.Rows)
        {
            var type = row["Type"];
            var amount = decimal.Parse(row["Amount"]);

            switch(row["Type"].ToLower())
            {
                case "deposit":
                    account.Deposit(amount);
                    break;
                case "withdraw":
                    // the test currently does not care about the result of each withdrawal
                    account.TryWithdraw(amount);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown transaction type: {type}");
            }
        }
    }

    [Then(@"my account balance should be \$(.*)")]
    public void ThenMyAccountBalanceShouldBe(decimal expectedBalance)
    {
        var account = _scenarioContext.Get<BankAccount>("BankAccount");

        Assert.NotNull(account);
        Assert.Equal(expectedBalance, account.Balance);
    }

    [Then(@"my account balance should remain \$(.*)")]
    public void ThenMyAccountBalanceShouldRemain(decimal expectedBalance)
    {
        var account = _scenarioContext.Get<BankAccount>("BankAccount");

        Assert.NotNull(account);
        Assert.Equal(expectedBalance, account.Balance);
    }

    [Then(@"I should see an error ""(.*)""")]
    public void ThenIShouldSeeAnError(string expectedError)
    {
        var account = _scenarioContext.Get<BankAccount>("BankAccount");

        Assert.NotNull(account);
        Assert.False(_withdrawSuccess);
        Assert.Equal(expectedError, _withdrawMessage);
    }
}