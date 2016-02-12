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
        private double _counter = 0;
        private Stance Stance = Stance.Standing;
        private int _steps;
        private int _direction;

        public GenericMapObject(string name, MapObjectType objType, int HexInt)
        {
            TextureName = name;
            HexPosition = HexInt;
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
            _texture = new FRM(_repo + _prefix + TextureName + "AA.frm", MovementHelper.HexX(HexInt), MovementHelper.HexY(HexInt));
        }

        public void Turn(int turn)
        {
            _texture.CurrentDirection = turn;
        }

        public void Walk(int direction)
        {
            _direction = direction;

            Stance = Stance.Walking;
            _texture = new FRM(_repo + _prefix + TextureName + "AB.frm", MovementHelper.HexX(HexPosition), MovementHelper.HexY(HexPosition));
            _texture.CurrentDirection = direction;
            _steps = _texture._directions[direction]._frames.Length;
            _steps = 4;
            
        }

        public override void Update(double gameTime)
        {
            switch (Stance)
            {
                case Stance.Standing:
                    Standing(gameTime);
                    break;
                case Stance.Walking:
                    Walking(gameTime);
                    break;
                default:
                    break;
            }
        }

        

        private void Standing(double gameTime)
        {
            _counter += gameTime;
            if (_counter > 150)
            {
                _texture.Update(gameTime);
                _counter = 0;
            }
        }

        private void Walking(double gameTime)
        {
            _counter += gameTime;
            if (_counter > 150)
            {
                _texture.Update(gameTime);
                _counter = 0;
                _steps--;
            }
            if (_steps == 0)
            {
                Stance = Stance.Standing;
                HexPosition = MovementHelper.GetAdjecentHex(_direction, HexPosition);
                _texture = new FRM(_repo + _prefix + TextureName + "AA.frm", MovementHelper.HexX(HexPosition), MovementHelper.HexY(HexPosition));
                _texture.CurrentDirection = _direction;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            _texture.Draw(sb);
        }
    }
}
