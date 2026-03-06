using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Framework.Tests;

public class Assignment01Tests<TWork> : Assignment<TWork>
    where TWork : VariablesAndTypesBase, new()
{
    public override int Number => 1;
    public override string Title => "Variables & Types";
    public override string Description =>
        "Declare variables of type string, int, double and bool with valid values.";

    public override IEnumerable<TestResult> Validate()
    {
        var g = new Grader();
        var w = Work;

        g.Assert("studentName is not empty",
            !string.IsNullOrWhiteSpace(w.studentName),
            "studentName must not be empty or whitespace");

        g.Assert("studentAge is positive",
            w.studentAge > 0,
            $"Expected age > 0, got {w.studentAge}");

        g.Assert("gpa is in valid range (0.0 \u2013 4.0)",
            w.gpa > 0.0 && w.gpa <= 4.0,
            $"Expected 0.0 < gpa <= 4.0, got {w.gpa}");

        g.Assert("isEnrolled is true",
            w.isEnrolled,
            "isEnrolled must be set to true");

        return g.Results();
    }
}
