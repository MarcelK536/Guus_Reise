using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Guus_Reise.Animation;

namespace Guus_Reise
{
    class KI
    {
        
        public static void Update(GameTime time, GraphicsDevice graphicsDevice)
        {

            List<Hex> npcOldHex = new List<Hex>();
            List<Hex> npcNewHex = new List<Hex>();
            List<Charakter> npcs = new List<Charakter>();
            bool makeAnimation = false;
            foreach (Charakter charakter in HexMap.npcs)
            {
                Point near;
                List<Hex> neighbours = new List<Hex>();
                List<Point> neighbourpoints = new List<Point>();
                List<Node> path = new List<Node>();
                Point move;
                float distance;
                switch (charakter.KI)
                {
                    case 1:         //steht still
                        charakter.CanMove = false;
                        break;
                    case 2:         //rennt von anfang an auf den nächsten Spieler zu
                        npcs.Add(charakter);
                        makeAnimation = true;
                        npcOldHex.Add(HexMap._board[charakter.LogicalPosition.X, charakter.LogicalPosition.Y]);
                        near = NearestPlayerCharacter(charakter);
                        neighbours = HexMap.GetNeighbourTiles(HexMap._board[near.X, near.Y]);
                        neighbours.RemoveAll(e => e.Charakter != null);
                        for(int i = 0; i< neighbours.Count; i++)
                        {
                            neighbourpoints.Add(neighbours[i].LogicalPosition);
                        }
                        path = FindPath(neighbourpoints, charakter.LogicalPosition);
                        path.Reverse();
                        HexMap.CalculatePossibleMoves(charakter.LogicalPosition.X, charakter.LogicalPosition.Y, charakter.Bewegungsreichweite, HexMap._board[charakter.LogicalPosition.X, charakter.LogicalPosition.Y]);
                        move = charakter.LogicalPosition;
                        for(int i = 0; i < path.Count; i++)
                        {
                            if (HexMap.possibleMoves.Contains(path[i].Position))
                            {
                                move = path[i].Position;
                            }
                        }
                        HexMap._board[charakter.LogicalPosition.X, charakter.LogicalPosition.Y].Charakter = null;
                        HexMap._board[move.X, move.Y].Charakter = charakter;
                        npcNewHex.Add(HexMap._board[move.X, move.Y]);
                        HexMap._board[move.X, move.Y].Charakter.CharakterAnimation.Hexagon = HexMap._board[move.X, move.Y];
                        HexMap._board[move.X, move.Y].Charakter.LogicalPosition = charakter.LogicalPosition;
                        charakter.LogicalBoardPosition = move;
                        charakter.CanMove = false;
                        break;
                    case 3: //rennt auf Spieler zu sobald er sich in gewisser Reichweite befindet
                        npcs.Add(charakter);
                        makeAnimation = true;
                        npcOldHex.Add(HexMap._board[charakter.LogicalPosition.X, charakter.LogicalPosition.Y]);
                        near = NearestPlayerCharacter(charakter);
                        distance = DistanceToNearestPlayer(charakter, near);
                        if (distance < charakter.Bewegungsreichweite)
                        {
                            neighbours = HexMap.GetNeighbourTiles(HexMap._board[near.X, near.Y]);
                            neighbours.RemoveAll(e => e.Charakter != null);
                            for (int i = 0; i < neighbours.Count; i++)
                            {
                                neighbourpoints.Add(neighbours[i].LogicalPosition);
                            }
                            path = FindPath(neighbourpoints, charakter.LogicalPosition);
                            path.Reverse();
                            HexMap.CalculatePossibleMoves(charakter.LogicalPosition.X, charakter.LogicalPosition.Y, charakter.Bewegungsreichweite, HexMap._board[charakter.LogicalPosition.X, charakter.LogicalPosition.Y]);
                            move = charakter.LogicalPosition;
                            for (int i = 0; i < path.Count; i++)
                            {
                                if (HexMap.possibleMoves.Contains(path[i].Position))
                                {
                                    move = path[i].Position;
                                }
                            }
                            HexMap._board[charakter.LogicalPosition.X, charakter.LogicalPosition.Y].Charakter = null;
                            HexMap._board[move.X, move.Y].Charakter = charakter;
                            npcNewHex.Add(HexMap._board[move.X, move.Y]);
                            HexMap._board[move.X, move.Y].Charakter.CharakterAnimation.Hexagon = HexMap._board[move.X, move.Y];
                            HexMap._board[move.X, move.Y].Charakter.LogicalPosition = charakter.LogicalPosition;
                            charakter.LogicalPosition = move;
                        }
                        charakter.CanMove = false;
                        break;
                    case 4: //patroulliert bis der Spieler sich in gewisser Reichweite befindet
                        npcs.Add(charakter);
                        makeAnimation = true;
                        npcOldHex.Add(HexMap._board[charakter.LogicalPosition.X, charakter.LogicalPosition.Y]);
                        near = NearestPlayerCharacter(charakter);
                        distance = DistanceToNearestPlayer(charakter, near);
                        if (distance < charakter.Bewegungsreichweite)
                        {
                            neighbours = HexMap.GetNeighbourTiles(HexMap._board[near.X, near.Y]);
                            neighbours.RemoveAll(e => e.Charakter != null);
                            for (int i = 0; i < neighbours.Count; i++)
                            {
                                neighbourpoints.Add(neighbours[i].LogicalPosition);
                            }
                            path = FindPath(neighbourpoints, charakter.LogicalPosition);
                            path.Reverse();
                            HexMap.CalculatePossibleMoves(charakter.LogicalPosition.X, charakter.LogicalPosition.Y, charakter.Bewegungsreichweite, HexMap._board[charakter.LogicalPosition.X, charakter.LogicalPosition.Y]);
                            move = charakter.LogicalPosition;
                            for (int i = 0; i < path.Count; i++)
                            {
                                if (HexMap.possibleMoves.Contains(path[i].Position))
                                {
                                    move = path[i].Position;
                                }
                            }
                            HexMap._board[charakter.LogicalPosition.X, charakter.LogicalPosition.Y].Charakter = null;
                            HexMap._board[move.X, move.Y].Charakter = charakter;
                            npcNewHex.Add(HexMap._board[move.X, move.Y]);
                            HexMap._board[move.X, move.Y].Charakter.CharakterAnimation.Hexagon = HexMap._board[move.X, move.Y];
                            HexMap._board[move.X, move.Y].Charakter.LogicalPosition = charakter.LogicalPosition;
                            charakter.LogicalPosition = move;
                        }
                        else
                        {
                            if (charakter.LogicalPosition == charakter.Patroullienpunkte[0])
                            {
                                charakter.Patroullienpunkte.Add(charakter.Patroullienpunkte[0]);
                                charakter.Patroullienpunkte.RemoveAt(0);
                            }
                            neighbourpoints.Add(charakter.Patroullienpunkte[0]);                           
                            path = FindPath(neighbourpoints, charakter.LogicalPosition);
                            path.Reverse();
                            HexMap.CalculatePossibleMoves(charakter.LogicalPosition.X, charakter.LogicalPosition.Y, charakter.Bewegungsreichweite, HexMap._board[charakter.LogicalPosition.X, charakter.LogicalPosition.Y]);
                            move = charakter.LogicalPosition;
                            for (int i = 0; i < path.Count; i++)
                            {
                                if (HexMap.possibleMoves.Contains(path[i].Position))
                                {
                                    move = path[i].Position;
                                }
                            }
                            HexMap._board[charakter.LogicalPosition.X, charakter.LogicalPosition.Y].Charakter = null;
                            HexMap._board[move.X, move.Y].Charakter = charakter;
                            HexMap._board[move.X, move.Y].Charakter.CharakterAnimation.Hexagon = HexMap._board[move.X, move.Y];
                            HexMap._board[move.X, move.Y].Charakter.LogicalPosition = charakter.LogicalPosition;
                            charakter.LogicalPosition = move;
                            npcNewHex.Add(HexMap._board[move.X, move.Y]);
                        }
                        charakter.CanMove = false;
                            break;
                    default: charakter.CanMove = false;
                        break;
                }

            }
            if (makeAnimation == true)
            {
                HexMap.NoGlow();
                MovementAnimationManager.InitNPCMovement("NPCMovement", npcOldHex, npcNewHex, npcs);
            }
        }
        private static float DistanceToNearestPlayer(Charakter charakter, Point near)
        {
            Vector3 hilf1 = new Vector3(charakter.LogicalPosition.X - (charakter.LogicalPosition.Y - (charakter.LogicalPosition.Y & 1)) / 2, -(charakter.LogicalPosition.X - (charakter.LogicalPosition.Y - (charakter.LogicalPosition.Y & 1)) / 2) - charakter.LogicalPosition.Y, charakter.LogicalPosition.Y);
            Vector3 hilf2 = new Vector3(near.X - (near.Y - (near.Y & 1)) / 2, -(near.X - (near.Y - (near.Y & 1)) / 2) - near.Y, near.Y);
            float hilf3 = Math.Max(Math.Abs(hilf1.X - hilf2.X), Math.Abs(hilf1.Y - hilf2.Y));
            hilf3 = Math.Max(hilf3, Math.Abs(hilf1.Z - hilf2.Z));
            return hilf3;
        }
        private static Point NearestPlayerCharacter(Charakter character)
        {
            Vector3 hilf = HexMap._board[character.LogicalPosition.X, character.LogicalPosition.Y].Position;
            Point nearest = character.LogicalPosition;
            float bestdistance = float.MaxValue;
            foreach(Charakter player in HexMap.playableCharacter)
            {
                Vector3 hilf2 = HexMap._board[player.LogicalPosition.X, player.LogicalPosition.Y].Position;               
                if((hilf-hilf2).Length() < bestdistance)
                {
                    bestdistance = (hilf - hilf2).Length();
                    nearest = player.LogicalPosition;
                }
            }
            return nearest;
        }

        private static List<Node> FindPath(List<Point> ziel, Point start)
        {
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();
            List<Node> path = new List<Node>();
            Node first = new Node(start);
            first.Bestmove = 0;
            openList.Add(first);
            while (openList.Count > 0)
            {
                Node currentNode = openList[0];
                openList.RemoveAt(0);
                int x = currentNode.Position.X;
                int y = currentNode.Position.Y;
                if(ziel.Contains(currentNode.Position))
                {
                    path.Add(currentNode);
                    while(currentNode.Prev != null)
                    {                       
                        currentNode = currentNode.Prev;
                        path.Add(currentNode);
                    }
                    return path;
                }
                if (x - 1 >= 0 && HexMap._board[x - 1, y].Charakter == null)
                {
                    if(!closedList.Exists(e => e.Position == new Point(x-1, y)))
                    {
                        if (openList.Exists(e => e.Position == new Point(x - 1, y)))
                        {
                            if (currentNode.Bestmove + HexMap._board[x - 1, y].Tile.Begehbarkeit < openList.Find(e => e.Position == new Point(x - 1, y)).Bestmove)
                            {
                                openList.Find(e => e.Position == new Point(x - 1, y)).Prev = currentNode;
                                openList.Find(e => e.Position == new Point(x - 1, y)).Bestmove = currentNode.Bestmove + HexMap._board[x - 1, y].Tile.Begehbarkeit;
                            }                           
                        }
                        else
                        {
                            Node hilf = new Node(new Point(x - 1, y));
                            hilf.Prev = currentNode;
                            hilf.Bestmove = currentNode.Bestmove + HexMap._board[x - 1, y].Tile.Begehbarkeit;
                            openList.Add(hilf);
                        }
                    }                   
                }

                if (x + 1 < HexMap._board.GetLength(0) && HexMap._board[x + 1, y].Charakter == null)
                {
                    if (!closedList.Exists(e => e.Position == new Point(x + 1, y)))
                    {
                        if (openList.Exists(e => e.Position == new Point(x + 1, y)))
                        {
                            if (currentNode.Bestmove + HexMap._board[x + 1, y].Tile.Begehbarkeit < openList.Find(e => e.Position == new Point(x + 1, y)).Bestmove)
                            {
                                openList.Find(e => e.Position == new Point(x + 1, y)).Prev = currentNode;
                                openList.Find(e => e.Position == new Point(x + 1, y)).Bestmove = currentNode.Bestmove + HexMap._board[x + 1, y].Tile.Begehbarkeit;
                            }
                        }
                        else
                        {
                            Node hilf = new Node(new Point(x + 1, y));
                            hilf.Prev = currentNode;
                            hilf.Bestmove = currentNode.Bestmove + HexMap._board[x + 1, y].Tile.Begehbarkeit;
                            openList.Add(hilf);
                        }
                    }
                }

                if (y - 1 >= 0 && HexMap._board[x, y - 1].Charakter == null)
                {
                    if (!closedList.Exists(e => e.Position == new Point(x, y-1)))
                    {
                        if (openList.Exists(e => e.Position == new Point(x, y-1)))
                        {
                            if (currentNode.Bestmove + HexMap._board[x, y-1].Tile.Begehbarkeit < openList.Find(e => e.Position == new Point(x, y-1)).Bestmove)
                            {
                                openList.Find(e => e.Position == new Point(x, y-1)).Prev = currentNode;
                                openList.Find(e => e.Position == new Point(x, y-1)).Bestmove = currentNode.Bestmove + HexMap._board[x, y-1].Tile.Begehbarkeit;
                            }
                        }
                        else
                        {
                            Node hilf = new Node(new Point(x, y-1));
                            hilf.Prev = currentNode;
                            hilf.Bestmove = currentNode.Bestmove + HexMap._board[x, y-1].Tile.Begehbarkeit;
                            openList.Add(hilf);
                        }
                    }
                }

                if (y + 1 < HexMap._board.GetLength(1) && HexMap._board[x, y + 1].Charakter == null)
                {
                    if (!closedList.Exists(e => e.Position == new Point(x, y + 1)))
                    {
                        if (openList.Exists(e => e.Position == new Point(x, y + 1)))
                        {
                            if (currentNode.Bestmove + HexMap._board[x, y + 1].Tile.Begehbarkeit < openList.Find(e => e.Position == new Point(x, y + 1)).Bestmove)
                            {
                                openList.Find(e => e.Position == new Point(x, y + 1)).Prev = currentNode;
                                openList.Find(e => e.Position == new Point(x, y + 1)).Bestmove = currentNode.Bestmove + HexMap._board[x, y + 1].Tile.Begehbarkeit;
                            }
                        }
                        else
                        {
                            Node hilf = new Node(new Point(x, y + 1));
                            hilf.Prev = currentNode;
                            hilf.Bestmove = currentNode.Bestmove + HexMap._board[x, y + 1].Tile.Begehbarkeit;
                            openList.Add(hilf);
                        }
                    }
                }

                if (y % 2 == 0)
                {
                    if (x - 1 >= 0 && y - 1 >= 0 && HexMap._board[x - 1, y - 1].Charakter == null)
                    {
                        if (!closedList.Exists(e => e.Position == new Point(x - 1, y - 1)))
                        {
                            if (openList.Exists(e => e.Position == new Point(x - 1, y - 1)))
                            {
                                if (currentNode.Bestmove + HexMap._board[x - 1, y - 1].Tile.Begehbarkeit < openList.Find(e => e.Position == new Point(x - 1, y - 1)).Bestmove)
                                {
                                    openList.Find(e => e.Position == new Point(x - 1, y - 1)).Prev = currentNode;
                                    openList.Find(e => e.Position == new Point(x - 1, y - 1)).Bestmove = currentNode.Bestmove + HexMap._board[x - 1, y - 1].Tile.Begehbarkeit;
                                }
                            }
                            else
                            {
                                Node hilf = new Node(new Point(x - 1, y - 1));
                                hilf.Prev = currentNode;
                                hilf.Bestmove = currentNode.Bestmove + HexMap._board[x - 1, y - 1].Tile.Begehbarkeit;
                                openList.Add(hilf);
                            }
                        }
                    }

                    if (x - 1 >= 0 && y + 1 < HexMap._board.GetLength(1) && HexMap._board[x - 1, y + 1].Charakter == null)
                    {
                        if (!closedList.Exists(e => e.Position == new Point(x - 1, y + 1)))
                        {
                            if (openList.Exists(e => e.Position == new Point(x - 1, y + 1)))
                            {
                                if (currentNode.Bestmove + HexMap._board[x - 1, y + 1].Tile.Begehbarkeit < openList.Find(e => e.Position == new Point(x - 1, y + 1)).Bestmove)
                                {
                                    openList.Find(e => e.Position == new Point(x - 1, y + 1)).Prev = currentNode;
                                    openList.Find(e => e.Position == new Point(x - 1, y + 1)).Bestmove = currentNode.Bestmove + HexMap._board[x - 1, y + 1].Tile.Begehbarkeit;
                                }
                            }
                            else
                            {
                                Node hilf = new Node(new Point(x - 1, y + 1));
                                hilf.Prev = currentNode;
                                hilf.Bestmove = currentNode.Bestmove + HexMap._board[x - 1, y + 1].Tile.Begehbarkeit;
                                openList.Add(hilf);
                            }
                        }
                    }
                }
                else
                {
                    if (x + 1 < HexMap._board.GetLength(0) && y - 1 >= 0 && HexMap._board[x + 1, y - 1].Charakter == null)
                    {
                        if (!closedList.Exists(e => e.Position == new Point(x+1, y - 1)))
                        {
                            if (openList.Exists(e => e.Position == new Point(x+1, y - 1)))
                            {
                                if (currentNode.Bestmove + HexMap._board[x+1, y - 1].Tile.Begehbarkeit < openList.Find(e => e.Position == new Point(x+1, y - 1)).Bestmove)
                                {
                                    openList.Find(e => e.Position == new Point(x+1, y - 1)).Prev = currentNode;
                                    openList.Find(e => e.Position == new Point(x+1, y - 1)).Bestmove = currentNode.Bestmove + HexMap._board[x+1, y - 1].Tile.Begehbarkeit;
                                }
                            }
                            else
                            {
                                Node hilf = new Node(new Point(x+1, y - 1));
                                hilf.Prev = currentNode;
                                hilf.Bestmove = currentNode.Bestmove + HexMap._board[x+1, y - 1].Tile.Begehbarkeit;
                                openList.Add(hilf);
                            }
                        }
                    }

                    if (x + 1 < HexMap._board.GetLength(0) && y + 1 < HexMap._board.GetLength(1) && HexMap._board[x + 1, y + 1].Charakter == null)
                    {
                        if (!closedList.Exists(e => e.Position == new Point(x + 1, y + 1)))
                        {
                            if (openList.Exists(e => e.Position == new Point(x + 1, y + 1)))
                            {
                                if (currentNode.Bestmove + HexMap._board[x + 1, y + 1].Tile.Begehbarkeit < openList.Find(e => e.Position == new Point(x + 1, y + 1)).Bestmove)
                                {
                                    openList.Find(e => e.Position == new Point(x + 1, y + 1)).Prev = currentNode;
                                    openList.Find(e => e.Position == new Point(x + 1, y + 1)).Bestmove = currentNode.Bestmove + HexMap._board[x + 1, y + 1].Tile.Begehbarkeit;
                                }
                            }
                            else
                            {
                                Node hilf = new Node(new Point(x + 1, y + 1));
                                hilf.Prev = currentNode;
                                hilf.Bestmove = currentNode.Bestmove + HexMap._board[x + 1, y + 1].Tile.Begehbarkeit;
                                openList.Add(hilf);
                            }
                        }
                    }
                }
                closedList.Add(currentNode);
            }
            path.Add(new Node(start));
            return path;
        }
    } 
    
    class Node
    {
        private Node _prev;
        private float _bestmove;
        private Point _position;

        public Node(Point position)
        {
            this.Position = position;
        }
        public Node Prev
        {
            get => _prev;
            set => _prev = value;
        }
        public float Bestmove
        {
            get => _bestmove;
            set => _bestmove = value;
        }
        public Point Position
        {
            get => _position;
            set => _position = value;
        }
    }
}
