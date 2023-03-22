using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.EasyInput;
using MonoGame.Extended;
using System.Collections.Generic;
using System.Diagnostics;
using Tetris.Core;
using Tetris.Menus;

namespace Tetris;


public enum PieceType { O, I, S, Z, L, J, T }
public enum Direction { Up, Right, Down, Left }

public static class Globals
{
    public static EasyKeyboard keyboard;
    public static EasyMouse mouse;

    public static SpriteFont font;
    public static Song song;

}
public class Game1 : Game
{
    public static Game1 self;
    public static Dictionary<string, Texture2D> textures;
    readonly GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    public static GameScene scene;
    public static StateMachine gameState;
    public StartMenu start;
    public InGameMenu menu;
    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        Globals.keyboard = new();
        Globals.mouse = new();

        gameState = new();

        start = new();
        start.Enable();
        menu = new();


        self = this;
    }
    protected override void Initialize()
    {
        IsMouseVisible = true;
        graphics.PreferredBackBufferWidth = Config.cellSize * Config.cellsX + (int)Config.margin.X * 2;
        graphics.PreferredBackBufferHeight = Config.cellSize * Config.cellsY + (int)Config.margin.Y * 2;

        graphics.ApplyChanges();

        base.Initialize();
    }
    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        Globals.font = Content.Load<SpriteFont>("fonts/MarkerFelt-16");

        Globals.song = Content.Load<Song>("tetris");

        textures = new()
        {
            ["block"] = Content.Load<Texture2D>("block"),
            ["playbutton"] = Content.Load<Texture2D>("buttons/buttonnew1"),
            ["menubackground"] = Content.Load<Texture2D>("back"),
            ["returnbutton"] = Content.Load<Texture2D>("buttons/pause1"),
            ["musicbutton"] = Content.Load<Texture2D>("buttons/minus1"),
        };

        start.Initialize();
        menu.Initialize();


        MediaPlayer.Play(Globals.song);
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Volume = 0.5f;
    }
    protected override void Update(GameTime gameTime)
    {
        Globals.keyboard.Update();
        Globals.mouse.Update();

        switch (gameState.state)
        {
            case StateMachine.GameState.running:
                scene.Update(gameTime);
                menu.Update();
                break;
            case StateMachine.GameState.startMenu:
                start.Update();
                break;
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.LightGray);

        spriteBatch.Begin();

        switch (gameState.state)
        {
            case StateMachine.GameState.running or StateMachine.GameState.paused or StateMachine.GameState.waiting:
                scene.Draw(spriteBatch);
                spriteBatch.DrawRectangle(new RectangleF(Config.margin.X, Config.margin.Y, Config.cellSize * Config.cellsX, Config.cellSize * Config.cellsY), Color.Red);
                spriteBatch.DrawString(Globals.font, $"LEVEL: {scene.level} SCORE: {scene.score} HIGH SCORE: {gameState.max_score}", new(20, 10), Color.Black); 
                menu.Draw(spriteBatch);
                break;
            case StateMachine.GameState.startMenu:
                spriteBatch.Draw(textures["menubackground"], new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                start.Draw(spriteBatch);
                break;
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }
}
