using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Tetris.Core;

public class Piece
{
    public List<Square> squares;
    PieceType Type;
    Direction direction;
    Vector2 position;

    public Piece(PieceType type, Vector2 startPos)
    {
        squares = new List<Square>();

        direction = Direction.Up;
        Type = type;
        position = startPos;

        squares.Add(new Square(Type, startPos));
        Fill();
        squares.ForEach(Game1.scene.Add);
    }
    void Fill()
    {
        switch (Type)
        {
            case PieceType.O:
                squares.Add(new Square(Type, position + new Vector2(1, 0)));
                squares.Add(new Square(Type, position + new Vector2(1, 1)));
                squares.Add(new Square(Type, position + new Vector2(0, 1)));
                break;
            case PieceType.I:
                squares.Add(new Square(Type, position + new Vector2(0, -1)));
                squares.Add(new Square(Type, position + new Vector2(0, 1)));
                squares.Add(new Square(Type, position + new Vector2(0, 2)));
                break;
            case PieceType.S:
                squares.Add(new Square(Type, position + new Vector2(0, -1)));
                squares.Add(new Square(Type, position + new Vector2(1, 0)));
                squares.Add(new Square(Type, position + new Vector2(1, 1)));
                break;
            case PieceType.Z:
                squares.Add(new Square(Type, position + new Vector2(1, -1)));
                squares.Add(new Square(Type, position + new Vector2(0, 1)));
                squares.Add(new Square(Type, position + new Vector2(1, 0)));
                break;
            case PieceType.J:
                squares.Add(new Square(Type, position + new Vector2(0, -1)));
                squares.Add(new Square(Type, position + new Vector2(1, -1)));
                squares.Add(new Square(Type, position + new Vector2(0, 1)));
                break;
            case PieceType.L:
                squares.Add(new Square(Type, position + new Vector2(0, -1)));
                squares.Add(new Square(Type, position + new Vector2(0, 1)));
                squares.Add(new Square(Type, position + new Vector2(1, 1)));
                break;
            case PieceType.T:
                squares.Add(new Square(Type, position + new Vector2(0, -1)));
                squares.Add(new Square(Type, position + new Vector2(1, 0)));
                squares.Add(new Square(Type, position + new Vector2(0, 1)));
                break;
        }
    }
    public bool MoveTo(Vector2 pos)
    {
        foreach (Square item in squares)
        {
            Vector2 relative = item.Bounds.Position - position;
            if (!Game1.scene.CanMoveInto(pos + relative)) return false;
            item.Move(pos + relative, true);
        }
        position = pos;
        return true;
    }
    
    public bool Step(Direction direction, float amount = 1)
    {
        Vector2 change = direction switch
        {
            Direction.Left => new Vector2(-amount, 0),
            Direction.Right => new Vector2(amount, 0),
            Direction.Down => new Vector2(0, amount),
            Direction.Up => new Vector2(0, -amount),
            _ => new Vector2(0)
        };
        
        foreach (Square item in squares)
        {
            if (!Game1.scene.CanMoveInto(item.Bounds.Position + change)) return false;
        }
        
        squares.ForEach(delegate (Square square) { square.Move(square.Bounds.Position + change); });

        position += change;
        
        return true;
    }
    bool SetSquaresTo(List<Vector2> arg)
    {
        var taken = false;
        arg.ForEach(delegate (Vector2 pos) { if (!Game1.scene.CanMoveInto(pos)) taken = true; });
        if (taken) return false;
        for (int i = 1; i < 4; i++)
        {
            squares[i].Move(arg[i - 1]);
        }
        return true;
    }
    public void Turn()
    {
        GameScene scene = Game1.scene;
        List<Vector2> list = new();
        switch (Type)
        {
            case PieceType.O:
                return;
            case PieceType.T:
                switch (direction)
                {
                    case Direction.Up:
                        list = new List<Vector2>
                        {
                            position + new Vector2(1, 0),
                            position + new Vector2(0, 1),
                            position + new Vector2(-1, 0)
                        };
                        direction = Direction.Right;
                        break;
                    case Direction.Right:
                        list = new List<Vector2>
                        {
                            position + new Vector2(0, -1),
                            position + new Vector2(0, 1),
                            position + new Vector2(-1, 0)
                        };
                        direction = Direction.Down;
                        break;
                    case Direction.Down:
                        list = new List<Vector2>
                        {
                            position + new Vector2(0, -1),
                            position + new Vector2(1, 0),
                            position + new Vector2(-1, 0)
                        };
                        direction = Direction.Left;
                        break;
                    case Direction.Left:
                        list = new List<Vector2>
                        {
                            position + new Vector2(0, -1),
                            position + new Vector2(1, 0),
                            position + new Vector2(0, 1)
                        };
                        direction = Direction.Up;
                        break;
                }
                break;
            case PieceType.I:
                switch (direction)
                {
                    case Direction.Up:
                        for (int i = 0; i < 4; i++)
                        {
                            if (!scene.CanMoveInto(new Vector2(position.X - 2 + i, position.Y + 1))) return;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            squares[i].Move(new Vector2(position.X - 2 + i, position.Y + 1));
                        }
                        position += new Vector2(0, 1);
                        direction = Direction.Right;
                        break;
                    case Direction.Down:
                        for (int i = 0; i < 4; i++)
                        {
                            if (!scene.CanMoveInto(new Vector2(position.X - 1 + i, position.Y - 1))) return;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            squares[i].Move(new Vector2(position.X - 1 + i, position.Y - 1));
                        }
                        position += new Vector2(0, -1);
                        direction = Direction.Left;
                        break;
                    case Direction.Left:
                        for (int i = 0; i < 4; i++)
                        {
                            if (!scene.CanMoveInto(new Vector2(position.X + 1, position.Y - 1 + i))) return;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            squares[i].Move(new Vector2(position.X + 1, position.Y - 1 + i));
                        }
                        position += new Vector2(1, 0);
                        direction = Direction.Up;
                        break;
                    case Direction.Right:
                        for (int i = 0; i < 4; i++)
                        {
                            if (!scene.CanMoveInto(new Vector2(position.X - 1, position.Y - 2 + i))) return;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            squares[i].Move(new Vector2(position.X - 1, position.Y - 2 + i));
                        }
                        position += new Vector2(-1, 0);
                        direction = Direction.Down;
                        break;
                }
                break;
            case PieceType.S:
                switch (direction)
                {
                    case Direction.Up:
                        list = new List<Vector2>
                        {
                            position + new Vector2(1, 0),
                            position + new Vector2(0, 1),
                            position + new Vector2(-1, 1)
                        };
                        direction = Direction.Right;
                        break;
                    case Direction.Right:
                        list = new List<Vector2>
                        {
                            position + new Vector2(-1, -1),
                            position + new Vector2(-1, 0),
                            position + new Vector2(0, 1)
                        };
                        direction = Direction.Down;
                        break;
                    case Direction.Down:
                        list = new List<Vector2>
                        {
                            position + new Vector2(0, -1),
                            position + new Vector2(-1, 0),
                            position + new Vector2(1, -1)
                        };
                        direction = Direction.Left;
                        break;
                    case Direction.Left:
                        list = new List<Vector2>
                        {
                            position + new Vector2(0, -1),
                            position + new Vector2(1, 0),
                            position + new Vector2(1, 1)
                        };
                        direction = Direction.Up;
                        break;
                }
                break;
            case PieceType.Z:
                switch (direction)
                {
                    case Direction.Up:
                        list = new List<Vector2>
                        {
                            position + new Vector2(-1, 0),
                            position + new Vector2(0, 1),
                            position + new Vector2(1, 1)
                        };
                        direction = Direction.Right;
                        break;
                    case Direction.Right:
                        list = new List<Vector2>
                        {
                            position + new Vector2(-1, 0),
                            position + new Vector2(0, -1),
                            position + new Vector2(-1, 1)
                        };
                        direction = Direction.Down;
                        break;
                    case Direction.Down:
                        list = new List<Vector2>
                        {
                            position + new Vector2(-1, -1),
                            position + new Vector2(0, -1),
                            position + new Vector2(1, 0)
                        };
                        direction = Direction.Left;
                        break;
                    case Direction.Left:
                        list = new List<Vector2>
                        {
                            position + new Vector2(1, -1),
                            position + new Vector2(1, 0),
                            position + new Vector2(0, 1)
                        };
                        direction = Direction.Up;
                        break;
                }
                break;
            case PieceType.J:
                switch (direction)
                {
                    case Direction.Up:
                        list = new List<Vector2>
                        {
                            position + new Vector2(1, 0),
                            position + new Vector2(1, 1),
                            position + new Vector2(-1, 0)
                        };
                        direction = Direction.Right;
                        break;
                    case Direction.Right:
                        list = new List<Vector2>
                        {
                            position + new Vector2(0, -1),
                            position + new Vector2(0, 1),
                            position + new Vector2(-1, 1)
                        };
                        direction = Direction.Down;
                        break;
                    case Direction.Down:
                        list = new List<Vector2>
                        {
                            position + new Vector2(-1, -1),
                            position + new Vector2(-1, 0),
                            position + new Vector2(1, 0)
                        };

                        direction = Direction.Left;
                        break;
                    case Direction.Left:
                        list = new List<Vector2>
                        {
                            position + new Vector2(0, -1),
                            position + new Vector2(1, -1),
                            position + new Vector2(0, 1)
                        };
                        direction = Direction.Up;
                        break;
                }
                break;
            case PieceType.L:
                switch (direction)
                {
                    case Direction.Up:
                        list = new List<Vector2>
                        {
                            position + new Vector2(-1, 0),
                            position + new Vector2(1, 0),
                            position + new Vector2(-1, 1)
                        };
                        direction = Direction.Right;
                        break;
                    case Direction.Right:
                        list = new List<Vector2>
                        {
                            position + new Vector2(-1, -1),
                            position + new Vector2(0, -1),
                            position + new Vector2(0, 1)
                        };
                        direction = Direction.Down;
                        break;
                    case Direction.Down:
                        list = new List<Vector2>
                        {
                            position + new Vector2(-1, 0),
                            position + new Vector2(1, 0),
                            position + new Vector2(1, -1)
                        };

                        direction = Direction.Left;
                        break;
                    case Direction.Left:
                        list = new List<Vector2>
                        {
                            position + new Vector2(0, -1),
                            position + new Vector2(1, 1),
                            position + new Vector2(0, 1)
                        };
                        direction = Direction.Up;
                        break;
                }
                break;
        }
        SetSquaresTo(list);
    }
    public bool Fall()
    {
        foreach (Square item in squares)
        {
            if (!item.CheckMove(Direction.Down)) return false;
        }

        squares.ForEach(delegate (Square square) { square.Move(Direction.Down); });

        position += new Vector2(0, 1);
        return true;
    }
}
