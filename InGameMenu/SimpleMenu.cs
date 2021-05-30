using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Guus_Reise
{
    class SimpleMenu
    {
        protected bool _active;
        public Vector2 pos;
        public Texture2D bkgTexture;
        public SpriteFont textFont;
        public Button btnClose;
        public float btnWidth;

        public SimpleMenu(Vector2 position, Texture2D background, SpriteFont menuFont, GraphicsDevice graphicsDevice)
        {
            pos = position;
            textFont = menuFont;
            btnWidth = menuFont.MeasureString("Close").X+10;
            bkgTexture = background;
            Texture2D btnTexture = new Texture2D(graphicsDevice, (int)btnWidth, 50);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.White;
            }
            btnTexture.SetData(btnColor);
            btnClose = new Button("Close", btnTexture, 1, (int)pos.X, (int)pos.Y);
            

        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public virtual void Update()
        {
            if (Active)
            {
                if (btnClose.IsClicked()) 
                {
                    Active = false;
                }
            }
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(bkgTexture, pos, Color.Black);
                btnClose.Draw(spriteBatch, textFont);
                spriteBatch.End();
            }
        }

        public virtual void Close(object INFO)
        {
            Active = false;
        }
    }
}
