using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class Charakter
    {
        private String _name;
        private int _widerstandskraft;
        private int _körperkraft;
        private int _abwehr;
        private int _wortgewandheit;
        private int _ingoranz;
        private int _geschwindigkeit;
        private int _glück;
        private int _bewegungsreichweite;

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

        public int Körperkraft
        {
            get => _körperkraft;
            set => _körperkraft = value;
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

        public int Glück
        {
            get => _glück;
            set => _glück = value;
        }

        public int Bewegungsreichweite
        {
            get => _bewegungsreichweite;
            set => _bewegungsreichweite = value;
        }

        public Charakter (String name, int leben, int angriff, int abwehr, int wortgewand, int ignoranz, int geschwindigkeit, int glück, int bewegungsreichweite)
        {
            this.Name = name;
            this.Widerstandskraft = leben;
            this.Körperkraft = angriff;
            this.Abwehr = abwehr;
            this.Wortgewandheit = wortgewand;
            this.Ignoranz = ignoranz;
            this.Geschwindigkeit = geschwindigkeit;
            this.Glück = glück;
            this.Bewegungsreichweite = bewegungsreichweite;
        }
    }
}
