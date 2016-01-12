using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fo2
{
    class ScenaryObject
    {

        public Vector2 Position { get; set; }
        public Texture2D _texture;

        public ScenaryObject(Vector2 position, Texture2D texture)
        {
            string fileName = texture.Name;
            string beginning = fileName.Substring(0, 11);
            fileName = "Content/" + beginning + "/inf" + fileName.Substring(11, fileName.Length - 11);
            if (fileName.Substring(fileName.Length - 3, 3) == "000") { fileName = fileName.Substring(0, fileName.Length - 4); }
            byte[] bytes = File.ReadAllBytes(fileName + ".inf");

            int x1 = bytes[10];
            int x2 = bytes[11];

            var asds = 0xFFFFFFFFFFFFFFF5;
            sbyte v = Convert.ToSByte(asds);

            var asd = 0xFFFFFFFFFFFF + bytes[10] + bytes[11];
          //  int numberX = (x1 == 255) ? asd : (x1 * 16 * 16 + x2);


            int y1 = bytes[22];
            int y2 = bytes[23];
            asd = 0xFFFFFFFFFFFF + bytes[22] + bytes[23];
        //    int numberY = (y1 == 255) ? asd : (y1 * 16 * 16 + y2);


           // Position = new Vector2(position.X - texture.Width/2 + numberX, position.Y - texture.Height + numberY);
            _texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont tempFont)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
            spriteBatch.DrawString(tempFont, _texture.Name, Position, Color.Red);
        }
    }
}
