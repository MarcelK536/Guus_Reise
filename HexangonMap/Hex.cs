using Microsoft.Xna.Framework;

namespace Guus_Reise
{
    class Hex
    {
        private Vector3 _position;              //Position im Raum
        private Point _logicalPosition;         //Position im _board Array

        private Vector3 _boardPosition;
        private Point _logicalBoardPosition;

        private Vector3 _fightPosition;
        private Point _logicalFightPosition;
        private int _tileRotation = 45; //Darf nur 0,45,90,135,180 sein
        public static readonly int[] possibleRotations = { 0, 45, 135, 180 };

        private Tile _tile;
        private Charakter _charakter;
        bool _isHovered = false;
        bool _isActive = false;

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

        public Vector3 FightPosition
        {
            get => _fightPosition;
            set => _fightPosition = value;
        }
        public Point LogicalFightPosition
        {
            get => _logicalFightPosition;
            set => _logicalFightPosition = value;
        }
        public Vector3 BoardPosition
        {
            get => _boardPosition;
            set => _boardPosition = value;
        }
        public Point LogicalBoardPosition
        {
            get => _logicalBoardPosition;
            set => _logicalBoardPosition = value;
        }

        public bool IsHovered 
        {
            get => _isHovered;
            set => _isHovered = value;
        }

        public bool IsActive 
        {
            get => _isActive;
            set => _isActive = value;
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
        public int TileRotation { get => _tileRotation; set => _tileRotation = value; }

        public Hex(Vector3 position, Point logicalPosition, Tile tile)
        {
            this.BoardPosition = position;
            this.LogicalBoardPosition = logicalPosition;
            this.LogicalPosition = this.LogicalBoardPosition;
            this.Tile = tile;
        }
        public Hex(Vector3 position,int rotation, Point logicalPosition, Tile tile)
        {
            TileRotation = rotation;
            BoardPosition = position;
            LogicalBoardPosition = logicalPosition;
            LogicalPosition = LogicalBoardPosition;
            Tile = tile;
        }

        public Hex Clone()
        {
            Hex clone = new Hex(this.Position, this.TileRotation, this.LogicalPosition, this.Tile);
            clone.Charakter = this.Charakter;
            clone.BoardPosition = this.BoardPosition;
            clone.LogicalPosition = this.LogicalPosition;
            clone.Position = this.Position;
            clone.IsHovered = this.IsHovered;
            clone.IsActive = this.IsActive;

            return clone;
        }

        public void Draw(Camera camera)
        {
            if(Game1.GState == Game1.GameState.InFight || Game1.GState == Game1.GameState.InTalkFight)
            {
                Position = FightPosition;
                LogicalPosition = LogicalFightPosition;
            }
            else
            {
                Position = BoardPosition;
                LogicalPosition = LogicalBoardPosition;
            }
            this.Tile.World = (Matrix.CreateScale(0.001f, 0.001f, 0.001f) * Matrix.CreateRotationY(this.TileRotation) * Matrix.CreateTranslation(Position));
            this.Tile.Draw(camera);
        }

    }
}
