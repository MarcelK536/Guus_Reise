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

        private int _baseSchaden;
        private int _baseGeschwindigkeit;
        private int _baseKrit;

        private char _scalingKK;
        private char _scalingBW;
        private char _scalingWG;
        private char _scalingLS;

        public String Name
        {
            get => _name;
            set => _name = value;
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

        public Weapon(String name, int[] stats)
        {
            this.Name = name;
            this.BaseSchaden = stats[0];
            this.BaseGeschwindigkeit = stats[1];
            this.BaseKrit = stats[2];
            this.ScalingKK = IntToScaling(stats[3]);
            this.ScalingBW = IntToScaling(stats[4]);
            this.ScalingWG = IntToScaling(stats[5]);
            this.ScalingLS = IntToScaling(stats[6]);
        }

        public static void LoadWeapons(ContentManager Content)
        {
            weapons.Add(new Weapon("fist", new int[] { 5, 0, 0, 3, 1, 0, 0}));
            weapons.Add(new Weapon("knife", new int[] { 10, 0, 0, 3, 3, 0, 0}));
            weapons.Add(new Weapon("stick", new int[] { 10, 0, 0, 2, 4, 0, 0}));
            weapons.Add(new Weapon("boxing gloves", new int[] { 15, 0, 0, 6, 0, 0, 0}));
            weapons.Add(new Weapon("hammer", new int[] { 20, 0, 0, 5, 1, 0, 0}));
            weapons.Add(new Weapon("axe", new int[] { 20, 0, 0, 4, 2, 0, 0}));
            weapons.Add(new Weapon("katana", new int[] { 20, 0, 25, 0, 6, 0, 0}));

            weapons.Add(new Weapon("voice", new int[] { 5, 0, 0, 0, 0, 3, 2}));
            weapons.Add(new Weapon("megafone", new int[] { 10, 0, 0, 0, 0, 1, 5}));
            weapons.Add(new Weapon("smart book", new int[] { 10, 0, 0, 0, 0, 4, 2}));
            weapons.Add(new Weapon("universal translator", new int[] { 15, 0, 0, 0, 0, 6, 0}));
            weapons.Add(new Weapon("costume", new int[] { 15, 0, 0, 0, 0, 3, 3}));
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
