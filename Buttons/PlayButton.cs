using Microsoft.Xna.Framework;
using MonoGame.EasyInput;
using System;
using System.Collections.Generic;
using System.Text;
using Tetris.Core;

namespace Tetris.Buttons
{
    public class PlayButton : Button
    {
        public PlayButton(Vector2 arg)
        {
            position = arg - new Vector2(texture.Width / 2, texture.Height / 2);
            text = "PLAY";
        }
        protected override void OnClick(MouseButtons button)
        {
            if (hovered)
            {
                Globals.gameRunning = !Globals.gameRunning;
            }
        }
    }
}
