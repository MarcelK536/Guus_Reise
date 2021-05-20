using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Guus_Reise
{
    class MoveMenu : SimpleMenu
    {
        public GraphicsDevice GraphicsDevice{ get; }
        Button btnConfirm;
        Button btnQuitGame;

        public MoveMenu(SpriteFont moveMenuFont, GraphicsDevice graphicsDevice) : base(new Vector2(), new Texture2D(graphicsDevice, 350, 600), moveMenuFont,graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
            Texture2D btnTexture = new Texture2D(graphicsDevice, 100, 50);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.Aquamarine;
            }
            btnTexture.SetData(btnColor);
            btnConfirm = new Button("Confirm Move", btnTexture, 1, (int)pos.X, (int)pos.Y+btnTexture.Height);
            btnQuitGame = new Button("Quit Game", btnTexture, 1, (int)pos.X, (int)pos.Y+btnTexture.Height*2);
        }

        public override void Update()
        {
            base.Update();
            if (Active)
            {
                if (btnQuitGame.IsClicked())
                {
                    Game1.GState = Game1.GameState.MainMenu;
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
                btnQuitGame.Draw(spriteBatch, textFont);
                spriteBatch.End();
            }
        }
    }
}
