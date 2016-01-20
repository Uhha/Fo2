using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework;

namespace Fo2
{
    internal class Frame
    {
        public FrameDirecion _parent;
        private Texture2D _texture;
        private int _previousFrame;
        public int _width;
        public int _hight;
        private int _offsetX;
        private int _offsetY;
        private int _widthSpr;
        private int _heightSpr;
        
        

       
        public Frame(FrameDirecion parent, int previousFrame, int width, int height, int offsetX, int offsetY, int widthSpr, int heightSpr, byte[] bytes, int pixelDataSize, int startingPoint) 
        {
            _parent = parent;
            _previousFrame = previousFrame;
            _width = width;
            _hight = height;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _widthSpr = widthSpr;
            _heightSpr = heightSpr;

            byte[] textureArray = new byte[pixelDataSize * 4];
            for (int i = 0; i < textureArray.Length; i++,startingPoint++)
            {
                HelperFuncts.GetPalleteColor(bytes[startingPoint], out textureArray[i], out textureArray[++i], out textureArray[++i], out textureArray[++i]);
            }
            _texture = new Texture2D(HelperFuncts.GraphicsDevicePointer, _width, _hight);
            _texture.SetData<byte>(textureArray);
            
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_texture, new Rectangle(PositionX(), PositionY(), _width, _hight), Color.White);


            var additionaDraw = _parent._parent._additionalDrawElements;
            if (additionaDraw != null)
            {
                foreach (var posVect in additionaDraw)
                {
                    Vector2 pos = new Vector2(posVect.X - _width/2, posVect.Y - _hight);
                    sb.Draw(_texture, new Rectangle((int)pos.X, (int)pos.Y, _width, _hight), Color.White);
                }
            }
        }

        private int PositionX()
        {
            return _parent._parent._posX + _parent._directionOffsetX + _parent._frames[_previousFrame]._offsetX - _width / 2;
        }

        private int PositionY()
        {
            return _parent._parent._posY + _parent._directionOffsetY + _parent._frames[_previousFrame]._offsetY - _hight;
        }

        //private void FormTexture()
        //{

        //_res = new byte[textureArray.Length * 4];
        //int aind = 0;
        //for (int j = 0; j < _res.Length; j++)
        //{

        //        _res[j] = (byte)(pal[textureArray[aind] * 3] * 4);
        //        _res[++j] = (byte)(pal[textureArray[aind] * 3 + 1] * 4);
        //        _res[++j] = (byte)(pal[textureArray[aind] * 3 + 2] * 4);
        //        _res[++j] = 255;

        //    aind++;
        //}


        //FormTexture();
        //    //var a_width = (4 - _width % 4) % 4;
        //    //int size = (a_width + _width) * _hight;

        //    //int[] data = new int[size];

        //    //var lastByte = _width * _hight - 1;

        //    //for (int i = 0; i < _hight; i++) {

        //    //    int k = lastByte - i * _width;
        //    //    int m = i * (_width + a_width);
        //    //    for (int j =0;  j < _width; j++, k--, m++)
        //    //    {
        //    //        data[m] = _bytes[k];
        //    //    }
        //        //memcpy(data + i * (width +  a_width), tdata - i * width,   width);

        //}
    }
}