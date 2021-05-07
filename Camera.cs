using Microsoft.Xna.Framework;

namespace Guus_Reise
{
    class Camera
    {
        public Matrix view;
        public Matrix projection;

        public Camera(float aspectRatio)
        {
            view = Matrix.CreateLookAt(new Vector3(0, 10, 7), new Vector3(0, 0, 0), Vector3.UnitY);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(30), aspectRatio, 0.1f, 100f);
        }

        public void MoveCamera(int zähler)
        {
            switch (zähler)
            {
                case 1: this.view.Translation = new Vector3(0, -0.1f, 0) + this.view.Translation;
                    break;

                case 2: this.view.Translation = new Vector3(0, 0.1f, 0) + this.view.Translation;
                    break;

                case 3: this.view.Translation = new Vector3(0.1f, 0, 0) + this.view.Translation;
                    break;

                case 4: this.view.Translation = new Vector3(-0.1f, 0, 0) + this.view.Translation;
                    break;

                default: break;
            }
        }
    }
}
