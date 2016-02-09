using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fo2
{
    public class MouseLight
    {
        public Vector2 Position;
        public Vector4 Color;
        public float Intensity;
        private float _intensityMin = 1f;
        private float _intensityMax = 2f;
        public float Radius;
        private float _radiusMin = 2;
        private float _radiusMax = 50;

        private float _intensityMult = 0.1f;
        private float _radiusMult = 5f;

        private double _count = 0;

        public MouseLight(Vector2 Position, Vector4 Color)
        {
            this.Position = Position;
            this.Color = Color;
            
        }

        public void LightUp(double gameTime)
        {
            _count += gameTime;
            if (_count > 10)
            {

                if (Intensity < _intensityMax)
                {
                    Intensity += _intensityMult;
                }

                if (Radius < _radiusMax)
                {
                    Radius += _radiusMult;
                }
                _count = 0;
            }
        }
        public void LightDown(double gameTime)
        {
            _count += gameTime;
            if (_count > 20)
            {

                if (Intensity > _intensityMin)
                {
                    Intensity -= _intensityMult;
                }

                if (Radius > _radiusMin)
                {
                    Radius -= _radiusMult;
                }
                _count = 0;
            }
        }

        public void UpdatePosition(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
    }
}
