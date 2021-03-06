using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.EasyInput;

namespace Tetris.Core
{
    public abstract class Button
    {
        protected Vector2 position;
        protected Texture2D texture;
        protected bool hovered = false;
        protected int id;
        protected bool enabled = false;
        protected Button(Vector2 arg)
        {
            Globals.mouse.OnMouseButtonPressed += OnClick;
            texture = Globals.buttonTextures[(id, false)];
            position = arg;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
        protected bool EnteredButton()
        {
            if (Globals.mouse.Position.X < position.X + texture.Width &&
                    Globals.mouse.Position.X > position.X &&
                    Globals.mouse.Position.Y < position.Y + texture.Height &&
                    Globals.mouse.Position.Y > position.Y)
            {
                return true;
            }
            return false;
        }
        public virtual void Update()
        {
            hovered = EnteredButton();

            texture = Globals.buttonTextures[(id, hovered)];
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
            if (hovered && enabled && button == MouseButtons.Left)
            {
                Action();
            }
        }
        protected abstract void Action();
    }
}
