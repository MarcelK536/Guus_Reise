using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Guus_Reise.HexangonMap;

namespace Guus_Reise
{
    [Serializable]
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
        private int[] _currentFightStats = new int[9]; //zum speichern der aktuellen stats im Kampf (wird zu beginn eines neuen resetet)
        private int _bewegungsreichweite;
        private int _fpunkte;
        private Point _logicalPosition;         //Position an welcher der Charakter gezeichnet wird
        private Point _logicalFightPosition;    //Platzhalter für Position im Kampf
        private Point _logicalBoardPosition;    //Platzhalter für Position auf Karte
        private bool _isMoving; //für die Drwaing Methode, movender Charakter wird über die MovementAnimation gezeichnet
        CharakterAnimation _charakterAnimation;
        private Weapon _currWeapon = Weapon.weapons[0];         //Ausgewählte Waffe Standard Faust
        private List<Skill> _currSkills = new List<Skill>() { Guus_Reise.Skill.skills[0], Guus_Reise.Skill.skills[1] }; //Ausgewählte Skills
        public List<Weapon> _inventar;          //Liste aller für den Spieler verfügbaren Waffen
        public List<Skill> _skills;             //Liste aller für den Spieler verfügbaren Skills

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

        public List<Weapon> WeaponInv
        {
            get => _inventar;
            set => _inventar = value;
        }

        public List<Skill> Skill
        {
            get => _currSkills;
            set => _currSkills = value;
        }

        public List<Skill> SkillInv
        {
            get => _skills;
            set => _skills = value;
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


        public Charakter (String name, int level, int xp, int leben, int angriff1, int angriff2, int abwehr, int wortgewand1, int wortgewand2, int ignoranz, int geschwindigkeit, int glück, int bewegungsreichweite, int fpunkte)
        {
            this.Name = name;
            this.Level = level;
            this.XP = xp;
            this.Widerstandskraft = leben;
            this.Koerperkraft = angriff1;
            this.Beweglichkeit = angriff2;
            this.Abwehr = abwehr;
            this.Wortgewandheit = wortgewand1;
            this.Lautstaerke = wortgewand2;
            this.Ignoranz = ignoranz;
            this.Geschwindigkeit = geschwindigkeit;
            this.Glueck = glück;
            this.Bewegungsreichweite = bewegungsreichweite;
            this.Fähigkeitspunkte = fpunkte;
            IsMoving = false;
        }


        public Charakter(String klasse, int level, Hex hex, CharakterAnimation charakterAnimation)
        {
            int[] stats = LevelToStats(level, klasse);
            SetCharakter(klasse, stats);
            _charakterAnimation = charakterAnimation;

            //Fehlende Parameter für die CharakterAnimation setzen
            charakterAnimation.SetParametersAfterInitCharakter(this, hex);
        }
        public Charakter(Charakter toClone)
        {
            Name = toClone.Name;
            IsNPC = toClone.IsNPC;
            KI = toClone.KI;
            Patroullienpunkte = toClone.Patroullienpunkte;
            CanMove = toClone.CanMove;
            Level = toClone.Level;
            XP = toClone.XP;
            Widerstandskraft = toClone.Widerstandskraft;
            Koerperkraft = toClone.Koerperkraft;
            Beweglichkeit = toClone.Beweglichkeit;
            Abwehr = toClone.Abwehr;
            Wortgewandheit = toClone.Wortgewandheit;
            Lautstaerke = toClone.Lautstaerke;
            Ignoranz = toClone.Ignoranz;
            Geschwindigkeit = toClone.Geschwindigkeit;
            Glueck = toClone.Glueck;
            Array.Copy(toClone.CurrentFightStats, CurrentFightStats, CurrentFightStats.Length);
            Bewegungsreichweite = toClone.Bewegungsreichweite;
            Fähigkeitspunkte = toClone.Fähigkeitspunkte;
            LogicalPosition = toClone.LogicalPosition;
            LogicalFightPosition = toClone.LogicalFightPosition;
            LogicalBoardPosition = toClone.LogicalBoardPosition;
            IsMoving = toClone.IsMoving;
            CharakterAnimation = toClone.CharakterAnimation;
            Weapon = toClone.Weapon;
            Skill = toClone.Skill;
            WeaponInv = toClone.WeaponInv;
            SkillInv = toClone.SkillInv;
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
                    stats[0] = 10;
                    for (int i = 1; i<=8; i++)
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

        public Charakter Clone()
        {
            return new Charakter(this);
        }
    }
}
