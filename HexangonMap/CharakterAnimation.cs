using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Guus_Reise.HexangonMap
{
    class CharakterAnimation
    {
        private Vector2 _charakterScale = new Vector2(1f, 1f);
        private Vector3 _cubePosition;
        Vector3 translation = new Vector3(0.0f, 0.25f, 0f);
        Hex _hexagon;

        public CharakterAnimation(Hex hexagon, Charakter charakter)
        {
            _hexagon = hexagon;
            _cubePosition = hexagon.Position + translation;
        }

        public Hex Hexagon
        {
            get => _hexagon;
            set => _hexagon = value;
        }

        public Vector2 CharakterScale
        {
            get => _charakterScale;
            set => _charakterScale = value;
        }

        public Vector3 CubePosition
        {
            get => _cubePosition;
            set => _cubePosition = value;
        }

        public void Update()
        {
            _cubePosition = _hexagon.Position + translation;
        }

        public static void LoadContent(ContentManager content, SpriteBatch spriteBatch)
        {
            //Lade Guu Animation
            //SpriteSheet spritesheet;
            //spritesheet = content.Load<SpriteSheet>("Charakter\\Guu.json", new JsonContentLoader());
            //spriteCharakter = new AnimatedSprite(spritesheet);
            //spriteCharakter.Play("walk_left");
            //_spriteBatch = spriteBatch;
        }





    }
}
