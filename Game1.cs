using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

//Adding SubClasses within Folders
using Guus_Reise.Menu;
using Guus_Reise.HexangonMap;
using Guus_Reise.Animation;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace Guus_Reise
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        private static KeyboardState _prevKeyState;
        private static Song _mainMenuSong;

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
            MovementAnimation,
            YouWon
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
           
            MainMenu.Init(_graphics);
            YouWon.Init();
            Credits.Init();  
            CharakterAnimationManager.Init(Content);            //CharakterAnimationManager muss VOR der HexMap initialisiert werden
            Weapon.LoadWeapons(Content);                        //Waffen müssen vor den Charakteren initialisert werden
            Skill.LoadSkills(Content);
            LevelHandler.InitContent(Content);
            HexMap.Init(Content, GraphicsDevice, _graphics);
            InformationIcon.Init(GraphicsDevice);
            PlanetMenu.Init(_graphics);
            Fighthandler.Init(GraphicsDevice, Content);
            GameOver.Init(Content);
            Controls.Init(Content);
            _mainMenuSong = Content.Load<Song>("Sounds\\Hypnotic-Puzzle2");
            MediaPlayer.Play(_mainMenuSong);
            MediaPlayer.IsRepeating = true;
            
        }

        protected override void LoadContent()
        {
            InformationComponents.Init(GraphicsDevice, Content);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textureSoundButton  = Content.Load<Texture2D>("Buttons\\ButtonSound");
            textureSoundButtonOff = Content.Load<Texture2D>("Buttons\\soundButtonOff");

            mainMenuFont = Content.Load<SpriteFont>("MainMenu\\MainMenuFont");

            MainMenu.LoadTexture(Content);
            YouWon.LoadTexture(Content);
            PlanetMenu.LoadTexture(Content, _spriteBatch);
            Credits.LoadTexture(Content);
            HexMap.LoadContent(Content, _graphics);
            InformationIcon.LoadTexture(Content);
            MovementAnimationManager.LoadTextures(Content, _spriteBatch);
            FightPlayer._soundEffect = Content.Load<SoundEffect>("Sounds\\mixkit-knife-fast-hit-2184");
            
        }

        protected override void Update(GameTime gameTime)
        {
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
                    MediaPlayer.Stop();
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
                case GameState.YouWon:
                    YouWon.Update(gameTime);
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
                    GraphicsDevice.Clear(Color.Black);
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
                case GameState.YouWon:
                    GraphicsDevice.Clear(Color.Blue);
                    YouWon.Draw(_spriteBatch, gameTime);
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

            if (_state == GameState.MovementAnimation)
            {
                MovementAnimationManager.SetParameterFromWindowScale();
            }

            HexMap.SetParameterFromWindowScale();

            if (_state == GameState.InFight || _state == GameState.InTalkFight)
            {
                Fighthandler.fightResults.UpdateScreenParameters();
                Fighthandler.fightMenu.SetParameterFromWindowScale();
                Fighthandler.fightMenu.CheckMenuStatus();
                
            }



        }
    }
}
