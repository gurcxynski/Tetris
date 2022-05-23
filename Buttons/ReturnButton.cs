using Microsoft.Xna.Framework;
using MonoGame.EasyInput;
using Tetris.Core;

namespace Tetris.Menus
{
    class ReturnButton : Button
    {
        public ReturnButton(Vector2 arg)
        {
            position = arg;
            id = 3;
        }
        protected override void OnClick(MouseButtons button)
        {
            if(hovered && Globals.state == GameState.optionsMenu)
            {
                Globals.state = GameState.startMenu;
            }
        }
    }
}