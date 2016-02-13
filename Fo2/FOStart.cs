using Fo2.MapObjects;
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
        private FrameCounter _frameCounter = new FrameCounter();
        private Map _map;

        private Effect effect;
        RenderTarget2D renderTarget;
        List<Light> Lights = new List<Light>();

        public static Texture2D BlankTexture { get; private set; }
        public MouseLight _mouseLight;

        private GenericMapObject _dude;
        private int _direction;

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
            _mouseLight = new MouseLight(new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y), new Vector4(1, 1, 1, 1));

            _hexes = new Hex[40000];
            for (int i = 0; i < _hexes.Length; i++)
            {
                Vector2 position = HelperFuncts.NextHexPos();
                _hexes[i] = new Hex(position);
            }
            MovementHelper.Hexes = _hexes;
            previousState = Keyboard.GetState();
            HelperFuncts.GraphicsDevicePointer = graphics.GraphicsDevice;
            Components.Add(new FrameRateCounter(this));

//            MovementHelper.ShortestPath(202,805);

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

            effect = Content.Load<Effect>("ambient");
            effect.Parameters["ScreenWidth"].SetValue((float)graphics.GraphicsDevice.Viewport.Width);
            effect.Parameters["ScreenHeight"].SetValue((float)graphics.GraphicsDevice.Viewport.Height);

            effect.Parameters["AmbientColor"].SetValue(new Vector4(0.15f, 0.18f, 0.9f, 1f));
            effect.Parameters["AmbientColor"].SetValue(new Vector4(0.8f,0.8f,1f,1));
            effect.Parameters["AmbientIntensity"].SetValue(0.8f);

            PresentationParameters pp = graphics.GraphicsDevice.PresentationParameters;
            renderTarget = new RenderTarget2D(graphics.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight, true, graphics.GraphicsDevice.DisplayMode.Format, DepthFormat.Depth24);
            Lights.Add(new Light(new Vector2(-883, 1586), new Vector4(1, 0.8f, 0.4f, 1), 1.31f, 10));
            Lights.Add(new Light(new Vector2(-515, 1489), new Vector4(1, 0.8f, 0.4f, 1), 1.5f, 10));
            
            //Lights.Add(new Light(new Vector2(-600, 1500), new Vector4(1, 1, 1, 1), 1.1f, 500));



            _map = new Map(_hexes);
            _dude =(GenericMapObject) MapObjectFactory.GetMapObject("HMMAXX", MapObjectType.Critter, 202 ); //18890
            _direction = 3;
            _dude.Turn(_direction);
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
            var gt = gameTime.ElapsedGameTime.Milliseconds;

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

            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                _mouseLight.LightUp(gt);
            }
            else
            {
                _mouseLight.LightDown(gt);
            }
            _mouseLight.UpdatePosition(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);

            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                var end = -1;
                foreach (var hex in _hexes)
                {
                    end = MovementHelper.Inside(hex._vertexes,
                        _camera.WorldToScreen(new Vector2(Mouse.GetState().Position.X,Mouse.GetState().Position.Y)), hex._actualNum);
                    if (end != -1) break; 
                }
                _dude.Walk(MovementHelper.ShortestPath(_dude.HexPosition, end));
            }


            foreach (var mapObj in _map._mapObjects)
            {
                mapObj.Update(gt);
            }
            foreach (var light in Lights)
            {
                light.Update(gt);
            }

            
            if (keyboardState.IsKeyDown(Keys.E) && keyboardState != previousState)
            {
                if(_direction == 5)
                {
                    _direction = 0;
                }
                else
                {
                    _direction++;
                }
                _dude.Turn(_direction);
            }
            if (keyboardState.IsKeyDown(Keys.Q) && keyboardState != previousState)
                _dude.Walk(new int[] {3,3,3,2 }); //202, 403,404,605,805


            _dude.Update(gt);
            

            previousState = keyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            //spriteBatch.Begin();
            var viewMatrix = _camera.GetViewMatrix();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: viewMatrix);
            //effect.CurrentTechnique.Passes[0].Apply();

            foreach (var tiles in _map._tiles)
            {
                tiles.Value.Draw(spriteBatch);
            }

            foreach (var mo in _map._mapObjects)
            {
                mo.Draw(spriteBatch);
            }

            _dude.Draw(spriteBatch);

            foreach (var hex in _hexes)
            {
                hex.Draw(spriteBatch, tempFont);
            }

            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            effect.CurrentTechnique.Passes["AmbientLightPass"].Apply();
            spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
            spriteBatch.End();

            for (int i = 0; i < Lights.Count; i++)
                {
                    effect.Parameters["LightPosition"].SetValue(_camera.WorldToScreen(Lights[i].Position));
                    //effect.Parameters["LightPosition"].SetValue(Lights[i].Position);
                    effect.Parameters["LightColor"].SetValue(Lights[i].Color);
                    effect.Parameters["LightIntensity"].SetValue(Lights[i].Intensity);
                    effect.Parameters["LightRadius"].SetValue(_camera.Zoom*(Lights[i].Radius));

                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                    effect.CurrentTechnique.Passes["PointLightPass"].Apply();
                    spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
                    spriteBatch.End();
                }

            //LightMouse
            effect.Parameters["LightPosition"].SetValue(_mouseLight.Position);
            effect.Parameters["LightColor"].SetValue(_mouseLight.Color);
            effect.Parameters["LightIntensity"].SetValue(_mouseLight.Intensity);
            effect.Parameters["LightRadius"].SetValue(_camera.Zoom * (_mouseLight.Radius));

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            effect.CurrentTechnique.Passes["PointLightPass"].Apply();
            spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
            spriteBatch.End();
            //


            spriteBatch.Begin(transformMatrix: viewMatrix);
            //coordinates
            spriteBatch.DrawString(tempFont, "screen X=" + Mouse.GetState().Position.X.ToString(), _camera.ScreenToWorld(new Vector2(700, 20)), Color.White);
            spriteBatch.DrawString(tempFont, "screen Y=" + Mouse.GetState().Position.Y.ToString(), _camera.ScreenToWorld(new Vector2(700, 40)), Color.White);

            spriteBatch.DrawString(tempFont, "world X=" + _camera.ScreenToWorld(new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)).X, _camera.ScreenToWorld(new Vector2(700, 60)), Color.White);
            spriteBatch.DrawString(tempFont, "world Y=" + _camera.ScreenToWorld(new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)).Y, _camera.ScreenToWorld(new Vector2(700, 80)), Color.White);


            //FPS
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _frameCounter.Update(deltaTime);
            var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);
            spriteBatch.DrawString(tempFont, fps, _camera.ScreenToWorld(new Vector2(700, 100)), Color.White);
            ///
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
