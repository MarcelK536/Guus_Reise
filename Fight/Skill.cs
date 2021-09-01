using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Guus_Reise
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
            skills.Add(new Skill("blow", 10, 0.7f));
            skills.Add(new Skill("shout", 10, 0.7f));

            skills.Add(new Skill("push", 8, 0.6f));           
            skills.Add(new Skill("persuade", 8, 0.6f));

            skills.Add(new Skill("disarm", 10, 0.7f));
            skills.Add(new Skill("pastoral care", 10, 0.7f));

            skills.Add(new Skill("focus", 8, 0.6f));
            skills.Add(new Skill("airhead", 8, 0.6f));

            skills.Add(new Skill("targeted stitch", 8, 0.6f));
            skills.Add(new Skill("lie", 8, 0.6f));

            skills.Add(new Skill("sweeping blow", 8, 0.6f));
            skills.Add(new Skill("sing", 8, 0.6f));

            skills.Add(new Skill("death blow", 15, 1.0f));
            skills.Add(new Skill("bribe", 15, 1.0f));
        }
    }
}
