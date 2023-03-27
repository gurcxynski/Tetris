using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Tetris.Core;

public abstract class Menu
{
    protected List<Button> buttons = new();
    public void Draw(SpriteBatch spriteBatch)
    {
        buttons.ForEach(btn => btn.Draw(spriteBatch));
    }
    public void Initialize()
    {
        buttons.ForEach(btn => btn.Initialize());
    }
    public void Add(Button arg)
    {
        buttons.Add(arg);
    }
    public void Update()
    {
        buttons.ForEach(btn => btn.Update());
    }
    public void Enable()
    {
        buttons.ForEach(btn => btn.Enable());
    }
    public void Disable()
    {
        buttons.ForEach(btn => btn.Disable());
    }
}
