
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
        private Effect _shader;
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
            this.Color = new Vector3(0, 0, 0);
            this._shader = contentmanager.Load<Effect>("LightShader");
            switch (type)
            {

                case 1: this.Tile1 = contentmanager.Load<Model>("TileModels\\hexagonWald");
                    this.Type = "Wald";
                    this.Begehbarkeit = 2;
                    break;
                case 2: this.Tile1 = contentmanager.Load<Model>("TileModels\\hexagonBerg");
                    this.Type = "Berg";
                    this.Begehbarkeit = 2.5f;
                    break;
                case 3: this.Tile1 = contentmanager.Load<Model>("TileModels\\hexagonLeer");
                    this.Type = "Straße";
                    this.Begehbarkeit = 0.5f;
                    break;
                default: this.Tile1 = contentmanager.Load<Model>("TileModels\\hexagonTerrain2");
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

        public void DrawShader(Camera camera, bool ishoverd, bool isactiv)
        {

            float glowe = Glow.Length()*2;

            
            //Vector4 colore = new Vector4(glowe*Color.X, glowe * Color.Y, glowe * Color.Z, 1);
            Vector4 colore = new Vector4(Color, 1);


            foreach (var mesh in _tile.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = _shader;
                  
                    _shader.Parameters["World"].SetValue(_world);
                    _shader.Parameters["View"].SetValue(camera.view);
                    _shader.Parameters["Projection"].SetValue(camera.projection);


                    if (ishoverd)
                    {
                        _shader.Parameters["Color"].SetValue(new Vector4(1,1,1,1));
                    }
                    else if (_isglowing)
                    {
                        _shader.Parameters["Color"].SetValue(colore);
                    }
                    else
                    {
                        if (this.Type == "Wald")
                        {
                            _shader.Parameters["Color"].SetValue(greene);
                        }
                        else if (this.Type == "Berg")
                        {
                            _shader.Parameters["Color"].SetValue(greye);
                        }
                        else if (this.Type == "Wiese")
                        {
                            _shader.Parameters["Color"].SetValue(greene);
                        }
                        else if (this.Type == "Straße")
                        {
                            _shader.Parameters["Color"].SetValue(greye);
                        }
                        else
                        {
                            _shader.Parameters["Color"].SetValue(rede);
                        }


                    }

                }
                mesh.Draw();
              
            }
        }
    }
}
