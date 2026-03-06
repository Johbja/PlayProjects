using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Framework.Tests;

public class Assignment02Tests<TWork> : Assignment<TWork>
    where TWork : StringInterpolationBase, new()
{
    public override int Number => 2;
    public override string Title => "String Interpolation";
    public override string Description =>
        "Use $\"...\" string interpolation to compose sentences from variables.";

    public override IEnumerable<TestResult> Validate()
    {
        var g = new Grader();
        var w = Work;

        g.Assert("city is set",
            !string.IsNullOrWhiteSpace(w.city),
            "city must not be empty");

        g.Assert("year is a plausible value",
            w.year > 1900 && w.year <= DateTime.Now.Year,
            $"Expected a year between 1901 and {DateTime.Now.Year}, got {w.year}");

        g.Assert("introduction contains name and age",
            w.introduction.Contains(w.name) && w.introduction.Contains(w.age.ToString()),
            $"Expected introduction to contain \"{w.name}\" and \"{w.age}\", got: \"{w.introduction}\"");

        g.Assert("location contains city and year",
            w.location.Contains(w.city) && w.location.Contains(w.year.ToString()),
            $"Expected location to contain \"{w.city}\" and \"{w.year}\", got: \"{w.location}\"");

        return g.Results();
    }
}
