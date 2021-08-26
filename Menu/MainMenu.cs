﻿using Microsoft.Xna.Framework;
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
        static Button btnControls;
        static Button btnCredits;
        static Button btnExit;

        static GraphicsDeviceManager _graphics;

        public static void Init(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            btnPlay = new Button("Start Game", btnDefaultTexture, btnHoverTexture, 0.5f, 75, graphics.PreferredBackBufferHeight - (int)(graphics.PreferredBackBufferHeight *0.2));
            btnControls = new Button("Controls", btnDefaultTexture, btnHoverTexture, 0.5f, 295, graphics.PreferredBackBufferHeight - (int)(graphics.PreferredBackBufferHeight * 0.2));
            btnCredits = new Button("Credits", btnDefaultTexture, btnHoverTexture, 0.5f, 515, graphics.PreferredBackBufferHeight - (int)(graphics.PreferredBackBufferHeight * 0.2));
            btnExit = new Button("Exit", btnDefaultTexture, btnHoverTexture, 0.5f, 735, graphics.PreferredBackBufferHeight - (int)(graphics.PreferredBackBufferHeight * 0.2));
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
            btnControls.Draw(spriteBatch, mainMenuFont);
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
            if(btnControls.IsClicked() == true)
            {
                GState = GameState.Controls;
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
                btnPlay.ButtonX = 425;
                btnControls.ButtonX = 725;
                btnCredits.ButtonX = 1025;
                btnExit.ButtonX = 1325;
                // Scale
                btnPlay.Scale = 0.65f;
            }
            else
            {
                btnPlay.ButtonY = _graphics.PreferredBackBufferHeight - (int)(_graphics.PreferredBackBufferHeight * 0.2);
                //X
                btnPlay.ButtonX = 75;
                btnControls.ButtonX = 295;
                btnCredits.ButtonX = 515;
                btnExit.ButtonX = 735;
                //Scale
                btnPlay.Scale = 0.5f;
                
            }
            btnControls.ButtonY = btnPlay.ButtonY;
            btnCredits.ButtonY = btnPlay.ButtonY;
            btnExit.ButtonY = btnPlay.ButtonY;
            btnControls.Scale = btnPlay.Scale;
            btnCredits.Scale = btnPlay.Scale;
            btnExit.Scale = btnPlay.Scale;
        }

    }
}
