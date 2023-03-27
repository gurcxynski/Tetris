using Microsoft.Xna.Framework;
using Tetris.Core;

namespace Tetris.Buttons;

class ScoresButton : Button
{
    public ScoresButton(Vector2 arg) : base(arg)
    {
    }
    public override void Initialize()
    {
        text = "HIGH SCORES";
        bounds.Size = new(190, 65);
    }
    protected override void Action()
    {
        Game1.gameState.ToScoresScreen();
    }

}