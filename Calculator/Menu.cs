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

    private Menu() { }

    public static Menu Create()
    {
        return new Menu();
    }

    public Action HandleInput(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.Enter:
                return () => menuMapping[menuState].MenuAction();
            case ConsoleKey.DownArrow:
                return () => menuState = (menuState + 1) % menuMapping.Count;
            case ConsoleKey.UpArrow:
                return () => menuState = (menuState - 1) % menuMapping.Count;
            default:
                return () => { };
        }
    }

    public string[] GetMenuOptions()
    {
        return menuMapping.Values.Select((menuOption, i) => $"{(menuState == i ? "->" : "")} {menuOption.Name}").ToArray();
    }

    public Menu AddMenuOption(MenuOption menuOption)
    {
        menuMapping.Add(menuMapping.Count, menuOption);

        return this;
    }
}

internal class MenuOption(string name, Action menuAction)
{
    public string Name { get; } = name;
    public Action MenuAction { get; } = menuAction;
}
