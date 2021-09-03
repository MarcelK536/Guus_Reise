using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Guus_Reise.Menu
{
    class GameOver
    {
        private static bool doReinitBoard = true;

        static Texture2D btnDefaultTexture;
        static Texture2D btnHoverTexture;
        static Texture2D _backround;
        static SpriteFont mainMenuFont;

        static Button btnMainMenu;
        static Button btnLoadGame;

        private static SoundEffect _soundOnButton;

        public static void Init(ContentManager content)
        {
            //Content for Button Back & Load
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
            mainMenuFont = content.Load<SpriteFont>("MainMenu\\MainMenuFont");

            btnLoadGame = new Button("Load Game", btnDefaultTexture, btnHoverTexture, 0.4f, 40, 20);
            btnMainMenu = new Button("Back", btnDefaultTexture, btnHoverTexture, 0.4f, 800, 20);

            _soundOnButton = content.Load<SoundEffect>("Sounds\\mixkit-positive-interface-click-1112");

            _backround = content.Load<Texture2D>("MainMenu\\backround");
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
                _soundOnButton.Play();
            }
        }

        internal static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_backround, new Rectangle(0, 0, HexMap._graphicsDevice.Viewport.Width, HexMap._graphicsDevice.Viewport.Height), Color.White);
           

            spriteBatch.DrawString(mainMenuFont, "Game Over", new Vector2(350, 250), Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            btnMainMenu.Draw(spriteBatch,mainMenuFont);
            btnLoadGame.Draw(spriteBatch, mainMenuFont);
            spriteBatch.End();
        }
    }
}
