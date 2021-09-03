using System;
using System.Collections.Generic;
using System.Text;
using Guus_Reise.HexangonMap;
using Guus_Reise.InGameMenu.MenuComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using static Guus_Reise.Game1;
using Microsoft.Xna.Framework.Audio;
namespace Guus_Reise
{
    class Fighthandler
    {
        //Original Tiles
        public static List<Hex> playerTiles = new List<Hex>();      //Der Initierende Spieler steht am Ende der Liste
        public static List<Hex> npcTiles = new List<Hex>();         //Liste der NPC
        public static List<Hex> newTeamMember = new List<Hex>();        //Liste von neuen Gruppenmitgliedern (Wortgewandtheit)
        public static List<Hex> waitList = new List<Hex>();         //Überlauf Liste falls im Kampf mehr als 4 Spieler / NPCs vorhanden



        static readonly int[,] fightMap = new int[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 }, { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 }, { 1, 1, 1 }}; //input Array der die Art der Tiles für die map generierung angibt
        public static Hex[,] _fightBoard;
        public static int[,] charPositionsPlayer = new int[,] { { 1, 0 }, { 0, 1 }, { 1, 2 }, { 2, 1 } };  //Positionen für Spieler Der Initierende Spieler befindet sich an der Letzten Position
        public static int[,] charPositionsEnemy = new int[,] { { 6, 0 }, { 6, 1 }, { 6, 2 }, { 4, 1 } };   //Positionen für Gegner  Der dem Spieler gegenüber stehenenden Gegner ist an der Letzten Position

        public static bool createdBoard = false;
        public static ContentManager contentFight;

        public static FightMenu fightMenu;
        public static bool initPlayers = false;
        public static FightTurnBar turnBar;

        public static VisualisationManagerHexmap visFightManager;

        public static int hoeheArena;
        public static int hoehePanel;

        private static Texture2D backgroundTexture;
        public static Texture2D texPanel;

        public static Texture2D textureEditbutton;
        public static Texture2D textureEditbuttonHover;

        public static Texture2D playerCharakterInfobox;
        public static Texture2D enemyCharakterInfobox;

        public static List<Weapon> weaponNormal = new List<Weapon> { Weapon.weapons[0], Weapon.weapons[1], Weapon.weapons[2], Weapon.weapons[3], Weapon.weapons[4], Weapon.weapons[5], Weapon.weapons[6] };
        public static List<Weapon> weaponTalk = new List<Weapon> { Weapon.weapons[7], Weapon.weapons[8], Weapon.weapons[9], Weapon.weapons[10], Weapon.weapons[11] };


        public static GraphicsDevice _graphicsDevice;

        public static int currentMenuStatus;
        public static List<string> menuStatusList = new List<string> { "Attack", "CharakterUebersicht" };

        public static bool _isInModeWeaponEdit = false;

        public static bool showFightResults = false;
        public static FightResults fightResults;

        static SoundEffect _fightSound;

        public static void Init(GraphicsDevice graphicsDevice, ContentManager content)
        {
            _graphicsDevice = graphicsDevice;
            hoeheArena = ((_graphicsDevice.Viewport.Height / 2) + _graphicsDevice.Viewport.Height / 2 / 2);
            hoehePanel = _graphicsDevice.Viewport.Height - hoeheArena;
            contentFight = content;

            texPanel = content.Load<Texture2D>("Fight\\FightMenuPanel2");
            fightMenu = new FightMenu(InformationComponents.fantasma, graphicsDevice, SimpleMenu.BlendDirection.None);

            backgroundTexture = content.Load<Texture2D>("Fight\\backgroundFight");
            playerCharakterInfobox = content.Load<Texture2D>("Buttons\\PlayercharakterSheet");
            enemyCharakterInfobox = content.Load<Texture2D>("Buttons\\EnemycharakterSheet");
            currentMenuStatus = 0;
            textureEditbutton = content.Load<Texture2D>("Buttons\\pencil");
            textureEditbuttonHover = content.Load<Texture2D>("Buttons\\pencilHover");
            _fightSound = content.Load<SoundEffect>("Sounds\\mixkit-knife-fast-hit-2184");

            fightResults = new FightResults(mainMenuFont, graphicsDevice, SimpleMenu.BlendDirection.None);
        }

        

        public static void InitPlayers(List<Hex> tiles, int[,] places)
        {
            if(tiles.Count > 4)
            {
                for(int i = 3; i > tiles.Count; i++)
                {
                    waitList.Add(tiles[i]);
                }
                tiles.RemoveRange(3, tiles.Count - 3);
            }
            for(int i = tiles.Count-1; i >= 0; i--) 
            {
                for(int j = places.GetLength(0)-1; j > 0; j--)
                {
                    if (_fightBoard[places[j, 0], places[j, 1]].Charakter == null)
                    {
                        tiles[i].FightPosition = _fightBoard[places[j, 0], places[j, 1]].FightPosition;
                        tiles[i].Charakter.LogicalFightPosition = new Point(places[j, 0], places[j, 1]);
                        tiles[i].LogicalFightPosition = new Point(places[j, 0], places[j, 1]);

                        tiles[i].LogicalPosition = tiles[i].LogicalFightPosition;
                        tiles[i].Charakter.LogicalPosition = tiles[i].Charakter.LogicalFightPosition;
                        tiles[i].Position = tiles[i].FightPosition;

                        _fightBoard[places[j, 0], places[j, 1]] = tiles[i];

                        tiles[i].Charakter.CurrentFightStats[0] = tiles[i].Charakter.Widerstandskraft;
                        tiles[i].Charakter.CurrentFightStats[1] = tiles[i].Charakter.Koerperkraft;
                        tiles[i].Charakter.CurrentFightStats[2] = tiles[i].Charakter.Beweglichkeit;
                        tiles[i].Charakter.CurrentFightStats[3] = tiles[i].Charakter.Abwehr;
                        tiles[i].Charakter.CurrentFightStats[4] = tiles[i].Charakter.Wortgewandheit;
                        tiles[i].Charakter.CurrentFightStats[5] = tiles[i].Charakter.Lautstaerke;
                        tiles[i].Charakter.CurrentFightStats[6] = tiles[i].Charakter.Ignoranz;
                        tiles[i].Charakter.CurrentFightStats[7] = tiles[i].Charakter.Geschwindigkeit;
                        tiles[i].Charakter.CurrentFightStats[8] = tiles[i].Charakter.Glueck;

                        break;
                    }
                }
            }

            //Hier werden auch die Waffenlisten der Spieler initiiert
            fightMenu.InitMenuBoxes(tiles);

            initPlayers = true;
        }

        public static void DeInitPlayers(List<Hex> tiles)
        {
            for(int i = tiles.Count-1; i >= 0; i--)
            {
                tiles[i].LogicalPosition = tiles[i].LogicalBoardPosition;
                if (tiles[i].Charakter != null)
                {
                    tiles[i].Charakter.LogicalPosition = tiles[i].Charakter.LogicalBoardPosition;
                }
                tiles[i].Position = tiles[i].BoardPosition;
            }            
        }

        public static void ExitFight()
        {
            DeInitPlayers(playerTiles);
            DeInitPlayers(npcTiles);
            DeInitPlayers(newTeamMember);

            initPlayers = false;
            fightMenu.Active = false;
            
            for (int i = _fightBoard.GetLength(0) - 1; i > 0; i--)
            {
                for (int j = _fightBoard.GetLength(1) - 1; j > 0; j--)
                {
                    if (_fightBoard[i, j] != null)
                    {
                        if (_fightBoard[i, j].Charakter != null)
                        {
                            Hex hilf = _fightBoard[i, j].Clone();
                            int hilfIndex = playerTiles.IndexOf(_fightBoard[i,j]);
                            bool isPlayerTile = true;
                            if (hilfIndex == -1)
                            {
                                hilfIndex = npcTiles.IndexOf(_fightBoard[i, j]);
                                isPlayerTile = false;
                            }
                            _fightBoard[i, j].Charakter = null;
                            if (isPlayerTile == true)
                            {
                                playerTiles[hilfIndex] = hilf;
                            }
                            else
                            {
                                npcTiles[hilfIndex] = hilf;
                            }
                        }
                    }
                }
            }

            for(int i = playerTiles.Count-1; i >= 0; i--)
            {
                HexMap._board[playerTiles[i].LogicalBoardPosition.X, playerTiles[i].LogicalBoardPosition.Y] = playerTiles[i];
            }
            for(int i = newTeamMember.Count-1; i >= 0; i--)
            {
                newTeamMember[i].LogicalBoardPosition = newTeamMember[i].Charakter.LogicalBoardPosition;
                HexMap._board[newTeamMember[i].LogicalBoardPosition.X, newTeamMember[i].LogicalBoardPosition.Y] = newTeamMember[i];
                playerTiles.Add(newTeamMember[i]);
                HexMap.playableCharacter.Add(newTeamMember[i].Charakter);
            }
            for(int i = npcTiles.Count-1; i >= 0; i--)
            {
                if (newTeamMember.Count > 0)
                {
                    Hex isNewFriend = newTeamMember.Find(e => e.LogicalPosition == npcTiles[i].LogicalPosition);
                    if (isNewFriend != null)
                    {
                        HexMap._board[npcTiles[i].LogicalBoardPosition.X, npcTiles[i].LogicalBoardPosition.Y] = npcTiles[i];
                    }
                }
                else
                {
                    HexMap._board[npcTiles[i].LogicalBoardPosition.X, npcTiles[i].LogicalBoardPosition.Y] = npcTiles[i];
                }
            }
            newTeamMember.Clear();
            Player.activeTile.IsActive = false;
            Player.activeTile = null;
            HexMap.activeHex = null;
            HexMap.NoGlow();
            Player.actionMenu.Active = false;
            createdBoard = false;

            showFightResults = true;
            
        }

        public static void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            
            if (createdBoard == false && showFightResults == false)
            {
                Createboard(fightMap);
                createdBoard = true;
            }

            if (showFightResults)
            {
                fightResults.Update();
            }
            else
            {

                if (initPlayers == false && showFightResults == false)
                {
                    InitPlayers(playerTiles, charPositionsPlayer);
                    InitPlayers(npcTiles, charPositionsEnemy);
                    turnBar = new FightTurnBar(graphicsDevice, playerTiles, npcTiles);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    Fighthandler.fightResults.gaveUp = true;
                    fightMenu.Active = false;
                    ExitFight();
                }
                if (turnBar.ReturnCurrentCharakter().IsNPC == false)
                {
                    fightMenu.Active = true;
                    fightMenu.Update(gameTime);
                    turnBar.ReSort();
                }
                else
                {
                    FightKI.MakeGreedyMove();
                    fightMenu.Active = false;
                    System.Threading.Thread.Sleep(500);
                    turnBar.ReSort();
                }

                turnBar.Update(graphicsDevice);
                visFightManager.Update(gameTime);
                RemoveDeadCharacters(npcTiles);
                RemoveDeadCharacters(playerTiles);

                

                WinFight();
                LoseFight();
                
            }

            
        }
        //Wenn Gamestate = Fight > remove. falls GameState = TalkFight > zur gruppe hinzufügen.
        public static void RemoveDeadCharacters(List<Hex> tiles)
        {
            foreach (Hex hexTiles in tiles)
            {
                if (hexTiles.Charakter != null)
                {
                    if (hexTiles.Charakter.CurrentFightStats[0] <= 0)
                    {
                        turnBar.RemoveCharakter(hexTiles.Charakter);
                        if (!hexTiles.Charakter.IsNPC)
                        {
                            fightResults.KilledFriends.Add(hexTiles.Charakter.Name);
                            HexMap.playableCharacter.Remove(hexTiles.Charakter);
                        }
                        if (hexTiles.Charakter.IsNPC)
                        {
                            if (Game1.GState == GameState.InFight)
                            {
                                fightResults.KilledEnemys.Add(hexTiles.Charakter.Name);
                                HexMap.npcs.Remove(hexTiles.Charakter);
                            }
                            if(Game1.GState == GameState.InTalkFight)
                            {
                                fightResults.NewFriends.Add(hexTiles.Charakter.Name);
                                hexTiles.Charakter.IsNPC = false;
                                hexTiles.Charakter.CanBefriended = false;
                                newTeamMember.Add(hexTiles.Clone());
                                HexMap.npcs.Remove(hexTiles.Charakter);
                            }
                        }

                        hexTiles.Charakter = null;
                    }
                }
            }
        }

        public static void WinFight()
        {
            bool noEnemysLeft = true;
            foreach (Hex h in npcTiles)
            {
                if(h.Charakter != null)
                {
                    noEnemysLeft = false;
                }
            }

            if(noEnemysLeft == true)
            {
                _fightSound.Play();
                ExitFight();
            }
        }

        public static void LoseFight()
        {
            bool guuDead = true;
            if (playerTiles.Exists(c => c.Charakter.Name == "Guu"))
            {
                foreach (Hex h in playerTiles)
                {
                    if (h.Charakter != null && h.Charakter.Name == "Guu")
                    {
                        guuDead = false;
                    }
                }

                if (guuDead == true)
                {
                    fightResults.gameOver = true;
                    showFightResults = true;
                }
            }
            else
            {
                if(playerTiles.Count == 0)
                {
                    showFightResults = true;
                }
            }
        }

        public static void Createboard(int[,] tilemap)                                 //generiert die Map, jedes Tile wird einzeln erstell und im _board gespeichert
        {
            _fightBoard = new Hex[tilemap.GetLength(0), tilemap.GetLength(1)];       //hier wird die größe von _board festgelegt, immer so groß wie der eingabe array -> ermöglicht dynamische Mapgröße

            for (int i = 0; i < tilemap.GetLength(0); i++)
            {
                for (int k = 0; k < tilemap.GetLength(1); k++)
                {
                    if (tilemap[i, k] != 0)
                    {
                        if (k % 2 == 0)                                             //unterscheidung da bei Hex Map jede zweite Reihe versetzt ist -> im else für z koordinate -0,5
                        {
                            Tile hilf = new Tile(new Vector3(i, 0, (k * 0.8665f)), tilemap[i, k], contentFight);
                            _fightBoard[i, k] = new Hex(new Vector3(i, 0, (k * 0.8665f)), new Point(i, k), hilf);

                        }
                        else
                        {
                            Tile hilf = new Tile(new Vector3(i + 0.5f, 0, (k * 0.8665f)), tilemap[i, k], contentFight);
                            _fightBoard[i, k] = new Hex(new Vector3(i + 0.5f, 0, (k * 0.8665f)), new Point(i, k), hilf);
                        }

                        _fightBoard[i, k].FightPosition = _fightBoard[i, k].BoardPosition;
                    }
                }           
            }
            visFightManager = new VisualisationManagerHexmap(_fightBoard.GetLength(0), _fightBoard.GetLength(1), HexMap.Camera);
            visFightManager.SetCameraToMiddleOfMap();
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            //Arena zeichnen
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, _graphicsDevice.Viewport.Width, hoeheArena), Color.White);
            spriteBatch.End();

            if (showFightResults)
            {

                //Arena zeichnen
                spriteBatch.Begin();
                spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), Color.White);
                spriteBatch.End();

                fightResults.Draw(spriteBatch);

                return;
            }

            if (initPlayers == true)
            {
                for (int i = 0; i < _fightBoard.GetLength(0); i++)           //sorgt dafür das jedes einzelne Tile in _board auf der Kamera abgebildet wird
                {
                    for (int k = 0; k < _fightBoard.GetLength(1); k++)
                    {
                        if (_fightBoard[i, k] != null)
                        {
                            _fightBoard[i, k].Draw(HexMap.Camera);
                            if (_fightBoard[i, k].Charakter != null)
                            {
                                _fightBoard[i, k].Charakter.Draw(HexMap.Camera);
                            }
                        }
                    }
                }


                if (turnBar.ReturnCurrentCharakter().IsNPC == false)
                {
                    fightMenu.Draw(spriteBatch);
                   
                }


            }
            
        }

        public static float GetBaseDmg(Charakter charakter, Weapon weapon)
        {
            float erg = weapon.BaseSchaden;
            erg += charakter.CurrentFightStats[1] * Weapon.StringToScale(weapon.ScalingKK);
            erg += charakter.CurrentFightStats[2] * Weapon.StringToScale(weapon.ScalingBW);
            erg += charakter.CurrentFightStats[4] * Weapon.StringToScale(weapon.ScalingWG);
            erg += charakter.CurrentFightStats[5] * Weapon.StringToScale(weapon.ScalingLS);
            return erg;
        }
    }
}
