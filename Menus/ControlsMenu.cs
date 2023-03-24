using Tetris.Buttons;
using Tetris.Core;

namespace Tetris.Menus;

public class ControlsMenu : Menu
{
    public ControlsMenu()
    {
        Add(new ResumeButton(new(200, 580)));
    }
}