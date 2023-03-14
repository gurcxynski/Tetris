using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tetris.Core;

public class GameScene
{
    Piece falling;
    Piece shade;
    readonly List<Square> squares;
    Queue<PieceType> queue = new();
    PieceType? held = null;
    //        int score = 0;
    double lastTick = 0;
    bool changedPiece = false;


    public GameScene()
    {
        squares = new List<Square>();
        QueueNew(3);
        Globals.keyboard.OnKeyReleased += HandleInput;
    }
    public void Initialize()
    {
        falling = new(queue.Dequeue(), Config.start);
        NewShade();
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
    void NewShade()
    {
        shade = new(falling.Type, falling.position, false, 25);
        while (shade.Fall()) ;
    }
    void UpdateShade()
    {
        shade.Step(Direction.Up, 20);
        while (shade.Fall()) ;
    }
    void UpdateShade(Direction dir)
    {
        shade.Step(Direction.Up, 20);
        shade.Step(dir);
        while (shade.Fall()) ;
    }
    void TakeNew()
    {
        falling = new(queue.Dequeue(), Config.start);
        NewShade();
        QueueNew();
        changedPiece = false;
    }
    public void Add(Square arg)
    { 
        squares.Add(arg);
    }
    bool IsRowFull(int n)
    {
        var row = from square in squares
                  where square.gridPosition.Y == n
                  select square;
        if (row.Count() == Config.cellsX) return true;
        return false;
    }
    void ClearRow(int n)
    {
        squares.RemoveAll(square => square.gridPosition.Y == n);
        squares.ForEach(delegate (Square item)
        {
            if (item.gridPosition.Y < n) item.MoveToPos(item.gridPosition += new Vector2(0, 1));
        });
    }

    void Hold()
    {
        squares.RemoveAll(square => falling.squares.Contains(square));
        if (held is null)
        {
            held = falling.Type;
            TakeNew();
        }
        else
        {
            var temp = falling.Type;
            falling = new Piece((PieceType)held, Config.start);
            NewShade();
            held = temp;
        }
        changedPiece = true;
    }
    void HandleInput(Keys button)
    {
        if (Game1.gameState.state != StateMachine.GameState.running) return;
        switch (button)
        {
            case Keys.Left:
                falling.Step(Direction.Left);
                UpdateShade(Direction.Left);
                break;
            case Keys.Right:
                falling.Step(Direction.Right);
                UpdateShade(Direction.Right);
                break;
            case Keys.Up:
                if (!changedPiece) Hold();
                break;
            case Keys.Down:
                while (falling.Fall());
                break;
            case Keys.Space:
                falling.Turn();
                shade.Step(Direction.Up, 20);
                shade.Turn();
                UpdateShade();
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
        for (int i = 0; i < Config.cellsY; i++) if (IsRowFull(i)) ClearRow(i);
        UpdateShade();
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
    public void Draw(SpriteBatch spriteBatch)
    {
        shade.squares.ForEach(delegate (Square square) { square.Draw(spriteBatch, true); });

        squares.ForEach(delegate (Square item) { item.Draw(spriteBatch); });

        DrawSmallPiece(queue.ToArray()[2], Config.queuePosition, spriteBatch);
        DrawSmallPiece(queue.ToArray()[1], Config.queuePosition + new Vector2(0, 5), spriteBatch);
        DrawSmallPiece(queue.ToArray()[0], Config.queuePosition + new Vector2(0, 10), spriteBatch);
        if (held is not null) DrawSmallPiece((PieceType)held, Config.heldPosition, spriteBatch);

    }
    void DrawSmallPiece(PieceType pieceType, Vector2 position, SpriteBatch spriteBatch)
    {
        var piece = new Piece(pieceType, position, false, 25);
        piece.squares.ForEach(delegate (Square square) { square.Draw(spriteBatch); });
    }
}