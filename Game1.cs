using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Tetris.Core;
using System.Random;
namespace Tetris
{
    public static class Globals
    {
        public static Dictionary<TileColor, Texture2D> textures;
        public static GameScene scene;
        public static int maxX = 15, maxY = 23;
        public static Queue<Piece> queue;
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        System.Random rnd;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Globals.textures = new Dictionary<TileColor, Texture2D>();
            Globals.scene = new GameScene();
            Globals.queue = new Queue<Piece>();
            rnd = new System.Random();
        }
        protected override void Initialize()
        {
            base.Initialize();

            graphics.PreferredBackBufferWidth = 30 * Globals.maxX; //14
            graphics.PreferredBackBufferHeight = 30 * Globals.maxY; //23
            IsMouseVisible = true;

            graphics.ApplyChanges();

            Piece.Type nextType = (Piece.Type)rnd.Next(0, 7);
            Globals.queue.Enqueue(new Piece(nextType, new Vector2(7, 0)));
            for (int i = 0; i < 2; i++){
                while(nextType == Globals.queue.Peek().GetType()) nextType = (Piece.Type)rnd.Next(0, 7);
            }
            

        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.textures[TileColor.red] = Content.Load<Texture2D>("red");
            Globals.textures[TileColor.blue] = Content.Load<Texture2D>("blue");
            Globals.textures[TileColor.green] = Content.Load<Texture2D>("green");
            Globals.textures[TileColor.yellow] = Content.Load<Texture2D>("yellow");
            Globals.textures[TileColor.pink] = Content.Load<Texture2D>("pink");
            Globals.textures[TileColor.purple] = Content.Load<Texture2D>("purple");
            Globals.textures[TileColor.orange] = Content.Load<Texture2D>("orange");

        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Globals.scene.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Globals.scene.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
