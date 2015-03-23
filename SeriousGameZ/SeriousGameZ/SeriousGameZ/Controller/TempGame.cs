using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeriousGameZ.Model;

namespace SeriousGameZ.Controller
{
    public class TempGame : GameLogic
    {
        public static void Play(GraphicsDevice graphicsDevice)
        {
            const float orbWidth = 50f;
            //move the orb
            GameSettings.TempGameContent.OrbPosition += new Vector2(GameSettings.TempGameContent.Speed, 0);

            //prevent out of bounds
            if (GameSettings.TempGameContent.OrbPosition.X > (graphicsDevice.Viewport.Width - orbWidth) || GameSettings.TempGameContent.OrbPosition.X < 0)
                GameSettings.TempGameContent.Speed *= -1;
        }

        public static void Draw()
        {
            GameSettings.SreenSettings.SpriteBatch.Draw(GameSettings.TempGameContent.Orb, GameSettings.TempGameContent.OrbPosition, Color.White);
            GameSettings.SreenSettings.SpriteBatch.Draw(GameSettings.Buttons.ReturnButton, GameSettings.ButtonPositions.ReturnButtonPosition, Color.White);
            GameSettings.SreenSettings.SpriteBatch.Draw(GameSettings.Buttons.ExitButton, GameSettings.ButtonPositions.ExitButtonPosition, Color.White);
        }
    }
}
