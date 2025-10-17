namespace BDDDemo.Library.Models;

public class BankAccount
{
    public decimal Balance { get; private set; }

    public BankAccount(decimal initialBalance = 0)
    {
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        Balance -= amount;
    }

    public (bool Success, string? ErrorMessage) TryWithdraw(decimal amount)
    {
        if (amount > Balance)
        {
            return (false, "Insufficient funds");
        }

        Balance -= amount;
        return (true, null);
    }
}