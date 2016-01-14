using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fo2
{
    class FRM
    {
        private FrameDirecion[] _directions;

        

        private int _numberOfDirections = 0;
        private byte[] _bytes;


        public FRM()
        {
            //_fs = new FileStream("C:/HANPWRMJ.FRM", FileMode.Open);
            _bytes = File.ReadAllBytes("C:/HANPWRMJ.FRM");
            _directions = new FrameDirecion[6];
            int numberOfFrames = HelperFuncts.SumTwoBytes(_bytes[8], _bytes[9]);
            int frameArea = HelperFuncts.SumTwoBytes(_bytes[58], _bytes[59], _bytes[60], _bytes[61]);

            InitializeDirections(numberOfFrames);

            for (int i = 0; i < _numberOfDirections; i++)
            {
                int directionOffset = ;
                //Получаем смещение кадра по Х и У для данного направления
                l_pFRM->doffX[nDir] = pUtil->GetW((WORD*)&doffX[nDir]);
                l_pFRM->doffY[nDir] = pUtil->GetW((WORD*)&doffY[nDir]);


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
