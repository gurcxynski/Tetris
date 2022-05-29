using Microsoft.Xna.Framework;
using Tetris.Buttons;
using Tetris.Core;

namespace Tetris.Menus
{
    public class OptionsMenu : Menu
    {
        public OptionsMenu()
        {
            buttons.Add(new EnableMusicButton(new Vector2(200, 250)));
            buttons.Add(new MusicDownButton(new Vector2(150, 350)));
            buttons.Add(new MusicUpButton(new Vector2(350, 350)));
            buttons.Add(new ReturnButton(new Vector2(200, 450)));
            buttons.Add(new VerticalLineButton(new Vector2(200, 370)));
            buttons.Add(new RoundIndicatorButton(new Vector2(390, 360)));
        }
    }
}