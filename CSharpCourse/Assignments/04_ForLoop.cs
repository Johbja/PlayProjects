using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Assignments;

// ╔══════════════════════════════════════════════════════╗
// ║  ASSIGNMENT 4 — For Loop                             ║
// ╚══════════════════════════════════════════════════════╝
//
// Implement both methods using a for loop.
//
//  Sum(n)
//    Returns the sum of all integers from 1 up to and
//    including n.
//    Example: Sum(5) → 1+2+3+4+5 = 15
//
//  BuildCountdown(n)
//    Returns a string counting down from n to 1,
//    with numbers separated by ", ".
//    Example: BuildCountdown(5) → "5, 4, 3, 2, 1"
//    [Tip]: use string concatenation (e.g. text += "5") to build the result string in the loop.
//
public class ForLoop : ForLoopBase
{
    public override int Sum(int n)
    {
        // TODO: sum integers 1..n using a for loop
        return 0;
    }

    public override string BuildCountdown(int n)
    {
        // TODO: build "n, n-1, ..., 1" using a for loop
        return "";
    }
}
