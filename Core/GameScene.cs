using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using MonoGame.EasyInput;
using Microsoft.Xna.Framework.Input;

namespace Tetris.Core
{
    public class GameScene
    {
        List<Square> squares;
        Piece piece;
        public Piece hold;
        double sinceMove = 0;
        readonly EasyKeyboard keyboard;
        readonly Random rnd = new Random();
        internal Piece Piece { get => piece; set => piece = value; }

        public GameScene()
        {
            squares = new List<Square>();
            keyboard = new EasyKeyboard();
        }
        public bool Add(Square arg)
        {
            squares.Add(arg);
            
            return true;
        }
        bool IsRowFull(int n)
        {
            List<float> present = new List<float>();
            foreach (var item in squares)
            {
                if (item.GetPos().Y == n) present.Add(item.GetPos().X);
            }
            for (int i = 0; i < Globals.maxX; i++)
            {
                if(!present.Contains(i)) 
                {
                    return false;
                }
            }
            return true;
        }
        void ClearRow(int n)
        {
            List<Square> marked = new List<Square>();
            foreach (var item in squares)
            {
                if (item.GetPos().Y == n) marked.Add(item);
            }
            foreach (var item in marked)
            {
                squares.Remove(item);
            }
        }
        public void Update(GameTime updateTime)
        {
            keyboard.Update();
            if (keyboard.ReleasedThisFrame(Keys.Space))
            {
                Globals.Pause = !Globals.Pause;
            }
            if (keyboard.ReleasedThisFrame(Keys.Left))
            {
                piece.MoveLeft();
            }
            if (keyboard.ReleasedThisFrame(Keys.Right))
            {
                piece.MoveRight();
            }
            if (keyboard.ReleasedThisFrame(Keys.Up))
            {
                hold.UnHold(Piece.Hold());
                Piece temp = piece;
                piece = hold;
                hold = temp;
            }
            if (updateTime.TotalGameTime.TotalMilliseconds - sinceMove > 100 && !Globals.Pause)
            {
                sinceMove = updateTime.TotalGameTime.TotalMilliseconds;
                if (!piece.MoveDown())
                {
                    foreach (var item in piece.squares)
                    {
                        item.inPiece = false;
                    }
                    piece = new Piece(Globals.nextType, new Vector2(7, 0));
                    Globals.nextType = (Piece.Type)rnd.Next(0, 7);
                    for (int i = 0; i < Globals.maxY; i++)
                    {
                        if(IsRowFull(i))
                        {
                            ClearRow(i);
                            foreach (var item in squares)
                            {
                                item.MoveDown();
                            }
                            foreach (var item in Globals.scene.hold.squares)
                            {
                                item.MoveUp();
                            }
                        }
                    }
                }
            }
        }
        public bool IsTaken(Vector2 pos)
        {
            foreach (var item in squares)
            {
                if (item.GetPos() == pos && !piece.squares.Contains(item)) return true;
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in squares)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
