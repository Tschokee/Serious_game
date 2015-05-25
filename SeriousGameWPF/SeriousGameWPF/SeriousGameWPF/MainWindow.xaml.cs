﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using SeriousGameWPF.Static;
using SeriousGameWPF.Games;

namespace SeriousGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Game osztoka, helyes; // nem biztos hogy kell

        private bool isHorizontal = true;


        public MainWindow()
        {
            PirateVersion(); //if this is false: Message - Valószínűleg szoftverhamisítás áldozata lettél 
            MainMenuHandler.DataContext = this;
            InitializeComponent();
            InitGames();
            InitWindow();
        }

        private bool PirateVersion()
        {
            var detector = new PirateDetector();
            return detector.ReadAuthenticationFile();
        }

        private void InitWindow()
        {

            this.DataContext = MainMenuHandler.DataContext;
            WindowCenterX = this.ActualWidth / 2;
            WindowCenterY = this.ActualHeight / 2;
            MainMenuHandler.ChangeScreenTo = this.ChangeScreenTo;
            MainMenuHandler.ChangeScreenTo("MainMenu.xaml");

        }
        private void InitGames()
        {
            //TODO Itt hívd meg a definiált metódust
            InitOsztoka();
            InitHelyes();
            InitSzorzoka();
            //  InitDummyGames(); // ezt csak teszt, ki kell majd venni
            MainMenuHandler.CalculatePositionFor(MainMenuHandler.GamesList, MainMenuHandler.DataContext as MainWindow, isHorizontal);
        }
        #region LogicRegion
        #region GameMenuInits
        //TODO Itt hozd létre a metódust
        private void InitOsztoka()
        {
            osztoka = new Osztoka(StartGame);
            
            
            
            MainMenuHandler.AddGame(osztoka);

        }
        private void InitSzorzoka()
        {
            MainMenuHandler.AddGame(new Game
            {
                ImageUri = MainMenuHandler.ConvertStringToImageSource("/Images/szorzoka.png"),
                Name = "Szorzóka",
                
            });


        }
        private void InitHelyes()
        {
            helyes = new Game
            {
                ImageUri = MainMenuHandler.ConvertStringToImageSource("/Images/helyes_vagy_hejes.jpg"),
                Name = "Helyes vagy hejes",

            };
            MainMenuHandler.AddGame(helyes);
        }
        private void InitDummyGames()
        {
            // MainMenuHandler.AddGame(osztoka);
            for (int i = 0; i < 20; i++)
            {
                MainMenuHandler.AddGame(new Game
                {
                    ImageUri = MainMenuHandler.ConvertStringToImageSource("/Images/osztoka.jpg"),
                    Name = "Osztóka",
                    Start = StartGame
                });
            }
        }
        #endregion
        #region StartGameMethods
        public void StartGame(GameMode gm)
        {
            ChangeScreenTo(gm);
        }
       
        #endregion
        #endregion
        #region VisualRegion
        #region StaticMethods
      
        #endregion
        #region WPFEventHandlers

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void myimg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var img = sender as Image;
            var gametostart = img.DataContext as Game;

            if (gametostart.Start != null)
            {
              
            }
            else
            {
                MessageBox.Show("A játék nem indítható.");
            }
        }

        private void Viewbox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var vb = sender as Viewbox;
            var gametoview = vb.DataContext as Game;


        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FixView();


        }
        public void FixView()
        {
            double[] canvasData = MainMenuHandler.CalculatePositionFor(MainMenuHandler.GamesList, this, isHorizontal); ;
            // var canvasData = MainMenuHandler.CalculatePositionForAllGamesIn(this,isHorizontal);
            if (DisplayPage == "MainWindow.xaml")
            {
                canvasData = MainMenuHandler.CalculatePositionFor(MainMenuHandler.GamesList, MainMenuHandler.DataContext as MainWindow, isHorizontal);
            }
            else if (DisplayPage == "GameStartPage.xaml")
            {
                canvasData = MainMenuHandler.CalculatePositionFor(MainMenuHandler.SelectedGame.GameModes, MainMenuHandler.DataContext as MainWindow, isHorizontal);
            }




            CanvasHeight = canvasData[0];
            CanvasWidth = canvasData[1];
            WindowCenterY = this.ActualWidth / 2;
            WindowCenterX = this.ActualHeight / 2;
            ScrollViewerForCanvas.UpdateLayout();

        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            FixView();
        }
        private void mainWindow_Closing(object sender, CancelEventArgs e)
        {

        }
        #endregion
        #region WPFProperties
        double _windowCenterX;
        double _windowCenterY;
        double _canvasHeight = 200;
        double _canvasWidth = 200;
        private string displayPage;
        public bool IsBackEnabled
        {
            get
            {
                if (DisplayPage == "MainMenu.xaml")
                {
                    return false;
                }
                return true;
            }
        }
        public string DisplayPage
        {
            get
            {
                return displayPage;
            }
            set
            {
                if (displayPage == value)
                {
                    return;
                }

                this.displayPage = value;
                OnPropertyChanged("DisplayPage");
                OnPropertyChanged("IsBackEnabled");
            }
        }
        public double WindowCenterX
        {
            get
            {
                return _windowCenterX;
            }
            set
            {
                _windowCenterX = value;
                OnPropertyChanged("WindowCenterX");
            }
        }
        public double WindowCenterY
        {
            get
            {
                return _windowCenterY;
            }
            set
            {
                _windowCenterY = value;
                OnPropertyChanged("WindowCenterY");
            }
        }
        public double CanvasHeight
        {
            get
            {
                return MainMenuHandler._canvasHeight;
            }
            set
            {
                MainMenuHandler._canvasHeight = value;
                OnPropertyChanged("CanvasHeight");
            }
        }
        public double CanvasWidth
        {
            get
            {
                return MainMenuHandler._canvasWidth;
            }
            set
            {
                MainMenuHandler._canvasWidth = value;
                OnPropertyChanged("CanvasWidth");
            }
        }

        #endregion
        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        #region Delegates
        public async void ChangeScreenTo(string page)
        {
            
            FadeFrameOut();
            await Task.Delay(1000);
            DisplayPage = page;
            FixView();
            FadeFrameIn();
            await Task.Delay(1000);
        }
        public async void ChangeScreenTo(GameMode gm)
        {

            FadeFrameOut();
            await Task.Delay(1000);
            frame.Navigate(new PlayGame(MainMenuHandler.SelectedGame, gm, this));
            FixView();
            FadeFrameIn();
            await Task.Delay(1000);
        }
        public void FadeFrameOut()
        {
            (this.Resources["FadeOut"] as Storyboard).Begin();


        }
        public void FadeFrameIn()
        {
            (this.Resources["FadeIn"] as Storyboard).Begin();


        }
        #endregion

        private void menuItem_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Biztos ki akarsz lépni?", "Exit", MessageBoxButton.YesNo); //lehetne szebben is
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
           
        }



        #endregion

        private void menuItemBack_Click(object sender, RoutedEventArgs e)
        {
            ChangeScreenTo("MainMenu.xaml");
        }

    }
}
