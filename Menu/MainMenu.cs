using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise.Menu
{
    class MainMenu
    {
        static Texture2D btnDefaultTexture;
        static Texture2D btnHoverTexture;
        static Vector2 btnPosition = new Vector2(100,100);

        Button btnDefault = new Button("Play", btnDefaultTexture, 100, 100);
        Button btnHover = new Button("Play", btnHoverTexture, 100, 100);

        public static void LoadTexture(ContentManager content)
        {
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
        }
        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            btnDefault.texture;
        }

        public static void Update(GameTime gameTime)
        {

        }

    }
}
