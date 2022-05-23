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
        }
        protected override void OnClick(MouseButtons button)
        {
            if (hovered && Globals.state == GameState.optionsMenu)
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
}
