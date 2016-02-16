using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordCommon.classes
{
    public class Setup : INotifyPropertyChanged
    {
        private int _maxTry;
        public int MaxTry {
            get
            {
                return _maxTry;
            }
            set
            {
                if (value != _maxTry)
                {
                    _maxTry = value;

                    onSetupChange("MaxTry");
                }
            }

        }
        
        public int Status { get; set; }
        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void onSetupChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
