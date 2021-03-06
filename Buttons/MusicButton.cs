using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Tetris.Core;

namespace Tetris.Buttons
{
    class EnableMusicButton : Button
    {
        public EnableMusicButton(Vector2 arg) : base(arg) => id = 2;
        protected override void Action()
        {
            if (Settings.music)
            {
                Settings.music = false;
                MediaPlayer.Stop();
            }
            else
            {
                MediaPlayer.Play(Globals.song);
                Settings.music = true;
            }
        }
    }
}
