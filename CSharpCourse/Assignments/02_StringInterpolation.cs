using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Assignments;

// ╔══════════════════════════════════════════════════════╗
// ║  ASSIGNMENT 2 — String Interpolation                 ║
// ╚══════════════════════════════════════════════════════╝
//
// Use $"..." string interpolation to build sentences.
//
//  • city          — name of a city            (string)
//  • year          — a year (e.g. 2019)         (int)
//  • introduction  — must read:
//      "My name is <name> and I am <age> years old."
//  • location      — must read:
//      "I live in <city> since <year>."
//
public class StringInterpolation : StringInterpolationBase
{
    public StringInterpolation()
    {
        city         = "";  // TODO: assign a city name
        year         = 0;   // TODO: assign a year (e.g. 2019)
        introduction = "";  // TODO: use $"..." with 'name' and 'age'
        location     = "";  // TODO: use $"..." with 'city' and 'year'
    }
}
