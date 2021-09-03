using Microsoft.Xna.Framework;
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
            needCloseBtn = false;
            pos = new Vector2((_graphicsDevice.Viewport.Width / 2)-90,0);
            bkgPos = pos;
            btnClose.MoveButton(pos - Vector2.UnitX * btnClose.TextureDefault.Width);
            menuHeight = btnClose.TextureDefault.Height;
            menuWidth = 20;
        }

        public void Update(GameTime time)
        {
            if (btnClose.IsClicked())
            {
                Active = false;
            }
            btnClose.MoveButton(pos - Vector2.UnitX * btnClose.TextureDefault.Width);
            bkgPos = pos;
            if(menuHeight < textFont.MeasureString("Placeholder").Y * HexMap.lvlObjectiveText.Length)
            {
                menuHeight = textFont.MeasureString("Placeholder").Y * HexMap.lvlObjectiveText.Length;
            }
            else
            {
                menuHeight = btnClose.TextureDefault.Height;
            }
            foreach (string obj in HexMap.lvlObjectiveText) {
                if(textFont.MeasureString(obj).X > menuWidth)
                {
                    menuWidth = textFont.MeasureString(obj + "   Status: false " ).X ;
                }
            }
            pos.X = _graphicsDevice.Viewport.Width - HexMap.btSoundEinstellungen.TextureDefault.Width-10;
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
