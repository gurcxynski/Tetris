using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using Tetris.Core;
using MonoGame.EasyInput;
using Microsoft.Xna.Framework.Input;
using Tetris.Menus;
using Tetris.Buttons;

namespace Tetris
{
    public enum GameState { startMenu, pauseMenu, gameRunning, optionsMenu }
    public static class Globals
    {
        public static Dictionary<Piece.Type, Texture2D> blockTextures;
        public static Dictionary<(int, bool), Texture2D> buttonTextures;

        public static GameScene scene;

        public static int maxX = 13, maxY = 27;
        public static Vector2 holdPos = new Vector2(15, 18);
        public static Vector2 queueLastPos = new Vector2(15, 4);
        public static Vector2 startPos = new Vector2(7, 1);

        public static GameState state = GameState.startMenu;

        public static SpriteFont font;
        public static EasyKeyboard keyboard;
        public static EasyMouse mouse;
        public static OptionsMenu optionsMenu;
        public static StartMenu startMenu;
        public static PauseMenu pauseMenu;
        public static Song song;

    }
    public static class Settings
    {
        public static bool music = true;
    }
    public class Game1 : Game
    {
        public static Game1 self;

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
            Globals.buttonTextures = new Dictionary<(int, bool), Texture2D>();

            self = this;
        }
        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 30 * Globals.maxX + 170;
            graphics.PreferredBackBufferHeight = 30 * Globals.maxY;

            graphics.ApplyChanges();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.font = Content.Load<SpriteFont>("fonts/MarkerFelt-16");

            back = Content.Load<Texture2D>("Bck");
            m_back = Content.Load<Texture2D>("back");

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

            Globals.buttonTextures[(0, false)] = Content.Load<Texture2D>("buttonnew1");
            Globals.buttonTextures[(0, true)] = Content.Load<Texture2D>("buttonnew2");
            
            Globals.buttonTextures[(1, false)] = Content.Load<Texture2D>("option1");
            Globals.buttonTextures[(1, true)] = Content.Load<Texture2D>("option2");
            
            Globals.buttonTextures[(2, false)] = Content.Load<Texture2D>("buttonnew1");
            Globals.buttonTextures[(2, true)] = Content.Load<Texture2D>("buttonnew2");
            
            Globals.buttonTextures[(3, false)] = Content.Load<Texture2D>("buttonnew1");
            Globals.buttonTextures[(3, true)] = Content.Load<Texture2D>("buttonnew2");

            Globals.buttonTextures[(4, false)] = Content.Load<Texture2D>("exit1");
            Globals.buttonTextures[(4, true)] = Content.Load<Texture2D>("exit2");



            Globals.startMenu = new StartMenu();
            Globals.pauseMenu = new PauseMenu();
            Globals.optionsMenu = new OptionsMenu();


            Globals.song = Content.Load<Song>("tetris");
            MediaPlayer.Play(Globals.song);
            MediaPlayer.IsRepeating = true;
        }
        protected override void Update(GameTime gameTime)
        {
            Globals.keyboard.Update();
            Globals.mouse.Update();

            
            Globals.optionsMenu.Update();
            Globals.pauseMenu.Update();
            Globals.startMenu.Update();
           
            if(Globals.state == GameState.gameRunning)
            {
                if(Globals.keyboard.ReleasedThisFrame(Keys.Escape))
                {
                    Globals.state = GameState.pauseMenu;
                    return;
                } 
                if(!Globals.scene.Update(gameTime))
                {
                    Globals.scene = new GameScene();
                    Globals.state = GameState.startMenu;
                }
            }
                    
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();



            switch (Globals.state)
            {
                case GameState.startMenu:
                    spriteBatch.Draw(m_back, new Vector2(0, 0), Color.White);
                    Globals.startMenu.Draw(spriteBatch);
                    break;
                case GameState.pauseMenu:
                    spriteBatch.Draw(m_back, new Vector2(0, 0), Color.White);
                    Globals.pauseMenu.Draw(spriteBatch);
                    break;
                case GameState.optionsMenu:
                    spriteBatch.Draw(m_back, new Vector2(0, 0), Color.White);
                    Globals.optionsMenu.Draw(spriteBatch);
                    break;
                case GameState.gameRunning:
                    spriteBatch.Draw(back, new Vector2(0, 0), Color.White);
                    Globals.scene.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
