using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class LevelDatabase
    {
        #region Level1
            int[,] L1tilemap = new int[,] { { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } }; //input Array der die Art der Tiles für die map generierung angibt
            int[] L1charakterlevel = new int[] { 5, 4, 4 };
            string[] L1names = new string[] { "Guu", "Timmae", "Paul" };       //input Array für Namen
            int[,] L1charPositions = new int[,] { { 0, 1 }, { 4, 4 }, { 4, 2 } };   //input Array für Positionen
        #endregion

        public void getLevelInfo()
        {

        }
    }
}
