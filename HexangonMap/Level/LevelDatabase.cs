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
        public readonly static int[,] W1L1tilemap = new int[,] { { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[] W1L1charLevel = new int[] { 5, 4, 4 };
        public readonly static string[] W1L1charNames = new string[] { "Guu", "Timmae", "Paul" };       //input Array für Namen
        public readonly static int[,] W1L1charPos = new int[,] { { 0, 1 }, { 4, 4 }, { 4, 2 } };   //input Array für Positionen
        public readonly static string[] W1L1objectiveText = { "Gehe nach unten Links", "Besiege alle Gegner" };
        public static bool[] W1L1objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new Point(7,7)), LevelObjectives.EliminateAllEnemys(HexMap.npcs) }; 
        #endregion
        
        #region Level2Data
        public readonly static int[,] W1L2tilemap = new int[,] {  { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[] W1L2charLevel = new int[] { 5, 4, 4 };
        public readonly static string[] W1L2charNames = new string[] { "Guu", "Timmae", "Paul" };       //input Array für Namen
        public readonly static int[,] W1L2charPos = new int[,] { { 0, 1 }, { 4, 4 }, { 4, 2 } };   //input Array für Positionen
        public readonly static string[] W1L2objectiveText = { "Gehe nach unten Links", "Besiege alle Gegner" };
        public static bool[] W1L2objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new Point(7, 7)), LevelObjectives.EliminateAllEnemys(HexMap.npcs) };
        #endregion
        
        #endregion

        #region Welt2
        #region Level1Data
        public readonly static int[,] W2L1tilemap = new int[,] {{ 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[] W2L1charLevel = new int[] { 5, 4, 4 };
        public readonly static string[] W2L1charNames = new string[] { "Guu", "Timmae", "Paul" };       //input Array für Namen
        public readonly static int[,] W2L1charPos = new int[,] { { 0, 1 }, { 4, 4 }, { 4, 2 } };   //input Array für Positionen
        public readonly static string[] W2L1objectiveText = { "Gehe nach unten Links", "Besiege alle Gegner" };
        public static bool[] W2L1objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new Point(7, 7)), LevelObjectives.EliminateAllEnemys(HexMap.npcs) };
        #endregion
        
        #endregion

        
    }
}
