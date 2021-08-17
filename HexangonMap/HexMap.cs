using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Guus_Reise.HexangonMap;
using Guus_Reise.Animation;

namespace Guus_Reise
{
    class HexMap
    {
        public static Hex[,] _board; //Spielbrett
        public static List<Point> possibleMoves = new List<Point>();
        public static VisualisationManagerHexmap visManager;

        public static Hex activeHex;
        public static Hex hoveredHex;

        private static Camera _camera;

        public static List<Charakter> npcs = new List<Charakter>();
        public static List<Charakter> playableCharacter = new List<Charakter>();
        public static List<Hex> enemyNeighbourTiles = new List<Hex>();
        public static List<Hex> friendNeighbourTiles = new List<Hex>();
        public static int enemyNeighbourCount;
        public static int friendlyNeighbourCount;
        
        private static bool playerTurn;
        public static bool[] lvlObjectives;
        public static string[] lvlObjectiveText;

        internal static Camera Camera { get => _camera; set => _camera = value; }



        public static void Init(ContentManager Content, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            _board = LevelDatabase.W1L1.Board;
            playableCharacter = LevelDatabase.W1L1.PlayableCharacters;
            npcs = LevelDatabase.W1L1.NPCCharacters;
            lvlObjectives = LevelDatabase.W1L1.LevelObjective;
            lvlObjectiveText = LevelDatabase.W1L1.LevelObjectiveText;

            visManager = new VisualisationManagerHexmap(_board.GetLength(0), _board.GetLength(1), Camera);
            //Fokus der Camera auf die Mitte der Karte setzen
            visManager.SetCameraToMiddleOfMap();


            Player._prevMouseState = Mouse.GetState();
            Player._prevKeyState = Keyboard.GetState();
            playerTurn = true;

            Player.actionMenuFont = Content.Load<SpriteFont>("Fonts\\Jellee");
            Player.actionMenu = new MoveMenu(Player.actionMenuFont,graphicsDevice, SimpleMenu.BlendDirection.LeftToRight);
            Player.levelUpMenu = new SkillUpMenu(Player.actionMenuFont, graphicsDevice, SimpleMenu.BlendDirection.None);
            Player.objectiveMenu = new LevelObjectiveMenu(Player.actionMenuFont, graphicsDevice, SimpleMenu.BlendDirection.TopToBottom);
            Player.charakterMenu = new CharakterMenu(Player.actionMenuFont, graphicsDevice, SimpleMenu.BlendDirection.None);
        }

        public static void LoadContent(ContentManager content, GraphicsDeviceManager _graphics)
        {
            Camera = new Camera((float)_graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight);
        }
        public static void Update(GameTime time, GraphicsDevice graphicsDevice)
        {

            // Aktualisieren der Charakter-Positionen
            foreach(Charakter c in playableCharacter)
            {
                c.CharakterAnimation.Update(time);
            }
            foreach(Charakter c in npcs)
            {
                c.CharakterAnimation.Update(time);
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
            visManager.Update(time);
            if(activeHex != null)
            {
                CharakterAnimationManager.ActiveHexExists = true;
            }
            else
            {
                CharakterAnimationManager.ActiveHexExists = false;
            }
            LevelDatabase.UpdateObjective();
        }

        public static void DrawInGame(SpriteBatch spriteBatch,GameTime gameTime)
        {
            for (int i = 0; i < _board.GetLength(0); i++)           //sorgt dafür das jedes einzelne Tile in _board auf der Kamera abgebildet wird
            {
                for (int k = 0; k < _board.GetLength(1); k++)
                {
                    _board[i, k].Draw(Camera);
                }
            }

            //Zeichnen der Charaktere nach dem die komplette Map fertig ist (da es sonst zu nem Graphik-Bug kommt)
            foreach(Charakter c in playableCharacter)
            {
                c.Draw(Camera);
            }
            foreach (Charakter c in npcs)
            {
                c.Draw(Camera);
            }

            if (playerTurn)
            {
                Player.Draw(spriteBatch, gameTime);
            }

        }

        public static Hex[,] CreateHexboard(int[,] tilemap, ContentManager Content)                                 //generiert die Map, jedes Tile wird einzeln erstell und im _board gespeichert
        {
            Hex[,] createBoard;
            createBoard = new Hex[tilemap.GetLength(0), tilemap.GetLength(1)];       //hier wird die größe von _board festgelegt, immer so groß wie der eingabe array -> ermöglicht dynamische Mapgröße

            for (int i = 0; i < tilemap.GetLength(0); i++)
            {
                for (int k = 0; k < tilemap.GetLength(1); k++)
                {
                    if (k % 2 == 0)                                             //unterscheidung da bei Hex Map jede zweite Reihe versetzt ist -> im else für z koordinate -0,5
                    {
                        Tile hilf = new Tile(new Vector3(i, 0, (k * 0.8665f)), tilemap[i, k], Content);
                        createBoard[i, k] = new Hex(new Vector3(i, 0, (k * 0.8665f)), new Point(i, k), hilf);

                    }
                    else
                    {
                        Tile hilf = new Tile(new Vector3(i + 0.5f, 0, (k * 0.8665f)), tilemap[i, k], Content);
                        createBoard[i, k] = new Hex(new Vector3(i + 0.5f, 0, (k * 0.8665f)), new Point(i, k), hilf);
                    }
                }
            }
            return createBoard;
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
                    _board[i, k].Tile.Glow = new Vector3(1f, 1f, 1f);
                    _board[i, k].Tile.Color = new Vector3(0.6f, 0.6f, 0.6f);
                    _board[i, k].Tile.isglowing = false;
                }
            }
        }
        public static void CalculatePossibleMoves(int x, int y, float bewegung, Hex activeTile) //hebt alle möglichen Züge hervor und speichert diese in possibleMoves
        {
            if (bewegung >= 0)
            {
                _board[x, y].Tile.Glow = new Vector3(0.2f, 0.2f, 0.2f);
                _board[x, y].Tile.isglowing = true;

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
            Hex curr;

            for (int i = 0; i < charakter.GetLength(0); i++)
            {
                //for (int k = 0; k < charakter.GetLength(1); k++)
                //{
                //    hilf[k] = charakter[i, k];
                //}
                //_board[positions[i, 0], positions[i, 1]].Charakter = new Charakter(names[i], hilf);
                curr = _board[positions[i, 0], positions[i, 1]];
                curr.Charakter = new Charakter(names[i], charakter[i], curr, CharakterAnimationManager.GetCharakterAnimation(names[i]));
                _board[positions[i, 0], positions[i, 1]].Charakter.LogicalBoardPosition = _board[positions[i, 0], positions[i, 1]].LogicalPosition;
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
