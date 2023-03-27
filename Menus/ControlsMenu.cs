using Tetris.Buttons;
using Tetris.Core;

namespace Tetris.Menus;

public class ControlsMenu : Menu
{
    public ControlsMenu()
    {
        Add(new PauseButton(new(200, 580), "BACK"));
    }
}