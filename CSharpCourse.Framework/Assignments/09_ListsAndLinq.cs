namespace CSharpCourse.Framework.Assignments;

// Abstract base that enforces the methods students must implement.
public abstract class ListsAndLinqBase
{
    public abstract List<int> GetEvenNumbers(List<int> numbers);
    public abstract List<int> GetTopThree(List<int> numbers);
    public abstract List<string> GetWordsShorterThan(List<string> words, int maxLength);
}
