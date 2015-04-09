using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OsztokaWPFPage;
using SeriousGameWPF.Static;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

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
           
            InitializeComponent();
            InitGames();
            InitWindow();                  
        }
        private void InitWindow()
        {
            MainMenuHandler.DataContext = this;
            this.DataContext = this;
            WindowCenterX = this.ActualWidth / 2;
            WindowCenterY = this.ActualHeight / 2;
            MainMenuHandler.ChangeScreenTo = this.ChangeScreenTo;
            MainMenuHandler.ChangeScreenTo("MainMenu.xaml");

        }
        private void InitGames()
        {

            InitOsztoka();
            InitHelyes();
            InitSzorzoka();
          //  InitDummyGames(); // ezt csak teszt, ki kell majd venni
            MainMenuHandler.CalculatePositionFor(MainMenuHandler.GamesList, this, isHorizontal);
        }
        #region LogicRegion
        #region GameMenuInits
        private void InitOsztoka()
        {
            osztoka = new Game
            {
                ImageUri = ConvertStringToImageSource("/Images/osztoka.jpg"),
                Name = "Osztóka",
                start = StartOsztoka,
                GameModes = new ObservableCollection<GameMode>()
            
            };
            osztoka.GameModes.Add(new GameMode() { GameDesc = "123" });
            osztoka.GameModes.Add(new GameMode() { GameDesc = "223" });

            osztoka.GameModes.Add(new GameMode() { GameDesc = "323" });
            MainMenuHandler.AddGame(osztoka);

        }
        private void InitSzorzoka()
        {
            MainMenuHandler.AddGame( new Game
            {
                ImageUri = ConvertStringToImageSource("/Images/szorzoka.png"),
                Name = "Szorzóka",
                //start = 
            });


        }
        private void InitHelyes()
        {
            helyes = new Game
            {
                ImageUri = ConvertStringToImageSource("/Images/helyes_vagy_hejes.jpg"),
                Name = "Helyes vagy hejes",

            };
            MainMenuHandler.AddGame(helyes);
        }
        private void InitDummyGames() {
           // MainMenuHandler.AddGame(osztoka);
            for (int i = 0; i < 20; i++)
            {
                MainMenuHandler.AddGame(new Game
                {
                    ImageUri = ConvertStringToImageSource("/Images/osztoka.jpg"),
                    Name = "Osztóka",
                    start = StartOsztoka
                });
            }
        }
        #endregion
        #region StartGameMethods
        public void StartOsztoka()
        {
            var o = new OsztokaPage();
        }
        #endregion
        #endregion
        #region VisualRegion
        #region StaticMethods
        private static ImageSource ConvertStringToImageSource(string uri)
        {
            var bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.UriSource = new Uri(uri, UriKind.Relative);
            bimage.EndInit();

            return bimage;
        }
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
            
            if (gametostart.start != null)
            {
                gametostart.start();
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
            if (DisplayPage=="MainWindow.xaml")
            {
             canvasData = MainMenuHandler.CalculatePositionFor(MainMenuHandler.GamesList, this, isHorizontal);
            }
            else if (DisplayPage == "GameStartPage.xaml")
            {
                canvasData = MainMenuHandler.CalculatePositionFor(MainMenuHandler.SelectedGame.GameModes, this, isHorizontal);
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
        public async void ChangeScreenTo(string page) {
            menuItemBack.IsEnabled = true;
            FadeFrameOut();
            await Task.Delay(1000);
            DisplayPage = page;
            FixView();
            FadeFrameIn();
            await Task.Delay(1000);
        }
        public  void FadeFrameOut() {
            (this.Resources["FadeOut"] as Storyboard).Begin();
        
                
        }
        public void FadeFrameIn()
        {
            (this.Resources["FadeIn"] as Storyboard).Begin();
           

        }
        #endregion

        private void menuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        #endregion

        private void menuItemBack_Click(object sender, RoutedEventArgs e)
        {
            ChangeScreenTo("MainMenu.xaml");
        }

    }
}
