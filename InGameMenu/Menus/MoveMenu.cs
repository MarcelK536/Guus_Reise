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

namespace Guus_Reise
{
    class MoveMenu : SimpleMenu
    {
        Button btnConfirm;
        Button btnAttack;
        Button btnInteract;
        Button btnSaveGame;
        Button btnQuitGame;
        public bool fightTrue;
        public bool interactTrue;

        static public Texture2D menuTexture { get; set; }

        public MoveMenu(SpriteFont moveMenuFont, GraphicsDevice graphicsDevice, BlendDirection blend) : base(new Vector2(), moveMenuFont,graphicsDevice,blend)
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
            btnInteract = new Button("Iteract", btnTexture, 1, btnAttack.GetPosBelow());
            menuButtons.Add(btnInteract);
            btnSaveGame = new Button("Save", btnTexture, 1, btnAttack.GetPosBelow());
            menuButtons.Add(btnSaveGame);
            btnQuitGame = new Button("Quit Game", btnTexture, 1, btnInteract.GetPosBelow());
            menuButtons.Add(btnQuitGame);

            SetMenuHeight();
            SetMenuWidth();
            SetBackgroundTexture(Color.GhostWhite);
        }

        public void Update(GameTime gametime)
        {
            base.Update();
            menuHeight = btnQuitGame.GetPosBelow().Y;
            SetBackgroundTexture(bkgColor);
            if (Active)
            {
                HexMap._board[Player.moveTile.LogicalPosition.X, Player.moveTile.LogicalPosition.Y].Tile.Glow = new Vector3(0.5f, 0.5f, 0.5f);
                Hex targetHex = Player.moveTile;
                Hex startHex = Player.activeTile;
                if (btnQuitGame.IsClicked())
                {
                    Game1.GState = Game1.GameState.MainMenu;
                }
                if (btnSaveGame.IsClicked())
                {
                    //TODO SAVE GAME
                }
                if (btnConfirm.IsClicked())
                {
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
                    
                }
                if (fightTrue)
                {
                    if (btnAttack.IsClicked())
                    {
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

                        //Hier Unterscheidung in Art des Kampfes
                        if(true)
                        {
                            //Normaler Kampf
                            Fighthandler.isNormalFight = true;
                        }
                        else
                        {
                            //Wortgewandtheitskampf
                            Fighthandler.isNormalFight = false;
                        }
                        
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
                btnInteract.MoveButton(btnAttack.GetPosBelow());

                if (!fightTrue && !interactTrue)
                {
                    btnSaveGame.MoveButton(btnConfirm.GetPosBelow());
                    btnQuitGame.MoveButton(btnSaveGame.GetPosBelow());
                }
                else
                {
                    if (fightTrue)
                    {
                        btnAttack.Draw(spriteBatch, textFont);
                        btnSaveGame.MoveButton(btnAttack.GetPosBelow());
                        btnQuitGame.MoveButton(btnSaveGame.GetPosBelow());
                        if (interactTrue)
                        {
                            btnInteract.Draw(spriteBatch, textFont);
                            btnSaveGame.MoveButton(btnInteract.GetPosBelow());
                            btnQuitGame.MoveButton(btnSaveGame.GetPosBelow());
                        }
                    }
                    else
                    {
                        if (interactTrue)
                        {
                            btnInteract.MoveButton(btnConfirm.GetPosBelow());
                            btnInteract.Draw(spriteBatch, textFont);
                            btnSaveGame.MoveButton(btnInteract.GetPosBelow());
                            btnQuitGame.MoveButton(btnSaveGame.GetPosBelow());
                        }
                    }
                }
                btnSaveGame.Draw(spriteBatch, textFont);
                btnQuitGame.Draw(spriteBatch, textFont);
                spriteBatch.End();
            }
        }
    }
}
