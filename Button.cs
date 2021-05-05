using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;

namespace Guus_Reise
{
    class Button
    {
        private int _buttonX, _buttonY;
        private string _name;
        Texture2D Texture;
        private Color _tint;

        public Color Tint
        {
            get => _tint;
            set => _tint = value;
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

        public Button(string name, Texture2D texture, int buttonX, int buttonY)
        {
            this.Name = name;
            this.Texture = texture;
            this.ButtonX = buttonX;
            this.ButtonY = buttonY;
            _tint = Color.Gray;
        }


        public bool enterButton()
        {
            if (Mouse.GetState().Position.X < this.ButtonX + Texture.Width &&
                    Mouse.GetState().Position.X > this.ButtonX &&
                    Mouse.GetState().Position.Y < this.ButtonY + Texture.Height &&
                    Mouse.GetState().Position.Y > this.ButtonY)
            {
                _tint = Color.White;
                return true;
            }
            _tint = Color.Gray;
            return false;
        }
    }
}
