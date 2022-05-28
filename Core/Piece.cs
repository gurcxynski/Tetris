using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tetris.Core
{
    public class Piece
    {
        public enum Type { O, I, S, Z, L, J, T }
        public enum Direction { Up, Right, Down, Left }
        public List<Square> squares;
        readonly Type type;
        Direction direction;
        Vector2 position;
        readonly Random rnd;
        public bool isTurning = false;

        public Piece(Vector2 startPos)
        {
            squares = new List<Square>();
            rnd = new Random();

            direction = Direction.Up;
            type = (Type)rnd.Next(0, 7);
            position = startPos;

            squares.Add(new Square(type, startPos));
            Fill();
            squares.ForEach(Globals.scene.Add);
        }
        void Fill()
        {
            switch (type)
            {
                case Type.O:
                    squares.Add(new Square(type, position + new Vector2(1, 0)));
                    squares.Add(new Square(type, position + new Vector2(1, 1)));
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    break;
                case Type.I:
                    squares.Add(new Square(type, position + new Vector2(0, -1)));
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    squares.Add(new Square(type, position + new Vector2(0, 2)));
                    break;
                case Type.S:
                    squares.Add(new Square(type, position + new Vector2(0, -1)));
                    squares.Add(new Square(type, position + new Vector2(1, 0)));
                    squares.Add(new Square(type, position + new Vector2(1, 1)));
                    break;
                case Type.Z:
                    squares.Add(new Square(type, position + new Vector2(1, -1)));
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    squares.Add(new Square(type, position + new Vector2(1, 0)));
                    break;
                case Type.J:
                    squares.Add(new Square(type, position + new Vector2(0, -1)));
                    squares.Add(new Square(type, position + new Vector2(1, -1)));
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    break;
                case Type.L:
                    squares.Add(new Square(type, position + new Vector2(0, -1)));
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    squares.Add(new Square(type, position + new Vector2(1, 1)));
                    break;
                case Type.T:
                    squares.Add(new Square(type, position + new Vector2(0, -1)));
                    squares.Add(new Square(type, position + new Vector2(1, 0)));
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    break;
            }
        }
        public void MoveTo(Vector2 pos)
        {
            foreach (var item in squares)
            {
                Vector2 relative = item.GetPos() - position;
                item.ForceMoveTo(pos + relative);
            }
            position = pos;
        }
        
        public bool Move(Direction direction)
        {
            foreach (var item in squares)
            {
                if (!item.CheckMove(direction)) return false;
            }

            squares.ForEach(delegate (Square square) { square.Move(direction); });

            position += direction switch
            {
                Direction.Left => new Vector2(-1, 0),
                Direction.Right => new Vector2(1, 0),
                Direction.Down => new Vector2(0, 1),
                _ => new Vector2(0, 0)
            };
            return true;
        }
        public bool Move(Direction direction, float amount)
        {
            squares.ForEach(delegate (Square square) { square.Move(direction, amount); });
            position += direction switch
            {
                Direction.Left => new Vector2(-amount, 0),
                Direction.Right => new Vector2(amount, 0),
                Direction.Down => new Vector2(0, amount),
                _ => new Vector2(0, 0)
            };
            return true;
        }
        bool MoveToPos(List<Vector2> arg)
        {
            bool taken = false;
            arg.ForEach(delegate (Vector2 pos) { if (Globals.scene.IsTaken(pos)) taken = true; });
            if(taken) return false;
            for (int i = 1; i < 4; i++)
                {
                    squares[i].MoveTo(arg[i - 1]);
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
                case Type.O:
                    isTurning = false;
                    return;
                case Type.T:
                    if (direction == Direction.Up)
                    {
                        if(squares[1].MoveTo(position + new Vector2(-1, 0)))
                        {
                            direction = Direction.Right;
                        }
                        isTurning = false;
                        return;
                    }
                    if (direction == Direction.Right)
                    {
                        if(squares[2].MoveTo(position + new Vector2(0, -1)))
                        {
                            direction = Direction.Down;
                        }
                        isTurning = false;
                        return;
                    }
                    if (direction == Direction.Down)
                    {
                        if (squares[3].MoveTo(position + new Vector2(1, 0)))
                        {
                            direction = Direction.Left;
                        }
                        isTurning = false;
                        return;
                    }
                    if (direction == Direction.Left)
                    {
                        if (squares[1].MoveTo(position + new Vector2(0, 1)))
                        {
                            direction = Direction.Up;
                        }
                        isTurning = false;
                        return;
                    }
                    break;
                case Type.I:
                    if (direction == Direction.Up)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (scene.IsTaken(new Vector2(position.X - 2 + i, position.Y + 1))) return;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            squares[i].MoveTo(new Vector2(position.X - 2 + i, position.Y + 1));
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
                            squares[i].MoveTo(new Vector2(position.X - 1, position.Y - 2 + i));
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
                            squares[i].MoveTo(new Vector2(position.X - 1 + i, position.Y - 1));
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
                            squares[i].MoveTo(new Vector2(position.X + 1, position.Y - 1 + i));
                        }
                        position += new Vector2(1, 0);
                        direction = Direction.Up;
                        isTurning = false;
                        return;
                    }
                    break;
                case Type.S:
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
                                position + new Vector2(-1, 0),
                                position + new Vector2(0, 1),
                                position + new Vector2(1, 1)
                            };
                            direction = Direction.Up;
                            break;
                    }
                    break;
                case Type.Z:
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
                case Type.J:
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

                case Type.L:
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
            foreach (var item in squares)
            {
                if (!item.CheckMove(Direction.Down)) return false;
            }

            squares.ForEach(delegate (Square square) { square.Move(Direction.Down);});

            position += new Vector2(0, 1);
            return true;
        }
        public new Type GetType()
        {
            return type;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
    }
}
