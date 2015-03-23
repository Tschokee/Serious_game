using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeriousGameZ.Controller;
using SeriousGameZ.Model;

namespace SeriousGameZ.View
{
    public class Draw
    {
        public static void DrawSreen(GraphicsDevice graphicsDevice){

            graphicsDevice.Clear(Color.CornflowerBlue);

            GameSettings.SreenSettings.SpriteBatch.Begin();

            //draw the start menu
            if (GameSettings.GameState == GameState.StartMenu)
            {
                GameSettings.SreenSettings.SpriteBatch.Draw(GameSettings.Buttons.StartButton, GameSettings.ButtonPositions.StartButtonPosition, Color.White);
                GameSettings.SreenSettings.SpriteBatch.Draw(GameSettings.Buttons.ExitButton, GameSettings.ButtonPositions.ExitButtonPosition, Color.White);
                GameSettings.SreenSettings.SpriteBatch.Draw(GameSettings.Buttons.HelyesVagyHejesButton, GameSettings.ButtonPositions.HelyesVagyHejesStartButtonPosition, null, Color.White, 0, new Vector2(), .25f, SpriteEffects.None, 0);
                GameSettings.SreenSettings.SpriteBatch.Draw(GameSettings.Buttons.OsztokaButton, GameSettings.ButtonPositions.OsztokaButtonPosition, null, Color.White, 0, new Vector2(), .25f, SpriteEffects.None, 0);
            }

            //show the loading screen when needed
            if (GameSettings.GameState == GameState.Loading || GameSettings.GameState == GameState.LoadingHelyesVagyHejes)
            {
                GameSettings.SreenSettings.SpriteBatch.Draw(GameSettings.SreenSettings.LoadingScreen,
                    new Vector2((graphicsDevice.Viewport.Width / 2) - (GameSettings.SreenSettings.LoadingScreen.Width / 2),
                        (graphicsDevice.Viewport.Height / 2) - (GameSettings.SreenSettings.LoadingScreen.Height / 2)), Color.YellowGreen);
            }

            //draw the the game when playing
            switch (GameSettings.GameState)
            {
                case GameState.Playing:
                    TempGame.Draw();
                    break;
                case GameState.PlayingHelyesVagyHejes:
                    HelyesVagyHejesGame.Draw();
                    break;
            }

            GameSettings.SreenSettings.SpriteBatch.End();
        }
    }
}
