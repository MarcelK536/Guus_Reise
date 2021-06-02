using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Guus_Reise.Game1;
namespace Guus_Reise
{
    class Fighthandler
    {
        public static List<Hex> playerTiles = new List<Hex>();
        public static List<Hex> npcTiles = new List<Hex>();

        public static FightMenu fightMenu;

        public static void Init(GraphicsDevice graphicsDevice)
        {
            fightMenu = new FightMenu(Player1.actionMenuFont, graphicsDevice, SimpleMenu.BlendDirection.None);
        }

        public static void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                GState = Game1.GameState.InGame;
                fightMenu.Active = false;
            }
            fightMenu.Active = true;
            fightMenu.Update();

        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Hex tile in playerTiles)
            {
                tile.Draw(HexMap.Camera);
            }
            foreach (Hex tile in npcTiles)
            {
                tile.Draw(HexMap.Camera);
            }
            fightMenu.Draw(spriteBatch);
        }

       /* public static void CalculateMoves(List<Moves> player, List<Moves> npc)
        {

        }
       */
    }
}
