using System.Collections.ObjectModel;

namespace SeriousGameWPF
{
    public delegate void Start();
    public class Game : Displayable
    {
        public ObservableCollection<GameMode> GameModes;
        public static double YSize=200;
        public static double XSize=400;
        #region Privates
             

        #endregion

        #region Properties
       
        #endregion

        
        public Start Start;
        public Game()
        {
            GameModes= new ObservableCollection<GameMode>();
            GameModes.Add(new GameMode() { GameDesc = "Nincs elérhető játékmód." });
            //kell majd normális konstruktor
        }

        internal void MoveToCenter(MainWindow mainWindow)
        {
            
        }
    }
}
