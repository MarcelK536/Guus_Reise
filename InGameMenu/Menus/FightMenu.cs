using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class FightMenu : SimpleMenu
    {
        Button btnGiveUp;
        Button btnAttack1;
        Button btnAttack2;

        public FightMenu(SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(new Vector2(0,graphicsDevice.Viewport.Bounds.Center.Y), menuFont, graphicsDevice, direction)
        {
            //System.Diagnostics.Debug.WriteLine(graphicsDevice.Viewport.Bounds.Center.Y);
            needCloseBtn = false;
            btnWidth = menuFont.MeasureString("Attack 1").X + 10;
            Texture2D btnTexture = new Texture2D(graphicsDevice,(int) btnWidth, 50);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.Red * 0.8f;
            }
            btnTexture.SetData(btnColor);

            btnAttack1 = new Button("Attack 1", btnTexture, 1, pos);
            menuButtons.Add(btnAttack1);
            btnAttack2 = new Button("Attack 2", btnTexture, 1, btnAttack1.GetPosBelow());
            menuButtons.Add(btnAttack2);
            btnGiveUp = new Button("Give Up", btnTexture, 1, btnAttack2.GetPosBelow());
            menuButtons.Add(btnGiveUp);

            SetMenuHeight();
            SetMenuWidth();
            SetBackgroundTexture(Color.MistyRose);
        }

        public override void Update()
        {
            base.Update();
            if (Active)
            {
                int x = Player.activeTile.LogicalPosition.X;
                int y = Player.activeTile.LogicalPosition.Y;

                if (btnAttack1.IsClicked())
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
            btnAttack1.MoveButton(btnClose.GetPosBelow());
            btnAttack2.MoveButton(btnAttack1.GetPosBelow());
            btnGiveUp.MoveButton(btnAttack2.GetPosBelow());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Active)
            {
                spriteBatch.Begin();
                Vector2 textPosition = Vector2.Zero;
                foreach (Hex hex in Fighthandler.playerTiles)
                {
                    if (textPosition == Vector2.Zero)
                    {
                        textPosition = btnAttack1.GetPosRightOf();
                    }
                    else
                    {
                        textPosition = GetPositionBelow(GetPositionBelow(textPosition));
                    }
                    spriteBatch.DrawString(textFont, "Name: " + hex.Charakter.Name, textPosition, Color.Yellow);
                    spriteBatch.DrawString(textFont, "Widerstandskraft: " + hex.Charakter.Widerstandskraft, GetPositionBelow(textPosition), Color.Yellow);
                }
                btnAttack1.Draw(spriteBatch, textFont);
                btnAttack2.Draw(spriteBatch, textFont);
                btnGiveUp.Draw(spriteBatch, textFont);

                textPosition = Vector2.Zero;
                foreach (Hex hex in Fighthandler.npcTiles)
                {
                    if (textPosition == Vector2.Zero)
                    {
                        textPosition = btnAttack1.GetPosRightOf() + Vector2.UnitX*(btnAttack1.GetPosRightOf().X + 200);
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
