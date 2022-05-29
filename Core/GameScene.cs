using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace Tetris.Core
{
    public class GameScene
    {

        readonly List<Square> squares;
        readonly Queue<Piece> queue;

        public Piece fallingPiece;
        Piece heldPiece;

        int score = 0;
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
            queue.Enqueue(new Piece(Globals.queueLastPos));

            fallingPiece = new Piece(Globals.startPos);

            Settings.gravity = 1;

            initialized = true;

        }
        public void Add(Square arg)
        {
            squares.Add(arg);
        }
        bool IsRowFull(int n)
        {
            List<float> present = new List<float>();
            foreach (Square item in squares)
            {
                if (item.GetPos().Y == n) present.Add(item.GetPos().X);
            }
            for (int i = 0; i < Globals.maxX; i++)
            {
                if (!present.Contains(i)) return false;
            }
            return true;
        }
        void ClearRow(int n)
        {
            List<Square> marked = new List<Square>();
            foreach (Square item in squares)
            {
                if (item.GetPos().Y == n) marked.Add(item);
            }
            foreach (Square item in marked)
            {
                squares.Remove(item);
            }

            squares.ForEach(delegate (Square item)
            {
                if (item.GetPos().Y < n && item.toMoveWhenCleared) item.Move(Piece.Direction.Down);
            });
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
            if (Globals.state != GameState.gameRunning) return;
            if (button == Keys.P) pause = !pause;
            //if (pause) return;
            switch (button)
            {
                case Keys.Left:
                    fallingPiece.Move(Piece.Direction.Left);
                    break;
                case Keys.Right:
                    fallingPiece.Move(Piece.Direction.Right);
                    break;
                case Keys.Up:
                    Hold();
                    break;
                case Keys.Down:
                    while (fallingPiece.Fall()) { };
                    TakeNewPiece();
                    score += 2 * Settings.gravity;
                    break;
                case Keys.Space:
                    fallingPiece.Turn();
                    break;
                case Keys.Escape:
                    Globals.state = GameState.startMenu;
                    Globals.menus["start"].Enable();
                    break;
            }
        }
        Piece Dequeue()
        {
            Piece dequeued = queue.Dequeue();
            dequeued.MoveTo(Globals.startPos);
            foreach (Piece item in queue)
            {
                item.Move(Piece.Direction.Down, 4);
            }
            queue.Enqueue(new Piece(Globals.queueLastPos));
            return dequeued;
        }
        bool TakeNewPiece()
        {
            foreach (Square item in squares)
            {
                if (item.GetPos().Y < 3) return false;
            }

            fallingPiece.squares.ForEach(delegate (Square item)
            {
                item.toMoveWhenCleared = true;
            });

            fallingPiece = Dequeue();
            changedCurrentPiece = false;
            int cleared = 0;

            for (int i = 0; i < Globals.maxY; i++)
            {
                if (IsRowFull(i))
                {
                    cleared++;
                    ClearRow(i);
                }
            }

            switch (cleared)
            {
                case 1:
                    score += 100 * Settings.gravity;
                    break;
                case 2:
                    score += 300 * Settings.gravity;
                    break;
                case 3:
                    score += 500 * Settings.gravity;
                    break;
                case 4:
                    score += 800 * Settings.gravity;
                    break;
            }

            if (cleared > 0) Settings.gravity++;

            return true;
        }
        public bool Update(GameTime updateTime)
        {
            if (!initialized) Initialize();

            if (updateTime.TotalGameTime.TotalMilliseconds - sinceMove > 200 - Settings.gravity * 20 && !pause && !fallingPiece.isTurning)
            {
                sinceMove = updateTime.TotalGameTime.TotalMilliseconds;

                if (!fallingPiece.Fall())
                {
                    score += Settings.gravity;
                    if (!TakeNewPiece())
                    {
                        int max_score = 0;
                        if (File.ReadAllText("score.txt") != "") max_score = int.Parse(File.ReadAllText("score.txt"));
                        if (score > max_score)
                        {
                            using StreamWriter writer = new StreamWriter("score.txt");
                            writer.Flush();
                            writer.WriteLine(score);
                        }
                        return false;
                    }
                }
            }
            return true;
        }
        public bool IsTaken(Vector2 pos)
        {
            if (pos.X < 0 || pos.X >= Globals.maxX || pos.Y >= Globals.maxY) return true;
            foreach (Square item in squares)
            {
                if (item.GetPos() == pos && !fallingPiece.squares.Contains(item)) return true;
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            squares.ForEach(delegate (Square item) { item.Draw(spriteBatch); });
            spriteBatch.DrawString(Globals.font, score.ToString(), new Vector2(0), Color.White);
        }
    }
}
