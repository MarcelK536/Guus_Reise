﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class FightMenu : SimpleMenu
    {
        Button btnGiveUp;
        Button btnAttack;
        Button btnChangeWeapon;

        public FightMenu(SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(new Vector2(0,graphicsDevice.Viewport.Bounds.Center.Y), menuFont, graphicsDevice, direction)
        {
            needCloseBtn = false;
            btnWidth = menuFont.MeasureString("Change Weapon").X + 10;
            Texture2D btnTexture = new Texture2D(graphicsDevice,(int) btnWidth, 50);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.Red * 0.8f;
            }
            btnTexture.SetData(btnColor);

            btnAttack = new Button("Attack", btnTexture, 1, pos);
            menuButtons.Add(btnAttack);
            btnChangeWeapon = new Button("Change Weapon", btnTexture, 1, btnAttack.GetPosBelow());
            menuButtons.Add(btnChangeWeapon);
            btnGiveUp = new Button("Give Up", btnTexture, 1, btnChangeWeapon.GetPosBelow());
            menuButtons.Add(btnGiveUp);

            SetMenuHeight();
            menuWidth = graphicsDevice.Viewport.Width;
            SetBackgroundTexture(Color.Green);
        }

        public override void Update()
        {
            base.Update();
            if (Active)
            {
                int x = Player.activeTile.LogicalPosition.X;
                int y = Player.activeTile.LogicalPosition.Y;

                if (btnAttack.IsClicked())
                {
                    Fighthandler._fightBoard[x, y].Charakter.Widerstandskraft++;    
                }
                if (btnGiveUp.IsClicked())
                {
                    Game1.GState = Game1.GameState.InGame;
                    Fighthandler.ExitFight();
                }
            }
            UpdatePosition(new Vector2(0, _graphicsDevice.Viewport.Bounds.Center.Y));
            SetMenuHeight();
            bkgPos.Y = _graphicsDevice.Viewport.Height / 2;
            menuWidth = _graphicsDevice.Viewport.Width;
            btnAttack.MoveButton(btnClose.GetPosBelow());
            btnChangeWeapon.MoveButton(btnAttack.GetPosBelow());
            btnGiveUp.MoveButton(btnChangeWeapon.GetPosBelow());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Active)
            {
                SetBackgroundTexture(Color.Green);
                spriteBatch.Begin();
                Vector2 textPosition = Vector2.Zero;
                foreach (Hex hex in Fighthandler.playerTiles)
                {
                    if (textPosition == Vector2.Zero)
                    {
                        textPosition = btnAttack.GetPosRightOf();
                    }
                    else
                    {
                        textPosition = GetPositionBelow(GetPositionBelow(textPosition));
                    }
                    spriteBatch.DrawString(textFont, "Name: " + hex.Charakter.Name, textPosition, Color.Yellow);
                    spriteBatch.DrawString(textFont, "Widerstandskraft: " + hex.Charakter.Widerstandskraft, GetPositionBelow(textPosition), Color.Yellow);
                }
                btnAttack.Draw(spriteBatch, textFont);
                btnChangeWeapon.Draw(spriteBatch, textFont);
                btnGiveUp.Draw(spriteBatch, textFont);

                textPosition = Vector2.Zero;
                foreach (Hex hex in Fighthandler.npcTiles)
                {
                    if (textPosition == Vector2.Zero)
                    {
                        textPosition = btnAttack.GetPosRightOf() + Vector2.UnitX*(btnAttack.GetPosRightOf().X + 200);
                    }
                    else
                    {
                        textPosition = GetPositionBelow(GetPositionBelow(textPosition));
                    }
                    spriteBatch.DrawString(textFont, "Name: " + hex.Charakter.Name, textPosition, Color.Yellow);
                    spriteBatch.DrawString(textFont, "Widerstandskraft: " + hex.Charakter.Widerstandskraft, GetPositionBelow(textPosition), Color.Yellow);
                }

                spriteBatch.End();
            }
        }
    }
}
