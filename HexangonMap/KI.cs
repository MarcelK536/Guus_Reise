using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Guus_Reise
{
    class KI
    {
        public static void Update(GameTime time, GraphicsDevice graphicsDevice)
        {
            foreach(Charakter charakter in HexMap.npcs)
            {
                switch (charakter.KI)
                {
                    case 1:
                        charakter.CanMove = false;
                        break;
                    case 2:
                        Point near = NearestPlayerCharacter(charakter);
                        List<Node> path = FindPath(near, charakter.LogicalPosition);
                        path.Reverse();
                        HexMap.CalculatePossibleMoves(charakter.LogicalPosition.X, charakter.LogicalPosition.Y, charakter.Bewegungsreichweite, HexMap._board[charakter.LogicalPosition.X, charakter.LogicalPosition.Y]);
                        Point move = charakter.LogicalPosition;
                        for(int i = 0; i < path.Count; i++)
                        {
                            if (HexMap.possibleMoves.Contains(path[i].Position))
                            {
                                move = path[i].Position;
                            }
                        }
                        HexMap._board[charakter.LogicalPosition.X, charakter.LogicalPosition.Y].Charakter = null;
                        HexMap._board[move.X, move.Y].Charakter = charakter;
                        charakter.LogicalPosition = move;
                        HexMap._board[move.X, move.Y].Charakter.LogicalPosition = charakter.LogicalPosition;                       
                        charakter.CanMove = false;
                        break;
                    case 3:
                        break;
                    default: charakter.CanMove = false;
                        break;
                }
            }
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

        private static List<Node> FindPath(Point ziel, Point start)
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
                if(currentNode.Position == ziel)
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
