using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Buttons
{
    public class PauseButton : Button
    {
        public PauseButton(Vector2 arg) : base(arg)
        {
            id = 6;
        }
        protected override void Action()
        {
            Globals.state = GameState.pauseMenu;
            Globals.pauseMenu.Enable();
            Disable();
            Globals.scene.pauseButton.Disable();
        }
    }
}
