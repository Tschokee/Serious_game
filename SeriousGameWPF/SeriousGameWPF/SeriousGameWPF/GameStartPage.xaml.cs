using SeriousGameWPF.Static;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class GameStartPage : Page , INotifyPropertyChanged
    {
        public ObservableCollection<GameMode> SelectedGameModes { get; set; }

         public double CanvasHeight
        {
            get
            {
                return MainMenuHandler._canvasHeight;
            }
            set
            {
                MainMenuHandler._canvasHeight = value;
                OnPropertyChanged("CanvasHeight");
            }
        }
         public double CanvasWidth
         {
             get
             {
                 return MainMenuHandler._canvasWidth;
             }
             set
             {
                 MainMenuHandler._canvasWidth = value;
                 OnPropertyChanged("CanvasWidth");
             }
         }
         #region INotifyPropertyChanged members

         public event PropertyChangedEventHandler PropertyChanged;
         protected void OnPropertyChanged(string propertyName)
         {
             if (PropertyChanged != null)
                 this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
         }

         #endregion
        public GameStartPage()
        {
            InitializeComponent();
            this.DataContext = MainMenuHandler.DataContext;
            this.SelectedGameModes = MainMenuHandler.SelectedGame.GameModes;

            double[] canvasData = MainMenuHandler.CalculatePositionFor(SelectedGameModes, MainMenuHandler.DataContext as MainWindow, true);
            CanvasHeight = canvasData[0];
            CanvasWidth = canvasData[1];
        }

        
    }
}
