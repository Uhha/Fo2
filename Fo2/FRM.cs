using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fo2
{
    class FRM
    {
        public FrameDirecion[] _directions;
        private int _numberOfDirections = 0;
        public Direction CurrentDirection = Direction.N;
        private byte[] _bytes;
        public int _posX;
        public int _posY;


        public FRM(int posX, int posY)
        {
            _bytes = File.ReadAllBytes("C:/HANPWRMJ.FRM");
            //_bytes = File.ReadAllBytes(@"C:\!tmp\f2\data\art\scenery\TEMPLE04.FRM");
            _posX = posX;
            _posY = posY;

            _directions = new FrameDirecion[6];
            int numberOfFrames = HelperFuncts.SumTwoBytes(_bytes[8], _bytes[9]);

            Vector2[] dirOffsets = new Vector2[6];
            int v = 10;
            int w = 22;
            for (int i = 0; i < 6; i++, v++, w++)
            {
                dirOffsets[i] = new Vector2(HelperFuncts.SumTwoBytes(_bytes[v], _bytes[++v]), HelperFuncts.SumTwoBytes(_bytes[w], _bytes[++w]));
            }



            int frameAreaSize = HelperFuncts.SumTwoBytes(_bytes[58], _bytes[59], _bytes[60], _bytes[61]);
            int currentBytesPosition = 62; //3E

            InitializeDirections(numberOfFrames);

            int HeightMAX = 0;
            int HeightSpr = 0;
            int WidthSpr = 0;
            int WidthSprMAX = 0;


            for (int dir = 0; dir < _numberOfDirections; dir++)
            {
                int directionOffset = _directions[dir]._directionOffset;

                _directions[dir]._directionOffsetX = (int) dirOffsets[dir].X; 
                _directions[dir]._directionOffsetY = (int) dirOffsets[dir].Y;


                //int[] ofs = {-1, 2,-1,0,0,0,1,-1};
                int[] ofs = { 0, -1, +1, 0, 0, 0, 0, +1 };
                for (int frame = 0; frame < numberOfFrames; frame++)
                {

                    var width = HelperFuncts.SumTwoBytes(_bytes[currentBytesPosition + directionOffset], _bytes[currentBytesPosition + directionOffset + 1]);
                    WidthSpr += width;
                    var height = HelperFuncts.SumTwoBytes(_bytes[currentBytesPosition + directionOffset + 2], _bytes[currentBytesPosition + directionOffset + 3]);
                    HeightMAX = Math.Max(HeightMAX, height);

                    var PixelDataSize = HelperFuncts.SumTwoBytes(_bytes[currentBytesPosition + directionOffset + 4], _bytes[currentBytesPosition + directionOffset + 5], _bytes[currentBytesPosition + directionOffset + 6], _bytes[currentBytesPosition + directionOffset + 7]);
                    var offsetX = HelperFuncts.SumTwoBytes(_bytes[currentBytesPosition + directionOffset + 8], _bytes[currentBytesPosition + directionOffset + 9]);
                    var offsetY = HelperFuncts.SumTwoBytes(_bytes[currentBytesPosition + directionOffset + 10], _bytes[currentBytesPosition + directionOffset + 11]);

                    _directions[dir].AddFrame(frame,  width, height, ofs[frame], offsetY, WidthSpr, HeightSpr, _bytes, PixelDataSize, currentBytesPosition + directionOffset + 12);


                    directionOffset += (12 + PixelDataSize);
                }

                HeightSpr += HeightMAX;
                WidthSprMAX = Math.Max(WidthSprMAX, WidthSpr);
                WidthSpr = 0;
                HeightMAX = 0;

            }
        }

        private void InitializeDirections(int numberOfFrames)
        {
            int directionOffset = -1;
            _directions[0] = new FrameDirecion(numberOfFrames, 0, _posX, _posY);
            _numberOfDirections++;
            int bytePos = 38;
            for (int i = 1; i < 6; i++)
            {
                directionOffset = HelperFuncts.SumTwoBytes(_bytes[bytePos], _bytes[++bytePos], _bytes[++bytePos], _bytes[++bytePos]);
                if (directionOffset > 0)
                {
                    _directions[i] = new FrameDirecion(numberOfFrames, directionOffset, _posX, _posY);
                    _numberOfDirections++;
                }
                bytePos++;
            }
        }

        public void Update(double gameTime)
        {
            _directions[(int)CurrentDirection].Update(gameTime);
        }


        public void Draw(SpriteBatch sb)
        {
            _directions[(int)CurrentDirection].Draw(sb);
        }



    }
}
