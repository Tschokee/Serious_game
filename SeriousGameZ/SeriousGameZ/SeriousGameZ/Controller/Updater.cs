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
            GameLogic.ThreadHandling("TempGame", new Thread(() => LoadGame(graphicsDevice, contentManager)));
            GameLogic.ThreadHandling("HelyesVagyHejes", new Thread(() => LoadHelyesVagyHejesGame(graphicsDevice, contentManager)));
            
            switch (GameSettings.GameState)
            {
                case GameState.PlayingTempGame:
                    TempGame.Play(graphicsDevice);
                    break;
                case GameState.PlayingHelyesVagyHejes:
                    HelyesVagyHejesGame.Play(graphicsDevice);
                    break;
            }

            //wait for mouseclick
            GameSettings.MouseSettings.MouseState = Mouse.GetState();
            if (GameSettings.MouseSettings.PreviousMouseState.LeftButton == ButtonState.Pressed &&
                GameSettings.MouseSettings.MouseState.LeftButton == ButtonState.Released)
            {
                MouseClicked(game, GameSettings.MouseSettings.MouseState.X, GameSettings.MouseSettings.MouseState.Y);
            }

            GameSettings.MouseSettings.PreviousMouseState = GameSettings.MouseSettings.MouseState;

            if (RecentlyStartedPlaying(GameState.PlayingTempGame, GameSettings.GameStateSettings.TempGameIsLoading))
            {
                LoadGame(graphicsDevice, contentManager);
                GameSettings.GameStateSettings.TempGameIsLoading = false;
            }
            if (RecentlyStartedPlaying(GameState.PlayingHelyesVagyHejes, GameSettings.GameStateSettings.HelyesVagyHejesIsLoading))
            {
                LoadHelyesVagyHejesGame(graphicsDevice, contentManager);
                GameSettings.GameStateSettings.HelyesVagyHejesIsLoading = false;
            }
        }

        private static bool RecentlyStartedPlaying(GameState state, bool isLoading)
        {
            return GameSettings.GameState == state && isLoading;
        }

        /// <summary>
        /// Loads the orb
        /// </summary>
        public static void LoadGame(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //load the game images into the content pipeline
            GameSettings.TempGameContent.Orb = contentManager.Load<Texture2D>(@"Sprites/GameElements/orb");
            GameSettings.Buttons.ReturnButton = contentManager.Load<Texture2D>(@"Sprites/Navigation/pause");

            //set the position of the orb in the middle of the gamewindow
            GameSettings.TempGameContent.OrbPosition = new Vector2((graphicsDevice.Viewport.Width / 2) - (orbWidth / 2), (graphicsDevice.Viewport.Height / 2) - (orbHeight / 2));

            //since this will go to fast for this demo's purpose, wait for 3 seconds
            Thread.Sleep(100);

            //start playing
            GameSettings.GameState = GameState.PlayingTempGame;
            GameSettings.GameStateSettings.TempGameIsLoading = false;
        }

        /// <summary>
        /// Loads the orb
        /// </summary>
        public static void LoadHelyesVagyHejesGame(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            GameSettings.TempGameContent.Orb = contentManager.Load<Texture2D>(@"Sprites/GameElements/orb");
            GameSettings.Buttons.ReturnButton = contentManager.Load<Texture2D>(@"Sprites/Navigation/pause");
            GameSettings.TempGameContent.OrbPosition = new Vector2((graphicsDevice.Viewport.Width / 2) - (orbWidth / 2), (graphicsDevice.Viewport.Height / 2) - (orbHeight / 2));
            Thread.Sleep(100);
            GameSettings.GameState = GameState.PlayingHelyesVagyHejes;
            GameSettings.GameStateSettings.HelyesVagyHejesIsLoading = false;
        }

        public static void MouseClicked(Game1 game, int x, int y)
        {
            //creates a rectangle of 10x10 around the place where the mouse was clicked
            var mouseClickRect = new Rectangle(x, y, 10, 10);

            //check the startmenu
            if (GameSettings.GameState == GameState.StartMenu)
            {
                var startButtonRect = new Rectangle((int)GameSettings.ButtonPositions.TempGameStartButtonPosition.X, (int)GameSettings.ButtonPositions.TempGameStartButtonPosition.Y, 100, 20);
                var helyesVagyHejesButtonRect = new Rectangle((int)GameSettings.ButtonPositions.HelyesVagyHejesStartButtonPosition.X, (int)GameSettings.ButtonPositions.HelyesVagyHejesStartButtonPosition.Y, 200, 100);

                var exitButtonRect = new Rectangle((int)GameSettings.ButtonPositions.ExitButtonPosition.X, (int)GameSettings.ButtonPositions.ExitButtonPosition.Y, 100, 20);
                
                if (mouseClickRect.Intersects(startButtonRect)) 
                    SetGameState("TempGame");                    
                if (mouseClickRect.Intersects(helyesVagyHejesButtonRect)) 
                    SetGameState("HelyesVagyHejes");    
                if (mouseClickRect.Intersects(exitButtonRect)) //player clicked exit button
                    game.Exit();
            }

            //check the return button
            if (GameSettings.GameState == GameState.PlayingTempGame || GameSettings.GameState == GameState.PlayingHelyesVagyHejes)
            {
                var pauseButtonRect = new Rectangle((int)GameSettings.ButtonPositions.ReturnButtonPosition.X, (int)GameSettings.ButtonPositions.ReturnButtonPosition.Y, 70, 70);
                var exitButtonRect = new Rectangle((int)GameSettings.ButtonPositions.ExitButtonPosition.X, (int)GameSettings.ButtonPositions.ExitButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(pauseButtonRect))
                    GameSettings.GameState = GameState.StartMenu;

                if (mouseClickRect.Intersects(exitButtonRect)) //player clicked exit button
                    game.Exit();
            }
        }

        private static void SetGameState(string gameName)
        {
            switch (gameName)
            {
                case "TempGame":
                {
                    GameSettings.GameState = GameState.LoadingTempGame;
                    GameSettings.GameStateSettings.TempGameIsLoading = false;
                    break;
                }
                case "HelyesVagyHejes":
                {
                    GameSettings.GameState = GameState.LoadingHelyesVagyHejes;
                    GameSettings.GameStateSettings.HelyesVagyHejesIsLoading = false;
                    break;
                }
            }
        }
    }
}
