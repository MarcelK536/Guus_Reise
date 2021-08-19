﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Guus_Reise
{
    class LevelHandler
    {
       static List<Charakter> _currentPlayableCharacters = new List<Charakter>();
       static List<Charakter> _currentNPCs = new List<Charakter>();

        public static ContentManager contentLevel;

        static int currentWorld = 1;
        static int currentLevel = 1;

        readonly static int maxWorld = 3;
        readonly static int maxLevel = 3;

        public static Level activeLevel;


        public static void InitContent(ContentManager content)
        {
            contentLevel = content;
            activeLevel = InitLevel();
        }

        /// <summary>
        /// Initizialized the Level. Needs to be done before Loading new Levels.
        /// </summary>

        public static Level InitLevel()
        {
            
            switch (currentWorld, currentLevel)
            {
                case (1, 1):
                    activeLevel = new Level(LevelDatabase.W1L1charNames, LevelDatabase.W1L1charLevel, LevelDatabase.W1L1charPos, LevelDatabase.W1L1tilemap, LevelDatabase.W1L1objectiveText, LevelDatabase.W1L1objective, contentLevel);
                    return activeLevel;
                case (1, 2):
                    activeLevel = new Level(LevelDatabase.W1L2charNames, LevelDatabase.W1L2charLevel, LevelDatabase.W1L2charPos, LevelDatabase.W1L2tilemap, LevelDatabase.W1L2objectiveText, LevelDatabase.W1L2objective, contentLevel);
                    return activeLevel;
                case (2, 1):
                    activeLevel = new Level(LevelDatabase.W2L1charNames, LevelDatabase.W2L1charLevel, LevelDatabase.W2L1charPos, LevelDatabase.W2L1tilemap, LevelDatabase.W2L1objectiveText, LevelDatabase.W2L1objective, contentLevel);
                    return activeLevel;
            }
            return activeLevel;
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
        public static void UpdateLevel()
        {
            UpdateObjective();
            CheckIfLevelCondtionMet();
        }

        public static void CheckIfLevelCondtionMet()
        {
            bool conditionsMet = true;
            foreach(Boolean condition in activeLevel.LevelObjective)
            {
                if(condition == false)
                {
                    conditionsMet = false;
                }
            }

            if(conditionsMet == true)
            {
                InitNewLevel();
            }
        }

        public static void InitNewLevel()
        {
            UpdateLevelCounter();
            CopyCharacters(activeLevel.CharacterList);
            activeLevel = InitLevel();
            HexMap.InitBoard();
        }

        public static void UpdateLevelCounter()
        {
            if(currentLevel+1 > maxLevel)
            {
                currentLevel = 1;
                if (currentWorld + 1 < maxWorld)
                {
                    currentWorld += 1;
                }
                else
                {
                    //End of the Game
                }
            }
            else
            {
                currentLevel += 1;
            }
        }

        public static void CopyCharacters(List<Charakter> toCopy)
        {
            foreach (Charakter c in toCopy)
            {
                if (c.IsNPC)
                {
                    _currentNPCs.Add(c.Clone());
                }
                else
                {
                    _currentPlayableCharacters.Add(c.Clone());
                }
            }
        }
    }
}
