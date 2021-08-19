using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class CharakterMenu : SimpleMenu
    {
        Button btnLevelUp;
        Button btnWaffenWechsel;
        Button btnSkillWechsel;

        GraphicsDevice graphics;

        WeaponMenu weaponMenu;
        SkillMenu skillMenu;
        public CharakterMenu(SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(new Vector2(), menuFont, graphicsDevice, direction)
        {
            graphics = graphicsDevice;
            menuWidth = 600;
            menuHeight = 300;
            pos = new Vector2((_graphicsDevice.Viewport.Width / 2) -(int)(menuWidth / 2), (_graphicsDevice.Viewport.Height / 2) - (int)(menuHeight / 2));
            bkgPos = pos;
            btnClose.MoveButton(pos);
            btnWidth = menuFont.MeasureString("Faehigkeitenpunkte ").X;
            Texture2D btnTexture = new Texture2D(graphicsDevice, (int)btnWidth, 50);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.YellowGreen * 0.8f;
            }
            btnTexture.SetData(btnColor);
            btnLevelUp = new Button("Fähigkeitenpunkte", btnTexture, 1, btnClose.GetPosBelow());
            menuButtons.Add(btnLevelUp);
            btnWaffenWechsel = new Button("Waffenwechsel", btnTexture, 1, btnLevelUp.GetPosBelow());
            menuButtons.Add(btnWaffenWechsel);
            btnSkillWechsel = new Button("Skills tauschen", btnTexture, 1, btnWaffenWechsel.GetPosBelow());
            menuButtons.Add(btnSkillWechsel);
        }

        public void Update(GameTime time)
        {
            base.Update();
            pos = new Vector2((_graphicsDevice.Viewport.Width / 2) - (int)(menuWidth / 2), (_graphicsDevice.Viewport.Height / 2) - (int)(menuHeight / 2));
            bkgPos = pos;
            btnClose.MoveButton(pos);
            btnLevelUp.MoveButton(btnClose.GetPosBelow());
            btnWaffenWechsel.MoveButton(btnLevelUp.GetPosBelow());
            btnSkillWechsel.MoveButton(btnWaffenWechsel.GetPosBelow());
            SetBackgroundTexture(bkgColor);

            if (Active)
            {
                if(weaponMenu != null && weaponMenu.Active)
                {
                    weaponMenu.Update(time);
                }
                if(skillMenu != null && skillMenu.Active)
                {
                    skillMenu.Update(time);
                }
                else
                {
                    if (btnLevelUp.IsClicked())
                    {
                        Player.levelUpMenu.Active = true;
                        Player.charakterMenu.Active = false;
                    }
                    if (btnWaffenWechsel.IsClicked())
                    {
                        weaponMenu = new WeaponMenu(Weapon.weapons, btnWaffenWechsel.GetPosRightOf(), textFont, graphics, SimpleMenu.BlendDirection.None);
                        weaponMenu.Active = true;
                    }
                    if (btnSkillWechsel.IsClicked())
                    {
                        skillMenu = new SkillMenu(Skill.skills, btnSkillWechsel.GetPosRightOf(), textFont, graphics, SimpleMenu.BlendDirection.None);
                        skillMenu.Active = true;
                    }
                }
            }

            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Active)
            {
                int x = Player.activeTile.LogicalBoardPosition.X;
                int y = Player.activeTile.LogicalBoardPosition.Y;

                if (HexMap._board[x, y].Charakter == null)
                {
                    spriteBatch.Begin();
                    menuWidth = btnClose.GetPosRightOf().X + textFont.MeasureString("Kein Charakter ausgewählt").X;
                    menuHeight = btnClose.GetPosBelow().Y;
                    SetBackgroundTexture(bkgColor);
                    spriteBatch.DrawString(textFont, "Kein Charakter ausgewählt", btnClose.GetPosRightOf(), Color.Yellow);
                    spriteBatch.End();
                }
                else
                {
                    Vector2 textPos = new Vector2(btnLevelUp.GetTextPosRightOf().X,btnClose.GetTextPosRightOf().Y);
                    spriteBatch.Begin();
                    menuWidth = 600;
                    menuHeight = 300;
                    spriteBatch.DrawString(textFont,"Name: " + HexMap._board[x,y].Charakter.Name, textPos, Color.Yellow);
                    textPos.Y += textFont.MeasureString("Placeholder").Y*1.5f;
                    spriteBatch.DrawString(textFont,"Level: " + HexMap._board[x, y].Charakter.Level, textPos, Color.Yellow);
                    btnLevelUp.Draw(spriteBatch, textFont);
                    btnSkillWechsel.Draw(spriteBatch, textFont);
                    btnWaffenWechsel.Draw(spriteBatch, textFont);
                    spriteBatch.End();
                }

                if (weaponMenu != null && weaponMenu.Active)
                {
                    weaponMenu.Draw(spriteBatch);
                }
                if (skillMenu != null && skillMenu.Active)
                {
                    skillMenu.Draw(spriteBatch);
                }
            }
        }


    }
}
