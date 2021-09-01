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
        public string _ueberschriftBox;

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

        public Button editButton;

        public int _sizetype;

        public int currentWeapon;




        public Infobox()
        {

        }

        public Infobox(string name, string type, Texture2D textureBackground, SpriteFont fontUeberschriften, SpriteFont fontText, SpriteFont fontTitel, float scale, int positionX, int positionY, bool hasEditButton)
        {
            _colorInhalt = _colorTitel = _colorUeberschrift = Color.Black;
            _name = name;
            _ueberschriftBox = _name;
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

            _sizetype = 1;


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
                switch (_sizetype)
                {
                    case 1:
                        editButton.Scale = 0.05f;
                        editButton.ButtonX = (int)_infoboxX + (int)boxSize.X / 2 + (int)boxSize.X / 4 + 20;
                        editButton.ButtonY = (int)_infoboxY + (int)boxSize.Y - (int)boxSize.Y / 2 - (int)boxSize.Y / 4 - (int)boxSize.Y / 8 - (int)boxSize.Y / 16 - (int)boxSize.Y / 64 - 2;
                        break;
                    case 2:
                        editButton.Scale = 0.07f;
                        editButton.ButtonX = (int)_infoboxX + (int)boxSize.X / 2 + (int)boxSize.X / 4 + 10;
                        editButton.ButtonY = (int)_infoboxY + (int)boxSize.Y - (int)boxSize.Y / 2 - (int)boxSize.Y / 4 - (int)boxSize.Y / 8 - (int)boxSize.Y / 16 - (int)boxSize.Y / 64 + 5;
                        break;
                    case 3:
                        editButton.Scale = 0.1f;
                        editButton.ButtonX = (int)_infoboxX + (int)boxSize.X / 2 + (int)boxSize.X / 4 + 20;
                        editButton.ButtonY = (int)_infoboxY + (int)boxSize.Y - (int)boxSize.Y / 2 - (int)boxSize.Y / 4 - (int)boxSize.Y / 8 - (int)boxSize.Y / 16 - (int)boxSize.Y / 64 + 7;
                        break;
                    default:
                        editButton.Scale = 0.05f;
                        editButton.ButtonX = (int)_infoboxX + (int)boxSize.X / 2 + (int)boxSize.X / 4 + 10;
                        editButton.ButtonY = (int)_infoboxY + (int)boxSize.Y - (int)boxSize.Y / 2 - (int)boxSize.Y / 4 - (int)boxSize.Y / 8 - (int)boxSize.Y / 16 - (int)boxSize.Y / 64 + 5;
                        break;
                }
            }
            
        }


        public void SetParameterInfobox()
        {

            if (_hasToUpdate)
            {
                ueberschriftenPositions = new Vector2[_titel.Count];
                inhaltPositions = new Vector2[_inhalt.Count];

                //Länge und Höhe der Überschrift messen
                if (_hasEditButton)
                {
                    _sizelongestLine = _fontTitel.MeasureString(_longestString + "......");
                    _sizelongestLine.X += 10;
                }
                else
                {
                    _sizelongestLine = _fontTitel.MeasureString(_longestString + "... ");
                }
                float heightTitel = _sizelongestLine.Y + 10f;

                //Höhe einer einzelnen Zeile in der Box (nicht Titel)
                _lineHeight = _fontText.MeasureString(_longestString + "...").Y;

                
                float addY = 5f;
                float addX = 0f;


                float x = _infoboxX + 10f;

                //Höhe der Box Überschrift
                switch(_sizetype)
                {
                    case 1:
                        titelPosition = new Vector2(x + 10f, _infoboxY + 12f);
                        break;
                    case 2:
                        titelPosition = new Vector2(x + 5f, _infoboxY + 25f);
                        addY = 25f;
                        addX = 20f;
                        break;
                    case 3:
                        titelPosition = new Vector2(x + 10f, _infoboxY + 50f);
                        addY = 35f;
                        addX = 20f;
                        x += 15f;
                        _lineHeight += 2f;
                        break;
                    default:
                        titelPosition = new Vector2(x + 5f, _infoboxY + 10f);
                        break;
                }

               
                //Höhe aller Zeilen
                float heightAllLines = (_lineHeight) * _titel.Count;

                //Höhe des Headers
                float sizeHeader = (titelPosition.Y + heightTitel) - _infoboxY;
                
             //Größe der Box anpassen
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
             //Ende "Größe der Box anpassen"


                float y;

                //Beginn des Informationsimhalt (Höhe) versetzen
                switch (_sizetype)
                {
                    case 1:
                        y = titelPosition.Y + heightTitel;
                        break;
                    case 2:
                        y = titelPosition.Y + heightTitel + 15f;
                        break;
                    case 3:
                        y = titelPosition.Y + heightTitel + 30f;
                        break;
                    default:
                        y = titelPosition.Y + heightTitel + 15f;
                        break;
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
                            ueberschriftenPositions[index] = new Vector2(x + 3f, y);
                            inhaltPositions[index] = new Vector2((x + _fontUeberschriften.MeasureString(_titel[index]).X)+ 3f, y);

                        }
                        else
                        {
                            ueberschriftenPositions[index] = new Vector2(x + 3f, ueberschriftenPositions[index - 1].Y + _lineHeight);
                            inhaltPositions[index] = new Vector2((x + _fontUeberschriften.MeasureString(_titel[index]).X) + 3f, ueberschriftenPositions[index - 1].Y + _lineHeight);
                        }

                    }
                }
                else if(_type == "Waffenbox")
                {
                    switch (_sizetype)
                    {
                        case 1:
                            inhaltPositions[0] = new Vector2(x + 20f, y + 20f);
                            break;
                        case 2:
                            inhaltPositions[0] = new Vector2(x + 20f, y + 40f);
                            break;
                        case 3:
                            inhaltPositions[0] = new Vector2(x + 20f, y + 40f);
                            break;
                        default:
                            inhaltPositions[0] = new Vector2(x + 20f, y + 40f);
                            break;
                    }
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

            //Hintergrund des Panels zeichnen
            spriteBatch.Draw(_texBackground, new Rectangle(_infoboxX, _infoboxY, (int)boxSize.X, (int)boxSize.Y), Color.White);

            //Überschfrift der Box zeichnen
            spriteBatch.DrawString(_fontTitel, _ueberschriftBox, titelPosition, _colorTitel);

            //Informationsinhalt der Box zeichnen
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
            
            // Edit Button Zeichnen, sofern vorhanden
            if (_hasEditButton)
            {
                editButton.Draw(spriteBatch, _fontText);
            }

            spriteBatch.End();


        }

    }
}
