using System.ComponentModel;
using System.Windows.Media;

namespace SeriousGameWPF
{
    public delegate void Start();
    public class Game : INotifyPropertyChanged
    {
        #region Privates
        private int _posx;
        private string _name;
        private ImageSource _imageuri;
        #endregion

        #region Properties
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public int PosX { get { return _posx; } set { _posx = value; OnPropertyChanged("PosX"); } }
        public ImageSource ImageUri { get { return _imageuri; } set { _imageuri = value; OnPropertyChanged("ImageUri"); } }
        #endregion

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        public Start start;
        public Game()
        {
            //kell majd normális konstruktor
        }
    }
}
