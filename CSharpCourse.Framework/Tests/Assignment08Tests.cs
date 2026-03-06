using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Framework.Tests;

public class Assignment08Tests<TAccount> : Assignment
    where TAccount : BankAccountBase
{
    // Factory method: overridden in the student project to call new BankAccount(owner).
    protected TAccount CreateAccount(string owner) =>
        (TAccount)Activator.CreateInstance(typeof(TAccount), owner)!;

    public override int Number => 8;
    public override string Title => "Classes & Objects";
    public override string Description =>
        "Implement a BankAccount class with Deposit, Withdraw, and ToString.";

    public override IEnumerable<TestResult> Validate()
    {
        var g = new Grader();
        var account = CreateAccount("Alice");

        g.Expect("Initial balance is 0", account.GetBalance(), 0m);

        account.Deposit(100m);
        g.Expect("After Deposit(100) balance is 100", account.GetBalance(), 100m);

        account.Withdraw(40m);
        g.Expect("After Withdraw(40) balance is 60", account.GetBalance(), 60m);

        account.Withdraw(200m);
        g.Expect("Withdraw more than balance is ignored", account.GetBalance(), 60m);

        account.Deposit(-10m);
        g.Expect("Negative Deposit is ignored", account.GetBalance(), 60m);

        g.Assert("ToString() contains owner and balance",
            account.ToString().StartsWith("Alice") && account.ToString().Contains("60"),
            $"Expected something like \"Alice: $60\", got \"{account.ToString()}\"");

        return g.Results();
    }
}
