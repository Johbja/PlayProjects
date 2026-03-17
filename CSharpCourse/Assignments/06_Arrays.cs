using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Assignments;

// ╔══════════════════════════════════════════════════════╗
// ║  ASSIGNMENT 6 — Arrays                               ║
// ╚══════════════════════════════════════════════════════╝
//
// Implement both methods that work with integer arrays.
//
//  GetMax(arr)
//    Returns the largest value in the array.
//    Use a loop — do NOT use LINQ.
//    Example: GetMax([1, 5, 3, 2]) → 5
//    [Tip]: define an array varaiable with the syntax "int[] arr = new int[5];" to create an array of 5 integers.
//           You can also initialize it with values like "int[] arr = new int[] {1, 2, 3};".
//
//  Rotate(arr)
//    Returns a NEW array where every element has shifted
//    one position to the right and the last element wraps
//    around to index 0.
//    Example: Rotate([1, 2, 3, 4]) → [4, 1, 2, 3]
//
public class Arrays : ArraysBase
{
    public override int GetMax(int[] arr)
    {
        // TODO: return the maximum value using a loop
        return 0;
    }

    public override int[] Rotate(int[] arr)
    {
        // TODO: return a new array rotated one step to the right
        return System.Array.Empty<int>();
    }
}
