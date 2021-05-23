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
        static SpriteFont mainMenuFont;

        static Texture2D[] worldTextures;
        static AnimatedSprite[] planetButtonAnimations;
        static AnimatedButton[] planetButtons;
        static Button back;
        static List<string> listOfPlanets;
        static int indexOfSelectedPlanet;


        static int index;
        static Vector2 worldScale = new Vector2(1.5f, 1.5f);

        private static SpriteBatch _spriteBatch;
        public static void Init()
        {
            // here to insert Names of Planets
            List<string> planetNames = new List<string>{ "Planet 1", "Planet 2", "Planet 3"};
            indexOfSelectedPlanet = (planetNames.Count) / 2;

            // set Planet-Buttons
            planetButtons = new AnimatedButton[planetNames.Count];
            foreach(string planetName in planetNames)
            {
                index = planetNames.IndexOf(planetName);
                planetButtons[index] = new AnimatedButton(planetName,  worldTextures[index], planetButtonAnimations[index], worldScale, 150 + index * 250, 250 );
            }

            // Set Button Back
            back = new Button("Back", btnDefaultTexture, btnHoverTexture, 0.3f, 650,20);

        }
        public static void LoadTexture(ContentManager content, SpriteBatch spriteBatch)
        {
            // here to insert the names of the Planetbuttons which have to be initialized
            listOfPlanets = new List<string> { "worldOne", "worldTwo", "worldThree"};
            
            _spriteBatch = spriteBatch;
            SpriteSheet spritesheet;

            // Arrays for Planet-Buttons
            planetButtonAnimations = new AnimatedSprite[listOfPlanets.Count];
            worldTextures = new Texture2D[listOfPlanets.Count];

            //Content for Button Back
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
            mainMenuFont = content.Load<SpriteFont>("MainMenu\\MainMenuFont");

            // Filling of Arrays
            foreach (string planetName in listOfPlanets)
            {
                index = listOfPlanets.IndexOf(planetName);

                // fill array of Animations
                spritesheet = content.Load<SpriteSheet>("World\\"+ planetName+".json", new JsonContentLoader());
                planetButtonAnimations[index] = new AnimatedSprite(spritesheet);
                planetButtonAnimations[index].Play("world");

                // fill array of Textures
                worldTextures[index] = content.Load<Texture2D>("World\\" + planetName);
            }
        }

        // Test of Object at index index, are the current selected element, and if true, set a lower scale
        public static void SetMenuScale(int index)
        {
            Vector2 ScaleSelectedWorld = new Vector2(2.3f, 2.3f);
            if(indexOfSelectedPlanet == index)
            {
                planetButtons[index].Scale = ScaleSelectedWorld;
            }
            else
            {
                planetButtons[index].Scale = worldScale;
            }

        }
        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            // Draw Back-Button
            back.Draw(spriteBatch, mainMenuFont);

            // Draw Planet-Buttons
            foreach(AnimatedButton planet in planetButtons)
            {
                planet.Draw(_spriteBatch, mainMenuFont);
            }
            spriteBatch.End();
        }

        public static void Update(GameTime gameTime)
        {
            // Play Animation
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (string planet in listOfPlanets)
            {
                index = listOfPlanets.IndexOf(planet);
                SetMenuScale(index);
                if (planetButtons[index].IsHovered() == true)
                {
                    planetButtonAnimations[index].Play("world");
                }
                else
                {
                    planetButtonAnimations[index].Play("noMovement");
                }

                planetButtonAnimations[index].Update(deltaSeconds);

            }
            // Test for Click on Buttons
            foreach (AnimatedButton planet in planetButtons)
            {
                if (planet.IsClicked() == true)
                {
                    GState = GameState.InGame;
                }
            }
            if (back.IsClicked() == true)
            {
                GState = GameState.MainMenu;
            }
        }
    }
}
