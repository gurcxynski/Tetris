using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Tetris;

namespace Tetris.Core
{
    public enum TileColor { red, green, blue };

    public class Square
    {
        private TileColor color { get; set; }
        private Vector2 position { get; set; }

        public Square(TileColor color, Vector2 position)
        {
            this.color = color;
            this.position = position;
        }

        public bool CheckMove()
        {
            return position.Y != 22;
        }
        public void Move()
        {
            position += new Vector2(0, 1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Globals.textures[this.color], this.position * 30, Color.White);
            spriteBatch.End();
        }
    }
}
