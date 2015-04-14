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
            game.GenerateActiveContent(gm);   
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
            SelectedContent.Focus = true;
            
        }

        private void Viewbox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            m_IsPressed = false;
            SelectedContent.Focus = false;
            GameContent temp;
            if(game.CollusionTest(SelectedContent,out temp)){
                SelectedContent.TextContent = "HURRAH";
                temp.TextContent = "HURRAH2";
                SelectedContent.State = State.Solved;
                temp.State = State.Solved;
            }
            IsThisTheEnd();
        }
        private void IsThisTheEnd() {
            if (game.IsSolved()) {

                MainMenuHandler.ChangeScreenTo("EndScreen.Xaml");
            };
        }

        private void Viewbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
            {
                m_IsPressed = false;
                if (SelectedContent!=null)
                {
                    SelectedContent.Focus = false;
                }
               
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
