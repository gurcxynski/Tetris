using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Collections.Generic;

namespace Tetris.Core;

public class GameScene
{
    Piece falling;
    readonly List<Square> squares;


    //        int score = 0;
    //        double sinceMove = 0;
    //        bool initialized = false;
    //        bool changedCurrentPiece = false;
    //        bool pause = false;


    public GameScene()
    {
        squares = new List<Square>();
        //Globals.keyboard.OnKeyReleased += HandleInput;
    }
    //        public void Initialize()
    //        {
    //            queue.Enqueue(new Piece(Globals.queueLastPos + new Vector2(0, 8)));
    //            queue.Enqueue(new Piece(Globals.queueLastPos + new Vector2(0, 4)));
    //            queue.Enqueue(new Piece(Globals.queueLastPos));

    //            falling = new Piece(Globals.startPos);

    //            Settings.gravity = 1;


    //            File.Create("score.txt").Dispose();

    //            initialized = true;

    //        }
    public void Add(Square arg)
    { 
        squares.Add(arg);
    }
        
    //        bool IsRowFull(int n)
    //        {
    //            var row =  from point in squares
    //                       where point.Position.Y == n && Globals.maxX > point.Position.X && point.Position.X >= 0 select point;
    //            if (row.Count() == Globals.maxX) return true;
    //            return false;
    //        }
    //        void ClearRow(int n)
    //        {
    //            var row = from point in squares
    //                      where point.Position.Y == n && Globals.maxX > point.Position.X && point.Position.X >= 0 select point;
    //            row.All(point => squares.Remove(point));          
    //            squares.ForEach(delegate (Square item)
    //            {
    //                if (item.Position.Y < n && item.toMoveWhenCleared) item.Move(Direction.Down, 1, true);
    //            });
    //        }

    //        void Hold()
    //        {
    //            if (changedCurrentPiece) return;

    //            if (heldPiece is null)heldPiece = Dequeue();

    //            heldPiece.Move(Globals.startPos, true);
    //            falling.Move(Globals.holdPos, true);

    //            (falling, heldPiece) = (heldPiece, falling);

    //            changedCurrentPiece = true;
    //        }
    //        void HandleInput(Keys button)
    //        {
    //            if (Globals.state != GameState.gameRunning) return;
    //            if (button == Keys.P) pause = !pause;
    //            if (pause) return;
    //            switch (button)
    //            {
    //                case Keys.Left:
    //                    falling.Step(Direction.Left);
    //                    break;
    //                case Keys.Right:
    //                    falling.Step(Direction.Right);
    //                    break;
    //                case Keys.Up:
    //                    Hold();
    //                    break;
    //                case Keys.Down:
    //                    while (falling.Fall()) { };
    //                    TakeNewPiece();
    //                    sinceMove = 0;
    //                    score += 2 * Settings.gravity;
    //                    break;
    //                case Keys.Space:
    //                    falling.Turn();
    //                    break;
    //                case Keys.Escape:
    //                    Globals.state = GameState.startMenu;
    //                    Globals.menus["start"].Enable();
    //                    break;
    //            }
    //        }
    //        Piece Dequeue()
    //        {
    //            Piece dequeued = queue.Dequeue();
    //            dequeued.Move(Globals.startPos, true);
    //            foreach (Piece item in queue) item.Step(Direction.Down, 4, true);
    //            queue.Enqueue(new Piece(Globals.queueLastPos));
    //            return dequeued;
    //        }
    //        bool TakeNewPiece()
    //        {
    //            foreach (Square item in squares)
    //            {
    //                if (item.Position.Y < 3) 
    //                {
    //                    Globals.keyboard.OnKeyReleased -= HandleInput;
    //                    return false;
    //                }
    //            }

    //            falling.squares.ForEach(delegate (Square item)
    //            {
    //                item.toMoveWhenCleared = true;
    //            });

    //            falling = Dequeue();
    //            changedCurrentPiece = false;
    //            int cleared = 0;

    //            for (int i = 0; i < Globals.maxY; i++)
    //            {
    //                if (IsRowFull(i))
    //                {
    //                    cleared++;
    //                    ClearRow(i);
    //                }
    //            }

    //            switch (cleared)
    //            {
    //                case 1:
    //                    score += 100 * Settings.gravity;
    //                    break;
    //                case 2:
    //                    score += 300 * Settings.gravity;
    //                    break;
    //                case 3:
    //                    score += 500 * Settings.gravity;
    //                    break;
    //                case 4:
    //                    score += 800 * Settings.gravity;
    //                    break;
    //            }

    //            if (cleared > 0) Settings.gravity++;

    //            return true;
    //        }
            public bool Update(GameTime updateTime)
            {
    //            if (!initialized) Initialize();

    //            if (updateTime.TotalGameTime.TotalMilliseconds - sinceMove > 200 - Settings.gravity * 20 && !pause)
    //            {
    //                sinceMove = updateTime.TotalGameTime.TotalMilliseconds;

    //                if (!falling.Fall())
    //                {
    //                    score += Settings.gravity;
    //                    if (!TakeNewPiece())
    //                    {
    //                        int max_score = 0;
    //                        int.TryParse(File.ReadAllText("score.txt"), out max_score);
    //                        if (score > max_score)
    //                        {
    //                            using StreamWriter writer = new StreamWriter("score.txt");
    //                            writer.Flush();
    //                            writer.WriteLine(score);
    //                        }
    //                        return false;
    //                    }
    //                }
    //            }
                return true;
            }
    public bool CanMoveInto(Vector2 pos)
    {
        if (pos.X < 0 || pos.X >= Config.cellsX|| pos.Y >= Config.cellsY) return false;
        foreach (Square item in squares)
        {
            if (item.Bounds.Position == (Point2)pos && !falling.squares.Contains(item)) return false;
        }
        return true;
    }
    public void Draw(SpriteBatch spriteBatch) => squares.ForEach(delegate (Square item) { item.Draw(spriteBatch); });
}