using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Tetris.Core
{
    public abstract class Menu
    {
        readonly protected List<Button> buttons;
        protected Menu()
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
        public void Enable()
        {
            foreach (Menu item in Globals.menus.Values)
            {
                item.Disable();
            }
            buttons.ForEach(delegate (Button btn) { btn.Enable(); });

        }
        public void Disable()
        {
            buttons.ForEach(delegate (Button btn) { btn.Disable(); });
        }
    }
}
