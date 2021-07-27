﻿using Microsoft.Xna.Framework;
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

        public string movementType;

        public int currentStep;

        public float timer = 0;

        public bool isSlidingCameraVector = false;
        public bool isSlidingCharakter = false;
        public bool isSlidingCharakterPlural = false;
        public bool isWaiting = false;

        public float waittime;
        public int currIntervall;
        public string currDirection;
        public Vector3 currMovementVector;
        public Vector3 directionMovement;
        public Vector3 currTotalMovementVector;
        public Vector3 currDirectionMovement;
        public List<Vector3> currDirectionMovementList;
        Charakter movingCharakter;
        public float currValue;

        public bool isNewStep;

        public bool xReady = false;
        public bool yReady = false;
        public bool zReady = false;

        public bool xReadyCharakter = false;
        public bool yReadyCharakter = false;
        public bool zReadyCharakter = false;

        public List<Hex> oldNpcPos;
        public List<Hex> newNpcPos;
        public List<Charakter> movingCharakters;
        public bool[,] readyMatrix;
        public int readyCounter = 0;


        public List<string> ablauf;

        int indexCharakter = 0;

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

        public MovementAnimation(string type, List<Hex> oldHex, List<Hex> newHex, List<Charakter> movednpcs)
        {
            oldNpcPos = oldHex;
            newNpcPos = newHex;
            movingCharakters = movednpcs;
            movementType = type;
            _camera = HexMap.Camera;
            currDirectionMovementList = new List<Vector3>();
            switch (type)
            {
                case "NPCMovemernt":
                    ablauf = new List<string> { "FokusOnCenter", "Wait500", "CharakterMovementPlural" };
                    break;
            }
            currentStep = 0;
            readyMatrix = new bool[newHex.Count, 4];
            foreach(Charakter charakter in movingCharakters)
            {
                charakter.IsMoving = true;
                charakter.CharakterAnimation.CharakterMovementPostion = charakter.CharakterAnimation.Translation + oldNpcPos[movingCharakters.IndexOf(charakter)].Position;
                readyMatrix[movingCharakters.IndexOf(charakter),0] = false;
                readyMatrix[movingCharakters.IndexOf(charakter),1] = false;
                readyMatrix[movingCharakters.IndexOf(charakter),2] = false;
                readyMatrix[movingCharakters.IndexOf(charakter),3] = false;
            }
            
        }

        #region Update

        public void Update(GameTime gametime)
        {
            if(movementType == "NPCMovemernt")
            {
                foreach(Hex targetHex in newNpcPos)
                {
                    targetHex.Charakter.CharakterAnimation.Update(gametime);
                }
            }
            else
            {
                targetHex.Charakter.CharakterAnimation.Update(gametime);
            }
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
            if(isSlidingCharakterPlural)
            {
                foreach(Charakter movedCharakter in movingCharakters)
                {
                    MakeCharakterSlidePlural(gametime, currIntervall);
                }
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
                else if (ablauf[currentStep] == "FokusOnCenter")
                {
                    HexMap.visManager.SetCameraToMiddleOfMap();
                    currentStep++;
                    isNewStep = true;
                }
                else if(ablauf[currentStep] == "CharakterMovementPlural")
                {
                    if (isNewStep)
                    {
                        indexCharakter = 0;
                        currIntervall = 10;
                        isNewStep = false;
                        CharakterWalkPlural(currIntervall);
                    }
                    else
                    {
                        if (!isSlidingCharakterPlural)
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
            if(movementType == "NPCMovemernt")
            {
                foreach(Hex targetHex in newNpcPos)
                {
                    targetHex.Charakter.CharakterAnimation.DrawCharakterMovementPosition(_camera);
                }
            }
            else
            {
                targetHex.Charakter.CharakterAnimation.DrawCharakterMovementPosition(_camera);
            }
            
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
            if(directionMovement.X < 0)
            {
                movingCharakter.CharakterAnimation.AnimationPlanner = "Left";
            }
            else
            {
                movingCharakter.CharakterAnimation.AnimationPlanner = "Right";
            }
            
        }

        public void CharakterWalkPlural(int intervall)
        {
            currIntervall = intervall;
            foreach (Hex targetHex in newNpcPos)
            {
                var index = newNpcPos.IndexOf(targetHex);
                directionMovement = new Vector3(0, 0, 0);
                directionMovement.X = targetHex.Position.X - oldNpcPos[index].Position.X;
                directionMovement.Y = targetHex.Position.Y - oldNpcPos[index].Position.Y;
                directionMovement.Z = targetHex.Position.Z - oldNpcPos[index].Position.Z;
                Vector3 moveVector = NormOnLength(directionMovement, 0.01f);
                currDirectionMovementList.Add(moveVector);
                movingCharakter = targetHex.Charakter;
                if (directionMovement.X < 0)
                {
                    movingCharakter.CharakterAnimation.AnimationPlanner = "Left";
                }
                else
                {
                    movingCharakter.CharakterAnimation.AnimationPlanner = "Right";
                }
            }
            isSlidingCharakterPlural = true;
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

        public void MakeCharakterSlidePlural(GameTime gametime, int intervall)
        {
            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if(readyCounter == movingCharakters.Count)
            {
                isSlidingCharakterPlural = false;
                return;
            }
            foreach (Vector3 direction in currDirectionMovementList)
            {
                    var index = currDirectionMovementList.IndexOf(direction);
                    var movingCharakter = movingCharakters[index];

                    if (readyMatrix[index,0] == true)
                    {
                        continue;
                    }
                
                    if (direction.Y == 0)
                    {
                        readyMatrix[index, 2] = true;
                    }
                    if (direction.X == 0)
                    {
                        readyMatrix[index, 1] = true;
                    }
                    if (direction.Z == 0)
                    {
                        readyMatrix[index, 3] = true;
                    }

                    if (timer > intervall)
                    {
                        if (timer > intervall)
                        {
                            MoveCharakterValue(direction);
                            if (direction.Y < 0)
                            {
                                if (movingCharakter.CharakterAnimation.CharakterMovementPostion.Y <= (newNpcPos[index].Position.Y + newNpcPos[index].Charakter.CharakterAnimation.Translation.Y))
                                {
                                    readyMatrix[index, 2] = true;
                                }
                            }
                            else
                            {
                                if (movingCharakter.CharakterAnimation.CharakterMovementPostion.Y >= (newNpcPos[index].Position.Y + newNpcPos[index].Charakter.CharakterAnimation.Translation.Y))
                                {
                                    readyMatrix[index, 2] = true;
                                }
                            }
                            if (direction.X > 0)
                            {
                                if (movingCharakter.CharakterAnimation.CharakterMovementPostion.X >= (newNpcPos[index].Position.X + newNpcPos[index].Charakter.CharakterAnimation.Translation.X))
                                {
                                    readyMatrix[index, 1] = true;
                                }
                            }
                            else
                            {
                                if (movingCharakter.CharakterAnimation.CharakterMovementPostion.X <= (newNpcPos[index].Position.X + newNpcPos[index].Charakter.CharakterAnimation.Translation.X))
                                {
                                    readyMatrix[index, 1] = true;
                                }
                            }
                            if (direction.Z > 0)
                            {
                                if (movingCharakter.CharakterAnimation.CharakterMovementPostion.Z >= (newNpcPos[index].Position.Z + newNpcPos[index].Charakter.CharakterAnimation.Translation.Z))
                                {
                                    readyMatrix[index, 3] = true;
                                }
                            }
                            else
                            {
                                if (movingCharakter.CharakterAnimation.CharakterMovementPostion.Z <= (newNpcPos[index].Position.Z + newNpcPos[index].Charakter.CharakterAnimation.Translation.Z))
                                {
                                    readyMatrix[index, 3] = true;
                                }
                            }
                            timer = 0;
                        }

                        if (readyMatrix[index, 2] == true && readyMatrix[index, 1] == true && readyMatrix[index, 3] == true)
                        {

                            readyMatrix[index, 0] = true;
                            ++readyCounter;
                            movingCharakter.CharakterAnimation.AnimationPlanner = "stop";
                            continue;
                        }
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
