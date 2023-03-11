using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Buttons
{
    class PlayButton : Button
    {
        public PlayButton(Vector2 arg) : base(arg)
        {
        }
        public override void Initialize()
        {
            texture = Game1.textures["playbutton"];
        }
        protected override void Action()
        {
            Game1.gameState.NewGame();
        }

    }
}