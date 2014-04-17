using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    class Sprite
    {
        private Texture2D _texture;
        private Vector2 _position;

        public Texture2D Texture { get { return _texture; } set { _texture = value; } }
        public Vector2 Position { get { return _position; } set { _position = value; } }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}