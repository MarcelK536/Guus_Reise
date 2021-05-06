using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Guus_Reise
{
    class Tile
    {
        private string _type;
        private Vector3 _position;
        private Point _logicalPosition;
        private Charakter _charakter;
        private Model _tile;
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
            this.Position = position;
            this.LogicalPosition = logicalposition;
            this.Glow = new Vector3(0.1f, 0.1f, 0.1f);
            this.Color = new Vector3(0, 0, 0);
            this.World = Matrix.CreateScale(0.5f, 0.2f, 0.5f)* Matrix.CreateTranslation(this.Position);
            switch (type)
            {
                case 1: this.Tile1 = contentmanager.Load<Model>("Wald");
                    this.Type = "Wald";
                    break;
                case 2: this.Tile1 = contentmanager.Load<Model>("Berg");
                    this.Type = "Berg";
                    break;
                case 3: this.Tile1 = contentmanager.Load<Model>("Straße");
                    this.Type = "Straße";
                    break;
                default: this.Tile1 = contentmanager.Load<Model>("Wiese");
                    this.Type = "Wiese";
                    break;
            }
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
