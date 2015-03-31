using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;


namespace OsztokaWPFPage
{
    /// <summary>
    /// Interaction logic for OsztokaPage.xaml
    /// </summary>
    public partial class OsztokaPage : Window
    {
        Random rand = new Random();
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
            for (int i = 0; i < p; i++)
            {
                
                Ballon ballon = new Ballon();
                ballon.Number = rand.Next(600);
                ballon.PosX = 10;
                ballon.PosY = 10;
                ballon.ZIndex = i;
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
