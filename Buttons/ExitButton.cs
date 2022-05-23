using Microsoft.Xna.Framework;
using MonoGame.EasyInput;
using System;
using Tetris.Core;
using Tetris;
namespace Tetris.Buttons
{
    public class ExitButton: Button
    {
        public ExitButton(Vector2 arg)
        {
            position = arg;
            id = 4;
        }
        protected override void Action()
        {
            Game1.self.Exit();
        }
    }
}