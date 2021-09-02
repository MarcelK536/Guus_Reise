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

        public static GraphicsDevice _graphicsDevice;



        public static Hex activeHex;
        public static Hex hoveredHex;
        public static Hex soundHex;
        public static Hex prevSoundHex;

        static Texture2D _backroundMain;
        public static Button btSoundEinstellungen;

        private static Camera _camera;

        public static List<Charakter> npcs = new List<Charakter>();
        public static List<Charakter> playableCharacter = new List<Charakter>();
        public static List<Hex> enemyNeighbourTiles = new List<Hex>();
        public static List<Hex> friendNeighbourTiles = new List<Hex>();
        public static List<Hex> canBefriendNeighbourTiles = new List<Hex>();
        public static int enemyNeighbourCount;
        public static int canBefriendNeighbourTilesCount;
        public static int friendlyNeighbourCount;
        
        private static bool playerTurn;
        public static bool[] lvlObjectives;
        public static string[] lvlObjectiveText;

        public static int firsttimeCounter = 0;
        public static bool firsttime = true;

        internal static Camera Camera { get => _camera; set => _camera = value; }



        public static void Init(ContentManager Content, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            InitBoard();
            _graphicsDevice = graphicsDevice;
            visManager = new VisualisationManagerHexmap(_board.GetLength(0), _board.GetLength(1), Camera);
            //Fokus der Camera auf die Mitte der Karte setzen
            visManager.SetCameraToMiddleOfMap();


            //Sound-Button
            btSoundEinstellungen = new Button("", Game1.textureSoundButton, Game1.textureSoundButton, 0.1f, _graphicsDevice.Viewport.Width - 80, 20);
            SetParameterFromWindowScale();
            if (Game1.defaultValueSoundOn == false)
            {
                btSoundEinstellungen.TextureHover = Game1.textureSoundButtonOff;
                btSoundEinstellungen.TextureDefault = Game1.textureSoundButtonOff;
            }
            
            Player._prevMouseState = Mouse.GetState();
            Player._prevKeyState = Keyboard.GetState();
            playerTurn = true;

            Player.actionMenuFont = Content.Load<SpriteFont>("Fonts\\Jellee20");
            Player.actionMenu = new MoveMenu(Player.actionMenuFont,graphicsDevice, SimpleMenu.BlendDirection.LeftToRight);
            Player.levelUpMenu = new SkillUpMenu(Player.actionMenuFont, graphicsDevice, SimpleMenu.BlendDirection.None);
            Player.objectiveMenu = new LevelObjectiveMenu(Player.actionMenuFont, graphicsDevice, SimpleMenu.BlendDirection.TopToBottom);
            Player.charakterMenu = new CharakterMenu(Player.actionMenuFont, graphicsDevice);
            Player.escMenu = new ESCMenu(Player.actionMenuFont, graphicsDevice, SimpleMenu.BlendDirection.None);
            Player.infoIcon = new InformationIcon();

            _backroundMain = Content.Load<Texture2D>("MainMenu\\backround");
        }

        public static void InitBoard()
        {
            _board = LevelHandler.activeLevel.Board;
            playableCharacter = LevelHandler.activeLevel.PlayableCharacters;
            npcs = LevelHandler.activeLevel.NPCCharacters;
            lvlObjectives = LevelHandler.activeLevel.LevelObjective;
            lvlObjectiveText = LevelHandler.activeLevel.LevelObjectiveText;
            playerTurn = true;
        }

        public static void LoadContent(ContentManager content, GraphicsDeviceManager _graphics)
        {
            Camera = new Camera((float)_graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight);
        }
        public static void Update(GameTime time, GraphicsDevice graphicsDevice)
        {

            //Sound-Einstellungen
            if(btSoundEinstellungen.IsClicked())
            {
                CharakterAnimationManager.animationSound = !CharakterAnimationManager.animationSound;
                if(CharakterAnimationManager.animationSound)
                {
                    btSoundEinstellungen.TextureDefault = Game1.textureSoundButton;
                    btSoundEinstellungen.TextureHover = Game1.textureSoundButton;
                }
                else
                {
                    btSoundEinstellungen.TextureDefault = Game1.textureSoundButtonOff;
                    btSoundEinstellungen.TextureHover = Game1.textureSoundButtonOff;
                }
            }

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
                    else
                    {
                        if (!Player.infoIcon.CanMoveList.Contains(charakter.Name))
                        {
                            Player.infoIcon.CanMoveList.Add(charakter.Name);
                        }
                    }
                    if(charakter.Fähigkeitspunkte > 0)
                    {
                        if (!Player.infoIcon.HasSkillPoints.ContainsKey(charakter.Name))
                        {
                            Player.infoIcon.HasSkillPoints.Add(charakter.Name, charakter.Fähigkeitspunkte);
                        }
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
            if (SimpleMenu.CheckIfAnyMenuOpen(Player.objectiveMenu) == false)
            {
                visManager.Update(time);
            }
            CharakterAnimationManager._sm.Update(time);

            if(activeHex != null)
            {
                CharakterAnimationManager.ActiveHexExists = true;
            }
            else
            {
                CharakterAnimationManager.ActiveHexExists = false;
            }
            LevelHandler.UpdateLevel();
        }

        public static void DrawInGame(SpriteBatch spriteBatch,GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_backroundMain, new Rectangle(0, 0, _backroundMain.Width, _backroundMain.Height), Color.White);
            spriteBatch.Draw(_backroundMain, new Rectangle(_backroundMain.Width, 0, _backroundMain.Width, _backroundMain.Height), Color.White);
            spriteBatch.Draw(_backroundMain, new Rectangle(0, _backroundMain.Height, _backroundMain.Width, _backroundMain.Height), Color.White);
            spriteBatch.Draw(_backroundMain, new Rectangle(_backroundMain.Width, _backroundMain.Height, _backroundMain.Width, _backroundMain.Height), Color.White);
            spriteBatch.End();
            for (int i = 0; i < _board.GetLength(1); i++)           //sorgt dafür das jedes einzelne Tile in _board auf der Kamera abgebildet wird
            {
                for (int k = 0; k < _board.GetLength(0); k++)
                {
                    _board[k, i].Draw(Camera);
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

            if(Game1.GState == Game1.GameState.InGame)
            {
                Game1._spriteBatch.Begin();
                btSoundEinstellungen.Draw(Game1._spriteBatch, Game1.mainMenuFont);
                Game1._spriteBatch.End();
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
                        createBoard[i, k] = new Hex(new Vector3(i, 0, (k * 0.8665f)), RandomRotation(), new Point(i, k), hilf);

                    }
                    else
                    {
                        Tile hilf = new Tile(new Vector3(i + 0.5f, 0, (k * 0.8665f)), tilemap[i, k], Content);
                        createBoard[i, k] = new Hex(new Vector3(i + 0.5f, 0, (k * 0.8665f)), RandomRotation(), new Point(i, k), hilf);
                    }
                }
            }
            return createBoard;
        }

        public static int RandomRotation()
        {
            Random rnd = new Random();
            return Hex.possibleRotations[rnd.Next(0, 4)];
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

        public static void StartGlow() //setzt den gesamten Glow der Map zurück
        {
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int k = 0; k < _board.GetLength(1); k++)
                {
                    _board[i, k].Tile.Glow = new Vector3(0.2f, 0.2f, 0.2f);
                    _board[i, k].Tile.isglowing = true;
                }
            }
        }

        public static void CalculatePossibleMoves(int x, int y, float bewegung, Hex activeTile) //hebt alle möglichen Züge hervor und speichert diese in possibleMoves
        {
            ++firsttimeCounter;
            if(firsttime == true)
            {
                firsttime = false;
                StartGlow();
            }
            if (bewegung >= 0)
            {
                _board[x, y].Tile.Glow = new Vector3(1f, 1f, 1f);
                _board[x, y].Tile.Color = new Vector3(0.6f, 0.6f, 0.6f);
                _board[x, y].Tile.isglowing = false;


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
            --firsttimeCounter;
            if(firsttimeCounter == 0)
            {
                firsttime = true;
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

        public static void SetParameterFromWindowScale()
        {
            if (Game1._graphics.IsFullScreen == true)
            {

                //Sound Einstellungen
                btSoundEinstellungen.ButtonX = _graphicsDevice.Viewport.Width + 20;
                btSoundEinstellungen.ButtonY = 30;
                btSoundEinstellungen.Scale = 0.2f;
            }
            else
            {
                //Sound Einstellungen
                btSoundEinstellungen.ButtonX = _graphicsDevice.Viewport.Width - 80;
                btSoundEinstellungen.ButtonY = 20;
                btSoundEinstellungen.Scale = 0.1f;
            }
        }
    }
}
