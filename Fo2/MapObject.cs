using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fo2
{
    abstract class MapObject
    {
        public MapObjectType MapObjectType;
        public string TextureName;
        

        public abstract void Update(double gameTime);
        public abstract void Draw(SpriteBatch sb);
      
    }
}
