using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using MonoGame.EasyInput;
using Tetris.Core;

namespace Tetris.Buttons
{
    internal class MusicUpButton : Button
    {
        public MusicUpButton(Vector2 arg) : base(arg)
        {
            id = 6;
        }
        protected override void Action()
        {
            MediaPlayer.Volume += 0.2f;
        }
    }
}