using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fo2
{
    class Hex
    {
        public Vector2 Position { get; set; }
        public Vector2[] _vertexes;
        private const int _count = 6;

        public int _actualNum;
        private static int _number = -1;

        public int[] _connected;
        public int _rank;
        public int _cameFrom;

        public Hex(Vector2 position)
        {
            _number++;
            _actualNum = _number;
            Position = position;
            _rank = UInt16.MaxValue;
            _cameFrom = 0;
            _vertexes = new Vector2[_count];
            _vertexes[0] = new Vector2(position.X + 16, position.Y);
            _vertexes[1] = new Vector2(position.X + 32, position.Y + 4);
            _vertexes[2] = new Vector2(position.X + 32, position.Y + 12);
            _vertexes[3] = new Vector2(position.X + 16, position.Y + 16);
            _vertexes[4] = new Vector2(position.X, position.Y + 12);
            _vertexes[5] = new Vector2(position.X, position.Y + 4);

            LinkedList<int> list = new LinkedList<int>();
            for (int direction = 0; direction < 6; direction++) 
            {
                var adjecentHex = MovementHelper.GetAdjecentHex(direction, _actualNum);
                if (adjecentHex >= 0 && adjecentHex < 40000) list.AddFirst(adjecentHex);
            }
            _connected = list.ToArray<int>();
        }

        



        public void Draw(SpriteBatch spriteBatch, SpriteFont tempFont)
        {
            //HelperFuncts.DrawPolygon(spriteBatch, _vertexes, _count, Color.White, 1);
            if (_actualNum == 805) //17278 //17294 _actualNum == 28707
            {  //x39 y86
                //spriteBatch.DrawString(tempFont, _actualNum.ToString(), new Vector2(Position.X + 3, Position.Y + 3), Color.White);
                HelperFuncts.DrawPolygon(spriteBatch, _vertexes, _count, Color.Red, 1);
            }
        }

    }

  
}
