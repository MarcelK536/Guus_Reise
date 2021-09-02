using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class LevelDatabase
    {
        #region Welt1
        #region Level1Data
        public readonly static int[,] W1L1tilemap = new int[,] { { 6, 1, 5, 4, 3, 1, 1, 1 }, 
                                                                  { 1, 4, 4, 3, 1, 4, 1, 1 }, 
                                                                 { 4, 3, 3, 7, 2, 8, 4, 1 }, 
                                                                  { 1, 3, 1, 2, 2, 1, 1, 1 }, 
                                                                 { 3, 3, 3, 3, 2, 2, 1, 2 }, 
                                                                  { 1, 1, 2, 1, 1, 1, 1, 2 }, 
                                                                 { 1, 1, 1, 1, 1, 1, 1, 1 }, 
                                                                  { 1, 1, 1, 1, 2, 2, 2, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W1L1playerStats = new int[,] { { 5, 0, 10, 5, 5, 5, 3, 3, 3, 5, 0, 5, 3} };
        public readonly static int[,] W1L1npcStats = new int[,] { { 4, 2 }, { 4, 2 } };
        public readonly static int[,] W1L1npcPatroulPoints = new int[,] { { 1, 1 }, { 4, 1 }, { 4, 4 }, { 1, 4 } };
        public readonly static string[] W1L1playerNames = new string[] { "Guu"};       //input Array für Namen
        public readonly static string[] W1L1npcNames = new string[] {"Timmae", "Paul" };
        public readonly static bool[] W1L1canBefriended = new bool[] { false, true };
        public readonly static int[,] W1L1playerPos = new int[,] { { 0, 1 } };   //input Array für Positionen
        public readonly static int[,] W1L1npcPos = new int[,] { { 5, 4 }, { 4, 2 } };
        public readonly static string[] W1L1objectiveText = { "Gehe nach unten Links", "Besiege alle Gegner" };
        public static bool[] W1L1objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(7, 7) }), LevelObjectives.EliminateAllEnemys(HexMap.npcs) }; 
        #endregion
        
        #region Level2Data
        public readonly static int[,] W1L2tilemap = new int[,] { { 1, 2, 2, 2, 2, 2, 2, 2 },
                                                                  { 1, 5, 2, 1, 1, 2, 2, 2 },
                                                                 { 1, 5, 1, 1, 1, 5, 2, 2 },
                                                                  { 2, 1, 1, 5, 1, 1, 1, 2 },
                                                                 { 2, 1, 1, 5, 1, 1, 1, 2 },
                                                                  { 2, 2, 1, 1, 1, 5, 1, 2 },
                                                                 { 2, 2, 1, 1, 1, 1, 1, 2 },
                                                                  { 2, 1, 5, 1, 4, 4, 2, 2 },
                                                                 { 2, 2, 5, 1, 4, 4, 2, 2 },
                                                                 { 2, 2, 2, 2, 2, 2, 2, 2 }}; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W1L2playerStats = new int[,] { { 5, 0, 10, 5, 5, 5, 3, 3, 3, 5, 0, 5, 3 } };
        public readonly static int[,] W1L2npcStats = new int[,] { { 4, 2 }, { 4, 2 } };
        public readonly static int[,] W1L2npcPatroulPoints = new int[,] { };
        public readonly static string[] W1L2playerNames = new string[] { "Guu" };       //input Array für Namen
        public readonly static string[] W1L2npcNames = new string[] {};
        public readonly static bool[] W1L2canBefriended = new bool[] { };
        public readonly static int[,] W1L2playerPos = new int[,] { { 1, 1 } };   //input Array für Positionen
        public readonly static int[,] W1L2npcPos = new int[,] {};
        public readonly static string[] W1L2objectiveText = { "Besuche die Insel" };
        public static bool[] W1L2objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(7, 5), new Point(7, 4), new Point(8, 4), new Point(8, 5) })};
        #endregion

        #endregion

        #region Welt2
        #region Level1Data
        public readonly static int[,] W2L1tilemap = new int[,] { { 8, 8, 8, 8, 8, 3, 3, 3 },
                                                                  { 8, 4, 8, 7, 3, 7, 7, 3 },
                                                                 { 8, 8, 8, 7, 7, 3, 7, 3 },
                                                                  { 8, 3, 7, 7, 3, 8, 4, 8 },
                                                                 { 8, 7, 3, 7, 8, 8, 4, 8 },
                                                                  { 8, 7, 3, 7, 7, 8, 4, 8 },
                                                                 { 8, 4, 7, 7, 3, 7, 7, 8 },
                                                                  { 8, 7, 8, 7, 3, 3, 7, 8 },
                                                                 { 8, 4, 8, 8, 7, 3, 7, 8 },
                                                                  { 8, 8, 4, 7, 7, 7, 7, 8 },
                                                                 { 8, 8, 8, 8, 8, 8, 8, 8 }}; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W2L1playerStats = new int[,] { { 5, 0, 10, 5, 5, 5, 3, 3, 3, 5, 0, 5, 3 } };
        public readonly static int[,] W2L1npcStats = new int[,] { { 4, 2 }, { 4, 2 } };
        public readonly static int[,] W2L1npcPatroulPoints = new int[,] { };
        public readonly static string[] W2L1playerNames = new string[] { "Guu" };       //input Array für Namen
        public readonly static string[] W2L1npcNames = new string[] { "Timmae", "Paul" };
        public readonly static bool[] W2L1canBefriended = new bool[] { false, true };
        public readonly static int[,] W2L1playerPos = new int[,] { { 9, 2 } };   //input Array für Positionen
        public readonly static int[,] W2L1npcPos = new int[,] { { 3, 6 }, { 5, 4 } };
        public readonly static string[] W2L1objectiveText = { "Gehe nach unten Links", "Besiege alle Gegner" };
        public static bool[] W2L1objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(7, 7) }), LevelObjectives.EliminateAllEnemys(HexMap.npcs) };
        #endregion

        #region Level2Data
        public readonly static int[,] W2L2tilemap = new int[,] { { 3, 7, 3, 7, 3, 3, 3, 3, 3, 7, 6, 2 },
                                                                  { 3, 3, 7, 3, 3, 3, 4, 3, 3, 7, 8, 6 },
                                                                 { 3, 3, 7, 3, 3, 8, 8, 7, 3, 3, 8, 2 },
                                                                  { 3, 3, 3, 3, 8, 8, 8, 8, 4, 3, 3, 3 },
                                                                 { 3, 3, 7, 3, 7, 8, 1, 5, 8, 3, 3, 3 },
                                                                  { 3, 7, 7, 3, 4, 8, 8, 8, 8, 3, 3, 6 },
                                                                 { 3, 3, 8, 8, 3, 3, 4, 7, 8, 8, 6, 2 },
                                                                  { 3, 3, 7, 8, 8, 7, 3, 3, 3, 2, 6, 6 } }; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W2L2playerStats = new int[,] { { 5, 0, 10, 5, 5, 5, 3, 3, 3, 5, 0, 5, 3 } };
        public readonly static int[,] W2L2npcStats = new int[,] { { 4, 2 }, { 4, 2 } };
        public readonly static int[,] W2L2npcPatroulPoints = new int[,] { };
        public readonly static string[] W2L2playerNames = new string[] { "Guu" };       //input Array für Namen
        public readonly static string[] W2L2npcNames = new string[] { };
        public readonly static bool[] W2L2canBefriended = new bool[] { };
        public readonly static int[,] W2L2playerPos = new int[,] { { 7, 2 } };   //input Array für Positionen
        public readonly static int[,] W2L2npcPos = new int[,] { };
        public readonly static string[] W2L2objectiveText = { "Besuche die Insel" };
        public static bool[] W2L2objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(3, 3), new Point(3, 4), new Point(4, 3), new Point(4, 4) }) };
        #endregion

        #endregion

        #region Welt3
        #region Level1Data
        public readonly static int[,] W3L1tilemap = new int[,] { { 2, 2, 2, 6, 6, 6, 6, 6, 6, 6, 6 },
                                                                  { 2, 6, 6, 6, 6, 6, 2, 6, 6, 6, 6 },
                                                                 { 2, 6, 6, 6, 6, 6, 2, 2, 6, 6, 6 },
                                                                  { 2, 6, 6, 6, 6, 6, 6, 6, 8, 8, 6 },
                                                                 { 6, 6, 6, 2, 6, 6, 6, 8, 8, 8, 6 },
                                                                  { 6, 6, 6, 2, 2, 6, 6, 8, 5, 8, 6 },
                                                                 { 6, 6, 6, 2, 2, 2, 6, 8, 5, 8, 6 },
                                                                  { 6, 6, 6, 2, 2, 2, 6, 8, 8, 8, 6 },
                                                                 { 2, 6, 6, 6, 2, 2, 6, 6, 6, 8, 6 },
                                                                  { 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 },
                                                                 { 2, 2, 6, 6, 6, 6, 6, 6, 6, 6, 8 },
                                                                  { 2, 2, 2, 2, 2, 6, 6, 6, 6, 8, 8 }}; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W3L1playerStats = new int[,] { { 5, 0, 10, 5, 5, 5, 3, 3, 3, 5, 0, 5, 3 } };
        public readonly static int[,] W3L1npcStats = new int[,] { { 4, 2 }, { 4, 2 } };
        public readonly static int[,] W3L1npcPatroulPoints = new int[,] { };
        public readonly static string[] W3L1playerNames = new string[] { "Guu" };       //input Array für Namen
        public readonly static string[] W3L1npcNames = new string[] { "Timmae", "Paul" };
        public readonly static bool[] W3L1canBefriended = new bool[] { false, true };
        public readonly static int[,] W3L1playerPos = new int[,] { { 5, 0 } };   //input Array für Positionen
        public readonly static int[,] W3L1npcPos = new int[,] { { 3, 6 }, { 5, 4 } };
        public readonly static string[] W3L1objectiveText = { "Gehe nach unten Links", "Besiege alle Gegner" };
        public static bool[] W3L1objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(7, 7) }), LevelObjectives.EliminateAllEnemys(HexMap.npcs) };
        #endregion

        #region Level2Data
        public readonly static int[,] W3L2tilemap = new int[,] { { 2, 2, 2, 6, 6, 6, 6, 8, 8, 5, 5, 5 },
                                                                  { 2, 6, 6, 6, 6, 6, 6, 6, 8, 4, 5, 5 },
                                                                 { 8, 4, 6, 4, 6, 6, 6, 6, 3, 3, 4, 5 },
                                                                  { 8, 4, 8, 4, 6, 6, 6, 6, 6, 3, 8, 5 },
                                                                 { 8, 4, 8, 4, 6, 6, 6, 6, 6, 6, 8, 8 },
                                                                  { 8, 4, 8, 4, 6, 6, 6, 6, 6, 6, 6, 8 },
                                                                 { 6, 8, 8, 8, 6, 6, 6, 6, 6, 6, 6, 6 },
                                                                  { 2, 2, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6 } }; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W3L2playerStats = new int[,] { { 5, 0, 10, 5, 5, 5, 3, 3, 3, 5, 0, 5, 3 } };
        public readonly static int[,] W3L2npcStats = new int[,] { { 4, 2 }, { 4, 2 } };
        public readonly static int[,] W3L2npcPatroulPoints = new int[,] { };
        public readonly static string[] W3L2playerNames = new string[] { "Guu" };       //input Array für Namen
        public readonly static string[] W3L2npcNames = new string[] { };
        public readonly static bool[] W3L2canBefriended = new bool[] { };
        public readonly static int[,] W3L2playerPos = new int[,] { { 5, 1 } };   //input Array für Positionen
        public readonly static int[,] W3L2npcPos = new int[,] { };
        public readonly static string[] W3L2objectiveText = { "Besuche die Insel" };
        public static bool[] W3L2objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(3, 3), new Point(3, 4), new Point(4, 3), new Point(4, 4) }) };
        #endregion

        #endregion
    }
}
