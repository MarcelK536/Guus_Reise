using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Guus_Reise
{
    class Tile
    {
        private string _type;                   //Name des Tiletypen bspw. Wald
        private Vector3 _position;              //Position im Raum
        private Point _logicalPosition;         //Position im _board Array
        private Charakter _charakter;           //Platzhalter für Charakter, falls einer auf dem Tile steht
        private Model _tile;                    //3D Model
        private Vector3 _glow;                  
        private Vector3 _color;                 
        private Matrix _world;

        public string Type
        {
            get => _type;
            set => _type = value;
        }
        public Vector3 Position
        {
            get => _position;
            set => _position = value;
        }
        public Point LogicalPosition
        {
            get => _logicalPosition;
            set => _logicalPosition = value;
        }
        public Charakter Charakter
        {
            get => _charakter;
            set => _charakter = value;
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

        public Tile(Vector3 position, Point logicalposition, int type, ContentManager contentmanager)
        {
            this.LogicalPosition = logicalposition;
            this.Glow = new Vector3(0.1f, 0.1f, 0.1f);
            this.Color = new Vector3(0, 0, 0);
            switch (type)
            {
                case 1: this.Tile1 = contentmanager.Load<Model>("hexagonWald");
                    this.Type = "Wald";
                    position = position + new Vector3(1, 0, -1.5f);
                    break;
                case 2: this.Tile1 = contentmanager.Load<Model>("hexagonBerg");
                    this.Type = "Berg";
                    position = position + new Vector3(2.18f, 0, -4.5f);
                    break;
                case 3: this.Tile1 = contentmanager.Load<Model>("hexagonStraße");
                    this.Type = "Straße";
                    position = position + new Vector3(5.4f, 0, -2.4f);
                    break;
                default: this.Tile1 = contentmanager.Load<Model>("hexagonWiese");
                    this.Type = "Wiese";
                    break;
            }

            this.Position = position;
            this.World = Matrix.CreateScale(0.53f, 0.2f, 0.5f) * Matrix.CreateTranslation(this.Position);
        }

        public void Draw(Camera camera)
        {
            foreach (var mesh in _tile.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.TextureEnabled = false;
                    effect.LightingEnabled = true;
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.World = this.World;
                    effect.View = camera.view;
                    effect.Projection = camera.projection;
                    effect.DiffuseColor = this.Glow;
                    effect.AmbientLightColor = this.Color;
                }
                mesh.Draw();
            }
        }
    }
}
