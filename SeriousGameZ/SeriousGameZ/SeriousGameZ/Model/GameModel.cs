using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SeriousGameZ.Model
{
    public class GameModel
    {
        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            //set the position of the buttons
            GameSettings.ButtonPositions.TempGameStartButtonPosition = new Vector2(50, 200);
            GameSettings.ButtonPositions.ExitButtonPosition = new Vector2(graphicsDevice.Viewport.Width - 100, 100);
            GameSettings.ButtonPositions.HelyesVagyHejesStartButtonPosition = new Vector2(125, 25);
            GameSettings.ButtonPositions.OsztokaButtonPosition = new Vector2(325, 25);
            GameSettings.ButtonPositions.ReturnButtonPosition = new Vector2(graphicsDevice.Viewport.Width-100, 0);

            //set the gamestate to start menu
            GameSettings.GameState = GameState.StartMenu;

            //get the mouse state
            GameSettings.MouseSettings.MouseState = Mouse.GetState();
            GameSettings.MouseSettings.PreviousMouseState = GameSettings.MouseSettings.MouseState;

            GameSettings.GameStateSettings.TempGameIsLoading = false;
        }
    }
}
