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
        static Button btnMainMenu;
        static Texture2D btnDefaultTexture;
        static Texture2D btnHoverTexture;
        static Texture2D _backround;

        public static void Init()
        {
            btnMainMenu = new Button("Main Menu", btnDefaultTexture, btnHoverTexture,1f,50,50);
        }

        public static void LoadTexture(ContentManager content)
        {
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
            screenFont = content.Load<SpriteFont>("Fonts\\Jellee30");
            _backround = content.Load<Texture2D>("MainMenu\\backround");
        }
        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_backround, new Rectangle(0, 0, HexMap._graphicsDevice.Viewport.Width, HexMap._graphicsDevice.Viewport.Height), Color.White);

            spriteBatch.DrawString(screenFont, "You Won", btnMainMenu.GetPosBelow(), Color.Yellow, 0, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            btnMainMenu.Draw(spriteBatch, screenFont);
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

