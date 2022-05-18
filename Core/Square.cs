using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris.Core
{

    public class Square
    {
        private readonly Piece.Type _type;
        private Vector2 _position;
        
        public Square(Piece.Type type, Vector2 position)
        {
            _position = position;
            _type = type;
        }
        public bool CheckMove(Keys direction)
        {
            return direction switch
            {
                Keys.Left => _position.X > 0 && !Globals.scene.IsTaken(_position + new Vector2(-1, 0)),
                Keys.Right => _position.X < Globals.maxX - 1 && !Globals.scene.IsTaken(_position + new Vector2(1, 0)),
                Keys.Down => _position.Y < Globals.maxY - 1 && !Globals.scene.IsTaken(_position + new Vector2(0, 1)),
                _ => false,
            };
        }
        
        public void MoveTo(Vector2 pos)
        {
            _position = pos;
        }
        public void Move(Keys direction)
        {
            if (direction == Keys.Left) _position += new Vector2(-1, 0);
            if (direction == Keys.Right) _position += new Vector2(1, 0);
            if (direction == Keys.Down) _position += new Vector2(0, 1);
        }
        public void Move(Keys direction, float amount)
        {
            if (direction == Keys.Left) _position += new Vector2(-amount, 0);
            if (direction == Keys.Right) _position += new Vector2(amount, 0);
            if (direction == Keys.Down) _position += new Vector2(0, amount);
        }
        public Vector2 GetPos()
        {
            return _position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.blockTextures[_type], (_position * 30) + new Vector2(22, 0), Color.White);
        }
    }
}
