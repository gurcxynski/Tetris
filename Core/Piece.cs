using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.Core
{
    public class Piece
    {
        public enum Type { O, I, S, Z, L, J, T }
        public List<Square> squares;
        Type type;
        public Piece(Type typeArg, Vector2 startPos)
        {
            squares = new List<Square>();
            type = typeArg;
            TileColor color = TileColor.red;
            switch(type)
            {
                case Type.O:
                    color = TileColor.yellow;
                    break;
                case Type.I:
                    color = TileColor.blue;
                    break;
                case Type.S:
                    color = TileColor.red;
                    break;
                case Type.Z:
                    color = TileColor.green;
                    break;
                case Type.L:
                    color = TileColor.orange;
                    break;
                case Type.J:
                    color = TileColor.pink;
                    break;
                case Type.T:
                    color = TileColor.purple;
                    break;
            }
            squares.Add(new Square(color, startPos));
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
        public new Type GetType()
        {
            return type;
        }
        public bool MoveDown()
        {
            foreach (var item in squares)
            {
                if (!item.CheckMoveDown()) return false;
            }
            foreach (var item in squares)
            {
                item.MoveDown();
            }
            return true;
        }
        public bool MoveLeft()
        {
            foreach (var item in squares)
            {
                if (!item.CheckMoveLeft()) return false;
            }
            foreach (var item in squares)
            {
                item.MoveLeft();
            }
            return true;
        }
        public bool MoveRight()
        {
            foreach (var item in squares)
            {
                if (!item.CheckMoveRight()) return false;
            }
            foreach (var item in squares)
            {
                item.MoveRight();
            }
            return true;
        }
    }
}
