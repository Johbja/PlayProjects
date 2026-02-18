using Calculator.Calculator.Engine;
using Calculator.Rendering;
using System;

namespace Calculator;

internal class Program
{
    private static Menu mainMenu;

    static void Main(string[] args)
    {
        mainMenu = Menu.Create()
            .AddMenuOption(new MenuOption("Start calculator simulator", () => new CalculatorSimulator(MainLoop).Emulate()))
            .AddMenuOption(new MenuOption("Open custom 3d renderer", new Renderer().StartRendering))
            .AddMenuOption(new MenuOption("Exit", () => Environment.Exit(0)));

        MainLoop();
    }

    private static void MainLoop()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine($"---Select project---");
        Console.WriteLine();
        foreach (var menu in mainMenu.GetMenuOptions())
        {
            Console.WriteLine(menu);
        }

        mainMenu.HandleInput(Console.ReadKey().Key)();
        MainLoop();
    }


}
