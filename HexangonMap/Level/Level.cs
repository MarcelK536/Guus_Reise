using Guus_Reise.Animation;
using Guus_Reise.HexangonMap;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Guus_Reise
{
    class Level
    {
        string[] levelObjectiveText;
        bool[] levelObjective;
        List<Charakter> characterList; //All Characters
        List<Charakter> playableCharacters = new List<Charakter>();
        List<Charakter> npcCharacters = new List<Charakter>();
        Hex[,] board;

        internal string[] LevelObjectiveText 
        { 
            get => levelObjectiveText; 
            set => levelObjectiveText = value; 
        }
        internal bool[] LevelObjective
        {
            get => levelObjective;
            set => levelObjective = value;
        }

        internal List<Charakter> CharacterList
        {
            get => characterList; 
            set => characterList = value; 
        }

        internal List<Charakter> PlayableCharacters
        {
            get => playableCharacters;
            set => playableCharacters = value;
        }
        internal List<Charakter> NPCCharacters
        {
            get => npcCharacters;
            set => npcCharacters = value;
        }
        internal Hex[,] Board 
        { 
            get => board; 
            set => board = value; 
        }

        public Level(List<Charakter> charakters, int[,] tilemap, string[] objectiveText,bool[] objective,ContentManager content)  //Init Level with existing Characters
        {
            LevelObjective = objective;
            LevelObjectiveText = objectiveText;
            CharacterList = charakters;
            Board = HexMap.CreateHexboard(tilemap, content);
        }

        public Level(string[] charNames, int[] charLevel, int[,] charPos, int[,] tilemap, string[] objectiveText, bool[] objective, ContentManager content)  //Init Level with new Characters
        {
            LevelObjective = objective;
            LevelObjectiveText = objectiveText;
            Board = HexMap.CreateHexboard(tilemap, content);
            if (CharacterList == null)
            {
                CharacterList = CreateCharakters(Board, charNames, charLevel, charPos);
            }
            else
            {
                CharacterList.Union(CreateCharakters(Board, charNames, charLevel, charPos));    //current Characters are Merged with new Charakters Duplicates will be removed 
            }
        }

        public void AddNewCharacter(Hex[,] levelBoard, string[] charNames, int[] charLevel, int[,] charPos)
        {
            CharacterList.Union(CreateCharakters(levelBoard, charNames, charLevel, charPos));
        }

        public List<Charakter> CreateCharakters(Hex[,] levelBoard, string[] charNames, int[] charLevel, int[,] charPos)
        {
            List<Charakter> createdCharakters = new List<Charakter>();
            for (int i = 0; i < charNames.GetLength(0); i++)
            {
                Charakter currChar = new Charakter(charNames[i], charLevel[i], levelBoard[charPos[i, 0], charPos[i, 0]], CharakterAnimationManager.GetCharakterAnimation(charNames[i]));
                Hex currHex = levelBoard[charPos[i, 0], charPos[i, 1]];
                currHex.Charakter = currChar;
                levelBoard[charPos[i, 0], charPos[i, 1]].Charakter.LogicalBoardPosition = levelBoard[charPos[i, 0], charPos[i, 1]].LogicalPosition;
                if (levelBoard[charPos[i, 0], charPos[i, 1]].Charakter.IsNPC)
                {
                    levelBoard[charPos[i, 0], charPos[i, 1]].Charakter.CanMove = false;
                    NPCCharacters.Add(levelBoard[charPos[i, 0], charPos[i, 1]].Charakter);
                }
                else
                {
                    levelBoard[charPos[i, 0], charPos[i, 1]].Charakter.CanMove = true;
                    PlayableCharacters.Add(levelBoard[charPos[i, 0], charPos[i, 1]].Charakter);
                }

                createdCharakters.Add(currChar);
            }
            return createdCharakters;
        }
    }
}
