using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.EasyInput;

namespace Tetris.Core
{
    public abstract class Button
    {
        protected Vector2 position;
        protected Texture2D texture;
        protected string text;
        protected bool hovered = false;
        protected Button()
        {
            Globals.mouse.OnMouseButtonPressed += OnClick; 
            texture = Globals.buttonTextures.Key;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
             spriteBatch.Draw(texture, position, Color.White);
             spriteBatch.DrawString(Globals.font, text, new Vector2((position.X + texture.Width / 2) - Globals.font.MeasureString(text).X / 2, position.Y + 5), Color.Black);
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
        public void Update()
        {
            hovered = EnteredButton();

            if (hovered && !Globals.gameRunning) texture = Globals.buttonTextures.Value;
            else texture = Globals.buttonTextures.Key;
        }

        protected abstract void OnClick(MouseButtons button);
    }
}
