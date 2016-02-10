using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fo2
{
    static class MovementHelper
    {
        public static Hex[] Hexes;

        public static int GetNextHex(int direction, int currentHex)
        {
            int returnHex = -1;
            if(currentHex % 2 == 0)
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
