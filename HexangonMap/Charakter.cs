using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Guus_Reise.HexangonMap;
using Guus_Reise.Animation;

namespace Guus_Reise
{
    [Serializable]
    class Charakter
    {
        private String _name;
        private bool _npc;
        private bool _canBefriend;
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

        private Weapon _currWeapon;         //Ausgewählte Waffe Standard Faust
        private Weapon _currWeaponTalkFight; //Ausgewählte Waffe für den TalkFight
        private List<Skill> _currSkills = new List<Skill>() { Guus_Reise.Skill.skills[0] }; //Ausgewählte Skills
        private List<Skill> _currSkillsTalk = new List<Skill>() { Guus_Reise.Skill.skills[1] }; //Ausgewählte Skills
        public List<Weapon> _inventar = new List<Weapon>();          //Liste aller für den Spieler verfügbaren Waffen
        public List<Weapon> _inventarTalkFight = new List<Weapon>(); //Liste aller für den Spieler verfügbaren Waffen im Talkfight
        public List<Skill> _skills = new List<Skill>();             //Liste aller für den Spieler verfügbaren Skills
        public List<Skill> _skillsTalk = new List<Skill>();
        private bool gaveUp = false;

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

        public Weapon WeaponTalkFight
        {
            get => _currWeaponTalkFight;
            set => _currWeaponTalkFight = value;
        }

        public List<Weapon> WeaponInv
        {
            get => _inventar;
            set => _inventar = value;
        }

        public List<Weapon> WeaponInvTalk
        {
            get => _inventarTalkFight;
            set => _inventarTalkFight = value;
        }

        public List<Skill> Skill
        {
            get => _currSkills;
            set => _currSkills = value;
        }

        public List<Skill> SkillTalk
        {
            get => _currSkillsTalk;
            set => _currSkillsTalk = value;
        }

        public List<Skill> SkillInv
        {
            get => _skills;
            set => _skills = value;
        }

        public List<Skill> SkillInvTalk
        {
            get => _skillsTalk;
            set => _skillsTalk = value;
        }

        public bool IsNPC
        {
            get => _npc;
            set => _npc = value;
        }

        public bool CanBefriended
        {
            get => _canBefriend;
            set => _canBefriend = value;
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
        public bool GaveUp 
        { 
            get => gaveUp; 
            set => gaveUp = value; 
        }

        public Charakter (String name, int level, int xp, int leben, int angriff1, int angriff2, int abwehr, int wortgewand1, int wortgewand2, int ignoranz, int geschwindigkeit, int glück, int bewegungsreichweite, int fpunkte, Hex hex, CharakterAnimation charakterAnimation)
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

            _charakterAnimation = charakterAnimation;

            this.WeaponInv.Add(Weapon.weapons[0]);
            this.WeaponInvTalk.Add(Weapon.weapons[7]);
            this.Weapon = this.WeaponInv[0];
            this.WeaponTalkFight = this.WeaponInvTalk[0];

            for(int i=0; i< this.Level/5; i += 2)
            {
                this.SkillInv.Add(Guus_Reise.Skill.skills[i]);
                this.SkillInvTalk.Add(Guus_Reise.Skill.skills[i+1]);
            }

            //Fehlende Parameter für die CharakterAnimation setzen
            charakterAnimation.SetParametersAfterInitCharakter(this, hex);
        }


        public Charakter(String name, int level, int ki, Hex hex, CharakterAnimation charakterAnimation)
        {
            int[] stats = LevelToStats(name, level);
            SetCharakter(name, stats);
            this.KI = ki;
            _charakterAnimation = charakterAnimation;

            if (this.KI == 4)
            {
                switch (LevelHandler.currentWorld, LevelHandler.currentLevel)
                {
                    case (1, 1):
                        for (int i = 0; i < LevelDatabase.W1L1npcPatroulPoints.GetLength(0); i++)
                        {
                            this.Patroullienpunkte.Add(new Point(LevelDatabase.W1L1npcPatroulPoints[i, 0], LevelDatabase.W1L1npcPatroulPoints[i, 1]));
                        }
                        break;
                    case (1, 2):
                        for (int i = 0; i < LevelDatabase.W1L2npcPatroulPoints.GetLength(0); i++)
                        {
                            this.Patroullienpunkte.Add(new Point(LevelDatabase.W1L2npcPatroulPoints[i, 0], LevelDatabase.W1L2npcPatroulPoints[i, 1]));
                        }
                        break;
                    case (2, 1):
                        for (int i = 0; i < LevelDatabase.W2L1npcPatroulPoints.GetLength(0); i++)
                        {
                            this.Patroullienpunkte.Add(new Point(LevelDatabase.W2L1npcPatroulPoints[i, 0], LevelDatabase.W2L1npcPatroulPoints[i, 1]));
                        }
                        break;
                }
            }
            switch (name)
            {
                case "Bully":
                    this.WeaponInv.Add(Weapon.weapons[2]);
                    this.WeaponInvTalk.Add(Weapon.weapons[7]);
                    break;
                case "Heavyweight":
                    this.WeaponInv.Add(Weapon.weapons[1]);
                    this.WeaponInvTalk.Add(Weapon.weapons[7]);
                    break;
                case "Preacher":
                    this.WeaponInv.Add(Weapon.weapons[4]);
                    this.WeaponInvTalk.Add(Weapon.weapons[8]);
                    break;
                case "Politician":
                    this.WeaponInv.Add(Weapon.weapons[5]);
                    this.WeaponInvTalk.Add(Weapon.weapons[9]);
                    break;
                case "Assassin":
                    this.WeaponInv.Add(Weapon.weapons[6]);
                    this.WeaponInvTalk.Add(Weapon.weapons[10]);
                    break;
                case "Daydreamer":
                    this.WeaponInv.Add(Weapon.weapons[3]);
                    this.WeaponInvTalk.Add(Weapon.weapons[11]);
                    break;
                case "old_man":
                    this.WeaponInv.Add(Weapon.weapons[2]);
                    this.WeaponInvTalk.Add(Weapon.weapons[8]);
                    break;
                default:
                    this.WeaponInv.Add(Weapon.weapons[0]);
                    this.WeaponInvTalk.Add(Weapon.weapons[7]);
                    break;
            }           
            this.Weapon = this.WeaponInv[0];
            this.WeaponTalkFight = this.WeaponInvTalk[0];

            for (int i = 0; i < this.Level / 5; i += 2)
            {
                this.SkillInv.Add(Guus_Reise.Skill.skills[i]);
                this.SkillInvTalk.Add(Guus_Reise.Skill.skills[i + 1]);
            }

            //Fehlende Parameter für die CharakterAnimation setzen
            charakterAnimation.SetParametersAfterInitCharakter(this, hex);
        }
        public Charakter(Charakter toClone)
        {
            Name = toClone.Name;
            IsNPC = toClone.IsNPC;
            CanBefriended = toClone.CanBefriended;
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
            WeaponTalkFight = toClone.WeaponTalkFight;
            Skill = toClone.Skill;
            SkillTalk = toClone.SkillTalk;
            WeaponInv = toClone.WeaponInv;
            WeaponInvTalk = toClone.WeaponInvTalk;
            SkillInv = toClone.SkillInv;
            SkillInvTalk = toClone.SkillInvTalk;
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
            this.IsNPC = true;            
            this.Fähigkeitspunkte = werte[10];
            this.Level = werte[11];
            _isMoving = false;     
        }

        public int[] LevelToStats(String name, int level)
        {
            int[] stats = new int[14];
            int fpoints = level + 30;

            switch (name)
            {
                case "Bully":
                    stats[0] = (int)(fpoints * 0.3f);
                    stats[1] = (int)(fpoints * 0.33f);
                    stats[2] = (int)(fpoints * 0.03f);
                    stats[3] = (int)(fpoints * 0.1f);
                    stats[4] = (int)(fpoints * 0.03f);
                    stats[5] = (int)(fpoints * 0.03f);
                    stats[6] = (int)(fpoints * 0.03f);
                    stats[7] = (int)(fpoints * 0.1f);
                    stats[8] = (int)(fpoints * 0.05f);
                    stats[9] = 5;
                    stats[10] = fpoints;
                    stats[11] = level;
                    break;
                case "Heavyweight":
                    stats[0] = (int)(fpoints * 0.13f);
                    stats[1] = (int)(fpoints * 0.2f);
                    stats[2] = (int)(fpoints * 0.03f);
                    stats[3] = (int)(fpoints * 0.4f);
                    stats[4] = (int)(fpoints * 0.03f);
                    stats[5] = (int)(fpoints * 0.03f);
                    stats[6] = (int)(fpoints * 0.03f);
                    stats[7] = (int)(fpoints * 0.1f);
                    stats[8] = (int)(fpoints * 0.05f);
                    stats[9] = 5;
                    stats[10] = fpoints;
                    stats[11] = level;
                    break;
                case "Preacher":
                    stats[0] = (int)(fpoints * 0.13f);
                    stats[1] = (int)(fpoints * 0.03f);
                    stats[2] = (int)(fpoints * 0.03f);
                    stats[3] = (int)(fpoints * 0.03f);
                    stats[4] = (int)(fpoints * 0.08f);
                    stats[5] = (int)(fpoints * 0.35f);
                    stats[6] = (int)(fpoints * 0.2f);
                    stats[7] = (int)(fpoints * 0.1f);
                    stats[8] = (int)(fpoints * 0.05f);
                    stats[9] = 5;
                    stats[10] = fpoints;
                    stats[11] = level;
                    break;
                case "Politician":
                    stats[0] = (int)(fpoints * 0.13f);
                    stats[1] = (int)(fpoints * 0.03f);
                    stats[2] = (int)(fpoints * 0.03f);
                    stats[3] = (int)(fpoints * 0.03f);
                    stats[4] = (int)(fpoints * 0.35f);
                    stats[5] = (int)(fpoints * 0.08f);
                    stats[6] = (int)(fpoints * 0.2f);
                    stats[7] = (int)(fpoints * 0.1f);
                    stats[8] = (int)(fpoints * 0.05f);
                    stats[9] = 5;
                    stats[10] = fpoints;
                    stats[11] = level;
                    break;
                case "Assassin":
                    stats[0] = (int)(fpoints * 0.13f);
                    stats[1] = (int)(fpoints * 0.03f);
                    stats[2] = (int)(fpoints * 0.3f);
                    stats[3] = (int)(fpoints * 0.1f);
                    stats[4] = (int)(fpoints * 0.03f);
                    stats[5] = (int)(fpoints * 0.03f);
                    stats[6] = (int)(fpoints * 0.03f);
                    stats[7] = (int)(fpoints * 0.3f);
                    stats[8] = (int)(fpoints * 0.05f);
                    stats[9] = 5;
                    stats[10] = fpoints;
                    stats[11] = level;
                    break;
                case "Daydreamer":
                    stats[0] = (int)(fpoints * 0.2f);
                    stats[1] = (int)(fpoints * 0.03f);
                    stats[2] = (int)(fpoints * 0.03f);
                    stats[3] = (int)(fpoints * 0.03f);
                    stats[4] = (int)(fpoints * 0.03f);
                    stats[5] = (int)(fpoints * 0.03f);
                    stats[6] = (int)(fpoints * 0.1f);
                    stats[7] = (int)(fpoints * 0.05f);
                    stats[8] = (int)(fpoints * 0.5f);
                    stats[9] = 5;
                    stats[10] = fpoints;
                    stats[11] = level;
                    break;
                case "old_man":
                    stats[0] = (int)(fpoints * 0.13f);
                    stats[1] = (int)(fpoints * 0.03f);
                    stats[2] = (int)(fpoints * 0.03f);
                    stats[3] = (int)(fpoints * 0.03f);
                    stats[4] = (int)(fpoints * 0.03f);
                    stats[5] = (int)(fpoints * 0.2f);
                    stats[6] = (int)(fpoints * 0.4f);
                    stats[7] = (int)(fpoints * 0.1f);
                    stats[8] = (int)(fpoints * 0.05f);
                    stats[9] = 5;
                    stats[10] = fpoints;
                    stats[11] = level;
                    break;
                default:
                    stats[0] = (int)(fpoints * 0.12f);
                    stats[1] = (int)(fpoints * 0.11f);
                    stats[2] = (int)(fpoints * 0.11f);
                    stats[3] = (int)(fpoints * 0.11f);
                    stats[4] = (int)(fpoints * 0.11f);
                    stats[5] = (int)(fpoints * 0.11f);
                    stats[6] = (int)(fpoints * 0.11f);
                    stats[7] = (int)(fpoints * 0.11f);
                    stats[8] = (int)(fpoints * 0.11f);
                    stats[9] = 5;
                    stats[10] = fpoints;
                    stats[11] = level;
                    break;
            }
                    return stats;
        }

        public void Draw(Camera camera)
        {
            if (Game1.GState == Game1.GameState.InFight || Game1.GState == Game1.GameState.InTalkFight)
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
                if(MovementAnimationManager._currentMovementAnimation.movementType != "NPCMovement")
                {
                    if (this != MovementAnimationManager._currentMovementAnimation.movingCharakter)
                    {
                        _charakterAnimation.DrawCharakter(camera);
                    }
            }
                else
                {
                    if (_charakterAnimation.Charakter.IsNPC != true)
                    {
                        _charakterAnimation.DrawCharakter(camera);
                    }

                }
                
            }
            else
            {
                _charakterAnimation.DrawCharakter(camera);
            }
        }

        public int GainXp(Charakter winner, Charakter looser)
        {
            int hilf = ((looser.Level - winner.Level) * 15) + 50;
            
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
                Fighthandler.fightResults.LevelUp.Add(Name);
            }
            else
            {
                winner.XP += hilf;
            }

            for (int i = 0; i < winner.Level / 5; i += 2)
            {
                if (!winner.SkillInv.Contains(Guus_Reise.Skill.skills[i]))
                {
                    winner.SkillInv.Add(Guus_Reise.Skill.skills[i]);
                }
                if (!winner.SkillInvTalk.Contains(Guus_Reise.Skill.skills[i+1]))
                {
                    winner.SkillInvTalk.Add(Guus_Reise.Skill.skills[i + 1]);
                }             
            }

            return hilf;
        }

        public Charakter Clone()
        {
            return new Charakter(this);
        }
    }
}
