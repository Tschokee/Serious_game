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

        //TODO: these variables will be transfered to model
        private Texture2D  orb;
        private Texture2D pauseButton;
        private Texture2D resumeButton;

        private Vector2 orbPosition;
        private Vector2 resumeButtonPosition;

        private const float OrbWidth = 50f;
        private const float OrbHeight = 50f;
        private float speed = 10f;

        private Thread backgroundThread;

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
            GameModel.Initialize();
            IsMouseVisible = true;            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            ContentLoader.ContentLoader.LoadContent(GraphicsDevice, Content);
        }
        
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            ContentLoader.ContentLoader.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Updater.Update(this, gameTime, GraphicsDevice, Content);

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //load the game when needed
            if (GameSettings.GameState == GameState.Loading && !GameSettings.GameStateSettings.IsLoading) //isLoading bool is to prevent the LoadGame method from being called 60 times a seconds
            {
                //set backgroundthread
                backgroundThread = new Thread(LoadGame);
                GameSettings.GameStateSettings.IsLoading = true;

                //start backgroundthread
                backgroundThread.Start();
            }

            //move the orb if the game is in progress
            if (GameSettings.GameState == GameState.Playing)
            {
                //move the orb
                orbPosition.X += GameSettings.TempGameContent.Speed;

                //prevent out of bounds
                if (orbPosition.X > (GraphicsDevice.Viewport.Width - OrbWidth) || orbPosition.X < 0)
                    GameSettings.TempGameContent.Speed *= -1;
            }

            //wait for mouseclick
            GameSettings.MouseSettings.MouseState = Mouse.GetState();
            if (GameSettings.MouseSettings.PreviousMouseState.LeftButton == ButtonState.Pressed &&
                GameSettings.MouseSettings.MouseState.LeftButton == ButtonState.Released)
            {
                MouseClicked(GameSettings.MouseSettings.MouseState.X, GameSettings.MouseSettings.MouseState.Y);
            }

            GameSettings.MouseSettings.PreviousMouseState = GameSettings.MouseSettings.MouseState;

            if (GameSettings.GameState == GameState.Playing && GameSettings.GameStateSettings.IsLoading)
            {
                LoadGame();
                GameSettings.GameStateSettings.IsLoading = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //It's working
            View.Draw.DrawSreen(GraphicsDevice);

            GameSettings.SreenSettings.SpriteBatch.Begin();
            //draw the the game when playing
            if (GameSettings.GameState == GameState.Playing)
            {
                //orb
                GameSettings.SreenSettings.SpriteBatch.Draw(GameSettings.TempGameContent.Orb, orbPosition, Color.White);

                //pause button
                GameSettings.SreenSettings.SpriteBatch.Draw(GameSettings.TempGameContent.PauseButton, new Vector2(0, 0), Color.White);
                GameSettings.SreenSettings.SpriteBatch.Draw(GameSettings.Buttons.ExitButton, GameSettings.MainMenuSettings.ExitButtonPosition, Color.White);
            }

            //draw the pause screen
            if (GameSettings.GameState == GameState.Paused)
                GameSettings.SreenSettings.SpriteBatch.Draw(GameSettings.TempGameContent.ResumeButton, GameSettings.TempGameContent.ResumeButtonPosition, Color.White);

            GameSettings.SreenSettings.SpriteBatch.End();

            base.Draw(gameTime);
        }
        
        /// <summary>
        /// Loads the orb
        /// </summary>
        protected void LoadGame()
        {
            //load the game images into the content pipeline
            GameSettings.TempGameContent.Orb = Content.Load<Texture2D>(@"Sprites/GameElements/orb");
            GameSettings.TempGameContent.PauseButton = Content.Load<Texture2D>(@"Sprites/Navigation/pause");
            GameSettings.TempGameContent.ResumeButton = Content.Load<Texture2D>(@"Sprites/Navigation/resume");
            GameSettings.TempGameContent.ResumeButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - (GameSettings.TempGameContent.ResumeButton.Width / 2),
                                               (GraphicsDevice.Viewport.Height / 2) - (GameSettings.TempGameContent.ResumeButton.Height / 2));

            //set the position of the orb in the middle of the gamewindow
            orbPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - (OrbWidth / 2), (GraphicsDevice.Viewport.Height / 2) - (OrbHeight / 2));

            //since this will go to fast for this demo's purpose, wait for 3 seconds
            Thread.Sleep(500);

            //start playing
            GameSettings.GameState = GameState.Playing;
            GameSettings.GameStateSettings.IsLoading = false;
        }

        protected void MouseClicked(int x, int y)
        {
            //creates a rectangle of 10x10 around the place where the mouse was clicked
            var mouseClickRect = new Rectangle(x, y, 10, 10);

            //check the startmenu
            if (GameSettings.GameState == GameState.StartMenu)
            {
                var startButtonRect = new Rectangle((int)GameSettings.MainMenuSettings.StartButtonPosition.X, (int)GameSettings.MainMenuSettings.StartButtonPosition.Y, 100, 20);
                var exitButtonRect = new Rectangle((int)GameSettings.MainMenuSettings.ExitButtonPosition.X, (int)GameSettings.MainMenuSettings.ExitButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(startButtonRect)) //player clicked start button
                {
                    GameSettings.GameState = GameState.Loading;
                    GameSettings.GameStateSettings.IsLoading = false;
                }
                else if (mouseClickRect.Intersects(exitButtonRect)) //player clicked exit button
                    Exit();
            }

            //check the pausebutton
            if (GameSettings.GameState == GameState.Playing)
            {
                var pauseButtonRect = new Rectangle(0, 0, 70, 70);
                var exitButtonRect = new Rectangle((int)GameSettings.MainMenuSettings.ExitButtonPosition.X, (int)GameSettings.MainMenuSettings.ExitButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(pauseButtonRect))
                    GameSettings.GameState = GameState.Paused;

                if (mouseClickRect.Intersects(exitButtonRect)) //player clicked exit button
                    Exit();
            }

            //check the resumebutton
            if (GameSettings.GameState == GameState.Paused)
            {
                var resumeButtonRect = new Rectangle((int)GameSettings.TempGameContent.ResumeButtonPosition.X, (int)GameSettings.TempGameContent.ResumeButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(resumeButtonRect))
                    GameSettings.GameState = GameState.Playing;
            }
        }
    }
}
