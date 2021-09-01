using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static Guus_Reise.Game1;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace Guus_Reise.Menu
{
    class Credits
    {
        static Texture2D btnDefaultTexture;
        static Texture2D btnHoverTexture;
        static SpriteFont mainMenuFont;

        static Texture2D referenzen;
        static Texture2D referenzen2;

        static List<Texture2D> referenzenList;


        public static int currentReferenzen;

        static Button btnBack;

        private static SoundEffect _soundOnButton;

        private static KeyboardState _prevKeyState;

        public static void Init()
        {
            btnBack = new Button("Back", btnDefaultTexture, btnHoverTexture, 0.3f, 10, 10);
        }

        public static void LoadTexture(ContentManager content)
        {
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            referenzen = content.Load<Texture2D>("Buttons\\Referenzen");
            referenzen2 = content.Load<Texture2D>("Buttons\\Referenzen2");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
            mainMenuFont = content.Load<SpriteFont>("MainMenu\\MainMenuFont");
            _soundOnButton = content.Load<SoundEffect>("Sounds\\mixkit-positive-interface-click-1112");
            referenzenList = new List<Texture2D> { referenzen, referenzen2 };
            currentReferenzen = 0;
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            btnBack.Draw(spriteBatch, mainMenuFont);

            //Referenzen-Image zeichnen   
            if (Game1._graphics.IsFullScreen == true)
            {
                spriteBatch.Draw(referenzenList[currentReferenzen], new Rectangle(0, 80, Fighthandler._graphicsDevice.Viewport.Width, Fighthandler._graphicsDevice.Viewport.Height - 80), Color.White);
            }
            else
            {
                spriteBatch.Draw(referenzenList[currentReferenzen], new Rectangle(0, 80, Fighthandler._graphicsDevice.Viewport.Width, Fighthandler._graphicsDevice.Viewport.Height - 80), Color.White);
            }

            spriteBatch.End();
        }

        public static void Update(GameTime gameTime)
        {
            if(btnBack.IsClicked() == true)
            {
                GState = GameState.MainMenu;
                _soundOnButton.Play();
            }

            // Test if an swipe in left or right direktion was initialized
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && _prevKeyState.IsKeyUp(Keys.Right))
            {
                ++currentReferenzen;
                if(currentReferenzen == referenzenList.Count)
                {
                    currentReferenzen = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && _prevKeyState.IsKeyUp(Keys.Left))
            {
                --currentReferenzen;
                if(currentReferenzen == -1)
                {
                    currentReferenzen = referenzenList.Count - 1;
                }
            }
            _prevKeyState = Keyboard.GetState();
        }
    }
}
