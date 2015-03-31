using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeriousGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Game> GamesList0 { get; set; }
        public Game osztoka,helyes; // nem biztos hogy kell
        
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
            Image img = sender as Image;
            Game gametostart = img.DataContext as Game;
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
            osztoka = new Game();
            osztoka.ImageUri = ConvertStringToImageSource("/Images/osztoka.jpg");
            osztoka.Name = "Osztoka";
            osztoka.PosX = 1;
            osztoka.start = new Start(StartOsztoka);
            GamesList0.Add(osztoka);
        }
        private void InitHelyes()
        {
            helyes = new Game();

            helyes.ImageUri = ConvertStringToImageSource("/Images/helyes_vagy_hejes.jpg");
            helyes.Name = "Helyes vagy hejes";
            helyes.PosX = 2;

            GamesList0.Add(helyes);

        }
        #endregion
        #region StartGameMethods
        public void StartOsztoka() {
            OsztokaWPFPage.OsztokaPage o = new OsztokaWPFPage.OsztokaPage();


        }
        #endregion
        #region StaticMethods
        private static ImageSource ConvertStringToImageSource(string uri)
        {

            BitmapImage bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.UriSource = new Uri(uri, UriKind.Relative);
            bimage.EndInit();

            return bimage;


        }
        #endregion
    }

    
   
}
