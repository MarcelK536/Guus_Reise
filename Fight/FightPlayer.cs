using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using Microsoft.Xna.Framework.Audio;


namespace Guus_Reise
{
    class FightPlayer
    {
        public static bool isSelecting = false;
        public static Skill _selSkill = null;
        public static Hex clickedTile = null;
        public static MouseState _prevMouseState;
        public static SoundEffect _soundEffect;


        public static void PrepareMove()
        {
            if(isSelecting == true)
            {
                getTile();
            }
            if(clickedTile != null && _selSkill != null)
            {
                isSelecting = false;
                MakeMove(_selSkill, clickedTile);
                _soundEffect.Play();
                clickedTile.Tile.Glow = new Vector3(0f, 0f, 0f);
                clickedTile = null;
            }
        }

        public static void SaveMove(Skill selSkill)
        {
            _selSkill = selSkill;
            isSelecting = true;
        }

        public static void CancelAttack()
        {
            isSelecting = false;
            _selSkill = null;
        }

        public static void getTile()
        {
            float? minDistance = float.MaxValue;
            bool mouseOverSomething = false;

            Hex hoverTile = null;
            MouseState mouseState = Mouse.GetState();
            Vector2 mouseLocation = new Vector2(mouseState.X, mouseState.Y);
            

            for (int i = 0; i < Fighthandler._fightBoard.GetLength(0); i++) //berechnet ob die Maus über einem Tile steht, sowie dieses Tile
            {
                for (int k = 0; k < Fighthandler._fightBoard.GetLength(1); k++)
                {
                    if (Fighthandler._fightBoard[i, k] != null)
                    {
                        float? distance = HexMap.Intersects(mouseLocation, Fighthandler._fightBoard[i, k].Tile.Tile1, Matrix.CreateScale(0.001f, 0.001f, 0.001f) * Matrix.CreateRotationY(45) * Matrix.CreateTranslation(Fighthandler._fightBoard[i, k].Position), HexMap.Camera.view, HexMap.Camera.projection, AttackMenu.graphicDevice.Viewport);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            hoverTile = Fighthandler._fightBoard[i, k];
                            mouseOverSomething = true;
                        }
                    }
                }
            }

            if (mouseOverSomething && Fighthandler._fightBoard[hoverTile.LogicalPosition.X, hoverTile.LogicalPosition.Y] != null)
            {
                Fighthandler._fightBoard[hoverTile.LogicalPosition.X, hoverTile.LogicalPosition.Y].Tile.Glow = new Vector3(0.3f, 0.3f, 0.3f);
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
                {
                    clickedTile = hoverTile;
                }
            }
            _prevMouseState = mouseState;
        }

        public static void MakeMove(Skill selSkill, Hex clickedTile)
        {
            Charakter boi = Fighthandler.turnBar.ReturnCurrentCharakter();
            for(int i = 1; i < Fighthandler.turnBar.NextTurn.Count; i++)
            {
                Fighthandler.turnBar.NextTurn[i].Geschwindigkeit -= boi.Geschwindigkeit;
            }
            double basedmg = Fighthandler.GetBaseDmg(boi, boi.Weapon);

            Random rnd = new Random();
            int krit = rnd.Next(100);
            if (krit < boi.CurrentFightStats[8] + boi.Weapon.BaseKrit)
            {
                basedmg = basedmg * 1.5f;
            }
            double armor = (20.0 / (20 + Fighthandler._fightBoard[clickedTile.LogicalFightPosition.X, clickedTile.LogicalFightPosition.Y].Charakter.CurrentFightStats[3]));
            Fighthandler._fightBoard[clickedTile.LogicalFightPosition.X, clickedTile.LogicalFightPosition.Y].Charakter.CurrentFightStats[0] -= (int)((basedmg * selSkill.MoveValue) * armor);
            boi.CurrentFightStats[7] = (int)(selSkill.Geschwindigkeit - Math.Pow(0.95, (boi.Geschwindigkeit - selSkill.Geschwindigkeit)));
            if (clickedTile.Charakter.CurrentFightStats[0] <= 0)
            {
                int gotXP = boi.GainXp(boi, clickedTile.Charakter);
                Random rand = new Random();
                int zahl = rand.Next(100);
                if (zahl < boi.CurrentFightStats[8] + 15)
                {
                    boi.WeaponInv.Add(clickedTile.Charakter.Weapon);
                }

                if (Fighthandler.fightResults.EarnedXP.ContainsKey(boi.Name))
                {
                    Fighthandler.fightResults.EarnedXP[boi.Name] =+ gotXP;
                }
                else
                {
                    Fighthandler.fightResults.EarnedXP.Add(boi.Name, gotXP);
                }
            }
            isSelecting = false;

        }
    }
}
