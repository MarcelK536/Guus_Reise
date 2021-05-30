using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Guus_Reise
{
    class HexMap
    {
        public static Hex[,] _board; //Spielbrett
        private static GraphicsDevice _graphicsDevice;

        private static Camera _camera;
        private static int lastwheel; // hilfsvariable für Camerazoom
        public static Hex activeTile; //active Tile nach linkem Mousclick
        public static Hex hoverTile; //Tile über welchem der mauszeiger steht
        public static Hex moveTile; //als Zugziel ausgewühltes Tile
        private static List<Point> possibleMoves = new List<Point>();
        private static List<Hex> enemyNeighbourTiles = new List<Hex>();
        private static List<Hex> friendNeighbourTiles = new List<Hex>();
        private static int enemyNeighbourCount;
        private static int friendlyNeighbourCount;
        private static MouseState _prevMouseState;
        private static KeyboardState _prevKeyState;

        public static MoveMenu actionMenu;
        public static SpriteFont actionMenuFont;

        public static SkillUpMenu levelUpMenu;

        public static void Init(ContentManager Content, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            int[,] tilemap = new int[,] { { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
            int[,] charakter = new int[,] { { 20, 10, 8, 5, 5, 8, 2, 5, 0 }, { 20, 7, 8, 9, 8, 8, 2, 4, 1 }, { 20, 7, 8, 9, 8, 8, 2, 4, 0 } };         //input Array für die Charaktere
            string[] names = new string[] { "Guu", "Peter", "Paul" };       //input Array für Namen
            int[,] charPositions = new int[,] { { 0, 1 }, { 4, 4 }, { 4, 2 } };   //input Array für Positionen

            Createboard(tilemap, Content);
            CreateCharakter(names, charakter, charPositions, _board);
            lastwheel = 0;
            _prevMouseState = Mouse.GetState();
            _prevKeyState = Keyboard.GetState();

            actionMenuFont = Content.Load<SpriteFont>("MainMenu\\MainMenuFont");
            actionMenu = new MoveMenu(actionMenuFont,graphicsDevice);
            levelUpMenu = new SkillUpMenu(actionMenuFont, graphicsDevice);
            _graphicsDevice = graphicsDevice;
        }

        public static void LoadContent(ContentManager content, GraphicsDeviceManager _graphics)
        {
            _camera = new Camera((float)_graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight);
        }

        public static void Update(GameTime time, GraphicsDevice graphicsDevice)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keystate = Keyboard.GetState();
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
         
            if (Keyboard.GetState().IsKeyDown(Keys.H) && _prevKeyState.IsKeyUp(Keys.H))
            {
                levelUpMenu.Active = !levelUpMenu.Active;
            }
            NoGlow();

            for (int i = 0; i < _board.GetLength(0); i++) //berechnet ob die Maus über einem Tile steht, sowie dieses Tile
            {
                for (int k = 0; k < _board.GetLength(1); k++)
                {

                    float? distance = Intersects(mouseLocation, _board[i, k].Tile.Tile1, _board[i, k].Tile.World, _camera.view, _camera.projection, graphicsDevice.Viewport);
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
                if (mouseOverSomething)
                {
                    _board[hoverTile.LogicalPosition.X, hoverTile.LogicalPosition.Y].Tile.Glow = new Vector3(0.3f, 0.3f, 0.3f);

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released) //wenn zusätzlich die linke Maustaste gedrückt wird, wird das hoverTile zum activeTile
                    {
                        activeTile = hoverTile;
                    }
                }
            }
            else
            {
                _board[activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y].Tile.Glow = new Vector3(0.5f, 0.5f, 0.5f); //das activeTile wird hervorgehoben
                levelUpMenu.Update(_board, activeTile);

                if (activeTile.Charakter != null)
                {
                    _board[activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y].Tile.Color = new Vector3(0, 0, 2);
                    CalculatePossibleMoves(activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y, activeTile.Charakter.Bewegungsreichweite);
                    possibleMoves = possibleMoves.Distinct().ToList();      //entfernt alle Duplikate aus der Liste

                    if (mouseOverSomething)
                    {
                        _board[hoverTile.LogicalPosition.X, hoverTile.LogicalPosition.Y].Tile.Glow = new Vector3(0.3f, 0.3f, 0.3f);

                        if (Mouse.GetState().LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released && possibleMoves.Contains(hoverTile.LogicalPosition) && hoverTile.LogicalPosition != activeTile.LogicalPosition) //wenn ein possibleMove Tile geklickt wird, wird dieses aks Zug vorgemerkt
                        {
                            actionMenu.Active = true;
                            actionMenu.fightTrue = false;
                            actionMenu.interactTrue = false;
                            moveTile = hoverTile;
                            List<Hex> neighbours = new List<Hex>(GetNeighbourTiles(moveTile));
                            enemyNeighbourCount = 0;
                            friendlyNeighbourCount = 0;
                            enemyNeighbourTiles.Clear();
                            friendNeighbourTiles.Clear();
                            foreach(Hex tile in neighbours)
                            {
                                if(tile.Charakter != null && tile != activeTile)
                                {
                                    if (tile.Charakter.IsNPC != activeTile.Charakter.IsNPC)
                                    {
                                        enemyNeighbourCount++;
                                        enemyNeighbourTiles.Add(tile);
                                    }
                                    else
                                    {
                                        friendlyNeighbourCount++;
                                        friendNeighbourTiles.Add(tile);
                                    }
                                }
                            }
                            if(enemyNeighbourCount > 0)
                            {
                                actionMenu.fightTrue = true;
                            }
                            if(friendlyNeighbourCount > 0)
                            {
                                actionMenu.interactTrue = true;
                            }
                        }
                    }
                }

                if (Mouse.GetState().RightButton == ButtonState.Pressed && _prevMouseState.RightButton == ButtonState.Released)    //wenn die rechte Maustaste gedrückt wird, wird das activeTile zurückgesetzt
                {
                    activeTile = null;
                    moveTile = null;
                    actionMenu.Active = false;
                    enemyNeighbourCount = 0;
                    friendlyNeighbourCount = 0;
                    enemyNeighbourTiles.Clear();
                    friendNeighbourTiles.Clear();
                    actionMenu.fightTrue = false;
                    actionMenu.interactTrue = false;
                }
            }
            actionMenu.Update(_board, activeTile, moveTile);
            _prevMouseState = mouseState;
            _prevKeyState = keystate;

        }
        public static void DrawInGame(SpriteBatch spriteBatch,GameTime gameTime)
        {
            for (int i = 0; i < _board.GetLength(0); i++)           //sorgt dafür das jedes einzelne Tile in _board auf der Kamera abgebildet wird
            {
                for (int k = 0; k < _board.GetLength(1); k++)
                {
                    _board[i, k].Draw(_camera);
                }
            }

            actionMenu.Draw(spriteBatch);
            if (activeTile != null)
            {
                    levelUpMenu.Draw(spriteBatch, _board, activeTile);               
            }
            
        }
        public static void Createboard(int[,] tilemap, ContentManager Content)                                 //generiert die Map, jedes Tile wird einzeln erstell und im _board gespeichert
        {
            _board = new Hex[tilemap.GetLength(0), tilemap.GetLength(1)];       //hier wird die größe von _board festgelegt, immer so groß wie der eingabe array -> ermöglicht dynamische Mapgröße

            for (int i = 0; i < tilemap.GetLength(0); i++)
            {
                for (int k = 0; k < tilemap.GetLength(1); k++)
                {
                    if (k % 2 == 0)                                             //unterscheidung da bei Hex Map jede zweite Reihe versetzt ist -> im else für z koordinate -0,5
                    {
                        Tile hilf = new Tile(new Vector3(i, 0, (k * 0.8665f)), tilemap[i, k], Content);
                        _board[i, k] = new Hex(new Vector3(i, 0, (k * 0.8665f)), new Point(i, k), hilf);

                    }
                    else
                    {
                        Tile hilf = new Tile(new Vector3(i + 0.5f, 0, (k * 0.8665f)), tilemap[i, k], Content);
                        _board[i, k] = new Hex(new Vector3(i + 0.5f, 0, (k * 0.8665f)), new Point(i, k), hilf);
                    }
                }
            }
        }
        public static float? Intersects(Vector2 mouseLocation, Model model, Matrix world, Matrix view, Matrix projection, Viewport viewport) //gibt die küruzeste distanz zum Model zurück (null falls keine Kollision)
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
        public static float? IntersectDistance(BoundingSphere sphere, Vector2 mouseLocation, Matrix view, Matrix projection, Viewport viewport)    //überprüft wie weit ein Strahl reisen muss um mit dem gegebenen Objekt zu kollidieren
        {
            Ray mouseRay = CalculateRay(mouseLocation, view, projection, viewport);
            return mouseRay.Intersects(sphere);
        }
        public static Ray CalculateRay(Vector2 mouseLocation, Matrix view, Matrix projection, Viewport viewport)       //erstellt einen Strahl im 3D Raum, mit dem Mauszeiger als Usprung, in Blickrichtung
        {
            Vector3 nearPoint = viewport.Unproject(new Vector3(mouseLocation.X, mouseLocation.Y, 0.0f),
                projection, view, Matrix.Identity);

            Vector3 farPoint = viewport.Unproject(new Vector3(mouseLocation.X, mouseLocation.Y, 1.0f),
                projection, view, Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            return new Ray(nearPoint, direction);
        }
        public static void NoGlow() //setzt den gesamten Glow der Map zurück
        {
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int k = 0; k < _board.GetLength(1); k++)
                {
                    _board[i, k].Tile.Glow = new Vector3(0.1f, 0.1f, 0.1f);
                    _board[i, k].Tile.Color = new Vector3(0, 0, 0);
                }
            }
        }
        public static void CalculatePossibleMoves(int x, int y, float bewegung) //hebt alle möglichen Züge hervor und speichert diese in possibleMoves
        {
            if (bewegung >= 0)
            {
                _board[x, y].Tile.Glow = new Vector3(0.2f, 0.2f, 0.2f);

                if (_board[x, y].Charakter != null && activeTile.LogicalPosition != new Point(x,y)) //erkennt andere charaktere
                {
                    if(_board[x, y].Charakter.IsNPC)
                    {
                        _board[x, y].Tile.Color = new Vector3(4, 0, 0);
                    }
                    else
                    {
                        _board[x, y].Tile.Color = new Vector3(0, 3, 0);
                    }
                    
                    possibleMoves.Remove(new Point(x, y));
                }
                else
                {

                    if (x - 1 >= 0)
                    {
                        possibleMoves.Add(new Point(x - 1, y));
                        CalculatePossibleMoves(x - 1, y, bewegung - _board[x - 1, y].Tile.Begehbarkeit);
                    }

                    if (x + 1 < _board.GetLength(0))
                    {
                        possibleMoves.Add(new Point(x + 1, y));
                        CalculatePossibleMoves(x + 1, y, bewegung - _board[x + 1, y].Tile.Begehbarkeit);
                    }

                    if (y - 1 >= 0)
                    {
                        possibleMoves.Add(new Point(x, y - 1));
                        CalculatePossibleMoves(x, y - 1, bewegung - _board[x, y - 1].Tile.Begehbarkeit);
                    }

                    if (y + 1 < _board.GetLength(1))
                    {
                        possibleMoves.Add(new Point(x, y + 1));
                        CalculatePossibleMoves(x, y + 1, bewegung - _board[x, y + 1].Tile.Begehbarkeit);
                    }

                    if (y % 2 == 0)
                    {
                        if (x - 1 >= 0 && y - 1 >= 0)
                        {
                            possibleMoves.Add(new Point(x - 1, y - 1));
                            CalculatePossibleMoves(x - 1, y - 1, bewegung - _board[x - 1, y - 1].Tile.Begehbarkeit);
                        }

                        if (x - 1 >= 0 && y + 1 < _board.GetLength(1))
                        {
                            possibleMoves.Add(new Point(x - 1, y + 1));
                            CalculatePossibleMoves(x - 1, y + 1, bewegung - _board[x - 1, y + 1].Tile.Begehbarkeit);
                        }
                    }
                    else
                    {
                        if (x + 1 < _board.GetLength(0) && y - 1 >= 0)
                        {
                            possibleMoves.Add(new Point(x + 1, y - 1));
                            CalculatePossibleMoves(x + 1, y - 1, bewegung - _board[x + 1, y - 1].Tile.Begehbarkeit);
                        }

                        if (x + 1 < _board.GetLength(0) && y + 1 < _board.GetLength(1))
                        {
                            possibleMoves.Add(new Point(x + 1, y + 1));
                            CalculatePossibleMoves(x + 1, y + 1, bewegung - _board[x + 1, y + 1].Tile.Begehbarkeit);
                        }
                    }
                }

            }
            else
            {
                possibleMoves.Remove(new Point(x, y));
            }
        }
        public static void CreateCharakter(string[] names, int[,] charakter, int[,] positions, Hex[,] board)
        {
            int[] hilf = new int[charakter.GetLength(1)];

            for (int i = 0; i < charakter.GetLength(0); i++)
            {
                for (int k = 0; k < charakter.GetLength(1); k++)
                {
                    hilf[k] = charakter[i, k];
                }
                board[positions[i, 0], positions[i, 1]].Charakter = new Charakter(names[i], hilf);
                board[positions[i, 0], positions[i, 1]].Charakter.LogicalPosition = board[positions[i, 0], positions[i, 1]].LogicalPosition;
            }
        }
        public static List<Hex> GetNeighbourTiles(Hex tile)
        {
            List<Hex> list = new List<Hex>();
            int x = tile.LogicalPosition.X;
            int y = tile.LogicalPosition.Y;

            if (x - 1 >= 0)
            {
                list.Add(_board[x - 1, y]);
            }

            if (x + 1 < _board.GetLength(0))
            {
                list.Add(_board[x + 1, y]);
            }

            if (y - 1 >= 0)
            {
                list.Add(_board[x, y - 1]);
            }

            if (y + 1 < _board.GetLength(1))
            {
                list.Add(_board[x, y + 1]);
            }

            if (y % 2 == 0)
            {
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    list.Add(_board[x - 1, y - 1]);
                }

                if (x - 1 >= 0 && y + 1 < _board.GetLength(1))
                {
                    list.Add(_board[x - 1, y + 1]);
                }
            }
            else
            {
                if (x + 1 < _board.GetLength(0) && y - 1 >= 0)
                {
                    list.Add(_board[x + 1, y - 1]);
                }

                if (x + 1 < _board.GetLength(0) && y + 1 < _board.GetLength(1))
                {
                    list.Add(_board[x + 1, y + 1]);
                }
            }

            return list;
        }
    }
}
