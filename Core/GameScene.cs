using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using static Tetris.Core.StateMachine;

namespace Tetris.Core;

public class Generator
{
    Queue<PieceType> history = new();
    Random rnd = new();
    public PieceType GetNew()
    {
        var type = (PieceType)rnd.Next(7);
        var tries = 1;
        while (tries < 4 && history.Contains(type))
        {
            type = (PieceType)rnd.Next(7);
            tries++;
        }
        if (history.Count > 4) history.Dequeue();
        history.Enqueue(type);
        return type;
    }
}

public class GameScene
{
    Piece falling;
    Piece shade;
    readonly List<Square> squares;
    Queue<PieceType> queue = new();
    PieceType? held = null;
    double lastTick = 0;
    bool changedPiece = false;
    Generator gen = new();
    int exp = 0;
    double tickspeed = Config.tickSpeed;
    public int level = 1;
    public int score = 0;
    double isHeldFor = 0;
    double lastSlideTick = 0;
    public GameScene()
    {
        squares = new List<Square>();
        QueueNew(3);
        Globals.keyboard.OnKeyReleased = HandleInput;
    }
    public void Initialize()
    {
        falling = new(queue.Dequeue(), Config.start);
        NewShade();
        QueueNew();
    }
    void QueueNew(int n = 1)
    {
        for (int i = 0; i < n; i++)
            {
                queue.Enqueue(gen.GetNew());
            }
    }
    void NewShade()
    {
        shade = new(falling.Type, falling.position, false, 25);
        while (shade.Fall()) ;
    }
    void UpdateShade()
    {
        shade.Step(Direction.Up, Math.Abs(falling.position.Y - shade.position.Y));
        while (shade.Fall()) ;
    }
    void UpdateShade(Direction dir)
    {
        shade.Step(Direction.Up, 20);
        shade.Step(dir);
        while (shade.Fall()) ;
    }
    bool TakeNew()
    {
        falling = null;
        var type = queue.Dequeue();
        QueueNew();
        var piece = new Piece(type, Config.start, false);
        foreach (var item in piece.squares)
        {
            if (!CanMoveInto(item.gridPosition))
            {
                return false;
            }
        }
        falling = new(type, Config.start);
        NewShade();
        changedPiece = false;
        return true;
    }
    public void Add(Square arg) => squares.Add(arg);
    bool IsRowFull(int n)
    {
        var row = from square in squares
                  where square.gridPosition.Y == n
                  select square;
        return row.Count() == Config.cellsX;
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

    public void HandleInput(Keys button)
    {
        isHeldFor = 0;
        if (Game1.gameState.state != GameState.running && Game1.gameState.state != GameState.waiting && button != Keys.Escape && button != Keys.F1) return;
        switch (button)
        {
            case Keys.Left or Keys.NumPad4:
                falling.Step(Direction.Left);
                UpdateShade(Direction.Left);
                break;
            case Keys.Right or Keys.NumPad6:
                falling.Step(Direction.Right);
                UpdateShade(Direction.Right);
                break;
            case Keys.C or Keys.RightShift or Keys.LeftShift or Keys.NumPad0:
                if (!changedPiece) Hold();
                break;
            case Keys.Up or Keys.X or Keys.NumPad3 or Keys.NumPad7:
                falling.Turn(Direction.Left);
                shade.Step(Direction.Up, 20);
                shade.Turn(Direction.Left);
                UpdateShade();
                break;
            case Keys.LeftControl or Keys.RightControl or Keys.Z or Keys.NumPad1 or Keys.NumPad5 or Keys.NumPad9:
                falling.Turn(Direction.Right);
                shade.Step(Direction.Up, 20);
                shade.Turn(Direction.Right);
                UpdateShade();
                break;
            case Keys.Space or Keys.NumPad8 or Keys.NumPad2:
                var lines = 0;
                for (; falling.Fall(); lines++) ;
                score += lines;
                break;
            case Keys.Escape or Keys.F1:
                if (Game1.gameState.state == GameState.paused) Game1.gameState.UnPause();
                else if (Game1.gameState.state == GameState.running) Game1.gameState.Pause();
                else if (Game1.gameState.state == GameState.waiting) Game1.gameState.NewGame();
                break;
        }
    }
    public bool Update(GameTime updateTime)
    {
        if (Globals.keyboard.AnyKeyPressed) isHeldFor += updateTime.ElapsedGameTime.TotalMilliseconds;
        if (isHeldFor > 250 && updateTime.TotalGameTime.TotalMilliseconds - lastSlideTick > 100)
        {
            if (Globals.keyboard.IsPressed(Keys.Left))
            {
                falling.Step(Direction.Left);
                UpdateShade(Direction.Left);
                lastSlideTick = updateTime.TotalGameTime.TotalMilliseconds;
            }
            if (Globals.keyboard.IsPressed(Keys.Right))
            {
                falling.Step(Direction.Right);
                UpdateShade(Direction.Right);
                lastSlideTick = updateTime.TotalGameTime.TotalMilliseconds;
            }
        }
        if (updateTime.TotalGameTime.TotalMilliseconds - lastTick > tickspeed)
        {
            lastTick = updateTime.TotalGameTime.TotalMilliseconds;

            if (!falling.Fall())
            {
                if (!TakeNew())
                {
                    Globals.keyboard.OnKeyReleased -= HandleInput;
                    Game1.gameState.GameEnd();
                    return false;
                }
            }

        }
        var cleared = 0;
        for (int i = 0; i < Config.cellsY; i++)
        {
            if (IsRowFull(i)) {
                ClearRow(i);
                cleared++;
            }
        }
        if (cleared > 0) Globals.clear.Play(0.3f, 0.1f, 0.5f);
        exp += cleared switch
        {
            1 => 1,
            2 => 3,
            3 => 5,
            4 => 8,
            _ => 0
        };
        score += level * cleared switch
        {
            1 => 40,
            2 => 100,
            3 => 300,
            4 => 1200,
            _ => 0
        };
        if (exp >= 5 * level)
        {
            exp = 0;
            level++;
        }
        tickspeed = 300 * Math.Pow(0.9, 0.6f * level);
        UpdateShade();
        return true;
    }
    public bool CanMoveInto(Vector2 target)
    {
        if (target.X < 0 || target.X >= Config.cellsX || target.Y >= Config.cellsY) return false;
        foreach (var item in squares)
        {
            if (item.gridPosition == target && (falling is null || !falling.squares.Contains(item))) return false;
        }
        return true;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        shade.squares.ForEach(delegate (Square square) { square.Draw(spriteBatch, true); });

        var thicc = 5;
        spriteBatch.DrawRectangle(new RectangleF(Config.margin.X - thicc, Config.margin.Y - thicc, Config.cellSize * Config.cellsX + 2 * thicc, Config.cellSize * Config.cellsY + 2 * thicc), Color.DarkBlue, thicc);
        spriteBatch.DrawString(Globals.font, $"LVL: {level} SCORE: {score} HI-SCORE: {(Game1.gameState.scores.Count == 0 ? 0 : Game1.gameState.scores[0])}", new(20, 10), Color.White);
        spriteBatch.Draw(Game1.textures["hold"], new Vector2(-3, 60), Color.White);
        spriteBatch.Draw(Game1.textures["next"], new Vector2(480, 85), Color.White);

        squares.ForEach(delegate (Square item) { item.Draw(spriteBatch); });

        DrawSmallPiece(queue.ToArray()[2], Config.queuePosition, spriteBatch);
        DrawSmallPiece(queue.ToArray()[1], Config.queuePosition + new Vector2(0, 5), spriteBatch);
        DrawSmallPiece(queue.ToArray()[0], Config.queuePosition + new Vector2(0, 10), spriteBatch);

        if (held is not null) DrawSmallPiece((PieceType)held, Config.heldPosition, spriteBatch);
        if (Game1.gameState.state == GameState.waiting) spriteBatch.DrawString(Globals.fontbig, "GAME OVER", new(200, 300), Color.White);
    }
    void DrawSmallPiece(PieceType pieceType, Vector2 position, SpriteBatch spriteBatch)
    {
        var piece = new Piece(pieceType, position, false);
        piece.squares.ForEach(delegate (Square square) { square.Draw(spriteBatch); });
    }
}