using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Menus
{
    class ReturnButton : Button
    {
        public ReturnButton(Vector2 arg) : base(arg) => id = 3;
        protected override void Action()
        {
            Globals.state = GameState.startMenu;
            Globals.menus["start"].Enable();
        }
    }
}