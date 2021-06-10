using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Guus_Reise.Game1;
namespace Guus_Reise
{
    class Fighthandler
    {
        public static List<Hex> playerTiles = new List<Hex>();      //Der Initierende Spieler steht am Ende der Liste
        public static List<Hex> npcTiles = new List<Hex>();

        static int[,] fightMap = new int[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 }, { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 }, { 1, 1, 1 }}; //input Array der die Art der Tiles für die map generierung angibt
        public static Hex[,] _fightBoard;
        public static int[,] charPositionsPlayer = new int[,] { { 1, 0 }, { 0, 1 }, { 1, 2 }, { 2, 1 } };  //Positionen für Spieler Der Initierende Spieler befindet sich an der Letzten Position
        public static int[,] charPositionsEnemy = new int[,] { { 6, 0 }, { 6, 1 }, { 6, 2 }, { 4, 1 } };   //Positionen für Gegner

        public static int[,] originalPositionsPlayer = new int[,] { };
        public static int[,] originalPositionsEnemy = new int[,] { };


        public static FightMenu fightMenu;


        public static void Init(GraphicsDevice graphicsDevice, ContentManager content)
        {
            fightMenu = new FightMenu(Player.actionMenuFont, graphicsDevice, SimpleMenu.BlendDirection.None);
            Createboard(fightMap, content);
        }

        public static void InitPlayers()
        {  
            for(int i = 0; i < playerTiles.Count - 1; i++) 
            {
                originalPositionsPlayer[i, 0] = playerTiles[i].LogicalPosition.X;
                originalPositionsPlayer[i, 1] = playerTiles[i].LogicalPosition.Y;
            }
            for (int i = 0; i < npcTiles.Count - 1; i++)
            {
                originalPositionsEnemy[i, 0] = playerTiles[i].LogicalPosition.X;
                originalPositionsEnemy[i, 1] = playerTiles[i].LogicalPosition.Y;
            }

            for(int i = 0; i < playerTiles.Count - 1; i++)
            {
                if (i == playerTiles.Count - 1)
                {
                    _fightBoard[charPositionsPlayer[charPositionsPlayer.Length - 1, 0], charPositionsPlayer[charPositionsPlayer.Length - 1, 1]].Charakter = playerTiles[i].Charakter;
                }
                else
                {
                    _fightBoard[charPositionsPlayer[i, 0], charPositionsPlayer[i, 1]].Charakter = playerTiles[i].Charakter;
                }
            }
            /* 
             * ToDo: -  Kopiere die originalen Positionen in originalPositions
             *       -  Platziere die Spieler auf ihren Platz in der FightMap (charPositionsXXX) Arrays
             *       -  Lade die Texturen
             */
        }

        public static void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                GState = Game1.GameState.InGame;
                fightMenu.Active = false;
            }
            fightMenu.Active = true;
            fightMenu.Update();

        }

        public static void Createboard(int[,] tilemap, ContentManager Content)                                 //generiert die Map, jedes Tile wird einzeln erstell und im _board gespeichert
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
                            Tile hilf = new Tile(new Vector3(i, 0, (k * 0.8665f)), tilemap[i, k], Content);
                            _fightBoard[i, k] = new Hex(new Vector3(i, 0, (k * 0.8665f)), new Point(i, k), hilf);

                        }
                        else
                        {
                            Tile hilf = new Tile(new Vector3(i + 0.5f, 0, (k * 0.8665f)), tilemap[i, k], Content);
                            _fightBoard[i, k] = new Hex(new Vector3(i + 0.5f, 0, (k * 0.8665f)), new Point(i, k), hilf);
                        }
                    }
                }           
            }
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
           /*
            foreach (Hex tile in playerTiles)
            {
                tile.Draw(HexMap.Camera);
            }
           */
            foreach (Hex tile in npcTiles)
            {
                tile.Draw(HexMap.Camera);
            }


            for (int i = 0; i < _fightBoard.GetLength(0); i++)           //sorgt dafür das jedes einzelne Tile in _board auf der Kamera abgebildet wird
            {
                for (int k = 0; k < _fightBoard.GetLength(1); k++)
                {
                    if (_fightBoard[i, k] != null)
                    {
                        _fightBoard[i, k].Draw(HexMap.Camera);
                    }
                }
            }
        
            fightMenu.Draw(spriteBatch);
        }

       /* public static void CalculateMoves(List<Moves> player, List<Moves> npc)
        {

        }
       */
    }
}
