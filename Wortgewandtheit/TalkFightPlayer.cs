using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Guus_Reise
{
    class TalkFightPlayer
    {
        public static bool isSelecting = false;
        public static Skill _selSkill = null;
        public static Hex clickedTile = null;
        public static MouseState _prevMouseState;

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
            

            for (int i = 0; i < TalkFighthandler._fightBoard.GetLength(0); i++) //berechnet ob die Maus über einem Tile steht, sowie dieses Tile
            {
                for (int k = 0; k < TalkFighthandler._fightBoard.GetLength(1); k++)
                {
                    if (TalkFighthandler._fightBoard[i, k] != null)
                    {
                        float? distance = HexMap.Intersects(mouseLocation, TalkFighthandler._fightBoard[i, k].Tile.Tile1, Matrix.CreateScale(0.001f, 0.001f, 0.001f) * Matrix.CreateRotationY(45) * Matrix.CreateTranslation(TalkFighthandler._fightBoard[i, k].Position), HexMap.Camera.view, HexMap.Camera.projection, AttackMenu.graphicDevice.Viewport);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            hoverTile = TalkFighthandler._fightBoard[i, k];
                            mouseOverSomething = true;
                        }
                    }
                }
            }

            if (mouseOverSomething && TalkFighthandler._fightBoard[hoverTile.LogicalPosition.X, hoverTile.LogicalPosition.Y] != null)
            {
                TalkFighthandler._fightBoard[hoverTile.LogicalPosition.X, hoverTile.LogicalPosition.Y].Tile.Glow = new Vector3(0.3f, 0.3f, 0.3f);
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
                {
                    clickedTile = hoverTile;
                }
            }
            _prevMouseState = mouseState;
        }

        public static void MakeMove(Skill selSkill, Hex clickedTile)
        {
            Charakter boi = TalkFighthandler.turnBar.ReturnCurrentCharakter();
            for(int i = 1; i < TalkFighthandler.turnBar.NextTurn.Count; i++)
            {
                TalkFighthandler.turnBar.NextTurn[i].Geschwindigkeit -= boi.Geschwindigkeit;
            }
            double basedmg = TalkFighthandler.GetBaseDmg(boi, boi.Weapon);
            TalkFighthandler._fightBoard[clickedTile.LogicalFightPosition.X, clickedTile.LogicalFightPosition.Y].Charakter.CurrentFightStats[0] -= (int)(basedmg * selSkill.MoveValue) *(20/20+ TalkFighthandler._fightBoard[clickedTile.LogicalFightPosition.X, clickedTile.LogicalFightPosition.Y].Charakter.CurrentFightStats[3]);
            boi.CurrentFightStats[7] = (int)(selSkill.Geschwindigkeit - Math.Pow(0.95, (boi.Geschwindigkeit - selSkill.Geschwindigkeit)));
            if (clickedTile.Charakter.CurrentFightStats[0] <= 0)
            {
                boi.GainXp(boi, clickedTile.Charakter);
            }
            isSelecting = false;
        }
    }
}
