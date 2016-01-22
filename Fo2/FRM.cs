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
        public int CurrentDirection = 0;
        public int _posX;
        public int _posY;
        public LinkedList<Vector2> _additionalDrawElements;


        public FRM(string filename, int posX, int posY)
        {
            byte[] bytes = File.ReadAllBytes(filename);
            _posX = posX;
            _posY = posY;

            _directions = new FrameDirecion[6];
            int numberOfFrames = HelperFuncts.SumTwoBytes(bytes[8], bytes[9]);

            Vector2[] dirOffsets = new Vector2[6];
            int v = 10;
            int w = 22;
            for (int i = 0; i < 6; i++, v++, w++)
            {
                dirOffsets[i] = new Vector2(HelperFuncts.SumTwoBytes(bytes[v], bytes[++v]), HelperFuncts.SumTwoBytes(bytes[w], bytes[++w]));
            }



            int frameAreaSize = HelperFuncts.SumTwoBytes(bytes[58], bytes[59], bytes[60], bytes[61]);
            int currentBytesPosition = 62; //3E

            InitializeDirections(numberOfFrames, bytes);

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
                //int[] ofs = { 0, -1, +1, 0, 0, 0, 0, +1 };
                //int[] ofsy = { 0, +1, 0, +1, +1, +1, +1, +1 };
                //int[] ofs = { 0, -5, -4, -12, -12, -16, -18, +1 };
                //int[] ofs = {-1, 1, 0 ,0,0,0,1,0};

                int previousOffsetX = 0;
                int previousOffsetY = 0;
                for (int frame = 0; frame < numberOfFrames; frame++)
                {

                    var width = HelperFuncts.SumTwoBytes(bytes[currentBytesPosition + directionOffset], bytes[currentBytesPosition + directionOffset + 1]);
                    WidthSpr += width;
                    var height = HelperFuncts.SumTwoBytes(bytes[currentBytesPosition + directionOffset + 2], bytes[currentBytesPosition + directionOffset + 3]);
                    HeightMAX = Math.Max(HeightMAX, height);

                    var PixelDataSize = HelperFuncts.SumTwoBytes(bytes[currentBytesPosition + directionOffset + 4], bytes[currentBytesPosition + directionOffset + 5], bytes[currentBytesPosition + directionOffset + 6], bytes[currentBytesPosition + directionOffset + 7]);
                    var offsetX = HelperFuncts.SumTwoBytes(bytes[currentBytesPosition + directionOffset + 8], bytes[currentBytesPosition + directionOffset + 9]);
                    var offsetY = HelperFuncts.SumTwoBytes(bytes[currentBytesPosition + directionOffset + 10], bytes[currentBytesPosition + directionOffset + 11]);

                    int previousFrame;
                    if (frame == 0) { previousFrame = 0; } //numberOfFrames - 1
                    else { previousFrame = frame; }
                    _directions[dir].AddFrame(previousFrame,  width, height, offsetX + previousOffsetX, offsetY + previousOffsetY, WidthSpr, HeightSpr, bytes, PixelDataSize, currentBytesPosition + directionOffset + 12);
                    previousOffsetX += offsetX;
                    previousOffsetY += offsetY;

                    directionOffset += (12 + PixelDataSize);
                }

                HeightSpr += HeightMAX;
                WidthSprMAX = Math.Max(WidthSprMAX, WidthSpr);
                WidthSpr = 0;
                HeightMAX = 0;

            }
        }

        private void InitializeDirections(int numberOfFrames, byte[] bytes)
        {
            int directionOffset = -1;
            _directions[0] = new FrameDirecion(this, numberOfFrames, 0);
            _numberOfDirections++;
            int bytePos = 38;
            for (int i = 1; i < 6; i++)
            {
                directionOffset = HelperFuncts.SumTwoBytes(bytes[bytePos], bytes[++bytePos], bytes[++bytePos], bytes[++bytePos]);
                if (directionOffset > 0)
                {
                    _directions[i] = new FrameDirecion(this, numberOfFrames, directionOffset);
                    _numberOfDirections++;
                }
                bytePos++;
            }
        }

        

        public void Update(double gameTime)
        {
            _directions[CurrentDirection].Update(gameTime);
        }


        public void Draw(SpriteBatch sb)
        {
            _directions[CurrentDirection].Draw(sb);
        }


        internal void AddDrawElement(Vector2 position)
        {
            if (_additionalDrawElements == null)
            {
                _additionalDrawElements = new LinkedList<Vector2>();
            }
            _additionalDrawElements.AddFirst(position);

        }


    }
}
