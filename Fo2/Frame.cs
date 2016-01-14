using Microsoft.Xna.Framework.Graphics;

namespace Fo2
{
    internal class Frame
    {
        private Texture2D _texture;
        private int _frameNumber;
        private int _width;
        private int _hight;
        private int _offsetX;
        private int _offsetY;


        public Frame(int frameNumber, int width, int height, int offsetX, int offsetY)
        {
            _frameNumber = frameNumber;
            _width = width;
            _hight = height;
            _offsetX = offsetX;
            _offsetY = offsetY;
        }
    }
}