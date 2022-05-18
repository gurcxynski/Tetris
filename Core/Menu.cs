using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Tetris.Core
{
    public class Menu
    {
        readonly List<Button> buttons;
        public Menu()
        {
            buttons = new List<Button>();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            buttons.ForEach(delegate (Button btn) { btn.Draw(spriteBatch); });
        }
        public void Add(Button arg)
        {
            buttons.Add(arg);
        }
        public void Update()
        {
            buttons.ForEach(delegate (Button btn) { btn.Update(); });
        }
    }
}
