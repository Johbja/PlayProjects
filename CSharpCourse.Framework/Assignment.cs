namespace CSharpCourse;

// Base class for all assignments.
// TWork is the student's work class for this assignment.
public abstract class Assignment
{
    public abstract int Number { get; }
    public abstract string Title { get; }
    public abstract string Description { get; }
    public abstract IEnumerable<TestResult> Validate();
}

public abstract class Assignment<TWork> : Assignment where TWork : new()
{
    // The framework instantiates the student's work automatically.
    protected TWork Work { get; } = new TWork();
}
