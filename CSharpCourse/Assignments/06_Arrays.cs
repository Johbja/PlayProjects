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
