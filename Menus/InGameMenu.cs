using MonoGame.Extended;
using Tetris.Buttons;
using Tetris.Core;

namespace Tetris.Menus;

public class InGameMenu : Menu
{
    public InGameMenu()
    {
        Add(new PauseButton(new(450, 10)));
        Add(new EnableMusicButton(new(500, 10)));
    }
}