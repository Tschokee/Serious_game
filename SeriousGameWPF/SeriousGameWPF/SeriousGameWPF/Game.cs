using System.Collections.ObjectModel;

namespace SeriousGameWPF
{
    public delegate void Start(GameMode gm);
    public delegate void GenerateActiveContent(GameMode gm);
    public class Game : Displayable
    {
        public ObservableCollection<GameMode> GameModes;
        public static double YSize=200;
        public static double XSize=400;
        #region Privates
        public ObservableCollection<GameContent> ActiveContent { set; get; }     

        #endregion

        #region Properties
       
        #endregion

        
        public Start Start;
        public GenerateActiveContent GenerateActiveContent;
        public Game()
        {
            GameModes= new ObservableCollection<GameMode>();
            GameModes.Add(new GameMode() { GameDesc = "Nincs elérhető játékmód." });
            //kell majd normális konstruktor
        }


    }
}
