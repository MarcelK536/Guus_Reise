using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class LevelDatabase
    {
        static int currentWorld = 1;
        static int currentLevel = 1;
        #region Welt1
        #region Level1Data
        readonly static int[,] W1L1tilemap = new int[,] { { 1, 2, 1, 1, 1, 1, 3, 1 }, { 1, 1, 1, 3, 1, 2, 1, 1 }, { 1, 1, 2, 1, 3, 1, 3, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 4, 1, 1, 2, 1, 1 }, { 1, 1, 1, 4, 1, 1, 1, 1 }, { 1, 1, 1, 4, 1, 1, 1, 1 }, { 1, 2, 3, 2, 1, 4, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
        readonly static int[] W1L1charLevel = new int[] { 5, 4, 4 };
        readonly static string[] W1L1charNames = new string[] { "Guu", "Timmae", "Paul" };       //input Array für Namen
        readonly static int[,] W1L1charPos = new int[,] { { 0, 1 }, { 4, 4 }, { 4, 2 } };   //input Array für Positionen
        readonly static string[] W1L1objectiveText = { "Gehe nach unten Links", "Besiege alle Gegner" };
        static bool[] W1L1objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new Point(7,7)), LevelObjectives.EliminateAllEnemys(HexMap.npcs) }; 
        #endregion
        public static Level W1L1;
        #region Level2Data
        readonly static int[,] W1L2tilemap = new int[,] { { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
        readonly static int[] W1L2charLevel = new int[] { 5, 4, 4 };
        readonly static string[] W1L2charNames = new string[] { "Guu", "Timmae", "Paul" };       //input Array für Namen
        readonly static int[,] W1L2charPos = new int[,] { { 0, 1 }, { 4, 4 }, { 4, 2 } };   //input Array für Positionen
        readonly static string[] W1L2objectiveText = { "Gehe nach unten Links", "Besiege alle Gegner" };
        static bool[] W1L2objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new Point(7, 7)), LevelObjectives.EliminateAllEnemys(HexMap.npcs) };
        #endregion
        public static Level W1L2;
        #endregion

        #region Welt2
        #region Level1Data
        readonly static int[,] W2L1tilemap = new int[,] { { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
        readonly static int[] W2L1charLevel = new int[] { 5, 4, 4 };
        readonly static string[] W2L1charNames = new string[] { "Guu", "Timmae", "Paul" };       //input Array für Namen
        readonly static int[,] W2L1charPos = new int[,] { { 0, 1 }, { 4, 4 }, { 4, 2 } };   //input Array für Positionen
        readonly static string[] W2L1objectiveText = { "Gehe nach unten Links", "Besiege alle Gegner" };
        static bool[] W2L1objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new Point(7, 7)), LevelObjectives.EliminateAllEnemys(HexMap.npcs) };
        #endregion
        public static Level W2L1;
        #endregion

        /// <summary>
        /// Initizialized the Level. Needs to be done before Loading new Levels.
        /// </summary>

        public static Level InitLevel(ContentManager content) 
        {
            switch (currentWorld, currentLevel)
            {
                case (1, 1):
                    W1L1 = new Level(W1L1charNames, W1L1charLevel, W1L1charPos, W1L1tilemap, W1L1objectiveText, W1L1objective, content);
                    return W1L1;
                case (1, 2):
                    W1L2 = new Level(W1L2charNames, W1L2charLevel, W1L2charPos, W1L2tilemap, W1L2objectiveText, W1L2objective, content);
                    return W1L2;
                case (2, 1):
                    W2L1 = new Level(W2L1charNames, W2L1charLevel, W2L1charPos, W2L1tilemap, W2L1objectiveText, W2L1objective, content);
                    return W2L1;
            }
            return W1L1;
        }

        public static void UpdateObjective()
        {
            W1L1objective[0] = LevelObjectives.GoToHexAny(HexMap.playableCharacter, new Point(7, 7));
            W1L1objective[1] = LevelObjectives.EliminateAllEnemys(HexMap.npcs);
        }
    }
}
