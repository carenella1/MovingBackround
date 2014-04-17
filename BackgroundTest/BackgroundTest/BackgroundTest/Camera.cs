using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    class Camera
    {
        private Viewport _viewport;
        private Vector2 _position;
        private Vector2 _origin;
        private float _zoom;
        private float _rotation;

        public Vector2 Position { get { return _position; } set { _position = value; } }
        public Vector2 Origin { get { return _origin; } set { _origin = value; } }
        public float Zoom { get { return _zoom; } set { _zoom = value; } }
        public float Rotation { get { return _rotation; } set { _rotation = value; } }

        public Camera(Viewport viewport)
        {
            _viewport = viewport;
            _origin = new Vector2(_viewport.Width / 2.0f, _viewport.Height / 2.0f);
            _zoom = 1.0f;
        }

        public void Move(Vector2 distance)
        {
            _position += distance;
        }

        public void Reset()
        {
            _position = Vector2.Zero;
            _origin = new Vector2(_viewport.Width / 2.0f, _viewport.Height / 2.0f);
            _zoom = 1.0f;
        }

        //Parallax is speed of movement. 0.5f is half speed, 2.0f is double speed.
        public Matrix View(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-_position * parallax, 0.0f))
                * Matrix.CreateTranslation(new Vector3(-_origin, 0.0f))                 //Scrolling will not affect rotation or zoom (center of screen is still pivot point).
                * Matrix.CreateRotationZ(_rotation)
                * Matrix.CreateScale(_zoom, _zoom, 1)
                * Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }
    }
}