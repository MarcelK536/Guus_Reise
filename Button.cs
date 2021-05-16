using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;
using System.Diagnostics;
using System.Threading;

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

        MouseState prevMouseState;



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

        public void MoveButton(int moveX, int moveY)
        {
            this.ButtonX += moveX;
            this.ButtonY += moveY;
        }


        //Gets Text Position by Calculating the Vector using the Placement of the Button AND the details of the Texture
        public Vector2 GetTextPos()
        {
            return new Vector2(this.ButtonX+this.TextureDefault.Height*this.Scale*0.2f, this.ButtonY + (this.TextureDefault.Width*this.Scale)/2);
        }

        //Returns Position Vector of the Button used for Drawing
        public Vector2 GetPos()
        {
            return new Vector2(this.ButtonX, this.ButtonY);
        }

        //Returns Boolean to Check the State of the Button
        public bool IsHovered()
        {
            if (Mouse.GetState().Position.X < this.ButtonX + this.TextureDefault.Width*this.Scale &&
                    Mouse.GetState().Position.X > this.ButtonX &&
                    Mouse.GetState().Position.Y < this.ButtonY + this.TextureDefault.Height*this.Scale &&
                    Mouse.GetState().Position.Y > this.ButtonY)
            {
                _tint = Color.White;
                return true;
            }
            _tint = Color.Gray;
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
            if (this.IsHovered() == true && this.TextureHover != null)
            {
                spriteBatch.Draw(this.TextureHover, this.GetPos(), null, this.Tint, 0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(this.TextureDefault, this.GetPos(), null, this.Tint, 0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0f);
            }
            spriteBatch.DrawString(spriteFont, this.Name, this.GetTextPos(), Color.Black);
        }

    }
}
