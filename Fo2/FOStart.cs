using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fo2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class FOStart : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Camera2D _camera;
        private Hex[] _hexes;
        private KeyboardState previousState;
        private SpriteFont tempFont;
        private Map _map;
        


        public static Texture2D BlankTexture { get; private set; }

        public FOStart()
        {
            IsMouseVisible = true;
            
            
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
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
            _camera = new Camera2D(GraphicsDevice);
            _camera.Position = new Vector2(-400,1400);

            _hexes = new Hex[40000];
            for (int i = 0; i < _hexes.Length; i++)
            {
                Vector2 position = HelperFuncts.NextHexPos();
                _hexes[i] = new Hex(position);
            }

            previousState = Keyboard.GetState();
            HelperFuncts.GraphicsDevicePointer = graphics.GraphicsDevice;
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tempFont = Content.Load<SpriteFont>("TempFont");
            BlankTexture = new Texture2D(GraphicsDevice, 1, 1);
            BlankTexture.SetData(new Color[] { Color.White });
            HelperFuncts.blankTexture = BlankTexture;

           _map = new Map(_hexes);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();

            //// rotation
            //if (keyboardState.IsKeyDown(Keys.Q))
            //    _camera.Rotation -= deltaTime;

            //if (keyboardState.IsKeyDown(Keys.W))
            //    _camera.Rotation += deltaTime;

            //zoomation
            if (keyboardState.IsKeyDown(Keys.X))
                _camera.ZoomIn(0.08f);

            if (keyboardState.IsKeyDown(Keys.Z))
                _camera.ZoomOut(0.08f);

            // movement
            if (keyboardState.IsKeyDown(Keys.Up))
                _camera.Position -= new Vector2(0, 1250) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Down))
                _camera.Position += new Vector2(0, 1250) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Left))
                _camera.Position -= new Vector2(1250, 0) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Right))
                _camera.Position += new Vector2(1250, 0) * deltaTime;



            foreach (var sceneryObjects in _map._sceneryObjects)
            {
                sceneryObjects.Update(gameTime.ElapsedGameTime.Milliseconds);
            }

            foreach (var mapObj in _map._mapObjects)
            {
                mapObj.Update(gameTime.ElapsedGameTime.Milliseconds);
            }

            previousState = keyboardState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //spriteBatch.Begin();
            var viewMatrix = _camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: viewMatrix);


            foreach (var tiles in _map._tiles)
            {
                tiles.Value.Draw(spriteBatch);
            }


            foreach (var mo in _map._mapObjects)
            {
                mo.Draw(spriteBatch);
            }

            foreach (var so in _map._sceneryObjects)
            {
                so.Draw(spriteBatch);
            }

            

            foreach (var hex in _hexes)
            {
                hex.Draw(spriteBatch, tempFont);
            }




            spriteBatch.DrawString(tempFont, "screen X=" + Mouse.GetState().Position.X.ToString(), _camera.ScreenToWorld(new Vector2(700, 20)), Color.White);
            spriteBatch.DrawString(tempFont, "screen Y=" + Mouse.GetState().Position.Y.ToString(), _camera.ScreenToWorld(new Vector2(700, 40)), Color.White);

            spriteBatch.DrawString(tempFont, "world X=" + _camera.ScreenToWorld(new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)).X, _camera.ScreenToWorld(new Vector2(700, 60)), Color.White);
            spriteBatch.DrawString(tempFont, "world Y=" + _camera.ScreenToWorld(new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)).Y, _camera.ScreenToWorld(new Vector2(700, 80)), Color.White);
            

            spriteBatch.End();

            spriteBatch.Begin();
           
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
