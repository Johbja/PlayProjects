namespace CSharpCourse.Framework.Assignments;

// Interface and abstract bases that enforce the contract students must fulfil.
public interface IShape
{
    double Area();
    string Describe();
}

public abstract class ShapeCalculatorBase
{
    public abstract double TotalArea(List<IShape> shapes);
}
