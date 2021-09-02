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

        SoundEffect _clickSound;


        public SkillUpMenu(SpriteFont moveMenuFont, GraphicsDevice graphicsDevice, BlendDirection blend, SoundEffect clickSound) : base(new Vector2(), moveMenuFont,  graphicsDevice, blend) 
        {
            Texture2D btnTexture = new Texture2D(graphicsDevice, 25, 25);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.Aquamarine;
            }
            btnTexture.SetData(btnColor);
            btnPlusWiderstandskraft = new Button("+", btnTexture, 1, btnClose.GetPosBelow());
            btnPlusKoerperkraft = new Button("+", btnTexture, 1, btnPlusWiderstandskraft.GetPosBelow());
            btnPlusBeweglichkeit = new Button("+", btnTexture, 1, btnPlusKoerperkraft.GetPosBelow());
            btnPlusAbwehr = new Button("+", btnTexture, 1, btnPlusBeweglichkeit.GetPosBelow());
            btnPlusWortgewandtheit = new Button("+", btnTexture, 1, btnPlusAbwehr.GetPosBelow());
            btnPlusLautstaerke = new Button("+", btnTexture, 1, btnPlusWortgewandtheit.GetPosBelow());
            btnPlusIgnoranz = new Button("+", btnTexture, 1, btnPlusLautstaerke.GetPosBelow());
            btnPlusGeschwindigkeit = new Button("+", btnTexture, 1, btnPlusIgnoranz.GetPosBelow());
            btnPlusGlueck = new Button("+", btnTexture, 1, btnPlusGeschwindigkeit.GetPosBelow());
            menuButtons.Add(btnPlusGlueck);
            //btnPlusBewegung = new Button("+", btnTexture, 1, btnPlusGlueck.GetPosBelow());

            SetMenuHeight();
            menuWidth = btnClose.TextureDefault.Width + moveMenuFont.MeasureString("Name: ").X;
            SetBackgroundTexture(bkgColor);

            _clickSound = clickSound;
        }

        public override void Update()
        {
            if (Active)
            {
                if (btnClose.IsClicked())
                {
                    _clickSound.Play();
                    Player.levelUpMenu.Active = false;
                    Player.charakterMenu.Active = true;
                }

                int x = Player.activeTile.LogicalPosition.X;
                int y = Player.activeTile.LogicalPosition.Y;
                if (btnPlusWiderstandskraft.IsClicked() && HexMap._board[x,y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    HexMap._board[x, y].Charakter.Widerstandskraft++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusKoerperkraft.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    HexMap._board[x, y].Charakter.Koerperkraft++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusBeweglichkeit.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    HexMap._board[x, y].Charakter.Beweglichkeit++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusAbwehr.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    HexMap._board[x, y].Charakter.Abwehr++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusWortgewandtheit.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    HexMap._board[x, y].Charakter.Wortgewandheit++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusLautstaerke.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    HexMap._board[x, y].Charakter.Lautstaerke++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusIgnoranz.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    HexMap._board[x, y].Charakter.Ignoranz++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusGeschwindigkeit.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    HexMap._board[x, y].Charakter.Geschwindigkeit++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusGlueck.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _clickSound.Play();
                    HexMap._board[x, y].Charakter.Glueck++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                //if (btnPlusBewegung.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                //{
                //    HexMap._board[x, y].Charakter.Bewegungsreichweite++;
                //    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                //}
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

                    spriteBatch.Begin();
                    spriteBatch.DrawString(textFont, "Name: " + HexMap._board[x, y].Charakter.Name + " Points: " + HexMap._board[x, y].Charakter.Fähigkeitspunkte, btnClose.GetPosRightOf(), Color.Yellow);
                    btnPlusWiderstandskraft.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Life Points: " + HexMap._board[x, y].Charakter.Widerstandskraft, btnPlusWiderstandskraft.GetPosRightOf(), Color.Yellow);
                    btnPlusKoerperkraft.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Strength: " + HexMap._board[x, y].Charakter.Koerperkraft, btnPlusKoerperkraft.GetPosRightOf(), Color.Yellow);
                    btnPlusBeweglichkeit.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Agilty: " + HexMap._board[x, y].Charakter.Beweglichkeit, btnPlusBeweglichkeit.GetPosRightOf(), Color.Yellow);
                    btnPlusAbwehr.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Defense: " + HexMap._board[x, y].Charakter.Abwehr, btnPlusAbwehr.GetPosRightOf(), Color.Yellow);
                    btnPlusWortgewandtheit.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Interaction: " + HexMap._board[x, y].Charakter.Wortgewandheit, btnPlusWortgewandtheit.GetPosRightOf(), Color.Yellow);
                    btnPlusLautstaerke.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Loudness: " + HexMap._board[x, y].Charakter.Lautstaerke, btnPlusLautstaerke.GetPosRightOf(), Color.Yellow);
                    btnPlusIgnoranz.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Ignorance: " + HexMap._board[x, y].Charakter.Ignoranz, btnPlusIgnoranz.GetPosRightOf(), Color.Yellow);
                    btnPlusGeschwindigkeit.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Speed: " + HexMap._board[x, y].Charakter.Geschwindigkeit, btnPlusGeschwindigkeit.GetPosRightOf(), Color.Yellow);
                    btnPlusGlueck.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Luck: " + HexMap._board[x, y].Charakter.Glueck, btnPlusGlueck.GetPosRightOf(), Color.Yellow);
                    //btnPlusBewegung.Draw(spriteBatch, textFont);
                    //spriteBatch.DrawString(textFont, "Bewegungsreichweite: " + HexMap._board[x, y].Charakter.Bewegungsreichweite, btnPlusBewegung.GetPosRightOf(), Color.Yellow);
                    spriteBatch.End();
                }
            }
        }


    }
}
