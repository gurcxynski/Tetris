using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using MonoGame.EasyInput;
using System;
using System.Collections.Generic;
using System.Text;
using Tetris.Core;

namespace Tetris.Buttons
{
    public class MusicButton : Button
    {
        public MusicButton(Vector2 arg)
        {
            position = arg - new Vector2(texture.Width / 2, texture.Height / 2);
            text = "DISABLE MUSIC";
        }
        protected override void OnClick(MouseButtons button)
        {
            if (hovered)
            {
                if (Globals.music)
                {
                    Globals.music = false;
                    MediaPlayer.Stop();
                    text = "ENABLE MUSIC";
                }
                else
                {
                    MediaPlayer.Play(Globals.song);
                    Globals.music = true;
                    text = "DISABLE MUSIC";
                }
            }
        }
    }
}
