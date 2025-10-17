using BDDDemo.Library.Models;
using BDDDemo.Tests.Contexts;

using Reqnroll;

namespace BDDDemo.Tests.StepDefinitions;

[Binding]
public class SharedSteps
{
    private readonly BankAccountContext _accountContext;

    public SharedSteps(BankAccountContext context)
    {
        _accountContext = context;
    }

    [Given(@"I have a bank account with a balance of \$(.*)")]
    public void GivenIHaveABankAccountWithABalanceOf(decimal initialBalance)
    {
        _accountContext.Account = new BankAccount(initialBalance);
    }
}