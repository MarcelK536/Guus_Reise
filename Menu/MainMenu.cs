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
            btnPlay = new Button("Play", btnDefaultTexture, btnHoverTexture, 10, 10);
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
                spriteBatch.Draw(btnPlay.TextureHover, new Rectangle(btnPlay.ButtonX, btnPlay.ButtonY, btnPlay.TextureHover.Width, btnPlay.TextureHover.Height), btnPlay.Tint);
            }
            else
            {
                spriteBatch.Draw(btnPlay.TextureDefault, new Rectangle(btnPlay.ButtonX, btnPlay.ButtonY, btnPlay.TextureDefault.Width, btnPlay.TextureDefault.Height), btnPlay.Tint);
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
