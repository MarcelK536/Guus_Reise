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
        Button btnPlusAbwehr;
        Button btnPlusWortgewandtheit;
        Button btnPlusIgnoranz;
        Button btnPlusGeschwindigkeit;
        Button btnPlusGlueck;
        Button btnPlusBewegung;


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
            btnPlusAbwehr = new Button("+", btnTexture, 1, btnPlusKoerperkraft.GetPosBelow());
            btnPlusWortgewandtheit = new Button("+", btnTexture, 1, btnPlusAbwehr.GetPosBelow());
            btnPlusIgnoranz = new Button("+", btnTexture, 1, btnPlusWortgewandtheit.GetPosBelow());
            btnPlusGeschwindigkeit = new Button("+", btnTexture, 1, btnPlusIgnoranz.GetPosBelow());
            btnPlusGlueck = new Button("+", btnTexture, 1, btnPlusGeschwindigkeit.GetPosBelow());
            btnPlusBewegung = new Button("+", btnTexture, 1, btnPlusGlueck.GetPosBelow());
        }

        public void Update(Tile[,] _board, Tile _activeTile)
        {
            base.Update();
            if (Active)
            {
                int x = _activeTile.LogicalPosition.X;
                int y = _activeTile.LogicalPosition.Y;
                if (btnPlusWiderstandskraft.IsClicked() && _board[x,y].Charakter.Fähigkeitspunkte > 0)
                {
                    _board[x, y].Charakter.Widerstandskraft++;
                    _board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusKoerperkraft.IsClicked() && _board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _board[x, y].Charakter.Koerperkraft++;
                    _board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusAbwehr.IsClicked() && _board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _board[x, y].Charakter.Abwehr++;
                    _board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusWortgewandtheit.IsClicked() && _board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _board[x, y].Charakter.Wortgewandheit++;
                    _board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusIgnoranz.IsClicked() && _board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _board[x, y].Charakter.Ignoranz++;
                    _board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusGeschwindigkeit.IsClicked() && _board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _board[x, y].Charakter.Geschwindigkeit++;
                    _board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusGlueck.IsClicked() && _board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _board[x, y].Charakter.Glueck++;
                    _board[x, y].Charakter.Fähigkeitspunkte--;
                }
                if (btnPlusBewegung.IsClicked() && _board[x, y].Charakter.Fähigkeitspunkte > 0)
                {
                    _board[x, y].Charakter.Bewegungsreichweite++;
                    _board[x, y].Charakter.Fähigkeitspunkte--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Tile[,] _board, Tile _activeTile)
        {
            base.Draw(spriteBatch);
            if (Active)
            {
                int x = _activeTile.LogicalPosition.X;
                int y = _activeTile.LogicalPosition.Y;
                spriteBatch.Begin();
                spriteBatch.DrawString(textFont, "Name: " + _board[x, y].Charakter.Name + " Punkte: " + _board[x, y].Charakter.Fähigkeitspunkte, btnClose.GetPosRightOf(), Color.Yellow);
                btnPlusWiderstandskraft.Draw(spriteBatch,textFont);
                spriteBatch.DrawString(textFont, "Widerstandskraft: " + _board[x, y].Charakter.Widerstandskraft, btnPlusWiderstandskraft.GetPosRightOf(), Color.Yellow);
                btnPlusKoerperkraft.Draw(spriteBatch, textFont);
                spriteBatch.DrawString(textFont, "Koerperkraft: " + _board[x, y].Charakter.Koerperkraft, btnPlusKoerperkraft.GetPosRightOf(), Color.Yellow);
                btnPlusAbwehr.Draw(spriteBatch, textFont);
                spriteBatch.DrawString(textFont, "Abwehr: " + _board[x, y].Charakter.Abwehr, btnPlusAbwehr.GetPosRightOf(), Color.Yellow);
                btnPlusWortgewandtheit.Draw(spriteBatch, textFont);
                spriteBatch.DrawString(textFont, "Wortgewandheit: " + _board[x, y].Charakter.Wortgewandheit, btnPlusWortgewandtheit.GetPosRightOf(), Color.Yellow);
                btnPlusIgnoranz.Draw(spriteBatch, textFont);
                spriteBatch.DrawString(textFont, "Ignoranz: " + _board[x, y].Charakter.Ignoranz, btnPlusIgnoranz.GetPosRightOf(), Color.Yellow);
                btnPlusGeschwindigkeit.Draw(spriteBatch, textFont);
                spriteBatch.DrawString(textFont, "Geschwindigkeit: " + _board[x, y].Charakter.Geschwindigkeit, btnPlusGeschwindigkeit.GetPosRightOf(), Color.Yellow);
                btnPlusGlueck.Draw(spriteBatch, textFont); 
                spriteBatch.DrawString(textFont, "Glueck: " + _board[x, y].Charakter.Glueck, btnPlusGlueck.GetPosRightOf(), Color.Yellow);
                btnPlusBewegung.Draw(spriteBatch, textFont);
                spriteBatch.DrawString(textFont, "Bewegungsreichweite: " + _board[x, y].Charakter.Bewegungsreichweite, btnPlusBewegung.GetPosRightOf(), Color.Yellow);
                spriteBatch.End();
            }
        }


    }
}
