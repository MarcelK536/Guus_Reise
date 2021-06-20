using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Timers;

namespace Guus_Reise
{
    class SimpleMenu
    {
        protected bool _active;
        public Vector2 pos;
        public SpriteFont textFont;
        public Button btnClose;
        public float btnWidth;

        public BlendDirection blendDirection;
        public List<Button> menuButtons= new List<Button>();
        public float menuWidth;
        public bool needCloseBtn = true;
        
        public enum BlendDirection
        {
            None,
            LeftToRight,
            RightToLeft,
            TopToBottom,
            BottomToTop
        }

        static List<SimpleMenu> allInstances = new List<SimpleMenu>();
        public SimpleMenu(Vector2 position, SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction)
        {
            pos = position;
            textFont = menuFont;
            btnWidth = menuFont.MeasureString("Close").X + 10;
            Texture2D btnCloseTexture = new Texture2D(graphicsDevice, (int)btnWidth, 50);
            Color[] btnColor = new Color[btnCloseTexture.Width * btnCloseTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.White*0.5f;
            }
            btnCloseTexture.SetData(btnColor);
            btnClose = new Button("Close", btnCloseTexture, 1, (int)pos.X, (int)pos.Y);
            menuButtons.Add(btnClose);

            blendDirection = direction;
            menuWidth = menuButtons[menuButtons.Count-1].TextureDefault.Width;

            allInstances.Add(this);
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
            if (blendDirection == BlendDirection.None)
            {
                if (Active)
                {
                    if (needCloseBtn == true)
                    {
                        spriteBatch.Begin();
                        btnClose.Draw(spriteBatch, textFont);
                        spriteBatch.End();
                    }
                }
            }
            else if(blendDirection == BlendDirection.LeftToRight)
            {
                DrawBlendLeftToRight(spriteBatch);
            }

        }

        public virtual void DrawBlendLeftToRight(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                BlendIn();
                spriteBatch.Begin();
                btnClose.Draw(spriteBatch, textFont);
                spriteBatch.End();
            }
            else
            {
                foreach (Button button in menuButtons)
                {
                    button.ButtonX = (int)-menuWidth;
                }
            }
        }

        public virtual Vector2 GetPositionBelow(Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y + textFont.MeasureString("T").Y + 5); 
        }

        public virtual void Close(object INFO)
        {
            Active = false;
        }

        public static void DeactivateAllOtherMenus(SimpleMenu activeMenu)
        {
            foreach (SimpleMenu menu in allInstances)
            {
                if(menu == activeMenu)
                {
                    continue;
                }
                menu.Active = false;
            }
        }

        public static bool CheckIfAnyMenuOpen() 
        {
            foreach (SimpleMenu menu in allInstances)
            {
                if(menu.Active == true)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void BlendIn()
        {
            foreach (Button button in menuButtons)
            {
                if (button.ButtonX < 0)
                {
                    button.ButtonX += 2;
                }
            }
        }
    }
}
