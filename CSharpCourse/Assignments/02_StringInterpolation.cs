using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Assignments;

// ╔══════════════════════════════════════════════════════╗
// ║  ASSIGNMENT 2 — String Interpolation                 ║
// ╚══════════════════════════════════════════════════════╝
//
// Use $"..." string interpolation to build sentences.
//
//  • name          — name of a person          (string)
//  • age           — age of a person           (int)
//  • city          — name of a city            (string)
//  • year          — a year (e.g. 2019)        (int)
//  • introduction  — must read:
//      "My name is <name> and I am <age> years old."
//  • location      — must read:
//      "I live in <city> since <year>."
//
public class StringInterpolation : StringInterpolationBase
{
    /// <summary>
    /// Inherited variable fields from <StringInterpolationBase> 
    /// </summary>
    public int age;
    public int year;
    public string name;
    public string city;
    public string introduction;
    public string location;

    public StringInterpolation()
    {
        name = "";          // TODO: assign a name
        age = 0;            // TODO: assign an age (e.g. 25)
        city= "";           // TODO: assign a city name
        year= 0;            // TODO: assign a year (e.g. 2019)
        introduction = "";  // TODO: use $"..." with 'name' and 'age'
        location= "";       // TODO: use $"..." with 'city' and 'year'
    }
}
