using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise.HexangonMap
{
    class VisualisationManagerHexmap
    {
        public Camera _camera;

        public float lengthHexMap;
        public float widthHexMap;
       
        public int lastwheel;
        private KeyboardState _prevKeyState;


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

            // Mit G setzen der Kamera wieder in die Start Position (Mitte der Map, Map komplett zu sehen)
            if (Keyboard.GetState().IsKeyDown(Keys.G) && _prevKeyState.IsKeyUp(Keys.G))
            {
                this.SetCameraToMiddleOfMap();
            }
            _prevKeyState = Keyboard.GetState();
        }

        // Setzt den Fokues der Camera auf die Mitte der Map, sodass die komplette Map zu sehen ist
        public void SetCameraToMiddleOfMap()
        {
            //Set Moving-Values
            float valueZoom;
            float laengsteSeite;
            if (lengthHexMap >= widthHexMap)
            {
                laengsteSeite = lengthHexMap;
            }
            else
            {
                laengsteSeite = widthHexMap;
            }
            if(laengsteSeite % 2 ==0)
            {
                valueZoom = laengsteSeite / 2;
            }
            else
            {
                valueZoom = (laengsteSeite - 1) / 2;
            }
            valueZoom = valueZoom - _camera.CurrentTranslation.Z;
            float valueX = lengthHexMap / 2 - 0.5f;
            valueX = valueX - _camera.CurrentTranslation.X;
            float valueY = widthHexMap / 4 - 0.5f;
            valueY = valueY - _camera.CurrentTranslation.Y;

            _camera.MoveCameraValue("Y", valueY);
            _camera.MoveCameraValue("X", valueX);
            _camera.MoveCameraValue("zoom", -1*(valueZoom+2));

        }
    }
}
