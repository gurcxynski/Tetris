using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Menus;

class ReturnButton : Button
{
    public ReturnButton(Vector2 arg) : base(arg)
    {
    }
    public override void Initialize()
    {
        text = "BACK";
        bounds.Size = new(190, 65);
    }
    protected override void Action()
    {
        Game1.gameState.ToStartMenu();
    }
}