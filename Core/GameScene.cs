using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.EasyInput;
using System.Collections.Generic;

namespace Tetris.Core
{
    public class GameScene
    {
        readonly List<Square> squares;
        public Piece fallingPiece;
        Piece heldPiece;
        readonly Queue<Piece> queue;
        double sinceMove = 0;
        bool initialized = false;
        readonly EasyKeyboard keyboard;
        bool changedPiece = false;

        public GameScene()
        {
            squares = new List<Square>();
            keyboard = new EasyKeyboard();
            queue = new Queue<Piece>();
            keyboard.OnKeyReleased += HandleInput;
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
            if (changedPiece) return;
            if (heldPiece is null)
            {
                heldPiece = Dequeue();
            }
            heldPiece.MoveTo(Globals.startPos);
            fallingPiece.MoveTo(Globals.holdPos);
            (fallingPiece, heldPiece) = (heldPiece, fallingPiece);
            changedPiece = true;
        }
        void HandleInput(Keys button)
        {
            if (button == Keys.Space) Globals.pause = !Globals.pause;
            if (Globals.pause) return;
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
        public void Update(GameTime updateTime)
        {
            keyboard.Update();

            if (!initialized)
            {
                queue.Enqueue(new Piece(Globals.queueLastPos + new Vector2(0, 8)));
                queue.Enqueue(new Piece(Globals.queueLastPos + new Vector2(0, 4)));
                queue.Enqueue(new Piece());

                fallingPiece = new Piece(Globals.startPos);

                initialized = true;
            }

            if (updateTime.TotalGameTime.TotalMilliseconds - sinceMove > 100 && !Globals.pause)
            {
                sinceMove = updateTime.TotalGameTime.TotalMilliseconds;

                if (!fallingPiece.Fall())
                {
                    fallingPiece = Dequeue();
                    changedPiece = false;

                    for (int i = 0; i < Globals.maxY; i++)
                    {
                        if(IsRowFull(i))
                        {
                            ClearRow(i);
                            foreach (var item in squares)
                            {
                                if(item.GetPos().Y < i) item.Move(Keys.Down);
                            }

                            if (heldPiece != null)
                            {
                                Globals.scene.heldPiece.squares.ForEach(delegate (Square item)
                                {
                                    item.Move(Keys.Up);
                                });
                            }
                                
                            foreach (var item in queue)
                            {
                                foreach (var item2 in item.squares)
                                {
                                    item2.Move(Keys.Up);
                                }
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
                if (item.GetPos() == pos && !fallingPiece.squares.Contains(item)) return true;
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
