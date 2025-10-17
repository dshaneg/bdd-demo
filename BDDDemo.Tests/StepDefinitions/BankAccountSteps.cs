using Reqnroll;
using BDDDemo.Tests.Contexts;

namespace BDDDemo.Tests.StepDefinitions;

[Binding]
public class BankAccountSteps
{
    private BankAccountContext _accountContext;
    private bool _withdrawSuccess;
    private string? _withdrawMessage;

    public BankAccountSteps(BankAccountContext context)
    {
        _accountContext = context;
    }

    [When(@"I deposit \$(.*)")]
    public void WhenIDeposit(decimal amount)
    {
        _accountContext.Account?.Deposit(amount);
    }

    [When(@"I (?:attempt to )?withdraw \$(.*)")]
    public void WhenIAttemptToWithdraw(decimal amount)
    {
        if (_accountContext.Account == null)
        {
            throw new InvalidOperationException("Bank account has not been initialized.");
        }

        (_withdrawSuccess, _withdrawMessage) = _accountContext.Account.TryWithdraw(amount);
    }

    [When(@"I perform the following transactions:")]
    public void WhenIPerformTheFollowingTransactions(Table table)
    {
        if (_accountContext.Account == null)
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
                    _accountContext.Account.Deposit(amount);
                    break;
                case "withdraw":
                    // the test currently does not care about the result of each withdrawal
                    _accountContext.Account.TryWithdraw(amount);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown transaction type: {type}");
            }
        }
    }

    [Then(@"my account balance should be \$(.*)")]
    public void ThenMyAccountBalanceShouldBe(decimal expectedBalance)
    {
        Assert.NotNull(_accountContext.Account);
        Assert.Equal(expectedBalance, _accountContext.Account.Balance);
    }

    [Then(@"my account balance should remain \$(.*)")]
    public void ThenMyAccountBalanceShouldRemain(decimal expectedBalance)
    {
        Assert.NotNull(_accountContext.Account);
        Assert.Equal(expectedBalance, _accountContext.Account.Balance);
    }

    [Then(@"I should see an error ""(.*)""")]
    public void ThenIShouldSeeAnError(string expectedError)
    {
        Assert.NotNull(_accountContext.Account);
        Assert.False(_withdrawSuccess);
        Assert.Equal(expectedError, _withdrawMessage);
    }
}