using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Guus_Reise
{
    class LevelHandler
    {
       public static List<Charakter> _currentPlayableCharacters = new List<Charakter>();
       public static List<Charakter> _currentNPCs = new List<Charakter>();

        public static ContentManager contentLevel;

        public static int currentWorld = 1;
        public static int currentLevel = 1;

        readonly static int maxWorld = 3; 
        readonly static int maxLevel = 2; 

        public static Level activeLevel;

        public static void InitContent(ContentManager content)
        {
            contentLevel = content;
            activeLevel = InitLevel();
        }

        public static void FirstTimeCreation()
        {
            activeLevel = InitLevel();
            HexMap.InitBoard();
        }

        /// <summary>
        /// Initizialized the Level. Needs to be done before Loading new Levels.
        /// </summary>

        public static Level InitLevel()
        {
            switch (currentWorld, currentLevel)
            {
                case (1, 1):
                    activeLevel = new Level(LevelDatabase.W1L1playerNames, LevelDatabase.W1L1canBefriended, LevelDatabase.W1L1playerStats, LevelDatabase.W1L1playerPos, LevelDatabase.W1L1tilemap, LevelDatabase.W1L1objectiveText, LevelDatabase.W1L1objective, contentLevel);
                    activeLevel.AddNewCharacter(activeLevel.Board, LevelDatabase.W1L1npcNames, LevelDatabase.W1L1canBefriended, LevelDatabase.W1L1npcStats, LevelDatabase.W1L1npcPos);
                    return activeLevel;
                case (1, 2):
                    activeLevel = new Level(LevelDatabase.W1L2playerNames, LevelDatabase.W1L2canBefriended, LevelDatabase.W1L2playerStats, LevelDatabase.W1L2playerPos, LevelDatabase.W1L2tilemap, LevelDatabase.W1L2objectiveText, LevelDatabase.W1L2objective, contentLevel);
                    activeLevel.AddNewCharacter(activeLevel.Board, LevelDatabase.W1L2npcNames, LevelDatabase.W1L2canBefriended, LevelDatabase.W1L2npcStats, LevelDatabase.W1L2npcPos);
                    return activeLevel;
                case (2, 1):
                    activeLevel = new Level(LevelDatabase.W2L1playerNames, LevelDatabase.W1L2canBefriended, LevelDatabase.W2L1playerStats, LevelDatabase.W2L1playerPos, LevelDatabase.W2L1tilemap, LevelDatabase.W2L1objectiveText, LevelDatabase.W2L1objective, contentLevel);
                    activeLevel.AddNewCharacter(activeLevel.Board, LevelDatabase.W2L1npcNames, LevelDatabase.W2L1canBefriended, LevelDatabase.W2L1npcStats, LevelDatabase.W2L1npcPos);
                    return activeLevel;
                case (2, 2):
                    activeLevel = new Level(LevelDatabase.W2L2playerNames, LevelDatabase.W2L2canBefriended, LevelDatabase.W2L2playerStats, LevelDatabase.W2L2playerPos, LevelDatabase.W2L2tilemap, LevelDatabase.W2L2objectiveText, LevelDatabase.W2L2objective, contentLevel);
                    activeLevel.AddNewCharacter(activeLevel.Board, LevelDatabase.W2L2npcNames, LevelDatabase.W2L2canBefriended, LevelDatabase.W2L2npcStats, LevelDatabase.W2L2npcPos);
                    return activeLevel;
                case (3, 1):
                    activeLevel = new Level(LevelDatabase.W3L1playerNames, LevelDatabase.W3L1canBefriended, LevelDatabase.W3L1playerStats, LevelDatabase.W3L1playerPos, LevelDatabase.W3L1tilemap, LevelDatabase.W3L1objectiveText, LevelDatabase.W3L1objective, contentLevel);
                    activeLevel.AddNewCharacter(activeLevel.Board, LevelDatabase.W3L1npcNames, LevelDatabase.W3L1canBefriended, LevelDatabase.W3L1npcStats, LevelDatabase.W3L1npcPos);
                    return activeLevel;
                case (3, 2):
                    activeLevel = new Level(LevelDatabase.W3L2playerNames, LevelDatabase.W3L2canBefriended, LevelDatabase.W3L2playerStats, LevelDatabase.W3L2playerPos, LevelDatabase.W3L2tilemap, LevelDatabase.W3L2objectiveText, LevelDatabase.W3L2objective, contentLevel);
                    activeLevel.AddNewCharacter(activeLevel.Board, LevelDatabase.W3L2npcNames, LevelDatabase.W3L2canBefriended, LevelDatabase.W3L2npcStats, LevelDatabase.W3L2npcPos);
                    return activeLevel;
            }

            return activeLevel;
        }

        public static void UpdateObjective()
        {
            switch (currentWorld, currentLevel)
            {
                case (1, 1):
                    LevelDatabase.W1L1objective[0] = LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(7, 7) });
                    LevelDatabase.W1L1objective[1] = LevelObjectives.EliminateAllEnemys(HexMap.npcs);
                    break;
                case (1, 2):
                    LevelDatabase.W1L2objective[0] = LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(7, 5), new Point(7, 4), new Point(8, 4), new Point(8, 5) });
                    break;
                case (2, 1):
                    LevelDatabase.W2L1objective[0] = LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(0, 6), new Point(0, 7), new Point(1, 7) });
                    break;
                case (2, 2):
                    LevelDatabase.W2L2objective[0] = LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(3,11), new Point(4, 11) });
                    break;
                case (3, 1):
                    LevelDatabase.W3L1objective[0] = LevelObjectives.SurviveRounds(LevelDatabase.W3L1curRound,LevelDatabase.W3L1reachRound) || LevelObjectives.EliminateAllEnemys(HexMap.npcs);
                    break;
                case (3, 2):
                    var character = HexMap.playableCharacter.Find(c => c.Name == "characterName");
                    if (character != null)
                    {
                        LevelDatabase.W3L2objective[0] = LevelObjectives.GoToHexSpecific(character, new List<Point>() { new Point(11, 0), new Point(11, 1), new Point(11, 2), new Point(11, 3), new Point(10, 0), new Point(10, 1), new Point(9, 0) });
                    }
                    else
                    {
                        LevelDatabase.W3L2objective[0] = false;
                    }
                    LevelDatabase.W3L2objective[1] = HexMap.playableCharacter.Exists(c => c.Name == "Timmae");
                    if(!(HexMap.playableCharacter.Exists(c=>c.Name == "Timmae") || HexMap.npcs.Exists(c => c.Name == "Timmae")))
                    {
                        Game1.GState = Game1.GameState.GameOver;
                    }
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

            if(conditionsMet == true && Game1.GState != Game1.GameState.MovementAnimation)
            {
                _currentPlayableCharacters = activeLevel.PlayableCharacters;
                InitNewLevel();
            }
        }

        //Bereitet das neue Level vor und lädt es ein
        public static void InitNewLevel()
        {
            UpdateLevelCounter();
            CopyCharacters(activeLevel.CharacterList);
            activeLevel = InitLevel();
            HexMap.InitBoard();
            for(int i = 0; i < _currentPlayableCharacters.Count; i++)
            {
                if (activeLevel.CharacterList.Exists(e=>e.Name == _currentPlayableCharacters[i].Name))
                { 
                    int hilf = activeLevel.CharacterList.FindIndex(e=>e.Name == _currentPlayableCharacters[i].Name);
                    activeLevel.CharacterList[hilf].Level = _currentPlayableCharacters[i].Level;
                    activeLevel.CharacterList[hilf].XP = _currentPlayableCharacters[i].XP;
                    activeLevel.CharacterList[hilf].Widerstandskraft = _currentPlayableCharacters[i].Widerstandskraft;
                    activeLevel.CharacterList[hilf].Koerperkraft = _currentPlayableCharacters[i].Koerperkraft;
                    activeLevel.CharacterList[hilf].Beweglichkeit = _currentPlayableCharacters[i].Beweglichkeit;
                    activeLevel.CharacterList[hilf].Abwehr = _currentPlayableCharacters[i].Abwehr;
                    activeLevel.CharacterList[hilf].Wortgewandheit = _currentPlayableCharacters[i].Wortgewandheit;
                    activeLevel.CharacterList[hilf].Lautstaerke = _currentPlayableCharacters[i].Lautstaerke;
                    activeLevel.CharacterList[hilf].Ignoranz = _currentPlayableCharacters[i].Ignoranz;
                    activeLevel.CharacterList[hilf].Geschwindigkeit = _currentPlayableCharacters[i].Geschwindigkeit;
                    activeLevel.CharacterList[hilf].Glueck = _currentPlayableCharacters[i].Glueck;
                    activeLevel.CharacterList[hilf].Bewegungsreichweite = _currentPlayableCharacters[i].Bewegungsreichweite;
                    activeLevel.CharacterList[hilf].Fähigkeitspunkte = _currentPlayableCharacters[i].Fähigkeitspunkte;
                    activeLevel.CharacterList[hilf].Weapon = _currentPlayableCharacters[i].Weapon;
                    activeLevel.CharacterList[hilf].WeaponInv = _currentPlayableCharacters[i].WeaponInv;
                    activeLevel.CharacterList[hilf].Skill = _currentPlayableCharacters[i].Skill;
                    activeLevel.CharacterList[hilf].SkillInv = _currentPlayableCharacters[i].SkillInv;

                }
            }
        }

        //Resetet das aktuelle Level
        public static void ReInitLevel()
        {
            activeLevel = InitLevel();
            HexMap.InitBoard();
        }

        public static void UpdateLevelCounter()
        {
            if(currentLevel+1 > maxLevel)
            {
                currentLevel = 1;
                if (currentWorld + 1 <= maxWorld)
                {
                    currentWorld += 1;
                }
                else
                {
                    Game1.GState = Game1.GameState.YouWon;
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
