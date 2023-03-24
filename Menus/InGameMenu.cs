using MonoGame.Extended;
using Tetris.Buttons;
using Tetris.Core;

namespace Tetris.Menus;

public class InGameMenu : Menu
{
    public InGameMenu()
    {
        Add(new PauseButton(new(500, 10)));
        Add(new EnableMusicButton(new(550, 10)));
    }
}