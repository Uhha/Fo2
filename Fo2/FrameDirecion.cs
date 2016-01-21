using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Fo2
{
    internal class FrameDirecion
    {
        public FRM _parent;
        public Frame[] _frames;
        public int _directionOffsetX;
        public int _directionOffsetY;
        private int _numberOfFrames;

        public int _directionOffset;

        private double _counter = 0;
        private int _anumationIndex = 0;


        public FrameDirecion(FRM parent, int numberOfFrames, int directionOffset) 
        {
            _parent = parent;
            _frames = new Frame[numberOfFrames];
            _numberOfFrames = numberOfFrames;
            _directionOffset = directionOffset;
        }

        private int _currentFrame = 0;
        public void AddFrame(int previousFrame, int width, int height, int offsetX, int offsetY, int widthSpr, int heightSpr, byte[] _bytes, int pixelDataSize, int startingPoint)
        {
            _frames[_currentFrame] = new Frame(this, previousFrame, width, height, offsetX, offsetY, widthSpr, heightSpr, _bytes, pixelDataSize, startingPoint);
            _currentFrame++;
        }

        private void NextIndex()
        {
            _anumationIndex++;
            if (_anumationIndex == _numberOfFrames )
            {
                _anumationIndex = 0;
            }
        }


        public void Update(double gameTime)
        {
            _counter += gameTime;
            if (_counter > 150)
            {
                NextIndex();
                _counter = 0;
            }
        }

        

        public void Draw(SpriteBatch sb)
        {
            _frames[_anumationIndex].Draw(sb);
        }


    }
}