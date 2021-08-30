using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guus_Reise
{
    class FightResults : SimpleMenu
    {
        private List<string> _killedEnemys = new List<string>();
        private List<string> _killedFriends = new List<string>();
        private List<string> _newFriends = new List<string>();
        private Dictionary<string, int> _earnedXP = new Dictionary<string,int>();

        public bool gameOver = false;
        public Button btnExitFight;

        public List<string> KilledEnemys { get => _killedEnemys; set => _killedEnemys = value; }
        public List<string> KilledFriends { get => _killedFriends; set => _killedFriends = value; }
        public List<string> NewFriends { get => _newFriends; set => _newFriends = value; }
        public Dictionary<string, int> EarnedXP { get => _earnedXP; set => _earnedXP = value; }

        public FightResults(SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(new Vector2(), menuFont, graphicsDevice, direction)
        {
            menuWidth = 600;
            menuHeight = 300;
            Vector2 position = new Vector2((_graphicsDevice.Viewport.Width / 2) - (int)(menuWidth / 2), (_graphicsDevice.Viewport.Height / 2) - (int)(menuHeight / 2));
            bkgPos = position;
            Texture2D btnTexture = new Texture2D(graphicsDevice, (int)btnWidth, 50);
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.YellowGreen * 0.8f;
            }
            SetBackgroundTexture(bkgColor);
            btnExitFight = new Button("Exit", btnTexture, 1, (int)(position.X+menuHeight)-btnTexture.Width/2,(int) (position.Y +(menuWidth / 2))-btnTexture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textPos = bkgPos + new Vector2(25, 25);
            spriteBatch.Begin();
            btnExitFight.Draw(spriteBatch, textFont);
            spriteBatch.Draw(bkgTexture, bkgPos, Color.White);
            foreach(string e in _killedEnemys)
            {
                spriteBatch.DrawString(textFont, e + " ist Gestorben", textPos, Color.White);
                textPos.Y += textFont.MeasureString("Placeholder").Y;
            }
            foreach(string f in _killedFriends)
            {
                spriteBatch.DrawString(textFont, f + " ist nicht mehr dabei", textPos, Color.White);
                textPos.Y += textFont.MeasureString("Placeholder").Y;
            }
            foreach(string n in _newFriends)
            {
                spriteBatch.DrawString(textFont, n + " ist der Gruppe beigetreten", textPos, Color.Green);
                textPos.Y += textFont.MeasureString("Placeholder").Y;
            }
            int pPos = 0;
            foreach (KeyValuePair<string, int> xp in _earnedXP)
            {
                spriteBatch.DrawString(textFont, xp.Key + " hat " + xp.Value + " XP erhalten.", textPos, Color.White);
                pPos++;
                textPos.Y += textFont.MeasureString("Placeholder").Y;
            }
            spriteBatch.End();
        }

        public override void Update()
        {
            if (btnClose.IsClicked() || btnExitFight.IsClicked())
            {
                if (!gameOver)
                {
                    Game1.GState = Game1.GameState.InGame;
                    Fighthandler.showFightResults = false;
                }
                else
                {
                    Game1.GState = Game1.GameState.GameOver;
                }
                KilledEnemys.Clear();
                KilledFriends.Clear();
                EarnedXP.Clear();
                NewFriends.Clear();
            }
        }
    }
}
