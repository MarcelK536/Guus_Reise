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
    class FightResults : SimpleMenu
    {
        private List<string> _killedEnemys = new List<string>();
        private List<string> _killedFriends = new List<string>();
        private List<string> _newFriends = new List<string>();
        private List<string> _levelUp = new List<string>();
        private Dictionary<string, int> _earnedXP = new Dictionary<string,int>();
        private Dictionary<string, List<string>> _earnedWeapons = new Dictionary<string, List<string>>();

        public bool gameOver = false;
        public bool gaveUp = false;
        public Button btnExitFight;
        private Texture2D btnTexture;
        static SoundEffect _clickSound;

        public List<string> KilledEnemys { get => _killedEnemys; set => _killedEnemys = value; }
        public List<string> KilledFriends { get => _killedFriends; set => _killedFriends = value; }
        public List<string> NewFriends { get => _newFriends; set => _newFriends = value; }
        public Dictionary<string, int> EarnedXP { get => _earnedXP; set => _earnedXP = value; }
        public Dictionary<string, List<string>> EarnedWeapons { get => _earnedWeapons; set => _earnedWeapons = value; }
        public List<string> LevelUp { get => _levelUp; set => _levelUp = value; }

        public FightResults(SpriteFont menuFont, GraphicsDevice graphicsDevice, BlendDirection direction) : base(new Vector2(), menuFont, graphicsDevice, direction, _clickSound)
        {
            Init(Fighthandler.contentFight);
            btnTexture = new Texture2D(graphicsDevice, (int)btnWidth, 50);
            btnExitFight = new Button("Exit", btnTexture, 1, 0, 0);
            UpdateScreenParameters();            
            Color[] btnColor = new Color[btnTexture.Width * btnTexture.Height];
            for (int i = 0; i < btnColor.Length; i++)
            {
                btnColor[i] = Color.YellowGreen * 0.8f;
            }
            SetBackgroundTexture(bkgColor);
            
        }

        internal static void Init(ContentManager content)
        {
            _clickSound = content.Load<SoundEffect>("Sounds\\mixkit-positive-interface-click-1112");
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textPos = bkgPos + new Vector2(25, 25);
            spriteBatch.Begin();
            btnExitFight.Draw(spriteBatch, textFont);
            spriteBatch.Draw(bkgTexture, bkgPos, Color.White);
            foreach(string e in _killedEnemys)
            {
                spriteBatch.DrawString(textFont, e + " died.", textPos, Color.White);
                textPos.Y += textFont.MeasureString("Placeholder").Y;
            }
            foreach(string f in _killedFriends)
            {
                spriteBatch.DrawString(textFont, f + " is no longer in the group.", textPos, Color.White);
                textPos.Y += textFont.MeasureString("Placeholder").Y;
            }
            foreach(string n in _newFriends)
            {
                spriteBatch.DrawString(textFont, n + " joined the group.", textPos, Color.White);
                textPos.Y += textFont.MeasureString("Placeholder").Y;
            }
            int pPos = 0;
            foreach (KeyValuePair<string, int> xp in _earnedXP)
            {
                spriteBatch.DrawString(textFont, xp.Key + " got " + xp.Value + " XP.", textPos, Color.White);
                pPos++;
                textPos.Y += textFont.MeasureString("Placeholder").Y;
            }
            foreach (string lvlUP in _levelUp)
            {
                spriteBatch.DrawString(textFont, lvlUP + " has gone up a level.", textPos, Color.White);
                textPos.Y += textFont.MeasureString("Placeholder").Y;
            }
            foreach (KeyValuePair<string,List<string>> weapons in _earnedWeapons)
            {
                spriteBatch.DrawString(textFont, weapons.Key + " found the weapon(s) " + string.Join(",",weapons.Value), textPos, Color.White);
                textPos.Y += textFont.MeasureString("Placeholder").Y;
            }
            if(gaveUp == true)
            {
                spriteBatch.DrawString(textFont, "You gave up.", textPos, Color.White);
            }
            spriteBatch.End();
        }

        public override void Update()
        {
            if (btnClose.IsClicked() || btnExitFight.IsClicked())
            {
                _clickSound.Play();
                if (!gameOver)
                {
                    Game1.GState = Game1.GameState.InGame;
                    Fighthandler.showFightResults = false;

                }
                else
                {
                    Game1.GState = Game1.GameState.GameOver;
                    Fighthandler.showFightResults = false;
                }
                if(gaveUp == true)
                {
                    gaveUp = false;
                    Fighthandler.showFightResults = false;
                    if (Fighthandler.playerTiles.Last().Charakter != null)
                    {
                        Fighthandler.playerTiles.Last().Charakter.GaveUp = true;
                    }
                }
                Player.actionMenu.Active = false;
                KilledEnemys.Clear();
                KilledFriends.Clear();
                EarnedXP.Clear();
                NewFriends.Clear();
            }
        }

        public void UpdateScreenParameters()
        {
            
            if (Game1._graphics.IsFullScreen == true)
            {
                menuWidth = 1000;
                menuHeight = 600;
                bkgPos = new Vector2((_graphicsDevice.Viewport.Width / 2) - 200, (_graphicsDevice.Viewport.Height / 2) - 100);
                btnExitFight.ButtonX = (int)(bkgPos.X +250);
                btnExitFight.ButtonY = (int)(bkgPos.Y + 250);
            }
            else
            {
                menuWidth = 600;
                menuHeight = 300;
                bkgPos = new Vector2((_graphicsDevice.Viewport.Width / 2) - (int)(menuWidth / 2), (_graphicsDevice.Viewport.Height / 2) - (int)(menuHeight / 2));
                btnExitFight.ButtonX = (int)(bkgPos.X + menuHeight) - btnTexture.Width / 2;
                btnExitFight.ButtonY = (int)(bkgPos.Y + (menuWidth / 2)) - btnTexture.Height;

            }

            




        }
    }
}
