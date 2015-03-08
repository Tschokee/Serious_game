using Microsoft.Xna.Framework.Graphics;
using SeriousGameZ.Model;
using Microsoft.Xna.Framework.Content;

namespace SeriousGameZ.ContentLoader
{
    public class ContentLoader
    {
        public static void LoadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            GameSettings.SreenSettings.SpriteBatch = new SpriteBatch(graphicsDevice);

            //load the buttonimages into the content pipeline
            GameSettings.Buttons.StartButton= contentManager.Load<Texture2D>(@"Sprites/Navigation/start");
            GameSettings.Buttons.ExitButton = contentManager.Load<Texture2D>(@"Sprites/Navigation/exit");
            GameSettings.Buttons.HelyesVagyHejesButton = contentManager.Load<Texture2D>(@"Sprites/Navigation/helyes_vagy_hejes");
            GameSettings.Buttons.OsztokaButton = contentManager.Load<Texture2D>(@"Sprites/Navigation/osztoka");

            //load the loading screen
            GameSettings.SreenSettings.LoadingScreen = contentManager.Load<Texture2D>(@"Sprites/Navigation/loading");
        }

        public static void UnloadContent()
        {
        }
    }
}
