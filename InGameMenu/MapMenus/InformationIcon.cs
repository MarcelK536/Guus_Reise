using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guus_Reise
{
    class InformationIcon
    {
        private Dictionary<string,int> hasSkillPoints = new Dictionary<string, int>();
        private List<string> canMoveList = new List<string>();
        static Texture2D infoIcon;
        static SpriteFont infoFont;
        static Button btnInfo;

        public List<string> CanMoveList { get => canMoveList; set => canMoveList = value; }
        public Dictionary<string, int> HasSkillPoints { get => hasSkillPoints; set => hasSkillPoints = value; }

        public static void LoadTexture(ContentManager content)
        {
            infoIcon = content.Load<Texture2D>("Buttons\\ausrufezeichen");
            infoFont = content.Load<SpriteFont>("Fonts\\Jellee20");
        }

        public static void Init(GraphicsDevice _graphicsDevice)
        {
            btnInfo = new Button("", infoIcon, 0.2f, new Vector2(_graphicsDevice.Viewport.Width - infoIcon.Width, _graphicsDevice.Viewport.Height - infoIcon.Height));
        }

        public void Update(GraphicsDevice _graphicsDevice)
        {
            btnInfo.MoveButton(new Vector2(_graphicsDevice.Viewport.Width - infoIcon.Width*btnInfo.Scale, _graphicsDevice.Viewport.Height - infoIcon.Height*btnInfo.Scale));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            btnInfo.Draw(spriteBatch, infoFont);
            Vector2 textPos = btnInfo.GetTextPosAbove(infoFont);
            textPos.X -= 175;
            if (btnInfo.IsHovered())
            {
                foreach(string s in canMoveList)
                {
                    spriteBatch.DrawString(infoFont, s + " can Move", textPos, Color.Yellow);
                    textPos.Y -= infoFont.MeasureString("Placeholder").Y * 2 + 10;
                }
                if (hasSkillPoints.Count >= 0)
                {
                    spriteBatch.DrawString(infoFont, "Press H on a Character\nto use them.", textPos, Color.Yellow);
                    textPos.Y -= infoFont.MeasureString("Placeholder").Y * 2 + 10;
                }
                foreach (KeyValuePair<string,int> remSkillPoints in hasSkillPoints)
                {
                    spriteBatch.DrawString(infoFont, remSkillPoints.Key + " has " + remSkillPoints.Value + "\nunused SkillPoints",textPos, Color.Yellow);
                    textPos.Y -= infoFont.MeasureString("Placeholder").Y * 2 + 10;
                }
                if(HexMap.lvlObjectives.Any(x => x==false))
                {
                    spriteBatch.DrawString(infoFont, "You have unfinished level Tasks.\nPress G to see more", textPos, Color.Yellow);
                }
            }
            spriteBatch.End();
        }

        public void Clear()
        {
            CanMoveList.Clear();
            HasSkillPoints.Clear();
        }
    }
}
