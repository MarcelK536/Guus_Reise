﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

//Adding SubClasses within Folders
using Guus_Reise.Menu;
using Guus_Reise.HexangonMap;
using Guus_Reise.Animation;


namespace Guus_Reise
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        private static KeyboardState _prevKeyState;

        public static Texture2D textureSoundButton;
        public static Texture2D textureSoundButtonOff;
        public static Texture2D buttonPlanke;

        public static SpriteFont mainMenuFont;

        public static bool defaultValueSoundOn = false;
        

        public enum GameState
        {
            MainMenu,
            Controls,
            LevelSelect,
            InGame,
            InFight,
            InTalkFight,
            GameOver,
            Exit,
            Credits,
            PlanetMenu,
            MovementAnimation
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
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            base.Initialize();
            InformationComponents.Init(GraphicsDevice, Content);
            MainMenu.Init(_graphics);
            Credits.Init();  
            CharakterAnimationManager.Init(Content);            //CharakterAnimationManager muss VOR der HexMap initialisiert werden
            Weapon.LoadWeapons(Content);                        //Waffen müssen vor den Charakteren initialisert werden
            Skill.LoadSkills(Content);
            LevelHandler.InitContent(Content);
            HexMap.Init(Content, GraphicsDevice, _graphics);
            PlanetMenu.Init(_graphics);
            Fighthandler.Init(GraphicsDevice, Content);
            GameOver.Init(Content);
            Controls.Init(Content);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textureSoundButton  = Content.Load<Texture2D>("Buttons\\ButtonSound");
            textureSoundButtonOff = Content.Load<Texture2D>("Buttons\\soundButtonOff");
            buttonPlanke = Content.Load<Texture2D>("Buttons\\buttonPlanke");
            mainMenuFont = Content.Load<SpriteFont>("MainMenu\\MainMenuFont");

            MainMenu.LoadTexture(Content);
            PlanetMenu.LoadTexture(Content, _spriteBatch);
            Credits.LoadTexture(Content);
            HexMap.LoadContent(Content, _graphics);
            MovementAnimationManager.LoadTextures(Content, _spriteBatch);
            
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
                case GameState.Controls:
                    Controls.Update(gameTime);
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
                    Fighthandler.Update(gameTime, GraphicsDevice);
                    break;
                case GameState.InTalkFight:
                    Fighthandler.Update(gameTime, GraphicsDevice);
                    break;
                case GameState.GameOver:
                    GameOver.Update(gameTime, GraphicsDevice);
                    break;
                case GameState.Exit:
                    Exit();
                    break;
                case GameState.MovementAnimation:
                    MovementAnimationManager.UdpateMovement(gameTime);
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
                case GameState.Controls:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    Controls.Draw(_spriteBatch, gameTime);
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
                    GraphicsDevice.Clear(Color.Black);
                    Fighthandler.Draw(_spriteBatch, gameTime);
                    break;
                case GameState.InTalkFight:
                    GraphicsDevice.Clear(Color.Coral);
                    Fighthandler.Draw(_spriteBatch, gameTime);
                    break;
                case GameState.GameOver:
                    GraphicsDevice.Clear(Color.DarkBlue);
                    GameOver.Draw(_spriteBatch, gameTime);
                    break;
                case GameState.MovementAnimation:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    HexMap.DrawInGame(_spriteBatch, gameTime);
                    MovementAnimationManager.DrawMovementAnimation();
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
            if(_state == GameState.MovementAnimation)
            {
                MovementAnimationManager.SetParameterFromWindowScale();
            }
            if(_state == GameState.InGame)
            {
                HexMap.SetParameterFromWindowScale();
            }
            if (_state == GameState.InFight)
            {
                Fighthandler.fightMenu.SetParameterFromWindowScale();
            }



        }
    }
}
