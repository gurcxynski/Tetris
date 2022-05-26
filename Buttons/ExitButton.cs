using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Buttons
{
    public class ExitButton: Button
    {
        public ExitButton(Vector2 arg) : base(arg)
        {
            id = 4;
        }
        protected override void Action()
        {
            Game1.self.Exit();
        }
    }
}