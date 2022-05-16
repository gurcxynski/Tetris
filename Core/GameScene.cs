using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.EasyInput;
using Microsoft.Xna.Framework.Input;

namespace Tetris.Core
{
    public class GameScene
    {
        List<Square> squares;
        Piece piece;
        double sinceMove = 0;
        EasyKeyboard keyboard;
        Random rnd = new Random();
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
        public void Update(GameTime updateTime)
        {
            keyboard.Update();
            if (keyboard.ReleasedThisFrame(Keys.Left))
            {
                piece.MoveLeft();
            }
            if (keyboard.ReleasedThisFrame(Keys.Right))
            {
                piece.MoveRight();
            }
            if (updateTime.TotalGameTime.TotalMilliseconds - sinceMove > 100)
            {
                sinceMove = updateTime.TotalGameTime.TotalMilliseconds;
                if (!piece.MoveDown())
                {
                    Piece = new Piece((Piece.Type)rnd.Next(0, 7), new Vector2(7, 0));
                }
            }
        }
        public bool isTaken(Vector2 pos)
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
