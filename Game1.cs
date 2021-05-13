using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

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
        private List<Point> possibleMoves = new List<Point>();
        private MouseState _prevMouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            int[,] tilemap = new int[,] { { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
            int[,] charakter = new int[,] { { 20, 10, 8, 5, 5, 8, 2, 5 }, { 20, 7, 8, 9, 8, 8, 2, 4 } };         //input Array für die Charaktere
            string[] names = new string[] { "Guu", "Peter" };
            int[,] charPositions = new int[,] { { 0, 1 }, { 4, 4 } };
            Createboard(tilemap);
            CreateCharakter(names, charakter, charPositions);
            lastwheel = 0;
            _prevMouseState = Mouse.GetState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _camera = new Camera((float)_graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouseState = Mouse.GetState();
            Vector2 mouseLocation = new Vector2(mouseState.X, mouseState.Y); //position der Maus auf dem monitor
            float? minDistance = float.MaxValue;
            bool mouseOverSomething = false;
            hoverTile = null;
            possibleMoves.Clear();

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

            NoGlow(); 

            for (int i = 0; i < _board.GetLength(0); i++) //berechnet ob die Maus über einem Tile steht, sowie dieses Tile
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

            if (activeTile == null)
            {
                if (mouseOverSomething)     //wenn die Maus über sich über einem Tile befindet wird dieses hervorgehoben
                {
                    _board[hoverTile.LogicalPosition.X, hoverTile.LogicalPosition.Y].Glow = new Vector3(0.4f, 0.4f, 0.4f);

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released) //wenn zusätzlich die linke Maustaste gedrückt wird, wird das hoverTile zum activeTile
                    {
                        activeTile = hoverTile;
                    }
                }
            }
            else
            {
                _board[activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y].Glow = new Vector3(0.4f, 0.4f, 0.4f); //das activeTile wird hervorgehoben

                if (activeTile.Charakter != null)
                {
                    ShowMoves(activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y, activeTile.Charakter.Bewegungsreichweite);
                    possibleMoves = possibleMoves.Distinct().ToList();      //entfernt alle Duplikate aus der Liste
                    if(Mouse.GetState().LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released && possibleMoves.Contains(hoverTile.LogicalPosition))
                    {
                        _board[hoverTile.LogicalPosition.X, hoverTile.LogicalPosition.Y].Charakter = _board[activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y].Charakter;
                        _board[activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y].Charakter = null;
                        activeTile = null;
                    }
                }

                if (Mouse.GetState().RightButton == ButtonState.Pressed && _prevMouseState.RightButton == ButtonState.Released)    //wenn die rechte Maustaste gedrückt wird, wird das activeTile zurückgesetzt
                {                   
                    activeTile = null;
                }
            }

            _prevMouseState = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
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

        public void NoGlow() //setzt den gesamten Glow der Mapd zurück
        {
            for(int i=0; i< _board.GetLength(0); i++)
            {
                for (int k = 0; k < _board.GetLength(1); k++)
                {
                    _board[i, k].Glow = new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }

        public void CreateCharakter(string[] names, int[,] charakter, int[,] positions)
        {
            int[] hilf = new int[charakter.GetLength(1)];

            for (int i = 0; i < charakter.GetLength(0); i++)
            {
                for (int k = 0; k < charakter.GetLength(1); k++)
                {
                    hilf[k] = charakter[i, k];
                }

                _board[positions[i,0], positions[i,1]].Charakter = new Charakter(names[i], hilf);
            }
        }

        public void ShowMoves(int x, int y, float bewegung)
        {
            if (bewegung >= 0)
            {
                _board[x, y].Glow = new Vector3(0.3f, 0.3f, 0.3f);
                
                if (x - 1 >= 0)
                {
                    possibleMoves.Add(new Point(x - 1,y));
                    ShowMoves(x - 1, y, bewegung - _board[x - 1, y].Begehbarkeit);
                }

                if(x + 1 < _board.GetLength(0))
                {
                    possibleMoves.Add(new Point(x + 1, y));
                    ShowMoves(x + 1, y, bewegung - _board[x + 1, y].Begehbarkeit);
                }

                if(y - 1 >= 0)
                {
                    possibleMoves.Add(new Point(x, y - 1));
                    ShowMoves(x, y - 1, bewegung - _board[x, y - 1].Begehbarkeit);
                }

                if(y + 1 < _board.GetLength(1))
                {
                    possibleMoves.Add(new Point(x, y + 1));
                    ShowMoves(x, y + 1, bewegung - _board[x, y + 1].Begehbarkeit);
                }

                if (y % 2 == 0)
                {
                    if (x - 1 >= 0 && y - 1 >= 0)
                    {
                        possibleMoves.Add(new Point(x - 1, y - 1));
                        ShowMoves(x - 1, y - 1, bewegung - _board[x - 1, y - 1].Begehbarkeit);
                    }

                    if (x - 1 >= 0 && y + 1 < _board.GetLength(1))
                    {
                        possibleMoves.Add(new Point(x - 1, y - 1));
                        ShowMoves(x - 1, y + 1, bewegung - _board[x - 1, y + 1].Begehbarkeit);
                    }
                }
                else
                {
                    if (x + 1 >= 0 && y - 1 >= 0)
                    {
                        possibleMoves.Add(new Point(x + 1, y - 1));
                        ShowMoves(x + 1, y - 1, bewegung - _board[x + 1, y - 1].Begehbarkeit);
                    }

                    if (x + 1 >= 0 && y + 1 < _board.GetLength(1))
                    {
                        possibleMoves.Add(new Point(x + 1, y + 1));
                        ShowMoves(x + 1, y + 1, bewegung - _board[x + 1, y + 1].Begehbarkeit);
                    }
                }
            }           
        }
    }
}
