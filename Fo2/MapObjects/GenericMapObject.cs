using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Fo2.MapObjects
{
    class GenericMapObject : MapObject
    {
        public FRM _texture;
        string _prefix = "";
        private string _repo = HelperFuncts.Repo;
        int _posX;
        int _posY;

        public GenericMapObject(string name, MapObjectType objType, int posX, int posY)
        {
            TextureName = name;
            _posX = posX;
            _posY = posY;
            switch (objType)
            {
                case MapObjectType.Item:
                    break;
                case MapObjectType.Critter:
                    _prefix = "/art/critters/";
                    break;
                case MapObjectType.Scenery:
                    break;
                case MapObjectType.Wall:
                    break;
                case MapObjectType.Tile:
                    break;
                case MapObjectType.Misc:
                    break;
                case MapObjectType.Interface:
                    break;
                case MapObjectType.Invent:
                    break;
                case MapObjectType.Head:
                    break;
                case MapObjectType.Backgrnd:
                    break;
                case MapObjectType.Skilldex:
                    break;
                default:
                    break;
            }

            MapObjectType = objType;
            _texture = new FRM(_repo + _prefix + TextureName + "AA.frm", _posX, _posY);
        }

        public void Turn(int turn)
        {
            _texture.CurrentDirection = turn;
        }

        public void Walk()
        {
            _texture = new FRM(_repo + _prefix + TextureName + "AB.frm", _posX, _posY);
        }

        public override void Update(double gameTime)
        {
            _texture.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            _texture.Draw(sb);
        }
    }
}
