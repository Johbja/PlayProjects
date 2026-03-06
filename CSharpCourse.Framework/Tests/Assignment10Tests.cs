using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Framework.Tests;

public class Assignment10Tests<TCircle, TRectangle, TTriangle, TCalculator> : Assignment
    where TCircle : CircleBase
    where TRectangle : RectangleBase
    where TTriangle : TriangleBase
    where TCalculator : ShapeCalculatorBase, new()
{
    // Factory methods: overridden in the student project to call the concrete constructors.
    protected TCircle CreateCircle(double radius) =>
        (TCircle)Activator.CreateInstance(typeof(TCircle), radius)!;
    protected TRectangle CreateRectangle(double width, double height) =>
        (TRectangle)Activator.CreateInstance(typeof(TRectangle), width, height)!;
    protected TTriangle CreateTriangle(double baseLength, double height) =>
        (TTriangle)Activator.CreateInstance(typeof(TTriangle), baseLength, height)!;

    public override int Number => 10;
    public override string Title => "Interfaces & Polymorphism";
    public override string Description =>
        "Implement IShape with Circle, Rectangle, and Triangle; compute total area.";

    public override IEnumerable<TestResult> Validate()
    {
        var g = new Grader();

        var circle = CreateCircle(5);
        g.ExpectApprox("Circle(5).Area() \u2248 \u03c0*25", circle.Area(), Math.PI * 25);
        g.Assert("Circle(5).Describe() mentions radius",
            circle.Describe().Contains("5"),
            $"Got \"{circle.Describe()}\"");

        var rect = CreateRectangle(4, 6);
        g.ExpectApprox("Rectangle(4,6).Area() == 24", rect.Area(), 24);
        g.Assert("Rectangle(4,6).Describe() mentions width and height",
            rect.Describe().Contains("4") && rect.Describe().Contains("6"),
            $"Got \"{rect.Describe()}\"");

        var tri = CreateTriangle(3, 8);
        g.ExpectApprox("Triangle(3,8).Area() == 12", tri.Area(), 12);
        g.Assert("Triangle(3,8).Describe() mentions base and height",
            tri.Describe().Contains("3") && tri.Describe().Contains("8"),
            $"Got \"{tri.Describe()}\"");

        var calc = new TCalculator();
        double expectedTotal = Math.PI + 6 + 10;
        g.ExpectApprox(
            "TotalArea([Circle(1), Rectangle(2,3), Triangle(4,5)]) is correct",
            calc.TotalArea([CreateCircle(1), CreateRectangle(2, 3), CreateTriangle(4, 5)]),
            expectedTotal);

        return g.Results();
    }
}
