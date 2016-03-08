
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HiddenWordCommon.classes
{
    public class User : INotifyPropertyChanged
    {
        private string _pseudo;
        public string Pseudo {
            get
            {
                return _pseudo;
            }
            set
            {
                if (_pseudo != value)
                {
                    _pseudo = value;
                    onPropertyChnage("Pseudo");
                }
            }
        }

        private void onPropertyChnage(string v)
        {
            if(PropertyChanged != null )
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        public int Id { get; set; }
        public  List<Statistic> UserStats { get; set; }
        public List<Words> UserWordsStats { get; set; }
        public List<Setup> UserSetupsStats { get; set; }

        public User()
        {
            UserStats = new List<Statistic>();
            UserWordsStats = new List<Words>();
            UserSetupsStats = new List<Setup>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
