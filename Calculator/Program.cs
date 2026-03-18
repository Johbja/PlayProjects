using Calculator.Calculator.Engine;
using Calculator.Rendering;
using Calculator.Extensions;

namespace Calculator;

internal class Program
{
    private static Menu mainMenu;
    private static readonly string title = "Select Project";

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
        while (true)
        {
            Console.Clear();
            title.PrintAsBannerWithColor();
            mainMenu.PrintMenuWithSpacing();
            mainMenu.HandleInput(Console.ReadKey().Key).Execute();
        }
    }
}
