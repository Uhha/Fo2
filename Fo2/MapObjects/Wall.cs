using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace Fo2.MapObjects
{
    class Wall : MapObject
    {
        private FRM _texture;
        private int _frameNumer;
        private int _objDirection;
        private int _protoId;
        private int _numberOfSubItems;
        private int _hitPoints;
        private string _repo = HelperFuncts.Repo;


        public Wall(int start, byte[] bytes, Hex[] hexes, string[] wallNames, string[] itemsProtoNames, out int newStart)
        {
            MapObjectType = MapObjectType.Wall;
            TextureName = wallNames[HelperFuncts.SumTwoBytes(bytes[start + 34], bytes[start + 35])].Trim();

            int hexNumber = HelperFuncts.SumTwoBytes(bytes[start + 6], bytes[start + 7]);
            _objDirection = bytes[start + 31];
            string frmName = _repo + "art/walls/" + TextureName;
            _texture = new FRM(frmName, (int)hexes[hexNumber]._vertexes[0].X, (int)hexes[hexNumber]._vertexes[0].Y);
            _texture.CurrentDirection = _objDirection;
            _protoId = HelperFuncts.SumTwoBytes(bytes[start + 46], bytes[start + 47]); ;

            newStart = start += 88;
        }

        
        public override void Update(double gameTime)
        {
            _texture.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            _texture.Draw(sb);

            //var frame = _texture._directions[0]._frames[0];
           // sb.Draw(HelperFuncts.blankTexture, new Rectangle(_texture._posX, _texture._posY, frame._width, frame._hight), Color.White);
        }
        
    }
}
