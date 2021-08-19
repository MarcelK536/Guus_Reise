using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class LevelHandler
    {
        List<Charakter> _currentPlayableCharacters = new List<Charakter>();
        List<Charakter> _currentNPCs = new List<Charakter>();

        public void UpdateLevel()
        {
            LevelDatabase.UpdateObjective();
            CheckIfLevelCondtionMet();
        }

        public void CheckIfLevelCondtionMet()
        {
            bool conditionsMet = true;
            foreach(Boolean condition in LevelDatabase.activeLevel.LevelObjective)
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
            CopyCharacters(LevelDatabase.activeLevel.CharacterList);
            LevelDatabase.activeLevel = LevelDatabase.InitLevel();
        }

        public void UpdateLevelCounter()
        {
            if(LevelDatabase.currentLevel+1 > LevelDatabase.maxLevel)
            {
                LevelDatabase.currentLevel = 1;
                if (LevelDatabase.currentWorld + 1 < LevelDatabase.maxWorld)
                {
                    LevelDatabase.currentWorld += 1;
                }
                else
                {
                    //End of the Game
                }
            }
            else
            {
                LevelDatabase.currentLevel += 1;
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
