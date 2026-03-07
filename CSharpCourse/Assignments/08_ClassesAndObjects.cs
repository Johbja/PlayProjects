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
