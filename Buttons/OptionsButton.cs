using Microsoft.Xna.Framework;
using Tetris.Core;
namespace Tetris.Buttons
{
    class OptionsButton : Button
    {
        public OptionsButton(Vector2 arg) : base(arg)
        {

        }
        public override void Initialize()
        {
            texture = Game1.textures["optionsbutton"];
        }
        protected override void Action()
        {
            Game1.gameState.OpenOptions();
        }
    }
}