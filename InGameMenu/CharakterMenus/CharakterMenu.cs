using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Guus_Reise
{
    class CharakterMenu : SimpleMenu
    {
        Button btnLevelUp;
        Button btnSkillWechsel;

        GraphicsDevice graphics;

        static SoundEffect _clickSound;
        bool preClickState;

        SkillMenu skillMenu;
        public CharakterMenu(SpriteFont menuFont, GraphicsDevice graphicsDevice, SoundEffect clickSound) : base(new Vector2(), menuFont, graphicsDevice, SimpleMenu.BlendDirection.None, _clickSound)
        {
            graphics = graphicsDevice;
            menuWidth = 600;
            menuHeight = 300;
            preClickState = false;
            pos = new Vector2((_graphicsDevice.Viewport.Width / 2) -(int)(menuWidth / 2), (_graphicsDevice.Viewport.Height / 2) - (int)(menuHeight / 2));
            bkgPos = pos;
            btnClose.MoveButton(pos);
            btnWidth = menuFont.MeasureString("Change Weapon ").X;
            Texture2D btnTexture = new Texture2D(graphicsDevice, (int)btnWidth, 50);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.YellowGreen * 0.8f;
            }
            btnTexture.SetData(btnColor);
            btnLevelUp = new Button("Skill Points", btnTexture, 1, btnClose.GetPosBelow());
            menuButtons.Add(btnLevelUp);
            btnSkillWechsel = new Button("Change Skills", btnTexture, 1, btnLevelUp.GetPosBelow());
            menuButtons.Add(btnSkillWechsel);
            _clickSound = clickSound;

        }

 
        public void Update(GameTime time)
        {
            pos = new Vector2((_graphicsDevice.Viewport.Width / 2) - (int)(menuWidth / 2), (_graphicsDevice.Viewport.Height / 2) - (int)(menuHeight / 2));
            bkgPos = pos;
            btnClose.MoveButton(pos);
            btnLevelUp.MoveButton(btnClose.GetPosBelow());
            btnSkillWechsel.MoveButton(btnLevelUp.GetPosBelow());
            SetBackgroundTexture(bkgColor);

            if (Active)
            {
                if (Player.activeTile == null)
                {
                    if (btnClose.IsClicked())
                    {
                        _clickSound.Play();
                        Active = false;
                        Player.levelUpMenu.Active = false;
                        Player.charakterMenu.Active = false;
                    }
                }
                else
                {
                    int x = Player.activeTile.LogicalBoardPosition.X;
                    int y = Player.activeTile.LogicalBoardPosition.Y;

                    if (skillMenu != null && skillMenu.Active)
                    {
                        skillMenu.Update(time);
                    }
                    else
                    {
                        if (btnClose.IsClicked())
                        {
                            _clickSound.Play();
                            Active = false;
                            Player.levelUpMenu.Active = false;
                            Player.charakterMenu.Active = false;
                            if (skillMenu != null)
                            {
                                skillMenu.Active = false;
                            }
                        }
                        if (HexMap._board[x, y].Charakter != null && HexMap._board[x, y].Charakter.IsNPC == false)
                        {
                            if (btnLevelUp.IsClicked())
                            {
                                _clickSound.Play();
                                Player.levelUpMenu.Active = true;
                                Player.charakterMenu.Active = false;

                            }
  
                                if (skillMenu != null)
                                {
                                    skillMenu.Active = false;
                                }
                         
                            if (btnSkillWechsel.IsClicked())
                            {

                                _clickSound.Play();
                                skillMenu = new SkillMenu(Skill.skills, btnSkillWechsel.GetPosRightOf(), textFont, graphics, SimpleMenu.BlendDirection.None, _clickSound);
                                skillMenu.Active = !skillMenu.Active;

                            }
                        }
                    }
                }
            }      
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Active)
            {
                if (Player.activeTile == null)
                {
                    spriteBatch.Begin();
                    menuWidth = btnClose.GetPosRightOf().X + textFont.MeasureString("No Character Selected").X;
                    menuHeight = btnClose.GetPosBelow().Y;
                    SetBackgroundTexture(bkgColor);
                    spriteBatch.DrawString(textFont, "Please select a Tile \nto see the charakter informations", btnClose.GetPosRightOf(), Color.Yellow);
                    spriteBatch.End();
                }
                else
                {
                    int x = Player.activeTile.LogicalBoardPosition.X;
                    int y = Player.activeTile.LogicalBoardPosition.Y;

                    if (HexMap._board[x, y].Charakter == null)
                    {
                        // spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, rasterizerState);
                        spriteBatch.Begin();
                        menuWidth = btnClose.GetPosRightOf().X + textFont.MeasureString("No character selected").X;
                        menuHeight = btnClose.GetPosBelow().Y;
                        SetBackgroundTexture(bkgColor);
                        spriteBatch.DrawString(textFont, "No charakter selected", btnClose.GetPosRightOf(), Color.Yellow);
                        spriteBatch.End();
                    }
                    else
                    {
                        Vector2 textPos = new Vector2(btnLevelUp.GetTextPosRightOf().X, btnClose.GetTextPosRightOf().Y);
                        //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, rasterizerState);
                        spriteBatch.Begin();
                        menuWidth = 600;
                        menuHeight = 300;
                        spriteBatch.DrawString(textFont, "Name: " + HexMap._board[x, y].Charakter.Name, textPos, Color.Yellow);
                        textPos.Y += textFont.MeasureString("Placeholder").Y * 1.5f;
                        spriteBatch.DrawString(textFont, "Level: " + HexMap._board[x, y].Charakter.Level, textPos, Color.Yellow);

                        if (HexMap._board[x, y].Charakter.IsNPC == false)
                        {
                            btnLevelUp.Draw(spriteBatch, textFont);
                            btnSkillWechsel.Draw(spriteBatch, textFont);
                        }
                        spriteBatch.End();
                    }

                    if (skillMenu != null && skillMenu.Active)
                    {
                        skillMenu.Draw(spriteBatch);
                    }
                }
            }
        }
    }
}
