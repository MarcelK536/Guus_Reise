using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guus_Reise
{
    class SkillMenu : SimpleMenu
    {
        Texture2D btnTexture;
        Texture2D btnTextureSelected;
        public SkillMenu(List<Skill> skills, Vector2 position, SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(position, menuFont, graphicsDevice, direction)
        {
            menuWidth = 200;
            menuHeight = 200;
            btnWidth = 15;
            Vector2 btnPosition = btnClose.GetPosBelow();

            foreach (Skill item in skills)
            {
                if(btnWidth < textFont.MeasureString(item.Name).X)
                {
                    btnWidth = textFont.MeasureString(item.Name).X;
                }
            }
            btnTexture = new Texture2D(graphicsDevice, (int)btnWidth, 25);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.YellowGreen * 0.8f;
            }
            btnTexture.SetData(btnColor);
            btnTextureSelected = new Texture2D(graphicsDevice, (int)btnWidth, 25);
            Color[] btnColorSelected = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColorSelected[i] = Color.Yellow * 0.8f;
            }
            btnTextureSelected.SetData(btnColorSelected);

            foreach (Skill item in skills)
            {
                menuButtons.Add(new Button(item.Name, btnTexture, 1, btnPosition));
                btnPosition.Y += btnTexture.Height + 10;
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Active)
            {
                RasterizerState rasterizerState = new RasterizerState() { ScissorTestEnable = true };
                
                spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend,null,null,rasterizerState);

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

        public void Update(GameTime time) 
        {
            int x = Player.activeTile.LogicalBoardPosition.X;
            int y = Player.activeTile.LogicalBoardPosition.Y;

            SetBackgroundTexture(Color.CornflowerBlue, 1f);

            foreach(Button btn in menuButtons)
            {
                if (btn.Name == "Close")
                {
                    continue;
                }
                if(HexMap._board[x, y].Charakter.Skills.Where(p => p.Name == btn.Name).Any())
                {
                    btn.TextureDefault = btnTextureSelected;
                    btn.TextureHover = btnTextureSelected;
                }
                else
                {
                    btn.TextureDefault = btnTexture;
                    btn.TextureHover = btnTexture;
                }
                if (btn.IsClicked())
                {
                    if(HexMap._board[x, y].Charakter.Skills.Where(p => p.Name == btn.Name).Any())
                    {
                        HexMap._board[x, y].Charakter.Skills.Remove(Skill.skills.Where(p => p.Name == btn.Name).First());
                    }
                    else if(HexMap._board[x,y].Charakter.Skills.Count < 4)
                    {
                        HexMap._board[x, y].Charakter.Skills.Add(Skill.skills.Where(p => p.Name == btn.Name).First());
                    }
                }
            }

            if (btnClose.IsClicked())
            {
                Active = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                UpdateButtons(btnTexture.Height);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                UpdateButtons(-btnTexture.Height);
            }
        }

        public void UpdateButtons(int yDirection)
        {
            foreach (Button btn in menuButtons)
            {
                if(btn.Name == "Close")
                {
                    continue;
                }
                btn.ButtonY += yDirection;
            }
        }
    }
}
