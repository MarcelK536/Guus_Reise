using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using static Guus_Reise.Game1;
using Guus_Reise.HexangonMap;
using Guus_Reise.Animation;
using Microsoft.Xna.Framework.Audio;

namespace Guus_Reise
{
    class MoveMenu : SimpleMenu
    {
        Button btnConfirm;
        Button btnAttack;
        Button btnInteract;
        public bool fightTrue;
        public bool interactTrue;
        static SoundEffect _clickSound;

        static public Texture2D menuTexture { get; set; }

        public MoveMenu(SpriteFont moveMenuFont, GraphicsDevice graphicsDevice, BlendDirection blend, SoundEffect clickSound) : base(new Vector2(), moveMenuFont,graphicsDevice, blend, _clickSound)
        {
            btnWidth = moveMenuFont.MeasureString("Confirm Move").X + 10;
            Texture2D btnTexture = new Texture2D(graphicsDevice, (int)btnWidth, 50);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.Aquamarine*0.5f;
            }
            btnTexture.SetData(btnColor);
            btnConfirm = new Button("Confirm Move", btnTexture, 1, btnClose.GetPosBelow());
            menuButtons.Add(btnConfirm);
            btnAttack = new Button("Attack", btnTexture, 1, btnConfirm.GetPosBelow());
            menuButtons.Add(btnAttack);
            btnInteract = new Button("Interact", btnTexture, 1, btnAttack.GetPosBelow());
            menuButtons.Add(btnInteract);

            SetMenuHeight();
            SetMenuWidth();
            SetBackgroundTexture(Color.GhostWhite);
            _clickSound = clickSound;
        }

        public void Update(GameTime gametime)
        {
            base.Update();
            menuHeight = btnConfirm.GetPosBelow().Y;
            if (fightTrue)
            {
                menuHeight = btnAttack.GetPosBelow().Y;
            }
            if (interactTrue)
            {
                menuHeight = btnInteract.GetPosBelow().Y;
            }

            SetBackgroundTexture(bkgColor);
            if (Active)
            {
                HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Tile.Glow = new Vector3(0.5f, 0.5f, 0.5f);
                Hex targetHex = Player.moveTile;
                Hex startHex = Player.activeTile;
                if (btnClose.IsClicked() && needCloseBtn == true || ClickedOutside())
                {
                    _clickSound.Play();
                    Active = false;
                }
                if (btnConfirm.IsClicked())
                {
                    _clickSound.Play();
                    HexMap._board[Player.activeTile.LogicalPosition.X, Player.activeTile.LogicalPosition.Y].Charakter.GaveUp = false;
                    HexMap._board[Player.activeTile.LogicalPosition.X, Player.activeTile.LogicalPosition.Y].Charakter.CharakterAnimation.Hexagon = HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y];
                    HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Charakter = HexMap._board[Player.activeTile.LogicalPosition.X, Player.activeTile.LogicalPosition.Y].Charakter;
                    if (Player.moveTile != Player.activeTile)
                    {
                        HexMap._board[Player.activeTile.LogicalPosition.X, Player.activeTile.LogicalPosition.Y].Charakter = null;
                    }
                    HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Charakter.LogicalPosition = HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].LogicalPosition;
                    HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Charakter.LogicalBoardPosition = HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].LogicalPosition;
                    HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Charakter.CanMove = false;
                    
                    this.Active =! this.Active;
                    Player.activeTile.IsActive = false;
                    Player.activeTile = null;
                    HexMap.activeHex = null;

                    Player.moveTile = null;
                    fightTrue = false;
                    interactTrue = false;

                    // Movement Animation starten
                    MovementAnimationManager.Init("CharakterMovement", startHex, targetHex);
                    
                    //Update TurnCounter for Level 3-1
                    if(LevelHandler.currentLevel == 1 || LevelHandler.currentWorld == 3)
                    {
                        LevelDatabase.W3L1curRound++;
                    }
                    
                }
                if (fightTrue)
                {
                    if (btnAttack.IsClicked() && HexMap._board[Player.activeTile.LogicalPosition.X, Player.activeTile.LogicalPosition.Y].Charakter.GaveUp == false)
                    {
                        _clickSound.Play();
                        HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Charakter = HexMap._board[Player.activeTile.LogicalPosition.X, Player.activeTile.LogicalPosition.Y].Charakter;
                        if (Player.activeTile != Player.moveTile)
                        {
                            HexMap._board[Player.activeTile.LogicalPosition.X, Player.activeTile.LogicalPosition.Y].Charakter = null;
                        }
                        HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Charakter.LogicalPosition = HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].LogicalPosition;
                        HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Charakter.LogicalBoardPosition = HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].LogicalPosition;
                        HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Charakter.CanMove = false;
                       
                        Player.activeTile = Player.moveTile;

                        Fighthandler.npcTiles = HexMap.enemyNeighbourTiles;
                        Fighthandler.playerTiles = HexMap.friendNeighbourTiles;
                        Fighthandler.playerTiles.Add(Player.activeTile);
                        GState = Game1.GameState.InFight;
                    }
                }
                if (interactTrue)
                {
                    if (btnInteract.IsClicked() && HexMap._board[Player.activeTile.LogicalPosition.X, Player.activeTile.LogicalPosition.Y].Charakter.GaveUp == false)
                    {
                        _clickSound.Play();
                        HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Charakter = HexMap._board[Player.activeTile.LogicalPosition.X, Player.activeTile.LogicalPosition.Y].Charakter;
                        if (Player.activeTile != Player.moveTile)
                        {
                            HexMap._board[Player.activeTile.LogicalPosition.X, Player.activeTile.LogicalPosition.Y].Charakter = null;
                        }
                        HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Charakter.LogicalPosition = HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].LogicalPosition;
                        HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Charakter.LogicalBoardPosition = HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].LogicalPosition;
                        HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Charakter.CanMove = false;

                        Player.activeTile = Player.moveTile;

                        Fighthandler.npcTiles = HexMap.canBefriendNeighbourTiles;
                        Fighthandler.playerTiles = HexMap.friendNeighbourTiles;
                        Fighthandler.playerTiles.Add(Player.activeTile);
                        GState = Game1.GameState.InTalkFight;
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
                btnInteract.MoveButton(btnAttack.GetPosBelow());

               if (!fightTrue && !interactTrue)
               {
               }
                else
                {
                    if (fightTrue)
                    {
                        btnAttack.Draw(spriteBatch, textFont);
                        if(btnAttack.IsHovered() && HexMap._board[Player.activeTile.LogicalPosition.X, Player.activeTile.LogicalPosition.Y].Charakter.GaveUp == true)
                        {
                            spriteBatch.DrawString(textFont, "You cannot attack, because you gave up last fight. \nWait 1 Turn",btnAttack.GetTextPosRightOf(), Color.Yellow);
                        }
                        if (interactTrue)
                        {
                            btnInteract.Draw(spriteBatch, textFont);
                            if (btnInteract.IsHovered() && HexMap._board[Player.activeTile.LogicalPosition.X, Player.activeTile.LogicalPosition.Y].Charakter.GaveUp == true)
                            {
                                spriteBatch.DrawString(textFont, "You cannot interact, because you gave up last fight. \nWait 1 Turn", btnAttack.GetTextPosRightOf(), Color.Yellow);
                            }
                        }
                    }
                    else
                    {
                        if (interactTrue)
                        {
                            btnInteract.MoveButton(btnConfirm.GetPosBelow());
                            btnInteract.Draw(spriteBatch, textFont);
                            if (btnAttack.IsHovered() && HexMap._board[Player.activeTile.LogicalPosition.X, Player.activeTile.LogicalPosition.Y].Charakter.GaveUp == true)
                            {
                                spriteBatch.DrawString(textFont, "You cannot interact, because you gave up last fight. \nWait 1 Turn", btnAttack.GetTextPosRightOf(), Color.Yellow);
                            }
                        }
                    }
                }
                spriteBatch.End();
            }
        }
    }
}
