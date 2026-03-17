using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Assignments;

// ╔══════════════════════════════════════════════════════╗
// ║  ASSIGNMENT 7 — Methods & Recursion                  ║
// ╚══════════════════════════════════════════════════════╝
//
// Implement both methods recursively (no loops).
//  Recursion is when a method calls itself to solve a smaller version of the same problem,
//  until it reaches a base case that can be solved directly.
//
//  Factorial(n)
//    Returns n!  (n factorial).
//    Defined as n! = n * (n-1)! with 0! = 1. i.e.: 5! = 5 * 4 * 3 * 2 * 1 = 120.
//    Expected results: Factorial(0) = 1,  Factorial(5) = 120
//
//  Fibonacci(n)
//    Returns the nth Fibonacci number (0-indexed).
//    Defined as F(n) = F(n-1) + F(n-2) with base cases F(0) = 0 and F(1) = 1.
//    Which would start the sequence by: 0, 1, 1, 2, 3, 5, 8, 13, ...
//      To clarify, look at the 4th number in the sequence, i.e. the number "2" or F(4) = 2 if you will.
//      To get the 4th number, we add the previous two numbers: 1 and 1, which are F(3) and F(2) respectively. So F(4) = F(3) + F(2) --> 1 + 1 = 2.
//    Expected results:
//    Fibonacci(0) = 0,  Fibonacci(1) = 1,  Fibonacci(7) = 13
//
public class MethodsAndRecursion : MethodsAndRecursionBase
{
    private int RecurssionSyntaxExample(int n)
    {
        if (n <= 0)                          
        {
            return 1; //base case
        }
        
        return n * RecurssionSyntaxExample(n - 1); //recursive case, where the method calls itself with a smaller value of n until it reaches the base case.
    }


    public override long Factorial(int n)
    {
        // TODO: return n! recursively
        return 0;
    }

    public override long Fibonacci(int n)
    {
        // TODO: return the nth Fibonacci number recursively
        return 0;
    }
}
