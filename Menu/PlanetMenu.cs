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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

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
        static Button btnBack;
        //static Button btnLoadGame;
        static List<string> listOfPlanets;
        static int indexOfSelectedPlanet;

        static GraphicsDeviceManager _graphics;

        static int index;
        static Vector2 worldScale = new Vector2(1.5f, 1.5f);

        private static SpriteBatch _spriteBatch;

        private static KeyboardState _prevKeyState;

        private static SoundEffect _soundOnButton;



        public static void Init(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            // here to insert Names of Planets
            List<string> planetNames = new List<string>{ "Planet 1", "Planet 2", "Planet 3"};
            indexOfSelectedPlanet = 0; //(planetNames.Count) / 2;

            // set Planet-Buttons
            planetButtons = new AnimatedButton[planetNames.Count];
            foreach(string planetName in planetNames)
            {
                index = planetNames.IndexOf(planetName);
                planetButtons[index] = new AnimatedButton(planetName,  worldTextures[index], planetButtonAnimations[index], worldScale, 200 + index * 300, 300, false);
            }
            planetButtons[indexOfSelectedPlanet].isFocused = true;
            bool test = planetButtons[indexOfSelectedPlanet].isFocused;
            // Set Button Back
           // btnLoadGame = new Button("Load Game", btnDefaultTexture, btnHoverTexture, 0.4f, 40, 20);
            btnBack = new Button("Back", btnDefaultTexture, btnHoverTexture, 0.4f, 800,20);

            // Set previous Keyboard State
            _prevKeyState = Keyboard.GetState();

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

            //Content for Button Back & Load
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
            mainMenuFont = content.Load<SpriteFont>("MainMenu\\MainMenuFont");

            _soundOnButton = content.Load<SoundEffect>("Sounds\\mixkit-positive-interface-click-1112");

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
        private static void SetMenuScale(int index)
        {
            Vector2 ScaleSelectedWorld = new Vector2(2.6f, 2.6f);
            if(indexOfSelectedPlanet == index)
            {
                planetButtons[index].Scale = ScaleSelectedWorld;
            }
            else
            {
                planetButtons[index].Scale = worldScale;
            }

        }


        // Function returns index of next or previous Object
        private static int GetIndexOfNextPreviousObject(int oldindex, string nextPrevious)
        {
            int newIndex = 0;
            switch(nextPrevious)
            {
                case "Next":
                    if (oldindex == planetButtons.Length - 1)
                    {
                        newIndex = 0;
                    }
                    else
                    {
                        newIndex = oldindex + 1;
                    }
                    break;
                case "Previous":
                    if (oldindex == 0)
                    {
                        newIndex = planetButtons.Length - 1;
                    }
                    else
                    {
                        newIndex = oldindex - 1;
                    }
                    break;
            }
            return newIndex;
        }

        // Function sets the new Position of the Objects after swiping left or right
        private static void SwitchPositionObjects(string lr)
        {
            int[] nextXPosition = new int[planetButtons.Length];
            switch (lr)
            {
                case "Left":
                    for (int i = 0; i < planetButtons.Length; i++)
                    {
                        nextXPosition[i] = planetButtons[GetIndexOfNextPreviousObject(i, "Next")].ButtonX;
                    }
                    
                    break;
                case "Right":
                    for (int i = 0; i < planetButtons.Length; i++)
                    {
                        nextXPosition[i] = planetButtons[GetIndexOfNextPreviousObject(i, "Previous")].ButtonX;
                    }
                    break;
            }
            for (int i = 0; i < planetButtons.Length; i++)
            {
                planetButtons[i].ButtonX = nextXPosition[i];
            }
        }

        // Function set Index of the Selected Object after swiping left or right
        private static void DefineSelectedObject(string lr)
        {
            planetButtons[indexOfSelectedPlanet].isFocused = false;
            switch (lr)
            {
                case "Left":
                    indexOfSelectedPlanet = GetIndexOfNextPreviousObject(indexOfSelectedPlanet, "Previous");
                    break;
                case "Right":
                    indexOfSelectedPlanet = GetIndexOfNextPreviousObject(indexOfSelectedPlanet, "Next");
                    break;
            }
            planetButtons[indexOfSelectedPlanet].isFocused = true;
            SwitchPositionObjects(lr);
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            // Draw Back-Button
            btnBack.Draw(spriteBatch, mainMenuFont);

            //btnLoadGame.Draw(spriteBatch, mainMenuFont);

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
            // Test if an swipe in left or right direktion was initialized
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && _prevKeyState.IsKeyUp(Keys.Right))
            {
                DefineSelectedObject("Right");
                
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && _prevKeyState.IsKeyUp(Keys.Left))
            {
                DefineSelectedObject("Left");
            }

            // Test for Click on Buttons
            foreach (AnimatedButton planet in planetButtons)
            {
                if (planet.IsClicked() == true && planet.isFocused || Keyboard.GetState().IsKeyDown(Keys.Enter) && planet.isFocused)
                {
                    switch (planet.Name)
                    {
                        case ("Planet 1"):
                            LevelHandler.currentWorld = 1;
                            break;
                        case ("Planet 2"):
                            LevelHandler.currentWorld = 2;
                            break;
                        case ("Planet 3"):
                            LevelHandler.currentWorld = 3;
                            break;
                    }
                    LevelHandler.FirstTimeCreation();
                    GState = GameState.InGame;
                    _soundOnButton.Play();
                }
            }
            if (btnBack.IsClicked() == true)
            {
                GState = GameState.MainMenu;
                _soundOnButton.Play();
            }
           // if(btnLoadGame.IsClicked() == true)
           // {
                //TODO LOAD GAME
               // _soundOnButton.Play();
            //}
            _prevKeyState = Keyboard.GetState();
        }

        public static void SetParametersFromWindowScale()
        {
            int index;
            if (_graphics.IsFullScreen == true)
            {
                foreach (string planet in listOfPlanets)
                {
                    index = listOfPlanets.IndexOf(planet);
                    planetButtons[index].ButtonX = planetButtons[index].ButtonX + 450;
                    planetButtons[index].ButtonY = 600;
                }
                btnBack.ButtonX = btnBack.ButtonX + 900;

            }
            else
            {
                foreach (string planet in listOfPlanets)
                {
                    index = listOfPlanets.IndexOf(planet);
                    planetButtons[index].ButtonX = planetButtons[index].ButtonX - 450;
                    planetButtons[index].ButtonY = 300;
                }
                btnBack.ButtonX = btnBack.ButtonX - 900;
            }
        }
    }
}
