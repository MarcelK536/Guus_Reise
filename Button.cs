using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;
using System.Threading;

namespace Guus_Reise
{
    class Button
    {
        private int _buttonX, _buttonY;     //used for the Postion Vector
        private string _name;               //the Printed Name of the Button
        Texture2D _textureDefault;          //Default Texture of the Button
        Texture2D _textureHover;            //OPTIONAL Hover Texture to further show Hovering
        protected Color _tint;                //Colors the Texture to Symbolise Hovering
        protected float _scale;               //Adjusts the Size of the Button
        protected bool _isAnimated = false;   //for default a Button ist non-animated
        protected bool _isFocused = false;  // For deafault a Button is not focused
        protected bool _isOnlyClickButton = true; // For default a Button is a only-Click-Button

        protected MouseState prevMouseState;


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

        public bool isFocused
        {
            get => _isFocused;
            set => _isFocused = value;
        }

        public bool isOnlyClickButton
        {
            get => _isOnlyClickButton;
            set => _isOnlyClickButton = value;
        }
        #endregion

        #region Constructors

        //Defailt Constructor - important for Child Class AnimatedButton
        public Button()
        {

        }
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
        #endregion


        //Creates Animated-Button with only a Default Animation
        
        #region positionFunctions
        public void MoveButton(Vector2 newPostition)
        {
            this.ButtonX = (int)newPostition.X;
            this.ButtonY = (int)newPostition.Y;
        }

        // Tests if the given coordinates lies into the given texture (into Button with given texture)
        public bool LiesInto(Vector2 coordinates, Texture2D texture)
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

        public Vector2 GetTextPosRightOf()
        {
            return new Vector2(this.ButtonX + this.TextureDefault.Width + 10, this.ButtonY + (this.TextureDefault.Height / 8));
        }

        //Returns Position Vector to Place Something below Button
        public Vector2 GetPosBelow()
        {
            return new Vector2(this.ButtonX, this.ButtonY + this.TextureDefault.Height + 10);
        }

        // return Boolean to Check, if Mouse ist pointed at the Button
        public bool IsPointedAt()
        {
            Vector2 currentMouseState = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
            if (LiesInto(currentMouseState, this.TextureDefault))
            {
                _tint = Color.White;
                return true;
            }
            return false;
        }
        #endregion

        //Returns Boolean to Check the State of the Button
        public bool IsHovered()
        {
            if(this.isOnlyClickButton == true)
            {
                if (IsPointedAt())
                {
                    _tint = Color.White;
                    return true;
                }
                _tint = Color.Gray;
            }
            else
            {
                if (isFocused == true)
                {
                    return true;
                }
            }

            return false;
        }

        //Checks if the Button is Clicked
        public bool IsClicked()
        {
            MouseState mouseState = Mouse.GetState();
            if (this.IsPointedAt())
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
        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
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

    }
}
