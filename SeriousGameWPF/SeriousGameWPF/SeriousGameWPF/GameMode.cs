namespace SeriousGameWPF
{
    public class GameMode : Displayable
    {
        private string _gameDesc;
        private string _startParameters;//?
        public static double XSize=200;
        public static double YSize=200;
        public string GameDesc { get { return _gameDesc; } set { _gameDesc = value; OnPropertyChanged("GameDesc"); } }
        public string StartParameters { get { return _startParameters; } set { _startParameters = value; OnPropertyChanged("StartParameters"); } }
        public GameMode()
        {

        }
    }
}
