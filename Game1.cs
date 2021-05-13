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
        private int lastwheel;
        private Tile activeTile; //active Tile nach linkem Mousclick
        private Tile hoverTile; //Tile über welchem der mauszeiger steht

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

            int[,] tilemap = new int[,] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
            Createboard(tilemap);
            lastwheel = 0;
            activeTile = null;

            base.Initialize();
            MainMenu.Init();
            Credits.Init();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _camera = new Camera((float)_graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight);
            
            MainMenu.LoadTexture(Content);
            Credits.LoadTexture(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            Vector2 mouseLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y); //position der Maus auf dem monitor
            float? minDistance = float.MaxValue;
            bool mouseOverSomething = false;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _camera.MoveCamera("w");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _camera.MoveCamera("s");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _camera.MoveCamera("a");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _camera.MoveCamera("d");
            }

            if (Mouse.GetState().ScrollWheelValue > lastwheel)
            {
                lastwheel = Mouse.GetState().ScrollWheelValue;
                _camera.MoveCamera("hoch");
            }
            
            if (Mouse.GetState().ScrollWheelValue < lastwheel)
            {
                lastwheel = Mouse.GetState().ScrollWheelValue;
                _camera.MoveCamera("runter");
            }

            if(hoverTile != null)           //setzt das hoverTile zurück
            {
                _board[hoverTile.LogicalPosition.X, hoverTile.LogicalPosition.Y].Glow = new Vector3(0.1f, 0.1f, 0.1f);
                hoverTile = null;
            }

            if (activeTile == null)
            {

                for (int i = 0; i < _board.GetLength(0); i++) //wenn es kein activeTile gibt wird berechnet ob die Maus über einem Tile steht
                {
                    for (int k = 0; k < _board.GetLength(1); k++)
                    {

                        float? distance = Intersects(mouseLocation, _board[i, k].Tile1, _board[i, k].World, _camera.view, _camera.projection, GraphicsDevice.Viewport);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            hoverTile = _board[i, k];
                            mouseOverSomething = true;
                        }
                    }
                }

                if (mouseOverSomething)     //wenn die Maus über sich über einem Tile befindet wird dieses hervorgehoben
                {
                    _board[hoverTile.LogicalPosition.X, hoverTile.LogicalPosition.Y].Glow = new Vector3(0.4f, 0.4f, 0.4f);

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed) //wenn zusätzlich die linke Maustaste gedrückt wird, wird das hoverTile zum activeTile
                    {
                        activeTile = hoverTile;
                    }
                }
            }
            else
            {
                _board[activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y].Glow = new Vector3(0.4f, 0.4f, 0.4f); //das activeTile wird hervorgehoben

                if (Mouse.GetState().RightButton == ButtonState.Pressed)    //wenn die rechte Maustaste gedrpckt wird, wird das activeTile zurückgesetzt
                {                   
                    _board[activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y].Glow = new Vector3(0.1f, 0.1f, 0.1f);
                    activeTile = null;
                }
            }
            
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
                    DrawInGame(gameTime);
                    break;
                default:
                    break;
            }
        }

        protected void DrawInGame(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            for (int i = 0; i < _board.GetLength(0); i++)           //sorgt dafür das jedes einzelne Tile in _board auf der Kamera abgebildet wird
            {
                for (int k = 0; k < _board.GetLength(1); k++)
                {
                    _board[i, k].Draw(_camera);
                }
            }

            base.Draw(gameTime);
        }


        public void Createboard(int[,] tilemap)                                 //generiert die Map, jedes Tile wird einzeln erstell und im _board gespeichert
        {
            _board = new Tile[tilemap.GetLength(0),tilemap.GetLength(1)];       //hier wird die größe von _board festgelegt, immer so groß wie der eingabe array -> ermöglicht dynamische Mapgröße

            for(int i = 0; i < tilemap.GetLength(0); i++)
            {
                for(int k =0; k < tilemap.GetLength(1); k++)
                {
                    if (k % 2 == 0)                                             //unterscheidung da bei Hex Map jede zweite Reihe versetzt ist -> im else für z koordinate -0,5
                    {
                        _board[i, k] = new Tile(new Vector3(i, 0, (k*0.8665f)), new Point(i, k), tilemap[i, k], Content);

                    }
                    else
                    {
                        _board[i, k] = new Tile(new Vector3(i+0.5f, 0, (k*0.8665f)), new Point(i, k), tilemap[i, k], Content);
                    }
                }
            }
        }

        public float? Intersects(Vector2 mouseLocation, Model model, Matrix world, Matrix view, Matrix projection, Viewport viewport) //gibt die küruzeste distanz zum Model zurück (null falls keine Kollision)
        {
            float? minDistance = null;

            for (int index = 0; index < model.Meshes.Count; index++)    //berechnet für jedes Mesh im Model einen sphere um den Punkt und überprüft auf strahlenkollision
            {
                BoundingSphere sphere = model.Meshes[index].BoundingSphere;
                sphere = sphere.Transform(Matrix.CreateScale(0.8f) * world);
                float? distance = IntersectDistance(sphere, mouseLocation, view, projection, viewport);
                if (minDistance == null)
                {
                    minDistance = distance;
                }
                if (distance != null & distance < minDistance)
                {
                    minDistance = distance;
                }
            }
            return minDistance;
        }

        public float? IntersectDistance(BoundingSphere sphere, Vector2 mouseLocation, Matrix view, Matrix projection, Viewport viewport)    //überprüft wie weit ein Strahl reisen muss um mit dem gegebenen Objekt zu kollidieren
        {
            Ray mouseRay = CalculateRay(mouseLocation, view, projection, viewport);
            return mouseRay.Intersects(sphere);
        }

        public Ray CalculateRay(Vector2 mouseLocation, Matrix view, Matrix projection, Viewport viewport)       //erstellt einen Strahl im 3D Raum, mit dem Mauszeiger als Usprung, in Blickrichtung
        {
            Vector3 nearPoint = viewport.Unproject(new Vector3(mouseLocation.X, mouseLocation.Y, 0.0f),
                projection, view, Matrix.Identity);

            Vector3 farPoint = viewport.Unproject(new Vector3(mouseLocation.X, mouseLocation.Y, 1.0f),
                projection, view, Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            return new Ray(nearPoint, direction);
        } 
    }
}
