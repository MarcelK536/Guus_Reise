using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Guus_Reise
{
    class Weapon
    {
        public static List<Weapon> weapons = new List<Weapon>();

        private String _name;
        private bool _isHiebwaffe;
        private bool _isStoßwaffe;
        private bool _isKlingenwaffe;
        private bool _isStumpf;

        private int _baseSchaden;
        private int _baseGeschwindigkeit;
        private int _baseKrit;

        private char _scalingKK;
        private char _scalingBW;
        private char _scalingWG;
        private char _scalingLS;

        private int _minKK;
        private int _minBW;
        private int _minWG;
        private int _minLS;

        public String Name
        {
            get => _name;
            set => _name = value;
        }
        public bool IsHiebwaffe
        {
            get => _isHiebwaffe;
            set => _isHiebwaffe = value;
        }
        public bool IsStoßwaffe
        {
            get => _isStoßwaffe;
            set => _isStoßwaffe = value;
        }
        public bool IsKligenwaffe
        {
            get => _isKlingenwaffe;
            set => _isKlingenwaffe = value;
        }
        public bool IsStumpf
        {
            get => _isStumpf;
            set => _isStumpf = value;
        }
        public int BaseSchaden
        {
            get => _baseSchaden;
            set => _baseSchaden = value;
        }
        public int BaseGeschwindigkeit
        {
            get => _baseGeschwindigkeit;
            set => _baseGeschwindigkeit = value;
        }
        public int BaseKrit
        {
            get => _baseKrit;
            set => _baseKrit = value;
        }
        public char ScalingKK
        {
            get => _scalingKK;
            set => _scalingKK = value;
        }
        public char ScalingBW
        {
            get => _scalingBW;
            set => _scalingBW = value;
        }
        public char ScalingWG
        {
            get => _scalingWG;
            set => _scalingWG = value;
        }
        public char ScalingLS
        {
            get => _scalingLS;
            set => _scalingLS = value;
        }
        public int MinKK
        {
            get => _minKK;
            set => _minKK = value;
        }
        public int MinBW
        {
            get => _minBW;
            set => _minBW = value;
        }
        public int MinWG
        {
            get => _minWG;
            set => _minWG = value;
        }
        public int MinLS
        {
            get => _minLS;
            set => _minLS = value;
        }

        public Weapon(String name, int[] stats)
        {
            this.Name = name;
            this.IsHiebwaffe = IntToAttribute(stats[0]);
            this.IsStoßwaffe = IntToAttribute(stats[1]);
            this.IsKligenwaffe = IntToAttribute(stats[2]);
            this.IsStumpf = IntToAttribute(stats[3]);
            this.BaseSchaden = stats[4];
            this.BaseGeschwindigkeit = stats[5];
            this.BaseKrit = stats[6];
            this.ScalingKK = IntToScaling(stats[7]);
            this.ScalingBW = IntToScaling(stats[8]);
            this.ScalingWG = IntToScaling(stats[9]);
            this.ScalingLS = IntToScaling(stats[10]);
            this.MinKK = stats[11];
            this.MinBW = stats[12];
            this.MinWG = stats[13];
            this.MinLS = stats[14];
        }

        public static void LoadWeapons(ContentManager Content)
        {
            weapons.Add(new Weapon("Faust", new int[] { 0, 0, 0, 0, 5, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0 }));
            weapons.Add(new Weapon("Messer", new int[] { 0, 0, 0, 0, 5, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0 }));
            weapons.Add(new Weapon("Aliestole", new int[] { 0, 0, 0, 0, 5, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0 }));
        }

        private bool IntToAttribute(int zahl)
        {
            bool hilf;
            if(zahl == 0)
            {
                hilf = false;
            }
            else
            {
                hilf = true;
            }
            return hilf;
        }

        private char IntToScaling(int zahl)
        {
            char hilf;
            switch (zahl) {
                case 0:
                    hilf = 'O';
                    break;
                case 1:
                    hilf = 'E';
                    break;
                case 2:
                    hilf = 'D';
                    break;
                case 3:
                    hilf = 'C';
                    break;
                case 4:
                    hilf = 'B';
                    break;
                case 5:
                    hilf = 'A';
                    break;
                case 6:
                    hilf = 'S';
                    break;
                default:
                    hilf = 'O';
                    break;
            }
            return hilf;
        }
        public static float IntToScale(int zahl)
        {
            float hilf;
            switch (zahl)
            {
                case 0:
                    hilf = 0.0f;
                    break;
                case 1:
                    hilf = 0.1f;
                    break;
                case 2:
                    hilf = 0.25f;
                    break;
                case 3:
                    hilf = 0.5f;
                    break;
                case 4:
                    hilf = 0.75f;
                    break;
                case 5:
                    hilf = 1.0f;
                    break;
                case 6:
                    hilf = 1.5f;
                    break;
                default:
                    hilf = 0.0f;
                    break;
            }
            return hilf;
        }
    }
}
