using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Tetris.Core;

namespace Tetris.Buttons
{
    internal class RoundIndicatorButton : Button
    {
        public RoundIndicatorButton(Vector2 arg) : base(arg)
        {
            id = 8;
            texture = Globals.buttonTextures[(id, false)];
        }
        protected override void Action()
        {
            
        }
        public override void Update()
        {
            position = new Vector2(200 + MediaPlayer.Volume * 120, position.Y);
        }
    }
}