using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Assignments;

// ╔══════════════════════════════════════════════════════╗
// ║  ASSIGNMENT 3 — If / Else Conditionals               ║
// ╚══════════════════════════════════════════════════════╝
//
// Implement the two methods using if/else logic.
//
//  Classify(n)
//    Returns "negative" if n < 0, "zero" if n == 0,
//    or "positive" if n > 0.
//
//  IsLeapYear(year)
//    Returns true when the year is a leap year.
//    Rule: divisible by 4, EXCEPT centuries unless also by 400.
//    Examples: 2000 → true, 1900 → false, 2024 → true
//    [Tip]: use the modulo operator (%) to check for divisibility.
//
public class IfElseConditionals : IfElseConditionalsBase
{
    public override string Classify(int n)
    {
        // TODO: return "negative", "zero", or "positive"
        return "";
    }

    public override bool IsLeapYear(int year)
    {
        // TODO: return true if the year is a leap year
        return false;
    }
}
