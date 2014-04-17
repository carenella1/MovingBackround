using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    class Layer
    {
        private Vector2 _parallax;
        private List<Sprite> _SPRITES;
        private Camera _camera;

        public Vector2 Parallax { get { return _parallax; } set { _parallax = value; } }
        public List<Sprite> SPRITES { get { return _SPRITES; } }

        public Layer(Camera camera)
        {
            _camera = camera;
            _parallax = Vector2.One;
            _SPRITES = new List<Sprite>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.View(_parallax));

            foreach (Sprite sprite in _SPRITES)
                sprite.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}