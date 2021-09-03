using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class LevelObjectives
    {
        /// <summary>
        /// The Objective is for any playable Character to reach a specific Tile
        /// </summary>
        public static bool GoToHexAny (List<Charakter> who, List<Point> where)
        {
            foreach (Charakter c in who)
            {
                foreach (Point w in where)
                {
                    if (c.LogicalBoardPosition == w)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// The Objective is for a specific playable Character to reach a specific Tile needs to be in a list
        /// </summary>
        public static bool GoToHexSpecific (Charakter who, List<Point> where)
        {
            foreach (Point w in where)
            {
                if (who.LogicalBoardPosition == w)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// The Objective is to fight all enemys
        /// </summary>
        public static bool EliminateAllEnemys(List<Charakter> enemys)
        {
            if(enemys.Count == 0)
            {
                return true;
            }
            return false;
        }

        public static bool ObtainWeapon(List<Charakter> who, Weapon what)
        {
            foreach (Charakter c in who)
            {
                if (c.WeaponInv.Contains(what))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ObtainTeamMember(List<Charakter> team, Charakter who)
        {
            if (team.Contains(who))
            {
                return true;
            }
            return false;
        }

        public static bool SurviveRounds(int curRound, int reachRound)
        {
            if(curRound == reachRound)
            {
                return true;
            }
            return false;
        }
    }
}
