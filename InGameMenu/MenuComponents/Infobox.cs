using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Color = Microsoft.Xna.Framework.Color;

namespace Guus_Reise.InGameMenu.MenuComponents
{
    class Infobox
    {

        public int _infoboxX, _infoboxY;     //used for the Postion Vector
        protected string _name;               //the Printed Name of the Button
        protected Texture2D _texBackground;          //Default Texture of the Button
        //protected Texture2D _textureHover;            //OPTIONAL Hover Texture to further show Hovering

        public SpriteFont _fontUeberschriften;
        public SpriteFont _fontText;
        public SpriteFont _fontTitel;

        protected float _scale;               //Adjusts the Size of the Button
        protected bool _isAnimated = false;   //for default a Button ist non-animated
        protected bool _isFocused = false;  // For deafault a Button is not focused
        protected bool _iseditable = true; // For default a Button is a only-Click-Button
        public Vector2 boxSize;

        public List<string> _titel;
        public List<string> _inhalt;

        public Vector2[] ueberschriftenPositions;
        public Vector2[] inhaltPositions;
        public Vector2 titelPosition;

        protected string _type;

        protected Color _colorInhalt;
        protected Color _colorTitel;
        protected Color _colorUeberschrift;

        protected Vector2 _sizelongestLine;
        protected float _lineHeight;

        protected string _longestString;

        public bool _hasToUpdate;
        public bool _hasEditButton;

        protected Button editButton;




        public Infobox()
        {

        }

        public Infobox(string name, string type, Texture2D textureBackground, SpriteFont fontUeberschriften, SpriteFont fontText, SpriteFont fontTitel, float scale, int positionX, int positionY, bool hasEditButton)
        {
            _colorInhalt = _colorTitel = _colorUeberschrift = Color.Black;
            _name = name;
            _texBackground = textureBackground;
            _scale = scale;
            _infoboxX = positionX;
            _infoboxY = positionY;
            _fontText = fontText;
            _fontTitel = fontTitel;
            _fontUeberschriften = fontUeberschriften;
            _titel = new List<string> { "bla", "bla2", "bla3" };
            _inhalt = new List<string> { "0", "1", "23", };
            boxSize = GetInfoboxWidthHeight();
            _longestString = "Bla: 1234";
            _type = type;
            _hasToUpdate = true;
            _hasEditButton = hasEditButton;


            if(_type == "OneLine")
            {
                SetParameterInfobox();
            }
            if (_hasEditButton)
            {
                editButton = new Button("", Fighthandler.textureEditbutton, Fighthandler.textureEditbuttonHover, 0.1f, 0, 0);
                UpdateButtonInfobox();
            }

        }

        public Infobox(Charakter charakter, string type,  string text, SpriteFont fontTitel, SpriteFont fontText, Texture2D textureBackground, float scale, int positionX, int positionY, bool hasEditButton)
        {
            _colorInhalt = _colorTitel = _colorUeberschrift = Color.Black;
            _name = charakter.Name;

            if(_type == "Waffenbox")
            {
                _name = "Waffe´von " + charakter.Name;
            }

            _texBackground = textureBackground;
            _scale = scale;
            _infoboxX = positionX;
            _infoboxY = positionY;
            _fontText = fontText;
            _fontTitel = fontTitel;

            _fontUeberschriften = fontText;
            _titel = new List<string> { "Test", "test2" }; 
            _inhalt = new List<string> { "test", "Test2" };
            boxSize = GetInfoboxWidthHeight();
            _longestString = "Bla: 1234";
            _type = type;
            _hasToUpdate = true;
            _hasEditButton = hasEditButton;


            if (_type == "OneLine" || _type == "Waffenbox")
            {
                SetParameterInfobox();
            }
            if (_hasEditButton)
            {
                editButton = new Button("", Fighthandler.textureEditbutton, Fighthandler.textureEditbuttonHover, 0.1f, 0, 0);
                UpdateButtonInfobox();
            }
        }


        public void UpdateButtonInfobox()
        {
            if(_hasEditButton)
            {
                editButton.ButtonX = (int)_infoboxX + (int)boxSize.X / 2 + (int)boxSize.X / 4;
                editButton.ButtonY = (int)_infoboxY + (int)boxSize.Y - (int)boxSize.Y / 2 - (int)boxSize.Y / 4 - (int)boxSize.Y / 8 - (int)boxSize.Y / 16 - (int)boxSize.Y / 64;

                if (Game1._graphics.IsFullScreen == true)
                {
                    editButton.Scale = 0.07f;
                }
                else
                {
                    editButton.Scale = 0.05f;
                }

                if (editButton.IsClicked())
                {
                    Fighthandler._isInModeCharakterEdit = true;
                }
            }
            
        }


        public void SetParameterInfobox()
        {
            if(_hasToUpdate)
            {
                if(_type == "OneLine")
                {
                    _sizelongestLine = _fontUeberschriften.MeasureString(_longestString);
                }
                else
                {
                    _sizelongestLine = _fontTitel.MeasureString(_longestString);
                    _sizelongestLine.X += _sizelongestLine.X;
                    if(_sizelongestLine.X < 120)
                    {
                        _sizelongestLine.X += 50;
                    }
                }
                
               
                Vector2 fontSizeTitel = _fontTitel.MeasureString(_name);
                float heightTitel = fontSizeTitel.Y + 10f;

                if (_type == "Waffenbox")
                {
                    heightTitel = fontSizeTitel.Y + 30f;
                }
                _lineHeight = _sizelongestLine.Y;

                
                float addY = 15f;
                float addX = 5f;

                if (_type == "Waffenbox")
                {
                    addY = 25f;
                }

                if (Game1._graphics.IsFullScreen == true)
                {
                    addY = 25f;
                    addX = 10f;
                }


                float x = _infoboxX + 10f;

                

                if (Game1._graphics.IsFullScreen == true)
                {
                    titelPosition = new Vector2(x, _infoboxY + 25f);
                }
                else
                {
                    titelPosition = new Vector2(x, _infoboxY + 10f);
                }
               

                float heightAllLines = _lineHeight * _titel.Count;
                ueberschriftenPositions = new Vector2[_titel.Count];
                inhaltPositions = new Vector2[_inhalt.Count];

                float sizeHeader = (titelPosition.Y + heightTitel) - _infoboxY;

                while (_sizelongestLine.X + addX <= boxSize.X)
                {
                    boxSize.X -= 0.2f;

                }

                while (heightAllLines + addY <= boxSize.Y - sizeHeader)
                {
                    boxSize.Y -= 0.2f;
                }


                // Box vergrößern falls notwendig
                while (_sizelongestLine.X + addX > boxSize.X)
                {
                    boxSize.X += 0.2f;
                }

                while (heightAllLines + addY > boxSize.Y - sizeHeader)
                {
                    boxSize.Y += 0.2f;
                }

                if (_type == "Waffenbox")
                {
                    titelPosition.X +=20f;
                }

                float y;
                if (Game1._graphics.IsFullScreen == true)
                {
                    y = titelPosition.Y + heightTitel + 25f;
                }
                else
                {
                    y = titelPosition.Y + heightTitel + 10f;
                }
                
                if(_type == "OneLine")
                {
                    for (int i = 0; i < ueberschriftenPositions.Length; i++)
                    {
                        int index = i;
                        if (_type == "OneLine")
                        {
                            _titel[index] = _titel[index] + ": ";
                        }

                        if (index == 0)
                        {
                            ueberschriftenPositions[index] = new Vector2(x, y);
                            inhaltPositions[index] = new Vector2((x + _fontUeberschriften.MeasureString(_titel[index]).X), y);

                        }
                        else
                        {
                            ueberschriftenPositions[index] = new Vector2(x, ueberschriftenPositions[index - 1].Y + _lineHeight);
                            inhaltPositions[index] = new Vector2((x + _fontUeberschriften.MeasureString(_titel[index]).X), ueberschriftenPositions[index - 1].Y + _lineHeight);
                        }

                    }
                }
                else if(_type == "Waffenbox")
                {
                    inhaltPositions[0] = new Vector2(x  + 20f, y);
                }
                
                _hasToUpdate = false;
            }
            
        }


        public void UpdateInfobox()
        {
            SetParameterInfobox();
            UpdateButtonInfobox();
        }

        //Returns Position Vector of the Button used for Drawing
        public Vector2 GetPos()
        {
            return new Vector2(this._infoboxX, this._infoboxY);
        }

        public Vector2 GetInfoboxWidthHeight()
        {
            float width = (_texBackground.Width * _scale);
            float height = (_texBackground.Height * _scale);
            return new Vector2(width, height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_texBackground, new Rectangle(_infoboxX, _infoboxY, (int)boxSize.X, (int)boxSize.Y), Color.White);

            spriteBatch.DrawString(_fontTitel, _name, titelPosition, _colorTitel);
            if (_type == "OneLine")
            {
                for (int i = 0; i < ueberschriftenPositions.Length; i++)
                {
                    spriteBatch.DrawString(_fontUeberschriften, _titel[i], ueberschriftenPositions[i], _colorUeberschrift);
                    spriteBatch.DrawString(_fontText, _inhalt[i], inhaltPositions[i], _colorInhalt);
             
                }
            }
            else if(_type == "Waffenbox")
            {
                spriteBatch.DrawString(_fontText, _titel[0], inhaltPositions[0], _colorInhalt);
            }
            
            if (_hasEditButton)
            {
                editButton.Draw(spriteBatch, _fontText);
            }

            spriteBatch.End();


        }

    }
}
