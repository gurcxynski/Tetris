using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Buttons
{
    public class PlayButton : Button
    {
        public PlayButton(Vector2 arg) : base(arg)
        {
            id = 0;
        }
        protected override void Action()
        {
            Globals.state = GameState.gameRunning;
            foreach (Menu item in Globals.menus.Values)
            {
                item.Disable();
            }
            //Globals.scene.pauseButton.Enable();
        }
    }
}
