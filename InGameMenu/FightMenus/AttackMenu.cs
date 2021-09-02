using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Guus_Reise
{
    class AttackMenu : SimpleMenu
    {
        Charakter currCharakter;
        Texture2D btnTexture;
        static SoundEffect _clickSound;
        public static GraphicsDevice graphicDevice;
        public AttackMenu(Vector2 position, SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(position, menuFont, graphicsDevice, direction)
        {
            menuWidth = 200;
            menuHeight = 200;
            btnWidth = 25;
            Vector2 btnPosition = btnClose.GetPosBelow();
            Init(Fighthandler.contentFight);
            currCharakter = Fighthandler.turnBar.ReturnCurrentCharakter(); 

            graphicDevice = graphicsDevice;

            List<Skill> currentSkillList;

            if (Game1.GState == Game1.GameState.InTalkFight)
            {
                currentSkillList = currCharakter.SkillTalk;

            }
            else
            {
                currentSkillList = currCharakter.Skill;
            }

            foreach (Skill s in currentSkillList)
            {
                if (btnWidth < textFont.MeasureString(s.Name).X)
                {
                    btnWidth = textFont.MeasureString(s.Name).X;
                }
            }


            btnTexture = new Texture2D(graphicsDevice, (int)btnWidth, 25);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.YellowGreen * 0.8f;
            }
            btnTexture.SetData(btnColor);

            foreach (Skill s in currentSkillList)
            {
                menuButtons.Add(new Button(s.Name, btnTexture, 1, btnPosition));
                btnPosition.Y += btnTexture.Height + 10;
            }
        }
        internal static void Init(ContentManager content)
        {
            _clickSound = content.Load<SoundEffect>("Sounds\\mixkit-positive-interface-click-1112");
        }

        public void Update(GameTime time) 
        {
            if (FightPlayer.isSelecting == true)
            {
                Active = false;
                FightPlayer.PrepareMove();
            }
            else
            {
                foreach (Button btn in menuButtons)
                {
                    if (btn.Name == "Close")
                    {
                        continue;
                    }
                    if (btn.IsClicked())
                    {
                        Skill selSkill = Skill.skills.Where(p => p.Name == btn.Name).First();
                        Active = false;
                        FightPlayer.SaveMove(selSkill);
                        Fighthandler.turnBar.ReSort();
                        _clickSound.Play();
                    }
                }
                if (btnClose.IsClicked())
                {
                    Active = false;
                    _clickSound.Play();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            RasterizerState rasterizerState = new RasterizerState() { ScissorTestEnable = true };

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, rasterizerState);

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
}
