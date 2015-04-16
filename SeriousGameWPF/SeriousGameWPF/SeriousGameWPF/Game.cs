using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace SeriousGameWPF
{
    public delegate void Start(GameMode gm);
    public delegate void GenerateActiveContent(GameMode gm);
    public class Game : Displayable
    {
        public ObservableCollection<GameMode> GameModes;
        public static double YSize=200;
        public static double XSize=400;
        public ObservableCollection<GameContent> ActiveContent { set; get; }   

        #region Privates
        private Brush _background;
        
        #endregion

        #region Properties
        public Brush Background
        {
            get
            {
                return _background;
            }

            set
            {
                _background = value;
                OnPropertyChanged("Background");
            }
        }
        #endregion

        
        public Start Start;
        public GenerateActiveContent GenerateActiveContent;
        public Game()
        {
            GameModes= new ObservableCollection<GameMode>();
            GameModes.Add(new GameMode() { GameDesc = "Nincs elérhető játékmód." });
            //kell majd normális konstruktor
        }
        
        /// <summary>
        /// Collusion detection in the ActiveContent.
        /// </summary>
        /// <param name="lastMovedObject">GameContent to be tested.</param>
        /// <param name="detectedObject">Result for the search. <c>null</c> if there are no collusion.</param>
        /// <returns><see langword="true"/> if the detection vas succesfull(<paramref name="detectedObject">detectedObject</paramref> is not <see langword="null"/>),<see langword="false"/> otherwise. </returns>
        public bool CollusionTest(GameContent lastMovedObject, out GameContent detectedObject) {
            detectedObject = null;
            if (ActiveContent.Contains(lastMovedObject))
            {
                foreach (GameContent gameContent in ActiveContent)
                {
                    if (gameContent!=lastMovedObject)
                    {
                        if (lastMovedObject.Collusion(gameContent)) {
                            detectedObject = gameContent;
                            return true;
                        }
                    }
                }
            }

            return false;
        }



        internal bool IsSolved()
        {
            return ActiveContent.All(gameContent => gameContent.State == State.Solved);
        }
    }
}
