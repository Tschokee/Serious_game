using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeriousGameZ.Model;
using SeriousGameZ.Model.Interface;

namespace SeriousGameZ.Controller
{
    public class HelyesVagyHejesGame : GameLogic
    {
        public static void Play(GraphicsDevice graphicsDevice)
        {
            const float orbWidth = 50f;
            //move the orb
            GameSettings.TempGameContent.OrbPosition += new Vector2(0, GameSettings.TempGameContent.Speed);

            //prevent out of bounds
            if (GameSettings.TempGameContent.OrbPosition.Y > (graphicsDevice.Viewport.Height - orbWidth)
                || GameSettings.TempGameContent.OrbPosition.Y < 0)
                GameSettings.TempGameContent.Speed *= -1;
        }
    }
}
