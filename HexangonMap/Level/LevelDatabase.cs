using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class LevelDatabase
    {
        #region Level1Data
        readonly static int[,] L1tilemap = new int[,] { { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
        readonly static int[] L1charLevel = new int[] { 5, 4, 4 };
        readonly static string[] L1charNames = new string[] { "Guu", "Timmae", "Paul" };       //input Array für Namen
        readonly static int[,] L1charPos = new int[,] { { 0, 1 }, { 4, 4 }, { 4, 2 } };   //input Array für Positionen
        readonly static string[] L1objectiveText = { "Gehe nach unten Links", "Besiege alle Gegner" };
        static bool[] L1objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new Point(7,7)), LevelObjectives.EliminateAllEnemys(HexMap.npcs) }; 
        #endregion
        public static Level L1;

        #region Level2Data

        #endregion

        public static Level InitLevel(ContentManager content) 
        {
            L1 = new Level(L1charNames, L1charLevel, L1charPos, L1tilemap, L1objectiveText, L1objective, content);
            return L1;
        }

        public static void UpdateObjective()
        {
            L1objective[0] = LevelObjectives.GoToHexAny(HexMap.playableCharacter, new Point(7, 7));
            L1objective[1] = LevelObjectives.EliminateAllEnemys(HexMap.npcs);
        }
    }
}
