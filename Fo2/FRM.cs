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
        private byte[] _bytes;
        


        public FRM()
        {
            _bytes = File.ReadAllBytes("C:/HANPWRMJ.FRM");
            _directions = new FrameDirecion[6];
            int numberOfFrames = HelperFuncts.SumTwoBytes(_bytes[8], _bytes[9]);
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

                int directionOffsetX = _directions[dir]._directionOffsetX;
                int directionOffsetY = _directions[dir]._directionOffsetY;

                for (int frame = 0; frame < numberOfFrames; frame++)
                {

                    var width = HelperFuncts.SumTwoBytes(_bytes[currentBytesPosition + directionOffset], _bytes[currentBytesPosition + directionOffset + 1]);
                    WidthSpr += width;
                    var height = HelperFuncts.SumTwoBytes(_bytes[currentBytesPosition + directionOffset + 2], _bytes[currentBytesPosition + directionOffset + 3]);
                    HeightMAX = Math.Max(HeightMAX, height);

                    var PixelDataSize = HelperFuncts.SumTwoBytes(_bytes[currentBytesPosition + directionOffset + 4], _bytes[currentBytesPosition + directionOffset + 5], _bytes[currentBytesPosition + directionOffset + 6], _bytes[currentBytesPosition + directionOffset + 7]);
                    var offsetX = HelperFuncts.SumTwoBytes(_bytes[currentBytesPosition + directionOffset + 8], _bytes[currentBytesPosition + directionOffset + 9]);
                    var offsetY = HelperFuncts.SumTwoBytes(_bytes[currentBytesPosition + directionOffset + 10], _bytes[currentBytesPosition + directionOffset + 11]);

                    _directions[dir].AddFrame(frame,  width, height, offsetX, offsetY, WidthSpr, HeightSpr, _bytes, PixelDataSize, currentBytesPosition + directionOffset + 12);


                    directionOffset += (12 + PixelDataSize);
                }

                HeightSpr += HeightMAX;
                WidthSprMAX = Math.Max(WidthSprMAX, WidthSpr);
                WidthSpr = 0;
                HeightMAX = 0;

            }

            string a = "";



        }

        private void InitializeDirections(int numberOfFrames)
        {
            int directionOffset = -1;
            _directions[0] = new FrameDirecion(numberOfFrames, 0);
            int bytePos = 38;
            for (int i = 1; i < 6; i++)
            {
                directionOffset = HelperFuncts.SumTwoBytes(_bytes[bytePos], _bytes[++bytePos], _bytes[++bytePos], _bytes[++bytePos]);
                if (directionOffset > 0)
                {
                    _directions[i] = new FrameDirecion(numberOfFrames, directionOffset);
                    _numberOfDirections++;
                }
                bytePos++;
            }




            //N = new FrameDirecion(numberOfFrames, 0);
            //_numberOfDirections++;
            //int directionOffset = -1;
            //directionOffset = HelperFuncts.SumTwoBytes(_bytes[38], _bytes[39], _bytes[40], _bytes[41]);
            //if (directionOffset > 0)
            //{
            //    NE = new FrameDirecion(numberOfFrames, directionOffset);
            //    _numberOfDirections++;
            //}
            //directionOffset = HelperFuncts.SumTwoBytes(_bytes[42], _bytes[43], _bytes[44], _bytes[45]);
            //if (directionOffset > 0)
            //{
            //    SE = new FrameDirecion(numberOfFrames, directionOffset);
            //    _numberOfDirections++;
            //}
            //directionOffset = HelperFuncts.SumTwoBytes(_bytes[46], _bytes[47], _bytes[48], _bytes[49]);
            //if (directionOffset > 0)
            //{
            //    S = new FrameDirecion(numberOfFrames, directionOffset);
            //    _numberOfDirections++;
            //}
            //directionOffset = HelperFuncts.SumTwoBytes(_bytes[50], _bytes[51], _bytes[52], _bytes[53]);
            //if (directionOffset > 0)
            //{
            //    SW = new FrameDirecion(numberOfFrames, directionOffset);
            //    _numberOfDirections++;
            //}
            //directionOffset = HelperFuncts.SumTwoBytes(_bytes[54], _bytes[55], _bytes[56], _bytes[57]);
            //if (directionOffset > 0)
            //{
            //    NW = new FrameDirecion(numberOfFrames, directionOffset);
            //    _numberOfDirections++;
            //}
        }


        //private int ReadByte(int number)
        //{
        //    int hexIn = -1;
        //    for (int i = 0; i <= number; i++)
        //    {
        //        hexIn = _fs.ReadByte();
        //        //hex = string.Format("{0:X2}", hexIn);
        //    }
        //    return hexIn;
        //}

        //private int[] ReadByte(int starting, int number)
        //{
        //    int[] ret = new int[number];
        //    int hexIn = -1;
        //    for (int i = 0; i <= number; i++)
        //    {
        //        hexIn = _fs.ReadByte();
        //        //hex = string.Format("{0:X2}", hexIn);
        //    }
        //    return hexIn;
        //}
    }
}
