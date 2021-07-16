﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guus_Reise
{
    class AttackMenu : SimpleMenu
    {
        Charakter currCharakter;
        Texture2D btnTexture;
        public AttackMenu(Vector2 position, SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(position, menuFont, graphicsDevice, direction)
        {
            menuWidth = 200;
            menuHeight = 200;
            btnWidth = 25;
            Vector2 btnPosition = btnClose.GetPosBelow();

            int x = Player.activeTile.LogicalPosition.X;
            int y = Player.activeTile.LogicalPosition.Y;
            currCharakter = HexMap._board[x, y].Charakter;

            foreach (Skill s in currCharakter.Skills)
            {
                if (btnWidth < textFont.MeasureString(s.Name).X)
                {
                    btnWidth = textFont.MeasureString(s.Name).X;
                }
            }
            btnTexture = new Texture2D(graphicsDevice, (int)btnWidth, 25);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.YellowGreen * 0.8f;
            }
            btnTexture.SetData(btnColor);

            foreach (Skill s in currCharakter.Skills)
            {
                menuButtons.Add(new Button(s.Name, btnTexture, 1, btnPosition));
                btnPosition.Y += btnTexture.Height + 10;
            }
        }

        public void Update(GameTime time) 
        {
            foreach (Button btn in menuButtons)
            {
                if (btn.Name == "Close")
                {
                    continue;
                }
                if (btn.IsClicked())
                {
                    Skill selSkill = Skill.skills.Where(p => p.Name == btn.Name).First();
                    //ToDo Select Charakter
                }
            }
            if (btnClose.IsClicked())
            {
                Active = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            RasterizerState rasterizerState = new RasterizerState() { ScissorTestEnable = true };

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, rasterizerState);

            Rectangle tempScissorRect = spriteBatch.GraphicsDevice.ScissorRectangle;

            spriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle((int)pos.X, (int)pos.Y, (int)menuWidth, (int)menuHeight);

            foreach (Button btn in menuButtons)
            {
                btn.Draw(spriteBatch, textFont);
            }

            spriteBatch.GraphicsDevice.ScissorRectangle = tempScissorRect;
            spriteBatch.End();
        }
    }
}
