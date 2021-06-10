﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Guus_Reise.HexangonMap
{
    class CharakterAnimation
    {
        private Vector3 _charakterPostion; //Position des Charakters
        
        Vector3 translation = new Vector3(-0.3f, 0.1f, 0f); // Verschiebung des Charakters Ausgehend vom Hex
        private Vector3 _charakterScale = new Vector3(0.002f, 0.002f, 0.002f); //Skaliserung des Charakters;
        
        Hex _hexagon;
        Charakter _charakter;
        
        private static Model _model;
        private static Texture2D texCharakter;

        private Vector3 _glow;
        private Vector3 _color;

        public CharakterAnimation(Hex hexagon, Charakter charakter)
        {
            _hexagon = hexagon;
            _charakterPostion = hexagon.Position + translation;
            this.Glow = new Vector3(0.1f, 0.1f, 0.1f);
            this.Color = new Vector3(0, 0, 0);
        }

        public Hex Hexagon
        {
            get => _hexagon;
            set => _hexagon = value;
        }

        public Vector3 CharakterScale
        {
            get => _charakterScale;
            set => _charakterScale = value;
        }


        public Vector3 CharakterPostion
        {
            get => _charakterPostion;
            set => _charakterPostion = value;
        }

        public Charakter Charakter
        {
            get => _charakter;
            set => _charakter = value;
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

        public static void LoadContent(ContentManager content, SpriteBatch spriteBatch)
        {
            _model = content.Load<Model>("Charakter\\plane");
            texCharakter = content.Load<Texture2D>("Charakter\\texTimmae");

        }

        public void DrawCharakter(Camera camera)
        {
            Matrix world = (Matrix.CreateScale(_charakterScale) * Matrix.CreateRotationX(45) * Matrix.CreateTranslation(_charakterPostion));
            foreach (var mesh in _model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.TextureEnabled = true;
                    effect.Texture = texCharakter;
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

        public void Update()
        {
            _charakterPostion = _hexagon.Position + translation;
        }

        public void Play()
        {

        }





    }
}
