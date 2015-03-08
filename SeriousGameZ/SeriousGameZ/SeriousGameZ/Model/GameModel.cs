using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SeriousGameZ.Model
{
    public class GameModel
    {
        public static void Initialize()
        {
            //set the position of the buttons
            GameSettings.MainMenuSettings.StartButtonPosition = new Vector2(50, 200);
            GameSettings.MainMenuSettings.ExitButtonPosition = new Vector2(50, 250);
            GameSettings.MainMenuSettings.HelyesVagyHejesStartButtonPosition = new Vector2(125, 25);
            GameSettings.MainMenuSettings.OsztokaButtonPosition = new Vector2(225, 25);

            //set the gamestate to start menu
            GameSettings.GameState = GameState.StartMenu;

            //get the mouse state
            GameSettings.MouseSettings.MouseState = Mouse.GetState();
            GameSettings.MouseSettings.PreviousMouseState = GameSettings.MouseSettings.MouseState;

            GameSettings.GameStateSettings.IsLoading = false;
        }
    }
}
