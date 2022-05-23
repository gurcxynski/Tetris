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
            position = arg;
            id = 0;
        }
        protected override void Action()
        {
            Globals.state = GameState.gameRunning;
            Disable();
        }
    }
}
