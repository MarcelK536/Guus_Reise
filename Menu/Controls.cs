using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Guus_Reise
{
    class Controls
    {
        static Button btnMainMenu;
        static Button btnStartGame;

        static Texture2D btnDefaultTexture;
        static Texture2D btnHoverTexture;
        static SpriteFont mainMenuFont;

        private static SoundEffect _soundOnButton;

        internal static void Init(ContentManager content)
        {
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
            mainMenuFont = content.Load<SpriteFont>("MainMenu\\MainMenuFont");
            _soundOnButton = content.Load<SoundEffect>("Sounds\\mixkit-positive-interface-click-1112");

            btnMainMenu = new Button("Hauptmenü", btnDefaultTexture, btnHoverTexture, 0.5f, 800, 20);
            btnStartGame = new Button("Spiel Starten", btnDefaultTexture, btnHoverTexture, 0.5f, 40, 20);
        }

        internal static void Update(GameTime gameTime)
        {
            if (btnMainMenu.IsClicked())
            {
                Game1.GState = Game1.GameState.MainMenu;
                _soundOnButton.Play();
            }
            if (btnStartGame.IsClicked())
            {
                Game1.GState = Game1.GameState.PlanetMenu;
                _soundOnButton.Play();

            }
        }

        internal static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(mainMenuFont, "Du spielst als Guu und möchtest von dem Planeten wegfliegen.", new Vector2(50, 100), Color.White);
            spriteBatch.DrawString(mainMenuFont, "Tastatursteuerung: H - Charaktermenü, G - Levelziele", new Vector2(50, 150), Color.White);
            spriteBatch.DrawString(mainMenuFont, "Maussteuerung: Bewegen und Linksklicken - Spielfeld auswählen, Rechtsklick - Spielfeld abwählen", new Vector2(50, 200), Color.White);
            btnMainMenu.Draw(spriteBatch, mainMenuFont);
            btnStartGame.Draw(spriteBatch, mainMenuFont);
            spriteBatch.End();
        }
    }
}
