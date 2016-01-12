using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
            Position = new Vector2(position.X - texture.Width / 2, position.Y - 200);
            _texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont tempFont)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
            spriteBatch.DrawString(tempFont, _texture.Name, Position, Color.Red);
        }
    }
}
