using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.IO;

namespace Fo2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class FOStart : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D[] _texturesList;
        int[] _sequence;
        private Camera2D _camera;
        private Hex[] _hexes;
        private SpriteFont tempFont;
        private Texture2D _scenary;

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
            // TODO: Add your initialization logic here
            
            _camera = new Camera2D(GraphicsDevice);
            _camera.Position = new Vector2(-400,1400);
            _hexes = new Hex[40000];
            for (int i = 0; i < _hexes.Length; i++)
            {
                Vector2 position = HelperFuncts.NextHexPos();
                _hexes[i] = new Hex(position);
            }

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
            tempFont = Content.Load<SpriteFont>("TempFont");

            BlankTexture = new Texture2D(GraphicsDevice, 1, 1);
            BlankTexture.SetData(new Color[] { Color.White });
            HelperFuncts.blankTexture = BlankTexture;

            byte[] bytes = File.ReadAllBytes(@"C:\!tmp\f2\data\maps\artemple.map");
            string[] tilesNames = File.ReadAllLines(@"C:\!tmp\f2\data\art\tiles\tiles.lst");
            _texturesList = new Texture2D[tilesNames.Length];
            _sequence = new int[10000];

            int j = 0;
            for (int i = 236; i < 40236 - 3; i++)
            {
                int Y1 = bytes[i];
                int Y2 = bytes[++i];
                int X1 = bytes[++i];
                int X2 = bytes[++i];

                int positionInTheList = (X1 * 16 * 16 + X2) + 0;
                _sequence[j] = positionInTheList;
                j++;
                if (_texturesList[positionInTheList] == null) {
                    try
                    {
                        _texturesList[positionInTheList] = Content.Load<Texture2D>("bmp/"+tilesNames[positionInTheList].Substring(0, tilesNames[positionInTheList].Length - 4));
                    }
                    catch (System.Exception)
                    {
                       _texturesList[positionInTheList] = Content.Load<Texture2D>(tilesNames[positionInTheList].Substring(0, tilesNames[positionInTheList].Length - 4).ToUpper());

                    }
                } 
                
            }

            _scenary = Content.Load<Texture2D>("scenary/firpit01_000");

            // TODO: use this.Content to load your game content here
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

            // rotation
            if (keyboardState.IsKeyDown(Keys.Q))
                _camera.Rotation -= deltaTime;

            if (keyboardState.IsKeyDown(Keys.W))
                _camera.Rotation += deltaTime;

            // movement
            if (keyboardState.IsKeyDown(Keys.Up))
                _camera.Position -= new Vector2(0, 1250) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Down))
                _camera.Position += new Vector2(0, 1250) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Left))
                _camera.Position -= new Vector2(1250, 0) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Right))
                _camera.Position += new Vector2(1250, 0) * deltaTime;

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
            //spriteBatch.Begin();
            var viewMatrix = _camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: viewMatrix);



            HelperFuncts.TileNumX = 0;
            HelperFuncts.TileNumY = 0;
            for (int i = 0; i < 10000; i++)
            {
                Vector2 pos = HelperFuncts.NextTilePos();
                spriteBatch.Draw(_texturesList[_sequence[i]], new Rectangle((int)pos.X, (int)pos.Y, 80, 36), Color.White);
            }


            foreach (var hex in _hexes)
            {
                hex.Draw(spriteBatch, tempFont);
            }

            spriteBatch.Draw(_scenary, new Rectangle((int)(_hexes[17278]._vertexes[0].X) , (int)(_hexes[17278]._vertexes[0].Y), _scenary.Width, _scenary.Height), Color.White);

            spriteBatch.DrawString(tempFont, "X=" + Mouse.GetState().Position.X.ToString(), new Vector2(700, 20), Color.White);
            spriteBatch.DrawString(tempFont, "Y=" + Mouse.GetState().Position.Y.ToString(), new Vector2(700, 40), Color.White);




            spriteBatch.End();

            spriteBatch.Begin();
           
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
