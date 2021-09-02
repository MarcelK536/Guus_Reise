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
            Skill name = boi.Skill[0];

            foreach (Skill s in boi.Skill)
            {
                for (int i = 0; i < Fighthandler.playerTiles.Count; i++)
                {
                    if (bestdmg < (basedmg * s.MoveValue) * (20.0 / (20 + Fighthandler.playerTiles[i].Charakter.CurrentFightStats[3])))
                    {
                        bestdmg = (basedmg * s.MoveValue) * (20.0 / (20 + Fighthandler.playerTiles[i].Charakter.CurrentFightStats[3]));
                        player = i;
                        name = s;
                    }
                }
            }

            

            Random rnd = new Random();
            int krit = rnd.Next(100);
            if(krit < boi.CurrentFightStats[8] + boi.Weapon.BaseKrit)
            {
                bestdmg = bestdmg * 1.5f;
            }

            Fighthandler.playerTiles[player].Charakter.CurrentFightStats[0] -= (int)bestdmg;
            for (int i = 1; i < Fighthandler.turnBar.NextTurn.Count; i++)
            {
                Fighthandler.turnBar.NextTurn[i].Geschwindigkeit -= boi.Geschwindigkeit;
            }
            boi.CurrentFightStats[7] = (int)(name.Geschwindigkeit - Math.Pow(0.95, (boi.Geschwindigkeit - name.Geschwindigkeit))); 
        }
    }
}
