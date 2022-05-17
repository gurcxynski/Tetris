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
        Vector2 middle;
        public Piece(Type typeArg, Vector2 startPos)
        {
            squares = new List<Square>();
            type = typeArg;
            TileColor color = TileColor.red;
            middle = startPos;
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
                    squares.Add(new Square(color, startPos + new Vector2(-1, 0)));
                    squares.Add(new Square(color, startPos + new Vector2(1, 1)));
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
                item.inPiece = true;
                Globals.scene.Add(item);
            }
        }
        public Vector2 GetMid()
        {
            return middle;
        }
        public Vector2 Hold()
        {
            Vector2 prevMid = middle;
            middle = new Vector2(15, 16);
            foreach (var item in squares)
            {
                Vector2 relative = item.GetPos() - prevMid;
                item.Move(middle + relative);
            }
            return prevMid;
        }
        public void MoveTo(Vector2 pos)
        {
            foreach (var item in squares)
            {
                Vector2 relative = item.GetPos() - middle;
                item.Move(pos + relative);
            }
            middle = pos;
        }
        public new Type GetType()
        {
            return type;
        }
        public Vector2 GetPos()
        {
            return middle;
        }
        public void Remove()
        {
            squares.RemoveRange(0, squares.Count);
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
            middle += new Vector2(0, 1);
            return true;
        }
        public bool MoveDown(int n)
        {
            foreach (var item in squares)
            {
                for (int i = 0; i < n; i++)
                {
                    item.MoveDown();
                }

            }
            middle += new Vector2(0, n);
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
            middle += new Vector2(-1, 0);
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
            middle += new Vector2(1, 0);
            return true;
        }
    }
}
