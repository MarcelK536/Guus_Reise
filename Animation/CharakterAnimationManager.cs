using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Guus_Reise.HexangonMap;
using Microsoft.Xna.Framework.Audio;


namespace Guus_Reise.Animation
{
    class CharakterAnimationManager
    {
        static List<string> charakterNames;
        static List<string> animations = new List<string> { "Idle", "moveLeft", "moveRight", "moveFront", "moveBack", "readyToFight" };
        static CharakterAnimation[] charakterAnimations;
        public static SoundManager _sm;


        public static bool animationSound;

        static bool _activeHexExists = false;
        // (Idle, moveLeft, moveRight, moveFront, moveBack, readyToFight)

        public static bool ActiveHexExists
        {
            get => _activeHexExists;
            set => _activeHexExists = value;
        }


        public static CharakterAnimation[] CharakterAnimations
        {
            get => charakterAnimations;
            set => charakterAnimations = value;
        }

        public static void Init(ContentManager content)
        {
            charakterNames = new List<string> {"Guu", "Bully", "Heavyweight", "Preacher", "Politician", "Assassin", "Daydreamer", "old_man", "average_joe" };
            charakterAnimations = new CharakterAnimation[charakterNames.Count];

            animationSound = Game1.defaultValueSoundOn;




            _sm = new SoundManager();
            _sm.Load(content);


            string curPath;
            string path;
            int numberOfFrames;
            DirectoryInfo di;
            Model planeModel = content.Load<Model>("Charakter\\plane");

            float standardIntervall = 100f;
            foreach (string name in charakterNames)
            {
                path = "Content\\Charakter\\" + name;
                Texture2D texCharakter = content.Load<Texture2D>("Charakter\\"+name+"\\tex"+name);

                List<Texture2D> idle = new List<Texture2D>();
                List<Texture2D> jump = new List<Texture2D>();
                List<Texture2D> walkLeft = new List<Texture2D>();
                List<Texture2D> walkRight = new List<Texture2D>();
                List<Texture2D> fightWeapons = new List<Texture2D>();

                //...

                int indexCharakter = charakterNames.IndexOf(name);

                //Idle
                curPath = path + "\\Idle";
                di = new DirectoryInfo(curPath);
                numberOfFrames = di.GetFiles().Length;
                
                for (int i = 0; i < numberOfFrames; i++)
                {
                    string number = (i+1).ToString();
                    Texture2D curr = content.Load<Texture2D>("Charakter\\"+name+"\\Idle\\" + name + "Idle" + number);
                    idle.Add(curr);
                }

                //Jump

                curPath = path + "\\Jump";
                di = new DirectoryInfo(curPath);
                numberOfFrames = di.GetFiles().Length;

                for (int i = 0; i < numberOfFrames; i++)
                {
                    string number = (i + 1).ToString();
                    Texture2D curr = content.Load<Texture2D>("Charakter\\" + name + "\\Jump\\" + name + "Jump" + number);
                    jump.Add(curr);
                }

                //WalkLeft
                curPath = path + "\\WalkLeft";
                di = new DirectoryInfo(curPath);
                numberOfFrames = di.GetFiles().Length;

                for (int i = 0; i < numberOfFrames; i++)
                {
                    string number = (i + 1).ToString();
                    Texture2D curr = content.Load<Texture2D>("Charakter\\" + name + "\\WalkLeft\\" + name + "WalkLeft" + number);
                    walkLeft.Add(curr);
                }

                //WalkRight
                curPath = path + "\\WalkRight";
                di = new DirectoryInfo(curPath);
                numberOfFrames = di.GetFiles().Length;

                for (int i = 0; i < numberOfFrames; i++)
                {
                    string number = (i + 1).ToString();
                    Texture2D curr = content.Load<Texture2D>("Charakter\\" + name + "\\WalkRight\\" + name + "WalkRight" + number);
                    walkRight.Add(curr);
                }

                if (name == "Paul")
                {
                    standardIntervall = 250f;
                }

                //FightWeapons
                curPath = path + "\\FightWeapon";
                di = new DirectoryInfo(curPath);
                numberOfFrames = di.GetFiles().Length;

                for (int i = 0; i < numberOfFrames; i++)
                {
                    string number = (i + 1).ToString();
                    Texture2D curr = content.Load<Texture2D>("Charakter\\" + name + "\\FightWeapon\\" + name + "FightWeapon" + number);
                    fightWeapons.Add(curr);
                }


                if (name == "Paul")
                {
                    standardIntervall = 250f;
                }

                //moveLeft
                //moveRight
                //...


                charakterAnimations[indexCharakter] = new CharakterAnimation(planeModel, texCharakter, idle, jump, walkLeft, walkRight, fightWeapons, standardIntervall, _sm);  
            }
        }

        public static CharakterAnimation GetCharakterAnimation(string name)
        {
            int index = charakterNames.IndexOf(name);
            return charakterAnimations[index];
        }
    }
}
