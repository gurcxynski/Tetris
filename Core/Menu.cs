using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Tetris.Core;

public abstract class Menu
{
    protected List<Button> buttons = new();
    public void Draw(SpriteBatch spriteBatch)
    {
        buttons.ForEach(delegate (Button btn) { btn.Draw(spriteBatch); });
    }
    public void Initialize()
    {
        buttons.ForEach(delegate (Button button) { button.Initialize(); });
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
        buttons.ForEach(delegate (Button btn) { btn.Enable(); });
    }
    public void Disable()
    {
        buttons.ForEach(delegate (Button btn) { btn.Disable(); });
    }
}
