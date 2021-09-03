using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise.Menu
{
    class YouWon
    {
        static SpriteFont screenFont;
        static SpriteFont mainMenuFont;
        static Button btnMainMenu;
        static Texture2D btnDefaultTexture;
        static Texture2D btnHoverTexture;
        static Texture2D _backround;
        static Texture2D _guu;

        public static void Init()
        {
            btnMainMenu = new Button("Main Menu", btnDefaultTexture, btnHoverTexture, 0.4f, 40, 20);
        }

        public static void LoadTexture(ContentManager content)
        {
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
            screenFont = content.Load<SpriteFont>("Fonts\\Jellee30");
            mainMenuFont = content.Load<SpriteFont>("MainMenu\\MainMenuFont");
            _backround = content.Load<Texture2D>("MainMenu\\backround");
            _guu = content.Load<Texture2D>("MainMenu\\Guu_Main");
        }
        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_backround, new Rectangle(0, 0, HexMap._graphicsDevice.Viewport.Width, HexMap._graphicsDevice.Viewport.Height), Color.White);
            spriteBatch.Draw(_guu, new Rectangle(250, 150, 450, 450), Color.White);

            spriteBatch.DrawString(screenFont, "Great! You sent Guu back to his home galaxy", new Vector2(75, 100), Color.Yellow);
            btnMainMenu.Draw(spriteBatch, mainMenuFont);
            spriteBatch.End();
        }
        public static void Update(GameTime gameTime)
        {
            if (btnMainMenu.IsClicked())
            {
                Game1.GState = Game1.GameState.MainMenu;
            }
        }
    }
}

