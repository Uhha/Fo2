using System;
using System.Collections.Generic;

namespace Fo2
{
    internal class FrameDirecion
    {
        private Frame[] _frames;
        public int _directionOffsetX;
        public int _directionOffsetY;
        private int _numberOfFrames;

        public int _directionOffset;


        public FrameDirecion(int numberOfFrames, int directionOffset)
        {
            _frames = new Frame[numberOfFrames];
            _numberOfFrames = numberOfFrames;
            _directionOffset = directionOffset;
        }

        private int _currentFrame = 0;
        public void AddFrame(int frame, int width, int height, int offsetX, int offsetY, int widthSpr, int heightSpr, byte[] _bytes, int pixelDataSize, int startingPoint)
        {
            _frames[_currentFrame] = new Frame(frame, width, height, offsetX, offsetY, widthSpr, heightSpr, _bytes, pixelDataSize, startingPoint);
            _currentFrame++;
        }

       
    }
}