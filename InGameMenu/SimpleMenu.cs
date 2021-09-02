using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.Timers;
using Microsoft.Xna.Framework.Audio;

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
        public Rectangle tempScissorRect;
        public RasterizerState rasterizerState;
        public bool defaultSet;
        public float tempWidth;
        public float tempHeight;
        public bool needCloseBtn = true;

        SoundEffect _clickSound;
        

        
        public enum BlendDirection
        {
            None,
            LeftToRight,
            RightToLeft,
            TopToBottom,
            BottomToTop,
            InsideOut,
            OutsideIn
        }

        static List<SimpleMenu> allInstances = new List<SimpleMenu>();



        /// <summary>
        ///     Der Grundaufbau für alle Menus
        /// </summary>
        /// <param name="position">Position ist die obere linke Ecke des Menus</param>
        /// <param name="menuFont">Die verwendetete Schriftart</param>
        /// <param name="graphicsDevice">Zum Berechnen der Hintergründe</param>
        /// <param name="direction">Richtung für die Animation zum Öffnen des Menus</param>
        public SimpleMenu(Vector2 position, SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction, SoundEffect clickSound)
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
            if (needCloseBtn == true)
            {
                bkgPos = pos;
            }
            else 
            {
                bkgPos = btnClose.GetPosBelow();
            }
            allInstances.Add(this);
            _clickSound = clickSound;
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public virtual void Update()
        {
            /*if (Active)
            {
                if (btnClose.IsClicked() && needCloseBtn == true || ClickedOutside()) 
                {
                    Active = false;
                }
            } */
        }

        public virtual bool ClickedOutside()
        {
            //TODO: Implement function, currently Blending does not work
            /* 
            MouseState mouseState = Mouse.GetState();
            if (mouseState.X < pos.X || mouseState.Y < pos.Y || mouseState.X > pos.X+menuWidth || mouseState.Y > pos.Y + menuHeight)
            {
                if(mouseState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            */
            return false;
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

        public virtual void SetBackgroundTexture(Color color, float transparence)
        {
            Texture2D menuBackground = new Texture2D(_graphicsDevice, (int)menuWidth, (int)menuHeight);
            Color[] bkgColor = new Color[menuBackground.Width * menuBackground.Height];
            for (int i = 0; i < bkgColor.Length; i++)
            {
                bkgColor[i] = color * transparence;
            }
            menuBackground.SetData(bkgColor);
            bkgTexture = menuBackground;
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

        public virtual void SetBackgroundTexturePicture(Texture2D texPicture)
        {
            Texture2D menuBackground = texPicture;
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
                    //spriteBatch.Draw(bkgTexture, bkgPos, Color.White);
                    spriteBatch.Draw(bkgTexture, new Rectangle((int)bkgPos.X, (int)bkgPos.Y, (int)menuWidth, (int)menuHeight), Color.White);
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
            else if(blendDirection == BlendDirection.TopToBottom)
            {
                DrawBlendTopToBottom(spriteBatch);
            }
            else if(blendDirection == BlendDirection.InsideOut)
            {
                DrawInsideOut(spriteBatch);
            }
            else if(blendDirection == BlendDirection.OutsideIn)
            {
                DrawOutsideIn(spriteBatch);
            }
        }

        private void DrawBlendLeftToRight(SpriteBatch spriteBatch)
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

        private void DrawBlendTopToBottom(SpriteBatch spriteBatch)
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
                    button.ButtonY = (int)-menuHeight;
                }
                bkgPos.Y = (int)-menuHeight;
            }
        }
        
        private void DrawInsideOut(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                if (defaultSet == false)
                {
                    tempScissorRect = spriteBatch.GraphicsDevice.ScissorRectangle;
                    tempWidth = 0f;
                    tempHeight = 0f;
                    defaultSet = true;
                }

                rasterizerState = new RasterizerState() { ScissorTestEnable = true };

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, rasterizerState);

                spriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle((int)pos.X, (int)pos.Y, (int)tempWidth, (int)tempHeight);
                
                if(tempWidth <= menuWidth)
                {
                    tempWidth += menuWidth / menuHeight;
                }
                if(tempHeight <= menuHeight)
                {
                    tempHeight += menuHeight / menuWidth;
                }

                spriteBatch.Draw(bkgTexture, bkgPos, Color.White);
                btnClose.Draw(spriteBatch, textFont);

                spriteBatch.GraphicsDevice.ScissorRectangle = tempScissorRect;

                spriteBatch.End();
            }
            else
            {
                spriteBatch.GraphicsDevice.ScissorRectangle = tempScissorRect;
                defaultSet = false;
            }
        }

        private void DrawOutsideIn(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                if (defaultSet == false)
                {
                    tempScissorRect = spriteBatch.GraphicsDevice.ScissorRectangle;
                    tempWidth = menuWidth;
                    tempHeight = menuHeight;
                    defaultSet = true;
                }

                rasterizerState = new RasterizerState() { ScissorTestEnable = true };

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, rasterizerState);

                spriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle((int)pos.X, (int)pos.Y, (int)tempWidth, (int)tempHeight);

                if (tempWidth >= menuWidth)
                {
                    tempWidth -= menuWidth / menuHeight;
                }
                if (tempHeight >= menuHeight)
                {
                    tempHeight -= menuHeight / menuWidth;
                }

                spriteBatch.Draw(bkgTexture, bkgPos, Color.White);
                btnClose.Draw(spriteBatch, textFont);

                spriteBatch.GraphicsDevice.ScissorRectangle = tempScissorRect;

                spriteBatch.End();
            }
            else
            {
                spriteBatch.GraphicsDevice.ScissorRectangle = tempScissorRect;
                defaultSet = false;
            }
        }

        public virtual Vector2 GetPositionBelow(Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y + textFont.MeasureString("T").Y + 5); 
        }

        public static void DeactivateAllOtherMenus(SimpleMenu? exceptMenu)
        {
            foreach (SimpleMenu menu in allInstances)
            {
                if(exceptMenu != null && menu == exceptMenu)
                {
                    continue;
                }
                menu.Active = false;
            }
        }

        public static bool CheckIfAnyMenuOpen(SimpleMenu? exceptMenu) 
        {
            foreach (SimpleMenu menu in allInstances)
            {
                if (exceptMenu != null && menu == exceptMenu)
                {
                    continue;
                }
                if (menu.Active == true)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void BlendIn()
        {
            if (blendDirection == BlendDirection.LeftToRight)
            {
                foreach (Button button in menuButtons)
                {
                    if (button.ButtonX < pos.X)
                    {
                        button.ButtonX += 6;
                    }
                }
                if (bkgPos.X < pos.X)
                {
                    bkgPos.X += 6;
                }
            }
            if (blendDirection == BlendDirection.TopToBottom)
            {
                foreach(Button button in menuButtons)
                {
                    if(button.ButtonY < pos.Y)
                    {
                        button.ButtonY += 6;
                    }
                }
                if(bkgPos.Y < pos.Y)
                {
                    bkgPos.Y += 6;
                }
            }
        }
    }
}
