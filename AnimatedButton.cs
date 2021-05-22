using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;
using System.Diagnostics;
using System.Threading;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace Guus_Reise
{
    class AnimatedButton : Button
    {
        AnimatedSprite _spriteAnimated;
        new protected Vector2 _scale;

        public new Vector2 Scale
        {
            get => _scale;
            set => _scale = value;
        }

        // Constructor defines AnimatedButton with AnimatedSprite spriteAnimated, scale and at pos buttonX, buttonY
        public AnimatedButton(string name, Texture2D textureDefault, AnimatedSprite spriteAnimated, Vector2 scale, int buttonX, int buttonY)
        {
            this.Name = name;
            this.TextureDefault = textureDefault;
            this.TextureHover = textureDefault;
            _spriteAnimated = spriteAnimated;
            this.Scale = scale;
            this.ButtonX = buttonX;
            this.ButtonY = buttonY;
            _tint = Color.Gray;
            this.IsAnimated = true;
        }

        public bool liesInto(Vector2 coordinates) 
        {
            if (this._spriteAnimated.IsVisible == true)
            {
                Vector2 pos = new Vector2();
                pos = this.GetPos();
                Vector2[] corners = new Vector2[10];
                corners = this._spriteAnimated.GetCorners(pos, 0f, this.Scale);
                if (coordinates.X > corners[0].X && coordinates.Y > corners[0].Y)
                {
                    if (coordinates.X < corners[2].X && coordinates.Y < corners[2].Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Returns Boolean to Check the State of the Button
        public new bool IsHovered()
        {
            Vector2 currentMouseState = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
            if (liesInto(currentMouseState))
            {
                _tint = Color.White;
                return true;
            }
            return false;
        }

        //Checks if the Button is Clicked
        public new bool IsClicked()
        {
            MouseState mouseState = Mouse.GetState();
            if (this.IsHovered())
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {

                    Thread.Sleep(100);
                    return true;
                }
            }

            prevMouseState = mouseState;
            return false;
        }

        //Draws the Button, Needs the .Begin and .End function in the Class to function
        public new void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
                if (this.IsHovered() == true)
                {
                    spriteBatch.Draw(this._spriteAnimated, this.GetPos(), 0, this.Scale);
                }
                else
                {
                    spriteBatch.Draw(this._spriteAnimated, this.GetPos(), 0, this.Scale);
                }
                Vector2 textPosition = new Vector2((this.GetTextPos(spriteFont).X) - 10, (this.GetTextPos(spriteFont).Y) + 50);
                spriteBatch.DrawString(spriteFont, this.Name, textPosition, Color.White);
        }

    }
}
