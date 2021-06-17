using System;
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
        private int _ki;
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
        private int[] _currentFightStats;
        private int _bewegungsreichweite;
        private int _fpunkte;
        private Point _logicalPosition;
        private static Model _model;
        private Vector3 _glow;
        private Vector3 _color;
        CharakterAnimation _charakterAnimation;
        private static Texture2D texBlue;


        public String Name
        {
            get => _name;
            set => _name = value;
        }
        public bool IsNPC
        {
            get => _npc;
            set => _npc = value;
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
        public Model Model
        {
            get => _model;
            set => _model = value;
        }
        public Vector3 Glow
        {
            get => _glow;
            set => _glow = value;
        }
        public Vector3 Color
        {
            get => _color;
            set => _color = value;
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

        
        public Charakter(String klasse, int level)
        {
            int[] stats = LevelToStats(level, klasse);
            SetCharakter(klasse, stats);
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
            this.Glow = new Vector3(0.1f, 0.1f, 0.1f);
            this.Color = new Vector3(0, 0, 0);
            _charakterAnimation = new CharakterAnimation(hexagon, this);
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
                    stats[11] = 1;
                    stats[12] = 0;
                    stats[13] = level;
                    break;
            }
            return stats;
        }

        public static void LoadContent(ContentManager content, SpriteBatch spriteBatch)
        {
            _model = content.Load<Model>("Charakter\\plane");
            texBlue = content.Load<Texture2D>("Charakter\\texTimmae");
            

        }

        public void Draw(Camera camera)
        {
            Matrix world = (Matrix.CreateScale(CharakterAnimation.CharakterScale) * Matrix.CreateRotationX(45) * Matrix.CreateTranslation(CharakterAnimation.CharakterPostion));
            foreach (var mesh in _model.Meshes)
            {
                 foreach (BasicEffect effect in mesh.Effects)
                 {
                    effect.TextureEnabled = true;
                    effect.Texture = texBlue;
                    //effect.LightingEnabled = true;
                    //effect.EnableDefaultLighting();
                    //effect.PreferPerPixelLighting = true;
                    effect.World = world;
                    effect.View = camera.view;
                    effect.Projection = camera.projection;
                    //effect.DiffuseColor = this.Glow;
                    effect.AmbientLightColor = this.Color;
                 }
                 mesh.Draw();
             }
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
