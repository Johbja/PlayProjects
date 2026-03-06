namespace CSharpCourse;

public class CourseRunner
{
    private readonly List<Assignment> assignments;

    public CourseRunner(IEnumerable<Assignment> assignments)
    {
        this.assignments = assignments.OrderBy(a => a.Number).ToList();
    }

    public void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;

        while (true)
        {
            Console.Clear();
            PrintBanner();
            PrintMenu();
            string? input = Console.ReadLine()?.Trim();

            if (input == "q" || input == "Q")
                break;

            if (input == "a" || input == "A")
            {
                RunAll();
                continue;
            }

            if (int.TryParse(input, out int number) && number >= 1 && number <= assignments.Count)
            {
                RunSingle(assignments[number - 1]);
            }
            else
            {
                int errorRow = Console.CursorTop;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  Invalid choice. Enter 1–{assignments.Count}, A, or Q.");
                Console.ResetColor();
                Thread.Sleep(1200);
                Console.SetCursorPosition(0, errorRow);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        Console.CursorVisible = true;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n  Goodbye! Keep coding! 🚀\n");
        Console.ResetColor();
    }

    private void PrintBanner()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║        C# Beginner Course — 10 Assignments           ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    private void PrintMenu()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  Select an assignment to run its tests:");
        Console.ResetColor();
        Console.WriteLine();

        foreach (var a in assignments)
        {
            Console.WriteLine($"   [{a.Number,2}]  {a.Title}");
        }

        Console.WriteLine();
        Console.WriteLine("   [A]  Run ALL assignments");
        Console.WriteLine("   [Q]  Quit");
        Console.Write("\n  > ");
    }

    private void RunSingle(Assignment assignment)
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"  Assignment {assignment.Number}: {assignment.Title}");
        Console.WriteLine($"  {new string('─', 50)}");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"  {assignment.Description}");
        Console.ResetColor();
        Console.WriteLine();

        var results = assignment.Validate().ToList();

        foreach (var result in results)
        {
            if (result.Passed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  ✓  {result.TestName}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗  {result.TestName}");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"     → {result.Message}");
            }
            Console.ResetColor();
        }

        int passed = results.Count(r => r.Passed);
        int failed = results.Count - passed;

        Console.WriteLine();
        Console.WriteLine($"  {new string('─', 50)}");

        if (failed == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  🎉  All {passed} test(s) passed!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  {passed}/{results.Count} passed    {failed} failed");
        }

        Console.ResetColor();

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("  Press any key to return to the menu...");
        Console.ResetColor();
        Console.CursorVisible = false;
        Console.ReadKey(intercept: true);
    }

    private void RunAll()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("  All Assignments — Overview");
        Console.WriteLine($"  {new string('─', 50)}");
        Console.ResetColor();
        Console.WriteLine();

        int totalPassed = 0;
        int totalFailed = 0;

        foreach (var assignment in assignments)
        {
            var results = assignment.Validate().ToList();
            int passed = results.Count(r => r.Passed);
            int failed = results.Count - passed;
            totalPassed += passed;
            totalFailed += failed;

            string icon = failed == 0 ? "✓" : "✗";
            Console.ForegroundColor = failed == 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write($"  {icon}  Assignment {assignment.Number:00}: {assignment.Title,-32}");
            Console.ResetColor();
            Console.ForegroundColor = failed == 0 ? ConsoleColor.DarkGreen : ConsoleColor.DarkYellow;
            Console.WriteLine($"  {passed}/{results.Count} tests");
            Console.ResetColor();

            foreach (var result in results.Where(r => !r.Passed))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"       → {result.TestName}: {result.Message}");
                Console.ResetColor();
            }
        }

        Console.WriteLine();
        Console.WriteLine($"  {new string('─', 50)}");
        Console.ForegroundColor = totalFailed == 0 ? ConsoleColor.Green : ConsoleColor.Yellow;
        Console.WriteLine($"  Total: {totalPassed} passed, {totalFailed} failed" +
                          $" out of {totalPassed + totalFailed} tests");
        Console.ResetColor();

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("  Press any key to return to the menu...");
        Console.ResetColor();
        Console.CursorVisible = false;
        Console.ReadKey(intercept: true);
    }
}
