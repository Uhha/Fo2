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
            if(currentHex % 2 == 0 || currentHex == 0)
            {
                switch (direction)
                {
                    case 0: returnHex = currentHex - 1;
                        break;
                    case 1: returnHex = currentHex + 199;
                        break;
                    case 2: returnHex = currentHex + 200;
                        break;
                    case 3: returnHex = currentHex + 201;
                        break;
                    case 4: returnHex = currentHex + 1;
                        break;
                    case 5: returnHex = currentHex - 200;
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

        public static int[] ShortestPath(int start, int end)
        {
            LinkedList<int> pq = new LinkedList<int>();
            pq.AddFirst(start);

            bool found = false;
            while (found == false)
            {
                int node = pq.First();
                Hexes[node]._rank = 0;
                if (node == end) {
                    found = true;
                    break;
                }
                pq.RemoveFirst();

                foreach (var n in Hexes[node]._connected)
                {
                    if (Hexes[n]._rank  > Hexes[node]._rank + 1)
                    {
                        Hexes[n]._rank = Hexes[node]._rank++;
                        Hexes[n]._cameFrom = Hexes[node]._actualNum;
                        pq.AddLast(Hexes[n]._actualNum);
                    }
                }
            }
            LinkedList<int> ret = new LinkedList<int>();
            
            var current = end;
            while (current != start)
            {
                ret.AddFirst(current);
                current = Hexes[current]._cameFrom;
            }
            ret.AddFirst(start);
            return ret.ToArray<int>();

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
