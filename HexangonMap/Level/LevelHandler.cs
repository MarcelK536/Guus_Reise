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

        public static ContentManager contentLevel;

        public static int currentWorld = 1;
        public static int currentLevel = 1;

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
                    activeLevel = new Level(LevelDatabase.W1L1playerNames, LevelDatabase.W1L1playerStats, LevelDatabase.W1L1playerPos, LevelDatabase.W1L1tilemap, LevelDatabase.W1L1objectiveText, LevelDatabase.W1L1objective, contentLevel);
                    activeLevel.AddNewCharacter(activeLevel.Board, LevelDatabase.W1L1npcNames, LevelDatabase.W1L1npcStats, LevelDatabase.W1L1npcPos);
                    return activeLevel;
                case (1, 2):
                    activeLevel = new Level(LevelDatabase.W1L2playerNames, LevelDatabase.W1L2playerStats, LevelDatabase.W1L2playerPos, LevelDatabase.W1L2tilemap, LevelDatabase.W1L2objectiveText, LevelDatabase.W1L2objective, contentLevel);
                    activeLevel.AddNewCharacter(activeLevel.Board, LevelDatabase.W1L2npcNames, LevelDatabase.W1L2npcStats, LevelDatabase.W1L2npcPos);
                    return activeLevel;
                case (2, 1):
                    activeLevel = new Level(LevelDatabase.W2L1playerNames, LevelDatabase.W2L1playerStats, LevelDatabase.W2L1playerPos, LevelDatabase.W2L1tilemap, LevelDatabase.W2L1objectiveText, LevelDatabase.W2L1objective, contentLevel);
                    activeLevel.AddNewCharacter(activeLevel.Board, LevelDatabase.W2L1npcNames, LevelDatabase.W2L1npcStats, LevelDatabase.W2L1npcPos);
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
        public void UpdateLevel()
        {
            UpdateObjective();
            CheckIfLevelCondtionMet();
        }

        public void CheckIfLevelCondtionMet()
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

        public void InitNewLevel()
        {
            UpdateLevelCounter();
            CopyCharacters(activeLevel.CharacterList);
            activeLevel = InitLevel();
        }

        public void UpdateLevelCounter()
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

        public void CopyCharacters(List<Charakter> toCopy)
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
