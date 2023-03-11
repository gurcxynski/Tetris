using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tetris.Core;

public class GameScene
{
    Piece falling;
    readonly List<Square> squares;
    Queue<PieceType> queue = new();

    //        int score = 0;
    double lastTick = 0;
    //        bool changedCurrentPiece = false;


    public GameScene()
    {
        squares = new List<Square>();
        QueueNew(3);
        Globals.keyboard.OnKeyReleased += HandleInput;
    }
    public void Initialize()
    {
        falling = new Piece(queue.Dequeue(), Config.start);
        QueueNew();
    }
    void QueueNew(int n = 1)
    {
        var rnd = new Random();
        for (int i = 0; i < n; i++)
        {
            queue.Enqueue((PieceType)rnd.Next(7));
        }
    }
    void TakeNew()
    {
        falling = new Piece(queue.Dequeue(), Config.start);
        QueueNew();
    }
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
    void HandleInput(Keys button)
    {
        switch (button)
        {
            case Keys.Left:
                falling.Step(Direction.Left);
                break;
            case Keys.Right:
                falling.Step(Direction.Right);
                break;
            case Keys.Up:
                break;
            case Keys.Down:
                while (falling.Fall());
                break;
            case Keys.Space:
                falling.Turn();
                break;
            case Keys.Escape:
                TakeNew();
                break;
        }
    }
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
        if (updateTime.TotalGameTime.TotalMilliseconds - lastTick > Config.tickSpeed)
        {
            lastTick = updateTime.TotalGameTime.TotalMilliseconds;

            if (!falling.Fall())
            {
                TakeNew();
                //if (!TakeNewPiece())
                //{
                //    int max_score = 0;
                //    int.TryParse(File.ReadAllText("score.txt"), out max_score);
                //    if (score > max_score)
                //    {
                //        using StreamWriter writer = new StreamWriter("score.txt");
                //        writer.Flush();
                //        writer.WriteLine(score);
                //    }
                //    return false;
                //}
            }
        }
        return true;
    }
    public bool CanMoveInto(Vector2 target)
    {
        if (target.X < 0 || target.X >= Config.cellsX || target.Y >= Config.cellsY) return false;
        foreach (var item in squares)
        {
            if (item.gridPosition == target && !falling.squares.Contains(item)) return false;
        }
        return true;
    }
    public void Draw(SpriteBatch spriteBatch) => squares.ForEach(delegate (Square item) { item.Draw(spriteBatch); });
}