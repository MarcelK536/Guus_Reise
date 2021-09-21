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


        private SoundManager _sm;

        Vector3 translation = new Vector3(-0.3f, 0.1f, 0f); // Verschiebung des Charakters Ausgehend vom Hex
        readonly Vector3 defaultTranslation = new Vector3(-0.3f, 0.1f, 0f); // Verschiebung des Charakters Ausgehend vom Hex

        private Vector3 _charakterScale; // = new Vector3(0.002f, 0.002f, 0.002f); //Skaliserung des Charakters;
        //static List<string> animations = new List<string> { "Idle", "moveLeft", "moveRight", "moveFront", "moveBack", "readyToFight" };

        private readonly List<Texture2D> idle;
        private readonly List<Texture2D> jump;
        private readonly List<Texture2D> walkLeft;
        private readonly List<Texture2D> walkRight;
        private readonly List<Texture2D> fightWeapons;

        //static List<Texture2D> moveBack;
        //static List<Texture2D> moveRight;
        //static List<Texture2D> moveFront;
        //static List<Texture2D> readyToFight;

        public Hex _hexagon;
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

        public int identifier;


        public CharakterAnimation(Model planeModel, Texture2D texCharakter, List<Texture2D> animIdle, List<Texture2D> animJump, List<Texture2D> animWalkLeft, List<Texture2D> animWalkRight, List<Texture2D> animFightWeapons,  float standardintervall, SoundManager sm)
        {

            _standardIntervall = standardintervall;
            idle = animIdle;
            jump = animJump;
            walkLeft = animWalkLeft;
            walkRight = animWalkRight;
            fightWeapons = animFightWeapons;

            _planeModel = planeModel;

            _texCharakter = texCharakter;
            _curTex = _texCharakter;

            _sm = sm;

            // Set previous Keyboard State
            _prevKeyState = Keyboard.GetState();
        }



        public void SetParametersAfterInitCharakter(Charakter charakter, Hex hexagon)
        {

            _charakter = charakter;
            _hexagon = hexagon;
            Translation = Vector3.Transform(defaultTranslation, Matrix.CreateRotationY(hexagon.TileRotation));
            _charakterPostion = hexagon.Position + Translation;
            _charakterMovementPostion = _charakterPostion + Translation;
            _glow = new Vector3(0.1f, 0.1f, 0.1f);
            _color = new Vector3(0, 0, 0);
        }

        public CharakterAnimation Clone()
        {
            CharakterAnimation charakterAnimation = new CharakterAnimation(this._planeModel, this._texCharakter, this.idle, this.jump, this.walkLeft, this.walkRight, this.fightWeapons, this._standardIntervall, this._sm);
            charakterAnimation.Charakter = this.Charakter;
            charakterAnimation.Hexagon = this.Hexagon;
            if (charakterAnimation.Hexagon != null)
            {
                charakterAnimation.Translation = Vector3.Transform(defaultTranslation, Matrix.CreateRotationY(Hexagon.TileRotation));
            }
            else
            {
                charakterAnimation.Translation = this.translation;
            }
            charakterAnimation.CharakterPostion = this.CharakterPostion;
            charakterAnimation.CharakterMovementPostion = this.CharakterMovementPostion;
            charakterAnimation._glow = this._glow;
            charakterAnimation._color = this._color;
            Random rnd = new Random();
            charakterAnimation.identifier = rnd.Next();
            return charakterAnimation;
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

        public Vector3 DefaultTranslation
        {
            get => defaultTranslation;
        }

        // Wenn im Kampf: Charaktere werden mit Waffen angezeigt
        public void SetWeaponTexCharakter()
        {
            if(_charakter.Name != "Guu")
            {
                return;
            }

            if(Game1.GState == Game1.GameState.InFight)
            {
                switch (_charakter.Weapon.Name)
                {
                    case "knife":
                        _curTex = fightWeapons[1];
                        break;
                    case "fist":
                        _curTex = fightWeapons[0];
                        break;
                    case "katana":
                        _curTex = fightWeapons[5];
                        break;
                    case "axe":
                        _curTex = fightWeapons[8];
                        break;
                    case "hammer":
                        _curTex = fightWeapons[9];
                        break;
                    case "stick":
                        _curTex = fightWeapons[10];
                        break;
                    case "boxing gloves":
                        _curTex = fightWeapons[11];
                        break;
                    default:
                        _curTex = _texCharakter;
                        break;
                }
            }
            else if(Game1.GState == Game1.GameState.InTalkFight)
            {
                switch (_charakter.WeaponTalkFight.Name)
                {
                    case "voice":
                        _curTex = fightWeapons[2];
                        break;
                    case "costume":
                        _curTex = fightWeapons[3];
                        break;
                    case "megafone":
                        _curTex = fightWeapons[4];
                        break;
                    case "universal translator":
                        _curTex = fightWeapons[6];
                        break;
                    case "smart book":
                        _curTex = fightWeapons[7];
                        break;
                    default:
                        _curTex = _texCharakter;
                        break;
                }
            }
            else
            {
                _curTex = _texCharakter;
            }
            
                
            
   
        }

        public void UpdateHex(Hex hexagon)
        {
            Hexagon = hexagon;
            Translation = Vector3.Transform(defaultTranslation, Matrix.CreateRotationY(hexagon.TileRotation));
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
            if (Game1.GState == Game1.GameState.InFight || Game1.GState == Game1.GameState.InTalkFight)
            {
                SetWeaponTexCharakter();
                _charakterScale = new Vector3(0.003f, 0.003f, 0.003f);
            }
            else
            {
                _charakterScale = new Vector3(0.002f, 0.002f, 0.002f);
                
            }
            this.CharakterPostion = this.Hexagon.Position + Vector3.Transform(defaultTranslation, Matrix.CreateRotationY(this.Hexagon.TileRotation));
            Draw(camera, _charakterPostion);
        }

        public void DrawCharakterMovementPosition(Camera camera)
        {
            Draw(camera, _charakterMovementPostion);
        }

        public void Update(GameTime gametime)
        {
            _charakterPostion = _hexagon.Position + Vector3.Transform(defaultTranslation, Matrix.CreateRotationY(_hexagon.TileRotation));

            if (isPlayAnimation)
            {
                if(Game1.GState == Game1.GameState.MovementAnimation)
                {
                    if (MovementAnimationManager._currentMovementAnimation.movementType == "NPCMovement")
                    {
                        _curTex = currentAnimation[0];
                    }
                    else
                    {
                        if(currentFrame >= currentAnimation.Count)
                        {
                            currentFrame = currentAnimation.Count - 1;
                        }
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
                    if (CharakterAnimationManager.animationSound == true)
                    {
                        if (_charakter.Name == "Guu")
                        {
                            _sm.PlaySound(3);
                        }
                    }

                    Play("WalkLeft", _standardIntervall);
                   
                    
                }
                else if(_animationPlanner == "Right")
                {
                    if (CharakterAnimationManager.animationSound == true)
                    {
                        if (_charakter.Name == "Guu")
                        {
                            _sm.PlaySound(3);
                        }
                    }
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
                else
                {
                    //Sound-Einstelungen
                if(_hexagon != HexMap.soundHex)
                {

                        if (CharakterAnimationManager.animationSound == true )
                        {
                            if(_charakter.Name == "Guu")
                            {
                                _sm.PlaySound(0);
                            }
                            if (_charakter.Name == "Paul")
                            {
                                _sm.PlaySound(1);
                            }
                            if (_charakter.Name == "Timmae")
                            {
                                _sm.PlaySound(2);
                            }
                            

                            

                        }
                        HexMap.soundHex = _hexagon;
             

                    }
                    
             



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
          


        
        }

        // Setzen der Texture wieder auf die Standardtextur
        public void StopAnimation()
        {
            _curTex = _texCharakter;
            isPlayAnimation = false;
            _sm.RestPlayTimes();
        }





    }
}
