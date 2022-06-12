using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Tetris.Core;

namespace Tetris.Buttons
{
    class VerticalLineButton : Button
    {
        public VerticalLineButton(Vector2 arg) : base(arg)
        {
            id = 9;
            texture = Globals.buttonTextures[(id, false)];
        }
        protected override void Action()
        {
            MediaPlayer.Volume = (Globals.mouse.Position.X - 200) / 120f;
        }
        public override void Update()
        {
            hovered = EnteredButton();
        }
    }
}