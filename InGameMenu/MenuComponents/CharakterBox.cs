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


        
        public CharakterBox(Charakter charakter, string type, float scale, int positionX, int positionY, bool hasEditButton)
        {
            _charakter = charakter;
            
            _name = charakter.Name;
            _type = type;


            if (_type == "Waffenbox")
            {
                _ueberschriftBox = _name + ": Waffe";
            }
            else
            {
                _ueberschriftBox = _name + ": Uebersicht";
            }

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
            if (Game1._graphics.IsFullScreen == true)
            {
                _fontText = InformationComponents.aika20;
                _fontUeberschriften = InformationComponents.aika20;
                _fontTitel = InformationComponents.inclitodo;
            }
            else
            {
                _fontText = InformationComponents.aika12;
                _fontUeberschriften = InformationComponents.aika12;
                _fontTitel = InformationComponents.inclitodo15;
            }


            _longestString = _ueberschriftBox;


            //Schriftfarben
            if (_type == "Waffenbox")
            {
                _colorTitel = Color.White;
                _colorUeberschrift = Color.White;
                _colorInhalt = Color.White;
            }
            else
            {
                _colorTitel = Color.White;
                _colorUeberschrift = Color.White; //Coral
                _colorInhalt = Color.White;
            }

            List<string> titellist = new List<string> { "bla", "bla2", "bla3", "bla4" };
            List<string> inhaltlist = new List<string> { "0", "1", "23", "3637287363"};
            boxSize = GetInfoboxWidthHeight();
            SetParametereCharakterBox(charakter);

            

        }

        public void SetParametereCharakterBox(Charakter charakter)
        {
            if(_hasToUpdate == true)
            {
                //Size-Type setzen (legt später fest wie groß die Schrift ist und wie viele Infromationen die Box beinhaltet
                if (charakter.IsNPC)
                {
                    switch (Fighthandler.fightMenu.boxCount[1])
                    {
                        case 1:
                            _sizetype = 2;
                            break;
                        case 2:
                            _sizetype = 1;
                            break;
                        case 4:
                            _sizetype = 1;
                            break;
                        default:
                            _sizetype = 1;
                            break;
                    }
                }
                else
                {
                    switch (Fighthandler.fightMenu.boxCount[0])
                    {
                        case 1:
                            _sizetype = 2;
                            break;
                        case 2:
                            _sizetype = 1;
                            break;
                        case 4:
                            _sizetype = 1;
                            break;
                        default:
                            _sizetype = 1;
                            break;
                    }
                }
                if(Game1._graphics.IsFullScreen == true) // Wenn das Spiel im Vollbildmodus ist, so wird die Box größer
                {
                    _sizetype += 1;
                }

                //Fonts
                switch (_sizetype)
                {
                    case 1:
                        _fontText = InformationComponents.aika12;
                        _fontUeberschriften = InformationComponents.aika12;
                        _fontTitel = InformationComponents.inclitodo15;
                        break;
                    case 2:
                        _fontText = InformationComponents.aika;
                        _fontUeberschriften = InformationComponents.aika;
                        _fontTitel = InformationComponents.inclitodo;
                        break;
                    case 3:
                        _fontText = InformationComponents.aika20;
                        _fontUeberschriften = InformationComponents.aika20;
                        _fontTitel = InformationComponents.inclitodo30;
                        break;
                    default:
                        _fontText = InformationComponents.aika12;
                        _fontUeberschriften = InformationComponents.aika12;
                        _fontTitel = InformationComponents.inclitodo15;
                        break;

                }         

                //Content setzen (Welche Infomrationen werden dargestellt)
                SetContent(charakter);

                //Infobox Parameter setzen (Positionen des Inhaltes, Größe der Box)
                SetParameterInfobox();

                //Buttons der Boxen Updaten
                this.UpdateButtonInfobox();

                _hasToUpdate = false;


                
                
                
            }
            
        }

        public void UpdateCharakterboxParameters()
        {
            SetParametereCharakterBox(_charakter);
        }

        public void SetContent(Charakter charakter)
        {
            
            //Infomrations Variablen setzen
            string weapon;
            string wiederstandskraft;
            string abwehr;
            string wortgewandtheit;
            string glueck;
            string level;
            string ignoranz;

            wiederstandskraft = charakter.Widerstandskraft.ToString();
            weapon = charakter.Weapon.Name;
            abwehr = charakter.Abwehr.ToString();
            wortgewandtheit = charakter.Wortgewandheit.ToString();
            glueck = charakter.Glueck.ToString();
            level = charakter.Level.ToString();
            ignoranz = charakter.Ignoranz.ToString();

            // Arrays zuordnen: Welche Größe hat die Box? | Welche Kampfart? --> Was wird angezeigt?
            if(Game1.GState == Game1.GameState.InFight)
            {
                switch (_sizetype)
                {
                    case 1:
                        _titel = new List<string> { "Level", "Wiederstandskraft" };
                        _inhalt = new List<string> { level, wiederstandskraft };
                        break;
                    case 2:
                        _titel = new List<string> { "Level", "Wiederstandskraft", "Abwehr" };
                        _inhalt = new List<string> { level, wiederstandskraft, abwehr };
                        break;
                    case 3:
                        _titel = new List<string> { "Level", "Wiederstandskraft", "Abwehr", "Glueck" };
                        _inhalt = new List<string> { level, wiederstandskraft, abwehr, glueck };
                        break;
                    default:
                        _titel = new List<string> { "Level", "Wiederstandskraft" };
                        _inhalt = new List<string> { level, wiederstandskraft };
                        break;
                }
            }
            else if(Game1.GState == Game1.GameState.InTalkFight)
            {
                switch (_sizetype)
                {
                    case 1:
                        _titel = new List<string> { "Level", "Wortgewandtheit" };
                        _inhalt = new List<string> { level, wortgewandtheit };
                        break;
                    case 2:
                        _titel = new List<string> { "Level", "Wortgewandtheit", "Ignoranz" };
                        _inhalt = new List<string> { level, wortgewandtheit, ignoranz };
                        break;
                    case 3:
                        _titel = new List<string> { "Level", "Wortgewandtheit", "Ignoranz", "Glueck" };
                        _inhalt = new List<string> { level, wortgewandtheit, ignoranz, glueck };
                        break;
                    default:
                        _titel = new List<string> { "Level", "Wortgewandtheit" };
                        _inhalt = new List<string> { level, wortgewandtheit };
                        break;
                }
            }

        }


    }
}
