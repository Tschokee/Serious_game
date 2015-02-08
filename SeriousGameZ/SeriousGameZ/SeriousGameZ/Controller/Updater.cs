using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SeriousGameZ.Model;

namespace SeriousGameZ.Controller
{
    public class Updater
    {
        public static void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            //load the game when needed
            if (GameSettings.GameState == GameState.Loading && !GameSettings.GameStateSettings.IsLoading) //isLoading bool is to prevent the LoadGame method from being called 60 times a seconds
            {
                //TODO implement
                //set backgroundthread
                //GameSettings.ThreadSettings.BackgroundThread = new Thread(LoadGame);
                GameSettings.GameStateSettings.IsLoading = true;

                //start backgroundthread
                GameSettings.ThreadSettings.BackgroundThread.Start();
            }

            //move the orb if the game is in progress
            if (GameSettings.GameState == GameState.Playing)
            {
                ////move the orb
                //GameSettings.SampleGameSettings.OrbPosition.X += speed;

                ////prevent out of bounds
                //if (GameSettings.SampleGameSettings.OrbPosition.X > (graphicsDevice.Viewport.Width - OrbWidth) || GameSettings.SampleGameSettings.OrbPosition.X < 0)
                //    speed *= -1;
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
    }
}
