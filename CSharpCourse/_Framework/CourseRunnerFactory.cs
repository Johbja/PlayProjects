using CSharpCourse.Assignments;
using CSharpCourse.Framework.Tests;

namespace CSharpCourse._Framework;

public static class CourseRunnerFactory
{
    public static CourseRunner Create() => new CourseRunner(
    [
        new Assignment01Tests<VariablesAndTypes>(),
        new Assignment02Tests<StringInterpolation>(),
        new Assignment03Tests<IfElseConditionals>(),
        new Assignment04Tests<ForLoop>(),
        new Assignment05Tests<WhileLoop>(),
        new Assignment06Tests<Arrays>(),
        new Assignment07Tests<MethodsAndRecursion>(),
        new Assignment08Tests<BankAccount>(),
        new Assignment09Tests<ListsAndLinq>(),
        new Assignment10Tests<Circle, Rectangle, Triangle, ShapeCalculator>(),
    ]);
}
