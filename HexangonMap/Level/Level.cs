using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class Level
    {
        String levelObjective;
        List<Charakter> characterList;
        Hex[,] board;

        public Level(List<Charakter> charakters, int[,] tilemap, string objective,ContentManager content)
        {
            levelObjective = objective;
            characterList = charakters;
            board = HexMap.CreateHexboard(tilemap, content);
        }

        public Level(int[,] tilemap, string objective, ContentManager content)
        {
            levelObjective = objective;
            board = HexMap.CreateHexboard(tilemap, content);
        }
    }
}
