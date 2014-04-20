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
        private Rectangle? _limits;     //Question mark means you can set it to null.

        public Vector2 Origin { get { return _origin; } set { _origin = value; } }
        public float Zoom { get { return _zoom; } set { _zoom = value; } }
        public float Rotation { get { return _rotation; } set { _rotation = value; } }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                //If there's a limit and the camera isn't transformed (zoomed or rotated), clamp the position.
                if (_limits != null && _zoom == 1.0f && _rotation == 0.0f)
                {
                    //Clamp to the first parameter the min value of second parameter and max value of third parameter.
                    _position.X = MathHelper.Clamp(_position.X, _limits.Value.X, _limits.Value.X + _limits.Value.Width - _viewport.Width);
                    _position.Y = MathHelper.Clamp(_position.Y, _limits.Value.Y, _limits.Value.Y + _limits.Value.Height - _viewport.Height);
                }
            }
        }

        public Rectangle? Limits
        {
            get { return _limits; }
            set
            {
                if (value != null)
                {
                    //Assign the limits here, but be sure it's always bigger than the viewport.
                    _limits = new Rectangle
                    {
                        X = value.Value.X,
                        Y = value.Value.Y,
                        Width = System.Math.Max(_viewport.Width, value.Value.Width),
                        Height = System.Math.Max(_viewport.Height, value.Value.Height)
                    };

                    //Validates camera's position with the new limit.
                    _position = Position;
                }
                else
                    _limits = null;
            }
        }

        public Camera(Viewport viewport)
        {
            _viewport = viewport;
            _origin = new Vector2(_viewport.Width / 2.0f, _viewport.Height / 2.0f);
            _zoom = 1.0f;
        }

        public void Move(Vector2 distance, bool rotated = false)
        {
            if (rotated)
                distance = Vector2.Transform(distance, Matrix.CreateRotationZ(-_rotation));

            _position += distance;
        }

        public void Reset()
        {
            _position = Vector2.Zero;
            _zoom = 1.0f;
            _rotation = 0.0f;
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

        //Use to make the camera follow an object.
        public void LookAt(Vector2 target)
        {
            _position = target - new Vector2(_viewport.Width / 2.0f, _viewport.Height / 2.0f);
        }
    }
}