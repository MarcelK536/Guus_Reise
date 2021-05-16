using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using static Guus_Reise.Game1;
using System.Threading;

namespace Guus_Reise.Menu
{
    class PlanetMenu
    {
        static Texture2D btnDefaultTexture;
        static Texture2D btnHoverTexture;
        static SpriteFont mainMenuFont;

        static Button planet;
        static Button back;

        public static void Init()
        {
            planet = new Button("Planet 1", btnDefaultTexture, btnHoverTexture, 0.5f, 10, 10);
            back = new Button("Back", btnDefaultTexture, btnHoverTexture, 0.5f, 500, 10);
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
            planet.Draw(spriteBatch, mainMenuFont);
            back.Draw(spriteBatch, mainMenuFont);
            spriteBatch.End();
        }

        public static void Update(GameTime gameTime)
        {
            if (planet.IsClicked() == true)
            {
                GState = GameState.InGame;
            }
            if (back.IsClicked() == true)
            {
                GState = GameState.MainMenu;
            }
        }
    }
}
