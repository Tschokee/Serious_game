using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeriousGameZ.Model;

namespace SeriousGameZ.View
{
    public class Draw
    {
        public static void DrawSreen(GraphicsDevice graphicsDevice){
            graphicsDevice.Clear(Color.CornflowerBlue);

            GameSettings.SreenSettings.SpriteBatch.Begin();
            //TODO: use global variables everywhere like startButton -> GameSettings.Buttons.StartButton
            ////draw the start menu
            //if (GameSettings.GameState == GameState.StartMenu)
            //{
            //    GameSettings.SreenSettings.SpriteBatch.Draw(GameSettings.Buttons.StartButton, startButtonPosition, Color.White);
            //    GameSettings.SreenSettings.SpriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
            //    GameSettings.SreenSettings.SpriteBatch.Draw(_helyesVagyHejesStartButton, _helyesVagyHejesStartButtonPosition, null, Color.White, 0, new Vector2(), .25f, SpriteEffects.None, 0);
            //}

            ////show the loading screen when needed
            //if (GameSettings.GameState == GameState.Loading)
            //{
            //    GameSettings.SreenSettings.SpriteBatch.Draw(loadingScreen,
            //        new Vector2((graphicsDevice.Viewport.Width / 2) - (loadingScreen.Width / 2),
            //            (graphicsDevice.Viewport.Height / 2) - (loadingScreen.Height / 2)), 
            //        Color.YellowGreen);
            //}

            ////draw the the game when playing
            //if (GameSettings.GameState == GameState.Playing)
            //{
            //    //orb
            //    GameSettings.SreenSettings.SpriteBatch.Draw(orb, orbPosition, Color.White);

            //    //pause button
            //    GameSettings.SreenSettings.SpriteBatch.Draw(pauseButton, new Vector2(0, 0), Color.White);
            //    GameSettings.SreenSettings.SpriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
            //}

            ////draw the pause screen
            //if (GameSettings.GameState == GameState.Paused)
            //    GameSettings.SreenSettings.SpriteBatch.Draw(resumeButton, resumeButtonPosition, Color.White);

            GameSettings.SreenSettings.SpriteBatch.End();
        }
    }
}
