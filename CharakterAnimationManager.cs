using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Guus_Reise.HexangonMap;

namespace Guus_Reise
{
    class CharakterAnimationManager
    {
        static List<string> charakterNames;
        static List<string> animations = new List<string> { "Idle", "moveLeft", "moveRight", "moveFront", "moveBack", "readyToFight" };
        static CharakterAnimation[] charakterAnimations;

        static bool _activeHexExists = false;
        // (Idle, moveLeft, moveRight, moveFront, moveBack, readyToFight)

        public static bool ActiveHexExists
        {
            get => _activeHexExists;
            set => _activeHexExists = value;
        }

        public static void Init(ContentManager content)
        {
            charakterNames = new List<string> {"Guu", "Timmae", "Peter", "Paul" };
            charakterAnimations = new CharakterAnimation[charakterNames.Count];
            string curPath;
            string path;
            int numberOfFrames;
            DirectoryInfo di;
            Model planeModel = content.Load<Model>("Charakter\\plane");


            foreach (string name in charakterNames)
            {
                path = "Content\\Charakter\\" + name;
                Texture2D texCharakter = content.Load<Texture2D>("Charakter\\"+name+"\\tex"+name);

                List<Texture2D> idle = new List<Texture2D>();
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

                //moveLeft
                //moveRight
                //...

                charakterAnimations[indexCharakter] = new CharakterAnimation(planeModel, texCharakter, idle);  
            }
        }

        public static CharakterAnimation GetCharakterAnimation(string name)
        {
            int index = charakterNames.IndexOf(name);
            return charakterAnimations[index];
        }
    }
}
