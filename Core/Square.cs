using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Tetris;

namespace Tetris.Core
{
    public enum TileColor { red, green, blue, yellow, orange, pink, purple };

    public class Square
    {
        private TileColor color { get; set; }
        private Vector2 position;
        public Vector2 GetPos()
        {
            return position;
        }
        public Square(TileColor color, Vector2 position)
        {
            this.color = color;
            this.position = position;
        }

        public bool CheckMoveDown()
        {
            return position.Y < Globals.maxY - 1 && !Globals.scene.isTaken(position + new Vector2(0, 1));
        }
        public bool CheckMoveRight()
        {
            return position.X < Globals.maxX - 1 && !Globals.scene.isTaken(position + new Vector2(1, 0));
        }
        public bool CheckMoveLeft()
        {
            return position.X > 0 && !Globals.scene.isTaken(position + new Vector2(-1, 0));
        }
        public void MoveDown()
        {
            position += new Vector2(0, 1);
        }
        public void MoveLeft()
        {
            position += new Vector2(-1, 0);
        }
        public void MoveRight()
        {
            position += new Vector2(1, 0);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.textures[this.color], this.position * 30, Color.White);
        }
    }
}
