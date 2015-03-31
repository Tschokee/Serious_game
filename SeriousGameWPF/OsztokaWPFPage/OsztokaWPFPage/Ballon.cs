using System.ComponentModel;
using System.Windows;

namespace OsztokaWPFPage
{
    public class Ballon : INotifyPropertyChanged
    {
        #region Privates
        private int _posx;
        private int _posy;
        private string _color;
        private int _number;
        private int _zindex;
        #endregion

        #region Properties
        public string Color { get { return _color; } set { _color = value; OnPropertyChanged("Color"); } }
        public int PosX { get { return _posx; } set { _posx = value; OnPropertyChanged("PosX"); OnPropertyChanged("Margin"); } }
        public int PosY { get { return _posy; } set { _posy = value; OnPropertyChanged("PosY"); OnPropertyChanged("Margin"); } }
        public int Number { get { return _number; } set { _number = value; OnPropertyChanged("Number"); } }
        public Thickness Margin { get { return new Thickness(PosX, PosY, 0, 0); } }
        public int ZIndex { get { return _zindex; } set { _zindex = value; OnPropertyChanged("ZIndex"); } }
        #endregion

        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public Ballon()
        {
        }
    }
}
