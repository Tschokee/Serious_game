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

namespace SeriousGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Game osztoka, helyes; // nem biztos hogy kell

        public MainWindow()
        {

            InitializeComponent();
            InitGames();
            
        }
        private void InitGames()
        {

            InitOsztoka();
            InitHelyes();
            InitDummyGames(); // ezt csak teszt, ki kell majd venni
            MainMenuHandler.CalculatePositionForAllGamesIn(this);
        }
        #region LogicRegion
        #region GameMenuInits
        private void InitOsztoka()
        {
            osztoka = new Game
            {
                ImageUri = ConvertStringToImageSource("/Images/osztoka.jpg"),
                Name = "Osztóka",
                start = StartOsztoka
            };
         

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
            MainMenuHandler.AddGame(osztoka);
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FixView();


        }
        public void FixView()
        {
            var canvasData = MainMenuHandler.CalculatePositionForAllGamesIn(this);
            CanvasHeight = canvasData[0];
            CanvasWidth = canvasData[1];
            ScrollViewerForCanvas.UpdateLayout();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            FixView();
        }
        #endregion
        #region WPFProperties
        double _canvasHeight = 200;
        double _canvasWidth = 200;
        public double CanvasHeight
        {
            get
            {
                return _canvasHeight;
            }
            set
            {
                _canvasHeight = value;
                OnPropertyChanged("CanvasHeight");
            }
        }
        public double CanvasWidth
        {
            get
            {
                return _canvasWidth;
            }
            set
            {
                _canvasWidth = value;
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            

        }
        #endregion
    }
}
