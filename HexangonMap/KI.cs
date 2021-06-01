using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Guus_Reise
{
    class KI
    {
        public static void Update(GameTime time, GraphicsDevice graphicsDevice)
        {
            foreach(Charakter charakter in HexMap.npcs)
            {
                switch (charakter.KI)
                {
                    case 1:
                        charakter.CanMove = false;
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    default: charakter.CanMove = false;
                        break;
                }
            }
        }
    }
}
