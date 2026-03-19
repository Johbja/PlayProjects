using Calculator.Extensions;
using ContextEngine;

namespace Calculator;

internal class Menu : ContextExecutable
{
    private MenuTitle menuTitle = new(string.Empty);
    private MenuState menuState = new MenuState();
    
    private Menu(ProgramContext context) : base(context)
    {
    }

    public static Menu Create(ProgramContext context)
    {
        return new Menu(context);
    }

    private void HandleInput(ConsoleKey key)
    {
        if (key == ConsoleKey.Enter)
        {
            menuState.ExecuteCurrentState();

            return;
        }

        menuState.UpdateMenuState(key);
    }

    public (bool active, MenuOption)[] GetMenuOptions()
    {
        return menuState.GetCurrentState();
    }

    public Menu AddMenuOption(MenuOption menuOption)
    {
        menuState.AddMenuOption(menuOption);

        return this;
    }

    public Menu SetTitle(MenuTitle title)
    {
        menuTitle = title;
        
        return this;
    }

    public void Render()
    {
        ExecuteOnContext(new ContextAction(() =>
        {
            Console.Clear();
            menuTitle.Print();
            this.Print();
            HandleInput(Console.ReadKey().Key);

            Render();
        }));
    }
}

internal class MenuTitle(string title, MenuTitleStyleOptions? style = null)
{
    public string Title { get; } = title;
    public MenuTitleStyleOptions Style { get; } = style ?? MenuTitleStyleOptions.CreateDefault;
}

internal class MenuTitleStyleOptions(int width, ConsoleColor color)
{
    public ConsoleColor Color { get; } = color;
    public int Width { get; } = width;

    public static MenuTitleStyleOptions CreateDefault
        => new(
            32,
            ConsoleColor.Cyan);
}

internal class MenuState
{
    private int state = 0;
    private Dictionary<int, MenuOption> menuStateMapping = new();

    public void AddMenuOption(MenuOption menuOption)
    {
        menuStateMapping.Add(menuStateMapping.Count, menuOption);
    }

    public void ExecuteCurrentState()
    {
        menuStateMapping[state].MenuAction.Execute();
    }

    public void UpdateMenuState(ConsoleKey key)
    {
        if (key is not (ConsoleKey.DownArrow or ConsoleKey.UpArrow))
        {
            return;
        }

        var newState = key == ConsoleKey.DownArrow ? 1 : -1;
        state = ((state + newState) % menuStateMapping.Count + menuStateMapping.Count) % menuStateMapping.Count;
    }

    public (bool active, MenuOption)[] GetCurrentState()
    {
        return menuStateMapping.Select(option => (option.Key == state, option.Value)).ToArray();
    }
}

internal class MenuOption(
    string name,
    Action menuAction,
    MenuStyleOptions? style = null)
{
    public string Name { get; } = name;
    public MenuAction MenuAction { get; } = new MenuAction(menuAction);
    public MenuStyleOptions Style { get; } = style ?? MenuStyleOptions.CreateDefault;
}

internal class MenuAction(Action menuAction)
{
    private readonly Action menuAction = menuAction;

    public void Execute()
    {
        menuAction();
    }
}

internal class MenuStyleOptions
{
    private MenuStyleOptions(
        char openSymbol,
        char closeSymbol,
        ConsoleColor selectorColor,
        ConsoleColor textColor)
    {
        this.openSymbol = openSymbol;
        this.closeSymbol = closeSymbol;
        this.selectorColor = selectorColor;
        this.textColor = textColor;
    }

    private char openSymbol;
    private char closeSymbol;
    private ConsoleColor selectorColor;
    private ConsoleColor textColor;

    public string OpenSymbol => $"{openSymbol}";
    public string CloseSymbol => $"{closeSymbol}";
    public ConsoleColor SelectorColor => selectorColor;
    public ConsoleColor TextColor => textColor;

    public static MenuStyleOptions CreateDefault
        => new(
            '[',
            ']',
            ConsoleColor.Green,
            ConsoleColor.White);
}
