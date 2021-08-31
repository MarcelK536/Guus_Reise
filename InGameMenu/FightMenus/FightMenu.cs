using Guus_Reise.InGameMenu.MenuComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Button btnCancelAttack;
        GraphicsDevice graphics;

        public int[] boxCount;


        WeaponMenu weaponMenu;
        AttackMenu attackMenu;

        public int _hoehePanel;

        public Texture2D texPanel1;
        public Texture2D texPanelDouble;

        public Texture2D _currentTex;

        public Texture2D textureEditbutton;
        public Texture2D textureEditbuttonHover;

        public Texture2D playerCharakterInfobox;
        public Texture2D enemyCharakterInfobox;

        public CharakterBox[] infoBoxesPlayer;
        public CharakterBox[] infoBoxesNPCs;

        public Infobox[] weaponboxesPlayer;
        public Infobox[] weaponboxesNPCs;

        public Vector2 _positionPanel;
        public Vector2 _sizePanel;

        public int currentMenuStatus;
        public List<string> menuStatusList = new List<string> { "Attack", "CharakterUebersicht", "WaffenUebersicht" };

        public bool _isInModeWeaponEdit = false;

        private KeyboardState _prevKeyState;


        public FightMenu(SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(new Vector2(0,graphicsDevice.Viewport.Bounds.Center.Y), menuFont, graphicsDevice, direction)
        {
            InitFightMenu(Fighthandler.contentFight);

            SetParameterFromWindowScale();

            //Allgemeines
            graphics = graphicsDevice;
            needCloseBtn = false;

            boxCount = new int[2];

           

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
            btnCancelAttack = new Button("Cancel Attack", btnTexture, 1, btnChangeWeapon.GetPos());
            menuButtons.Add(btnCancelAttack);

            //Eigenschaften vom Menu-Panel
            _positionPanel = new Vector2(0, Fighthandler.hoeheArena);
            _currentTex = texPanel1;
            _sizePanel = new Vector2(_hoehePanel, Fighthandler._graphicsDevice.Viewport.Width);
            menuWidth = graphicsDevice.Viewport.Width;
            menuHeight = _hoehePanel;
            SetBackgroundTexturePicture(_currentTex);
        }

        public void InitFightMenu(ContentManager content)
        {
            texPanel1 = content.Load<Texture2D>("Fight\\FightMenuPanel2");
            texPanelDouble = content.Load<Texture2D>("Fight\\FightMenuPanel");
            playerCharakterInfobox = content.Load<Texture2D>("Buttons\\PlayercharakterSheet");
            enemyCharakterInfobox = content.Load<Texture2D>("Buttons\\EnemycharakterSheet");
            currentMenuStatus = 0;
            textureEditbutton = content.Load<Texture2D>("Buttons\\pencil");
            textureEditbuttonHover = content.Load<Texture2D>("Buttons\\pencilHover");

        }

        public void InitMenuBoxes(List<Hex> fightTiles)
        {
            bool isNPC = false;

            //Charakterboxen
            foreach (Hex playerHex in fightTiles)
            {
                int index = fightTiles.IndexOf(playerHex);
                //NPCs
                if (playerHex.Charakter.IsNPC)
                {
                    if (infoBoxesNPCs == null)
                    {
                        infoBoxesNPCs = new CharakterBox[fightTiles.Count];
                    }
                    if(weaponboxesNPCs == null)
                    {
                        weaponboxesNPCs = new CharakterBox[fightTiles.Count];
                    }
                    infoBoxesNPCs[index] = new CharakterBox(playerHex.Charakter, "OneLine", 0.2f, 0, 0, false); ;
                    weaponboxesNPCs[index] = new CharakterBox(playerHex.Charakter, "Waffenbox", 0.2f, 0, 0, true);
                    isNPC = true;
                }
                //Player
                else
                {
                    if (infoBoxesPlayer == null)
                    {
                        infoBoxesPlayer = new CharakterBox[fightTiles.Count];
                    }
                    if (weaponboxesPlayer == null)
                    {
                        weaponboxesPlayer = new Infobox[fightTiles.Count];
                    }
                    infoBoxesPlayer[index] = new CharakterBox(playerHex.Charakter, "OneLine", 0.2f, 0, 0, false);
                    weaponboxesPlayer[index] = new CharakterBox(playerHex.Charakter, "Waffenbox", 0.2f, 0, 0, true);
                }
            }
            if (isNPC)
            {
                SetPositionsCharakterboxes("NPC");
            }
            else
            {
                SetPositionsCharakterboxes("Player");
            }

        }

        public void CheckMenuStatus()
        {
            switch(menuStatusList[currentMenuStatus])
            {
                case "CharakterUebersicht":
                    foreach (CharakterBox info in infoBoxesNPCs)
                    {
                        info.UpdateCharakterboxParameters();
                    }

                    foreach (CharakterBox info in infoBoxesPlayer)
                    {
                        info.UpdateCharakterboxParameters();
                    }
                    break;

                case "WaffenUebersicht":
                    foreach (CharakterBox info in weaponboxesNPCs)
                    {
                        info.UpdateCharakterboxParameters();
                    }

                    foreach (CharakterBox info in weaponboxesPlayer)
                    {
                        info.UpdateCharakterboxParameters();
                    }
                    break;

                default:
                    break;
            }
            
        }

        public void UpdatePanel(GameTime gameTime)
        {
            SetParameterFromWindowScale();

            // Test if an swipe in left or right direktion was initialized
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && _prevKeyState.IsKeyUp(Keys.Right))
            {
                currentMenuStatus = (currentMenuStatus + 1) % menuStatusList.Count;
                SetParameterFromWindowScale();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && _prevKeyState.IsKeyUp(Keys.Left))
            {
                if (currentMenuStatus == 0)
                {
                    currentMenuStatus = menuStatusList.Count-1;
                }
                else
                {
                    currentMenuStatus = (currentMenuStatus - 1) % menuStatusList.Count;
                }
                SetParameterFromWindowScale();
            }

            if (_isInModeWeaponEdit)
            {

            }
            else
            {
                CheckMenuStatus();
            }

            _prevKeyState = Keyboard.GetState();

            switch(menuStatusList[currentMenuStatus])
            {
                case "Attack":
                    _currentTex = texPanel1;
                    break;
                case "CharakterUebersicht":
                    _currentTex = texPanelDouble;
                    break;
                case "WaffenUebersicht":
                    _currentTex = texPanelDouble;
                    break;

                default:
                    _currentTex = texPanel1;
                    break;

            }
        }

        public void Update(GameTime time)
        {
            base.Update();

            _positionPanel.Y = Fighthandler.hoeheArena;

            if (Fighthandler.initPlayers)
            {
                UpdatePanel(time);
            }
            
            int x = Player.activeTile.LogicalPosition.X;
            int y = Player.activeTile.LogicalPosition.Y;

            if (FightPlayer.isSelecting == false)
            {
                if (btnAttack.IsClicked())
                {
                    attackMenu = new AttackMenu(btnAttack.GetPosRightOf(), textFont, graphics, BlendDirection.None);
                    attackMenu.Active = true;
                    if (weaponMenu != null)
                    {
                        weaponMenu.Active = false;
                    }
                }
                if (btnChangeWeapon.IsClicked())
                {
                    weaponMenu = new WeaponMenu(Weapon.weapons, btnChangeWeapon.GetPosRightOf(), textFont, graphics, SimpleMenu.BlendDirection.None);
                    weaponMenu.Active = true;
                    if (attackMenu != null)
                    {
                        attackMenu.Active = false;
                    }
                }
            }
            else
            {
                if (btnCancelAttack.IsClicked())
                {
                    FightPlayer.CancelAttack();
                }
            }
            if (btnGiveUp.IsClicked())
            {
                Game1.GState = Game1.GameState.InGame;
                Active = false;
                Fighthandler.ExitFight();
            }

            UpdatePosition(new Vector2(0, _graphicsDevice.Viewport.Bounds.Center.Y));

            bkgPos.Y = Fighthandler.hoeheArena;
            menuWidth = _graphicsDevice.Viewport.Width;
            btnAttack.MoveButton(btnClose.GetPosBelow());
            btnChangeWeapon.MoveButton(btnAttack.GetPosBelow());
            btnGiveUp.MoveButton(btnChangeWeapon.GetPosBelow());

            if (weaponMenu != null && weaponMenu.Active)
            {
                weaponMenu.Update(time);
            }
            if ((attackMenu != null && attackMenu.Active)|| FightPlayer.isSelecting == true)
            {
                attackMenu.Update(time);
            }
        }

        public void SetParameterFromWindowScale()
        {
            if (Game1._graphics.IsFullScreen == true)
            {
                Fighthandler.hoeheArena = ((Fighthandler._graphicsDevice.Viewport.Height / 2) + Fighthandler._graphicsDevice.Viewport.Height / 2 / 2) - 300;
            }
            else
            {
                Fighthandler.hoeheArena = ((Fighthandler._graphicsDevice.Viewport.Height / 2) + Fighthandler._graphicsDevice.Viewport.Height / 2 / 2) - 100;
            }
            _hoehePanel = Fighthandler._graphicsDevice.Viewport.Height - Fighthandler.hoeheArena;
            menuHeight = _hoehePanel;

            _positionPanel.Y = Fighthandler.hoeheArena;

            if (Fighthandler.initPlayers)
            {
                SetPositionsCharakterboxes("NPC");
                SetPositionsCharakterboxes("Player");
            }

        }


        public void SetPositionsCharakterboxes(string type)
        {
            int posY = 0;
            int posX = 0;
            int index = 0;
            if (type == "NPC")
            {
                int countBoxes = infoBoxesNPCs.Length;
                for (int i = 0; i < countBoxes; i++)
                {
                    index = i;
                    switch (countBoxes)
                    {
                        case 1:
                            boxCount[1] = 1;
                            if (Game1._graphics.IsFullScreen == true)
                            {
                                posX = Fighthandler._graphicsDevice.Viewport.Width - (int)infoBoxesNPCs[i].boxSize.X - 100;
                                posY = (Fighthandler.hoeheArena) + (Fighthandler._graphicsDevice.Viewport.Height - Fighthandler.hoeheArena) / 2 - (int)infoBoxesNPCs[i].boxSize.Y / 2;
                            }
                            else
                            {
                                posX = Fighthandler._graphicsDevice.Viewport.Width - (int)infoBoxesNPCs[i].boxSize.X - 50;
                                posY = (Fighthandler.hoeheArena) + (Fighthandler._graphicsDevice.Viewport.Height - Fighthandler.hoeheArena) / 2 - (int)infoBoxesNPCs[i].boxSize.Y / 2;
                            }
                            break;
                        case 2:
                            boxCount[1] = 2;
                            if (Game1._graphics.IsFullScreen == true)
                            {
                                if (index == 0)
                                {
                                    posX = Fighthandler._graphicsDevice.Viewport.Width - 2 * ((int)infoBoxesNPCs[i].boxSize.X) - 2 * 5 - 20;
                                }
                                else
                                {
                                    posX = Fighthandler._graphicsDevice.Viewport.Width - ((int)infoBoxesNPCs[i].boxSize.X) - 20;
                                }
                                posY = (Fighthandler.hoeheArena) + (Fighthandler._graphicsDevice.Viewport.Height - Fighthandler.hoeheArena) / 2 - (int)infoBoxesNPCs[i].boxSize.Y / 2;
                            }
                            else
                            {
                                if (index == 0)
                                {
                                    posX = Fighthandler._graphicsDevice.Viewport.Width - 2 * ((int)infoBoxesNPCs[i].boxSize.X) - 2 * 5 - 20;
                                }
                                else
                                {
                                    posX = Fighthandler._graphicsDevice.Viewport.Width - ((int)infoBoxesNPCs[i].boxSize.X) - 20;
                                }
                                posY = (Fighthandler.hoeheArena) + (Fighthandler._graphicsDevice.Viewport.Height - Fighthandler.hoeheArena) / 2 - (int)infoBoxesNPCs[i].boxSize.Y / 2;
                            }
                            break;
                        case 3:
                            
                        case 4:
                            boxCount[1] = 4;
                            if (Game1._graphics.IsFullScreen == true)
                            {
                                if (index < 2)
                                {
                                    posY = (Fighthandler.hoeheArena) + 20;
                                }
                                else
                                {
                                    posY = (Fighthandler.hoeheArena) + (int)infoBoxesNPCs[i].boxSize.Y + 30;
                                }

                                if (index == 0 || index == 2)
                                {
                                    posX = Fighthandler._graphicsDevice.Viewport.Width - 2 * ((int)infoBoxesNPCs[i].boxSize.X) - 2 * 5 - 20;
                                }
                                else
                                {
                                    posX = Fighthandler._graphicsDevice.Viewport.Width - ((int)infoBoxesNPCs[i].boxSize.X) - 20;
                                }
                            }
                            else
                            {
                                if (index < 2)
                                {
                                    posY = (Fighthandler.hoeheArena) + 20;
                                }
                                else
                                {
                                    posY = (Fighthandler.hoeheArena) + (int)infoBoxesNPCs[i].boxSize.Y + 30;
                                }

                                if (index == 0 || index == 2)
                                {
                                    posX = Fighthandler._graphicsDevice.Viewport.Width - 2 * ((int)infoBoxesNPCs[i].boxSize.X) - 2 * 5 - 20;
                                }
                                else
                                {
                                    posX = Fighthandler._graphicsDevice.Viewport.Width - ((int)infoBoxesNPCs[i].boxSize.X) - 20;
                                }
                            }
                            break;


                    }

                    infoBoxesNPCs[i]._infoboxY = posY;
                    infoBoxesNPCs[i]._infoboxX = posX;

                    weaponboxesNPCs[i]._infoboxY = posY;
                    weaponboxesNPCs[i]._infoboxX = posX;
                   
                    infoBoxesNPCs[i]._hasToUpdate = true;
                    weaponboxesNPCs[i]._hasToUpdate = true;


                }

            }
            else if (type == "Player")
            {
                int countBoxes = infoBoxesPlayer.Length;
                for (int i = 0; i < countBoxes; i++)
                {
                    index = i;
                    switch (countBoxes)
                    {
                        case 1:
                            boxCount[0] = 1;
                            if (Game1._graphics.IsFullScreen == true)
                            {
                                posX = 150;
                                posY = (Fighthandler.hoeheArena) + (Fighthandler._graphicsDevice.Viewport.Height - Fighthandler.hoeheArena) / 2 - (int)infoBoxesPlayer[i].boxSize.Y / 2;
                            }
                            else
                            {
                                posX = 100;
                                posY = (Fighthandler.hoeheArena) + (Fighthandler._graphicsDevice.Viewport.Height - Fighthandler.hoeheArena) / 2 - (int)infoBoxesPlayer[i].boxSize.Y / 2;
                            }
                            break;
                        case 2:
                            boxCount[0] = 2;
                            if (Game1._graphics.IsFullScreen == true)
                            {
                                if (index == 0)
                                {
                                    posX = 20;
                                }
                                else
                                {
                                    posX = 20 + 2 * (int)infoBoxesPlayer[i].boxSize.Y - 2;
                                }
                                posY = (Fighthandler.hoeheArena) + (Fighthandler._graphicsDevice.Viewport.Height - Fighthandler.hoeheArena) / 2 - (int)infoBoxesPlayer[i].boxSize.Y / 2;
                            }
                            else
                            {
                                if (index == 0)
                                {
                                    posX = 20;
                                }
                                else
                                {
                                    posX = 20 + 2 * (int)infoBoxesPlayer[i].boxSize.Y - 2;
                                }
                                posY = (Fighthandler.hoeheArena) + (Fighthandler._graphicsDevice.Viewport.Height - Fighthandler.hoeheArena) / 2 - (int)infoBoxesPlayer[i].boxSize.Y / 2;
                            }
                            break;
                        case 3:

                        case 4:
                            boxCount[0] = 4;
                            if (Game1._graphics.IsFullScreen == true)
                            {
                                if (index < 2)
                                {
                                    posY = (Fighthandler.hoeheArena) + 20;
                                }
                                else
                                {
                                    posY = (Fighthandler.hoeheArena) + (int)infoBoxesPlayer[i].boxSize.Y + 30;
                                }

                                if (index == 0 || index == 2)
                                {
                                    posX = 20;
                                }
                                else
                                {
                                    posX = 20 + 2 * (int)infoBoxesPlayer[i].boxSize.Y - 2;
                                }
                            }
                            else
                            {
                                if (index < 2)
                                {
                                    posY = (Fighthandler.hoeheArena) + 20;
                                }
                                else
                                {
                                    posY = (Fighthandler.hoeheArena) + (int)infoBoxesPlayer[i].boxSize.Y + 30;
                                }

                                if (index == 0 || index == 2)
                                {
                                    posX = 20;
                                }
                                else
                                {
                                    posX = 20 + 2 * (int)infoBoxesPlayer[i].boxSize.Y - 2;
                                }
                            }
                            break;
                    }
                    infoBoxesPlayer[i]._infoboxY = posY;
                    infoBoxesPlayer[i]._infoboxX = posX;
                    infoBoxesPlayer[i]._hasToUpdate = true;

                    weaponboxesPlayer[i]._infoboxY = posY;
                    weaponboxesPlayer[i]._infoboxX = posX;
                    weaponboxesPlayer[i]._hasToUpdate = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (_isInModeWeaponEdit)
            {

            }
            else
            {
                DrawPanel(spriteBatch);
            }

            SetBackgroundTexturePicture(_currentTex);

            if (weaponMenu != null && weaponMenu.Active)
            {
                weaponMenu.Draw(spriteBatch);
            }
            if (attackMenu != null && attackMenu.Active)
            {
                attackMenu.Draw(spriteBatch);
            }
                
               
        }

        public void DrawPanel(SpriteBatch spriteBatch)
        {
            
            switch(menuStatusList[currentMenuStatus])
            {
                case "Attack":
                    spriteBatch.Begin();

                    if (FightPlayer.isSelecting == false)
                    {
                        btnAttack.Draw(spriteBatch, textFont);
                        btnChangeWeapon.Draw(spriteBatch, textFont);
                    }
                    else
                    {
                        spriteBatch.DrawString(textFont, "Select Enemy to Attack", pos, Color.Yellow);
                        btnCancelAttack.Draw(spriteBatch, textFont);
                    }

                    btnGiveUp.Draw(spriteBatch, textFont);

                    spriteBatch.End();
                    break;
                
                case "CharakterUebersicht":
                    for (int i = 0; i < infoBoxesPlayer.Length; i++)
                    {
                        infoBoxesPlayer[i].Draw(spriteBatch);
                    }

                    for (int i = 0; i < infoBoxesNPCs.Length; i++)
                    {
                        infoBoxesNPCs[i].Draw(spriteBatch);
                    }
                    break;

                case "WaffenUebersicht":
                    for(int i = 0; i < weaponboxesPlayer.Length; i++)
                    {
                        weaponboxesPlayer[i].Draw(spriteBatch);
                    }

                    for (int i = 0; i < weaponboxesNPCs.Length; i++)
                    {
                        weaponboxesNPCs[i].Draw(spriteBatch);
                    }
                    break;

                default:
                    break;
            }
            
        }
    }
}
