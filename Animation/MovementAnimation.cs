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

        public bool isSlidingCameraVector = false;
        public bool isSlidingCharakter = false;
        public bool isWaiting = false;

        public float waittime;
        public int currIntervall;
        public string currDirection;
        public Vector3 currMovementVector;
        public Vector3 directionMovement;
        public Vector3 currTotalMovementVector;
        public Vector3 currDirectionMovement;
        Charakter movingCharakter;
        public float currValue;

        public bool isNewStep;

        public bool xReady = false;
        public bool yReady = false;
        public bool zReady = false;

        public bool xReadyCharakter = false;
        public bool yReadyCharakter = false;
        public bool zReadyCharakter = false;


        public List<string> ablauf;

        public MovementAnimation(string type, Hex start, Hex target)
        {
            startHex = start;
            targetHex = target;
            _camera = HexMap.Camera;
            switch (type)
            {
                case "CharakterMovement":
                    ablauf = new List<string> { "FokusStartHex", "Wait500", "SlideHexToHexWalk", "ZoomOut" };
                    break;
            }
            currentStep = 0;
            targetHex.Charakter.IsMoving = true;
            targetHex.Charakter.CharakterAnimation.CharakterMovementPostion = targetHex.Charakter.CharakterAnimation.Translation + startHex.Position;
        }

        #region Update

        public void Update(GameTime gametime)
        {
            targetHex.Charakter.CharakterAnimation.Update(gametime);
            if (isSlidingCameraVector)
            {
                MakeCameraSlide(gametime, currIntervall, currMovementVector);
            }
            if (isWaiting)
            {
                Wait(gametime, waittime);
            }
            if (isSlidingCharakter)
            {
                MakeCharakterSlide(gametime, currIntervall, currDirectionMovement);
            }
            UpdateAnimation(gametime);
        }

        public void UpdateAnimation(GameTime gametime)
        {
            if (currentStep >= ablauf.Count)
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
                else if (ablauf[currentStep] == "Wait500")
                {
                    if (isNewStep)
                    {
                        isWaiting = true;
                        waittime = 500;
                        isNewStep = false;
                    }
                    else
                    {
                        if (!isWaiting)
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
                else if (ablauf[currentStep] == "SlideHexToHexWalk")
                {
                    if (isNewStep)
                    {
                        currIntervall = 10;
                        isNewStep = false;
                        movingCharakter = targetHex.Charakter;
                        SlideBetweenHex(currIntervall, startHex, targetHex);
                        CharakterWalk(currIntervall);
                    }
                    else
                    {
                        if (!isSlidingCameraVector && !isSlidingCharakter)
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
            if (timer > duration)
            {
                isWaiting = false;
            }
        }

        #region Kamerabewegung
        public void SlideBetweenHex(int intervall, Hex startHex, Hex targetHex)
        {
            HexMap.visManager.SetFocusToHex(startHex, 0);
            Vector3 direction = HexMap.visManager.GetVectorBewtweenTwoHex(startHex, targetHex);
            currTotalMovementVector = direction;
            currMovementVector = NormOnLength(direction, 0.01f);
            isSlidingCameraVector = true;
        }

        // Startet eine "smoothe" Kamerabewegung, um den Vector direction
        public void SlideCamera(int intervall, Vector3 direction)
        {
            currIntervall = intervall;
            currTotalMovementVector = direction;
            currMovementVector = NormOnLength(direction, 0.1f);
            isSlidingCameraVector = true;
        }

        public void CharakterWalk(int intervall)
        {
            directionMovement = new Vector3(0, 0, 0);
            directionMovement.X = targetHex.Position.X - startHex.Position.X;
            directionMovement.Y = targetHex.Position.Y - startHex.Position.Y;
            directionMovement.Z = targetHex.Position.Z - startHex.Position.Z;
            currDirectionMovement = NormOnLength(directionMovement, 0.01f);
            currIntervall = intervall;
            isSlidingCharakter = true;
            movingCharakter.CharakterAnimation.AnimationPlanner = "l";
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
            if (direction.Z == 0)
            {
                zReady = true;
            }
            if (timer > intervall)
            {
                _camera.MoveCameraValue(direction);
                if (direction.Y < 0)
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
                if (direction.X > 0)
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
                if (direction.Z > 0)
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
            if (yReady == true && xReady == true && zReady == true)
            {
                yReady = false;
                xReady = false;
                zReady = false;
                isSlidingCameraVector = false;

            }
        }

        public void MakeCharakterSlide(GameTime gametime, int intervall, Vector3 direction)
        {
            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if (direction.Y == 0)
            {
                yReadyCharakter = true;
            }
            if (direction.X == 0)
            {
                xReadyCharakter = true;
            }
            if (direction.Z == 0)
            {
                zReadyCharakter = true;
            }
            if (timer > intervall)
            {
                if (timer > intervall)
                {
                    MoveCharakterValue(direction);
                    if (direction.Y < 0)
                    {
                        if (movingCharakter.CharakterAnimation.CharakterMovementPostion.Y <= (targetHex.Position.Y + targetHex.Charakter.CharakterAnimation.Translation.Y))
                        {
                            yReadyCharakter = true;
                        }
                    }
                    else
                    {
                        if (movingCharakter.CharakterAnimation.CharakterMovementPostion.Y >= (targetHex.Position.Y + targetHex.Charakter.CharakterAnimation.Translation.Y))
                        {
                            yReadyCharakter = true;
                        }
                    }
                    if (direction.X > 0)
                    {
                        if (movingCharakter.CharakterAnimation.CharakterMovementPostion.X >= (targetHex.Position.X + targetHex.Charakter.CharakterAnimation.Translation.X))
                        {
                            xReadyCharakter = true;
                        }
                    }
                    else
                    {
                        if (movingCharakter.CharakterAnimation.CharakterMovementPostion.X <= (targetHex.Position.X + targetHex.Charakter.CharakterAnimation.Translation.X))
                        {
                            xReadyCharakter = true;
                        }
                    }
                    if (direction.Z > 0)
                    {
                        if (movingCharakter.CharakterAnimation.CharakterMovementPostion.Z >= (targetHex.Position.Z + targetHex.Charakter.CharakterAnimation.Translation.Z))
                        {
                            zReadyCharakter = true;
                        }
                    }
                    else
                    {
                        if (movingCharakter.CharakterAnimation.CharakterMovementPostion.Z <= (targetHex.Position.Z + targetHex.Charakter.CharakterAnimation.Translation.Z))
                        {
                            zReadyCharakter = true;
                        }
                    }
                    timer = 0;
                }
                if (yReadyCharakter == true && xReadyCharakter == true && zReadyCharakter == true)
                {
                    yReadyCharakter = false;
                    xReadyCharakter = false;
                    zReadyCharakter = false;
                    isSlidingCharakter = false;
                    movingCharakter.CharakterAnimation.AnimationPlanner = "stop";
                }
            }
        }

        public void MoveCharakterValue(Vector3 direction)
        {
            MoveCharakterValueDirection("X", direction.X);
            MoveCharakterValueDirection("Y", direction.Y);
            if (direction.Y != 0)
            {
                direction.Z -= direction.Y;
            }
            MoveCharakterValueDirection("zoom", direction.Z);
        }

        public void MoveCharakterValueDirection(string direction, float value)
        {
            switch (direction)
            {
                    case "Y":
                        movingCharakter.CharakterAnimation.CharakterMovementPostion += new Vector3(0, value, -1 * value);
                        break;

                    case "X":
                        movingCharakter.CharakterAnimation.CharakterMovementPostion += new Vector3(value, 0, 0);
                        break;

                case "zoom":
                    movingCharakter.CharakterAnimation.CharakterMovementPostion += new Vector3(0, 0, value);
                    break;
                default: break;
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
