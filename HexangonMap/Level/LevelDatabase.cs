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
        public readonly static int[,] W1L1tilemap = new int[,] { { 4, 1, 4, 4, 3, 1, 1, 1 }, 
                                                                 { 1, 4, 4, 3, 1, 4, 1, 1 }, 
                                                                 { 4, 3, 3, 1, 2, 1, 4, 1 }, 
                                                                 { 1, 3, 1, 2, 2, 2, 1, 1 }, 
                                                                 { 3, 3, 3, 3, 2, 1, 1, 1 }, 
                                                                 { 1, 1, 2, 1, 1, 1, 1, 1 }, 
                                                                 { 1, 1, 1, 1, 1, 1, 1, 1 }, 
                                                                 { 1, 1, 1, 1, 1, 1, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
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
        public readonly static int[,] W1L2tilemap = new int[,] { { 2, 2, 2, 2, 2, 2, 2, 2 }, 
                                                                 { 2, 2, 2, 1, 1, 2, 2, 2 }, 
                                                                 { 2, 2, 1, 1, 1, 1, 2, 2 }, 
                                                                 { 2, 1, 1, 4, 4, 1, 2, 2 }, 
                                                                 { 2, 1, 1, 4, 4, 1, 1, 2 }, 
                                                                 { 2, 2, 1, 1, 1, 1, 2, 2 }, 
                                                                 { 2, 2, 1, 1, 2, 2, 2, 2 }, 
                                                                 { 2, 2, 2, 2, 2, 2, 2, 2 } }; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W1L2playerStats = new int[,] { { 5, 0, 10, 5, 5, 5, 3, 3, 3, 5, 0, 5, 3 } };
        public readonly static int[,] W1L2npcStats = new int[,] { { 4, 2 }, { 4, 2 } };
        public readonly static int[,] W1L2npcPatroulPoints = new int[,] { };
        public readonly static string[] W1L2playerNames = new string[] { "Guu" };       //input Array für Namen
        public readonly static string[] W1L2npcNames = new string[] {};
        public readonly static bool[] W1L2canBefriended = new bool[] { };
        public readonly static int[,] W1L2playerPos = new int[,] { { 4, 2 } };   //input Array für Positionen
        public readonly static int[,] W1L2npcPos = new int[,] {};
        public readonly static string[] W1L2objectiveText = { "Besuche die Insel" };
        public static bool[] W1L2objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(3, 3), new Point(3, 4), new Point(4, 3), new Point(4, 4) })};
        #endregion
        
        #endregion

        #region Welt2
        #region Level1Data
        public readonly static int[,] W2L1tilemap = new int[,] { { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W2L1playerStats = new int[,] { { 5, 0, 10, 5, 5, 5, 3, 3, 3, 5, 0, 5, 3 } };
        public readonly static int[,] W2L1npcStats = new int[,] { { 4, 2 }, { 4, 2 } };
        public readonly static int[,] W2L1npcPatroulPoints = new int[,] { };
        public readonly static string[] W2L1playerNames = new string[] { "Guu" };       //input Array für Namen
        public readonly static string[] W2L1npcNames = new string[] { "Timmae", "Paul" };
        public readonly static bool[] W2L1canBefriended = new bool[] { false, true };
        public readonly static int[,] W2L1playerPos = new int[,] { { 0, 1 } };   //input Array für Positionen
        public readonly static int[,] W2L1npcPos = new int[,] { { 4, 4 }, { 4, 2 } };
        public readonly static string[] W2L1objectiveText = { "Gehe nach unten Links", "Besiege alle Gegner" };
        public static bool[] W2L1objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(7, 7) }), LevelObjectives.EliminateAllEnemys(HexMap.npcs) };
        #endregion
        
        #endregion

        
    }
}
