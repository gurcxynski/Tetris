using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Buttons
{
    public class PauseButton : Button
    {
        public PauseButton(Vector2 arg) : base(arg)
        {
            id = 7;
        }
        protected override void Action()
        {
            Globals.state = GameState.pauseMenu;
            Globals.menus["pause"].Enable();
            Globals.scene.pauseButton.Disable();
        }
    }
}
