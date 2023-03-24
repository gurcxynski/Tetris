using Tetris.Buttons;
using Tetris.Core;

namespace Tetris.Menus;

public class PauseMenu : Menu
{
    public PauseMenu()
    {
        Add(new ResumeButton(new(200, 180)));
        Add(new PlayButton(new(200, 280)));
        Add(new ControlsButton(new(200, 380)));
    }
}