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
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Globals.textures = new Dictionary<TileColor, Texture2D>();
            Globals.scene = new GameScene();
        }
        protected override void Initialize()
        {
            base.Initialize();

            graphics.PreferredBackBufferWidth = 420; //14
            graphics.PreferredBackBufferHeight = 690; //23
            IsMouseVisible = true;

            graphics.ApplyChanges();

            Globals.scene.Piece = new Piece(Piece.Type.S, new Vector2(6, 0), TileColor.red);

        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.textures[TileColor.red] = Content.Load<Texture2D>("red");
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Globals.scene.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Globals.scene.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
