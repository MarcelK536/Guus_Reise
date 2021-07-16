using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Guus_Reise
{
    class FightKI
    {
        public static void MakeGreedyMove()
        {
            Charakter boi = Fighthandler.turnBar.ReturnCurrentCharakter();
            double basedmg = Fighthandler.GetBaseDmg(boi, boi.Weapon);
            double bestdmg = 0;
            int player = 0;
            Skill name = boi.Skills[0];
            foreach(Skill s in boi.Skills)
            {
                for(int i=0; i < Fighthandler.playerTiles.Count; i++)
                {
                    if(bestdmg < (basedmg * s.MoveValue)-Fighthandler.playerTiles[i].Charakter.CurrentFightStats[3])
                    {
                        bestdmg = (basedmg * s.MoveValue) - Fighthandler.playerTiles[i].Charakter.CurrentFightStats[3];
                        player = i;
                        name = s;
                    }
                }
            }

            Fighthandler.playerTiles[player].Charakter.CurrentFightStats[0] -= (int)bestdmg;
            boi.CurrentFightStats[7] = (int)(name.Geschwindigkeit - Math.Pow(0.95, (boi.Geschwindigkeit - name.Geschwindigkeit))); 
        }
    }
}
