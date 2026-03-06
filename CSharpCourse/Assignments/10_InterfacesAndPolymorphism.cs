using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Assignments;

// ????????????????????????????????????????????????????????
// ?  ASSIGNMENT 10 — Interfaces & Polymorphism           ?
// ????????????????????????????????????????????????????????
//
// Implement the three shapes and the TotalArea helper.
//
//  Each shape must implement IShape:
//    double Area()      — area of the shape
//    string Describe()  — human-readable description
//
//  Circle(radius)
//    Area     = ? * r²
//    Describe = "Circle with radius <r>"
//
//  Rectangle(width, height)
//    Area     = width * height
//    Describe = "Rectangle <w> x <h>"
//
//  Triangle(baseLength, height)
//    Area     = 0.5 * base * height
//    Describe = "Triangle with base <b> and height <h>"
//
//  TotalArea(shapes)
//    Returns the sum of Area() for every shape in the list.
//
public class Circle : CircleBase
{
    public Circle(double radius)
    {
        // TODO: store radius
    }

    public override double Area()
    {
        // TODO: return ? * r²
        return 0;
    }

    public override string Describe()
    {
        // TODO: return "Circle with radius <r>"
        return "";
    }
}

public class Rectangle : RectangleBase
{
    public Rectangle(double width, double height)
    {
        // TODO: store width and height
    }

    public override double Area()
    {
        // TODO: return width * height
        return 0;
    }

    public override string Describe()
    {
        // TODO: return "Rectangle <w> x <h>"
        return "";
    }
}

public class Triangle : TriangleBase
{
    public Triangle(double baseLength, double height)
    {
        // TODO: store baseLength and height
    }

    public override double Area()
    {
        // TODO: return 0.5 * base * height
        return 0;
    }

    public override string Describe()
    {
        // TODO: return "Triangle with base <b> and height <h>"
        return "";
    }
}

public class ShapeCalculator : ShapeCalculatorBase
{
    public override double TotalArea(List<IShape> shapes)
    {
        // TODO: return the sum of all shape areas
        return 0;
    }
}
