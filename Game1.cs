using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Tetris.Core;
namespace Tetris
{
    public static class Globals
    {
        public static Dictionary<TileColor, Texture2D> textures;
        public static GameScene scene;
        public static int maxX = 15, maxY = 23;
        public static Piece.Type nextType;
        public static bool Pause = false;
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        System.Random rnd;
        SpriteFont font;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Globals.textures = new Dictionary<TileColor, Texture2D>();
            Globals.scene = new GameScene();
            rnd = new System.Random();
        }
        protected override void Initialize()
        {
            base.Initialize();

            graphics.PreferredBackBufferWidth = 30 * Globals.maxX; //14
            graphics.PreferredBackBufferHeight = 30 * Globals.maxY; //23
            IsMouseVisible = true;

            graphics.ApplyChanges();

            Globals.nextType = (Piece.Type)rnd.Next(0, 7);
            Globals.scene.Piece = new Piece((Piece.Type)rnd.Next(0, 7), new Vector2(7, 0));
            
            

        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("fonts/MarkerFelt-16");

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
            spriteBatch.Begin();
            Globals.scene.Draw(spriteBatch);
            spriteBatch.DrawString(font, Globals.nextType.ToString(), new Vector2(0, 0), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
