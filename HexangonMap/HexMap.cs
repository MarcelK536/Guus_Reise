using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Guus_Reise
{
    class HexMap
    {
        public static Hex[,] _board; //Spielbrett
        public static List<Point> possibleMoves = new List<Point>();

        public static Camera _camera;
        public static int lastwheel; // hilfsvariable für Camerazoom

        public static List<Charakter> npcs = new List<Charakter>();
        public static List<Charakter> playableCharacter = new List<Charakter>();
        public static List<Hex> enemyNeighbourTiles = new List<Hex>();
        public static List<Hex> friendNeighbourTiles = new List<Hex>();
        public static int enemyNeighbourCount;
        public static int friendlyNeighbourCount;
        
        private static bool playerTurn;

        public static void Init(ContentManager Content, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            int[,] tilemap = new int[,] { { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
            //int[,] charakter = new int[,] { { 20, 10, 8, 5, 5, 8, 2, 5, 0, 0 }, { 20, 7, 8, 9, 8, 8, 2, 4, 1, 1 }, { 20, 7, 8, 9, 8, 8, 2, 4, 0, 0 } };         //input Array für die Charaktere
            int[] charakterlevel = new int[] { 5, 4, 4 };
            string[] names = new string[] { "Guu", "Peter", "Paul" };       //input Array für Namen
            int[,] charPositions = new int[,] { { 0, 1 }, { 4, 4 }, { 4, 2 } };   //input Array für Positionen

            Createboard(tilemap, Content);
            CreateCharakter(names, charakterlevel, charPositions);
            lastwheel = 0;
            Player._prevMouseState = Mouse.GetState();
            Player._prevKeyState = Keyboard.GetState();
            playerTurn = true;

            Player.actionMenuFont = Content.Load<SpriteFont>("MainMenu\\MainMenuFont");
            Player.actionMenu = new MoveMenu(Player.actionMenuFont,graphicsDevice);
            Player.levelUpMenu = new SkillUpMenu(Player.actionMenuFont, graphicsDevice);
        }

        public static void LoadContent(ContentManager content, GraphicsDeviceManager _graphics)
        {
            _camera = new Camera((float)_graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight);
        }
        public static void Update(GameTime time, GraphicsDevice graphicsDevice)
        {
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

            if (playerTurn)
            {
                Player.Update(time, graphicsDevice);
                int movecounter = playableCharacter.Count;
                foreach(Charakter charakter in playableCharacter)
                {
                    if (!charakter.CanMove)
                    {
                        movecounter--;
                    }
                }
                if (movecounter <= 0)
                {
                    playerTurn = !playerTurn;
                    foreach(Charakter charakter in npcs)
                    {
                        charakter.CanMove = true;
                    }
                }
            }
            else
            {
                KI.Update(time, graphicsDevice);
                int movecounter = npcs.Count;
                foreach (Charakter charakter in npcs)
                {
                    if (!charakter.CanMove)
                    {
                        movecounter--;
                    }
                }
                if (movecounter <= 0)
                {
                    playerTurn = !playerTurn;
                    foreach(Charakter charakter in playableCharacter)
                    {
                        charakter.CanMove = true;
                    }
                }
            }
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
            if (playerTurn)
            {
                Player.Draw(spriteBatch, gameTime);
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
        public static void CalculatePossibleMoves(int x, int y, float bewegung, Hex activeTile) //hebt alle möglichen Züge hervor und speichert diese in possibleMoves
        {
            if (bewegung >= 0)
            {
                _board[x, y].Tile.Glow = new Vector3(0.2f, 0.2f, 0.2f);

                if (_board[x, y].Charakter != null && activeTile.LogicalPosition != new Point(x,y)) //erkennt andere charaktere
                {
                    if(_board[x, y].Charakter.IsNPC != activeTile.Charakter.IsNPC)
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
                        CalculatePossibleMoves(x - 1, y, bewegung - _board[x - 1, y].Tile.Begehbarkeit, activeTile);
                    }

                    if (x + 1 < _board.GetLength(0))
                    {
                        possibleMoves.Add(new Point(x + 1, y));
                        CalculatePossibleMoves(x + 1, y, bewegung - _board[x + 1, y].Tile.Begehbarkeit, activeTile);
                    }

                    if (y - 1 >= 0)
                    {
                        possibleMoves.Add(new Point(x, y - 1));
                        CalculatePossibleMoves(x, y - 1, bewegung - _board[x, y - 1].Tile.Begehbarkeit, activeTile);
                    }

                    if (y + 1 < _board.GetLength(1))
                    {
                        possibleMoves.Add(new Point(x, y + 1));
                        CalculatePossibleMoves(x, y + 1, bewegung - _board[x, y + 1].Tile.Begehbarkeit, activeTile);
                    }

                    if (y % 2 == 0)
                    {
                        if (x - 1 >= 0 && y - 1 >= 0)
                        {
                            possibleMoves.Add(new Point(x - 1, y - 1));
                            CalculatePossibleMoves(x - 1, y - 1, bewegung - _board[x - 1, y - 1].Tile.Begehbarkeit, activeTile);
                        }

                        if (x - 1 >= 0 && y + 1 < _board.GetLength(1))
                        {
                            possibleMoves.Add(new Point(x - 1, y + 1));
                            CalculatePossibleMoves(x - 1, y + 1, bewegung - _board[x - 1, y + 1].Tile.Begehbarkeit, activeTile);
                        }
                    }
                    else
                    {
                        if (x + 1 < _board.GetLength(0) && y - 1 >= 0)
                        {
                            possibleMoves.Add(new Point(x + 1, y - 1));
                            CalculatePossibleMoves(x + 1, y - 1, bewegung - _board[x + 1, y - 1].Tile.Begehbarkeit, activeTile);
                        }

                        if (x + 1 < _board.GetLength(0) && y + 1 < _board.GetLength(1))
                        {
                            possibleMoves.Add(new Point(x + 1, y + 1));
                            CalculatePossibleMoves(x + 1, y + 1, bewegung - _board[x + 1, y + 1].Tile.Begehbarkeit, activeTile);
                        }
                    }
                }

            }
            else
            {
                possibleMoves.Remove(new Point(x, y));
            }
        }
        public static void CreateCharakter(string[] names, int[] charakter, int[,] positions)
        {
            //int[] hilf = new int[charakter.GetLength(1)];

            for (int i = 0; i < charakter.GetLength(0); i++)
            {
                //for (int k = 0; k < charakter.GetLength(1); k++)
                //{
                //    hilf[k] = charakter[i, k];
                //}
                //_board[positions[i, 0], positions[i, 1]].Charakter = new Charakter(names[i], hilf);
                _board[positions[i, 0], positions[i, 1]].Charakter = new Charakter(names[i], charakter[i]);
                _board[positions[i, 0], positions[i, 1]].Charakter.LogicalPosition = _board[positions[i, 0], positions[i, 1]].LogicalPosition;
                if (_board[positions[i, 0], positions[i, 1]].Charakter.IsNPC)
                {
                    _board[positions[i, 0], positions[i, 1]].Charakter.CanMove = false;
                    npcs.Add(_board[positions[i, 0], positions[i, 1]].Charakter);
                }
                else
                {
                    _board[positions[i, 0], positions[i, 1]].Charakter.CanMove = true;
                    playableCharacter.Add(_board[positions[i, 0], positions[i, 1]].Charakter);
                }
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
