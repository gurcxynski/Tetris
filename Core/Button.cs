using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.EasyInput;
using MonoGame.Extended;

namespace Tetris.Core;

public abstract class Button
{
    protected RectangleF bounds;
    protected Texture2D texture;
    protected bool enabled = false;
    protected Button(Vector2 arg)
    {
        Globals.mouse.OnMouseButtonPressed += OnClick;
        bounds = new(arg, new Size2(100, 30));
    }
    public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(texture, (Rectangle)bounds, Hovered() ? Color.DarkGray : Color.White);
    public abstract void Initialize();
    protected bool Hovered()
    {
        if (Globals.mouse.Position.X < bounds.X + texture.Width &&
                Globals.mouse.Position.X > bounds.X &&
                Globals.mouse.Position.Y < bounds.Y + texture.Height &&
                Globals.mouse.Position.Y > bounds.Y)
        {
            return true;
        }
        return false;
    }
    public virtual void Update()
    {
    }
    public void Enable()
    {
        enabled = true;
    }
    public void Disable()
    {
        enabled = false;
    }
    protected void OnClick(MouseButtons button)
    {
        if (Hovered() && enabled && button == MouseButtons.Left)
        {
            Action();
        }
    }
    protected abstract void Action();
}
