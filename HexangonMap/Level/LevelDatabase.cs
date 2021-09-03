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
        public readonly static int[,] W1L1tilemap = new int[,] { { 6, 1, 5, 4, 3, 1, 1, 1 },
                                                                  { 1, 4, 4, 3, 1, 4, 1, 1 },
                                                                 { 4, 3, 3, 7, 2, 8, 4, 1 },
                                                                  { 1, 3, 1, 2, 2, 1, 1, 1 },
                                                                 { 3, 3, 3, 3, 2, 2, 1, 2 },
                                                                  { 1, 1, 2, 1, 1, 1, 1, 2 },
                                                                 { 1, 1, 1, 1, 1, 1, 1, 1 },
                                                                  { 1, 1, 1, 1, 2, 2, 2, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W1L1playerStats = new int[,] { { 5, 0, 25, 10, 10, 10, 10, 10, 10, 10, 5, 5, 5 } };
        public readonly static int[] W1L1npcStats = new int[] { 2 , 2, 2 };
        public readonly static int[,] W1L1npcPatroulPoints = new int[,] { { 1, 1 }, { 4, 1 }, { 4, 4 }, { 1, 4 } };
        public readonly static string[] W1L1playerNames = new string[] { "Guu"};       //input Array für Namen
        public readonly static string[] W1L1npcNames = new string[] {"Bully", "Paul","Bully" };
        public readonly static bool[] W1L1canBefriended = new bool[] { false, true, false };
        public readonly static int[,] W1L1playerPos = new int[,] { { 0, 1 } };   //input Array für Positionen
        public readonly static int[,] W1L1npcPos = new int[,] { { 5, 4 }, { 4, 2 },{ 0, 5 } };
        public readonly static string[] W1L1objectiveText = { "Go to the bottom right corner", "Fight every Enemy" };
        public static bool[] W1L1objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(7, 7) }), LevelObjectives.EliminateAllEnemys(HexMap.npcs) };
        #endregion

        #region Level2Data
        public readonly static int[,] W1L2tilemap = new int[,] { { 1, 2, 2, 2, 2, 2, 2, 2 },
                                                                  { 1, 5, 2, 1, 1, 2, 2, 2 },
                                                                 { 1, 5, 1, 1, 1, 5, 2, 2 },
                                                                  { 2, 1, 1, 5, 1, 1, 1, 2 },
                                                                 { 2, 1, 1, 5, 1, 1, 1, 2 },
                                                                  { 2, 2, 1, 1, 1, 5, 1, 2 },
                                                                 { 2, 2, 1, 1, 1, 1, 1, 2 },
                                                                  { 2, 1, 5, 1, 4, 4, 2, 2 },
                                                                 { 2, 2, 5, 1, 4, 4, 2, 2 },
                                                                 { 2, 2, 2, 2, 2, 2, 2, 2 }}; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W1L2playerStats = new int[,] { { 5, 0, 10, 5, 5, 5, 3, 3, 3, 5, 0, 5, 3 } };
        public readonly static int[] W1L2npcStats = new int[] { 2, 2, 2 };
        public readonly static int[,] W1L2npcPatroulPoints = new int[,] { };
        public readonly static string[] W1L2playerNames = new string[] { "Guu" };       //input Array für Namen
        public readonly static string[] W1L2npcNames = new string[] {"Heavyweight","Heavyweight","Preacher" };
        public readonly static bool[] W1L2canBefriended = new bool[] { false, false, false };
        public readonly static int[,] W1L2playerPos = new int[,] { { 1, 1 } };   //input Array für Positionen
        public readonly static int[,] W1L2npcPos = new int[,] { { 6, 2 },{ 3, 6 }, { 4, 4 } };
        public readonly static string[] W1L2objectiveText = { "Get to the Island" };
        public static bool[] W1L2objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(7, 5), new Point(7, 4), new Point(8, 4), new Point(8, 5) }) };
        #endregion

        #endregion

        #region Welt2
        #region Level1Data
        public readonly static int[,] W2L1tilemap = new int[,] { { 8, 8, 8, 8, 8, 3, 3, 3 },
                                                                  { 8, 4, 8, 7, 3, 7, 7, 3 },
                                                                 { 8, 8, 8, 7, 7, 3, 7, 3 },
                                                                  { 8, 3, 7, 7, 3, 8, 4, 8 },
                                                                 { 8, 7, 3, 7, 8, 8, 4, 8 },
                                                                  { 8, 7, 3, 7, 7, 8, 4, 8 },
                                                                 { 8, 4, 7, 7, 3, 7, 7, 8 },
                                                                  { 8, 7, 8, 7, 3, 3, 7, 8 },
                                                                 { 8, 4, 8, 8, 7, 3, 7, 8 },
                                                                  { 8, 8, 4, 7, 7, 7, 7, 8 },
                                                                 { 8, 8, 8, 8, 8, 8, 8, 8 }}; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W2L1playerStats = new int[,] { { 5, 0, 10, 5, 5, 5, 3, 3, 3, 5, 0, 5, 3 } };
        public readonly static int[] W2L1npcStats = new int[] { 1, 2, 2, 3, 1 };
        public readonly static int[,] W2L1npcPatroulPoints = new int[,] { };
        public readonly static string[] W2L1playerNames = new string[] { "Guu" };       //input Array für Namen
        public readonly static string[] W2L1npcNames = new string[] { "Bully", "Bully", "Heavyweight", "Heavyweight", "Politician" };
        public readonly static bool[] W2L1canBefriended = new bool[] { false, false, false, false, true };
        public readonly static int[,] W2L1playerPos = new int[,] { { 9, 2 } };   //input Array für Positionen
        public readonly static int[,] W2L1npcPos = new int[,] { { 3, 6 }, { 5, 4 },{ 3, 1 },{ 1, 7 },{ 5, 6 } };
        public readonly static string[] W2L1objectiveText = { "Make it to the bottom left corner.", };
        public static bool[] W2L1objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(6, 1), new Point(7, 1), new Point(7, 2) }) };
        #endregion

        #region Level2Data
        public readonly static int[,] W2L2tilemap = new int[,] { { 3, 7, 3, 7, 3, 3, 3, 3, 3, 7, 6, 2 },
                                                                  { 3, 3, 7, 3, 3, 3, 4, 3, 3, 7, 8, 6 },
                                                                 { 3, 3, 7, 3, 3, 8, 8, 7, 3, 3, 8, 2 },
                                                                  { 3, 3, 3, 3, 8, 8, 8, 8, 4, 3, 3, 3 },
                                                                 { 3, 3, 7, 3, 7, 8, 1, 5, 8, 3, 3, 3 },
                                                                  { 3, 7, 7, 3, 4, 8, 8, 8, 8, 3, 3, 6 },
                                                                 { 3, 3, 8, 8, 3, 3, 4, 7, 8, 8, 6, 2 },
                                                                  { 3, 3, 7, 8, 8, 7, 3, 3, 3, 2, 6, 6 } }; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W2L2playerStats = new int[,] { { 5, 0, 10, 5, 5, 5, 3, 3, 3, 5, 0, 5, 3 } };
        public readonly static int[] W2L2npcStats = new int[] { 2, 1, 1, 2, 1 };
        public readonly static int[,] W2L2npcPatroulPoints = new int[,] { };
        public readonly static string[] W2L2playerNames = new string[] { "Guu" };       //input Array für Namen
        public readonly static string[] W2L2npcNames = new string[] {"Assassin", "Heavyweight", "Heavyweight", "Bully", "old_man" };
        public readonly static bool[] W2L2canBefriended = new bool[] { false, false, false, false, true };
        public readonly static int[,] W2L2playerPos = new int[,] { { 7, 2 } };   //input Array für Positionen
        public readonly static int[,] W2L2npcPos = new int[,] { { 1, 6 }, { 3, 11 }, { 4, 11 }, { 3, 2 }, { 7, 8 } };
        public readonly static string[] W2L2objectiveText = { "Get through the Mountain Valley in the south" };
        public static bool[] W2L2objective = { LevelObjectives.GoToHexAny(HexMap.playableCharacter, new List<Point>() { new Point(11, 3), new Point(11, 4) }) };
        #endregion

        #endregion

        #region Welt3
        #region Level1Data
        public readonly static int[,] W3L1tilemap = new int[,] { { 2, 2, 2, 6, 6, 6, 6, 6, 6, 6, 6 },
                                                                  { 2, 6, 6, 6, 6, 6, 2, 6, 6, 6, 6 },
                                                                 { 2, 6, 6, 6, 6, 6, 2, 2, 6, 6, 6 },
                                                                  { 2, 6, 6, 6, 6, 6, 6, 6, 8, 6, 6 },
                                                                 { 6, 6, 6, 2, 6, 6, 6, 8, 8, 8, 6 },
                                                                  { 6, 6, 6, 2, 2, 6, 6, 8, 5, 8, 6 },
                                                                 { 6, 6, 6, 2, 2, 2, 6, 8, 5, 8, 6 },
                                                                  { 6, 6, 6, 2, 2, 2, 6, 8, 8, 8, 6 },
                                                                 { 2, 6, 6, 6, 2, 2, 6, 6, 6, 8, 6 },
                                                                  { 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 },
                                                                 { 2, 2, 6, 6, 6, 6, 6, 6, 6, 6, 8 },
                                                                  { 2, 2, 2, 2, 2, 6, 6, 6, 6, 8, 8 }}; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W3L1playerStats = new int[,] { { 5, 0, 15, 5, 5, 5, 3, 3, 3, 5, 0, 5, 3 } };
        public readonly static int[] W3L1npcStats = new int[] { 2, 2, 2, 3, 3, 3, 2, 2, 1 };
        public readonly static int[,] W3L1npcPatroulPoints = new int[,] { };
        public readonly static string[] W3L1playerNames = new string[] { "Guu" };       //input Array für Namen
        public readonly static string[] W3L1npcNames = new string[] {"Paul", "Bully", "Bully", "Daydreamer", "Daydreamer", "Assassin", "Preacher", "Preacher", "Politician" };
        public readonly static bool[] W3L1canBefriended = new bool[] { true, false, false, false, false, false, false, false, false};
        public readonly static int[,] W3L1playerPos = new int[,] { { 5, 0 } };   //input Array für Positionen
        public readonly static int[,] W3L1npcPos = new int[,] { { 3, 3 }, { 1, 1 }, { 9, 1 }, { 4, 6 }, { 8, 6 }, { 1, 7 }, { 0, 10 }, { 8, 10 }, { 5, 5 } };
        public static int W3L1curRound = 0;
        public readonly static int W3L1reachRound = 10;
        public readonly static string[] W3L1objectiveText = { "Survive "+ W3L1reachRound+ " rounds. " + (W3L1reachRound - W3L1curRound) + " remaining." };
        public static bool[] W3L1objective = { LevelObjectives.SurviveRounds(LevelDatabase.W3L1curRound, LevelDatabase.W3L1reachRound) || LevelObjectives.EliminateAllEnemys(HexMap.npcs)};
        #endregion

        #region Level2Data
        public readonly static int[,] W3L2tilemap = new int[,] { { 2, 2, 2, 6, 6, 6, 5, 8, 8, 5, 5, 5 },
                                                                  { 2, 6, 6, 6, 6, 6, 5, 5, 8, 4, 5, 5 },
                                                                 { 8, 4, 6, 8, 6, 6, 6, 6, 5, 5, 4, 5 },
                                                                  { 8, 4, 8, 8, 6, 6, 6, 6, 6, 5, 8, 5 },
                                                                 { 8, 4, 8, 8, 6, 6, 2, 2, 6, 5, 8, 8 },
                                                                  { 8, 4, 8, 8, 6, 6, 2, 6, 6, 5, 5, 8 },
                                                                 { 2, 8, 8, 8, 6, 6, 6, 6, 6, 6, 6, 2 },
                                                                  { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 } }; //input Array der die Art der Tiles für die map generierung angibt
        public readonly static int[,] W3L2playerStats = new int[,] { { 5, 0, 10, 5, 5, 5, 3, 3, 3, 5, 0, 5, 3 } };
        public readonly static int[] W3L2npcStats = new int[] { 2, 3, 3, 3, 1 };
        public readonly static int[,] W3L2npcPatroulPoints = new int[,] { };
        public readonly static string[] W3L2playerNames = new string[] { "Guu" };       //input Array für Namen
        public readonly static string[] W3L2npcNames = new string[] { "Timmae", "Heavyweight", "Daydreamer", "Bully", "old_man"};
        public readonly static bool[] W3L2canBefriended = new bool[] {true,false,false,false, true };
        public readonly static int[,] W3L2playerPos = new int[,] { { 5, 1 } };   //input Array für Positionen
        public readonly static int[,] W3L2npcPos = new int[,] { { 1, 1 }, { 5, 9 }, { 1, 9 }, { 4, 4 }, { 3, 6 }, { 2, 6 } };
        public readonly static string[] W3L2objectiveText = { "Befriend Timmae","Bring Timmae over the river" };
        public static bool[] W3L2objective = { false,false };
        #endregion

        #endregion
    }
}
