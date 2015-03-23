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
    }
}
