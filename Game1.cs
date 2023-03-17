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
    public OptionsMenu options;
    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        Globals.keyboard = new();
        Globals.mouse = new();

        scene = new();
        gameState = new();

        start = new();
        start.Enable();
        options = new();


        self = this;
    }
    protected override void Initialize()
    {
        IsMouseVisible = true;
        graphics.PreferredBackBufferWidth = Config.cellSize * Config.cellsX + (int)Config.margin.X * 2;
        graphics.PreferredBackBufferHeight = Config.cellSize * Config.cellsY + (int)Config.margin.Y * 2;

        graphics.ApplyChanges();


        scene.Initialize();

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
            ["optionsbutton"] = Content.Load<Texture2D>("buttons/option1"),
            ["returnbutton"] = Content.Load<Texture2D>("buttons/return1"),
            ["musicbutton"] = Content.Load<Texture2D>("buttons/return1"),
        };

        start.Initialize();
        options.Initialize();


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
                break;
            case StateMachine.GameState.startMenu:
                start.Update();
                break;
            case StateMachine.GameState.optionsMenu:
                options.Update();
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
            case StateMachine.GameState.running or StateMachine.GameState.paused:
                scene.Draw(spriteBatch);
                spriteBatch.DrawRectangle(new RectangleF(Config.margin.X, Config.margin.Y, Config.cellSize * Config.cellsX, Config.cellSize * Config.cellsY), Color.Red);
                spriteBatch.DrawString(Globals.font, $"LEVEL: {scene.level} SCORE: {scene.score}", new(170, 10), Color.Black);
                break;
            case StateMachine.GameState.startMenu:
                spriteBatch.Draw(textures["menubackground"], new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                start.Draw(spriteBatch);
                break;
            case StateMachine.GameState.optionsMenu:
                spriteBatch.Draw(textures["menubackground"], new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                options.Draw(spriteBatch);
                break;
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }
}
