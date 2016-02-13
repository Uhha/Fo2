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
        private int[] _directionsPath;
        private int _hexesOnPath;
        private int _hexesOnPathCount;
        private int _hexsteps;
        private bool _twoSteps;

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

        public void WalkOrig(int direction)
        {
            _direction = direction;

            Stance = Stance.Walking;
            _texture = new FRM(_repo + _prefix + TextureName + "AB.frm", MovementHelper.HexX(HexPosition), MovementHelper.HexY(HexPosition));
            _texture.CurrentDirection = direction;
            _steps = _texture._directions[direction]._frames.Length;
            _steps = 4;
            
        }

        public void Walk(int[] directionsPath)
        {
            _direction = directionsPath[0];
            _directionsPath = directionsPath;
            _hexesOnPath = directionsPath.Length;
            _hexesOnPathCount = 0;

            Stance = Stance.Walking;
            _texture = new FRM(_repo + _prefix + TextureName + "AB.frm", MovementHelper.HexX(HexPosition), MovementHelper.HexY(HexPosition));
            _texture.CurrentDirection = _direction;
            _steps = _texture._directions[_direction]._frames.Length;

            _twoSteps = SetNumberOfSteps();
            _steps = 4 * (directionsPath.Length);

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
                _hexsteps--;
            }

            if(_hexsteps == 0 && _steps != 0)
            {
                
                
                _texture._directions[_direction]._anumationIndex = 0;
                
                if (_twoSteps)
                {
                    var nextHex = MovementHelper.GetAdjecentHex(_direction, HexPosition);
                    nextHex = MovementHelper.GetAdjecentHex(_direction, nextHex);
                    HexPosition = nextHex;
                    _texture._posX = MovementHelper.HexX(nextHex);
                    _texture._posY = MovementHelper.HexY(nextHex);
                    _hexesOnPathCount++;
                }
                else
                {
                    var nextHex = MovementHelper.GetAdjecentHex(_direction, HexPosition);
                    HexPosition = nextHex;
                    _texture._posX = MovementHelper.HexX(nextHex);
                    _texture._posY = MovementHelper.HexY(nextHex);
                }
                _direction = _directionsPath[++_hexesOnPathCount];
                _twoSteps = SetNumberOfSteps();
                _texture.CurrentDirection = _direction;
            }


            if (_steps == 0)
            {
                Stance = Stance.Standing;
                var nextHex = MovementHelper.GetAdjecentHex(_direction, HexPosition);
                HexPosition = nextHex;
                _texture = new FRM(_repo + _prefix + TextureName + "AA.frm", MovementHelper.HexX(HexPosition), MovementHelper.HexY(HexPosition));
                _texture.CurrentDirection = _direction;
            }
        }

        private bool SetNumberOfSteps()
        {
            bool twoSteps = false;
            if (_hexesOnPathCount >= 0 && _hexesOnPathCount < _directionsPath.Length - 1)
            {
                if (_directionsPath[_hexesOnPathCount] == _directionsPath[_hexesOnPathCount + 1])
                {
                    _hexsteps = _texture._directions[0]._frames.Length;
                    twoSteps = true;
                }
                else
                {
                    _hexsteps = _texture._directions[0]._frames.Length / 2;
                }
            }
            else
            {
                _hexsteps = _texture._directions[0]._frames.Length / 2;
            }
            return twoSteps;
        }


        private void WalkingOrig(double gameTime)
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
