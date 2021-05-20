using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using static Guus_Reise.Game1;
using System.Threading;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;

namespace Guus_Reise.Menu
{
    class PlanetMenu
    {
        static Texture2D btnDefaultTexture;
        static Texture2D btnHoverTexture;
        static Texture2D worldTexture;
        static Texture2D worldTexture2;
        static SpriteFont mainMenuFont;
        static AnimatedSprite[] planetButtonAnimations;
        static Vector2 worldposition;
        //static Button[] planetButtons;
        static Button planet;
        static Button planetTwo;
        static Button back;
        //static Button planetAnimated;
        static AnimatedSprite planet1;
        static AnimatedSprite planet2;

        private static SpriteBatch _spriteBatch;

        //static AnimatedSprite world;

        public static void Init()
        {
            string[] planetNames = { "World1" }; //"World2"
            planet = new Button("worl1", worldTexture, planet1, 0.6f,200, 250);
            planetTwo = new Button("world2", worldTexture2, planet2, 0.6f, 400, 250);
            back = new Button("Back", btnDefaultTexture, btnHoverTexture, 0.5f, 570,-40);
            int index = 0;
            //foreach(string planetName in planetNames)
            //{
            //    planetButtons[index] = new Button(planetName, planetButtonAnimations[index], 0.5f, 450 + index * 50, 250 + index * 50);
            //    index++;
            //}

        }
        public static void LoadTexture(ContentManager content, SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            string[] contentStrings = { "worldJson.json" }; // "World\\worldYellow.json"
            //Content for Button Back
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            worldTexture = content.Load<Texture2D>("World\\worldPicute");
            worldTexture2 = content.Load<Texture2D>("World\\worldYellowPicture");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
            mainMenuFont = content.Load<SpriteFont>("MainMenu\\MainMenuFont");
            int index = 0;
            SpriteSheet spritesheet;
            foreach (string contentString in contentStrings)
            {
                spritesheet = content.Load<SpriteSheet>("World\\"+contentString, new JsonContentLoader());
                planet1 = new AnimatedSprite(spritesheet);
                planet1.Play("world");
                worldposition = new Vector2(400, 250);

            }
            spritesheet = content.Load<SpriteSheet>("World\\" + "worldYellow.json", new JsonContentLoader());
            planet2 = new AnimatedSprite(spritesheet);
            planet2.Play("worldYellow");
            worldposition = new Vector2(400, 250);

        }
        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            //planet.Draw(spriteBatch, mainMenuFont);
            back.Draw(spriteBatch, mainMenuFont);
            planet.Draw(_spriteBatch, mainMenuFont);
            planetTwo.Draw(_spriteBatch, mainMenuFont);
            //_spriteBatch.Draw(planet1, worldposition);
            spriteBatch.End();
        }

        public static void Update(GameTime gameTime)
        {
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            planet1.Play("world");
            planet1.Update(deltaSeconds);
            planet2.Play("worldYellow");
            planet2.Update(deltaSeconds);

            //foreach (AnimatedSprite planet in planetButtonAnimations)
            //{
            //    planet.Play("world");
            //    planet.Update(deltaSeconds);

            //}
            if (planet.IsClicked() == true || planetTwo.IsClicked() == true)
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
