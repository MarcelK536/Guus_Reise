using Microsoft.Xna.Framework;

namespace Guus_Reise
{
    class Camera
    {
        public Matrix view;
        public Matrix projection;

        public Camera(float aspectRatio)
        {
            view = Matrix.CreateLookAt(new Vector3(0, 10, 10), new Vector3(0, 0, 0), Vector3.UnitY);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(30), aspectRatio, 0.1f, 100f);
        }
    }
}
