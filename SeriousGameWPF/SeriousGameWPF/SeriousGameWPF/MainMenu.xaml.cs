using SeriousGameWPF.Static;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
            this.DataContext = MainMenuHandler.DataContext;
        }
        private void Viewbox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var vb = sender as Viewbox;
            var gametoview = vb.DataContext as Game;


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
    }
}
