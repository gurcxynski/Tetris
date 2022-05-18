using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Tetris.Core
{
    public class Piece
    {
        public enum Type { O, I, S, Z, L, J, T }

        public List<Square> squares;
        readonly Type type;
        Vector2 position;
        readonly Random rnd;
        public Piece(Vector2 startPos)
        {
            squares = new List<Square>();
            rnd = new Random();

            type = (Type)rnd.Next(0, 7);
            position = startPos;

            squares.Add(new Square(type, startPos));
            switch (type)
            {
                case Type.O:
                    squares.Add(new Square(type, startPos + new Vector2(1, 0)));
                    squares.Add(new Square(type, startPos + new Vector2(1, 1)));
                    squares.Add(new Square(type, startPos + new Vector2(0, 1)));
                    break;
                case Type.I:
                    squares.Add(new Square(type, startPos + new Vector2(0, 1)));
                    squares.Add(new Square(type, startPos + new Vector2(0, 2)));
                    squares.Add(new Square(type, startPos + new Vector2(0, 3)));
                    break;
                case Type.S:
                    squares.Add(new Square(type, startPos + new Vector2(0, 1)));
                    squares.Add(new Square(type, startPos + new Vector2(1, 0)));
                    squares.Add(new Square(type, startPos + new Vector2(-1, 1)));
                    break;
                case Type.Z:
                    squares.Add(new Square(type, startPos + new Vector2(0, 1)));
                    squares.Add(new Square(type, startPos + new Vector2(-1, 0)));
                    squares.Add(new Square(type, startPos + new Vector2(1, 1)));
                    break;
                case Type.J:
                    squares.Add(new Square(type, startPos + new Vector2(0, 1)));
                    squares.Add(new Square(type, startPos + new Vector2(0, 2)));
                    squares.Add(new Square(type, startPos + new Vector2(-1, 2)));
                    break;
                case Type.L:
                    squares.Add(new Square(type, startPos + new Vector2(0, 1)));
                    squares.Add(new Square(type, startPos + new Vector2(0, 2)));
                    squares.Add(new Square(type, startPos + new Vector2(1, 2)));
                    break;
                case Type.T:
                    squares.Add(new Square(type, startPos + new Vector2(0, 1)));
                    squares.Add(new Square(type, startPos + new Vector2(1, 0)));
                    squares.Add(new Square(type, startPos + new Vector2(-1, 0)));
                    break;
            }

            squares.ForEach(Globals.scene.Add);
        }
        public Piece()
        {
            squares = new List<Square>();
            rnd = new Random();

            type = (Type)rnd.Next(0, 7);
            position = Globals.queueLastPos;

            squares.Add(new Square(type, position));
            switch (type)
            {
                case Type.O:
                    squares.Add(new Square(type, position + new Vector2(1, 0)));
                    squares.Add(new Square(type, position + new Vector2(1, 1)));
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    break;
                case Type.I:
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    squares.Add(new Square(type, position + new Vector2(0, 2)));
                    squares.Add(new Square(type, position + new Vector2(0, 3)));
                    break;
                case Type.S:
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    squares.Add(new Square(type, position + new Vector2(1, 0)));
                    squares.Add(new Square(type, position + new Vector2(-1, 1)));
                    break;
                case Type.Z:
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    squares.Add(new Square(type, position + new Vector2(-1, 0)));
                    squares.Add(new Square(type, position + new Vector2(1, 1)));
                    break;
                case Type.J:
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    squares.Add(new Square(type, position + new Vector2(0, 2)));
                    squares.Add(new Square(type, position + new Vector2(-1, 2)));
                    break;
                case Type.L:
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    squares.Add(new Square(type, position + new Vector2(0, 2)));
                    squares.Add(new Square(type, position + new Vector2(1, 2)));
                    break;
                case Type.T:
                    squares.Add(new Square(type, position + new Vector2(0, 1)));
                    squares.Add(new Square(type, position + new Vector2(1, 0)));
                    squares.Add(new Square(type, position + new Vector2(-1, 0)));
                    break;
            }

            squares.ForEach(Globals.scene.Add);
        }

        public Vector2 Hold()
        {
            Vector2 prevMid = position;
            position = Globals.holdPos;
            foreach (var item in squares)
            {
                Vector2 relative = item.GetPos() - prevMid;
                item.MoveTo(position + relative);
            }
            return prevMid;
        }
        public void MoveTo(Vector2 pos)
        {
            foreach (var item in squares)
            {
                Vector2 relative = item.GetPos() - position;
                item.MoveTo(pos + relative);
            }
            position = pos;
        }
        
        public bool Move(Keys direction)
        {
            foreach (var item in squares)
            {
                if (!item.CheckMove(direction)) return false;
            }

            squares.ForEach(delegate (Square square) { square.Move(direction); });

            position += direction switch
            {
                Keys.Left => new Vector2(-1, 0),
                Keys.Right => new Vector2(1, 0),
                Keys.Down => new Vector2(0, 1),
                _ => new Vector2(0, 0)
            };
            return true;
        }
        public bool Move(Keys direction, float amount)
        {
            squares.ForEach(delegate (Square square) { square.Move(direction, amount); });
            position += direction switch
            {
                Keys.Left => new Vector2(-amount, 0),
                Keys.Right => new Vector2(amount, 0),
                Keys.Down => new Vector2(0, amount),
                _ => new Vector2(0, 0)
            };
            return true;
        }
        public bool Fall()
        {
            foreach (var item in squares)
            {
                if (!item.CheckMove(Keys.Down)) return false;
            }

            squares.ForEach(delegate (Square square) { square.Move(Keys.Down);});

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
