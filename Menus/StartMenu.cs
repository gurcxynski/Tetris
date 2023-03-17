using Tetris.Buttons;
using Tetris.Core;

namespace Tetris.Menus;

public class StartMenu : Menu
{
    public StartMenu()
    {
        Add(new PlayButton(new(100, 100)));
        Add(new OptionsButton(new(100, 200)));
    }
}