using Microsoft.Xna.Framework;
using MonoGame.EasyInput;
using System;
using Tetris.Core;
namespace Tetris.Buttons
{
    public class OptionsButton: Button
    {
        public OptionsButton(Vector2 arg)
        {
            position = arg;
            id = 1;
        }
        protected override void Action()
        {
            Globals.state = GameState.optionsMenu;
            Disable();
            Globals.optionsMenu.Enable();
        }
    }
}