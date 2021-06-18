
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Guus_Reise
{
    class Tile
    {
        private string _type;                   //Name des Tiletypen bspw. Wald
        private Model _tile;                    //3D Model
        private Vector3 _glow;                  
        private Vector3 _color;                 
        private Matrix _world;
        private float _begehbarkeit;            //wieviel das Tile von der Bewegungsreichweite abzieht

        public string Type
        {
            get => _type;
            set => _type = value;
        }
        
        public Model Tile1
        {
            get => _tile;
            set => _tile = value;
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
        public Matrix World
        {
            get => _world;
            set => _world = value;
        }
        public float Begehbarkeit
        {
            get => _begehbarkeit;
            set => _begehbarkeit = value;
        }
        public Tile(Vector3 position, int type, ContentManager contentmanager)
        {
            this.Glow = new Vector3(0.1f, 0.1f, 0.1f);
            this.Color = new Vector3(0, 0, 0);
            switch (type)
            {

                case 1: this.Tile1 = contentmanager.Load<Model>("TileModels\\TestModellObjects");
                    this.Type = "Wald";
                    this.Begehbarkeit = 2;
                    break;
                case 2: this.Tile1 = contentmanager.Load<Model>("TileModels\\TestModellObjects");
                    this.Type = "Berg";
                    this.Begehbarkeit = 2.5f;
                    break;
                case 3: this.Tile1 = contentmanager.Load<Model>("TileModels\\TestModellObjects");
                    this.Type = "Straße";
                    this.Begehbarkeit = 0.5f;
                    break;
                default: this.Tile1 = contentmanager.Load<Model>("TileModels\\TestModellObjects");
                    this.Type = "Wiese";
                    this.Begehbarkeit = 1;
                    break;
            }
            this.World = (Matrix.CreateScale(0.001f, 0.001f, 0.001f) * Matrix.CreateRotationY(45) * Matrix.CreateTranslation(position));

        }


        public void Draw(Camera camera)
        {
            foreach (var mesh in _tile.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.DiffuseColor = this.Glow;
                    effect.LightingEnabled = true;
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.World = this.World;
                    effect.View = camera.view;
                    effect.Projection = camera.projection;
                    effect.AmbientLightColor = this.Color;
                }
                mesh.Draw();
            }
        }
    }
}
