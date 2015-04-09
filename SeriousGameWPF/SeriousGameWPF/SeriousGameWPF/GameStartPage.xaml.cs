using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using SeriousGameWPF.Static;

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
            this.DataContext = this;
            this.SelectedGameModes = MainMenuHandler.SelectedGame.GameModes;

            double[] canvasData = MainMenuHandler.CalculatePositionFor(SelectedGameModes, MainMenuHandler.DataContext as MainWindow, true);
            CanvasHeight = canvasData[0];
            CanvasWidth = canvasData[1];
        }

        
    }
}
