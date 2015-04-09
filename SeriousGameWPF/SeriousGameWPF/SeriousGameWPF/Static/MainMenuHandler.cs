using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SeriousGameWPF.Static
{
    public delegate void ChangeScreen(string page);
    public static class MainMenuHandler
    {
        
       
        public static object DataContext;
        public static ChangeScreen ChangeScreenTo;
        public static Game SelectedGame;
        private static ObservableCollection<Game> _gamesList { get; set; }
        private static ObservableCollection<OMenuItem> _menu { get; set; }
        public static ObservableCollection<Game> GamesList
        {
            get
            {
                return _gamesList;
            }
        }
        public static ObservableCollection<OMenuItem> Menu
        {
            get
            {
                return _menu;
            }
        }
        static MainMenuHandler()
        {
            _menu = new ObservableCollection<OMenuItem>();
            _gamesList = new ObservableCollection<Game>();
        }
        public static void AddMenuItem(OMenuItem game)
        {
            _menu.Add(game);

        }
        public static void AddGame(Game game)
        {
            _gamesList.Add(game);

        }
        public static ObservableCollection<Game> GetGamesList()
        {
            return _gamesList;

        }
        
        public static double[] CalculatePositionFor<T>(ObservableCollection<T> stuff,MainWindow mainWindow, bool isHorizontal) where T:Displayable
        {
            double[] data;
            double _ySize = -1;
            double _xSize= -1;
            if (typeof(T)==typeof(Game))
            {
                _ySize = Game._ySize+20;
                _xSize = Game._xSize+20;

            }
            else if(typeof(T) == typeof(GameMode))
            {
                _ySize = GameMode._ySize+20;
                _xSize = GameMode._xSize+20;

            }
            if (isHorizontal)
            {
                double currentPosY = 0;
                double currentPosX = 20;
                double windowHeight = mainWindow.ActualHeight-23;
                if (windowHeight<0)
                {
                    windowHeight = 1;
                    
                }

                int db = (int)(windowHeight / _ySize);
                double extraspace = ((((windowHeight) - (_ySize * db)) / db) / 2);
                currentPosY += extraspace;
                double thisySize = _ySize + (windowHeight - (_ySize * db)) / db;
                double canvash = 0;
                double canvasw = 0;
                foreach (Displayable game in stuff)
                {
                    game.PosX = currentPosX;
                    game.PosY = currentPosY;
                    currentPosY += thisySize;
                    //currentPosX += _xSize;
                    if ((windowHeight < currentPosY + _ySize) && isHorizontal)
                    {
                        currentPosY = 0 + extraspace;
                        currentPosX += _xSize;
                        canvasw = currentPosX;
                    }
                }
                if (db != 0)
                {
                    if (_gamesList.Count % db > 0)
                    {
                        canvasw += _xSize;
                    }
                }

                data = new double[2] { canvash, canvasw };

            }
            else
            {
                double currentPosX = 20;
                double currentPosY = 0;
                int db = (int)(mainWindow.ActualWidth / _xSize);
                double extraspace = (((mainWindow.ActualWidth - (_xSize * db)) / db) / 2);
                currentPosX += extraspace;
                double thisxSize = _xSize + (mainWindow.ActualWidth - (_xSize * db)) / db;
                double canvash = 0;
                double canvasw = 0;
                foreach (Displayable game in stuff)
                {
                    game.PosX = currentPosX;
                    game.PosY = currentPosY;
                    currentPosX += thisxSize;
                    if ((mainWindow.ActualWidth < currentPosX + _xSize) && !isHorizontal)
                    {
                        currentPosX = 0 + extraspace;
                        currentPosY += _ySize;
                        canvash = currentPosY;
                    }
                }
                if (db != 0)
                {
                    if (_gamesList.Count % db > 0)
                    {
                        canvash += _ySize;
                    }
                }
                data = new double[2] { canvash, canvasw };


            }
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
