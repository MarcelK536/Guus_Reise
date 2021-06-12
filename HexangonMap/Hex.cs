using Microsoft.Xna.Framework;

namespace Guus_Reise
{
    class Hex
    {
        private Vector3 _position;              //Position im Raum
        private Point _logicalPosition;         //Position im _board Array
        private Tile _tile;
        private Charakter _charakter;
        bool _isHovered = false;

        public Vector3 Position
        {
            get => _position;
            set => _position = value;
        }

        public bool IsHovered
        {
            get => _isHovered;
            set => _isHovered = value;
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
        }

    }
}
