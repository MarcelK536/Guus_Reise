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

        static Button planet;
        static Button back;

        private static SpriteBatch _spriteBatch;

        static AnimatedSprite world;
        static Vector2 worldposition;

        public static void Init()
        {
            //planet = new Button("Planet 1", btnDefaultTexture, btnHoverTexture, 0.5f, 250, 100);
            back = new Button("Back", btnDefaultTexture, btnHoverTexture, 0.5f, 570,-40);
            
        }
        public static void LoadTexture(ContentManager content, SpriteBatch spriteBatch)
        {
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\B1");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\B1_hover");
            mainMenuFont = content.Load<SpriteFont>("MainMenu\\MainMenuFont");
            _spriteBatch = spriteBatch;
            var spritesheet = content.Load<SpriteSheet>("World\\worldJson.json", new JsonContentLoader());
            var sprite = new AnimatedSprite(spritesheet);
            sprite.Play("world");
            world = sprite;
            worldposition = new Vector2(450, 250);

        }
        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            //planet.Draw(spriteBatch, mainMenuFont);
            back.Draw(spriteBatch, mainMenuFont);
            _spriteBatch.Draw(world, worldposition);
            spriteBatch.End();
        }

        public static void Update(GameTime gameTime)
        {
            world.Play("world");
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            world.Update(deltaSeconds);
            //if (planet.IsClicked() == true)
            //{
            //    GState = GameState.InGame;
            //}
            if (back.IsClicked() == true)
            {
                GState = GameState.MainMenu;
            }
        }
    }
}
