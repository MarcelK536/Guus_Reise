using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise.Animation
{
    class MovementAnimationManager
    {
        static MovementAnimation _currentMovementAnimation;

        public static void Init(string type, Hex start, Hex target)
        {
            if(start != target)
            {
                _currentMovementAnimation = new MovementAnimation(type, start, target);
                Game1.GState = Game1.GameState.MovementAnimation;
            }
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

