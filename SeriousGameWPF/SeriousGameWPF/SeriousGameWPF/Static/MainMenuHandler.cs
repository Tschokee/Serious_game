using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriousGameWPF.Static
{
    public static class MainMenuHandler
    {
        static double _xSize;
        static double _ySize;

        private static ObservableCollection<Game> _gamesList { get; set; }

        public static ObservableCollection<Game> GamesList
        {
            get
            {
                return _gamesList;
            }
        }
        static MainMenuHandler()
        {
            _xSize = 420;
            _ySize = 220;
            _gamesList = new ObservableCollection<Game>();
        }
        public static void AddGame(Game game)
        {
            _gamesList.Add(game);

        }
        public static ObservableCollection<Game> GetGamesList()
        {
            return _gamesList;

        }
        public static double[] CalculatePositionForAllGamesIn(MainWindow mainWindow)
        {
            double currentPosX = 0;
            double currentPosY = 20;
            int db = (int)(mainWindow.ActualWidth / _xSize);
            double extraspace = ((mainWindow.ActualWidth - (_xSize * db)) / db) / 2;
            currentPosX += extraspace ;
            double thisxSize=_xSize+ (mainWindow.ActualWidth-(_xSize*db))/db;
            double canvash = 0;
            double canvasw = 0;
            foreach (Game game in _gamesList)
            {
                game.PosX = currentPosX;
                game.PosY = currentPosY;
                currentPosX += thisxSize;
                if (mainWindow.ActualWidth < currentPosX + _xSize)
                {
                    currentPosX = 0+ extraspace;
                    currentPosY += _ySize;
                    canvash = currentPosY;
                }
            }
            canvash += _ySize;
            double[] data = new double[2] { canvash, canvasw };

            return data;

        }
        #region Staticpropertymembers
        public static event EventHandler CanvasHeightPropertyChanged;
        public static void RaiseCanvasHeightPropertyChanged()
        {
            EventHandler handler = CanvasHeightPropertyChanged;
            if (handler != null)
                handler(null, EventArgs.Empty);
        }
        public static event EventHandler CanvasWidthPropertyChanged;
        public static void RaiseCanvasWidthPropertyChanged()
        {
            EventHandler handler = CanvasWidthPropertyChanged;
            if (handler != null)
                handler(null, EventArgs.Empty);
        }

        #endregion
    }
}
