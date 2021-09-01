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
            skip = new Button("", MovementAnimationManager.btnDefaultTexture, MovementAnimationManager.btnHoverTexture, 0.2f, 780, 470);
            SetParameterFromWindowScale();

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
            btnDefaultTexture = InformationComponents.texArrowRight;
            btnHoverTexture =InformationComponents.texArrowRight;
            mainMenuFont = content.Load<SpriteFont>("MainMenu\\MainMenuFont");
            _spriteBatch = spriteBatch;
        }

        public static void SetParameterFromWindowScale()
        {
            if (Game1._graphics.IsFullScreen == true)
            {
                
                //Skip
                skip.ButtonX = HexMap._graphicsDevice.Viewport.Width - 210;
                skip.ButtonY = HexMap._graphicsDevice.Viewport.Height - 250;
                skip.Scale = 0.4f;
            }
            else
            {
                //Skip
                skip.ButtonX = HexMap._graphicsDevice.Viewport.Width - 200;
                skip.ButtonY = HexMap._graphicsDevice.Viewport.Height - 200;
                skip.Scale = 0.3f;
            }
        }
    }
}

