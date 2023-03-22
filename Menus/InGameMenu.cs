using MonoGame.Extended;
using Tetris.Buttons;
using Tetris.Core;

namespace Tetris.Menus;

public class InGameMenu : Menu
{
    public InGameMenu()
    {
        Add(new BackToMenuButton(new(455, 10)));
        Add(new EnableMusicButton(new(405, 10)));
    }
}