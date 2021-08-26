using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise.Menu
{
    class GameOver
    {
        private static bool doReinitBoard = true;

        static Texture2D btnDefaultTexture;
        static Texture2D btnHoverTexture;
        static SpriteFont mainMenuFont;

        static Button btnMainMenu;
        static Button btnLoadGame;

        public static void Init(ContentManager content)
        {
            //Content for Button Back & Load
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
            mainMenuFont = content.Load<SpriteFont>("MainMenu\\MainMenuFont");

            btnLoadGame = new Button("Load Game", btnDefaultTexture, btnHoverTexture, 0.4f, 40, 20);
            btnMainMenu = new Button("Back", btnDefaultTexture, btnHoverTexture, 0.4f, 800, 20);
        }

        internal static void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            //One Time ReInit of the Level
            if (doReinitBoard)
            {
                LevelHandler.ReInitLevel();
                doReinitBoard = false;
            }

            if (btnMainMenu.IsClicked())
            {
                Game1.GState = Game1.GameState.MainMenu;
            }
        }

        internal static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(mainMenuFont, "Game Over", new Vector2(150, 150), Color.White);
            btnMainMenu.Draw(spriteBatch,mainMenuFont);
            btnLoadGame.Draw(spriteBatch, mainMenuFont);
            spriteBatch.End();
        }
    }
}
