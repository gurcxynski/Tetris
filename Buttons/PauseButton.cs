using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Buttons;

class PauseButton : Button
{
    public PauseButton(Vector2 arg) : base(arg)
    {

    }
    public override void Initialize()
    {
        texture = Game1.textures["returnbutton"];
        bounds.Size = new(40, 40);
    }
    protected override void Action()
    {
        Game1.gameState.ToPauseMenu();
    }
}