using Microsoft.Xna.Framework;
using Tetris.Core;
namespace Tetris.Buttons
{
    public class OptionsButton : Button
    {
        public OptionsButton(Vector2 arg) : base(arg)
        {
            id = 1;
        }
        protected override void Action()
        {
            Globals.state = GameState.optionsMenu;
            Globals.menus["options"].Enable();
        }
    }
}