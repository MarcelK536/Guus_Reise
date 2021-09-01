using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise.HexangonMap
{
    class VisualisationManagerHexmap
    {
        public static  Camera _camera;

        public float lengthHexMap;
        public float widthHexMap;
       
        public int lastwheel;
        private KeyboardState _prevKeyState;

        public static bool isMovementStop = false;
        public static bool isRunningMovement = false;

        public bool isDetailViewH;


        //Values for the Movement Animation
        public static Hex startHex;
        public static Hex targetHex;
        public static string currentStep;
        public static bool isMakeCameraSlide = false;
        public static int waittime = 1000;

        public static bool fokusValuesSet = false;
        public static bool fokusSet = false;


        public VisualisationManagerHexmap(int length, int width, Camera camera)
        {
            lengthHexMap = (float)length;
            widthHexMap = (float)width;
            _camera = camera;
            _prevKeyState = Keyboard.GetState();
            lastwheel = 0;
        }

        public void Update(GameTime gametime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _camera.MoveCamera("w");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _camera.MoveCamera("s");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _camera.MoveCamera("a");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _camera.MoveCamera("d");
            }

            if (Mouse.GetState().ScrollWheelValue > lastwheel)
            {
                lastwheel = Mouse.GetState().ScrollWheelValue;
                _camera.MoveCamera("hoch");
            }

            if (Mouse.GetState().ScrollWheelValue < lastwheel)
            {
                lastwheel = Mouse.GetState().ScrollWheelValue;
                _camera.MoveCamera("runter");
            }

            // Mit R setzen der Kamera wieder in die Start Position (Mitte der Map, Map komplett zu sehen)
            if (Keyboard.GetState().IsKeyDown(Keys.R) && _prevKeyState.IsKeyUp(Keys.R))
            {
                this.SetCameraToMiddleOfMap();
            }
            _prevKeyState = Keyboard.GetState();

        }



        public Vector3 GetVectorBewtweenTwoHex(Hex startHex, Hex targetHex)
        {

            Vector2 coordinatesStart = GetMovmentCoordinatesOfHex(startHex);
            Vector2 coordinatesTarget = GetMovmentCoordinatesOfHex(targetHex);
            Vector3 vectorToTarget = new Vector3(coordinatesTarget.X - coordinatesStart.X, coordinatesTarget.Y - coordinatesStart.Y, 0);
            return vectorToTarget;
        }

        public string GetDirectionBetweenTwoHex(Hex startHex, Hex targetHex)
        {
            Vector3 direction = GetVectorBewtweenTwoHex(startHex, targetHex);
            if(direction.X != 0)
            {

            }
            return "tets";
        }

        // Setzt den Fokues der Camera auf die Mitte der Map, sodass die komplette Map zu sehen ist
        public void SetCameraToMiddleOfMap()
        {
            float valueZoom;
            float laengsteSeite;
            float valueX;
            float valueY;

            if (lengthHexMap >= widthHexMap)
            {
                laengsteSeite = lengthHexMap;
            }
            else
            {
                laengsteSeite = widthHexMap;
            }

            //Set Moving-Values
            if (Game1.GState == Game1.GameState.InTalkFight || Game1.GState == Game1.GameState.InFight)
            {
                valueZoom = -laengsteSeite + 4.0f;

                valueZoom = valueZoom - _camera.CurrentTranslation.Z;
                valueX = lengthHexMap / 2 + 0.05f;
                valueX = valueX - _camera.CurrentTranslation.X;
                valueY = widthHexMap/ 2 - 0.2f;

                if(Game1._graphics.IsFullScreen == true)
                {
                    valueY = widthHexMap / 2 + 0.05f;
                }

                valueY = valueY - _camera.CurrentTranslation.Y;
            }
            else
            {
                
                if (laengsteSeite % 2 == 0)
                {
                    valueZoom = laengsteSeite / 2;
                }
                else
                {
                    valueZoom = (laengsteSeite - 1) / 2;
                }
                valueZoom = valueZoom - _camera.CurrentTranslation.Z;
                valueX = lengthHexMap / 2 - 0.5f;
                valueX = valueX - _camera.CurrentTranslation.X;
                valueY = widthHexMap / 4 - 0.5f;
                valueY = valueY - _camera.CurrentTranslation.Y;
            }
            
            

            _camera.MoveCameraValue("Y", valueY);
            _camera.MoveCameraValue("X", valueX);
            _camera.MoveCameraValue("zoom", -1*(valueZoom+2));

            isDetailViewH = false;

        }

        public void SetFocusToHex(Hex hex, float zoomValue)
        {
            Point locicalPosition = hex.LogicalPosition;
            float valueZoom = 0;
            if(zoomValue != 0)
            {
                valueZoom = zoomValue - (_camera.CurrentTranslation.Y * 0.4f);
                valueZoom = valueZoom + _camera.CurrentTranslation.Z;
            }
            float valueX = locicalPosition.X;
            if (locicalPosition.Y % 2 != 0)
            {
                valueX += 0.5f;
            }
            valueX = valueX - _camera.CurrentTranslation.X;
            float valueY = locicalPosition.Y * 0.5f;
            valueY = valueY - _camera.CurrentTranslation.Y;
            _camera.MoveCameraValue("Y", valueY);
            _camera.MoveCameraValue("X", valueX);
            if(zoomValue != 0)
            {
               _camera.MoveCameraValue("zoom", valueZoom);
            }
           
        }

        public Vector2 GetMovmentCoordinatesOfHex(Hex hex)
        {
            Point locicalPosition = hex.LogicalPosition;
            float valueX = locicalPosition.X;
            if (locicalPosition.Y % 2 != 0)
            {
                valueX += 0.5f;
            }
            valueX = valueX - _camera.CurrentTranslation.X;
            float valueY = locicalPosition.Y * 0.5f;
            valueY = valueY - _camera.CurrentTranslation.Y;
            return new Vector2(valueX, valueY);
        }

        public void ManageCharakterViewH(SkillUpMenu charakterMenu)
        {
            if (charakterMenu.Active != true)
            {
                if (HexMap.activeHex != null)
                {
                    SetFocusToHex(HexMap.activeHex,6);
                    isDetailViewH = true;

                }

            }
            else
            {
                HexMap.visManager.SetCameraToMiddleOfMap();
            }
        }

        public void VisMovement(Hex start, Hex target)
        {
            float valueX = 0;
            float valueY = 0;
            SetFocusToHex(start,6);
            _camera.MoveCameraValue("zoom", -6);
            valueX = (target.LogicalPosition.X - start.LogicalPosition.X);
            valueY = (target.LogicalPosition.Y - start.LogicalPosition.Y) * 0.5f;
            if(start.LogicalPosition.Y % 2 == 0 && target.LogicalPosition.Y % 2 != 0)
            {
                valueX += 0.5f;
            }
            else if(start.LogicalPosition.Y % 2 != 0 && target.LogicalPosition.Y % 2 == 0)
            {
                valueX -= 0.5f;
            }
            _camera.MoveCameraValue("X", valueX);
            _camera.MoveCameraValue("Y", valueY);
        }

    }
}
