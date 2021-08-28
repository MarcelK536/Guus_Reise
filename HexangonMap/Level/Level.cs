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

        public Level(string[] charNames, bool[] canBefriended, int[,] charStats, int[,] charPos, int[,] tilemap, string[] objectiveText, bool[] objective, ContentManager content)  //Init Level with new Characters
        {
            LevelObjective = objective;
            LevelObjectiveText = objectiveText;
            Board = HexMap.CreateHexboard(tilemap, content);
            if (CharacterList == null)
            {
                CharacterList = CreateCharakters(Board, charNames, canBefriended, charStats, charPos);
            }
            else
            {
                CharacterList.Union(CreateCharakters(Board, charNames, canBefriended, charStats, charPos));    //current Characters are Merged with new Charakters Duplicates will be removed 
            }
        }

        public void AddNewCharacter(Hex[,] levelBoard, string[] charNames, bool[] canBefriended,  int[,] charStats, int[,] charPos)
        {
            List<Charakter> createdCharakters = new List<Charakter>();
            for (int i = 0; i < charNames.GetLength(0); i++)
            {
                Hex currHex = levelBoard[charPos[i, 0], charPos[i, 1]];
                Charakter currChar = new Charakter(charNames[i], charStats[i, 0], charStats[i, 1], currHex, CharakterAnimationManager.GetCharakterAnimation(charNames[i]));
                currHex.Charakter = currChar;
                currHex.Charakter.LogicalBoardPosition = currHex.LogicalPosition;
                createdCharakters.Add(currChar);
                if (currHex.Charakter.IsNPC)
                {
                    currHex.Charakter.CanMove = false;
                    currHex.Charakter.CanBefriended = canBefriended[i];
                    NPCCharacters.Add(currHex.Charakter);
                }
                else
                {
                    currHex.Charakter.CanMove = true;
                    currHex.Charakter.CanBefriended = false;
                    PlayableCharacters.Add(currHex.Charakter);
                }
            }
            CharacterList.Union(createdCharakters);
        }

        public List<Charakter> CreateCharakters(Hex[,] levelBoard, string[] charNames, bool[] canBefriended, int[,] charStats, int[,] charPos)
        {
            List<Charakter> createdCharakters = new List<Charakter>();
            for (int i = 0; i < charNames.GetLength(0); i++)
            {
                Hex currHex = levelBoard[charPos[i, 0], charPos[i, 1]];
                Charakter currChar = new Charakter(charNames[i], charStats[i, 0], charStats[i, 1], charStats[i, 2], charStats[i, 3], charStats[i, 4], charStats[i, 5], charStats[i, 6], charStats[i, 7], charStats[i, 8], charStats[i, 9], charStats[i, 10], charStats[i, 11], charStats[i, 12], currHex, CharakterAnimationManager.GetCharakterAnimation(charNames[i]));               
                currHex.Charakter = currChar;
                currHex.Charakter.LogicalBoardPosition = currHex.LogicalPosition;
                if (currHex.Charakter.IsNPC)
                {
                    currHex.Charakter.CanMove = false;
                    currHex.Charakter.CanBefriended = canBefriended[i];
                    NPCCharacters.Add(currHex.Charakter);
                }
                else
                {
                    currHex.Charakter.CanMove = true;
                    currHex.Charakter.CanBefriended = false;
                    PlayableCharacters.Add(currHex.Charakter);
                }

                createdCharakters.Add(currChar);
            }
            return createdCharakters;
        }
    }
}
