using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise.HexangonMap
{
    class VisualisationManager
    {
        public float lengthHexMap;
        public float widthHexMap;
        public Camera _camera;


        public VisualisationManager(int length, int width, Camera camera)
        {
            lengthHexMap = (float)length;
            widthHexMap = (float)width;
            _camera = camera;
        }

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
