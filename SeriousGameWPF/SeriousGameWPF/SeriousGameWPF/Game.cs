using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Windows.Media;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        #endregion
        public Start start;
        public Game()
        {
            //kell majd normális konstruktor

        }


    }
}
