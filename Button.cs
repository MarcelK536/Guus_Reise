using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;
using System.Diagnostics;

namespace Guus_Reise
{
    class Button
    {
        private int _buttonX, _buttonY;
        private string _name;
        Texture2D _textureDefault;
        Texture2D _textureHover;
        private Color _tint;
        private float _scale;

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

        public Vector2 GetTextPos()
        {
            return new Vector2((this.ButtonX + this.TextureDefault.Height*this.Scale) / 2, (this.ButtonY + this.TextureDefault.Width*this.Scale) / 2);
        }

        public Vector2 GetPos()
        {
            return new Vector2(this.ButtonX, this.ButtonY);
        }
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

        public bool IsClicked()
        {
            return this.IsHovered() == true && Mouse.GetState().LeftButton == ButtonState.Pressed;
        }

    }
}
