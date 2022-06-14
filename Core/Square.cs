using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Core
{

    public class Square
    {
        public PieceType Type { get; }
        public Vector2 Position { get; set; }

        public bool toMoveWhenCleared = false;
        
        public Square(PieceType type, Vector2 position)
        {
            Type = type;
            Position = position;
        }
        public bool CheckMove(Direction direction, float amount = 1)
        {
            Vector2 newPos = Position + direction switch
            {
                Direction.Left => new Vector2(-amount, 0),
                Direction.Right => new Vector2(amount, 0),
                Direction.Down => new Vector2(0, amount),
                Direction.Up => new Vector2(0, -amount),
                _ => new Vector2(0)
            };

            return !Globals.scene.IsTaken(newPos);
        }
        public bool Move(Vector2 pos, bool force = false)
        {
            if (Globals.scene.IsTaken(Position) && !force) return false;
            Position = pos;
            return true;
        }
        
        public void Move(Direction direction, float amount = 1, bool force = false)
        {
            Vector2 newPos = Position + direction switch
            {
                Direction.Left => new Vector2(-amount, 0),
                Direction.Right => new Vector2(amount, 0),
                Direction.Down => new Vector2(0, amount),
                Direction.Up => new Vector2(0, -amount),
                _ => new Vector2(0)
            };
            
            Move(newPos, force);
        }
        public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(Globals.blockTextures[Type], (Position * 30) + new Vector2(22, 0), Color.White);
    }
}
