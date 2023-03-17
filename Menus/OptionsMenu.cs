using Microsoft.Xna.Framework;
using Tetris.Buttons;
using Tetris.Core;

namespace Tetris.Menus;

public class OptionsMenu : Menu
{
    public OptionsMenu()
    {
        Add(new EnableMusicButton(new Vector2(100, 100)));
        Add(new ReturnButton(new(100, 400)));
    }
}