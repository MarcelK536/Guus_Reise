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
        public GraphicsDevice _graphicsDevice;

        public Texture2D bkgTexture;
        public Color bkgColor = Color.BlanchedAlmond;
        public Vector2 bkgPos;

        public BlendDirection blendDirection;
        public List<Button> menuButtons= new List<Button>();
        public float menuWidth;
        public float menuHeight;
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

        /// <summary>
        ///     Der Grundaufbau für alle Menus
        /// </summary>
        /// <param name="position">Position ist die obere linke Ecke des Menus</param>
        /// <param name="menuFont">Die verwendetete Schriftart</param>
        /// <param name="graphicsDevice">Zum Berechnen der Hintergründe</param>
        /// <param name="direction">Richtung für die Animation zum Öffnen des Menus</param>
        public SimpleMenu(Vector2 position, SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction)
        {
            _graphicsDevice = graphicsDevice;
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
            menuHeight = menuButtons[menuButtons.Count-1].GetPosBelow().Y;

            SetBackgroundTexture(bkgColor);
            bkgPos = pos;
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
                if (btnClose.IsClicked() && needCloseBtn == true) 
                {
                    Active = false;
                }
            }
        }

        public virtual void SetMenuWidth()
        {
            foreach (Button btn in menuButtons)
            {
                if (menuWidth < btn.TextureDefault.Width)
                {
                    menuWidth = btn.TextureDefault.Width + 10;
                }
            }
        }

        public virtual void SetMenuHeight()
        {
            foreach(Button btn in menuButtons)
            {
                if(menuHeight < btn.GetPosBelow().Y)
                {
                    menuHeight = btn.GetPosBelow().Y;
                }
            }
        }

        public virtual void SetBackgroundTexture(Color color)
        {
            Texture2D menuBackground = new Texture2D(_graphicsDevice, (int)menuWidth, (int)menuHeight);
            Color[] bkgColor = new Color[menuBackground.Width * menuBackground.Height];
            for (int i = 0; i < bkgColor.Length; i++)
            {
                bkgColor[i] = color*0.3f;
            }
            menuBackground.SetData(bkgColor);
            bkgTexture = menuBackground;
        }
        public virtual void UpdatePosition(Vector2 newPos)
        {
            pos = newPos;
            btnClose.MoveButton(newPos);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (blendDirection == BlendDirection.None)
            {
                if (Active)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(bkgTexture, pos, Color.White);
                    spriteBatch.End();
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
                spriteBatch.Draw(bkgTexture, bkgPos, Color.White);
                btnClose.Draw(spriteBatch, textFont);
                spriteBatch.End();
            }
            else
            {
                foreach (Button button in menuButtons)
                {
                    button.ButtonX = (int)-menuWidth;
                }
                bkgPos.X = (int)-menuWidth;
            }
        }

        public virtual Vector2 GetPositionBelow(Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y + textFont.MeasureString("T").Y + 5); 
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
                if (button.ButtonX < pos.X)
                {
                    button.ButtonX += 6;
                }
            }
            if(bkgPos.X < pos.X)
            {
                bkgPos.X += 6;
            }

        }
    }
}
