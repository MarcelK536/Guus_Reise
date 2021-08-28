using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class InformationComponents
    {
        public static SpriteFont fantasma;
        public static SpriteFont inclitodo;
        public static SpriteFont inclitodo15;
        public static SpriteFont gemunuLibre;
        public static SpriteFont aika;
        public static SpriteFont aika12;
        public static SpriteFont aika20;
        public static SpriteFont inclitodo30;



        public static void Init(GraphicsDevice graphicsDevice, ContentManager content)
        {
            fantasma = content.Load<SpriteFont>("Fonts\\Fantasma");
            inclitodo = content.Load<SpriteFont>("Fonts\\Inclitodo");
            inclitodo15 = content.Load<SpriteFont>("Fonts\\Inclitodo15");
            gemunuLibre = content.Load<SpriteFont>("Fonts\\GemunuLibre");
            aika = content.Load<SpriteFont>("Fonts\\Aika");
            aika12 = content.Load<SpriteFont>("Fonts\\Aika12");
            aika20 = content.Load<SpriteFont>("Fonts\\Aika20");
            inclitodo30 = content.Load<SpriteFont>("Fonts\\Inclitodo30");
        }
    }
}
