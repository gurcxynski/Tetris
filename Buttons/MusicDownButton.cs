using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using MonoGame.EasyInput;
using Tetris.Core;

namespace Tetris.Buttons
{
    internal class MusicDownButton : Button
    {
        public MusicDownButton(Vector2 arg) : base(arg)
        {
            id = 5;
        }
        protected override void Action()
        {
            MediaPlayer.Volume -= 0.2f;
        }
    }
}