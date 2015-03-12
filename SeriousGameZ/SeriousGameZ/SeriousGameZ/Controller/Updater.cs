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
        private const float orbWidth = 50f;
        private const float orbHeight = 50f;

        public static void Update(Game1 game, GameTime gameTime, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //load the game when needed
            if (GameSettings.GameState == GameState.Loading && !GameSettings.GameStateSettings.IsLoading) //isLoading bool is to prevent the LoadGame method from being called 60 times a seconds
            {
                //set backgroundthread
                GameSettings.ThreadSettings.TmpGameBackgroundThread = new Thread(() => LoadGame(graphicsDevice, contentManager));
                GameSettings.GameStateSettings.IsLoading = true;

                //start backgroundthread
                GameSettings.ThreadSettings.TmpGameBackgroundThread.Start();
            }

            //move the orb if the game is in progress
            if (GameSettings.GameState == GameState.Playing)
            {
                //move the orb
                GameSettings.TempGameContent.OrbPosition += new Vector2(GameSettings.TempGameContent.Speed, 0); 

                //prevent out of bounds
                if (GameSettings.TempGameContent.OrbPosition.X > (graphicsDevice.Viewport.Width - orbWidth) || GameSettings.TempGameContent.OrbPosition.X < 0)
                    GameSettings.TempGameContent.Speed *= -1;
            }

            //wait for mouseclick
            GameSettings.MouseSettings.MouseState = Mouse.GetState();
            if (GameSettings.MouseSettings.PreviousMouseState.LeftButton == ButtonState.Pressed &&
                GameSettings.MouseSettings.MouseState.LeftButton == ButtonState.Released)
            {
                MouseClicked(game, GameSettings.MouseSettings.MouseState.X, GameSettings.MouseSettings.MouseState.Y);
            }

            GameSettings.MouseSettings.PreviousMouseState = GameSettings.MouseSettings.MouseState;

            if (GameSettings.GameState == GameState.Playing && GameSettings.GameStateSettings.IsLoading)
            {
                LoadGame(graphicsDevice, contentManager);
                GameSettings.GameStateSettings.IsLoading = false;
            }
        }

        /// <summary>
        /// Loads the orb
        /// </summary>
        public static void LoadGame(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //load the game images into the content pipeline
            GameSettings.TempGameContent.Orb = contentManager.Load<Texture2D>(@"Sprites/GameElements/orb");
            GameSettings.TempGameContent.PauseButton = contentManager.Load<Texture2D>(@"Sprites/Navigation/pause");

            //set the position of the orb in the middle of the gamewindow
            GameSettings.TempGameContent.OrbPosition = new Vector2((graphicsDevice.Viewport.Width / 2) - (orbWidth / 2), (graphicsDevice.Viewport.Height / 2) - (orbHeight / 2));

            //since this will go to fast for this demo's purpose, wait for 3 seconds
            Thread.Sleep(100);

            //start playing
            GameSettings.GameState = GameState.Playing;
            GameSettings.GameStateSettings.IsLoading = false;
        }

        /// <summary>
        /// Loads the orb
        /// </summary>
        public static void LoadHelyesVagyHejesGame(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //load the game images into the content pipeline
            GameSettings.TempGameContent.Orb = contentManager.Load<Texture2D>(@"Sprites/GameElements/orb");
            GameSettings.TempGameContent.PauseButton = contentManager.Load<Texture2D>(@"Sprites/Navigation/pause");

            //set the position of the orb in the middle of the gamewindow
            GameSettings.TempGameContent.OrbPosition = new Vector2((graphicsDevice.Viewport.Width / 2) - (orbWidth / 2), (graphicsDevice.Viewport.Height / 2) - (orbHeight / 2));

            //since this will go to fast for this demo's purpose, wait for 3 seconds
            Thread.Sleep(100);

            //start playing
            GameSettings.GameState = GameState.PlayingHelyesVagyHejes;
            //GameSettings.GameStateSettings.IsLoading = false;
        }

        public static void MouseClicked(Game1 game, int x, int y)
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
                    //GameSettings.GameState = GameState.Playing;
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
                    GameSettings.GameState = GameState.StartMenu;

                if (mouseClickRect.Intersects(exitButtonRect)) //player clicked exit button
                    game.Exit();
            }
        }
    }
}
