namespace CSharpCourse.Framework.Assignments;

// Abstract base that enforces the contract students must fulfil.
public abstract class BankAccountBase
{
    public abstract void Deposit(decimal amount);
    public abstract void Withdraw(decimal amount);
    public abstract decimal GetBalance();
    public abstract override string ToString();
}
