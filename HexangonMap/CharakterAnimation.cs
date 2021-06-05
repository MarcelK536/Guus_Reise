using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;

namespace Guus_Reise.HexangonMap
{
    class CharakterAnimation
    {
        Charakter _charakter;
        private static Model _cube;
        private Vector2 _charakterScale = new Vector2(1f, 1f);
        private Vector3 _cubePosition;
        private Vector2 _positionAnimatedSprite = new Vector2(465f, 80f);
        Vector3 translation = new Vector3(0.0f, 0.05f, 0f);
        Hex _hexagon;

        static AnimatedSprite spriteCharakter;
        private static SpriteBatch _spriteBatch;

        public CharakterAnimation(Hex hexagon, Charakter charakter)
        {
            _hexagon = hexagon;
            _charakter = charakter;
            _cubePosition = hexagon.Position + translation;
        }

       public Vector2 PositionAnimatedSprite
        {
            get => _positionAnimatedSprite;
            set => _positionAnimatedSprite = value;
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
