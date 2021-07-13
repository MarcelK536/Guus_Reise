using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Guus_Reise.HexangonMap;

namespace Guus_Reise
{
    class Charakter
    {
        private String _name;
        private bool _npc;
        private int _ki;                //speichert welche KI verwendet werden soll
        private List<Point> _patroullienPunkte;
        private bool _canMove;
        private int _level;
        private int _xp;
        private int _widerstandskraft; //Leben des Charakters
        private int _koerperkraft;      //physichen Angriff
        private int _beweglichkeit;     //2ter phyischer Angriff
        private int _abwehr;            //physiche Abwehr
        private int _wortgewandheit;    //social Angriff
        private int _lautstaerke;        //2ter social Angriff
        private int _ingoranz;          //social Abwehr
        private int _geschwindigkeit;   //Aktionspunkte des Charakters im Kampf
        private int _glueck;            //wirkt sich auf kritische trefferchance aus
        private int[] _currentFightStats; //zum speichern der aktuellen stats im Kampf (wird zu beginn eines neuen resetet)
        private int _bewegungsreichweite;
        private int _fpunkte;
        private Point _logicalPosition;         //Position an welcher der Charakter gezeichnet wird
        private Point _logicalFightPosition;    //Platzhalter für Position im Kampf
        private Point _logicalBoardPosition;    //Platzhalter für Position auf Karte
        private bool _isMoving; //für die Drwaing Methode, movender Charakter wird über die MovementAnimation gezeichnet
        CharakterAnimation _charakterAnimation;
        private Weapon _currWeapon = Weapon.weapons[0];

        public String Name
        {
            get => _name;
            set => _name = value;
        }

        public Weapon Weapon
        {
            get => _currWeapon;
            set => _currWeapon = value;
        }
        public bool IsNPC
        {
            get => _npc;
            set => _npc = value;
        }
        public bool IsMoving
        {
            get => _isMoving;
            set => _isMoving = value;
        }
        public CharakterAnimation CharakterAnimation
        {
            get => _charakterAnimation;
            set => _charakterAnimation = value;
        }
        public int KI
        {
            get => _ki;
            set => _ki = value;
        }
        public List<Point> Patroullienpunkte
        {
            get => _patroullienPunkte;
            set => _patroullienPunkte = value;
        }
        public bool CanMove
        {
            get => _canMove;
            set => _canMove = value;
        }
        public int Level
        {
            get => _level;
            set => _level = value;
        }
        public int XP
        {
            get => _xp;
            set => _xp = value;
        }
        public int Widerstandskraft
        {
            get => _widerstandskraft;
            set => _widerstandskraft = value;
        }
        public int Koerperkraft
        {
            get => _koerperkraft;
            set => _koerperkraft = value;
        }
        public int Beweglichkeit
        {
            get => _beweglichkeit;
            set => _beweglichkeit = value;
        }
        public int Abwehr
        {
            get => _abwehr;
            set => _abwehr = value;
        }
        public int Wortgewandheit
        {
            get => _wortgewandheit;
            set => _wortgewandheit = value;
        }
        public int Lautstaerke
        {
            get => _lautstaerke;
            set => _lautstaerke = value;
        }
        public int Ignoranz
        {
            get => _ingoranz;
            set => _ingoranz = value;
        }
        public int Geschwindigkeit
        {
            get => _geschwindigkeit;
            set => _geschwindigkeit = value;
        }
        public int Glueck
        {
            get => _glueck;
            set => _glueck = value;
        }
        public int[] CurrentFightStats
        {
            get => _currentFightStats;
            set => _currentFightStats = value;
        }
        public int Bewegungsreichweite
        {
            get => _bewegungsreichweite;
            set => _bewegungsreichweite = value;
        }
        public int Fähigkeitspunkte
        {
            get => _fpunkte;
            set => _fpunkte = value;
        }
        public Point LogicalPosition
        {
            get => _logicalPosition;
            set => _logicalPosition = value;
        }
        public Point LogicalFightPosition 
        { 
            get => _logicalFightPosition; 
            set => _logicalFightPosition = value; 
        }
        public Point LogicalBoardPosition 
        { 
            get => _logicalBoardPosition; 
            set => _logicalBoardPosition = value; 
        }


        /*public Charakter (String name, int leben, int angriff, int abwehr, int wortgewand, int ignoranz, int geschwindigkeit, int glück, int bewegungsreichweite)
        {
            this.Name = name;
            this.Widerstandskraft = leben;
            this.Koerperkraft = angriff;
            this.Abwehr = abwehr;
            this.Wortgewandheit = wortgewand;
            this.Ignoranz = ignoranz;
            this.Geschwindigkeit = geschwindigkeit;
            this.Glueck = glück;
            this.Bewegungsreichweite = bewegungsreichweite;
            this.Fähigkeitspunkte = 0;
            this.Glow = new Vector3(0.1f, 0.1f, 0.1f);
            this.Color = new Vector3(0, 0, 0);
        }*/


        public Charakter(String klasse, int level, Hex hex, CharakterAnimation charakterAnimation)
        {
            int[] stats = LevelToStats(level, klasse);
            SetCharakter(klasse, stats);
            _charakterAnimation = charakterAnimation;

            //Fehlende Parameter für die CharakterAnimation setzen
            charakterAnimation.SetParametersAfterInitCharakter(this, hex);
        }

        public void SetCharakter(String name, int[] werte)
        {
            this.Name = name;
            this.Widerstandskraft = werte[0];
            this.Koerperkraft = werte[1];
            this.Beweglichkeit = werte[2];
            this.Abwehr = werte[3];
            this.Wortgewandheit = werte[4];
            this.Lautstaerke = werte[5];
            this.Ignoranz = werte[6];
            this.Geschwindigkeit = werte[7];
            this.Glueck = werte[8];
            this.Bewegungsreichweite = werte[9];
            if (werte[10] == 0)
            {
                this.IsNPC = false;
            }
            if (werte[10] == 1)
            {
                this.IsNPC = true;
            }
            this.KI = werte[11];
            this.Fähigkeitspunkte = werte[12];
            this.Level = werte[13];
            _isMoving = false;
        }

        public int[] LevelToStats(int level, String klasse)
        {
            int[] stats = new int[14];
            int fpoints = level * 3 + 37;
            switch (klasse)
            {
                case "Guu":
                    for(int i = 0; i<=8; i++)
                    {
                        stats[i] = 0;
                    }
                    stats[9] = 5;
                    stats[10] = 0;
                    stats[11] = 0;
                    stats[12] = fpoints;
                    stats[13] = level;
                    break;
                default:
                    for(int i=0; i<=8; i++)
                    {
                        stats[i] = fpoints / 9;
                    }
                    stats[9] = 5;
                    stats[10] = 1;
                    stats[11] = 2;
                    stats[12] = 0;
                    stats[13] = level;
                    break;
            }
            return stats;
        }

        public void Draw(Camera camera)
        {
            if (Game1.GState == Game1.GameState.InFight)
            {
                LogicalPosition = LogicalFightPosition;
                _charakterAnimation.UpdateHex(Fighthandler._fightBoard[this.LogicalPosition.X, this.LogicalPosition.Y]);
            }
            else
            {
                LogicalPosition = LogicalBoardPosition;
                _charakterAnimation.UpdateHex(HexMap._board[this.LogicalPosition.X, this.LogicalPosition.Y]);
            }
            if(Game1.GState == Game1.GameState.MovementAnimation)
            {
                if(!IsMoving)
                {
                    _charakterAnimation.DrawCharakter(camera);
                }
                
                
            }
            else
            {
                _charakterAnimation.DrawCharakter(camera);
            }
        }

        public void Play(string nameAnimation, float intervall)
        {
            _charakterAnimation.Play(nameAnimation, intervall);
        }

        public void Stop()
        {
            _charakterAnimation.StopAnimation();
        }



        public void GainXp(Charakter winner, Charakter looser)
        {
            int hilf = ((looser.Level - winner.Level) * 5) + 30;
            
            if (hilf < 0)
            {
                hilf = 0;
            }

            if (hilf+winner.XP >= 100)
            {
                int hilf2 = (hilf+winner.XP) / 100;
                hilf = (hilf+winner.XP) % 100;
                winner.Level += hilf2;
                winner.Fähigkeitspunkte += hilf2;
                winner.XP += hilf;
            }
            else
            {
                winner.XP += hilf;
            }
        }
    }
}
