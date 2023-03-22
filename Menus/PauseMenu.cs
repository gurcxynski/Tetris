using Tetris.Buttons;
using Tetris.Core;

namespace Tetris.Menus
{
    public class PauseMenu : Menu
    {
        public PauseMenu()
        {
            Add(new ResumeButton(new(160, 180)));
            Add(new PlayButton(new(160, 280)));
        }
    }
}