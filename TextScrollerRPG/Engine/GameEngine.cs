using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextScrollerRPG.Engine;

internal class GameEngine
{
    public const int Width = 20;
    public const int Height = 20;

    public void Start()
    {
        NativeConsole.TrySetSize(Width + 2, Height);

        var mainMenu = GameMenu.CreateMainMenu("Text Scroller RPG", () =>
        {
            Console.Clear();
            Console.WriteLine("Starting game...");
            Console.ReadKey();
        }, () =>
        {
            Console.Clear();
            Console.WriteLine("Starting game...");
            Console.ReadKey();
        }, () =>
        {
            Environment.Exit(0);
        });

        while (true)
        {
            mainMenu.Print();
            var key = Console.ReadKey(true).Key;
            mainMenu.HandleInput(key);
        }
    }
}

internal class GameMenu
{
    private readonly string _banner;
    private GameMenuState _currentState;
    private readonly List<GameMenuState> _states;

    private GameMenu(List<GameMenuState> states, string banner = "Main Menu")
    {
        _states = states;
        _currentState = states[0];
        _banner = banner;
    }

    private void PrintBanner()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"╔{new string('═', GameEngine.Width)}╗");
        Console.WriteLine($"║{_banner.PadLeft((GameEngine.Width + _banner.Length) / 2),-GameEngine.Width}║");
        Console.WriteLine($"╚{new string('═', GameEngine.Width)}╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    public void HandleInput(ConsoleKey key)
    {
        _currentState = _currentState.HandleInput(key);
    }

    public void Print()
    {
        Console.Clear();
        Console.CursorVisible = false;
        PrintBanner();

        foreach (var state in _states)
        {
            if (state == _currentState)
            {
                state.Option.PrintSelected();
            }
            else
            {
                Console.Write(" ");
                state.Option.Print();
            }

            Console.WriteLine();
        }
    }

    public static GameMenu CreateMainMenu(string banner, Action onStartGame, Action onOptions, Action onExit)
    {
        var start = new GameMenuState(GameMenuOption.CreateDefault("Start Game", onStartGame));
        var options = new GameMenuState(GameMenuOption.CreateDefault("Options", onOptions));
        var exit  = new GameMenuState(GameMenuOption.CreateDefault("Exit", onExit));

        start.Map(ConsoleKey.DownArrow, options).Map(ConsoleKey.UpArrow, exit);
        options.Map(ConsoleKey.DownArrow, exit).Map(ConsoleKey.UpArrow, start);
        exit.Map(ConsoleKey.DownArrow, start).Map(ConsoleKey.UpArrow, options);

        return new GameMenu([start, options, exit], banner);
    }
}

internal class GameMenuState(GameMenuOption option)
{
    public GameMenuOption Option { get; } = option;
    
    private readonly Dictionary<ConsoleKey, GameMenuState> _transitions = new();

    public GameMenuState Map(ConsoleKey key, GameMenuState next)
    {
        _transitions[key] = next;

        return this;
    }

    public GameMenuState HandleInput(ConsoleKey key)
    {
        if (key != ConsoleKey.Enter)
        {
            return _transitions.TryGetValue(key, out var next) ? next : this;
        }

        Option.Select();

        return this;
    }
}

internal class GameMenuOption
{
    public string Name { get; }
    public ConsoleColor Color { get; }

    private readonly Action _action;

    private GameMenuOption(string name, ConsoleColor color, Action action)
    {
        Name = name;
        Color = color;
        _action = action;
    }

    public void Select()
    {
        _action.Invoke();
    }

    public Action Decorate(TextDecoration decoration)
    {
        return decoration.Decorate(Print);
    }

    public void Print()
    {
        Console.ForegroundColor = Color;
        Console.Write(Name);
        Console.ResetColor();
    }

    public void PrintSelected()
    {
        Decorate(TextDecoration.Brackets(ConsoleColor.Green)).Invoke();
    }

    public static GameMenuOption CreateDefault(string name, Action onSelect)
    {
        return new GameMenuOption(
            name, 
            ConsoleColor.White,
            onSelect);
    }
}


internal static class NativeConsole
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll")]
    private static extern bool GetCurrentConsoleFontEx(IntPtr hOutput, bool bMaxWindow, ref ConsoleFontInfo font);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int x, int y, int w, int h, uint flags);

    [StructLayout(LayoutKind.Sequential)]
    private struct Coord { public short X, Y; }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct ConsoleFontInfo
    {
        public uint cbSize;
        public uint nFont;
        public Coord dwFontSize;
        public int FontFamily;
        public int FontWeight;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FaceName;
    }

    private const uint SWP_NOMOVE = 0x0002;

    public static void TrySetSize(int widthChars, int heightChars)
    {
        try
        {
            Console.SetWindowSize(widthChars, heightChars);
            Console.SetBufferSize(widthChars, heightChars);
            return;
        }
        catch (IOException) { }

        try
        {
            var font = new ConsoleFontInfo { cbSize = (uint)Marshal.SizeOf<ConsoleFontInfo>() };
            GetCurrentConsoleFontEx(GetStdHandle(-11), false, ref font);

            int fw = font.dwFontSize.X > 0 ? font.dwFontSize.X : 10;
            int fh = font.dwFontSize.Y > 0 ? font.dwFontSize.Y : 20;

            SetWindowPos(GetConsoleWindow(), IntPtr.Zero, 0, 0,
                widthChars * fw + 17,
                heightChars * fh + 40,
                SWP_NOMOVE);
        }
        catch { }
    }
}

internal class TextDecoration
{
    public string Left { get; }
    public string Right { get; }
    public ConsoleColor Color { get; } = ConsoleColor.White;

    private TextDecoration(string left, string right, ConsoleColor color)
    {
        Left = left;
        Right = right;
        Color = color;
    }

    public Action Decorate(Action innerContent)
    {
        return () =>
        {
            if (!string.IsNullOrEmpty(Left))
            {
                Console.ForegroundColor = Color;
                Console.Write(Left);
                Console.ResetColor();
            }

            innerContent();

            if (string.IsNullOrEmpty(Right))
            {
                return;
            }

            Console.ForegroundColor = Color;
            Console.Write(Right);
            Console.ResetColor();
        };
    }

    public static TextDecoration Brackets(ConsoleColor color)
    {
        return new TextDecoration("[", "]", color);
    }
}

