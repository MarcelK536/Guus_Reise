﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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


        static List<SimpleMenu> allInstances = new List<SimpleMenu>();
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
                btnColor[i] = Color.White*0.5f;
            }
            btnTexture.SetData(btnColor);
            btnClose = new Button("Close", btnTexture, 1, (int)pos.X, (int)pos.Y);

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
    }
}
