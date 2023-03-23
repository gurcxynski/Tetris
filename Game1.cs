using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.EasyInput;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
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
    public static SpriteFont fontbig;
    public static SpriteFont fonttitle;

    public static Song song;
    public static SoundEffect clear;

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
    public PauseMenu pause;

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
        pause = new();

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

        Globals.font = Content.Load<SpriteFont>("basic");
        Globals.fontbig = Content.Load<SpriteFont>("button");
        Globals.fonttitle = Content.Load<SpriteFont>("title");

        Globals.song = Content.Load<Song>("tetris");
        Globals.clear = Content.Load<SoundEffect>("clear (1)");

        textures = new()
        {
            ["playbutton"] = Content.Load<Texture2D>("play"),
            ["menubackground"] = Content.Load<Texture2D>("back"),
            ["returnbutton"] = Content.Load<Texture2D>("pause"),
            ["musicbutton"] = Content.Load<Texture2D>("musicon"),
            ["musicbutton1"] = Content.Load<Texture2D>("musicoff"),
            ["hold"] = Content.Load<Texture2D>("hold"),
            ["next"] = Content.Load<Texture2D>("next"),
        };

        start.Initialize();
        menu.Initialize();
        pause.Initialize();

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
            case StateMachine.GameState.pauseMenu:
                pause.Update();
                break;
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin();

        switch (gameState.state)
        {
            case StateMachine.GameState.running or StateMachine.GameState.paused or StateMachine.GameState.waiting:
                scene.Draw(spriteBatch);
                
                menu.Draw(spriteBatch);
                break;
            case StateMachine.GameState.startMenu:
                spriteBatch.Draw(textures["menubackground"], new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.DrawString(Globals.fonttitle, "TETRIS", new Vector2(130, 20), Color.White);
                start.Draw(spriteBatch);
                break;
            case StateMachine.GameState.pauseMenu:
                spriteBatch.Draw(textures["menubackground"], new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.DrawString(Globals.fonttitle, "TETRIS", new Vector2(130, 20), Color.White);
                pause.Draw(spriteBatch);
                break;
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }
}
