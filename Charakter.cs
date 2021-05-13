using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Guus_Reise
{
    class Charakter
    {
        private String _name;
        private int _widerstandskraft; //Leben des Charakters
        private int _koerperkraft;      //physichen Angriff
        private int _abwehr;            //physiche Abwehr
        private int _wortgewandheit;    //social Angriff
        private int _ingoranz;          //social Abwehr
        private int _geschwindigkeit;   //geschwindigkeit des Charakters im Kamof
        private int _glueck;            //wirkt sich auf kritische trefferchance aus
        private int _bewegungsreichweite; 
        private Model _model;

        public String Name
        {
            get => _name;
            set => _name = value;
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

        public int Bewegungsreichweite
        {
            get => _bewegungsreichweite;
            set => _bewegungsreichweite = value;
        }

        public Model Model
        {
            get => _model;
            set => _model = value;
        }

        public Charakter (String name, int leben, int angriff, int abwehr, int wortgewand, int ignoranz, int geschwindigkeit, int glück, int bewegungsreichweite)
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
        }

        public Charakter (String name, int[] werte)
        {
            this.Name = name;
            this.Widerstandskraft = werte[0];
            this.Koerperkraft = werte[1];
            this.Abwehr = werte[2];
            this.Wortgewandheit = werte[3];
            this.Ignoranz = werte[4];
            this.Geschwindigkeit = werte[5];
            this.Glueck = werte[6];
            this.Bewegungsreichweite = werte[7];
        }

        public void Draw(Camera camera, Matrix world)
        {
            //TODO
        }
    }
}
