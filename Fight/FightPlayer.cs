using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Guus_Reise
{
    class FightPlayer
    {
        public static void MakeMove(Skill selSkill)
        {
            float? minDistance = float.MaxValue;
            bool mouseOverSomething = false;

            Hex hoverTile = Fighthandler.npcTiles[0];

            MouseState mouseState = Mouse.GetState();
            Vector2 mouseLocation = new Vector2(mouseState.X, mouseState.Y);
            MouseState _prevMouseState = Mouse.GetState();

            while (!(mouseState.LeftButton == ButtonState.Pressed && mouseOverSomething && _prevMouseState.LeftButton == ButtonState.Released && !hoverTile.Charakter.IsNPC))
            {
                for (int i = 0; i < Fighthandler._fightBoard.GetLength(0); i++) //berechnet ob die Maus über einem Tile steht, sowie dieses Tile
                {
                    for (int k = 0; k < Fighthandler._fightBoard.GetLength(1); k++)
                    {
                        if (Fighthandler._fightBoard[i, k] != null) {
                            float? distance = HexMap.Intersects(mouseLocation, Fighthandler._fightBoard[i, k].Tile.Tile1, Matrix.CreateScale(0.001f, 0.001f, 0.001f) * Matrix.CreateRotationY(45) * Matrix.CreateTranslation(Fighthandler._fightBoard[i,k].Position), HexMap.Camera.view, HexMap.Camera.projection, AttackMenu.graphicDevice.Viewport);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                hoverTile = Fighthandler._fightBoard[i, k];
                                mouseOverSomething = true;
                            }
                        }
                    }
                }

                System.Diagnostics.Debug.WriteLine(Mouse.GetState());

                _prevMouseState = mouseState;
                mouseState = Mouse.GetState();
                mouseLocation = new Vector2(mouseState.X, mouseState.Y);
            }

            Charakter boi = Fighthandler.turnBar.ReturnCurrentCharakter();
            double basedmg = Fighthandler.GetBaseDmg(boi, boi.Weapon);
            Fighthandler._fightBoard[hoverTile.LogicalFightPosition.X, hoverTile.LogicalFightPosition.Y].Charakter.CurrentFightStats[0] -= (int)(basedmg * selSkill.MoveValue) - Fighthandler._fightBoard[hoverTile.LogicalFightPosition.X, hoverTile.LogicalFightPosition.Y].Charakter.CurrentFightStats[3];
            boi.CurrentFightStats[7] = (int)(selSkill.Geschwindigkeit - Math.Pow(0.95, (boi.Geschwindigkeit - selSkill.Geschwindigkeit)));
        }
    }
}
