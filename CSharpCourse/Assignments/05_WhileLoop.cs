using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Assignments;

// ╔══════════════════════════════════════════════════════╗
// ║  ASSIGNMENT 5 — While Loop                           ║
// ╚══════════════════════════════════════════════════════╝
//
// Implement both methods using a while loop.
//
//  CollatzSteps(n)
//    Counts how many steps the Collatz sequence takes to
//    reach 1 starting from n.
//    The Collatz sequence is defined as follows:
//      • even ? n = n / 2
//      • odd  ? n = n * 3 + 1
//    Count each step until n == 1 (do not count the start).
//    Example: CollatzSteps(6) → 8
//
//    For example, starting with n = 6, as the above example, the sequence is:
//    iteration 1: n = 6 → 6 (n) is even → new value for n is: n = n/2 → n = 6/2 = 3
//    iteration 2: n = 3 → 3 (n) is odd → new value for n is: n = n*3+1 → n = 3*3+1 = 10
//    iteration 3: n = 10 → 10 (n) is even → new value for n is: n = n/2 → n = 10/2 = 5
//    iteration 4: n = 5 → 5 (n) is odd → new value for n is: n = n*3+1 → n = 5*3+1 = 16
//    iteration 5: n = 16 → 16 (n) is even → new value for n is: n = n/2 → n = 16/2 = 8
//    iteration 6: n = 8 → 8 (n) is even → new value for n is: n = n/2 → n = 8/2 = 4
//    iteration 7: n = 4 → 4 (n) is even → new value for n is: n = n/2 → n = 4/2 = 2
//    iteration 8: n = 2 → 2 (n) is even → new value for n is: n = n/2 → n = 2/2 = 1
//    At this point, n == 1, so we stop counting steps. We had 8 iterations, so we
//    return the number of iterations (8) it took to reach 1, not counting the start value (6).
//
//  ReverseString(s)
//    Returns the string reversed using a while loop
//    (not LINQ or Array.Reverse).
//    Example: ReverseString("hello") → "olleh"
//
public class WhileLoop : WhileLoopBase
{
    public override int CollatzSteps(int n)
    {
        // TODO: return number of steps to reach 1
        return 0;
    }

    public override string ReverseString(string s)
    {
        // TODO: return the reversed string using a while loop
        return "";
    }
}
