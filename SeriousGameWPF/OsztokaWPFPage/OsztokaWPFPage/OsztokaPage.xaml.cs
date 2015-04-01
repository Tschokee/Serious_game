using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OsztokaWPFPage
{
    /// <summary>
    /// Interaction logic for OsztokaPage.xaml
    /// </summary>
    public partial class OsztokaPage : Window
    {
        readonly Random rand = new Random();
        public ObservableCollection<Ballon> Ballons { get; set; }
        public OsztokaPage()
        {
            Ballons = new ObservableCollection<Ballon>();
            GenerateRandomBallons(6);
            InitializeComponent();
            Init();
            DataContext = this;
        }

        private void GenerateRandomBallons(int p)
        {
            for (var i = 0; i < p; i++)
            {
                var ballon = new Ballon { Number = rand.Next(600), PosX = rand.Next(1000), PosY = rand.Next(800), ZIndex = i, DisplayImage = ConvertStringToImageSource("Images/blueBalloon.png") };// itt random nem lehet random valami plusz megkötés kell / konstruktor is lehet kellene
                Ballons.Add(ballon);
            }           
        }
        

        private async void Init()
        {
            await Task.Delay(1000);
            this.Show();
            this.Focus();
        } 
        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Image).Opacity = 1;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image).Opacity = 0.40;
        }      
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
