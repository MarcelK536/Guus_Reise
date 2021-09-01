using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guus_Reise
{
    class WeaponMenu : SimpleMenu
    {
        Texture2D btnTexture;
        Texture2D btnTextureSelected;
        int lastWheel = 0;
        public WeaponMenu(List<Weapon> weapons, Vector2 position, SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(position, menuFont, graphicsDevice, direction)
        {
            menuWidth = 200;
            menuHeight = 200;
            btnWidth = 15;
            Vector2 btnPosition = btnClose.GetPosBelow();

            foreach (Weapon item in weapons)
            {
                if(btnWidth < textFont.MeasureString(item.Name).X)
                {
                    btnWidth = textFont.MeasureString(item.Name).X;
                }
            }
            menuWidth = btnWidth;
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

            foreach (Weapon item in weapons)
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
                if(btn.Name == HexMap._board[x, y].Charakter.Weapon.Name)
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
                    int weaponIndex = HexMap._board[x, y].Charakter.WeaponInv.IndexOf(HexMap._board[x, y].Charakter.WeaponInv.Where(p => p.Name == btn.Name).First());
                    HexMap._board[x, y].Charakter.Weapon = HexMap._board[x, y].Charakter.WeaponInv[weaponIndex];
                }
            }

            if (btnClose.IsClicked())
            {
                Active = false;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Up) || Mouse.GetState().ScrollWheelValue > lastWheel)&& menuButtons[0].ButtonY + menuButtons[0].TextureDefault.Height > menuButtons[1].ButtonY)
            {
                UpdateButtons(btnTexture.Height);
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Down) || Mouse.GetState().ScrollWheelValue < lastWheel) && menuButtons.Last().GetPosBelow().Y + btnTexture.Height > menuHeight + pos.Y)
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
