using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Guus_Reise.Fight
{
    class FightKI
    {
        public void MakeGreedyMove()
        {
            Charakter boi = Fighthandler.turnBar.ReturnCurrentCharakter();
            double bestdmg = Fighthandler.GetBaseDmg(boi, boi.Weapon);
        }
    }
}
