using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Color = Microsoft.Xna.Framework.Color;

namespace Guus_Reise.InGameMenu.MenuComponents
{
    class CharakterBox : Infobox
    {
        Charakter _charakter;


        
        public CharakterBox(Charakter charakter, float scale, int positionX, int positionY, bool hasEditButton)
        {
            _charakter = charakter;
            
            _name = charakter.Name;

            _type = "OneLine";

            _hasToUpdate = true;

            _hasEditButton = hasEditButton;

            if (_hasEditButton)
            {
                editButton = new Button("", Fighthandler.textureEditbutton, Fighthandler.textureEditbuttonHover, 0.05f, 0, 0);
            }

            //Textur festlegen (rot oder blau)
            if (charakter.IsNPC)
            {
                _texBackground = Fighthandler.enemyCharakterInfobox;
            }
            else
            {
                _texBackground = Fighthandler.playerCharakterInfobox;
            }
            

            //Größe der Box
            _scale = scale;
            _infoboxX = positionX;
            _infoboxY = positionY;

            //Fonts

            if(Game1._graphics.IsFullScreen == true)
            {
                _fontText = InformationComponents.aika;
                _fontUeberschriften = InformationComponents.aika;
                _fontTitel = InformationComponents.inclitodo;
            }
            else
            {
                _fontText = InformationComponents.aika12;
                _fontUeberschriften = InformationComponents.aika12;
                _fontTitel = InformationComponents.inclitodo15;
            }

            _longestString = "Wiederstandskraft: 1111";

            //Schriftfarben
            _colorTitel = Color.White;
            _colorUeberschrift = Color.BlueViolet; //Coral
            _colorInhalt = Color.Azure;

            

            List<string> titellist = new List<string> { "bla", "bla2", "bla3", "bla4" };
            List<string> inhaltlist = new List<string> { "0", "1", "23", "3637287363"};
            boxSize = GetInfoboxWidthHeight();
            SetParametereCharakterBox(charakter);

            

        }

        public void SetParametereCharakterBox(Charakter charakter)
        {
            if(_hasToUpdate == true)
            {
                SetContent(charakter);
                if (_type == "OneLine")
                {
                    SetParameterInfoboxOneLine();
                    if (Game1._graphics.IsFullScreen == true)
                    {
                        titelPosition.X = _infoboxX+ 25f;
                    }
                    else
                    {
                        titelPosition.X = _infoboxX + 10f;
                    }
                       
                    titelPosition.Y += 3f;
                    _hasToUpdate = false;
                }
                this.UpdateButtonInfobox();
                
                
            }
            
        }

        public void UpdateCharakterboxParameters()
        {
            SetParametereCharakterBox(_charakter);
        }

        public void SetContent(Charakter charakter)
        {
            string weapon;
            string wiederstandskraft;
            string abwehr;
            string xp;
            string wortgewandtheit;

            wiederstandskraft = charakter.Widerstandskraft.ToString();
            weapon = charakter.Weapon.Name;
            abwehr = charakter.Abwehr.ToString();
            wortgewandtheit = charakter.Wortgewandheit.ToString();
            xp = charakter.XP.ToString();
            if (Game1._graphics.IsFullScreen == true)
            {
                _titel = new List<string> { "Waffe", "Wiederstandskraft", "XP", "Abwehr", "Wortgewandtheit"};
                _inhalt = new List<string> { weapon, wiederstandskraft, xp, abwehr, wortgewandtheit };
            }
            else
            {
                _titel = new List<string> { "Waffe", "Wiederstandskraft" };
                _inhalt = new List<string> { weapon, wiederstandskraft };
            }       

        }


    }
}
