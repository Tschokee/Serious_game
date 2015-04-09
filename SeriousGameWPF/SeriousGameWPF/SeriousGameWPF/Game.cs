using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace SeriousGameWPF
{
    public delegate void Start();
    public class Game : Displayable
    {
        public ObservableCollection<GameMode> GameModes;
        public static double _ySize=200;
        public static double _xSize=400;
        #region Privates
             

        #endregion

        #region Properties
       
        #endregion

        
        public Start start;
        public Game()
        {

            //kell majd normális konstruktor
        }

        internal void MoveToCenter(MainWindow mainWindow)
        {
            
        }
    }
}
