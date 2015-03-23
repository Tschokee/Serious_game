using System;
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
                        //set backgroundthread
                        GameSettings.ThreadSettings.TmpGameBackgroundThread = thread;
                        GameSettings.GameStateSettings.TempGameIsLoading = true;
                        //start backgroundthread
                        GameSettings.ThreadSettings.TmpGameBackgroundThread.Start();
                    }
                    break;
                }
                case "HelyesVagyHejes":
                {
                    if (IsLoadingState(GameState.LoadingHelyesVagyHejes, GameSettings.GameStateSettings.HelyesVagyHejesIsLoading))
                    {
                        GameSettings.ThreadSettings.HelyesVagyHejesBackgroundThread = thread;
                        GameSettings.GameStateSettings.HelyesVagyHejesIsLoading = true;
                        GameSettings.ThreadSettings.HelyesVagyHejesBackgroundThread.Start();
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
