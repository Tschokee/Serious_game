using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SeriousGameZ.Model
{
    public class GameSettings
    {
        public static GameState GameState { get; set; }

        public class MainMenuSettings
        {
            public static Vector2 HelyesVagyHejesStartButtonPosition { get; set; }
            public static Vector2 StartButtonPosition { get; set; }
            public static Vector2 ExitButtonPosition { get; set; }
        }

        public static class Buttons
        {
            public static Texture2D StartButton { get; set; }
            public static Texture2D ExitButton { get; set; }
            public static Texture2D HelyesVagyHejesButton { get; set; }
        }

        public static class SreenSettings
        {
            public static Texture2D LoadingScreen { get; set; }
            public static SpriteBatch SpriteBatch { get; set; }
        }

        public static class MouseSettings
        {
            public static bool IsMouseVisible { get; set; }
            public static MouseState MouseState { get; set; }
            public static MouseState PreviousMouseState { get; set; }
        }
    }
}
