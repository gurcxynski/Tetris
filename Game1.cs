using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using Tetris.Core;
using MonoGame.EasyInput;
using Microsoft.Xna.Framework.Input;
using System;
using Tetris.Buttons;

namespace Tetris
{
    public static class Globals
    {
        public static Dictionary<Piece.Type, Texture2D> blockTextures;
        public static KeyValuePair<Texture2D, Texture2D> buttonTextures;

        public static GameScene scene;

        public static int maxX = 13, maxY = 27;
        public static Vector2 holdPos = new Vector2(15, 18);
        public static Vector2 queueLastPos = new Vector2(15, 4);
        public static Vector2 startPos = new Vector2(7, 1);

        public static bool gameRunning = false;
        public static bool music = true;

        public static SpriteFont font;
        public static EasyKeyboard keyboard;
        public static EasyMouse mouse;
        public static Menu menu;
        public static Song song;
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D back;
        Texture2D m_back;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Globals.keyboard = new EasyKeyboard();
            Globals.mouse = new EasyMouse();
            Globals.scene = new GameScene();
            Globals.menu = new Menu();


        }
        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 30 * Globals.maxX + 170;
            graphics.PreferredBackBufferHeight = 30 * Globals.maxY;

            graphics.ApplyChanges();

            Globals.menu.Add(new PlayButton(new Vector2(graphics.PreferredBackBufferWidth/2, 200)));
            Globals.menu.Add(new MusicButton(new Vector2(graphics.PreferredBackBufferWidth / 2, 250)));

        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.font = Content.Load<SpriteFont>("fonts/MarkerFelt-16");

            back = Content.Load<Texture2D>("Bck");
            m_back = Content.Load<Texture2D>("menu");

            Globals.blockTextures = new Dictionary<Piece.Type, Texture2D>
            {
                [Piece.Type.Z] = Content.Load<Texture2D>("red"),
                [Piece.Type.I] = Content.Load<Texture2D>("blue"),
                [Piece.Type.S] = Content.Load<Texture2D>("green"),
                [Piece.Type.O] = Content.Load<Texture2D>("yellow"),
                [Piece.Type.J] = Content.Load<Texture2D>("pink"),
                [Piece.Type.T] = Content.Load<Texture2D>("purple"),
                [Piece.Type.L] = Content.Load<Texture2D>("orange")
            };

            Globals.buttonTextures = new KeyValuePair<Texture2D, Texture2D>(Content.Load<Texture2D>("buttons"), Content.Load<Texture2D>("buttons2"));

            Globals.song = Content.Load<Song>("tetris");
            MediaPlayer.Play(Globals.song);
            MediaPlayer.IsRepeating = true;
        }
        protected override void Update(GameTime gameTime)
        {
            Globals.keyboard.Update();
            Globals.mouse.Update();

            if (Globals.keyboard.ReleasedThisFrame(Keys.Escape) && Globals.gameRunning) 
            {
                Globals.gameRunning = false;
                return;
            }

            if (Globals.gameRunning && !Globals.scene.Update(gameTime))
            {
                Globals.scene = new GameScene();
                Globals.gameRunning = !Globals.gameRunning;
            }

            if (!Globals.gameRunning) Globals.menu.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            if (Globals.gameRunning)
            {
                spriteBatch.Draw(back, new Vector2(0, 0), Color.White);
                Globals.scene.Draw(spriteBatch);
            }
            else
            {
                spriteBatch.Draw(m_back, new Vector2(0, 0), Color.White);
                Globals.menu.Draw(spriteBatch);
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
