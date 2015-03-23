using System.Threading;
using SeriousGameZ.Model;
using SeriousGameZ.Model.Interface;

namespace SeriousGameZ.Controller
{
    public class GameLogic : IGameLogic
    {
        #region Thread
        public static void ThreadHandling(string gameName, Thread thread)
        {
            switch (gameName)
            {
                case "TempGame":
                {
                    if (IsLoadingState(GameState.LoadingTempGame, GameSettings.GameStateSettings.TempGameIsLoading)) //isLoading bool is to prevent the LoadGame method from being called 60 times a seconds
                    {
                        GameSettings.ThreadSettings.BackgroundThread = thread;
                        GameSettings.GameStateSettings.TempGameIsLoading = true;
                        GameSettings.ThreadSettings.BackgroundThread.Start();
                    }
                    break;
                }
                case "HelyesVagyHejes":
                {
                    if (IsLoadingState(GameState.LoadingHelyesVagyHejes, GameSettings.GameStateSettings.HelyesVagyHejesIsLoading))
                    {
                        GameSettings.ThreadSettings.BackgroundThread = thread;
                        GameSettings.GameStateSettings.HelyesVagyHejesIsLoading = true;
                        GameSettings.ThreadSettings.BackgroundThread.Start();
                    }
                    break;
                }
                case "Osztoka":
                {
                    if (IsLoadingState(GameState.LoadingOsztoka, GameSettings.GameStateSettings.OsztokaIsLoading))
                    {
                        GameSettings.ThreadSettings.BackgroundThread = thread;
                        GameSettings.GameStateSettings.OsztokaIsLoading = true;
                        GameSettings.ThreadSettings.BackgroundThread.Start();
                    }
                    break;
                }
            }
        }

        private static bool IsLoadingState(GameState state, bool isLoading)
        {
            return GameSettings.GameState == state && !isLoading;
        }
        #endregion
    }
}
