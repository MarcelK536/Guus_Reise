using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Guus_Reise.HexangonMap
{
    class CharakterAnimation
    {
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

        private Vector3 _glow;
        private Vector3 _color;

        public CharakterAnimation(Model planeModel, Texture2D texCharakter, List<Texture2D> animIdle)
        {
            idle = animIdle;
            _planeModel = planeModel;
            _texCharakter = texCharakter;
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
                    effect.Texture = _texCharakter;
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

        public void Update()
        {
            _charakterPostion = _hexagon.Position + translation;
        }

        public void Play()
        {

        }





    }
}
