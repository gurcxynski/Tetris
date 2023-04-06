using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.EasyInput;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public static Dictionary<string, Menu> menus;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        Globals.keyboard = new();
        Globals.mouse = new();

        gameState = new();
        string[] read;
        try
        {
            read = File.ReadAllText("scores.txt").Trim().Split(";", StringSplitOptions.RemoveEmptyEntries);
        }
        catch
        {
            File.Create("scores.txt").Close();
            read = Array.Empty<string>();
        }
        foreach (var item in read)
        {
            gameState.scores.Add(int.Parse(item));
        }
        menus = new()
        {
            ["start"] = new StartMenu(),
            ["ingame"] = new InGameMenu(),
            ["pause"] = new PauseMenu(),
            ["controls"] = new ControlsMenu(),
            ["scores"] = new ScoresMenu(),
        };

        menus["start"].Enable();

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
            ["controls"] = Content.Load<Texture2D>("controls"),
        };

        foreach (var item in menus)
        {
            item.Value.Initialize();
        }

        MediaPlayer.Play(Globals.song);
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Volume = 0.2f;
    }
    protected override void Update(GameTime gameTime)
    {
        Globals.keyboard.Update();
        Globals.mouse.Update();

        switch (gameState.state)
        {
            case StateMachine.GameState.running:
                scene.Update(gameTime);
                menus["ingame"].Update();
                break;
            case StateMachine.GameState.startMenu:
                menus["start"].Update();
                break;
            case StateMachine.GameState.pauseMenu:
                menus["pause"].Update();
                break;
            case StateMachine.GameState.controls:
                menus["controls"].Update();
                break;
            case StateMachine.GameState.scores:
                menus["scores"].Update();
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
                menus["ingame"].Draw(spriteBatch);
                break;
            case StateMachine.GameState.startMenu:
                spriteBatch.Draw(textures["menubackground"], new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.DrawString(Globals.fonttitle, "TETRIS", new Vector2(160, 20), Color.White);
                menus["start"].Draw(spriteBatch);
                break;
            case StateMachine.GameState.pauseMenu:
                spriteBatch.Draw(textures["menubackground"], new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.DrawString(Globals.fonttitle, "TETRIS", new Vector2(160, 20), Color.White);
                menus["pause"].Draw(spriteBatch);
                break;
            case StateMachine.GameState.controls:
                spriteBatch.Draw(textures["controls"], new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                menus["controls"].Draw(spriteBatch);
                break;
            case StateMachine.GameState.scores:
                menus["scores"].Draw(spriteBatch);
                for(var i = 1; i <= gameState.scores.Count; i++)
                {
                    spriteBatch.DrawString(Globals.fontbig, $"{i}.", new(200, 100 + i * 40), Color.White);
                    spriteBatch.DrawString(Globals.fontbig, gameState.scores[i - 1].ToString(), new(430 - Globals.fontbig.MeasureString(gameState.scores[i - 1].ToString()).X, 100 + i * 40), Color.White);
                    spriteBatch.DrawString(Globals.fonttitle, "HIGH SCORES", new(40, 10), Color.White);
                }
                break;
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }
}
