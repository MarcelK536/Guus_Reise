using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Guus_Reise
{
    class LevelHandler
    {
        List<Charakter> _currentPlayableCharacters = new List<Charakter>();
        List<Charakter> _currentNPCs = new List<Charakter>();

        static int currentWorld = 1;
        static int currentLevel = 1;

        public static Level currLevel;

        /// <summary>
        /// Initizialized the Level. Needs to be done before Loading new Levels.
        /// </summary>

        public static Level InitLevel(ContentManager content)
        {
            
            switch (currentWorld, currentLevel)
            {
                case (1, 1):
                    currLevel = new Level(LevelDatabase.W1L1charNames, LevelDatabase.W1L1charLevel, LevelDatabase.W1L1charPos, LevelDatabase.W1L1tilemap, LevelDatabase.W1L1objectiveText, LevelDatabase.W1L1objective, content);
                    return currLevel;
                case (1, 2):
                    currLevel = new Level(LevelDatabase.W1L2charNames, LevelDatabase.W1L2charLevel, LevelDatabase.W1L2charPos, LevelDatabase.W1L2tilemap, LevelDatabase.W1L2objectiveText, LevelDatabase.W1L2objective, content);
                    return currLevel;
                case (2, 1):
                    currLevel = new Level(LevelDatabase.W2L1charNames, LevelDatabase.W2L1charLevel, LevelDatabase.W2L1charPos, LevelDatabase.W2L1tilemap, LevelDatabase.W2L1objectiveText, LevelDatabase.W2L1objective, content);
                    return currLevel;
            }
            return currLevel;
        }

        public static void UpdateObjective()
        {
            switch (currentWorld, currentLevel)
            {
                case (1, 1):
                    LevelDatabase.W1L1objective[0] = LevelObjectives.GoToHexAny(HexMap.playableCharacter, new Point(7, 7));
                    LevelDatabase.W1L1objective[1] = LevelObjectives.EliminateAllEnemys(HexMap.npcs);
                    break;
                case (1, 2):

                    break;
                case (2, 1):

                    break;
            }
        }
    }
}
