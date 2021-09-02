using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class ControlView : SimpleMenu

    {
        public ControlView(Vector2 position, SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(position, menuFont, graphicsDevice, direction)
        {
            Vector2 menuWH = menuFont.MeasureString("Mouse Controls: \n   -Left Click = Select Tile / Action \n   -Right Click = DeSelect Tile \n\n " +
                    "Keyboard Controls: \n   -G = Show Level Goal\n   -H = Show Character Info\n -R = Center Map\n   -ESC - Quit Menu");
            menuWidth = menuWH.X;
            menuHeight = menuWH.Y;
        }
        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Active)
            {
                RasterizerState rasterizerState = new RasterizerState() { ScissorTestEnable = true };

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, rasterizerState);

                Rectangle tempScissorRect = spriteBatch.GraphicsDevice.ScissorRectangle;

                spriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle((int)pos.X, (int)pos.Y, (int)menuWidth, (int)menuHeight);

                spriteBatch.DrawString(textFont, "Mouse Controls: \n   -Left Click = Select Tile / Action \n   -Right Click = DeSelect Tile \n\n " +
                    "Keyboard Controls: \n   -G = Show Level Goal\n   -H = Show Character Info\n -R = Center Map\n   -ESC - Quit Menu", btnClose.GetPosBelow(), Color.Black);

                spriteBatch.GraphicsDevice.ScissorRectangle = tempScissorRect;
                spriteBatch.End();
            }

        }
    }
}
