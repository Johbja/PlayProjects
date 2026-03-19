using Calculator.Calculator.Engine;
using Calculator.Rendering;
using Calculator.Extensions;
using ContextEngine;

namespace Calculator;

internal class Program
{
    static async Task Main(string[] args)
    {
        var context = new ProgramContext();
        Console.CursorVisible = false;

        Menu.Create(context)
            .SetTitle(new MenuTitle("Select Project"))
            .AddMenuOption(new MenuOption("Start calculator simulator", () => { new CalculatorSimulator(context); }))
            .AddMenuOption(new MenuOption("Open custom 3d renderer", new Renderer().StartRendering))
            .AddMenuOption(new MenuOption("Exit", () => Environment.Exit(0)))
            .Render();

        await context.ExecuteContextAsync();

        //MainLoop();
    }

    //private static void MainLoop()
    //{
    //    while (true)
    //    {
    //        Console.Clear();
    //        title.PrintAsBannerWithColor();
    //        mainMenu.PrintMenuWithSpacing();
    //        mainMenu.HandleInput(Console.ReadKey().Key).Execute();
    //    }
    //}
}
