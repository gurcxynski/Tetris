using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Buttons;

class ControlsButton : Button
{
    public ControlsButton(Vector2 arg) : base(arg)
    {
    }
    public override void Initialize()
    {
        text = "CONTROLS";
    }
    protected override void Action()
    {
        Game1.gameState.ToControlsScreen();
    }

}