using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                var ballon = new Ballon {Number = rand.Next(600), PosX = 10, PosY = 10, ZIndex = i};
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
    }
}
