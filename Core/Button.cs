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
    protected string text;
    protected Button(Vector2 position)
    {
        Globals.mouse.OnMouseButtonPressed += OnClick;
        bounds = new(position, new(230, 65));
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (text is null)
        {
            spriteBatch.Draw(texture, (Rectangle)bounds, Hovered() ? Color.Gray : Color.White);
            return;
        }
        var pos = bounds.Position + bounds.Size / 2 - Globals.fontbig.MeasureString(text) / 2;
        spriteBatch.FillRectangle(bounds, Hovered() ? Color.DarkCyan : Color.DarkTurquoise);
        spriteBatch.DrawRectangle(bounds, Color.DarkBlue, 3);
        spriteBatch.DrawString(Globals.fontbig, text, pos, Color.White);
    }
    public abstract void Initialize();
    protected bool Hovered() => 
        Globals.mouse.Position.X < bounds.X + bounds.Size.Width &&
        Globals.mouse.Position.X > bounds.X &&
        Globals.mouse.Position.Y < bounds.Y + bounds.Size.Height &&
        Globals.mouse.Position.Y > bounds.Y;
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
        if (Hovered() && enabled && button == MouseButtons.Left) Action();
    }
    protected abstract void Action();
}
