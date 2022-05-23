using Tetris.Core;
using Tetris.Buttons;
using Microsoft.Xna.Framework;

namespace Tetris.Menus
{
    public class OptionsMenu: Menu
    {
        public OptionsMenu()
        {
            buttons.Add(new EnableMusicButton(new Vector2(200, 250)));
            buttons.Add(new MusicVolumeButton(new Vector2(200, 350)));
            buttons.Add(new ReturnButton(new Vector2(200, 450)));
        }
    }
}