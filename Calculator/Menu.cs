using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator;

internal class Menu
{
    private int menuState = 0;
    private Dictionary<int, MenuOption> menuMapping = new();
    private MenuAction updateMenuStateDown;
    private MenuAction updateMenuStateUp;

    private Menu()
    {
        updateMenuStateDown = new MenuAction(() => UpdateMenuState(ConsoleKey.DownArrow));
        updateMenuStateUp = new MenuAction( () => UpdateMenuState(ConsoleKey.UpArrow));
    }

    public static Menu Create()
    {
        return new Menu();
    }

    public MenuAction HandleInput(ConsoleKey key)
    {
        return key switch
        {
            ConsoleKey.Enter => menuMapping[menuState].MenuAction,
            ConsoleKey.DownArrow => updateMenuStateDown,
            ConsoleKey.UpArrow => updateMenuStateUp,
            _ => new MenuAction(() => { })
        };
    }

    private void UpdateMenuState(ConsoleKey key)
    {
        var newState = key == ConsoleKey.DownArrow ? 1 : -1;
        menuState = (menuState + newState) % menuMapping.Count;
    }

    public MenuOption[] GetMenuOptions()
    {
        return menuMapping.Values.ToArray();
    }

    public Menu AddMenuOption(MenuOption menuOption)
    {
        menuMapping.Add(menuMapping.Count, menuOption);

        return this;
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
