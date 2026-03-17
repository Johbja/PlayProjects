using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Assignments;

// ╔══════════════════════════════════════════════════════╗
// ║  ASSIGNMENT 8 — Classes & Objects                    ║
// ╚══════════════════════════════════════════════════════╝
//
// Implement the BankAccount class so that:
//
//  • The constructor accepts an owner name (string) and
//    sets the starting balance to 0.
//  • Deposit(amount)  adds amount to the balance.
//                     Ignore the call if amount <= 0.
//  • Withdraw(amount) subtracts amount from the balance.
//                     Ignore if amount <= 0 or balance < amount.
//  • GetBalance()     returns the current balance.
//  • ToString()       returns "<owner>: $<balance>"
//                     e.g. "Alice: $42.50"
//
public class BankAccount : BankAccountBase
{
    // TODO: add fields for owner and balance
    // You can add whichever variable fields you want. However variable fields should be something useful to agument the BackAccount class with data you need to store and modify.
    // All fields are bound to the instance of the object of the class
    // meaning that each BankAccount object you create will have its own separate values for these fields.
    // For example, you might create a field like:
    // private string owner;
    // which would store the name of the account owner for each BankAccount instance.
    // You would then initialize this field in the constructor.


    /// <summary>
    /// This is the class constructor, which is called when you create a new instance of BankAccount, 
    /// i.e. use the syntax "new BankAccount("Alice")". It initializes the account with the owner's name and sets the balance to 0.
    /// everything inside the constructor body (between the curly braces) is executed when a new BankAccount object is created, 
    /// allowing you to set up the initial state of the account.
    /// </summary>
    /// <param name="owner">The name of the account owner.</param>
    public BankAccount(string owner)
    {
        // TODO: store owner, set balance to 0
    }

    public override void Deposit(decimal amount)
    {
        // TODO: add amount to balance (ignore if amount <= 0)
    }

    public override void Withdraw(decimal amount)
    {
        // TODO: subtract amount (ignore if amount <= 0 or balance < amount)
    }

    public override decimal GetBalance()
    {
        // TODO: return the current balance
        return 0m;
    }

    public override string ToString()
    {
        // TODO: return "<owner>: $<balance>"
        return "";
    }
}
