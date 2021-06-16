using Microsoft.Xna.Framework;

namespace Guus_Reise
{
    class Camera
    {
        public Matrix view;
        public Matrix projection;
        Vector3 _currentTranslation = new Vector3(0, 0, 0); // Verschiebung der Camera relativ zur Anfangsposition (aus dem Konstruktor)
        public Vector3 CurrentTranslation
        {
            get => _currentTranslation;
            set => _currentTranslation = value;
        }

        public Camera(float aspectRatio)
        {
            view = Matrix.CreateLookAt(new Vector3(0f, 5, 7), new Vector3(0f, 0, 0), Vector3.UnitY);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(30), aspectRatio, 0.01f, 100f);
        }

        public void MoveCamera(string symbol)
        {
            if (HexMap.visManager.isDetailViewH)
            {
                HexMap.visManager.isDetailViewH = false;
            }
            switch (symbol)
            {
                case "w": this.view.Translation = new Vector3(0, -0.1f, 0.1f) + this.view.Translation; //bewegt camera nach vorne
                    _currentTranslation.Y = _currentTranslation.Y - 0.1f;
                    break;

                case "s": this.view.Translation = new Vector3(0, 0.1f, -0.1f) + this.view.Translation; //camera nach hinte
                    _currentTranslation.Y = _currentTranslation.Y + 0.1f;
                    break;

                case "a": this.view.Translation = new Vector3(0.1f, 0, 0) + this.view.Translation; //camera links
                    _currentTranslation.X = _currentTranslation.X - 0.1f;
                    break;

                case "d": this.view.Translation = new Vector3(-0.1f, 0, 0) + this.view.Translation; //camera rechts
                    _currentTranslation.X = _currentTranslation.X + 0.1f;
                    break;

                case "hoch":
                    if (_currentTranslation.Z >= -6.5f+(_currentTranslation.Y*0.35f))
                    {
                        this.view.Translation = new Vector3(0, 0, 0.5f) + this.view.Translation;
                        _currentTranslation.Z = _currentTranslation.Z - 0.5f;
                    } 
                    break;

                case "runter":
                    if (_currentTranslation.Z <= 20)
                    {
                        this.view.Translation = new Vector3(0, 0, -0.5f) + this.view.Translation;
                        _currentTranslation.Z = _currentTranslation.Z + 0.5f;
                    } 
                    break;

                default: break;
            }
        }

        /* Bewegt die Camera sich um den Angegebenen Wert in gegebener Richtung
         *      + Y - nach Unten
         *      - Y - nach Oben
         *      + X - nach Rechts
         *      - X - nach Links
         *      + zoom - hinein scrollen
         *      - zoom - heraus scrollen
         * Es werden jeweils die currentTRansaltion aktualisert
         */
        public void MoveCameraValue(string direction, float value)
        {

            switch (direction)
            {
                case "Y":
                    this.view.Translation = new Vector3(0, value, -1*value) + this.view.Translation; //camera nach hinte
                    _currentTranslation.Y = _currentTranslation.Y + value;
                    break;

                case "X":
                    this.view.Translation = new Vector3(-1*value, 0, 0) + this.view.Translation; //camera rechts
                    _currentTranslation.X = _currentTranslation.X + value;
                    break;

                case "zoom":
                    this.view.Translation = new Vector3(0, 0, value) + this.view.Translation;
                    _currentTranslation.Z = _currentTranslation.Z - value;
                    break;

                default: break;
            }
        }
    }
}
