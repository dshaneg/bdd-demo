using BDDDemo.Library.Models;
using Reqnroll;

namespace BDDDemo.Tests.StepDefinitions;

[Binding]
public class SharedSteps
{
    private readonly ScenarioContext _scenarioContext;

    public SharedSteps(ScenarioContext context)
    {
        _scenarioContext = context;
    }

    [Given(@"I have a bank account with a balance of \$(.*)")]
    public void GivenIHaveABankAccountWithABalanceOf(decimal initialBalance)
    {
        _scenarioContext.Set(new BankAccount(initialBalance), "BankAccount");
    }
}