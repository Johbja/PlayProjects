using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Extensions;

internal static class ConsoleStringOperations
{
    public static void PrintAsBannerWithColor(this string text, int width = 54, ConsoleColor color = ConsoleColor.Cyan)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"╔{new string('═', width)}╗");
        Console.WriteLine($"║{text.PadLeft((width + text.Length) / 2).PadRight(width)}║");
        Console.WriteLine($"╚{new string('═', width)}╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void PrintMenuWithSpacing(this Menu menu, int spacing = 1)
    {
        foreach (var menuOption in menu.GetMenuOptions())
        {
            Console.Write(new string(' ', spacing));
            menuOption.PrintMenuOption();
        }
    }

    public static void PrintMenuOption(this MenuOption option)
    {
        Console.ForegroundColor = option.Style.SelectorColor;
        Console.Write(option.Style.OpenSymbol);
        Console.ForegroundColor = option.Style.TextColor;
        Console.Write(option.Name);
        Console.ForegroundColor = option.Style.SelectorColor;
        Console.Write(option.Style.CloseSymbol);
        Console.Write(Environment.NewLine);
        Console.ResetColor();
    }
}

