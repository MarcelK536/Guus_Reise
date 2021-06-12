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
        private static KeyboardState _prevKeyState;

        public enum GameState
        {
            MainMenu,
            LevelSelect,
            InGame,
            InFight,
            Exit,
            Credits,
            PlanetMenu
        }

        private static GameState _state;

        internal static GameState GState
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
            CharakterAnimationManager.Init(Content);
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            base.Initialize();
            MainMenu.Init(_graphics);
            Credits.Init();
            HexMap.Init(Content, GraphicsDevice, _graphics);
            PlanetMenu.Init(_graphics);
            Fighthandler.Init(GraphicsDevice, Content);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            MainMenu.LoadTexture(Content);
            PlanetMenu.LoadTexture(Content, _spriteBatch);
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
                case GameState.PlanetMenu:
                    PlanetMenu.Update(gameTime);
                    break;
                case GameState.LevelSelect:
                    break;
                case GameState.InGame:
                    HexMap.Update(gameTime, GraphicsDevice);
                    break;
                case GameState.InFight:
                    Fighthandler.Update();
                    break;
                case GameState.Exit:
                    Exit();
                    break;
                default:
                    break;
            }

            // Screeen Scale
            if (Keyboard.GetState().IsKeyDown(Keys.F1) && _prevKeyState.IsKeyUp(Keys.F1))
            {
                ResizeScreen();
            }
            _prevKeyState = Keyboard.GetState();
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;     //Fixt Zeichenreihnfolge
            base.Draw(gameTime);

            switch (_state)
            {
                case GameState.MainMenu:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    MainMenu.Draw(_spriteBatch, gameTime);
                    break;
                case GameState.PlanetMenu:
                    GraphicsDevice.Clear(Color.Black);
                    PlanetMenu.Draw(_spriteBatch, gameTime);
                    break;
                case GameState.Credits:
                    GraphicsDevice.Clear(Color.YellowGreen);
                    Credits.Draw(_spriteBatch, gameTime);
                    break;
                case GameState.LevelSelect:
                    break;
                case GameState.InGame:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    HexMap.DrawInGame(_spriteBatch, gameTime);
                    break;
                case GameState.InFight:
                    GraphicsDevice.Clear(Color.Coral);
                    Fighthandler.Draw(_spriteBatch, gameTime);
                    break;
                default:
                    break;
            }
        }

        private void ResizeScreen()
        {
            if (_graphics.PreferredBackBufferWidth == 1000)
            {
                _graphics.PreferredBackBufferWidth = 1706;
                _graphics.PreferredBackBufferHeight = 1024;
                _graphics.IsFullScreen = true;
            }
            else
            {
                _graphics.PreferredBackBufferWidth = 1000;
                _graphics.PreferredBackBufferHeight = 600;
                _graphics.IsFullScreen = false;
                
            }
            _graphics.ApplyChanges();
            PlanetMenu.SetParametersFromWindowScale();
            MainMenu.SetParametersFromWindowScale();


        }
    }
}
