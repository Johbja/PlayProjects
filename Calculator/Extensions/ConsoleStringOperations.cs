using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Calculator.Engine;

namespace Calculator.Extensions;

internal static class ConsoleStringOperations
{
    public static void Print(this MenuTitle title)
    {
        var text = title.Title;
        var width = title.Style.Width;
        var color = title.Style.Color;

        Console.ForegroundColor = color;
        Console.WriteLine($"╔{new string('═', width)}╗");
        Console.WriteLine($"║{text.PadLeft((width + text.Length) / 2).PadRight(width)}║");
        Console.WriteLine($"╚{new string('═', width)}╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void Print(this ExpressionCalculatorInfoTitle title)
    {
        var text = title.Title;
        var width = title.Style.Width;
        var color = title.Style.Color;

        Console.ForegroundColor = color;
        Console.WriteLine($"╔{new string('═', width)}╗");
        Console.WriteLine($"║{text.PadLeft((width + text.Length) / 2).PadRight(width)}║");
        Console.WriteLine($"╚{new string('═', width)}╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void Print(this Menu menu, int spacing = 1)
    {
        foreach (var (active, menuOption) in menu.GetMenuOptions())
        {
            if(!active)
            {
                Console.Write(new string(' ', spacing));
                menuOption.PrintMenuOption();
                continue;
            }

            menuOption.PrintMenuOptionSelected();
        }
    }

    public static void PrintMenuOption(this MenuOption option)
    {
        Console.ForegroundColor = option.Style.TextColor;
        Console.Write(option.Name);
        Console.Write(Environment.NewLine);
        Console.ResetColor();
    }

    public static void PrintMenuOptionSelected(this MenuOption option)
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

    public static void Print(this ExpressionCalculatorInstructions entry)
    {
        entry.InfoTitle.Print();

        var entries = entry.InstructionEntries;
        for (int i = 0; i < entries.Count; i++)
        {
            var infoEntry = entries[i];
            bool isExample = infoEntry.Description.StartsWith("->");
            bool prevWasExample = i > 0 && entries[i - 1].Description.StartsWith("->");
            bool isLastExample = isExample && (i == entries.Count - 1 || !entries[i + 1].Description.StartsWith("->"));

            if (isExample && !prevWasExample)
                Console.WriteLine();

            Console.ForegroundColor = infoEntry.Style.Color;
            if (isExample)
            {
                string prefix = isLastExample ? "  └─ " : "  ├─ ";
                Console.WriteLine($"{prefix}{infoEntry.Description[2..].TrimStart()}");
            }
            else
            {
                Console.WriteLine($"  \u2022 {infoEntry.Description}");
            }
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.WriteLine($"  {new string('─', 50)}");
    }

    public static void Print(this ExpressionCalculatorHistory entry)
    {
        entry.InfoTitle.Print();

        var entries = entry.HistoryEntries;
        for (int i = 0; i < entries.Count; i++)
        {
            var infoEntry = entries[i];
            string prefix = i == entries.Count - 1 ? "  └─ " : "  ├─ ";
            Console.ForegroundColor = infoEntry.Style.Color;
            Console.WriteLine($"{prefix}{infoEntry.Description}");
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.WriteLine($"  {new string('─', 50)}");
    }

    public static void PrintAsError(this Exception ex)
    {
        bool isWarning = ex is ArgumentException;
        string title = isWarning ? "⚠  Warning" : "⚠  Error";
        ConsoleColor color = isWarning ? ConsoleColor.Yellow : ConsoleColor.Red;
        int width = 32;

        Console.WriteLine();
        Console.ForegroundColor = color;
        Console.WriteLine($"╔{new string('═', width)}╗");
        Console.WriteLine($"║{title.PadLeft((width + title.Length) / 2).PadRight(width)}║");
        Console.WriteLine($"╚{new string('═', width)}╝");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"• {ex.Message}");
        Console.WriteLine();
        Console.ResetColor();
    }
}

