using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static Guus_Reise.Game1;
using System.Threading;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using Microsoft.Xna.Framework.Input;


namespace Guus_Reise.Animation
{
    class MovementAnimationManager
    {
        public static MovementAnimation _currentMovementAnimation;
        public static Texture2D btnDefaultTexture;
        public static Texture2D btnHoverTexture;
        public static SpriteFont mainMenuFont;
        public static SpriteBatch _spriteBatch;
        public static Button skip;


        public static void Init(string type, Hex start, Hex target)
        {
            skip = new Button("", MovementAnimationManager.btnDefaultTexture, MovementAnimationManager.btnHoverTexture, 0.4f, 780, 470);
            if(Game1._graphics.IsFullScreen == true)
            {
                SetParameterFromWindowScale();
            }
            if (start != target)
            {
                _currentMovementAnimation = new MovementAnimation(type, start, target);
                Game1.GState = Game1.GameState.MovementAnimation;
            }
        }

        public static void InitNPCMovement(string type, List<Hex> oldHexes, List<Hex> newHexes, List<Charakter> npcs)
        {
            _currentMovementAnimation = new MovementAnimation(type, oldHexes, newHexes, npcs);
            Game1.GState = Game1.GameState.MovementAnimation;
        }


        public static void UdpateMovement(GameTime gametime)
        {
            _currentMovementAnimation.Update(gametime);

        }

        public static void DrawMovementAnimation()
        {
            _currentMovementAnimation.Draw();
            
        }

        public static void LoadTextures(ContentManager content, SpriteBatch spriteBatch)
        {
            btnDefaultTexture = content.Load<Texture2D>("Buttons\\ButtonSkip");
            btnHoverTexture = content.Load<Texture2D>("Buttons\\ButtonSkip");
            mainMenuFont = content.Load<SpriteFont>("MainMenu\\MainMenuFont");
            _spriteBatch = spriteBatch;
        }

        public static void SetParameterFromWindowScale()
        {
            if (Game1._graphics.IsFullScreen == true)
            {
                
                //Skip
                skip.ButtonX = skip.ButtonX + 870;
                skip.ButtonY = skip.ButtonY + 450;
                skip.Scale = skip.Scale + 0.1f;
            }
            else
            {
                //Skip
                skip.ButtonX = skip.ButtonX - 870;
                skip.ButtonY = skip.ButtonY - 450;
                skip.Scale = skip.Scale - 0.1f;
            }
        }
    }
}

