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
        Button btnPlus;
        public Charakter hero;

        public SkillUpMenu(Charakter charakter, SpriteFont moveMenuFont, GraphicsDevice graphicsDevice) : base(new Vector2(), new Texture2D(graphicsDevice, 350, 600), moveMenuFont, graphicsDevice) 
        {
            hero = charakter;

            GraphicsDevice = graphicsDevice;
            Texture2D btnTexture = new Texture2D(graphicsDevice, 100, 50);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.Aquamarine;
            }
            btnTexture.SetData(btnColor);
            btnPlus = new Button("+", btnTexture, 1, (int)pos.X, (int)pos.Y + btnTexture.Height);
        }

        public override void Update()
        {
            base.Update();
            if (Active)
            {
                if (btnPlus.IsClicked())
                {
                    hero.Level++;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Active)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(textFont,"Name: " + hero.Name + " Level: " + hero.Level, pos, Color.Yellow);
                btnPlus.Draw(spriteBatch,textFont);
                spriteBatch.End();
            }
        }


    }
}
