using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.Core
{
    public class GameScene
    {
        List<Square> squares;
        Piece piece;
        double sinceMove = 0;
        internal Piece Piece { get => piece; set => piece = value; }

        public GameScene()
        {
            squares = new List<Square>();
        }
        public bool Add(Square arg)
        {
            squares.Add(arg);
            
            return true;
        }
        public void Update(GameTime updateTime)
        {
            if(updateTime.TotalGameTime.TotalMilliseconds - sinceMove > 30)
            {
                sinceMove = updateTime.TotalGameTime.TotalMilliseconds;
                piece.Move();
            }
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
