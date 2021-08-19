using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Guus_Reise.Animation;
using Microsoft.Xna.Framework.Audio;

namespace Guus_Reise.HexangonMap
{
    class CharakterAnimation
    {
        private static KeyboardState _prevKeyState;
        private Vector3 _charakterPostion; //Position des Charakters
        private Vector3 _charakterMovementPostion;
        
        Vector3 translation = new Vector3(-0.3f, 0.1f, 0f); // Verschiebung des Charakters Ausgehend vom Hex
        private Vector3 _charakterScale = new Vector3(0.002f, 0.002f, 0.002f); //Skaliserung des Charakters;
        //static List<string> animations = new List<string> { "Idle", "moveLeft", "moveRight", "moveFront", "moveBack", "readyToFight" };

        private readonly List<Texture2D> idle;
        private readonly List<Texture2D> jump;
        private readonly List<Texture2D> walkLeft;
        private readonly List<Texture2D> walkRight;
        //static List<Texture2D> moveBack;
        //static List<Texture2D> moveRight;
        //static List<Texture2D> moveFront;
        //static List<Texture2D> readyToFight;

        Hex _hexagon;
        Charakter _charakter;
        readonly Model _planeModel;
        Texture2D _texCharakter;
        Texture2D _curTex;
        private List<Texture2D> currentAnimation;
        int currentFrame = 0;

        private Vector3 _glow;
        private Vector3 _color;

        float timer;
        float _standardIntervall;
        float _currentIntervall;

        bool isPlayAnimation = false;

        string _animationPlanner = "";

        SoundEffect[] _sounds;


        public CharakterAnimation(Model planeModel, Texture2D texCharakter, List<Texture2D> animIdle, List<Texture2D> animJump, List<Texture2D> animWalkLeft, List<Texture2D> animWalkRight, float standardintervall, SoundEffect[] sounds)
        {
            _standardIntervall = standardintervall;
            idle = animIdle;
            jump = animJump;
            walkLeft = animWalkLeft;
            walkRight = animWalkRight;
            _planeModel = planeModel;
            _texCharakter = texCharakter;
            _curTex = _texCharakter;
            _sounds = sounds;

            // Set previous Keyboard State
            _prevKeyState = Keyboard.GetState();
        }



        public void SetParametersAfterInitCharakter(Charakter charakter, Hex hexagon)
        {
            _charakter = charakter;
            _hexagon = hexagon;
            _charakterPostion = hexagon.Position + translation;
            _charakterMovementPostion = _charakterPostion;
            _glow = new Vector3(0.1f, 0.1f, 0.1f);
            _color = new Vector3(0, 0, 0);
        }

        public Hex Hexagon
        {
            get => _hexagon;
            set => _hexagon = value;
        }

        public string AnimationPlanner
        {
            get => _animationPlanner;
            set => _animationPlanner = value;
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

        public Vector3 CharakterMovementPostion
        {
            get => _charakterMovementPostion;
            set => _charakterMovementPostion = value;
        }

        public Charakter Charakter
        {
            get => _charakter;
            set => _charakter = value;
        }

        public Vector3 Translation
        {
            get => translation;
            set => translation = value;
        }

        public void UpdateHex(Hex hexagon)
        {
            Hexagon = hexagon;
        }

        public void Draw(Camera camera, Vector3 position)
        {
            Matrix world = (Matrix.CreateScale(_charakterScale) * Matrix.CreateRotationX(45) * Matrix.CreateTranslation(position));
            foreach (var mesh in _planeModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.TextureEnabled = true;
                    effect.Texture = _curTex;
                    //effect.LightingEnabled = true;
                    //effect.EnableDefaultLighting();
                    //effect.PreferPerPixelLighting = true;
                    effect.World = world;
                    effect.View = camera.view;
                    effect.Projection = camera.projection;
                    //effect.DiffuseColor = this.Glow;
                    effect.AmbientLightColor = this._color;
                }
                mesh.Draw();
            }
        }

        public void DrawCharakter(Camera camera)
        {
            this.CharakterPostion = this.Hexagon.Position + this.translation;
            Draw(camera, _charakterPostion);
        }

        public void DrawCharakterMovementPosition(Camera camera)
        {
            Draw(camera, _charakterMovementPostion);
        }

        public void Update(GameTime gametime)
        {
            _charakterPostion = _hexagon.Position + translation;
            if(isPlayAnimation)
            {
                if(Game1.GState == Game1.GameState.MovementAnimation)
                {
                    if (MovementAnimationManager._currentMovementAnimation.movementType == "NPCMovement")
                    {
                        _curTex = currentAnimation[0];
                    }
                    else
                    {
                        _curTex = currentAnimation[currentFrame];
                        UpdateAnimation(gametime);
                    }

                }
                else
                {
                    if(currentAnimation.Count == currentFrame)
                    {
                        currentFrame--;
                    }
                    _curTex = currentAnimation[currentFrame];
                    UpdateAnimation(gametime);
                }
            }
            if(Game1.GState == Game1.GameState.MovementAnimation)
            {
                if (_animationPlanner == "Left")
                {
                    Play("WalkLeft", _standardIntervall);
                }
                else if(_animationPlanner == "Right")
                {
                    Play("WalkRight", _standardIntervall);
                }
                else if (_animationPlanner == "stop")
                {
                    StopAnimation();
                    _animationPlanner = "";
                }
            }
            else
            {
                if (!_hexagon.IsHovered)
                {
                    if (!_hexagon.IsActive)
                    {
                        StopAnimation();
                    }
                }
                if (_hexagon.IsHovered)
                {
                    if (CharakterAnimationManager.ActiveHexExists)
                    {
                        if (_hexagon.IsActive)
                        {
                            Play("Idle", _standardIntervall);
                        }
                    }
                    else
                    {
                        Play("Idle", _standardIntervall);
                    }
                }

            }

            _prevKeyState = Keyboard.GetState();
        }

        public void UpdateAnimation( GameTime gametime)
        {
            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds / 2;
            if(timer > _currentIntervall)
            {
                currentFrame++;
                timer = 0;
                if(currentFrame == currentAnimation.Count)
                {
                    currentFrame = 0;
                }
            }
        }

        public void Play(string nameAnimation, float intervall)
        {
            if(nameAnimation == "")
            {
                throw new InvalidOperationException("Animation kann nicht null sein");
            }
            currentAnimation = nameAnimation switch
            {
                "Idle" => idle,
                "Jump" => jump,
                "WalkLeft" => walkLeft,
                "WalkRight" => walkRight,
                _ => idle,
            };
            _currentIntervall = intervall;
            isPlayAnimation = true;
            _sounds[0].Play();
        
        }

        // Setzen der Texture wieder auf die Standardtextur
        public void StopAnimation()
        {
            _curTex = _texCharakter;
            isPlayAnimation = false;
        }





    }
}
