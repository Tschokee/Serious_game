using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SeriousGameWPF
{
    public class OMenuItem : INotifyPropertyChanged
    {
        private string _text;

        public string Text { get { return _text; } set { _text = value; OnPropertyChanged("Text"); } }

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
