using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OsztokaWPFPage;

namespace SeriousGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Game> GamesList0 { get; set; }
        public Game osztoka, helyes; // nem biztos hogy kell
        
        public MainWindow()
        {
            InitializeComponent();
            InitGames();
            DataContext = this;
        }
        private void InitGames() {
            GamesList0 = new ObservableCollection<Game>();
            InitOsztoka();
            InitHelyes();
        }
        #region WPFEventHandlers
        private void myimg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var img = sender as Image;
            var gametostart = img.DataContext as Game;
            if (gametostart.start!=null)
            {
                gametostart.start();
            }
            else
            {
                MessageBox.Show("A játék nem indítható.");
            }
        }
        #endregion
        #region GameMenuInits
        private void InitOsztoka()
        {
            osztoka = new Game
            {
                ImageUri = ConvertStringToImageSource("/Images/osztoka.jpg"),
                Name = "Osztoka",
                PosX = 1,
                start = StartOsztoka
            };
            GamesList0.Add(osztoka);
        }
        private void InitHelyes()
        {
            helyes = new Game
            {
                ImageUri = ConvertStringToImageSource("/Images/helyes_vagy_hejes.jpg"),
                Name = "Helyes vagy hejes",
                PosX = 2
            };
            GamesList0.Add(helyes);
        }
        #endregion
        #region StartGameMethods
        public void StartOsztoka() 
        {
            var o = new OsztokaPage();
        }
        #endregion
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
    }
}
