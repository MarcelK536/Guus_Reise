using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Guus_Reise.HexangonMap
{
    class CharakterAnimation
    {
        private Vector3 _charakterPostion; //Position des Charakters
        Vector3 translation = new Vector3(-0.3f, 0.1f, 0f); // Verschiebung des Charakters Ausgehend vom Hex
        private Vector3 _charakterScale = new Vector3(0.002f, 0.002f, 0.002f); //Skaliserung des Charakters;
        Hex _hexagon;

        public CharakterAnimation(Hex hexagon)
        {
            _hexagon = hexagon;
            _charakterPostion = hexagon.Position + translation;
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

        public void Update()
        {
            _charakterPostion = _hexagon.Position + translation;
        }





    }
}
