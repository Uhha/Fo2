using System;
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
        private int _sprX;
        private int _sprY;
        private byte[] _bytes;
        private int _pixelDataSize;
        private int _startingPoint;

       
        public Frame(int frameNumber, int width, int height, int offsetX, int offsetY, int sprX, int sprY, byte[] bytes, int pixelDataSize, int startingPoint) 
        {
            _frameNumber = frameNumber;
            _width = width;
            _hight = height;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _bytes = bytes;
            _pixelDataSize = pixelDataSize;
            _startingPoint = startingPoint;
            _sprX = sprX;
            _sprY = sprY;

            FormTexture();
        }

        private void FormTexture()
        {
            var a_width = (4 - _width % 4) % 4;
            int size = (a_width + _width) * _hight;

            int[] data = new int[size];

            var lastByte = _width * _hight - 1;

            for (int i = 0; i < _hight; i++) {

                int k = lastByte - i * _width;
                int m = i * (_width + a_width);
                for (int j =0;  j < _width; j++, k--, m++)
                {
                    data[m] = _bytes[k];
                }
                //memcpy(data + i * (width +  a_width), tdata - i * width,   width);



                //Texture2D asd = new Texture2D( new GraphicsDevice(), 74,56);
                //asd.SetData<int>(data);
                //Bitmap bmp = new Bitmap(512, 512, PixelFormat.Format8bppIndexed);
                //var bmpData = bmp.LockBits(
                //    new Rectangle(0, 0, bmp.Width, bmp.Height),
                //    ImageLockMode.WriteOnly, bmp.PixelFormat);

                //// move our data in
                //Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
                //bmp.UnlockBits(bmpData);

                //// create the palette
                //var pal = bmp.Palette;
                //for (int i = 0; i < colors.Count; i++) pal.Entries[i] = colors[i];
                //bmp.Palette = pal;

                //// display
                //pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                //pictureBox1.Image = bmp;
            }
            string a = "";
        }
    }
}