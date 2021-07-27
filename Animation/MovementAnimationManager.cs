using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise.Animation
{
    class MovementAnimationManager
    {
        static MovementAnimation _currentMovementAnimation;
        static List<Vector3> _npcPositions;

        public static void Init(string type, Hex start, Hex target)
        {
            if(start != target)
            {
                _currentMovementAnimation = new MovementAnimation(type, start, target);
                Game1.GState = Game1.GameState.MovementAnimation;
            }
        }

        public static void InitNPCMovement(string type, List<Hex> oldHexes, List<Hex> newHexes, List<Charakter> npcs)
        {
            _currentMovementAnimation = new MovementAnimation(type, oldHexes, newHexes, npcs);
            Game1.GState = Game1.GameState.MovementAnimation;
        }


        public static void UdpateMovement(GameTime gametime)
        {
            _currentMovementAnimation.Update(gametime);

        }

        public static void DrawMovementAnimation()
        {
            _currentMovementAnimation.Draw();

            
        }
    }
}

