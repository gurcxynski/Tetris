using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Tetris.Core
{
    public class GameScene
    {

        readonly List<Square> squares;
        readonly Queue<Piece> queue;

        public Piece fallingPiece;
        Piece heldPiece;

        double sinceMove = 0;
        bool initialized = false;
        bool changedCurrentPiece = false;
        bool pause = false;

        public GameScene()
        {
            squares = new List<Square>();
            queue = new Queue<Piece>();
            Globals.keyboard.OnKeyReleased += HandleInput;
        }
        public void Initialize()
        {
            queue.Enqueue(new Piece(Globals.queueLastPos + new Vector2(0, 8)));
            queue.Enqueue(new Piece(Globals.queueLastPos + new Vector2(0, 4)));
            queue.Enqueue(new Piece());

            fallingPiece = new Piece(Globals.startPos);

            initialized = true;

        }
        public void Add(Square arg)
        {
            squares.Add(arg);
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
                if(!present.Contains(i)) return false;
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
        
        void Hold()
        {
            if (changedCurrentPiece) return;
            if (heldPiece is null)
            {
                heldPiece = Dequeue();
            }
            heldPiece.MoveTo(Globals.startPos);
            fallingPiece.MoveTo(Globals.holdPos);
            (fallingPiece, heldPiece) = (heldPiece, fallingPiece);
            changedCurrentPiece = true;
        }
        void HandleInput(Keys button)
        {
            if (button == Keys.Space) pause = !pause;
            if (pause) return;
            if (button == Keys.Left || button == Keys.Right) fallingPiece.Move(button);
            if (button == Keys.Up) Hold();
        }
        Piece Dequeue()
        {
            Piece dequeued = queue.Dequeue();
            dequeued.MoveTo(Globals.startPos);
            foreach (var item in queue)
            {
                item.Move(Keys.Down, 4);
            }
            queue.Enqueue(new Piece());
            return dequeued;
        }
        public bool Update(GameTime updateTime)
        {
            if (!initialized) Initialize();

            if (updateTime.TotalGameTime.TotalMilliseconds - sinceMove > 100 && !pause)
            {
                sinceMove = updateTime.TotalGameTime.TotalMilliseconds;

                if (!fallingPiece.Fall())
                {
                    foreach (var item in squares)
                    {
                        if (item.GetPos().Y < 4) return false;
                    }

                    fallingPiece = Dequeue();
                    changedCurrentPiece = false;

                    for (int i = 0; i < Globals.maxY; i++)
                    {
                        if(IsRowFull(i))
                        {
                            ClearRow(i);

                            squares.ForEach(delegate (Square item)
                            {
                                if (item.GetPos().Y < i) item.Move(Keys.Down);
                            });

                            if (heldPiece != null)
                            {
                                Globals.scene.heldPiece.squares.ForEach(delegate (Square item)
                                {
                                    item.Move(Keys.Up);
                                });
                            }
                                
                            foreach (var item in queue)
                            {
                                item.squares.ForEach(delegate (Square item2)
                                {
                                    item2.Move(Keys.Up);
                                });
                            }
                        }
                    }
                }
            }
            return true;
        }
        public bool IsTaken(Vector2 pos)
        {
            foreach (var item in squares)
            {
                if (item.GetPos() == pos && !fallingPiece.squares.Contains(item)) return true;
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            squares.ForEach(delegate (Square item) { item.Draw(spriteBatch); });
        }
    }
}
