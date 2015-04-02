﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace SeriousGameWPF
{
    public delegate void Start();
    public class Game : INotifyPropertyChanged
    {
        #region Privates
        private double _posx;
        private double _posy;
        private string _name;
        private ImageSource _imageuri;

        #endregion

        #region Properties
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public ImageSource ImageUri { get { return _imageuri; } set { _imageuri = value; OnPropertyChanged("ImageUri"); } }
        public double PosX { get { return _posx; } set { _posx = value; OnPropertyChanged("PosX"); OnPropertyChanged("Margin"); } }
        public double PosY { get { return _posy; } set { _posy = value; OnPropertyChanged("PosY"); OnPropertyChanged("Margin"); } }
        public Thickness Margin { get { return new Thickness(PosX, PosY, 0, 0); } }
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
