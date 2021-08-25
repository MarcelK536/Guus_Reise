using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Guus_Reise
{
    class TalkFightKI
    {
        public static void MakeGreedyMove()
        {
            Charakter boi = TalkFighthandler.turnBar.ReturnCurrentCharakter();
            double basedmg = TalkFighthandler.GetBaseDmg(boi, boi.Weapon);
            double bestdmg = 0;
            int player = 0;
            Skill name = boi.Skill[0];
            foreach(Skill s in boi.Skill)
            {
                for(int i=0; i < TalkFighthandler.playerTiles.Count; i++)
                {
                    if(bestdmg < (basedmg * s.MoveValue)*(20/20+TalkFighthandler.playerTiles[i].Charakter.CurrentFightStats[3]))
                    {
                        bestdmg = (basedmg * s.MoveValue) * (20/20+TalkFighthandler.playerTiles[i].Charakter.CurrentFightStats[3]);
                        player = i;
                        name = s;
                    }
                }
            }

            TalkFighthandler.playerTiles[player].Charakter.CurrentFightStats[0] -= (int)bestdmg;
            for (int i = 1; i < TalkFighthandler.turnBar.NextTurn.Count; i++)
            {
                TalkFighthandler.turnBar.NextTurn[i].Geschwindigkeit -= boi.Geschwindigkeit;
            }
            boi.CurrentFightStats[7] = (int)(name.Geschwindigkeit - Math.Pow(0.95, (boi.Geschwindigkeit - name.Geschwindigkeit))); 
        }
    }
}
