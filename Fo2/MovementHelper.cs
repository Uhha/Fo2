using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fo2
{
    static class MovementHelper
    {
        public static Hex[] Hexes;

        public static int GetAdjecentHex(int direction, int currentHex)
        {
            int returnHex = -1;
            if (currentHex % 2 == 0 || currentHex == 0)
            {
                switch (direction)
                {
                    case 0:
                        returnHex = currentHex - 1;
                        break;
                    case 1:
                        returnHex = currentHex + 199;
                        break;
                    case 2:
                        returnHex = currentHex + 200;
                        break;
                    case 3:
                        returnHex = currentHex + 201;
                        break;
                    case 4:
                        returnHex = currentHex + 1;
                        break;
                    case 5:
                        returnHex = currentHex - 200;
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case 0:
                        returnHex = currentHex - 201;
                        break;
                    case 1:
                        returnHex = currentHex - 1;
                        break;
                    case 2:
                        returnHex = currentHex + 200;
                        break;
                    case 3:
                        returnHex = currentHex + 1;
                        break;
                    case 4:
                        returnHex = currentHex - 199;
                        break;
                    case 5:
                        returnHex = currentHex - 200;
                        break;
                }
            }
            return returnHex;
        }



            public static int GetAdjecentDirection(int currentHex, int nextHex)
        {
            int returnDirection = -1;
            if (currentHex % 2 == 0 || currentHex == 0)
            {
                switch (nextHex - currentHex)
                {
                    case -1:
                        returnDirection = 0;
                        break;
                    case 199:
                        returnDirection = 1;
                        break;
                    case 200:
                        returnDirection = 2;
                        break;
                    case 201:
                        returnDirection = 3;
                        break;
                    case 1:
                        returnDirection = 4;
                        break;
                    case -200:
                        returnDirection = 5;
                        break;
                }
            }
            else
            {
                switch (nextHex - currentHex)
                {
                    case -201:
                        returnDirection = 0;
                        break;
                    case -1:
                        returnDirection = 1;
                        break;
                    case 200:
                        returnDirection = 2;
                        break;
                    case 1:
                        returnDirection = 3;
                        break;
                    case -199:
                        returnDirection = 4;
                        break;
                    case -200:
                        returnDirection = 5;
                        break;
                }
            }

            return returnDirection;
        }

        public static int[] ShortestPath(int start, int end)
        {
            LinkedList<int> pq = new LinkedList<int>();
            LinkedList<int> vertexesToReset = new LinkedList<int>();
            pq.AddFirst(start);
            vertexesToReset.AddFirst(start);
            Hexes[pq.First()]._rank = 0;
            bool found = false;
            while (found == false)
            {
                int node = pq.First();
                if (node == end) {
                    found = true;
                    break;
                }
                pq.RemoveFirst();
                

                foreach (var n in Hexes[node]._connected)
                {
                    if (Hexes[n]._rank  > Hexes[node]._rank + 1)
                    {
                        if (Microsoft.Xna.Framework.Vector2.DistanceSquared(Hexes[n].Position, Hexes[end].Position) < 
                            Microsoft.Xna.Framework.Vector2.DistanceSquared(Hexes[node].Position, Hexes[end].Position))
                        {
                            Hexes[n]._rank = Hexes[node]._rank + 1;
                        }
                        else
                        {
                            Hexes[n]._rank = Hexes[node]._rank + 2;
                        }
                        Hexes[n]._cameFrom = Hexes[node]._actualNum;
                        if (!pq.Contains(Hexes[n]._actualNum))
                        {
                            pq.AddLast(Hexes[n]._actualNum);
                            vertexesToReset.AddFirst(Hexes[n]._actualNum);
                        }
                    }
                }
            }
            LinkedList<int> retNodes = new LinkedList<int>();
            
            var current = end;
            while (current != start)
            {
                retNodes.AddFirst(current);
                current = Hexes[current]._cameFrom;
            }
            retNodes.AddFirst(start);

            int[] ret = new int[retNodes.Count - 1];
            for (int i = 1; i < retNodes.Count; i++)
            {
                ret[i - 1] = MovementHelper.GetAdjecentDirection(retNodes.ElementAt(i-1), retNodes.ElementAt(i));
            }

            foreach (var item in vertexesToReset)
            {
                Hexes[item]._cameFrom = 0;
                Hexes[item]._rank = Int16.MaxValue;
            }

            return ret;

        }

        public static int Inside(IList<Vector2> vertices, Vector2 position, int actualHexNum, bool toleranceOnOutside = true, bool hole = false)
        {
            Vector2 point = position;

            const float epsilon = 0.5f;

            bool inside = false;

            // Must have 3 or more edges
            if (vertices.Count < 3) return -1;

            Vector2 oldPoint = vertices[vertices.Count - 1];
            float oldSqDist = Vector2.DistanceSquared(oldPoint, point);

            for (int i = 0; i < vertices.Count; i++)
            {
                Vector2 newPoint = vertices[i];
                float newSqDist = Vector2.DistanceSquared(newPoint, point);

                if (oldSqDist + newSqDist + 2.0f * System.Math.Sqrt(oldSqDist * newSqDist) - Vector2.DistanceSquared(newPoint, oldPoint) < epsilon)
                    return actualHexNum; //return (hole) ? !toleranceOnOutside : toleranceOnOutside;

                Vector2 left;
                Vector2 right;
                if (newPoint.X > oldPoint.X)
                {
                    left = oldPoint;
                    right = newPoint;
                }
                else
                {
                    left = newPoint;
                    right = oldPoint;
                }

                if (left.X < point.X && point.X <= right.X && (point.Y - left.Y) * (right.X - left.X) < (right.Y - left.Y) * (point.X - left.X))
                    inside = !inside;

                oldPoint = newPoint;
                oldSqDist = newSqDist;
            }

            
            return (inside) ? actualHexNum : -1;
        }

        public static int HexX(int hexNum)
        {
            return (int)Hexes[hexNum]._vertexes[0].X;
        }

        public static int HexY(int hexNum)
        {
            return (int)Hexes[hexNum]._vertexes[0].Y;
        }
    }
}
