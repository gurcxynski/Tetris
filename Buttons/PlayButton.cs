using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Buttons;

class PlayButton : Button
{
    public PlayButton(Vector2 arg) : base(arg)
    {
    }
    public override void Initialize()
    {
        text = "NEW GAME";
        bounds.Size = new(230, 65);
    }
    protected override void Action()
    {
        Game1.gameState.NewGame();
    }

}