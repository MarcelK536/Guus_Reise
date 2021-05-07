using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//Adding SubClasses within Folders
using Guus_Reise.Menu;

namespace Guus_Reise
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Tile[,] _board; //Spielbrett
        private Camera _camera;

        enum GameState
        {
            MainMenu,
            LevelSelect,
            InGame
        }
        GameState gameState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            int[,] tilemap = new int[,] { { 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 1 }, { 0, 0, 0, 0, 0 }, { 0, 0, 2, 2, 0 }, { 0, 0, 0, 2, 0 } }; //input Array der die Art der Tiles für die map generierung angibt
            createboard(tilemap);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _camera = new Camera((float)_graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight);
            
            MainMenu.LoadTexture(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            switch (gameState)
            {
                case GameState.MainMenu:
                    MainMenu.Update(gameTime);
                    break;
                case GameState.LevelSelect:
                    break;
                case GameState.InGame:
                    break;
                default:
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            switch (gameState)
            {
                case GameState.MainMenu:
                    MainMenu.Draw(_spriteBatch,gameTime);
                    break;
                case GameState.LevelSelect:
                    break;
                case GameState.InGame:
                    DrawInGame(gameTime);
                    break;
                default:
                    break;
            }
        }

        protected void DrawInGame(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int k = 0; k < _board.GetLength(1); k++)
                {
                    _board[i, k].Draw(_camera);
                }
            }

            base.Draw(gameTime);
        }


        public void createboard(int[,] tilemap)
        {
            _board = new Tile[tilemap.GetLength(0),tilemap.GetLength(1)];
            for(int i = 0; i < tilemap.GetLength(0); i++)
            {
                for(int k =0; k < tilemap.GetLength(1); k++)
                {
                    if (i % 2 == 0)
                    {
                        _board[i, k] = new Tile(new Vector3(i - tilemap.GetLength(0) / 2, 0, k - tilemap.GetLength(1) / 2), new Point(i, k), tilemap[i, k], Content); 
                    }
                    else
                    {
                        _board[i, k] = new Tile(new Vector3(i - tilemap.GetLength(0) / 2, 0, k - tilemap.GetLength(1) / 2-0.5f), new Point(i, k), tilemap[i, k], Content);
                    }
                }
            }
        }
    }
}
