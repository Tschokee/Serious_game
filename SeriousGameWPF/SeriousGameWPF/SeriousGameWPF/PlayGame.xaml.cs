using SeriousGameWPF.Static;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeriousGameWPF
{
    /// <summary>
    /// Interaction logic for PlayGame.xaml
    /// </summary>
    public partial class PlayGame : Page
    {
        private Game game;
        private GameMode gm;
        private MainWindow mainWindow;
        private bool m_IsPressed = false;
        private GameContent SelectedContent;
        public ObservableCollection<GameContent> ActiveContent { get; set; }

        public PlayGame()
        {
            InitializeComponent();
        }

        public PlayGame(Game game, GameMode gm,MainWindow mainWindow)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.game = game;
            this.gm = gm;
            this.DataContext = this;
            game.ActiveContent = new ObservableCollection<GameContent>();
            game.ActiveContent.Add(new GameContent() { DefaultPosX=10, DefaultPosY=10 , ImageUri= ConvertStringToImageSource("Images/blueBalloon.png"), Name="Balloon1", PairID=1, PosX=10,PosY=10, TextContent="10", TextLeft=30, TextTop=30 , ViewboxHeight=200, ViewboxWidth=200});
            game.ActiveContent.Add(new GameContent() { DefaultPosX = 10, DefaultPosY = 10, ImageUri = ConvertStringToImageSource("Images/blueBalloon.png"), Name = "Balloon1", PairID = 1, PosX = 20, PosY = 10, TextContent = "10", TextLeft = 30, TextTop = 30, ViewboxHeight = 200, ViewboxWidth = 200 });
            game.ActiveContent.Add(new GameContent() { DefaultPosX = 10, DefaultPosY = 10, ImageUri = ConvertStringToImageSource("Images/blueBalloon.png"), Name = "Balloon1", PairID = 1, PosX = 10, PosY = 30, TextContent = "10", TextLeft = 30, TextTop = 30, ViewboxHeight = 200, ViewboxWidth = 200 });
            game.ActiveContent.Add(new GameContent() { DefaultPosX = 10, DefaultPosY = 10, ImageUri = ConvertStringToImageSource("Images/blueBalloon.png"), Name = "Balloon1", PairID = 1, PosX = 30, PosY = 30, TextContent = "10", TextLeft = 30, TextTop = 30, ViewboxHeight = 200, ViewboxWidth = 200 });
            
            ActiveContent = game.ActiveContent;
            mainWindow.Height = MainMenuHandler.GameWindowHeight;
            mainWindow.Width = MainMenuHandler.GameWindowWidth;
        }
        private static ImageSource ConvertStringToImageSource(string uri)
        {
            var bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.UriSource = new Uri(uri, UriKind.Relative);
            bimage.EndInit();

            return bimage;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private double m_X;
        private double m_Y;
        private double c_X;
        private double c_Y;

        private void Viewbox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Viewbox vb = sender as Viewbox;
            SelectedContent = vb.DataContext as GameContent;
            m_IsPressed = true;
            m_X = Mouse.GetPosition(PlayArea).X;
            m_Y = Mouse.GetPosition(PlayArea).Y;
            c_X = SelectedContent.PosX;
            c_Y = SelectedContent.PosY;
        }

        private void Viewbox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            m_IsPressed = false;
        }

        private void Viewbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
            {
                m_IsPressed = false;
            }
            if (m_IsPressed)
            {
                SelectedContent.PosX = c_X-m_X+Mouse.GetPosition(PlayArea).X;    // ez így gagyi javítani kell
                SelectedContent.PosY = c_Y-m_Y+Mouse.GetPosition(PlayArea).Y;    // ez így gagyi javítani kell
                //SelectedContent.PosX = Mouse.GetPosition(PlayArea).X-(Mouse.GetPosition(sender as Viewbox).X);
                //SelectedContent.PosY = Mouse.GetPosition(PlayArea).Y - (Mouse.GetPosition(sender as Viewbox).Y);
            }
        }
       


    }
}
