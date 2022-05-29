using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.EasyInput;
using System.Collections.Generic;
using Tetris.Core;
using Tetris.Menus;

namespace Tetris
{
    public enum GameState { startMenu, gameRunning, optionsMenu }
    public static class Globals
    {
        public static Dictionary<Piece.Type, Texture2D> blockTextures;
        public static Dictionary<(int, bool), Texture2D> buttonTextures;

        public static GameScene scene;

        public static int maxX = 13, maxY = 27;
        public static Vector2 holdPos = new Vector2(15, 18);
        public static Vector2 queueLastPos = new Vector2(15, 4);
        public static Vector2 startPos = new Vector2(7, 1);

        public static GameState state;

        public static SpriteFont font;
        public static EasyKeyboard keyboard;
        public static EasyMouse mouse;

        public static Dictionary<string, Menu> menus = new Dictionary<string, Menu>();
        public static Song song;

    }
    public static class Settings
    {
        public static bool music = true;
        public static int gravity;
    }
    public class Game1 : Game
    {
        public static Game1 self;
        readonly GraphicsDeviceManager graphics;
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
                [Piece.Type.Z] = Content.Load<Texture2D>("blocks/red"),
                [Piece.Type.I] = Content.Load<Texture2D>("blocks/blue"),
                [Piece.Type.S] = Content.Load<Texture2D>("blocks/green"),
                [Piece.Type.O] = Content.Load<Texture2D>("blocks/yellow"),
                [Piece.Type.J] = Content.Load<Texture2D>("blocks/pink"),
                [Piece.Type.T] = Content.Load<Texture2D>("blocks/purple"),
                [Piece.Type.L] = Content.Load<Texture2D>("blocks/orange")
            };

            Globals.buttonTextures[(0, false)] = Content.Load<Texture2D>("buttons/buttonnew1");
            Globals.buttonTextures[(0, true)] = Content.Load<Texture2D>("buttons/buttonnew2");

            Globals.buttonTextures[(1, false)] = Content.Load<Texture2D>("buttons/option1");
            Globals.buttonTextures[(1, true)] = Content.Load<Texture2D>("buttons/option2");

            Globals.buttonTextures[(2, false)] = Content.Load<Texture2D>("buttons/music1");
            Globals.buttonTextures[(2, true)] = Content.Load<Texture2D>("buttons/music2");

            Globals.buttonTextures[(3, false)] = Content.Load<Texture2D>("buttons/return1");
            Globals.buttonTextures[(3, true)] = Content.Load<Texture2D>("buttons/return2");

            Globals.buttonTextures[(4, false)] = Content.Load<Texture2D>("buttons/exit1");
            Globals.buttonTextures[(4, true)] = Content.Load<Texture2D>("buttons/exit2");

            Globals.buttonTextures[(5, false)] = Content.Load<Texture2D>("buttons/minus1");
            Globals.buttonTextures[(5, true)] = Content.Load<Texture2D>("buttons/minus2");

            Globals.buttonTextures[(6, false)] = Content.Load<Texture2D>("buttons/plus1");
            Globals.buttonTextures[(6, true)] = Content.Load<Texture2D>("buttons/plus2");

            Globals.buttonTextures[(7, false)] = Content.Load<Texture2D>("buttons/pause1");
            Globals.buttonTextures[(7, true)] = Content.Load<Texture2D>("buttons/pause2");

            Globals.buttonTextures[(8, false)] = Content.Load<Texture2D>("buttons/round");

            Globals.buttonTextures[(9, false)] = Content.Load<Texture2D>("buttons/line");

            Globals.menus["start"] = new StartMenu();
            Globals.menus["options"] = new OptionsMenu();

            Globals.menus["start"].Enable();
            Globals.state = GameState.startMenu;

            Globals.song = Content.Load<Song>("tetris");
            MediaPlayer.Play(Globals.song);
            MediaPlayer.IsRepeating = true;
        }
        protected override void Update(GameTime gameTime)
        {
            Globals.keyboard.Update();
            Globals.mouse.Update();


            foreach (Menu menu in Globals.menus.Values)
            {
                menu.Update();
            }

            if (Globals.state == GameState.gameRunning && !Globals.scene.Update(gameTime))
            {
                Globals.scene = new GameScene();
                Globals.state = GameState.startMenu;
                Globals.menus["start"].Enable();
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
                    Globals.menus["start"].Draw(spriteBatch);
                    break;
                case GameState.optionsMenu:
                    spriteBatch.Draw(m_back, new Vector2(0, 0), Color.White);
                    Globals.menus["options"].Draw(spriteBatch);
                    break;
                case GameState.gameRunning:
                    spriteBatch.Draw(back, new Vector2(0, 0), Color.White);
                    Globals.scene.Draw(spriteBatch);
                    break;
            }

            //spriteBatch.DrawString(Globals.font, Globals.state.ToString(), new Vector2(0, 0), Color.Black);
            //if(Globals.state == GameState.optionsMenu) spriteBatch.DrawString(Globals.font, (100*Math.Round(MediaPlayer.Volume, 1)).ToString() + "%", new Vector2(250, 360), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
