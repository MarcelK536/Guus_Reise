using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guus_Reise
{
    class FightTurnBar
    {
        List<Charakter> FightCharacters = new List<Charakter>(); //Liste mit allen Charaktern die im Kampf sind
        public List<Charakter> NextTurn = new List<Charakter>(); //Liste wo am Index 0 der nächste Charakter ist der angreifen darf
        public List<Charakter> _prevNextTurn = new List<Charakter>();

        Texture2D barTexture;
        SpriteFont barFont = Player.actionMenuFont;

        Vector2 barPos;
        public int barWidth;
        public int barHeight;

        Vector2 charPos;

        public FightTurnBar(GraphicsDevice graphicsDevice, List<Hex> players, List<Hex> enemys)
        {
            barHeight = 50;
            barWidth = 400;

            UpdateBarTexture(graphicsDevice);
            barPos.X = graphicsDevice.Viewport.Width / 2 - barTexture.Width / 2;
            barPos.Y = 50;

            charPos = barPos + Vector2.UnitY * barPos.Y;
            foreach(Hex hex in players)
            {
                FightCharacters.Add(hex.Charakter);
            }
            foreach(Hex hex in enemys)
            {
                FightCharacters.Add(hex.Charakter);
            }

            InitNextTurn();
        }

        public void InitNextTurn()
        {
            NextTurn = FightCharacters.OrderBy(e => e.CurrentFightStats[7]).ToList();
            _prevNextTurn = CreateCopy(NextTurn);
        }

        public List<Charakter> CreateCopy(List<Charakter> currList)
        {       
             List<Charakter> tempCopy = new List<Charakter>();
             for(int i =0; i< currList.Count; i++)
             {
                 tempCopy.Add(currList[i].Clone());
             }
             return tempCopy;
        }

        public void AddCharakter(Charakter c)  //Fügt Charakter zur Liste hinzu
        {
            NextTurn.Add(c);
            ReSort();
            _prevNextTurn = CreateCopy(NextTurn);
        }

        public void RemoveCharakter(Charakter c)
        {
            if (NextTurn.Contains(c))
            {
                NextTurn.Remove(c);
            }
            _prevNextTurn = CreateCopy(NextTurn);
        }

        public void ReSort()
        {
            if (!IsSameList(_prevNextTurn))
            {
                List<Charakter> sortedList = NextTurn.OrderBy(e => e.CurrentFightStats[7]).ToList(); //Sortiert Aufsteigend
                if (IsSameList(sortedList))
                {
                    NextTurn.Add(NextTurn.First());
                    NextTurn.Remove(NextTurn.First());
                }
                else
                {
                    NextTurn = sortedList;
                }
                _prevNextTurn = CreateCopy(NextTurn);
            }
        }

        public bool IsSameList(List<Charakter> newList)
        {
            if(newList.Count != NextTurn.Count)
            {
                return false;
            }
            for(int i=0; i < newList.Count; i++)
            {
                    if (!Enumerable.SequenceEqual(newList[i].CurrentFightStats, NextTurn[i].CurrentFightStats))
                    {
                        if (newList[i].Name == NextTurn[i].Name)
                        {
                            return false;
                        }
                    }
            }
            return true;
        }

        public Charakter ReturnCurrentCharakter()
        {
            return NextTurn.First();
        }

        public void UpdateBarTexture(GraphicsDevice graphicsDevice)
        {
            barTexture = new Texture2D(graphicsDevice, barWidth, barHeight);
            Color[] barColor = new Color[barTexture.Width * barTexture.Height];
            for (int i = 0; i < barColor.Length; i++)
            {
                barColor[i] = Color.BurlyWood * 0.8f;
            }
            barTexture.SetData(barColor);
        }

        public void Update(GraphicsDevice graphicsDevice)
        {
            UpdateBarTexture(graphicsDevice);
            barPos.X = graphicsDevice.Viewport.Width / 2 - barTexture.Width / 2;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(barTexture, barPos, Color.White);
            foreach(Charakter c in NextTurn){
                //TODO Charaktere haben kein abrufbares Standbild --> spriteBatch.Draw(c.Picture, charPos, Color.White);
                spriteBatch.DrawString(barFont, c.Name, charPos, Color.Yellow);
                charPos += Vector2.UnitX * barFont.MeasureString(c.Name + " ");
            }
            charPos = barPos;
            spriteBatch.End();
        }

    }
}
