using Microsoft.Xna.Framework;
using Tetris.Buttons;
using Tetris.Core;

namespace Tetris.Menus
{
    public class StartMenu : Menu
    {
        public StartMenu()
        {
            buttons.Add(new PlayButton(new Vector2(200, 250)));
            buttons.Add(new OptionsButton(new Vector2(200, 350)));
            buttons.Add(new ExitButton(new Vector2(200, 450)));
        }
    }
}