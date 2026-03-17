using CSharpCourse.Framework.Assignments;

namespace CSharpCourse.Assignments;

// ╔══════════════════════════════════════════════════════╗
// ║  ASSIGNMENT 1 — Variables & Types                    ║
// ╚══════════════════════════════════════════════════════╝
//
// Declare fields with the correct values.
//
//  • studentName  — your name                  (string)
//  • studentAge   — your age, must be > 0      (int)
//  • gpa          — a GPA between 0.0 and 4.0  (double)
//  • isEnrolled   — must be true               (bool)
//
public class VariablesAndTypes : VariablesAndTypesBase
{
    /// <summary>
    /// Inherited variable fields from <VariablesAndTypesBase> 
    /// </summary>
    public int studentAge;
    public bool isEnrolled;
    public double gpa;
    public string studentName;

    public VariablesAndTypes()
    {
        studentName = "";     // TODO: assign your name
        studentAge  = 0;      // TODO: assign your age
        gpa         = 0.0;    // TODO: assign a GPA (0.0 – 4.0)
        isEnrolled  = false;  // TODO: set to true
    }
}
