﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Guus_Reise.HexangonMap
{
    class CharakterAnimation
    {
        private static KeyboardState _prevKeyState;
        private Vector3 _charakterPostion; //Position des Charakters
        
        Vector3 translation = new Vector3(-0.3f, 0.1f, 0f); // Verschiebung des Charakters Ausgehend vom Hex
        private Vector3 _charakterScale = new Vector3(0.002f, 0.002f, 0.002f); //Skaliserung des Charakters;
        //static List<string> animations = new List<string> { "Idle", "moveLeft", "moveRight", "moveFront", "moveBack", "readyToFight" };

        private List<Texture2D> idle;
        //static List<Texture2D> moveLeft;
        //static List<Texture2D> moveBack;
        //static List<Texture2D> moveRight;
        //static List<Texture2D> moveFront;
        //static List<Texture2D> readyToFight;

        Hex _hexagon;
        Charakter _charakter;
        
        Model _planeModel;
        Texture2D _texCharakter;
        Texture2D _curTex;
        private List<Texture2D> currentAnimation;
        int currentFrame = 0;

        private Vector3 _glow;
        private Vector3 _color;

        float timer;
        float intervall = 100f;

        bool isPlayAnimation = false;

        public CharakterAnimation(Model planeModel, Texture2D texCharakter, List<Texture2D> animIdle)
        {
            idle = animIdle;
            _planeModel = planeModel;
            _texCharakter = texCharakter;
            _curTex = _texCharakter;

            // Set previous Keyboard State
            _prevKeyState = Keyboard.GetState();
        }



        public void SetParametersAfterInitCharakter(Charakter charakter, Hex hexagon)
        {
            _charakter = charakter;
            _hexagon = hexagon;
            _charakterPostion = hexagon.Position + translation;
            _glow = new Vector3(0.1f, 0.1f, 0.1f);
            _color = new Vector3(0, 0, 0);
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

        public void DrawCharakter(Camera camera)
        {
            Matrix world = (Matrix.CreateScale(_charakterScale) * Matrix.CreateRotationX(45) * Matrix.CreateTranslation(_charakterPostion));
            foreach (var mesh in _planeModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.TextureEnabled = true;
                    effect.Texture = _curTex;
                    //effect.LightingEnabled = true;
                    //effect.EnableDefaultLighting();
                    //effect.PreferPerPixelLighting = true;
                    effect.World = world;
                    effect.View = camera.view;
                    effect.Projection = camera.projection;
                    //effect.DiffuseColor = this.Glow;
                    effect.AmbientLightColor = this._color;
                }
                mesh.Draw();
            }
        }

        public void Update(GameTime gametime)
        {
            _charakterPostion = _hexagon.Position + translation;
            if(isPlayAnimation)
            {
                _curTex = currentAnimation[currentFrame];
                UpdateAnimation(gametime);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.P) && _prevKeyState.IsKeyUp(Keys.P))
            {
                if(isPlayAnimation)
                {
                    Stop();
                }
                else
                {
                    Play("Idle");
                }

            }
            _prevKeyState = Keyboard.GetState();
        }

        public void UpdateAnimation( GameTime gametime)
        {
            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds / 2;
            if(timer > intervall)
            {
                currentFrame++;
                timer = 0;
                if(currentFrame == currentAnimation.Count)
                {
                    currentFrame = 0;
                }
            }
        }

        public void Play(string nameAnimation)
        {
            if(nameAnimation == "")
            {
                throw new InvalidOperationException("Animation kann nicht null sein");
            }
            switch(nameAnimation)
            {
                case "Idle":
                    currentAnimation = idle;
                    break;
                default:
                    currentAnimation = idle;
                    break;
            }
            isPlayAnimation = true;
        }

        // Setzen der Texture wieder auf die Standardtextur
        public void Stop()
        {
            _curTex = _texCharakter;
            isPlayAnimation = false;
        }





    }
}
