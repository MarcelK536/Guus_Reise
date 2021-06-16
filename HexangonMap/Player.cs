using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Guus_Reise.HexangonMap;

namespace Guus_Reise
{
    class Player
    {
        public static MouseState _prevMouseState;
        public static KeyboardState _prevKeyState;

        public static Hex activeTile; //active Tile nach linkem Mousclick
        public static Hex hoverTile; //Tile über welchem der mauszeiger steht
        public static Hex moveTile; //als Zugziel ausgewühltes Tile

        public static MoveMenu actionMenu;
        public static SpriteFont actionMenuFont;

        public static SkillUpMenu levelUpMenu;

        public static void Update(GameTime time, GraphicsDevice graphicsDevice)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keystate = Keyboard.GetState();
            Vector2 mouseLocation = new Vector2(mouseState.X, mouseState.Y); //position der Maus auf dem monitor

            float? minDistance = float.MaxValue;
            bool mouseOverSomething = false;
            HexMap.hoveredHex = null;
            hoverTile = null;
            HexMap.possibleMoves.Clear();

            
         
            if (Keyboard.GetState().IsKeyDown(Keys.H) && _prevKeyState.IsKeyUp(Keys.H))
            {
                SimpleMenu.DeactivateAllOtherMenus(levelUpMenu);
                HexMap.visManager.ManageCharakterViewH(levelUpMenu);
                levelUpMenu.Active = !levelUpMenu.Active;
            }

            HexMap.NoGlow();

            for (int i = 0; i < HexMap._board.GetLength(0); i++) //berechnet ob die Maus über einem Tile steht, sowie dieses Tile
            {
                for (int k = 0; k < HexMap._board.GetLength(1); k++)
                {
                    float? distance = HexMap.Intersects(mouseLocation, HexMap._board[i, k].Tile.Tile1, HexMap._board[i, k].Tile.World, HexMap.Camera.view, HexMap.Camera.projection, graphicsDevice.Viewport);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        hoverTile = HexMap._board[i, k];
                        hoverTile.IsHovered = true;
                        HexMap.hoveredHex = hoverTile;
                        mouseOverSomething = true;
                    }
                    else
                    {
                        HexMap._board[i, k].IsHovered = false;
                    }
                    
                }
            }

            if (activeTile == null)
            {
                if (mouseOverSomething)
                {
                    HexMap._board[hoverTile.LogicalPosition.X, hoverTile.LogicalPosition.Y].Tile.Glow = new Vector3(0.3f, 0.3f, 0.3f);

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released) //wenn zusätzlich die linke Maustaste gedrückt wird, wird das hoverTile zum activeTile
                    {
                        activeTile = hoverTile;
                        activeTile.IsActive = true;
                        HexMap.activeHex = activeTile;
                    }
                }
            }
            else
            {
                HexMap._board[activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y].Tile.Glow = new Vector3(0.5f, 0.5f, 0.5f); //das activeTile wird hervorgehoben
                levelUpMenu.Update();

                if (activeTile.Charakter != null)
                {
                    HexMap._board[activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y].Tile.Color = new Vector3(0, 0, 2);
                    if (activeTile.Charakter.CanMove)
                    {
                        HexMap.CalculatePossibleMoves(activeTile.LogicalPosition.X, activeTile.LogicalPosition.Y, activeTile.Charakter.Bewegungsreichweite, activeTile);
                        HexMap.possibleMoves = HexMap.possibleMoves.Distinct().ToList();      //entfernt alle Duplikate aus der Liste
                    }

                    if (mouseOverSomething)
                    {
                        HexMap._board[hoverTile.LogicalPosition.X, hoverTile.LogicalPosition.Y].Tile.Glow = new Vector3(0.3f, 0.3f, 0.3f);

                        if (Mouse.GetState().LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released && HexMap.possibleMoves.Contains(hoverTile.LogicalPosition)) //wenn ein possibleMove Tile geklickt wird, wird dieses aks Zug vorgemerkt
                        {
                            actionMenu.Active = true;
                            SimpleMenu.DeactivateAllOtherMenus(actionMenu);
                            actionMenu.fightTrue = false;
                            actionMenu.interactTrue = false;
                            moveTile = hoverTile;
                            List<Hex> neighbours = new List<Hex>(HexMap.GetNeighbourTiles(moveTile));
                            HexMap.enemyNeighbourCount = 0;
                            HexMap.friendlyNeighbourCount = 0;
                            HexMap.enemyNeighbourTiles.Clear();
                            HexMap.friendNeighbourTiles.Clear();
                            foreach (Hex tile in neighbours)
                            {
                                if (tile.Charakter != null && tile != activeTile)
                                {
                                    if (tile.Charakter.IsNPC != activeTile.Charakter.IsNPC)
                                    {
                                        HexMap.enemyNeighbourCount++;
                                        HexMap.enemyNeighbourTiles.Add(tile);
                                    }
                                    else
                                    {
                                        HexMap.friendlyNeighbourCount++;
                                        HexMap.friendNeighbourTiles.Add(tile);
                                    }
                                }
                            }
                            if (HexMap.enemyNeighbourCount > 0)
                            {
                                actionMenu.fightTrue = true;
                            }
                            if (HexMap.friendlyNeighbourCount > 0)
                            {
                                actionMenu.interactTrue = true;
                            }
                        }
                    }
                }

                if (Mouse.GetState().RightButton == ButtonState.Pressed && _prevMouseState.RightButton == ButtonState.Released)    //wenn die rechte Maustaste gedrückt wird, wird das activeTile zurückgesetzt
                {
                    activeTile.IsActive = false;
                    activeTile = null;
                    HexMap.activeHex = null;
                    if(HexMap.visManager.isDetailViewH)
                    {
                        HexMap.visManager.SetCameraToMiddleOfMap();
                    }

                    moveTile = null;
                    actionMenu.Active = false;
                    HexMap.enemyNeighbourCount = 0;
                    HexMap.friendlyNeighbourCount = 0;
                    HexMap.enemyNeighbourTiles.Clear();
                    HexMap.friendNeighbourTiles.Clear();
                    actionMenu.fightTrue = false;
                    actionMenu.interactTrue = false;
                }
            }
           

            actionMenu.Update();
            _prevMouseState = mouseState;
            _prevKeyState = keystate;
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            actionMenu.Draw(spriteBatch);
            if (activeTile != null)
            {
                levelUpMenu.Draw(spriteBatch);
            }
        }
    }
}
