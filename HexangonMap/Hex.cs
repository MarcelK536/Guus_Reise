using Microsoft.Xna.Framework;

namespace Guus_Reise
{
    class Hex
    {
        private Vector3 _position;              //Position im Raum
        private Point _logicalPosition;         //Position im _board Array
        private Tile _tile;
        private Charakter _charakter;

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
        public Tile Tile
        {
            get => _tile;
            set => _tile = value;
        }
        public Charakter Charakter
        {
            get => _charakter;
            set => _charakter = value;
        }

        public Hex(Vector3 position, Point logicalPosition, Tile tile)
        {
            this.Position = position;
            this.LogicalPosition = logicalPosition;
            this.Tile = tile;
        }

        public void Draw(Camera camera)
        {
            this.Tile.Draw(camera);
            if (this.Charakter != null) //wenn auf dem Tile ein Charakter ist soll dieser auch dargestellt werden
            {
                Vector3 hilf = new Vector3(0.0f, 0.005f, 0.0f);
                Matrix hilf2 = (Matrix.CreateScale(1, 1, 1) * Matrix.CreateRotationY(45) * Matrix.CreateTranslation(this.Position + hilf));
                this.Charakter.Draw(camera, hilf2);
            }
        }
    }
}
