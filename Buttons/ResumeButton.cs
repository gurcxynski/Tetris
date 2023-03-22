using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Buttons;

class ResumeButton : Button
{
    public ResumeButton(Vector2 arg) : base(arg)
    {
    }
    public override void Initialize()
    {
        text = "RESUME";
        bounds.Size = new(190, 65);
    }
    protected override void Action()
    {
        Game1.gameState.Resume();
    }

}