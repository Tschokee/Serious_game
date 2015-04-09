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
    /// Interaction logic for GameStartPage.xaml
    /// </summary>
    public partial class GameStartPage : Page
    {
        public ObservableCollection<GameMode> SelectedGameModes { get; set; }
        public GameStartPage()
        {
            InitializeComponent();
            this.DataContext = this;
            this.SelectedGameModes = MainMenuHandler.SelectedGame.GameModes;
            MainMenuHandler.CalculatePositionFor(SelectedGameModes, MainMenuHandler.DataContext as MainWindow, true);
        }
        
    }
}
