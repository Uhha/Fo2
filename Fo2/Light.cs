using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fo2
{
    class Light
    {
        public Vector2 Position;
        public Vector4 Color;
        public float Intensity;
        private float _intensityMin = 1.3f;
        private float _intensityMax = 1.8f;
        public float Radius;
        private float _radiusMin = 10;
        private float _radiusMax = 15;

        private float _intensityMult = 0.01f;
        private float _radiusMult = 0.1f;

        private double _count = 0;

        public Light(Vector2 Position, Vector4 Color, float Intensity, float Radius)
        {
            this.Position = Position;
            this.Color = Color;
            this.Intensity = Intensity;
            this.Radius = Radius;
        }

        public void Update(double gameTime)
        {
            _count += gameTime;
            if (_count > 30)
            {

                if (Intensity > _intensityMin && Intensity < _intensityMax)
                {
                    Intensity += _intensityMult;
                }
                else
                {
                    _intensityMult = _intensityMult * -1;
                    Intensity += _intensityMult;
                }

                if (Radius > _radiusMin && Radius < _radiusMax)
                {
                    Radius += _radiusMult;
                }
                else
                {
                    _radiusMult = _radiusMult * -1;
                    Radius += _radiusMult;
                }
                _count = 0;
            }
        }
    }
}
