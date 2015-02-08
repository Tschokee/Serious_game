using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SeriousGameZ.Controller;
using SeriousGameZ.Model;

//TODO: Draw function to view(?) and Update to controller

namespace SeriousGameZ
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //TODO: these variables will be transfered to model
        private Texture2D  orb;
        private Texture2D startButton;
        private Texture2D exitButton;
        private Texture2D pauseButton;
        private Texture2D resumeButton;
        private Texture2D loadingScreen;
        private Texture2D _helyesVagyHejesStartButton;

        private Vector2 orbPosition;
        private Vector2 startButtonPosition;
        private Vector2 exitButtonPosition;
        private Vector2 resumeButtonPosition;
        private Vector2 _helyesVagyHejesStartButtonPosition;

        private const float OrbWidth = 50f;
        private const float OrbHeight = 50f;
        private float speed = 10f;

        private Thread backgroundThread;
        private bool isLoading = false;
        MouseState  mouseState;
        MouseState  previousMouseState;
        private GameState gameState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            //TODO: if all variables changed use this
            //GameModel.Initialize();
            IsMouseVisible = true;

            //set the position of the buttons
            startButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 200);
            exitButtonPosition = new Vector2((GraphicsDevice.Viewport.Width) -125, 25);
            //TODO: set position & scale
            _helyesVagyHejesStartButtonPosition = GameSettings.MainMenuSettings.HelyesVagyHejesStartButtonPosition;

            //set the gamestate to start menu
            gameState = GameState.StartMenu;

            //get the mouse state
            mouseState = Mouse.GetState();
            previousMouseState = mouseState;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //TODO: if all variables changed use this
            //ContentLoader.ContentLoader.LoadContent(GraphicsDevice, Content);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load the buttonimages into the content pipeline
            startButton = Content.Load<Texture2D>(@"Sprites/Navigation/start");
            exitButton = Content.Load<Texture2D>(@"Sprites/Navigation/exit");
            _helyesVagyHejesStartButton = Content.Load<Texture2D>(@"Sprites/Navigation/helyes_vagy_hejes");

            //load the loading screen
            loadingScreen = Content.Load<Texture2D>(@"Sprites/Navigation/loading");
        }
        
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            ContentLoader.ContentLoader.UnloadContent();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Updater.Update(gameTime);

            //TODO: this will go to Controller
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //load the game when needed
            if (gameState == GameState.Loading && !isLoading) //isLoading bool is to prevent the LoadGame method from being called 60 times a seconds
            {
                //set backgroundthread
                backgroundThread = new Thread(LoadGame);
                isLoading = true;

                //start backgroundthread
                backgroundThread.Start();
            }

            //move the orb if the game is in progress
            if (gameState == GameState.Playing)
            {
                //move the orb
                orbPosition.X += speed;

                //prevent out of bounds
                if (orbPosition.X > (GraphicsDevice.Viewport.Width - OrbWidth) || orbPosition.X < 0)
                    speed *= -1;
            }

            //wait for mouseclick
            mouseState = Mouse.GetState();
            if (previousMouseState.LeftButton == ButtonState.Pressed &&
                mouseState.LeftButton == ButtonState.Released)
            {
                MouseClicked(mouseState.X, mouseState.Y);
            }

            previousMouseState = mouseState;

            if (gameState == GameState.Playing && isLoading)
            {
                LoadGame();
                isLoading = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //TODO: if all variables changed use this
            //View.Draw.DrawSreen(GraphicsDevice);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //draw the start menu
            if (gameState == GameState.StartMenu)
            {
                spriteBatch.Draw(startButton, startButtonPosition, Color.White);
                spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
                spriteBatch.Draw(_helyesVagyHejesStartButton, _helyesVagyHejesStartButtonPosition, null, Color.White, 0, new Vector2(), .25f, SpriteEffects.None, 0 );
            }

            //show the loading screen when needed
            if (gameState == GameState.Loading)
            {
                spriteBatch.Draw(loadingScreen,
                    new Vector2((GraphicsDevice.Viewport.Width / 2) - (loadingScreen.Width / 2),
                        (GraphicsDevice.Viewport.Height / 2) - (loadingScreen.Height / 2)), 
                    Color.YellowGreen);
            }

            //draw the the game when playing
            if (gameState == GameState.Playing)
            {
                //orb
                spriteBatch.Draw(orb, orbPosition, Color.White);

                //pause button
                spriteBatch.Draw(pauseButton, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
            }

            //draw the pause screen
            if (gameState == GameState.Paused)
                spriteBatch.Draw(resumeButton, resumeButtonPosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        
        /// <summary>
        /// Loads the orb
        /// </summary>
        protected void LoadGame()
        {
            //load the game images into the content pipeline
            orb = Content.Load<Texture2D>(@"Sprites/GameElements/orb");
            pauseButton = Content.Load<Texture2D>(@"Sprites/Navigation/pause");
            resumeButton = Content.Load<Texture2D>(@"Sprites/Navigation/resume");
            resumeButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - (resumeButton.Width / 2),
                                               (GraphicsDevice.Viewport.Height / 2) - (resumeButton.Height / 2));

            //set the position of the orb in the middle of the gamewindow
            orbPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - (OrbWidth / 2), (GraphicsDevice.Viewport.Height / 2) - (OrbHeight / 2));

            //since this will go to fast for this demo's purpose, wait for 3 seconds
            Thread.Sleep(500);

            //start playing
            gameState = GameState.Playing;
            isLoading = false;
        }

        protected void MouseClicked(int x, int y)
        {
            //creates a rectangle of 10x10 around the place where the mouse was clicked
            var mouseClickRect = new Rectangle(x, y, 10, 10);

            //check the startmenu
            if (gameState == GameState.StartMenu)
            {
                var startButtonRect = new Rectangle((int)startButtonPosition.X, (int)startButtonPosition.Y, 100, 20);
                var exitButtonRect = new Rectangle((int)exitButtonPosition.X, (int)exitButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(startButtonRect)) //player clicked start button
                {
                    gameState = GameState.Loading;
                    isLoading = false;
                }
                else if (mouseClickRect.Intersects(exitButtonRect)) //player clicked exit button
                    Exit();
            }

            //check the pausebutton
            if (gameState == GameState.Playing)
            {
                var pauseButtonRect = new Rectangle(0, 0, 70, 70);
                var exitButtonRect = new Rectangle((int)exitButtonPosition.X, (int)exitButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(pauseButtonRect))
                    gameState = GameState.Paused;

                if (mouseClickRect.Intersects(exitButtonRect)) //player clicked exit button
                    Exit();
            }

            //check the resumebutton
            if (gameState == GameState.Paused)
            {
                var resumeButtonRect = new Rectangle((int)resumeButtonPosition.X, (int)resumeButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(resumeButtonRect))
                    gameState = GameState.Playing;
            }
        }
    }
}
