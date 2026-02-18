using Calculator.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Calculator.Engine;
internal class CalculatorSimulator
{
    private string title = "Master Blaster Calculator";
    private Menu mainMenu;
    private ExpressionCalculator<double> calculator = new();

    public CalculatorSimulator(Action exitCallback = null)
    {
        mainMenu = Menu.Create()
            .AddMenuOption(new MenuOption("Enter equation", EnterEquation))
            .AddMenuOption(new MenuOption("View history", ViewHistory))
            .AddMenuOption(new MenuOption("Help", Help))
            .AddMenuOption(new MenuOption("Exit", exitCallback == null ? () => Environment.Exit(0) : exitCallback));
    }

    public void Emulate()
    {
        MainLoop();
    }

    private void MainLoop()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine($"---{title}---");
        Console.WriteLine();

        foreach (var menuOption in mainMenu.GetMenuOptions())
        {
            Console.WriteLine(menuOption);
        }

        mainMenu.HandleInput(Console.ReadKey().Key)();
        MainLoop();
    }

    private void EnterEquation()
    {
        string inputMessage = "Enter equation (or exit to return): ";
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("---Enter equation---");
        Loop();

        void Loop()
        {
            Console.WriteLine();
            Console.Write(inputMessage);
            var equation = Console.ReadLine();
            equation = equation?.Replace(inputMessage, "");

            if (equation?.ToLower() == "exit")
            {
                MainLoop();
                return;
            }

            if (equation is not null)
            {
                var result = calculator.Evaluate(equation);
                Console.WriteLine($"Result: {result}");
            }

            Loop();
        }
    }

    private void Help()
    {
        Console.Clear();
        Console.WriteLine();
        foreach (var instruction in calculator.Instructions())
        {
            Console.WriteLine(instruction);
        }
        Console.WriteLine();
        Console.WriteLine("Press any key to return to menu...");

        Console.ReadKey();
    }

    private void ViewHistory()
    {
        Console.Clear();
        Console.WriteLine();
        foreach (var history in calculator.GetFullHistory())
        {
            Console.WriteLine(history);
        }
        Console.WriteLine();
        Console.WriteLine("Press any key to return to menu...");

        Console.ReadKey();
    }
}
