using Microsoft.Xna.Framework;

namespace Guus_Reise
{
    class Camera
    {
        public Matrix view;
        public Matrix projection;

        public Camera(float aspectRatio)
        {
            view = Matrix.CreateLookAt(new Vector3(0, 5, 7), new Vector3(0, 0, 0), Vector3.UnitY);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(30), aspectRatio, 0.01f, 100f);
        }

        public void MoveCamera(string symbol)
        {
            switch (symbol)
            {
                case "w": this.view.Translation = new Vector3(0, -0.1f, 0.1f) + this.view.Translation; //bewegt camera nach vorne
                    break;

                case "s": this.view.Translation = new Vector3(0, 0.1f, -0.1f) + this.view.Translation; //camera nach hinte
                    break;

                case "a": this.view.Translation = new Vector3(0.1f, 0, 0) + this.view.Translation; //camera links
                    break;

                case "d": this.view.Translation = new Vector3(-0.1f, 0, 0) + this.view.Translation; //camera rechts
                    break;

                case "hoch": this.view.Translation = new Vector3(0, 0, 0.5f) + this.view.Translation;
                    break;

                case "runter": this.view.Translation = new Vector3(0, 0, -0.5f) + this.view.Translation;
                    break;

                default: break;
            }
        }
    }
}
