using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Tetris.Core;
public class Square
{
    Color color;
    public RectangleF Bounds { get; private set; }
    public Vector2 gridPosition;
    public Square(PieceType type, Vector2 position, int size = Config.cellSize)
    {
        color = type switch
        {
            PieceType.Z => Color.Red,
            PieceType.I => Color.Blue,
            PieceType.S => Color.Green,
            PieceType.O => Color.Yellow,
            PieceType.J => Color.Pink,
            PieceType.T => Color.Purple,
            PieceType.L => Color.Orange,
            _ => Color.White,
        };
        Bounds = new(position * Config.cellSize + Config.margin, new Size2(size, size));
        gridPosition = position;
    }
    public bool CheckMove(Direction direction, float amount = 1)
    {
        Vector2 newPos = gridPosition + direction switch
        {
            Direction.Left => new Vector2(-amount, 0),
            Direction.Right => new Vector2(amount, 0),
            Direction.Down => new Vector2(0, amount),
            Direction.Up => new Vector2(0, -amount),
            _ => new Vector2(0)
        };

        return Game1.scene.CanMoveInto(newPos);
    }
    public bool Move(Vector2 target)
    {
        if (!Game1.scene.CanMoveInto(target)) return false;
        Bounds = new(target * Config.cellSize + Config.margin, new Size2(Config.cellSize, Config.cellSize));
        gridPosition = target;
        return true;
    }

    public void Move(Direction direction, float amount = 1)
    {
        Vector2 newPos = gridPosition + direction switch
        {
            Direction.Left => new Vector2(-amount, 0),
            Direction.Right => new Vector2(amount, 0),
            Direction.Down => new Vector2(0, amount),
            Direction.Up => new Vector2(0, -amount),
            _ => new Vector2(0)
        };

        Move(newPos);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.FillRectangle(Bounds, color);
        spriteBatch.DrawRectangle(Bounds, Color.Black, 1);
    }
}
