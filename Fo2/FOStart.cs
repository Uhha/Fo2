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
        Texture2D[] _texturesList;
        int[] _sequence;
        private Camera2D _camera;
        private Hex[] _hexes;
        private KeyboardState previousState;
        private SpriteFont tempFont;
        //private Texture2D _scenary;
        private LinkedList<ScenaryObject> _scentaryObjects;

        private float _scenaryMoveX = -327;
        private float _scenaryMoveY = 1566;

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

            _scentaryObjects = new LinkedList<ScenaryObject>();
            _camera = new Camera2D(GraphicsDevice);
            _camera.Position = new Vector2(-400,1400);
            _hexes = new Hex[40000];
            for (int i = 0; i < _hexes.Length; i++)
            {
                Vector2 position = HelperFuncts.NextHexPos();
                _hexes[i] = new Hex(position);
            }


            //for (var i = 0; i < 40000; i++)
            //{
            //    var x = 200 - (i % 200);
            //    var y = i / 200;
            //    var mod = 0;
            //    if (x % 2 == 0)
            //    {
            //        mod = 1;
            //    }

            //    var xpos =( (x * 24) + ((mod) * 8) + (y * 16) + 41 ) -4800;

            //    var ypos = ((200 - x) * 6) + ((mod) * 6) + (y * 12) - 9;
            //    _hexes[i] = new Hex(new Vector2(xpos,ypos));
            //}


            previousState = Keyboard.GetState();



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

            //byte[] bytes = File.ReadAllBytes(@"Content/maps/artemple.map");
            byte[] bytes = File.ReadAllBytes(@"Content/maps/miniart3.map");
            string[] tilesNames = File.ReadAllLines(@"Content/art/tiles/tiles.lst");
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
                        _texturesList[positionInTheList] = Content.Load<Texture2D>("art/tiles/"+tilesNames[positionInTheList].Substring(0, tilesNames[positionInTheList].Length - 4));
                    }
                    catch (System.Exception)
                    {
                       _texturesList[positionInTheList] = Content.Load<Texture2D>(tilesNames[positionInTheList].Substring(0, tilesNames[positionInTheList].Length - 4).ToUpper());

                    }
                } 
                
            }

            //Objs
            string[] objNames = (File.ReadAllLines(@"Content/art/scenary/scenery.lst")).Select(l => l.Trim()).ToArray(); 
            int cnt = 0;
            for (int i = 42328; i < bytes.Length && cnt < 11; i+=88) // 46408
            {

                int x1 = bytes[i+6];
                int x2 = bytes[i+7];
                int onTheMap = (x1 * 16 * 16 + x2) + 0;

                int y1 = bytes[i + 34];
                int y2 = bytes[i + 35];
                int positionInTheList = (y1 * 16 * 16 + y2) + 0;
                if (bytes[i + 44] == 2)
                {
                    _scentaryObjects.AddFirst(ObjectFactory.GetScenaryObject(onTheMap, positionInTheList, objNames, _hexes, Content));
                }
                cnt++;
            }

            //_scenaryMoveX = _scentaryObjects.ElementAt<ScenaryObject>(44).Position.X;
           // _scenaryMoveY = _scentaryObjects.ElementAt<ScenaryObject>(44).Position.Y;

            //_scenary = Content.Load<Texture2D>("art/scenary/firpit01_000");
            //_scenaryMoveX = (int)(_hexes[17294]._vertexes[4].X) - 2;
            //_scenaryMoveY = (int)(_hexes[17294]._vertexes[4].Y) - _scenary.Height;

            //_scenaryMoveX = (_hexes[17294]._vertexes[0].X) - 17.5f;
            //_scenaryMoveY = (_hexes[17294]._vertexes[0].Y) - 55 + 12; // 27.5f

            //_scenary = Content.Load<Texture2D>("unt1");

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


            if (keyboardState.IsKeyDown(Keys.H) && !previousState.IsKeyDown(Keys.H))
                _scenaryMoveX -=  1f;
            if (keyboardState.IsKeyDown(Keys.K) && !previousState.IsKeyDown(Keys.K))
                _scenaryMoveX +=  1f;
            if (keyboardState.IsKeyDown(Keys.U) && !previousState.IsKeyDown(Keys.U))
                _scenaryMoveY -=  1f;
            if (keyboardState.IsKeyDown(Keys.J) && !previousState.IsKeyDown(Keys.J))
                _scenaryMoveY +=  1f;

            // rotation
            if (keyboardState.IsKeyDown(Keys.Q))
                _camera.Rotation -= deltaTime;

            if (keyboardState.IsKeyDown(Keys.W))
                _camera.Rotation += deltaTime;

            //zoomation
            if (keyboardState.IsKeyDown(Keys.A))
                _camera.ZoomIn(0.8f);

            if (keyboardState.IsKeyDown(Keys.Z))
                _camera.ZoomOut(0.8f);

            // movement
            if (keyboardState.IsKeyDown(Keys.Up))
                _camera.Position -= new Vector2(0, 1250) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Down))
                _camera.Position += new Vector2(0, 1250) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Left))
                _camera.Position -= new Vector2(1250, 0) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Right))
                _camera.Position += new Vector2(1250, 0) * deltaTime;


            //_scentaryObjects.ElementAt<ScenaryObject>(44).Position = new Vector2(_scenaryMoveX, _scenaryMoveY);

            previousState = keyboardState;

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


            //spriteBatch.Draw(_scenary, new Rectangle(_scenaryMoveX, _scenaryMoveY, _scenary.Width, _scenary.Height), Color.White);

            foreach (var so in _scentaryObjects)
            {
                so.Draw(spriteBatch, tempFont);
            }

            foreach (var hex in _hexes)
            {
                hex.Draw(spriteBatch, tempFont);
            }

            //spriteBatch.Draw(_scenary, new Rectangle((int)(_hexes[17294]._vertexes[4].X)-2 , (int)(_hexes[17294]._vertexes[4].Y) - _scenary.Height, _scenary.Width, _scenary.Height), Color.White);
            //spriteBatch.Draw(_scenary, new Vector2(_scenaryMoveX, _scenaryMoveY), Color.White);








            spriteBatch.DrawString(tempFont, "screen X=" + Mouse.GetState().Position.X.ToString(), _camera.ScreenToWorld(new Vector2(700, 20)), Color.White);
            spriteBatch.DrawString(tempFont, "screen Y=" + Mouse.GetState().Position.Y.ToString(), _camera.ScreenToWorld(new Vector2(700, 40)), Color.White);

            spriteBatch.DrawString(tempFont, "world X=" + _camera.ScreenToWorld(new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)).X, _camera.ScreenToWorld(new Vector2(700, 60)), Color.White);
            spriteBatch.DrawString(tempFont, "world Y=" + _camera.ScreenToWorld(new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)).Y, _camera.ScreenToWorld(new Vector2(700, 80)), Color.White);

            
            

            spriteBatch.DrawString(tempFont, "obj X=" + _scenaryMoveX, _camera.ScreenToWorld(new Vector2(700, 100)), Color.Red);
            spriteBatch.DrawString(tempFont, "obj Y=" + _scenaryMoveY, _camera.ScreenToWorld(new Vector2(700, 120)), Color.Red);



            


            Vector2[] ver1 = new Vector2[4];
            ver1[0] = new Vector2(520,0);
            ver1[1] = new Vector2(600, 0);
            ver1[2] = new Vector2(600, 36);
            ver1[3] = new Vector2(520, 36);


            HelperFuncts.DrawPolygon(spriteBatch, ver1, 4, Color.White, 1);

            Vector2[] ver2 = new Vector2[4];
            ver2[0] = new Vector2(568, 0);
            ver2[1] = new Vector2(600, 0);
            ver2[2] = new Vector2(600, 16);
            ver2[3] = new Vector2(568, 16);


            HelperFuncts.DrawPolygon(spriteBatch, ver2, 4, Color.White, 1);


            spriteBatch.End();

            spriteBatch.Begin();
           
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
