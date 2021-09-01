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

        Button btnExit;
        Button btnSave;

        Button btnRightLong;
        Button btnRight;
        Button btnLeftLong;
        Button btnLeft;
        

        CharakterBox editingCharakterWeaponbox;
        int oldWeapon;

        List<Texture2D> weaponTableaus;
        List<Texture2D> weaponTableausTalk;

        GraphicsDevice graphics;

        public int[] boxCount;


        WeaponMenu weaponMenu;
        AttackMenu attackMenu;

        public int _hoehePanel;

        Texture2D texPanel1;
        Texture2D texPanelDouble;
        Texture2D texPanelBlue;

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

        public bool _isInModeWeaponEdit;

        private KeyboardState _prevKeyState;


        public FightMenu(SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(new Vector2(0,graphicsDevice.Viewport.Bounds.Center.Y), menuFont, graphicsDevice, direction)
        {
            InitFightMenu(Fighthandler.contentFight);

            SetParameterFromWindowScale();

            //Allgemeines
            graphics = graphicsDevice;
            needCloseBtn = false;

            boxCount = new int[2];

            _isInModeWeaponEdit = false;
           

            btnWidth = menuFont.MeasureString("Change Weapon").X + 10;
            Texture2D btnTexture = new Texture2D(graphicsDevice,(int) btnWidth, 50);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.Red * 0.8f;
            }
            btnTexture.SetData(btnColor);

            btnAttack = new Button("Attack", btnTexture, 1, pos);

            //Speicher und Abbruch-Buttons für die Waffen-Bearbeitung
            btnExit = new Button("", InformationComponents.texExit, 0.2f, pos);
            btnExit.isOnlyClickButton = true;
            btnSave = new Button("", InformationComponents.texSave, 0.2f, pos);
            btnSave.isOnlyClickButton = true;

            //Pfeile nach links und rechts
            btnLeftLong = new Button("", InformationComponents.texArrowLeftLong, 0.2f, pos);
            btnRightLong = new Button("", InformationComponents.texArrowRightLong, 0.2f, pos);
            btnRight = new Button("", InformationComponents.texArrowRight, 0.2f, pos);
            btnLeft = new Button("", InformationComponents.texArrowLeft, 0.2f, pos);




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
            texPanelBlue = content.Load<Texture2D>("Fight\\FightMenuPanelBlue");
            playerCharakterInfobox = content.Load<Texture2D>("Buttons\\PlayercharakterSheet");
            enemyCharakterInfobox = content.Load<Texture2D>("Buttons\\EnemycharakterSheet");
            currentMenuStatus = 0;
            textureEditbutton = content.Load<Texture2D>("Buttons\\pencil");
            textureEditbuttonHover = content.Load<Texture2D>("Buttons\\pencilHover");



            Texture2D platzhalterFight = content.Load<Texture2D>("Fight\\Weapon\\platzhalterFight");
            Texture2D platzhalterTalk = content.Load<Texture2D>("Fight\\Weapon\\platzhalterTalk");

            //Hier werden die auswählbaren Waffen als Tableaus geladen
            weaponTableaus = new List<Texture2D> { content.Load<Texture2D>("Fight\\Weapon\\WeaponTabelauFaust"), content.Load<Texture2D>("Fight\\Weapon\\WeaponTabelauMesser"), content.Load<Texture2D>("Fight\\Weapon\\WeaponTabelauAliestole") };
            weaponTableausTalk = new List<Texture2D> { platzhalterTalk };

            for(int i = 0; i <4; i++)
            {
                weaponTableaus.Add(platzhalterFight);
            }

            for (int i = 0; i < 4; i++)
            {
                weaponTableausTalk.Add(platzhalterTalk);
            }


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
                    infoBoxesNPCs[index] = new CharakterBox(playerHex.Charakter, "OneLine", 0.2f, 0, 0, false);
                    weaponboxesNPCs[index] = new CharakterBox(playerHex.Charakter, "Waffenbox", 0.2f, 0, 0, false);
                    isNPC = true;

                    if (Game1.GState == Game1.GameState.InFight)
                    {
                        foreach (Weapon weapon in Fighthandler.weaponNormal)
                        {
                            if (playerHex.Charakter.Weapon.Name == weapon.Name)
                            {
                                weaponboxesNPCs[index].currentWeapon = Weapon.weapons.IndexOf(weapon);
                            }
                        }
                    }
                    else if (Game1.GState == Game1.GameState.InTalkFight)
                    {
                        foreach (Weapon weapon in Fighthandler.weaponTalk)
                        {
                            if (playerHex.Charakter.Weapon.Name == weapon.Name)
                            {
                                weaponboxesNPCs[index].currentWeapon = Weapon.weapons.IndexOf(weapon);
                            }
                        }
                    }                   
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

                    if (Game1.GState == Game1.GameState.InFight)
                    {
                        foreach (Weapon weapon in Fighthandler.weaponNormal)
                        {
                            if (playerHex.Charakter.Weapon.Name == weapon.Name)
                            {
                                weaponboxesPlayer[index].currentWeapon = Weapon.weapons.IndexOf(weapon);
                            }
                        }
                    }
                    else if (Game1.GState == Game1.GameState.InTalkFight)
                    {
                        foreach (Weapon weapon in Fighthandler.weaponTalk)
                        {
                            if (playerHex.Charakter.Weapon.Name == weapon.Name)
                            {
                                weaponboxesPlayer[index].currentWeapon = Weapon.weapons.IndexOf(weapon);
                            }
                        }
                    }
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

            if (Fighthandler._isInModeWeaponEdit)
            {
                //Hintergrund Textur des Panels beim Bearbieten der Waffe
                _currentTex = texPanelBlue;

                //Positionen und Skaliserungen der Button aktualisieren
                if (Game1._graphics.IsFullScreen == true)
                {
                    btnSave.Scale = 0.4f;
                    btnExit.Scale = 0.4f;
                    btnSave.ButtonX = _graphicsDevice.Viewport.Width - 150;
                    btnSave.ButtonY = Fighthandler.hoeheArena + 50;
                    btnExit.ButtonX = _graphicsDevice.Viewport.Width - 300;
                    btnExit.ButtonY = Fighthandler.hoeheArena + 50;

                    btnRightLong.Scale = 0.3f;
                    btnLeftLong.Scale = 0.3f;
                    btnRightLong.ButtonX = 1290;
                    btnRightLong.ButtonY = Fighthandler.hoeheArena + 110;
                    btnLeftLong.ButtonX = 50;
                    btnLeftLong.ButtonY = Fighthandler.hoeheArena + 110;


                }
                else
                {
                    btnRightLong.Scale = 0.15f;
                    btnLeftLong.Scale = 0.15f;
                    btnRightLong.ButtonX = 570;
                    btnRightLong.ButtonY = Fighthandler.hoeheArena + 40;
                    btnLeftLong.ButtonX = 50;
                    btnLeftLong.ButtonY = Fighthandler.hoeheArena + 40;

                    btnSave.Scale = 0.3f;
                    btnExit.Scale = 0.3f;
                    btnSave.ButtonX = _graphicsDevice.Viewport.Width - 100;
                    btnSave.ButtonY = Fighthandler.hoeheArena + 30;
                    btnExit.ButtonX = _graphicsDevice.Viewport.Width - 200;
                    btnExit.ButtonY = Fighthandler.hoeheArena + 30;

                }

                List<Weapon> currentWeaponList = Fighthandler.weaponTalk;
                if (Game1.GState == Game1.GameState.InFight)
                {
                    currentWeaponList = Fighthandler.weaponNormal;
                }
                int endOfList = currentWeaponList.Count;

                //Aktualisieren der Wafffen-Tableaus
                if ( (Keyboard.GetState().IsKeyDown(Keys.Right) && _prevKeyState.IsKeyUp(Keys.Right)) || btnRightLong.IsClicked())
                {
                    ++editingCharakterWeaponbox.currentWeapon;
                    
                    if (editingCharakterWeaponbox.currentWeapon == endOfList)
                    {
                        editingCharakterWeaponbox.currentWeapon = 0;
                    }
                }
                if ( (Keyboard.GetState().IsKeyDown(Keys.Left) && _prevKeyState.IsKeyUp(Keys.Left)) || btnLeftLong.IsClicked())
                {
                    --editingCharakterWeaponbox.currentWeapon;
                    if (editingCharakterWeaponbox.currentWeapon == -1)
                    {
                        editingCharakterWeaponbox.currentWeapon = endOfList -1;
                    }
                }

                //Prüfen ob Spichern oder X geklickt wurde
                if (btnExit.IsClicked())
                {
                    Fighthandler._isInModeWeaponEdit = false;
                    _currentTex = texPanelDouble;
                    editingCharakterWeaponbox.currentWeapon = oldWeapon;
                }
                else if (btnSave.IsClicked())
                {
                    Fighthandler._isInModeWeaponEdit = false;
                    _currentTex = texPanelDouble;
                    editingCharakterWeaponbox._charakter.Weapon = currentWeaponList[editingCharakterWeaponbox.currentWeapon];
                }

                CheckMenuStatus();

                _prevKeyState = Keyboard.GetState();

                return;

            }

            //Positionen und Skaliserungen der Button aktualisieren
            if (Game1._graphics.IsFullScreen == true)
            {
                btnRight.Scale = 0.2f;
                btnLeft.Scale = 0.2f;
                btnLeft.ButtonX = 40;
                btnLeft.ButtonY = _graphicsDevice.Viewport.Height - 150;
                btnRight.ButtonX = _graphicsDevice.Viewport.Width - 150;
                btnRight.ButtonY = _graphicsDevice.Viewport.Height - 150;

            }
            else
            {
                btnRight.Scale = 0.1f;
                btnLeft.Scale = 0.1f;
                btnLeft.ButtonX = 20;
                btnLeft.ButtonY = _graphicsDevice.Viewport.Height - 80;
                btnRight.ButtonX = _graphicsDevice.Viewport.Width - 75;
                btnRight.ButtonY = _graphicsDevice.Viewport.Height - 80;
            }

            // Test if an swipe in left or right direktion was initialized
            if ( (Keyboard.GetState().IsKeyDown(Keys.Right) && _prevKeyState.IsKeyUp(Keys.Right)) || btnRight.IsClicked())
            {
                currentMenuStatus = (currentMenuStatus + 1) % menuStatusList.Count;
                SetParameterFromWindowScale();
            }
            if ( (Keyboard.GetState().IsKeyDown(Keys.Left) && _prevKeyState.IsKeyUp(Keys.Left)) || btnLeft.IsClicked())
            {
                if (currentMenuStatus == 0)
                {
                    currentMenuStatus = menuStatusList.Count - 1;
                }
                else
                {
                    currentMenuStatus = (currentMenuStatus - 1) % menuStatusList.Count;
                }
                SetParameterFromWindowScale();
            }

            CheckMenuStatus();



            _prevKeyState = Keyboard.GetState();

            switch (menuStatusList[currentMenuStatus])
            {
                case "Attack":
                    _currentTex = texPanel1;
                    break;

                case "CharakterUebersicht":
                    _currentTex = texPanelDouble;
                    break;

                case "WaffenUebersicht":

                    //Hintergrund Textur für die Waffen-Übersicht
                    _currentTex = texPanelDouble;


                    //Prüfen ob eine der Waffen bearbietet werden soll
                    foreach (CharakterBox playerWeapon in weaponboxesPlayer)
                    {
                        if (playerWeapon.editButton.IsClicked())
                        {
                            oldWeapon = playerWeapon.currentWeapon;
                            editingCharakterWeaponbox = playerWeapon;
                            Fighthandler._isInModeWeaponEdit = true;
                            
                            break;
                        }
                    }
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
                Fighthandler.fightResults.gaveUp = true;
                Fighthandler.ExitFight();
                Active = false;
                Fighthandler.showFightResults = true;
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
                                posX = Fighthandler._graphicsDevice.Viewport.Width - (int)infoBoxesNPCs[i].boxSize.X - 240;
                                posY = (Fighthandler.hoeheArena) + (Fighthandler._graphicsDevice.Viewport.Height - Fighthandler.hoeheArena) / 2 - (int)infoBoxesNPCs[i].boxSize.Y / 2;
                            }
                            else
                            {
                                posX = Fighthandler._graphicsDevice.Viewport.Width - (int)infoBoxesNPCs[i].boxSize.X - 140;
                                posY = (Fighthandler.hoeheArena) + (Fighthandler._graphicsDevice.Viewport.Height - Fighthandler.hoeheArena) / 2 - (int)infoBoxesNPCs[i].boxSize.Y / 2;
                            }
                            break;
                        case 2:
                            boxCount[1] = 2;
                            if (Game1._graphics.IsFullScreen == true)
                            {
                                if (index == 0)
                                {
                                    posY = (Fighthandler.hoeheArena) + 90;
                                }
                                else
                                {
                                    posY = (Fighthandler.hoeheArena) + (int)infoBoxesNPCs[i].boxSize.Y + 110;
                                }
                                posX = Fighthandler._graphicsDevice.Viewport.Width - (int)infoBoxesNPCs[i].boxSize.X - 150;
                            }
                            else
                            {
                                if (index == 0)
                                {
                                    posY = (Fighthandler.hoeheArena) + 20;
                                }
                                else
                                {
                                    posY = (Fighthandler.hoeheArena) + (int)infoBoxesNPCs[i].boxSize.Y + 30;
                                }
                                posX = Fighthandler._graphicsDevice.Viewport.Width - (int)infoBoxesNPCs[i].boxSize.X - 100;
                            }
                            break;
                        case 3:
                            
                        case 4:
                            boxCount[1] = 4;
                            if (Game1._graphics.IsFullScreen == true)
                            {
                                if (index < 2)
                                {
                                    posY = (Fighthandler.hoeheArena) + 70;
                                }
                                else
                                {
                                    posY = (Fighthandler.hoeheArena) + (int)infoBoxesNPCs[i].boxSize.Y + 80;
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
                                    posY = (Fighthandler.hoeheArena) + 90;
                                }
                                else
                                {
                                    posY = (Fighthandler.hoeheArena) + (int)infoBoxesPlayer[i].boxSize.Y + 110;
                                }
                                posX = 150;
                            }
                            else
                            {
                                if (index == 0)
                                {
                                    posY = (Fighthandler.hoeheArena) + 20;
                                }
                                else
                                {
                                    posY = (Fighthandler.hoeheArena) + (int)infoBoxesPlayer[i].boxSize.Y + 30;
                                }
                                posX = 100;
                            }
                            break;
                        case 3:

                        case 4:
                            boxCount[0] = 4;
                            if (Game1._graphics.IsFullScreen == true)
                            {
                                if (index < 2)
                                {
                                    posY = (Fighthandler.hoeheArena) + 70;
                                }
                                else
                                {
                                    posY = (Fighthandler.hoeheArena) + (int)infoBoxesPlayer[i].boxSize.Y + 80;
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

        public void DrawEditWindow(string type, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            switch(type)
            {
                //Zeichnen des Editorfensters um die Waffen zu ändern
                case "WeaponEdit":
                    btnExit.Draw(spriteBatch, textFont);
                    btnSave.Draw(spriteBatch, textFont);
                    btnLeftLong.Draw(spriteBatch, textFont);
                    btnRightLong.Draw(spriteBatch, textFont);

                    List<Texture2D> currentTableauList = weaponTableaus;
                    if(Game1.GState == Game1.GameState.InTalkFight)
                    {
                        currentTableauList = weaponTableausTalk;
                    }

                    if (Game1._graphics.IsFullScreen == true)
                    {
                        spriteBatch.Draw(currentTableauList[editingCharakterWeaponbox.currentWeapon], new Rectangle(250, 540, 1000, 500), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(currentTableauList[editingCharakterWeaponbox.currentWeapon], new Rectangle(150, 370, 400, 200), Color.White);
                    }                       
                    break;
            }
            spriteBatch.End();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            switch (menuStatusList[currentMenuStatus])
            {
                case "WaffenUebersicht":

                    if (Fighthandler._isInModeWeaponEdit == true)
                    {
                        DrawEditWindow("WeaponEdit", spriteBatch);
                    }
                    else
                    {
                        DrawPanel(spriteBatch);
                        if (weaponMenu != null && weaponMenu.Active)
                        {
                            weaponMenu.Draw(spriteBatch);
                        }
                        if (attackMenu != null && attackMenu.Active)
                        {
                            attackMenu.Draw(spriteBatch);
                        }
                    }
                    break;

                default:
                    DrawPanel(spriteBatch);
                    if (weaponMenu != null && weaponMenu.Active)
                    {
                        weaponMenu.Draw(spriteBatch);
                    }
                    if (attackMenu != null && attackMenu.Active)
                    {
                        attackMenu.Draw(spriteBatch);
                    }
                    break;

            }
            SetBackgroundTexturePicture(_currentTex);    
               
        }



        public void DrawPanel(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            btnRight.Draw(spriteBatch, textFont);
            btnLeft.Draw(spriteBatch, textFont);
            spriteBatch.End();
            
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

                    if(!_isInModeWeaponEdit)
                    {
                        for (int i = 0; i < weaponboxesPlayer.Length; i++)
                        {
                            weaponboxesPlayer[i].Draw(spriteBatch);
                        }

                        for (int i = 0; i < weaponboxesNPCs.Length; i++)
                        {
                            weaponboxesNPCs[i].Draw(spriteBatch);
                        }
                    }
                    break;

                default:
                    break;
            }
            
        }
    }
}
