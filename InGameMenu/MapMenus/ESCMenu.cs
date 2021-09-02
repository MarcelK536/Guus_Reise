using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;


namespace Guus_Reise
{
    class ESCMenu : SimpleMenu
    {
        Button btnControls;
        Button btnQuitGame;
        //Button btnSaveGame;

        ControlView controlView;

        static SoundEffect _clickSound;

        public ESCMenu(SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction, SoundEffect clickSound) : base(new Vector2(), menuFont, graphicsDevice, direction, _clickSound)
        {
            btnWidth = menuFont.MeasureString("Quit Game ").X;
            Texture2D btnTexture = new Texture2D(graphicsDevice, (int)btnWidth, 50);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i=0;i<btnColor.Length; i++)
            {
                btnColor[i] = Color.Aquamarine * 0.5f;
            }
            btnTexture.SetData(btnColor);
            btnControls = new Button("Controls", btnTexture, 1, btnClose.GetPosBelow());
            menuButtons.Add(btnControls);
            //btnSaveGame = new Button("Save", btnTexture, 1, btnControls.GetPosBelow());
            //menuButtons.Add(btnSaveGame);
            btnQuitGame = new Button("Quit Game", btnTexture, 1, btnControls.GetPosBelow());
            menuButtons.Add(btnQuitGame);

            controlView = new ControlView(btnControls.GetPosRightOf(),menuFont,graphicsDevice,BlendDirection.None);

            SetMenuHeight();
            SetMenuWidth();
            SetBackgroundTexture(Color.GhostWhite);
            _clickSound = clickSound;
        }
        public void Update(GameTime gameTime)
        {
            if (Active)
            {
                if (btnClose.IsClicked())
                {
                    _clickSound.Play();
                    Active = false;
                    if (controlView != null)
                    {
                        controlView.Active = false;
                    }
                }

                if (btnQuitGame.IsClicked())
                {
                    _clickSound.Play();
                    Game1.GState = Game1.GameState.MainMenu;
                }
                //if (btnSaveGame.IsClicked())
               // {
                    //TODO SAVE GAME
               // }
                if (btnControls.IsClicked())
                {
                    _clickSound.Play();
                    controlView.Active = !controlView.Active;
                }
                controlView.Update();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Active)
            {
                spriteBatch.Begin();
                btnControls.Draw(spriteBatch, textFont);
               // btnSaveGame.Draw(spriteBatch, textFont);
                btnQuitGame.Draw(spriteBatch, textFont);
                spriteBatch.End();

                if (controlView.Active)
                {
                    controlView.Draw(spriteBatch);
                }
            }
        }
    }
}
