using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Guus_Reise
{
    class MoveMenu : SimpleMenu
    {
        public GraphicsDevice GraphicsDevice{ get; }
        Button btnConfirm;
        Button btnAttack;
        Button btnQuitGame;
        Button btnInteract;
        public bool fightTrue;
        public bool interactTrue;

        public MoveMenu(SpriteFont moveMenuFont, GraphicsDevice graphicsDevice) : base(new Vector2(), new Texture2D(graphicsDevice, 350, 600), moveMenuFont,graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
            btnWidth = moveMenuFont.MeasureString("Confirm Move").X + 10;
            Texture2D btnTexture = new Texture2D(graphicsDevice, (int)btnWidth, 50);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.Aquamarine;
            }
            btnTexture.SetData(btnColor);
            btnConfirm = new Button("Confirm Move", btnTexture, 1, btnClose.GetPosBelow());
            btnAttack = new Button("Attack", btnTexture, 1, btnConfirm.GetPosBelow());
            btnInteract = new Button("Iteract", btnTexture, 1, btnAttack.GetPosBelow());
            btnQuitGame = new Button("Quit Game", btnTexture, 1, btnInteract.GetPosBelow());
        }

        public void Update(Tile[,] _board, Tile activeTile, Tile moveTile)
        {
            base.Update();
            if (Active)
            {
                _board[moveTile.LogicalPosition.X, moveTile.LogicalPosition.Y].Glow = new Vector3(0.5f, 0.5f, 0.5f);

                if (btnQuitGame.IsClicked())
                {
                    Game1.GState = Game1.GameState.MainMenu;
                }
                if (btnConfirm.IsClicked())
                {
                    _board[moveTile.LogicalPosition.X, moveTile.LogicalPosition.Y].Charakter = _board[activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y].Charakter;
                    _board[activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y].Charakter = null;
                    this.Active =! this.Active;
                    HexMap.activeTile = null;
                    HexMap.moveTile = null;
                    fightTrue = false;
                }
                if (fightTrue)
                {
                    if (btnAttack.IsClicked())
                    {
                        //TODO FIGHT
                    }
                }
                if (interactTrue)
                {
                    if (btnInteract.IsClicked())
                    {
                        //TODO INTERACT
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Active)
            {
                spriteBatch.Begin();
                btnConfirm.Draw(spriteBatch,textFont);

                if (!fightTrue && !interactTrue)
                {
                    btnQuitGame.MoveButton(btnConfirm.GetPosBelow());
                }
                else
                {
                    if (fightTrue)
                    {
                        btnAttack.Draw(spriteBatch, textFont);
                        btnQuitGame.MoveButton(btnAttack.GetPosBelow());
                        if (interactTrue)
                        {
                            btnInteract.Draw(spriteBatch, textFont);
                            btnQuitGame.MoveButton(btnInteract.GetPosBelow());
                        }
                    }
                    else
                    {
                        if (interactTrue)
                        {
                            btnInteract.MoveButton(btnConfirm.GetPosBelow());
                            btnInteract.Draw(spriteBatch, textFont);
                            btnQuitGame.MoveButton(btnInteract.GetPosBelow());
                        }
                    }
                }
                
                btnQuitGame.Draw(spriteBatch, textFont);
                spriteBatch.End();
            }
        }
    }
}
