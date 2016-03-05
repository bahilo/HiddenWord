
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HiddenWordCommon.classes
{
    public class Words : INotifyPropertyChanged
    {
        public int Id  { get; set; }
        public string _name;
        public string Name {
            get
            {
                return _name;
            }
            set
            {
                if( _name != value)
                {
                    _name = value;
                    onPropertyChanged("Name");
                }                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }



}
