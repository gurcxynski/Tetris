using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using Tetris.Core;

namespace Tetris
{
    public static class Globals
    {
        public static Dictionary<Piece.Type, Texture2D> textures;
        public static GameScene scene;
        public static int maxX = 13, maxY = 27;
        public static bool pause = false;
        public static Vector2 holdPos = new Vector2(15, 16);
        public static Vector2 queueLastPos = new Vector2(15, 4);
        public static Vector2 startPos = new Vector2(7, 1);
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D back;
        Song song;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Globals.textures = new Dictionary<Piece.Type, Texture2D>();
            Globals.scene = new GameScene();
        }
        protected override void Initialize()
        {
            base.Initialize();

            graphics.PreferredBackBufferWidth = 30 * Globals.maxX + 170;
            graphics.PreferredBackBufferHeight = 30 * Globals.maxY;
            IsMouseVisible = true;

            graphics.ApplyChanges();

        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("fonts/MarkerFelt-16");

            back = Content.Load<Texture2D>("Bck");

            Globals.textures[Piece.Type.Z] = Content.Load<Texture2D>("red");
            Globals.textures[Piece.Type.I] = Content.Load<Texture2D>("blue");
            Globals.textures[Piece.Type.S] = Content.Load<Texture2D>("green");
            Globals.textures[Piece.Type.O] = Content.Load<Texture2D>("yellow");
            Globals.textures[Piece.Type.J] = Content.Load<Texture2D>("pink");
            Globals.textures[Piece.Type.T] = Content.Load<Texture2D>("purple");
            Globals.textures[Piece.Type.L] = Content.Load<Texture2D>("orange");

            song = Content.Load<Song>("tetris");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Globals.scene.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(back, new Vector2(0, 0), Color.White);
            Globals.scene.Draw(spriteBatch);
            spriteBatch.DrawString(font, Globals.scene.fallingPiece.GetPosition().ToString(), new Vector2(0, 0), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
