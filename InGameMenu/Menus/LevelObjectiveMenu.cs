using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class LevelObjectiveMenu : SimpleMenu
    {
        bool objective1 = false;
        public LevelObjectiveMenu(SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(new Vector2(), menuFont, graphicsDevice, direction)
        {
            
            pos = new Vector2((_graphicsDevice.Viewport.Width / 2)-90,0);
            bkgPos = pos;
            btnClose.MoveButton(pos - Vector2.UnitX * btnClose.TextureDefault.Width);
            menuHeight = menuFont.MeasureString("Placeholder").Y;
            menuWidth = _graphicsDevice.Viewport.Width / 2;
        }

        public void Update(GameTime time)
        {
            base.Update();

            menuWidth = _graphicsDevice.Viewport.Width / 2 + 60;
            pos.X = _graphicsDevice.Viewport.Width / 2 - 90;
            btnClose.MoveButton(pos - Vector2.UnitX * btnClose.TextureDefault.Width);
            bkgPos = pos;
            SetBackgroundTexture(bkgColor);
            

            var found = HexMap.playableCharacter.Find(c => c.LogicalBoardPosition == HexMap._board[7, 7].LogicalBoardPosition);
            if(found != null)
            {
                objective1 = true;
            }
            else 
            {
                objective1 = false; 
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            base.Draw(spriteBatch);

            if (Active)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(textFont, "Go to the Bottom Right Tile. Status: " + objective1.ToString(), pos, Color.Yellow);
                spriteBatch.End();
            }
        }
    }
}
