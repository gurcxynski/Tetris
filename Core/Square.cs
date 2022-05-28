using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris.Core
{

    public class Square
    {
        private readonly Piece.Type _type;
        private Vector2 _position;
        public bool toMoveWhenCleared = false;
        public Square(Piece.Type type, Vector2 position)
        {
            _position = position;
            _type = type;
        }
        public bool CheckMove(Piece.Direction direction)
        {
            return direction switch
            {
                Piece.Direction.Left => _position.X > 0 && !Globals.scene.IsTaken(_position + new Vector2(-1, 0)),
                Piece.Direction.Right => _position.X < Globals.maxX - 1 && !Globals.scene.IsTaken(_position + new Vector2(1, 0)),
                Piece.Direction.Down => _position.Y < Globals.maxY - 1 && !Globals.scene.IsTaken(_position + new Vector2(0, 1)),
                _ => false,
            };
        }
        
        public bool MoveTo(Vector2 pos)
        {
            if (Globals.scene.IsTaken(_position)) return false;
            _position = pos;
            return true;
        }
        public void ForceMoveTo(Vector2 pos)
        {
            _position = pos;
        }
        public void Move(Piece.Direction direction, float amount = 1)
        {
            switch (direction)
            {
                case Piece.Direction.Left:
                    _position += new Vector2(-amount, 0);
                    break;
                case Piece.Direction.Right:
                    _position += new Vector2(amount, 0);
                    break;
                case Piece.Direction.Up:
                    _position += new Vector2(0, -amount);
                    break;
                case Piece.Direction.Down:
                    _position += new Vector2(0, amount);
                    break;
            }
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
