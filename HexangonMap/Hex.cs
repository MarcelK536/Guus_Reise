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

        public Hex(Vector3 position, Point logicalPosition, Tile tile)
        {
            this.BoardPosition = position;
            this.LogicalBoardPosition = logicalPosition;
            this.Tile = tile;
        }
        public void Draw(Camera camera)
        {
            if(Game1.GState == Game1.GameState.InFight)
            {
                Position = FightPosition;
                LogicalPosition = LogicalFightPosition;
            }
            else
            {
                Position = BoardPosition;
                LogicalPosition = LogicalBoardPosition;
            }
            this.Tile.World = (Matrix.CreateScale(0.001f, 0.001f, 0.001f) * Matrix.CreateRotationY(45) * Matrix.CreateTranslation(Position));
            this.Tile.Draw(camera);
        }

    }
}
