namespace CSharpCourse;

public class TestResult
{
    public string TestName { get; }
    public bool Passed { get; }
    public string Message { get; }

    public TestResult(string testName, bool passed, string message)
    {
        TestName = testName;
        Passed = passed;
        Message = message;
    }

    public static TestResult Pass(string testName) =>
        new(testName, true, "OK");

    public static TestResult Fail(string testName, string reason) =>
        new(testName, false, reason);
}
