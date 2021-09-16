using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Guus_Reise
{
    class SkillUpMenu : SimpleMenu
    {
        Button btnPlusWiderstandskraft;
        Button btnPlusKoerperkraft;
        Button btnPlusBeweglichkeit;
        Button btnPlusAbwehr;
        Button btnPlusWortgewandtheit;
        Button btnPlusLautstaerke;
        Button btnPlusIgnoranz;
        Button btnPlusGeschwindigkeit;
        Button btnPlusGlueck;
        //Button btnPlusBewegung;

        Button btnMinusWiderstandskraft;
        Button btnMinusKoerperkraft;
        Button btnMinusBeweglichkeit;
        Button btnMinusAbwehr;
        Button btnMinusWortgewandtheit;
        Button btnMinusLautstaerke;
        Button btnMinusIgnoranz;
        Button btnMinusGeschwindigkeit;
        Button btnMinusGlueck;


        static SoundEffect _clickSound;

        int[] statChanges = new int[9];
        Texture2D hintBackground;
        Vector2 hintPos;
        Vector2 hintTextPos;

        public SkillUpMenu(SpriteFont moveMenuFont, GraphicsDevice graphicsDevice, BlendDirection blend, SoundEffect clickSound) : base(new Vector2(), moveMenuFont,  graphicsDevice, blend, _clickSound) 
        {
            Texture2D btnTexture = new Texture2D(graphicsDevice, 25, 25);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.Aquamarine;
            }
            btnTexture.SetData(btnColor);

            hintBackground = new Texture2D(graphicsDevice, 250, 250);
            Color[] hintBkgColor = new Color[hintBackground.Width * hintBackground.Height];
            for (int i = 0; i < hintBkgColor.Length; i++)
            {
               hintBkgColor[i] = Color.GhostWhite*0.2f;
            }
            hintBackground.SetData(hintBkgColor);


            btnPlusWiderstandskraft = new Button("+", btnTexture, 1, btnClose.GetPosBelow());
            btnPlusKoerperkraft = new Button("+", btnTexture, 1, btnPlusWiderstandskraft.GetPosBelow());
            btnPlusBeweglichkeit = new Button("+", btnTexture, 1, btnPlusKoerperkraft.GetPosBelow());
            btnPlusAbwehr = new Button("+", btnTexture, 1, btnPlusBeweglichkeit.GetPosBelow());
            btnPlusWortgewandtheit = new Button("+", btnTexture, 1, btnPlusAbwehr.GetPosBelow());
            btnPlusLautstaerke = new Button("+", btnTexture, 1, btnPlusWortgewandtheit.GetPosBelow());
            btnPlusIgnoranz = new Button("+", btnTexture, 1, btnPlusLautstaerke.GetPosBelow());
            btnPlusGeschwindigkeit = new Button("+", btnTexture, 1, btnPlusIgnoranz.GetPosBelow());
            btnPlusGlueck = new Button("+", btnTexture, 1, btnPlusGeschwindigkeit.GetPosBelow());
         //   btnPlusBewegung = new Button("+", btnTexture, 1, btnPlusGlueck.GetPosBelow());
            menuButtons.Add(btnPlusGlueck);

            btnMinusWiderstandskraft = new Button("-", btnTexture, 1, btnPlusWiderstandskraft.GetPosRightOf());
            btnMinusKoerperkraft = new Button("-", btnTexture, 1, btnMinusWiderstandskraft.GetPosBelow());
            btnMinusBeweglichkeit = new Button("-", btnTexture, 1, btnMinusKoerperkraft.GetPosBelow());
            btnMinusAbwehr = new Button("-", btnTexture, 1, btnMinusBeweglichkeit.GetPosBelow());
            btnMinusWortgewandtheit = new Button("-", btnTexture, 1, btnMinusAbwehr.GetPosBelow());
            btnMinusLautstaerke = new Button("-", btnTexture, 1, btnMinusWortgewandtheit.GetPosBelow());
            btnMinusIgnoranz = new Button("-", btnTexture, 1, btnMinusLautstaerke.GetPosBelow());
            btnMinusGeschwindigkeit = new Button("-", btnTexture, 1, btnMinusIgnoranz.GetPosBelow());
            btnMinusGlueck = new Button("-", btnTexture, 1, btnMinusGeschwindigkeit.GetPosBelow());
         //   btnMinusBewegung = new Button("+", btnTexture, 1, btnMinusGlueck.GetPosBelow());

            SetMenuHeight();
            menuWidth = btnClose.TextureDefault.Width + moveMenuFont.MeasureString("Name: ").X;
            SetBackgroundTexture(bkgColor);

            hintPos = new Vector2(menuWidth + 5, 0);
            hintTextPos = hintPos + new Vector2(5, 5);

            _clickSound = clickSound;
        }

        public override void Update()
        {
            if (Active)
            {
                int x = Player.activeTile.LogicalPosition.X;
                int y = Player.activeTile.LogicalPosition.Y;

                if (btnClose.IsClicked())
                {
                    _clickSound.Play();
                    Player.levelUpMenu.Active = false;
                    Player.charakterMenu.Active = true;

                    HexMap._board[x, y].Charakter.Widerstandskraft += statChanges[0];
                    HexMap._board[x, y].Charakter.Koerperkraft += statChanges[1];
                    HexMap._board[x, y].Charakter.Beweglichkeit += statChanges[2];
                    HexMap._board[x, y].Charakter.Abwehr += statChanges[3];
                    HexMap._board[x, y].Charakter.Wortgewandheit += statChanges[4];
                    HexMap._board[x, y].Charakter.Lautstaerke += statChanges[5];
                    HexMap._board[x, y].Charakter.Ignoranz += statChanges[6];
                    HexMap._board[x, y].Charakter.Geschwindigkeit += statChanges[7];
                    HexMap._board[x, y].Charakter.Glueck += statChanges[8];
                    //HexMap._board[x, y].Charakter.Bewegungsreichweite += statChanges[9];

                    Array.Clear(statChanges, 0, statChanges.Length);
                }

                #region PlusButtons 
                if (btnPlusWiderstandskraft.IsClicked() && HexMap._board[x,y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    statChanges[0]++; // HexMap._board[x, y].Charakter.Widerstandskraft++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusKoerperkraft.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    statChanges[1]++; //HexMap._board[x, y].Charakter.Koerperkraft++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusBeweglichkeit.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    statChanges[2]++; //HexMap._board[x, y].Charakter.Beweglichkeit++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusAbwehr.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    statChanges[3]++; //HexMap._board[x, y].Charakter.Abwehr++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusWortgewandtheit.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    statChanges[4]++; //HexMap._board[x, y].Charakter.Wortgewandheit++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusLautstaerke.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    statChanges[5]++; //HexMap._board[x, y].Charakter.Lautstaerke++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusIgnoranz.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    statChanges[6]++; //HexMap._board[x, y].Charakter.Ignoranz++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusGeschwindigkeit.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    statChanges[7]++; // HexMap._board[x, y].Charakter.Geschwindigkeit++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusGlueck.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    statChanges[8]++; // HexMap._board[x, y].Charakter.Glueck++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                //if (btnPlusBewegung.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                //{
                //    statChanges[9]++; //HexMap._board[x, y].Charakter.Bewegungsreichweite++;
                //    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                //}

                #endregion
                #region MinusButtons
                if (btnMinusWiderstandskraft.IsClicked())
                {
                    _clickSound.Play();
                    if (statChanges[0] > 0)
                    {
                        statChanges[0]--; // HexMap._board[x, y].Charakter.Widerstandskraft--;
                        HexMap._board[x, y].Charakter.Fähigkeitspunkte++;
                    }
                }
                if (btnMinusKoerperkraft.IsClicked() )
                {
                    _clickSound.Play();
                    if (statChanges[1] > 0)
                    {
                        statChanges[1]--; //HexMap._board[x, y].Charakter.Koerperkraft--;
                        HexMap._board[x, y].Charakter.Fähigkeitspunkte++;
                    }
                }
                if (btnMinusBeweglichkeit.IsClicked() )
                {
                    _clickSound.Play();
                    if (statChanges[2] > 0)
                    {
                        statChanges[2]--; //HexMap._board[x, y].Charakter.Beweglichkeit--;
                        HexMap._board[x, y].Charakter.Fähigkeitspunkte++;
                    }
                }
                if (btnMinusAbwehr.IsClicked() )
                {
                    _clickSound.Play();
                    if (statChanges[3] > 0)
                    {
                        statChanges[3]--; //HexMap._board[x, y].Charakter.Abwehr--;
                        HexMap._board[x, y].Charakter.Fähigkeitspunkte++;
                    }
                }
                if (btnMinusWortgewandtheit.IsClicked() )
                {
                    _clickSound.Play();
                    if (statChanges[4] > 0)
                    {
                        statChanges[4]--; //HexMap._board[x, y].Charakter.Wortgewandheit--;
                        HexMap._board[x, y].Charakter.Fähigkeitspunkte++;
                    }
                }
                if (btnMinusLautstaerke.IsClicked() )
                {
                    _clickSound.Play();
                    if (statChanges[5] > 0)
                    {
                        statChanges[5]--; //HexMap._board[x, y].Charakter.Lautstaerke--;
                        HexMap._board[x, y].Charakter.Fähigkeitspunkte++;
                    }
                }
                if (btnMinusIgnoranz.IsClicked() )
                {
                    _clickSound.Play();
                    if (statChanges[6] > 0)
                    {
                        statChanges[6]--; //HexMap._board[x, y].Charakter.Ignoranz--;
                        HexMap._board[x, y].Charakter.Fähigkeitspunkte++;
                    }
                }
                if (btnMinusGeschwindigkeit.IsClicked() )
                {
                    _clickSound.Play();
                    if (statChanges[7] > 0)
                    {
                        statChanges[7]--; // HexMap._board[x, y].Charakter.Geschwindigkeit--;
                        HexMap._board[x, y].Charakter.Fähigkeitspunkte++;
                    }
                }
                if (btnMinusGlueck.IsClicked() )
                {
                    _clickSound.Play();
                    if (statChanges[8] > 0)
                    {
                        statChanges[8]--; // HexMap._board[x, y].Charakter.Glueck--;
                        HexMap._board[x, y].Charakter.Fähigkeitspunkte++;
                    }
                }
                //if (btnMinusBewegung.IsClicked() )
                //{
                //    statChanges[9]--; //HexMap._board[x, y].Charakter.Bewegungsreichweite--;
                //    HexMap._board[x, y].Charakter.Fähigkeitspunkte++;
                //}
                #endregion
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Active)
            {
                int x = Player.activeTile.LogicalPosition.X;
                int y = Player.activeTile.LogicalPosition.Y;
                if (HexMap._board[x, y].Charakter == null)
                {
                    spriteBatch.Begin();
                    menuWidth = btnClose.GetPosRightOf().X + textFont.MeasureString("No character selected").X;
                    menuHeight = btnClose.GetPosBelow().Y;
                    SetBackgroundTexture(bkgColor);
                    spriteBatch.DrawString(textFont, "No character selected", btnClose.GetPosRightOf(), Color.Yellow);
                    spriteBatch.End();
                }
                else
                {
                    menuWidth = btnClose.GetPosRightOf().X + textFont.MeasureString("Name: " + HexMap._board[x, y].Charakter.Name + " Points: " + HexMap._board[x, y].Charakter.Fähigkeitspunkte).X;
                    SetMenuHeight();
                    SetBackgroundTexture(bkgColor);
                    hintPos = new Vector2(menuWidth, 0);
                    hintTextPos = hintPos + new Vector2(5, 5);

                    spriteBatch.Begin();
                    spriteBatch.DrawString(textFont, "Name: " + HexMap._board[x, y].Charakter.Name + " Points: " + HexMap._board[x, y].Charakter.Fähigkeitspunkte, btnClose.GetPosRightOf(), Color.Yellow);


                    btnPlusWiderstandskraft.Draw(spriteBatch, textFont);
                    btnMinusWiderstandskraft.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Life Points: " + HexMap._board[x, y].Charakter.Widerstandskraft + statChanges[0], btnMinusWiderstandskraft.GetPosRightOf(), Color.Yellow);
                    if(btnPlusWiderstandskraft.IsHovered() || btnMinusWiderstandskraft.IsHovered())
                    {
                        spriteBatch.Draw(hintBackground, hintPos, Color.White);
                        spriteBatch.DrawString(textFont, "This is your Life \n(Normal and \nInteract Fight)", hintTextPos, Color.Yellow);
                    }

                    btnPlusKoerperkraft.Draw(spriteBatch, textFont);
                    btnMinusKoerperkraft.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Strength: " + HexMap._board[x, y].Charakter.Koerperkraft + statChanges[1], btnMinusKoerperkraft.GetPosRightOf(), Color.Yellow);
                    if (btnPlusKoerperkraft.IsHovered() || btnMinusKoerperkraft.IsHovered())
                    {
                        spriteBatch.Draw(hintBackground, hintPos, Color.White);
                        spriteBatch.DrawString(textFont, "Strength is the \nphysical Attack \n(Normal Fight)", hintTextPos, Color.Yellow);
                    }

                    btnPlusBeweglichkeit.Draw(spriteBatch, textFont);
                    btnMinusBeweglichkeit.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Agility: " + HexMap._board[x, y].Charakter.Beweglichkeit + statChanges[2], btnMinusBeweglichkeit.GetPosRightOf(), Color.Yellow);
                    if (btnPlusBeweglichkeit.IsHovered() || btnMinusBeweglichkeit.IsHovered())
                    {
                        spriteBatch.Draw(hintBackground, hintPos, Color.White);
                        spriteBatch.DrawString(textFont, "Agility contributs \nto Strength \n(Normal Fight)", hintTextPos, Color.Yellow);
                    }

                    btnPlusAbwehr.Draw(spriteBatch, textFont);
                    btnMinusAbwehr.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Defense: " + HexMap._board[x, y].Charakter.Abwehr + statChanges[3], btnMinusAbwehr.GetPosRightOf(), Color.Yellow);
                    if (btnPlusAbwehr.IsHovered() || btnMinusAbwehr.IsHovered())
                    {
                        spriteBatch.Draw(hintBackground, hintPos, Color.White);
                        spriteBatch.DrawString(textFont, "Defense is against \nphysical Attacks \n(Normal Fight)", hintTextPos, Color.Yellow);
                    }

                    btnPlusWortgewandtheit.Draw(spriteBatch, textFont);
                    btnMinusWortgewandtheit.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Interaction: " + HexMap._board[x, y].Charakter.Wortgewandheit + statChanges[4], btnMinusWortgewandtheit.GetPosRightOf(), Color.Yellow);
                    if (btnPlusWortgewandtheit.IsHovered() || btnMinusWortgewandtheit.IsHovered())
                    {
                        spriteBatch.Draw(hintBackground, hintPos, Color.White);
                        spriteBatch.DrawString(textFont, "Interaction is the \nsocial Attack \n(Interact Fight)", hintTextPos, Color.Yellow);
                    }

                    btnPlusLautstaerke.Draw(spriteBatch, textFont);
                    btnMinusLautstaerke.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Loudness: " + HexMap._board[x, y].Charakter.Lautstaerke + statChanges[5], btnMinusLautstaerke.GetPosRightOf(), Color.Yellow);
                    if (btnPlusLautstaerke.IsHovered() || btnMinusLautstaerke.IsHovered())
                    {
                        spriteBatch.Draw(hintBackground, hintPos, Color.White);
                        spriteBatch.DrawString(textFont, "Loudness \ncontributes to \nInteraction \n(Interact Fight)", hintTextPos, Color.Yellow);
                    }

                    btnPlusIgnoranz.Draw(spriteBatch, textFont);
                    btnMinusIgnoranz.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Ignorance: " + HexMap._board[x, y].Charakter.Ignoranz + statChanges[6], btnMinusIgnoranz.GetPosRightOf(), Color.Yellow);
                    if (btnPlusIgnoranz.IsHovered() || btnMinusIgnoranz.IsHovered())
                    {
                        spriteBatch.Draw(hintBackground, hintPos, Color.White);
                        spriteBatch.DrawString(textFont, "Ignorance is \nagainst social \nAttacks \n(Interact Fight)", hintTextPos, Color.Yellow);
                    }

                    btnPlusGeschwindigkeit.Draw(spriteBatch, textFont);
                    btnMinusGeschwindigkeit.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Speed: " + HexMap._board[x, y].Charakter.Geschwindigkeit + statChanges[7], btnMinusGeschwindigkeit.GetPosRightOf(), Color.Yellow);
                    if (btnPlusGeschwindigkeit.IsHovered() || btnMinusGeschwindigkeit.IsHovered())
                    {
                        spriteBatch.Draw(hintBackground, hintPos, Color.White);
                        spriteBatch.DrawString(textFont, "Speed is used to \ncalculate the \nAmount of \nAttacks possible \n(Normal and \nInteract Fight)", hintTextPos, Color.Yellow);
                    }

                    btnPlusGlueck.Draw(spriteBatch, textFont);
                    btnMinusGlueck.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Luck: " + HexMap._board[x, y].Charakter.Glueck + statChanges[8], btnMinusGlueck.GetPosRightOf(), Color.Yellow);
                    if (btnPlusGlueck.IsHovered() || btnMinusGlueck.IsHovered())
                    {
                        spriteBatch.Draw(hintBackground, hintPos, Color.White);
                        spriteBatch.DrawString(textFont, "Luck is used to \ncalculate Crit \nChance and \npossible Weapon \ndrops \n(Normal and \nInteract Fights)", hintTextPos, Color.Yellow);
                    }

                    //btnPlusBewegung.Draw(spriteBatch, textFont);
                    //btnMinusBewegung.Draw(spriteBatch, textFont);
                    //spriteBatch.DrawString(textFont, "Bewegungsreichweite: " + HexMap._board[x, y].Charakter.Bewegungsreichweite, btnMinusBewegung.GetPosRightOf(), Color.Yellow);

                    spriteBatch.End();
                }
            }
        }


    }
}
