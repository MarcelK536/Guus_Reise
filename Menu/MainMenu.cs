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

        static GraphicsDeviceManager _graphics;

        public static void Init(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            btnPlay = new Button("Start Game", btnDefaultTexture, btnHoverTexture, 0.5f, 170, graphics.PreferredBackBufferHeight - (int)(graphics.PreferredBackBufferHeight *0.2));
            btnCredits = new Button("Credits", btnDefaultTexture, btnHoverTexture, 0.5f, 410, graphics.PreferredBackBufferHeight - (int)(graphics.PreferredBackBufferHeight * 0.2));
            btnExit = new Button("Exit", btnDefaultTexture, btnHoverTexture, 0.5f, 650, graphics.PreferredBackBufferHeight - (int)(graphics.PreferredBackBufferHeight * 0.2));
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
                GState = GameState.PlanetMenu;   
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

        public static void SetParametersFromWindowScale()
        {
            if(_graphics.IsFullScreen == true)
            {
                btnPlay.ButtonY = _graphics.PreferredBackBufferHeight - (int)(_graphics.PreferredBackBufferHeight * 0.1);
                //X
                btnPlay.ButtonX = 550;
                btnCredits.ButtonX = 850;
                btnExit.ButtonX = 1150;
                // Scale
                btnPlay.Scale = 0.65f;
            }
            else
            {
                btnPlay.ButtonY = _graphics.PreferredBackBufferHeight - (int)(_graphics.PreferredBackBufferHeight * 0.2);
                //X
                btnPlay.ButtonX = 170;
                btnCredits.ButtonX = 410;
                btnExit.ButtonX = 650;
                //Scale
                btnPlay.Scale = 0.5f;
                
            }
            btnCredits.ButtonY = btnPlay.ButtonY;
            btnExit.ButtonY = btnPlay.ButtonY;
            btnCredits.Scale = btnPlay.Scale;
            btnExit.Scale = btnPlay.Scale;
        }

    }
}
