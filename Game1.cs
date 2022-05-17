using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Tetris.Core;
using System;
using Microsoft.Xna.Framework.Media;

namespace Tetris
{
    public static class Globals
    {
        public static Dictionary<TileColor, Texture2D> textures;
        public static GameScene scene;
        public static int maxX = 13, maxY = 27;
        public static bool Pause = false;
        public static Queue<Piece> Queue = new Queue<Piece>();
        public static int score;
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rnd;
        SpriteFont font;
        Texture2D back;
        Song song;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Globals.textures = new Dictionary<TileColor, Texture2D>();
            Globals.scene = new GameScene();
            rnd = new System.Random();
            Globals.score = 0;
        }
        protected override void Initialize()
        {
            base.Initialize();

            graphics.PreferredBackBufferWidth = 30 * Globals.maxX + 170;
            graphics.PreferredBackBufferHeight = 30 * Globals.maxY;
            IsMouseVisible = true;

            graphics.ApplyChanges();

            Globals.Queue.Enqueue(new Piece((Piece.Type)rnd.Next(0, 7), new Vector2(15, 12)));
            Globals.Queue.Enqueue(new Piece((Piece.Type)rnd.Next(0, 7), new Vector2(15, 8)));
            Globals.Queue.Enqueue(new Piece((Piece.Type)rnd.Next(0, 7), new Vector2(15, 4)));

            Globals.scene.Piece = new Piece((Piece.Type)rnd.Next(0, 7), new Vector2(7, 1));


        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("fonts/MarkerFelt-16");

            back = Content.Load<Texture2D>("Bck");

            Globals.textures[TileColor.red] = Content.Load<Texture2D>("red");
            Globals.textures[TileColor.blue] = Content.Load<Texture2D>("blue");
            Globals.textures[TileColor.green] = Content.Load<Texture2D>("green");
            Globals.textures[TileColor.yellow] = Content.Load<Texture2D>("yellow");
            Globals.textures[TileColor.pink] = Content.Load<Texture2D>("pink");
            Globals.textures[TileColor.purple] = Content.Load<Texture2D>("purple");
            Globals.textures[TileColor.orange] = Content.Load<Texture2D>("orange");

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
            //spriteBatch.DrawString(font, Globals.nextType.ToString(), new Vector2(0, 0), Color.White);
            //spriteBatch.DrawString(font, Globals.scene.hold.ToString(), new Vector2(20, 0), Color.White);
            spriteBatch.DrawString(font, Globals.score.ToString(), new Vector2(0, 30), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
