using Tetris.Buttons;
using Tetris.Core;

namespace Tetris.Menus;

public class StartMenu : Menu
{
    public StartMenu()
    {
        Add(new PlayButton(new(200, 180)));
        Add(new ScoresButton(new(200, 280)));
    }
}