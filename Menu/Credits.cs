﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static Guus_Reise.Game1;
using Microsoft.Xna.Framework.Audio;

namespace Guus_Reise.Menu
{
    class Credits
    {
        static Texture2D btnDefaultTexture;
        static Texture2D btnHoverTexture;
        static SpriteFont mainMenuFont;

        static Button btnBack;

        private static SoundEffect _soundOnButton;

        public static void Init()
        {
            btnBack = new Button("Back", btnDefaultTexture, btnHoverTexture, 0.5f, 10, 10);
        }

        public static void LoadTexture(ContentManager content)
        {
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
            mainMenuFont = content.Load<SpriteFont>("MainMenu\\MainMenuFont");
            _soundOnButton = content.Load<SoundEffect>("Sounds\\mixkit-positive-interface-click-1112");
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            btnBack.Draw(spriteBatch, mainMenuFont);
            spriteBatch.End();
        }

        public static void Update(GameTime gameTime)
        {
            if(btnBack.IsClicked() == true)
            {
                GState = GameState.MainMenu;
                _soundOnButton.Play();
            }
        }
    }
}
