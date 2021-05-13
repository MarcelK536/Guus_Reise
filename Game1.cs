using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

//Adding SubClasses within Folders
using Guus_Reise.Menu;
using Guus_Reise.HexangonMap;

namespace Guus_Reise
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public enum GameState
        {
            MainMenu,
            LevelSelect,
            InGame,
            Exit,
            Credits
        }

        private static GameState _state;

        public static GameState GState
        {
            get => _state;
            set => _state = value;
        }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {        
            base.Initialize();
            MainMenu.Init();
            Credits.Init();
            HexMap.Init(Content);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            MainMenu.LoadTexture(Content);
            Credits.LoadTexture(Content);
            HexMap.LoadContent(Content, _graphics);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
                                  
            base.Update(gameTime);

            switch (_state)
            {
                case GameState.MainMenu:
                    MainMenu.Update(gameTime);
                    break;
                case GameState.Credits:
                    Credits.Update(gameTime);
                    break;
                case GameState.LevelSelect:
                    break;
                case GameState.InGame:
                    HexMap.Update(gameTime, GraphicsDevice);
                    break;
                case GameState.Exit:
                    Exit();
                    break;
                default:
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            switch (_state)
            {
                case GameState.MainMenu:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    MainMenu.Draw(_spriteBatch,gameTime);
                    break;
                case GameState.Credits:
                    GraphicsDevice.Clear(Color.YellowGreen);
                    Credits.Draw(_spriteBatch, gameTime);
                    break;
                case GameState.LevelSelect:
                    break;
                case GameState.InGame:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    HexMap.DrawInGame(gameTime);
                    break;
                default:
                    break;
            }
        }
    }
}
