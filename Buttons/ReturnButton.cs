using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Menus
{
    class ReturnButton : Button
    {
        public ReturnButton(Vector2 arg) : base(arg)
        {
        }
        public override void Initialize()
        {
            texture = Game1.textures["returnbutton"];
        }
        protected override void Action()
        {
            Game1.gameState.ToStartMenu();
        }
    }
}