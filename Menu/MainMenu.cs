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
        static Button btnCredits;
        static Button btnExit;
        public static void Init()
        {
            btnPlay = new Button("Start Game", btnDefaultTexture, btnHoverTexture, 0.5f, 10, 10);
            btnCredits = new Button("Credits", btnDefaultTexture, btnHoverTexture, 0.5f, 250, 10);
            btnExit = new Button("Exit", btnDefaultTexture, btnHoverTexture, 0.5f, 500, 10);
        }
        public static void LoadTexture(ContentManager content)
        {
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
            mainMenuFont = content.Load<SpriteFont>("MainMenu\\MainMenuFont");
        }
        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {   
            spriteBatch.Begin();
            btnPlay.Draw(spriteBatch, mainMenuFont);
            btnCredits.Draw(spriteBatch, mainMenuFont);
            btnExit.Draw(spriteBatch, mainMenuFont);
            spriteBatch.End();
        }

        public static void Update(GameTime gameTime)
        {
            if (btnPlay.IsClicked() == true)
            {
                GState = GameState.InGame;   
            }
            if (btnCredits.IsClicked() == true)
            {
                GState = GameState.Credits;
            }
            if (btnExit.IsClicked() == true)
            {
                GState = GameState.Exit;
            }
        }

    }
}
