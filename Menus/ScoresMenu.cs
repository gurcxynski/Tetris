using Tetris.Buttons;
using Tetris.Core;

namespace Tetris.Menus;

public class ScoresMenu : Menu
{
    public ScoresMenu()
    {
        Add(new ReturnButton(new(200, 580)));
    }
}
