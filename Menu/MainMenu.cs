using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using static Guus_Reise.Game1;
using System.Diagnostics;

namespace Guus_Reise.Menu
{
    class MainMenu
    {
        static Texture2D btnDefaultTexture;
        static Texture2D btnHoverTexture;
        static SpriteFont mainMenuFont;

        static Button btnPlay;
        public static void Init()
        {
            btnPlay = new Button("Play", btnDefaultTexture, btnHoverTexture, 0.5f, 10, 10);
        }
        public static void LoadTexture(ContentManager content)
        {
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
            mainMenuFont = content.Load<SpriteFont>("MainMenuFont");
        }
        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {   
            spriteBatch.Begin();
            if (btnPlay.IsHovered() == true)
            {
                spriteBatch.Draw(btnPlay.TextureHover, btnPlay.GetPos(), null, btnPlay.Tint, 0f, Vector2.Zero, btnPlay.Scale, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(btnPlay.TextureDefault, btnPlay.GetPos(), null, btnPlay.Tint, 0f, Vector2.Zero, btnPlay.Scale, SpriteEffects.None, 0f);
            }
            spriteBatch.DrawString(mainMenuFont, "Start Game",btnPlay.GetTextPos(), Color.Black);
            spriteBatch.End();
        }

        public static void Update(GameTime gameTime)
        {
            if (btnPlay.IsClicked() == true)
            {
                GState = GameState.InGame;   
            }
        }

    }
}
