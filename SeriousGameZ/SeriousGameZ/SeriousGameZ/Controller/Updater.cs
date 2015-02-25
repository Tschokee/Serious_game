using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SeriousGameZ.Model;

namespace SeriousGameZ.Controller
{
    public class Updater
    {
        private static Vector2 orbPosition = new Vector2(0, 0);
        private const float orbWidth = 50f;
        private const float orbHeight = 50f;
        private static float speed = 10f;
        private static Texture2D orb;
        private static Texture2D pauseButton;
        private static Texture2D resumeButton;
        private static Vector2 resumeButtonPosition;
        //private GraphicsDevice graphicsDevice; //not sure this will work, take care
        //private ContentManager contentManager;
        private Game game; 

        public static void Update(GameTime gameTime, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //load the game when needed
            if (GameSettings.GameState == GameState.Loading && !GameSettings.GameStateSettings.IsLoading) //isLoading bool is to prevent the LoadGame method from being called 60 times a seconds
            {
                //TODO implement
                //set backgroundthread
                GameSettings.ThreadSettings.BackgroundThread = new Thread(() => LoadGame(graphicsDevice, contentManager));
                GameSettings.GameStateSettings.IsLoading = true;

                //start backgroundthread
                GameSettings.ThreadSettings.BackgroundThread.Start();
            }

            //move the orb if the game is in progress
            if (GameSettings.GameState == GameState.Playing)
            {
                ////move the orb
                orbPosition.X += speed;

                ////prevent out of bounds
                if (orbPosition.X > (graphicsDevice.Viewport.Width - orbWidth) || orbPosition.X < 0)
                    speed *= -1;
            }

            //wait for mouseclick
            GameSettings.MouseSettings.MouseState = Mouse.GetState();
            if (GameSettings.MouseSettings.PreviousMouseState.LeftButton == ButtonState.Pressed &&
                GameSettings.MouseSettings.MouseState.LeftButton == ButtonState.Released)
            {
                //MouseClicked(mouseState.X, mouseState.Y);
            }

            GameSettings.MouseSettings.PreviousMouseState = GameSettings.MouseSettings.MouseState;

            if (GameSettings.GameState == GameState.Playing && GameSettings.GameStateSettings.IsLoading)
            {
                //LoadGame();
                GameSettings.GameStateSettings.IsLoading = false;
            }
        }

        /// <summary>
        /// Loads the orb
        /// </summary>
        public static void LoadGame(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //load the game images into the content pipeline
            orb = contentManager.Load<Texture2D>(@"Sprites/GameElements/orb");
            pauseButton = contentManager.Load<Texture2D>(@"Sprites/Navigation/pause");
            resumeButton = contentManager.Load<Texture2D>(@"Sprites/Navigation/resume");
            resumeButtonPosition = new Vector2((graphicsDevice.Viewport.Width / 2) - (resumeButton.Width / 2),
                                               (graphicsDevice.Viewport.Height / 2) - (resumeButton.Height / 2));

            //set the position of the orb in the middle of the gamewindow
            orbPosition = new Vector2((graphicsDevice.Viewport.Width / 2) - (orbWidth / 2), (graphicsDevice.Viewport.Height / 2) - (orbHeight / 2));

            //since this will go to fast for this demo's purpose, wait for 3 seconds
            Thread.Sleep(500);

            //start playing
            GameSettings.GameState = GameState.Playing;
            GameSettings.GameStateSettings.IsLoading = false;
        }

        public void MouseClicked(int x, int y)
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
                    game.Exit();
            }

            //check the pausebutton
            if (GameSettings.GameState == GameState.Playing)
            {
                var pauseButtonRect = new Rectangle(0, 0, 70, 70);
                var exitButtonRect = new Rectangle((int)GameSettings.MainMenuSettings.ExitButtonPosition.X, (int)GameSettings.MainMenuSettings.ExitButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(pauseButtonRect))
                    GameSettings.GameState = GameState.Paused;

                if (mouseClickRect.Intersects(exitButtonRect)) //player clicked exit button
                    game.Exit();
            }

            //check the resumebutton
            if (GameSettings.GameState == GameState.Paused)
            {
                var resumeButtonRect = new Rectangle((int)resumeButtonPosition.X, (int)resumeButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(resumeButtonRect))
                    GameSettings.GameState = GameState.Playing;
            }
        }
    }
}
