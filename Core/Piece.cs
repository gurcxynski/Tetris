using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.Core
{
    internal class Piece
    {
        public enum Type { O, I, S, Z, L, J, T }
        public List<Square> squares;
        Type type;
        public Piece(Type typeArg, Vector2 startPos, TileColor color)
        {
            squares = new List<Square>();
            squares.Add(new Square(color, startPos));
            type = typeArg;
            switch (type)
            {
                case Type.O:
                    squares.Add(new Square(color, startPos + new Vector2(1, 0)));
                    squares.Add(new Square(color, startPos + new Vector2(1, 1)));
                    squares.Add(new Square(color, startPos + new Vector2(0, 1)));
                    break;
                case Type.I:
                    squares.Add(new Square(color, startPos + new Vector2(0, 1)));
                    squares.Add(new Square(color, startPos + new Vector2(0, 2)));
                    squares.Add(new Square(color, startPos + new Vector2(0, 3)));
                    break;
                case Type.S:
                    squares.Add(new Square(color, startPos + new Vector2(0, 1)));
                    squares.Add(new Square(color, startPos + new Vector2(1, 0)));
                    squares.Add(new Square(color, startPos + new Vector2(-1, 1)));
                    break;
                case Type.Z:
                    squares.Add(new Square(color, startPos + new Vector2(0, 1)));
                    squares.Add(new Square(color, startPos + new Vector2(1, 0)));
                    squares.Add(new Square(color, startPos + new Vector2(-1, 1)));
                    break;
                case Type.J:
                    squares.Add(new Square(color, startPos + new Vector2(0, 1)));
                    squares.Add(new Square(color, startPos + new Vector2(0, 2)));
                    squares.Add(new Square(color, startPos + new Vector2(-1, 2)));
                    break;
                case Type.L:
                    squares.Add(new Square(color, startPos + new Vector2(0, 1)));
                    squares.Add(new Square(color, startPos + new Vector2(0, 2)));
                    squares.Add(new Square(color, startPos + new Vector2(1, 2)));
                    break;
                case Type.T:
                    squares.Add(new Square(color, startPos + new Vector2(0, 1)));
                    squares.Add(new Square(color, startPos + new Vector2(1, 0)));
                    squares.Add(new Square(color, startPos + new Vector2(-1, 0)));
                    break;
            }
            foreach (var item in squares)
            {
                Globals.scene.Add(item);
            }
        }
        public bool Move()
        {
            foreach (var item in squares)
            {
                if (!item.CheckMove()) return false;
            }
            foreach (var item in squares)
            {
                item.Move();
            }
            return true;
        }
    }
}
