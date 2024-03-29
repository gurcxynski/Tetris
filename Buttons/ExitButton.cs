using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Buttons;

class ExitButton : Button
{
    public ExitButton(Vector2 arg) : base(arg)
    {
    }

    public override void Initialize()
    {
        text = "EXIT";
    }
    protected override void Action() => Game1.self.Exit();
}