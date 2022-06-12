using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Tetris;

namespace Tetris.Core
{
    public class Piece
    {
        
        public List<Square> squares;
        readonly PieceType type;
        Direction direction;
        Vector2 position;
        readonly Random rnd;
        public bool isTurning = false;

        public Piece(Vector2 startPos)
        {
            squares = new List<Square>();
            rnd = new Random();

            direction = Direction.Up;
            type = (PieceType)rnd.Next(0, 7);
            position = startPos;

            squares.Add(new Square(type, startPos));
            Fill();
            squares.ForEach(Globals.scene.Add);
        }
        void Fill()
        {
            switch (type)
            {
                case PieceType.O:
                    squares.Add(new Square(type, position + new Vector2(1, 0)));
                    squares.Add(new Square(type, position + new Vector2(1, 1)));
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    break;
                case PieceType.I:
                    squares.Add(new Square(type, position + new Vector2(0, -1)));
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    squares.Add(new Square(type, position + new Vector2(0, 2)));
                    break;
                case PieceType.S:
                    squares.Add(new Square(type, position + new Vector2(0, -1)));
                    squares.Add(new Square(type, position + new Vector2(1, 0)));
                    squares.Add(new Square(type, position + new Vector2(1, 1)));
                    break;
                case PieceType.Z:
                    squares.Add(new Square(type, position + new Vector2(1, -1)));
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    squares.Add(new Square(type, position + new Vector2(1, 0)));
                    break;
                case PieceType.J:
                    squares.Add(new Square(type, position + new Vector2(0, -1)));
                    squares.Add(new Square(type, position + new Vector2(1, -1)));
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    break;
                case PieceType.L:
                    squares.Add(new Square(type, position + new Vector2(0, -1)));
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    squares.Add(new Square(type, position + new Vector2(1, 1)));
                    break;
                case PieceType.T:
                    squares.Add(new Square(type, position + new Vector2(0, -1)));
                    squares.Add(new Square(type, position + new Vector2(1, 0)));
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    break;
            }
        }
        public bool Move(Vector2 pos, bool force = false)
        {
            foreach (Square item in squares)
            {
                Vector2 relative = item.Position - position;
                if (Globals.scene.IsTaken(pos + relative) && !force) return false;
                item.Move(pos + relative, true);
            }
            position = pos;
            return true;
        }
        
        public bool Move(Direction direction, float amount = 1, bool force = false)
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
                if (Globals.scene.IsTaken(item.Position + change) && !force) return false;
            }
            
            squares.ForEach(delegate (Square square) { square.Move(square.Position + change, force); });

            position += change;
            
            return true;
        }
        bool MoveToPos(List<Vector2> arg)
        {
            bool taken = false;
            arg.ForEach(delegate (Vector2 pos) { if (Globals.scene.IsTaken(pos)) taken = true; });
            if (taken) return false;
            for (int i = 1; i < 4; i++)
            {
                squares[i].Move(arg[i - 1]);
            }
            return true;
        }
        public void Turn()
        {
            GameScene scene = Globals.scene;
            isTurning = true;
            List<Vector2> list = new List<Vector2>();
            switch (type)
            {
                case PieceType.O:
                    isTurning = false;
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
                    if (direction == Direction.Up)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (scene.IsTaken(new Vector2(position.X - 2 + i, position.Y + 1))) return;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            squares[i].Move(new Vector2(position.X - 2 + i, position.Y + 1));
                        }
                        position += new Vector2(0, 1);
                        direction = Direction.Right;
                        isTurning = false;
                        return;
                    }
                    if (direction == Direction.Right)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (scene.IsTaken(new Vector2(position.X - 1, position.Y - 2 + i))) return;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            squares[i].Move(new Vector2(position.X - 1, position.Y - 2 + i));
                        }
                        position += new Vector2(-1, 0);
                        direction = Direction.Down;
                        isTurning = false;
                        return;
                    }
                    if (direction == Direction.Down)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (scene.IsTaken(new Vector2(position.X - 1 + i, position.Y - 1))) return;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            squares[i].Move(new Vector2(position.X - 1 + i, position.Y - 1));
                        }
                        position += new Vector2(0, -1);
                        direction = Direction.Left;
                        isTurning = false;
                        return;
                    }
                    if (direction == Direction.Left)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (scene.IsTaken(new Vector2(position.X + 1, position.Y - 1 + i))) return;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            squares[i].Move(new Vector2(position.X + 1, position.Y - 1 + i));
                        }
                        position += new Vector2(1, 0);
                        direction = Direction.Up;
                        isTurning = false;
                        return;
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

            MoveToPos(list);
            isTurning = false;
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
        public new PieceType GetType()
        {
            return type;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
    }
}
