using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BackgroundTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Layer> layers;
        Camera camera;
        KeyboardState keyState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            camera = new Camera(GraphicsDevice.Viewport);

            //List of different layered views. 0.0f is furthest back layer.
            layers = new List<Layer>
            {
                new Layer(camera) { Parallax = new Vector2(0.0f, 1.0f) },
                new Layer(camera) { Parallax = new Vector2(0.1f, 1.0f) },
                new Layer(camera) { Parallax = new Vector2(0.2f, 1.0f) }
            };

            //Each layer has it's own sprite. To add more sprites (images), just use the same layer.
            layers[0].SPRITES.Add(new Sprite { Texture = Content.Load<Texture2D>("Assets/ground mountain") });
            layers[1].SPRITES.Add(new Sprite { Texture = Content.Load<Texture2D>("Assets/forest layer1"), Position = new Vector2(0, 602) });
            //layers[2].SPRITES.Add(new Sprite { Texture = Content.Load<Texture2D>("Assets/tree1"), Position = new Vector2(0, 50) });

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W))
                camera.Move(new Vector2(0.0f, -400.0f * elapsedTime));

            if (keyState.IsKeyDown(Keys.A))
                camera.Move(new Vector2(-400.0f * elapsedTime, 0.0f));

            if (keyState.IsKeyDown(Keys.S))
                camera.Move(new Vector2(0.0f, 400.0f * elapsedTime));

            if (keyState.IsKeyDown(Keys.D))
                camera.Move(new Vector2(400.0f * elapsedTime, 0.0f));

            if (keyState.IsKeyDown(Keys.PageUp))
                camera.Zoom += 0.5f * elapsedTime;

            if (keyState.IsKeyDown(Keys.PageDown))
                camera.Zoom -= 0.5f * elapsedTime;

            if (keyState.IsKeyDown(Keys.R))
                camera.Reset();

            if (keyState.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {    
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            foreach (Layer layer in layers)
                layer.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}