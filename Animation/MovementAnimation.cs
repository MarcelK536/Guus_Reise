using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise.Animation
{
    class MovementAnimation
    {
        public Camera _camera;
        public Vector3 startTranslationCamera = new Vector3(0, 0, 0);

        public Hex startHex;
        public Hex targetHex;

        public int currentStep;

        public float timer = 0;
        public bool isSlidingCamera = false;
        public bool isSlidingCameraVector = false;
        public bool isWaiting = false;
        public float waittime;
        public int currIntervall;
        public string currDirection;
        public Vector3 currMovementVector;
        public Vector3 currTotalMovementVector;
        public float currValue;

        public bool isNewStep;

        public bool xReady = false;
        public bool yReady = false;
        public bool zReady = false;

        public List<string> ablauf;

        public MovementAnimation(string type, Hex start, Hex target)
        {
           startHex = start;
           targetHex = target;
           _camera = HexMap.Camera;
           switch (type)
           {
                case "CharakterMovement":
                    ablauf = new List<string> { "FokusStartHex", "Wait500", "SlideHexToHex","ZoomOut"};
                    break;
           }
           currentStep = 0;
           targetHex.Charakter.IsMoving = true;
           targetHex.Charakter.CharakterAnimation.CharakterMovementPostion = targetHex.Charakter.CharakterAnimation.Translation + startHex.Position;
        }

        #region Update

        public void Update(GameTime gametime)
        {
            if(isSlidingCameraVector)
            {
                MakeCameraSlide(gametime, currIntervall, currMovementVector);
            }
            if(isWaiting)
            {
                Wait(gametime, waittime);
            }
            UpdateAnimation(gametime);
        }

        public void UpdateAnimation(GameTime gametime)
        {
            if(currentStep >= ablauf.Count)
            {
                Game1.GState = Game1.GameState.InGame;

            }
            else
            {
                if (ablauf[currentStep] == "FokusStartHex")
                {
                    HexMap.visManager.SetFocusToHex(startHex, 6);
                    currentStep++;
                    isNewStep = true;
                }
                else if (ablauf[currentStep] == "ZoomOut")
                {
                    if (isNewStep)
                    {
                        Vector3 zoomValue = new Vector3(0, 0, -4f);
                        SlideCamera(3, zoomValue);
                        isNewStep = false;
                    }
                    if (!isSlidingCameraVector)
                    {
                        currentStep++;
                        isNewStep = true;
                    }
                }
                else if(ablauf[currentStep] == "Wait500")
                {
                    if(isNewStep)
                    {
                        isWaiting = true;
                        waittime = 500;
                        isNewStep = false;
                    }
                    else
                    {
                        if(!isWaiting)
                        {
                            currentStep++;
                            isNewStep = true;
                        }
                    }
                }
                else if (ablauf[currentStep] == "SlideHexToHex")
                {
                    if (isNewStep)
                    {
                        currIntervall = 10;
                        isNewStep = false;
                        SlideBetweenHex(currIntervall, startHex, targetHex);
                    }
                    else
                    {
                        if (!isSlidingCameraVector)
                        {
                            currentStep++;
                            isNewStep = true;
                        }
                    }
                }
            }

        }
        #endregion

        public void Draw()
        {
            
            targetHex.Charakter.CharakterAnimation.DrawCharakterMovementPosition(_camera);
        }

        public void Wait(GameTime gametime, float duration)
        {
            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if(timer > duration)
            {
                isWaiting = false;
            }
        }

        #region Kamerabewegung
        public void SlideBetweenHex(int intervall, Hex startHex, Hex targetHex)
        {
            HexMap.visManager.SetFocusToHex(startHex,0);
            Vector3 direction = HexMap.visManager.GetVectorBewtweenTwoHex(startHex, targetHex);
            currTotalMovementVector = direction;
            currMovementVector = NormOnLength(direction, 0.01f);
            isSlidingCameraVector = true;
        }

        // Startet eine "smoothe" Kamerabewegung
        public void SlideCamera(int intervall, string direction, float value)
        {
            isSlidingCamera = true;
            currIntervall = intervall;
            currDirection = direction;
            currValue = value;
        }

        public void SlideCamera(int intervall, Vector3 direction)
        {
            currIntervall = intervall;
            currTotalMovementVector = direction;
            currMovementVector = NormOnLength(direction, 0.1f);
            isSlidingCameraVector = true;
        }

        
        public void MakeCameraSlide(GameTime gametime, int intervall, Vector3 direction)
        {
            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if (startTranslationCamera == new Vector3(0, 0, 0))
            {
                startTranslationCamera = _camera.CurrentTranslation;
            }
            if (direction.Y == 0)
            {
                yReady = true;
            }
            if (direction.X == 0)
            {
                xReady = true;
            }
            if(direction.Z == 0)
            {
                zReady = true;
            }
            if (timer > intervall)
            {
                _camera.MoveCameraValue(direction);
                if(direction.Y < 0)
                {
                    if (_camera.CurrentTranslation.Y <= startTranslationCamera.Y + currTotalMovementVector.Y)
                    {
                        yReady = true;
                    }
                }
                else
                {
                    if (_camera.CurrentTranslation.Y >= startTranslationCamera.Y + currTotalMovementVector.Y)
                    {
                        yReady = true;
                    }
                }
                if(direction.X > 0)
                {
                    if (_camera.CurrentTranslation.X >= startTranslationCamera.X + currTotalMovementVector.X)
                    {
                        xReady = true;
                    }
                }
                else
                {
                    if (_camera.CurrentTranslation.X <= startTranslationCamera.X + currTotalMovementVector.X)
                    {
                        xReady = true;
                    }
                }
                if(direction.Z > 0)
                {
                    if (_camera.CurrentTranslation.Z <= startTranslationCamera.Z - currTotalMovementVector.Z)
                    {
                        zReady = true;
                    }
                }
                else
                {
                    if (_camera.CurrentTranslation.Z >= startTranslationCamera.Z - currTotalMovementVector.Z)
                    {
                        zReady = true;
                    }
                }
                timer = 0;
            }
            if(yReady == true && xReady == true && zReady == true)
            {
                yReady = false;
                xReady = false;
                zReady = false;
                isSlidingCameraVector = false;

            }
        }

        // Führt die Smoothe Kamerabewegung aus
        public void MakeCameraSlide(GameTime gametime, int intervall, string direction, float value)
        {
            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if (startTranslationCamera == new Vector3(0, 0, 0))
            {
                startTranslationCamera = _camera.CurrentTranslation;
            }
            if (timer > intervall)
            {
                switch (direction)
                {
                    case "Y":
                        if (value < 0)
                        {
                            _camera.MoveCamera("w");
                            if (_camera.CurrentTranslation.Y <= startTranslationCamera.Y + value)
                            {
                                isSlidingCamera = false;
                            }
                        }
                        else
                        {
                            _camera.MoveCamera("s");
                            if (_camera.CurrentTranslation.Y >= startTranslationCamera.Y + value)
                            {
                                isSlidingCamera = false;
                            }
                        }
                        break;
                    case "X":
                        if (value < 0)
                        {
                            _camera.MoveCamera("a");
                            if (_camera.CurrentTranslation.X <= startTranslationCamera.X + value)
                            {
                                isSlidingCamera = false;
                            }
                        }
                        else
                        {
                            _camera.MoveCamera("d");
                            if (_camera.CurrentTranslation.X >= startTranslationCamera.X + value)
                            {
                                isSlidingCamera = false;
                            }
                        }
                        break;
                    case "zoom":
                        if (value < 0)
                        {
                            _camera.MoveCamera("runter");
                            if (_camera.CurrentTranslation.Z >= startTranslationCamera.Z - value)
                            {
                                isSlidingCamera = false;
                            }
                        }
                        else
                        {
                            _camera.MoveCamera("hoch");
                            if (_camera.CurrentTranslation.Z <= startTranslationCamera.Z - value)
                            {
                                isSlidingCamera = false;
                            }

                        }
                        break;
                    case "diagOben":
                        if (value < 0)
                        {
                            _camera.MoveCamera("diag4");
                            if (_camera.CurrentTranslation.X <= startTranslationCamera.X + value)
                            {
                                isSlidingCamera = false;
                            }
                        }
                        else
                        {
                            _camera.MoveCamera("diag1");
                            if (_camera.CurrentTranslation.X >= startTranslationCamera.X + value)
                            {
                                isSlidingCamera = false;
                            }
                        }
                        break;
                    case "diagUnten":
                        if (value < 0)
                        {
                            _camera.MoveCamera("diag3");
                            if (_camera.CurrentTranslation.X <= startTranslationCamera.X + value)
                            {
                                isSlidingCamera = false;
                            }
                        }
                        else
                        {
                            _camera.MoveCamera("diag2");
                            if (_camera.CurrentTranslation.X >= startTranslationCamera.X + value)
                            {
                                isSlidingCamera = false;
                            }
                        }
                        break;
                    default:
                        break;
                }
                timer = 0;
            }

        }

        public Vector3 NormOnLength(Vector3 vector, float newLength)
        {
            double X = (double)vector.X;
            double Y = (double)vector.Y;
            double Z = (double)vector.Z;
            double quadSum = Math.Abs(Math.Pow(X, 2)) + Math.Abs(Math.Pow(Y, 2)) + Math.Abs(Math.Pow(Z, 2));
            double length = Math.Sqrt(quadSum);
            Vector3 normVector = vector;
            normVector.X = normVector.X * (newLength / (float)length);
            normVector.Y = normVector.Y * (newLength / (float)length);
            normVector.Z = normVector.Z * (newLength / (float)length);
            return normVector;
        }
        #endregion
    }
}
