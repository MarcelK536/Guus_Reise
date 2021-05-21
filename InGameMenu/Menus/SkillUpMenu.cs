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
        public Charakter hero;

        public SkillUpMenu(Charakter charakter, SpriteFont moveMenuFont, GraphicsDevice graphicsDevice) : base(new Vector2(), new Texture2D(graphicsDevice, 350, 600), moveMenuFont, graphicsDevice) 
        {
            hero = charakter;

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

        public override void Update()
        {
            base.Update();
            if (Active)
            {
                if (btnPlusWiderstandskraft.IsClicked())
                {
                    hero.Widerstandskraft++;
                }
                if (btnPlusKoerperkraft.IsClicked())
                {
                    hero.Koerperkraft++;
                }
                if (btnPlusAbwehr.IsClicked())
                {
                    hero.Abwehr++;
                }
                if (btnPlusWortgewandtheit.IsClicked())
                {
                    hero.Wortgewandheit++;
                }
                if (btnPlusIgnoranz.IsClicked())
                {
                    hero.Ignoranz++;
                }
                if (btnPlusGeschwindigkeit.IsClicked())
                {
                    hero.Geschwindigkeit++;
                }
                if (btnPlusGlueck.IsClicked())
                {
                    hero.Glueck++;
                }
                if (btnPlusBewegung.IsClicked())
                {
                    hero.Bewegungsreichweite++;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Active)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(textFont, "Name: " + hero.Name + " Level: " + hero.Level, btnClose.GetPosRightOf(), Color.Yellow);
                btnPlusWiderstandskraft.Draw(spriteBatch,textFont);
                spriteBatch.DrawString(textFont, "Widerstandskraft: " + hero.Widerstandskraft, btnPlusWiderstandskraft.GetPosRightOf(), Color.Yellow);
                btnPlusKoerperkraft.Draw(spriteBatch, textFont);
                spriteBatch.DrawString(textFont, "Koerperkraft: " + hero.Koerperkraft, btnPlusKoerperkraft.GetPosRightOf(), Color.Yellow);
                btnPlusAbwehr.Draw(spriteBatch, textFont);
                spriteBatch.DrawString(textFont, "Abwehr: " + hero.Abwehr, btnPlusAbwehr.GetPosRightOf(), Color.Yellow);
                btnPlusWortgewandtheit.Draw(spriteBatch, textFont);
                spriteBatch.DrawString(textFont, "Wortgewandheit: " + hero.Wortgewandheit, btnPlusWortgewandtheit.GetPosRightOf(), Color.Yellow);
                btnPlusIgnoranz.Draw(spriteBatch, textFont);
                spriteBatch.DrawString(textFont, "Ignoranz: " + hero.Ignoranz, btnPlusIgnoranz.GetPosRightOf(), Color.Yellow);
                btnPlusGeschwindigkeit.Draw(spriteBatch, textFont);
                spriteBatch.DrawString(textFont, "Geschwindigkeit: " + hero.Geschwindigkeit, btnPlusGeschwindigkeit.GetPosRightOf(), Color.Yellow);
                btnPlusGlueck.Draw(spriteBatch, textFont); 
                spriteBatch.DrawString(textFont, "Glueck: " + hero.Glueck, btnPlusGlueck.GetPosRightOf(), Color.Yellow);
                btnPlusBewegung.Draw(spriteBatch, textFont);
                spriteBatch.DrawString(textFont, "Bewegungsreichweite: " + hero.Bewegungsreichweite, btnPlusBewegung.GetPosRightOf(), Color.Yellow);
                spriteBatch.End();
            }
        }


    }
}
