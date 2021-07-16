using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Guus_Reise.Fight
{
    class Skill
    {
        public static List<Skill> skills = new List<Skill>();

        private String _name;
        private int _geschwindigkeit;
        private float _movevalue;

        public String Name
        {
            get => _name;
            set => _name = value;
        }
        public int Geschwindigkeit
        {
            get => _geschwindigkeit;
            set => _geschwindigkeit = value;
        }
        public float MoveValue
        {
            get => _movevalue;
            set => _movevalue = value;
        }

        public Skill(String name, int geschwindigkeit, float moveValue)
        {
            this.Name = name;
            this.Geschwindigkeit = geschwindigkeit;
            this.MoveValue = moveValue;
        }

        public static void LoadSkills(ContentManager Content)
        {
            skills.Add(new Skill("Hieb", 10, 0.7f));
            skills.Add(new Skill("Stoß", 8, 0.6f));
        }
    }
}
