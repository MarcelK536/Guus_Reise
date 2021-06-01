using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class SkillUpMenu : SimpleMenu
    {
        public GraphicsDevice GraphicsDevice { get; }
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


        public SkillUpMenu(SpriteFont moveMenuFont, GraphicsDevice graphicsDevice) : base(new Vector2(), new Texture2D(graphicsDevice, 350, 600), moveMenuFont, graphicsDevice) 
        {
            GraphicsDevice = graphicsDevice;
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
            //btnPlusBewegung = new Button("+", btnTexture, 1, btnPlusGlueck.GetPosBelow());
        }

        public override void Update()
        {
            base.Update();
            if (Active)
            {
                int x = Player1.activeTile.LogicalPosition.X;
                int y = Player1.activeTile.LogicalPosition.Y;
                if (btnPlusWiderstandskraft.IsClicked() && HexMap._board[x,y].Charakter.Fähigkeitspunkte > 0)
                {
                    HexMap._board[x, y].Charakter.Widerstandskraft++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusKoerperkraft.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    HexMap._board[x, y].Charakter.Koerperkraft++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusBeweglichkeit.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    HexMap._board[x, y].Charakter.Beweglichkeit++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusAbwehr.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    HexMap._board[x, y].Charakter.Abwehr++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusWortgewandtheit.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    HexMap._board[x, y].Charakter.Wortgewandheit++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusLautstaerke.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    HexMap._board[x, y].Charakter.Lautstaerke++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusIgnoranz.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    HexMap._board[x, y].Charakter.Ignoranz++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusGeschwindigkeit.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    HexMap._board[x, y].Charakter.Geschwindigkeit++;
                    HexMap._board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusGlueck.IsClicked() && HexMap._board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
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
                int x = Player1.activeTile.LogicalPosition.X;
                int y = Player1.activeTile.LogicalPosition.Y;
                if (HexMap._board[x, y].Charakter == null)
                {
                    spriteBatch.Begin();
                    spriteBatch.DrawString(textFont, "Kein Charakter ausgewaehlt", btnClose.GetPosRightOf(), Color.Yellow);
                    spriteBatch.End();
                }
                else
                {
                    spriteBatch.Begin();
                    spriteBatch.DrawString(textFont, "Name: " + HexMap._board[x, y].Charakter.Name + " Punkte: " + HexMap._board[x, y].Charakter.Fähigkeitspunkte, btnClose.GetPosRightOf(), Color.Yellow);
                    btnPlusWiderstandskraft.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Widerstandskraft: " + HexMap._board[x, y].Charakter.Widerstandskraft, btnPlusWiderstandskraft.GetPosRightOf(), Color.Yellow);
                    btnPlusKoerperkraft.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Koerperkraft: " + HexMap._board[x, y].Charakter.Koerperkraft, btnPlusKoerperkraft.GetPosRightOf(), Color.Yellow);
                    btnPlusBeweglichkeit.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Beweglichkeit: " + HexMap._board[x, y].Charakter.Beweglichkeit, btnPlusBeweglichkeit.GetPosRightOf(), Color.Yellow);
                    btnPlusAbwehr.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Abwehr: " + HexMap._board[x, y].Charakter.Abwehr, btnPlusAbwehr.GetPosRightOf(), Color.Yellow);
                    btnPlusWortgewandtheit.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Wortgewandheit: " + HexMap._board[x, y].Charakter.Wortgewandheit, btnPlusWortgewandtheit.GetPosRightOf(), Color.Yellow);
                    btnPlusLautstaerke.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Lautstaerke: " + HexMap._board[x, y].Charakter.Lautstaerke, btnPlusLautstaerke.GetPosRightOf(), Color.Yellow);
                    btnPlusIgnoranz.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Ignoranz: " + HexMap._board[x, y].Charakter.Ignoranz, btnPlusIgnoranz.GetPosRightOf(), Color.Yellow);
                    btnPlusGeschwindigkeit.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Geschwindigkeit: " + HexMap._board[x, y].Charakter.Geschwindigkeit, btnPlusGeschwindigkeit.GetPosRightOf(), Color.Yellow);
                    btnPlusGlueck.Draw(spriteBatch, textFont);
                    spriteBatch.DrawString(textFont, "Glueck: " + HexMap._board[x, y].Charakter.Glueck, btnPlusGlueck.GetPosRightOf(), Color.Yellow);
                    //btnPlusBewegung.Draw(spriteBatch, textFont);
                    //spriteBatch.DrawString(textFont, "Bewegungsreichweite: " + HexMap._board[x, y].Charakter.Bewegungsreichweite, btnPlusBewegung.GetPosRightOf(), Color.Yellow);
                    spriteBatch.End();
                }
            }
        }


    }
}
