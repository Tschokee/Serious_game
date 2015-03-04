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
            Updater.Update(this, gameTime, GraphicsDevice, Content);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            View.Draw.DrawSreen(GraphicsDevice);
            base.Draw(gameTime);
        }
    }
}
