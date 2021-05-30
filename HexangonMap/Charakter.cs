﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;


namespace Guus_Reise
{
    class Charakter
    {
        private String _name;
        private bool _npc;
        private bool _canMove;
        private int _level;
        private int _xp;
        private int _widerstandskraft; //Leben des Charakters
        private int _koerperkraft;      //physichen Angriff
        private int _abwehr;            //physiche Abwehr
        private int _wortgewandheit;    //social Angriff
        private int _ingoranz;          //social Abwehr
        private int _geschwindigkeit;   //geschwindigkeit des Charakters im Kamof
        private int _glueck;            //wirkt sich auf kritische trefferchance aus
        private int _bewegungsreichweite;
        private int _fpunkte;
        private Point _logicalPosition;
        private static Model _model;
        private Vector3 _glow;
        private Vector3 _color;
       // private Vector2 charakterScale = new Vector2(1.5f, 1.5f);
       // private Vector3 pos;

        static AnimatedSprite spriteCharakter;
        private static SpriteBatch _spriteBatch;

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
            if(werte[8] == 0)
            {
                this.IsNPC = false;
            }
            if (werte[8] == 1)
            {
                this.IsNPC = true;
            }
            this.Fähigkeitspunkte = 4;
            this.Glow = new Vector3(0.1f, 0.1f, 0.1f);
            this.Color = new Vector3(0, 0, 0);
        }

        public static void LoadContent(ContentManager content, SpriteBatch spriteBatch)
        {
            SpriteSheet spritesheet;
            spritesheet = content.Load<SpriteSheet>("Charakter\\Guu.json", new JsonContentLoader());
            spriteCharakter = new AnimatedSprite(spritesheet);
            spriteCharakter.Play("walk_left");
            _spriteBatch = spriteBatch;
            _model = content.Load<Model>("blueCube2");
        }

        public void Draw(Camera camera, Matrix world)
        {
            foreach (var mesh in _model.Meshes)
             {
                 foreach (BasicEffect effect in mesh.Effects)
                 {
                     effect.TextureEnabled = true;
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

        //Draws the Button, Needs the .Begin and .End function in the Class to function
        //public void Draw()
        //{
        //    _spriteBatch.Begin();
        //    _spriteBatch.Draw(spriteCharakter, pos, 0,charakterScale);
        //    _spriteBatch.End();
        //}

        public void Update(GameTime gameTime)
        {
            // Play Animation
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            spriteCharakter.Play("walk_left");
            spriteCharakter.Update(deltaSeconds);
        }

        public void GainXp(Charakter winner, Charakter looser)
        {
            int hilf = ((looser.Level - winner.Level) * 3) + 30;
            
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
