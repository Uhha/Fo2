using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fo2
{
    public static class HelperFuncts
    {
        public static int TileNumX = 0;
        public static int TileNumY = 0;
        public static int HexNumX = 0;
        public static int HexNumYfast = 0;
        public static int HexNumY = 0;
        public static int HexAdjX = 0;
        public static int HexAdjY = 0;


        public static Texture2D blankTexture;
        public const int MapZeroCoordinate = 0;



        public static Vector2 NextTilePos() {
            if (TileNumX == 100)
            {
                TileNumX = 0;
                TileNumY++;
            }
            Vector2 ret = new Vector2((MapZeroCoordinate - 80) - TileNumX * 48 + TileNumY * 32, 0 + 12 * TileNumX + TileNumY * 24);
            TileNumX++;
            return ret;
        }

        public static Vector2 NextHexPos()
        {


            if (HexNumX == 200)
            {
                HexNumX = 0;
                HexNumY++;
                HexAdjX = 0;
                HexAdjY = 0;
                HexNumYfast = 0;
            }
            if (HexNumYfast == 2) { HexAdjX += 16; HexAdjY += 12; HexNumYfast = 0; }
            Vector2 ret = new Vector2(((MapZeroCoordinate - 80 + 16 + 32 ) - HexNumX * 32 + 16 * HexNumY) + HexAdjX, (0 + HexNumY * 12) + HexAdjY);
            HexNumYfast++;
            HexNumX++;
            return ret;
        }

        public static void DrawPolygon(SpriteBatch spriteBatch, Vector2[] vertex, int count, Color color, int lineWidth)
        {
            if (count > 0)
            {
                for (int i = 0; i < count - 1; i++)
                {
                    DrawLineSegment(spriteBatch, vertex[i], vertex[i + 1], color, lineWidth);
                }
                DrawLineSegment(spriteBatch, vertex[count - 1], vertex[0], color, lineWidth);
            }
        }


        public static void DrawLineSegment(SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, int lineWidth)
        {

            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            spriteBatch.Draw(blankTexture, point1, null, color,
            angle, Vector2.Zero, new Vector2(length, lineWidth),
            SpriteEffects.None, 0f);
        }
    }
}
