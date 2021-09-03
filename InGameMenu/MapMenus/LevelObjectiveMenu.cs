﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
namespace Guus_Reise
{
    class LevelObjectiveMenu : SimpleMenu
    {
        static SoundEffect _clickSound;
        public LevelObjectiveMenu(SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction, SoundEffect clickSound) : base(new Vector2(), menuFont, graphicsDevice, direction, _clickSound)
        {
            
            pos = new Vector2((_graphicsDevice.Viewport.Width / 2)-90,0);
            bkgPos = pos;
            btnClose.MoveButton(pos - Vector2.UnitX * btnClose.TextureDefault.Width);
            menuHeight = menuFont.MeasureString("Placeholder").Y * HexMap.lvlObjectiveText.Length;
            menuWidth = _graphicsDevice.Viewport.Width / 2;
        }

        public void Update(GameTime time)
        {
            if (btnClose.IsClicked())
            {
                Active = false;
            }
            menuWidth = _graphicsDevice.Viewport.Width / 2 + 60;
            pos.X = _graphicsDevice.Viewport.Width / 2 - 90;
            btnClose.MoveButton(pos - Vector2.UnitX * btnClose.TextureDefault.Width);
            bkgPos = pos;
            SetBackgroundTexture(bkgColor);
        }

        public void Draw(SpriteBatch spriteBatch, bool[] objectives, string[] objText)
        {          
            base.Draw(spriteBatch);
            Vector2 orgPos = pos;

            if (Active)
            {
                spriteBatch.Begin();
                for(int i=0; i<objectives.Length;i++)
                {
                    spriteBatch.DrawString(textFont, objText[i]+" Status: " + objectives[i], pos, Color.Yellow);
                    pos.Y += textFont.MeasureString("Placeholder").Y;
                }
                pos = orgPos;
                spriteBatch.End();
            }
        }
    }
}
