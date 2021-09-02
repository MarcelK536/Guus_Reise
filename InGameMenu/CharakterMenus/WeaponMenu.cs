using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Guus_Reise
{
    class WeaponMenu : SimpleMenu
    {
        Texture2D btnTexture;
        Texture2D btnTextureSelected;
        int lastWheel = 0;
        static SoundEffect _clickSound;
        private bool[] preClickState;

        public WeaponMenu(List<Weapon> weapons, Vector2 position, SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(position, menuFont, graphicsDevice, direction)
        {
            menuWidth = 200;
            menuHeight = 200;
            btnWidth = 15;
            Vector2 btnPosition = btnClose.GetPosBelow();
            Init(Fighthandler.contentFight);
            preClickState = new bool[20];


            foreach (Weapon item in weapons)
            {
                if(btnWidth < textFont.MeasureString(item.Name).X)
                {
                    btnWidth = textFont.MeasureString(item.Name).X;
                }
            }
            menuWidth = btnWidth;
            btnTexture = new Texture2D(graphicsDevice, (int)btnWidth, 25);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.YellowGreen * 0.8f;
            }
            btnTexture.SetData(btnColor);
            btnTextureSelected = new Texture2D(graphicsDevice, (int)btnWidth, 25);
            Color[] btnColorSelected = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColorSelected[i] = Color.Yellow * 0.8f;
            }
            btnTextureSelected.SetData(btnColorSelected);

            foreach (Weapon item in weapons)
            {
                menuButtons.Add(new Button(item.Name, btnTexture, 1, btnPosition));
                btnPosition.Y += btnTexture.Height + 10;
            }
            
        }

        internal static void Init(ContentManager content)
        {
            _clickSound = content.Load<SoundEffect>("Sounds\\mixkit-positive-interface-click-1112");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Active)
            {
                RasterizerState rasterizerState = new RasterizerState() { ScissorTestEnable = true };
                
                spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend,null,null,rasterizerState);

                Rectangle tempScissorRect = spriteBatch.GraphicsDevice.ScissorRectangle;

                spriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle((int)pos.X, (int)pos.Y, (int)menuWidth, (int)menuHeight);
                
                foreach (Button btn in menuButtons)
                {
                    btn.Draw(spriteBatch, textFont);
                }

                spriteBatch.GraphicsDevice.ScissorRectangle = tempScissorRect;
                spriteBatch.End();
            }
        }

        public void Update(GameTime time) 
        {
            int x = Player.activeTile.LogicalBoardPosition.X;
            int y = Player.activeTile.LogicalBoardPosition.Y;

            SetBackgroundTexture(Color.CornflowerBlue, 1f);
            int j = 0;

            foreach(Button btn in menuButtons)
            {
                if (btn.Name == "Close")
                {
                    continue;
                }
                if(btn.Name == HexMap._board[x, y].Charakter.Weapon.Name)
                {
                    btn.TextureDefault = btnTextureSelected;
                    btn.TextureHover = btnTextureSelected;
                }
                else
                {
                    btn.TextureDefault = btnTexture;
                    btn.TextureHover = btnTexture;
                }
                if (btn.IsClicked())
                {
                    if (!preClickState[j])
                    {
                        _clickSound.Play();
                        preClickState[j] = true;
                    }
                    int weaponIndex = Weapon.weapons.IndexOf(Weapon.weapons.Where(p => p.Name == btn.Name).First());
                    HexMap._board[x, y].Charakter.Weapon = Weapon.weapons[weaponIndex];
                }
                else
                {
                    preClickState[j] = false;
                }
                j++;
            }

            if (btnClose.IsClicked())
            {
                _clickSound.Play();
                Active = false;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Up) || Mouse.GetState().ScrollWheelValue > lastWheel)&& menuButtons[0].ButtonY + menuButtons[0].TextureDefault.Height > menuButtons[1].ButtonY)
            {
                lastWheel = Mouse.GetState().ScrollWheelValue;
                UpdateButtons(btnTexture.Height);
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Down) || Mouse.GetState().ScrollWheelValue < lastWheel) && menuButtons.Last().GetPosBelow().Y + btnTexture.Height > menuHeight + pos.Y)
            {
                lastWheel = Mouse.GetState().ScrollWheelValue;
                UpdateButtons(-btnTexture.Height);
            }
        }

        public void UpdateButtons(int yDirection)
        {
            foreach (Button btn in menuButtons)
            {
                if(btn.Name == "Close")
                {
                    continue;
                }
                btn.ButtonY += yDirection;
            }
        }
    }
}
