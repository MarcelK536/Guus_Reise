
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
        private bool _isglowing;



        #region Colors
        Vector4 greene = new Vector4(0f, 0.6f, 0f, 1);
        Vector4 greye = new Vector4(0.5f, 0.5f, 0.5f, 1);
        Vector4 rede = new Vector4(0.7f, 0f, 0f, 1);

        Vector4 lightgreye = new Vector4(0.9f, 0.9f, 0.9f, 1);
        Vector4 lightgreene = new Vector4(0f, 0.9f, 0f, 1);
        #endregion



        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public bool isglowing
        {
            get => _isglowing;
            set => _isglowing = value;
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

            switch (type)
            {

                case 1: this.Tile1 = contentmanager.Load<Model>("TileModels\\hexagonWald");
                    this.Type = "Wald";
                    this.Begehbarkeit = 2f;
                    break;
                
                case 2: this.Tile1 = contentmanager.Load<Model>("TileModels\\hexagonBerg");
                    this.Type = "Berg";
                    this.Begehbarkeit = 10f;
                    break;
                case 3: this.Tile1 = contentmanager.Load<Model>("TileModels\\hexagonWueste");
                    this.Type = "Wueste";
                    this.Begehbarkeit = 1f;
                    break;
                case 4:
                    this.Tile1 = contentmanager.Load<Model>("TileModels\\hexagonWasser");
                    this.Type = "Wueste";
                    this.Begehbarkeit = 1f;
                    break;
                case 5:
                    this.Tile1 = contentmanager.Load<Model>("TileModels\\hexagonWald2");
                    this.Type = "Wueste";
                    this.Begehbarkeit = 1f;
                    break;
                case 6:
                    this.Tile1 = contentmanager.Load<Model>("TileModels\\hexagonBerg2");
                    this.Type = "Wald";
                    this.Begehbarkeit = 2f;
                    break;
                case 7:
                    this.Tile1 = contentmanager.Load<Model>("TileModels\\hexagonWueste2");
                    this.Type = "Wueste";
                    this.Begehbarkeit = 1f;
                    break;
                case 8:
                    this.Tile1 = contentmanager.Load<Model>("TileModels\\hexagonWasser");
                    this.Type = "Wald";
                    this.Begehbarkeit = 2f;
                    break;
                default: this.Tile1 = contentmanager.Load<Model>("TileModels\\hexagonWasser");
                    this.Type = "Wasser";
                    this.Begehbarkeit = 4f;
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
                    effect.AmbientLightColor = new Vector3(0.1f,0.1f,0.1f);
                    if(this.Color.X == 0 || this.Color.Y == 0 || this.Color.Z == 0)
                    {
                        effect.AmbientLightColor = this.Color;
                    }

                }
                mesh.Draw();
            }
        }
    }
}
