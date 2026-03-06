using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Framework.Tests;

public class Assignment03Tests<TWork> : Assignment<TWork>
    where TWork : IfElseConditionalsBase, new()
{
    public override int Number => 3;
    public override string Title => "If / Else Conditionals";
    public override string Description =>
        "Use if/else to classify a number and to determine leap years.";

    public override IEnumerable<TestResult> Validate()
    {
        var g = new Grader();
        var w = Work;

        (int input, string expected)[] classifyCases =
        [
            (-5, "negative"), (0, "zero"), (7, "positive"), (-1, "negative"), (100, "positive")
        ];

        foreach (var (input, expected) in classifyCases)
            g.Expect($"Classify({input}) == \"{expected}\"", w.Classify(input), expected);

        (int year, bool expected)[] leapCases =
        [
            (2000, true), (1900, false), (2024, true), (2023, false), (400, true), (100, false)
        ];

        foreach (var (year, expected) in leapCases)
            g.Expect($"IsLeapYear({year}) == {expected}", w.IsLeapYear(year), expected);

        return g.Results();
    }
}
