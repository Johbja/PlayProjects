using Calculator.Extensions;
using ContextEngine;

namespace Calculator.Calculator.Engine;
internal class CalculatorSimulator : ContextExecutable
{
    private ExpressionCalculator<double> calculator = new();
    private readonly ProgramContext childContext;

    public CalculatorSimulator(ProgramContext pc) : base(pc)
    {
        childContext = new ProgramContext();
        var subContext = pc.AddSubContext(childContext);

        Menu.Create(childContext)
            .SetTitle(new MenuTitle("Master Blaster Calculator"))
            .AddMenuOption(new MenuOption("Enter equation", EnterEquation))
            .AddMenuOption(new MenuOption("View history", ViewHistory))
            .AddMenuOption(new MenuOption("Help", Help))
            .AddMenuOption(new MenuOption("Exit", () => { subContext.Dispose(); }))
            .Render();
    }

    public override void ExecuteOnContext(IContextExecutableItem logic)
    {
        childContext.EnqueueExecution(logic);
    }

    private void EnterEquation()
    {
        ExecuteOnContext(new ContextAction(() =>
        {
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"╔{new string('═', 32)}╗");
                Console.WriteLine($"║{"Enter Equation".PadLeft((32 + "Enter Equation".Length) / 2).PadRight(32)}║");
                Console.WriteLine($"╚{new string('═', 32)}╝");
                Console.ResetColor();
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  type 'exit' to return");
                Console.ResetColor();
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("  \u00bb ");
                Console.ResetColor();

                var equation = Console.ReadLine();

                if (equation?.ToLower() == "exit" || equation is null)
                {
                    Console.CursorVisible = false;
                    break;
                }

                try
                {
                    var result = calculator.Evaluate(equation);
                    Console.WriteLine();
                    Console.WriteLine($"  {new string('─', 50)}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"  = {result}");
                    Console.ResetColor();
                    Console.WriteLine($"  {new string('─', 50)}");
                }
                catch (Exception ex)
                {
                    ex.PrintAsError();
                }
                finally
                {
                    Console.ReadKey();
                }
            }
        }));
    }

    private void Help()
    {
        ExecuteOnContext(new ContextAction(() =>
        {
            Console.Clear();
            calculator.Instructions().Print();
            Console.ReadKey();
        }));
    }

    private void ViewHistory()
    {
        ExecuteOnContext(new ContextAction(() =>
        {
            Console.Clear();
            calculator.GetFullHistory().Print();
            Console.ReadKey();
        }));
    }
}
