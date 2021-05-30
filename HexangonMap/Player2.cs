using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Guus_Reise
{
    class Player2
    {
        public static void Update(GameTime time, GraphicsDevice graphicsDevice)
        {
            foreach(Charakter charakter in HexMap.npcs)
            {
                charakter.CanMove = false;
            }
        }
    }
}
