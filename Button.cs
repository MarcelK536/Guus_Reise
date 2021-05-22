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
    class Button
    {
        private int _buttonX, _buttonY;     //used for the Postion Vector
        private string _name;               //the Printed Name of the Button
        Texture2D _textureDefault;          //Default Texture of the Button
        Texture2D _textureHover;            //OPTIONAL Hover Texture to further show Hovering
        private Color _tint;                //Colors the Texture to Symbolise Hovering
        private float _scale;               //Adjusts the Size of the Button
        private Vector2 _scale2;
        AnimatedSprite _spriteAnimated;     // Animation für Animated Button
        private bool _isAnimated = false;   //for default a Button ist non-animated

        MouseState prevMouseState;


        #region GetterSetter

        public Color Tint
        {
            get => _tint;
            set => _tint = value;
        }

        public float Scale
        {
            get => _scale;
            set => _scale = value;
        }

        public Vector2 Scale2
        {
            get => _scale2;
            set => _scale2 = value;
        }

        public int ButtonX
        {
            get => _buttonX;
            set => _buttonX = value;
        }

        public int ButtonY
        {
            get => _buttonY;
            set => _buttonY = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public Texture2D TextureDefault
        {
            get => _textureDefault;
            set => _textureDefault = value;
        }

        public Texture2D TextureHover
        {
            get => _textureHover;
            set => _textureHover = value;
        }

        public bool IsAnimated
        {
            get => _isAnimated;
            set => _isAnimated = value;
        }
        #endregion

        #region Constructors
        // Creates Button with an extra Hover Texture
        public Button(string name, Texture2D textureDefault, Texture2D textureHover, float scale, int buttonX, int buttonY)
        {
            this.Name = name;
            this.TextureDefault = textureDefault;
            this.TextureHover = textureHover;
            this.Scale = scale;
            this.ButtonX = buttonX;
            this.ButtonY = buttonY;
            _tint = Color.Gray;
        }


        // Creates Button with only an Default Textrue
        public Button(string name, Texture2D textureDefault, float scale, int buttonX, int buttonY)
        {
            this.Name = name;
            this.TextureDefault = textureDefault;
            this.Scale = scale;
            this.ButtonX = buttonX;
            this.ButtonY = buttonY;
            _tint = Color.Gray;
        }

        public Button(string name, Texture2D textureDefault, float scale, Vector2 pos)
        {
            this.Name = name;
            this.TextureDefault = textureDefault;
            this.Scale = scale;
            this.ButtonX = (int) pos.X;
            this.ButtonY = (int) pos.Y;
            _tint = Color.Gray;
        }

        //Creates Animated-Button with only a Default Animation
        public Button(string name, Texture2D textureDefault, AnimatedSprite spriteAnimated, Vector2 scale2, float scale, int buttonX, int buttonY)
        {
            this.Name = name;
            this.TextureDefault = textureDefault;
            this.TextureHover = textureDefault;
            _spriteAnimated = spriteAnimated;
            this.Scale = scale;
            this.Scale2 = scale2;
            this.ButtonX = buttonX;
            this.ButtonY = buttonY;
            _tint = Color.Gray;
            this.IsAnimated = true;
        }
        #endregion

        public void MoveButton(int moveX, int moveY)
        {
            this.ButtonX += moveX;
            this.ButtonY += moveY;
        }

        // Tests if the given coordinates lies into the animatedSprite of the Button (in the Animated Button)
        public bool liesInto(Vector2 coordinates)
        {
                if (this._spriteAnimated.IsVisible == true)
                {
                    Vector2 pos = new Vector2();
                    pos = this.GetPos();
                    Vector2[] corners = new Vector2[10];
                    corners = this._spriteAnimated.GetCorners(pos, 0f, this.Scale2);
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

        // Tests if the given coordinates lies into the given texture (into Button with given texture)
        public bool liesInto(Vector2 coordinates, Texture2D texture)
        {
            if ( coordinates.X < this.ButtonX + texture.Width * this.Scale && coordinates.Y < this.ButtonY + texture.Height * this.Scale)
            {
                if(coordinates.X > this.ButtonX && coordinates.Y > this.ButtonY)
                {
                    return true;
                }
            }
            return false;
        }

        //Gets Text Position by Calculating the Vector using the Placement of the Button AND the details of the Texture
        public Vector2 GetTextPos(SpriteFont font)
        {
            Vector2 fontSize = font.MeasureString(this.Name);
            Vector2 center = this.GetTextureCenter();
            
            return new Vector2(this.ButtonX + center.X - fontSize.X/2, this.ButtonY + center.Y - fontSize.Y/2);

        }

        public Vector2 GetTextureCenter()
        {
            float centerX = (this.TextureDefault.Width * this.Scale) / 2;
            float centerY = (this.TextureDefault.Height * this.Scale) / 2;
            return new Vector2(centerX, centerY);
        }

        //Returns Position Vector of the Button used for Drawing
        public Vector2 GetPos()
        {
            return new Vector2(this.ButtonX, this.ButtonY);
        }

        //Returns Position Vector to Place Something Right of Button
        public Vector2 GetPosRightOf()
        {
            return new Vector2(this.ButtonX + this.TextureDefault.Width + 10, this.ButtonY);
        }

        //Returns Position Vector to Place Something below Button
        public Vector2 GetPosBelow()
        {
            return new Vector2(this.ButtonX, this.ButtonY + this.TextureDefault.Height + 10);
        }


        //Returns Boolean to Check the State of the Button
        public bool IsHovered()
        {
            Vector2 currentMouseState = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
            if (this.IsAnimated)
            {
                
                if(liesInto(currentMouseState))
                {
                    _tint = Color.White;
                    return true;
                }
            }
            else
            {
                if(liesInto(currentMouseState, this.TextureDefault))
                {
                    _tint = Color.White;
                    return true;
                }
                _tint = Color.Gray;
            }
            return false;
        }


        //Checks if the Button is Clicked
        public bool IsClicked()
        {
            MouseState mouseState = Mouse.GetState();
            if (this.IsHovered() && Mouse.GetState().LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                Thread.Sleep(100);
                return true;
            }
            prevMouseState = mouseState;
            return false;
        }

        //Draws the Button, Needs the .Begin and .End function in the Class to function
        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            if(this.IsAnimated == false) // no Animation
            {
                if (this.IsHovered() == true && this.TextureHover != null)
                {
                    spriteBatch.Draw(this.TextureHover, this.GetPos(), null, this.Tint, 0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(this.TextureDefault, this.GetPos(), null, this.Tint, 0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0f);
                }
                spriteBatch.DrawString(spriteFont, this.Name, this.GetTextPos(spriteFont), Color.Black);
            }
            else // Button with Animation
            {
                if (this.IsHovered() == true && this.TextureHover != null)
                {
                    spriteBatch.Draw(this._spriteAnimated, this.GetPos(),0, this.Scale2);
                }
                else
                {
                    spriteBatch.Draw(this._spriteAnimated, this.GetPos(), 0, this.Scale2);
                }
                Vector2 textPosition = new Vector2((this.GetTextPos(spriteFont).X)-10, (this.GetTextPos(spriteFont).Y) + 50);
                spriteBatch.DrawString(spriteFont, this.Name, textPosition, Color.White);
            }
            
        }

    }
}
