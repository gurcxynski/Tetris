using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.EasyInput;
using System.Collections.Generic;
using Tetris.Core;

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
    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        Globals.keyboard = new();
        Globals.mouse = new();

        scene = new();
        new Piece(PieceType.O, new Vector2(3, 3));
        new Piece(PieceType.S, new Vector2(7, 7));
        self = this;
    }
    protected override void Initialize()
    {
        base.Initialize();

        IsMouseVisible = true;
        graphics.PreferredBackBufferWidth = Config.cellSize * Config.cellsX;
        graphics.PreferredBackBufferHeight = Config.cellSize * Config.cellsY;

        graphics.ApplyChanges();
    }
    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        Globals.font = Content.Load<SpriteFont>("fonts/MarkerFelt-16");

        Globals.song = Content.Load<Song>("tetris");

        textures = new()
        {
            ["block"] = Content.Load<Texture2D>("block")
        };

        MediaPlayer.Play(Globals.song);
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Volume = 0.5f;
    }
    protected override void Update(GameTime gameTime)
    {
        Globals.keyboard.Update();
        Globals.mouse.Update();

        scene.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin();

        scene.Draw(spriteBatch);

        spriteBatch.End();

        base.Draw(gameTime);
    }
}
