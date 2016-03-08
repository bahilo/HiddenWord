
using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordCommon.Interfaces.UI;
using System;
using System.ComponentModel;

namespace HiddenWordBusiness.classes
{
    public class Play : INotifyPropertyChanged
    {
        protected bool isSetting;        
        protected Random rd;
        protected IActionManager Bl;
        protected ISetting Setting { get; set; }

        public bool IsExitGame {
            get
            {
                return Setting.IsExitGame;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Words _newWord;
        public Words NewWord
        {
            get
            {
                return _newWord;
            }
            set
            {
                if (_newWord != null && _newWord.Id == value.Id) return;

                int idBefor = (_newWord != null) ? _newWord.Id : 0;
                int idAfter = (value != null) ? value.Id : 0 ;

                _newWord = value;

                if (idBefor != 0 && idBefor != idAfter)
                    onPropertyChanged("NewWord");                
            }
        }
        private Setup _setup;
        public Setup Setup
        {
            get
            {
                return _setup;
            }
            set
            {
                if (_setup != null && _setup.Id == value.Id) return;

                int idBefor = (_setup != null) ? _setup.Id : 0;
                int idAfter = (value != null) ? value.Id : 0;

                _setup = value;

                if ( idBefor != 0 && idBefor != idAfter)
                    onPropertyChanged("Setup");
            }
        }

        private User _user;
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                if (_user != null && _user.Id == value.Id) return;

                int idBefor = (_user != null) ? _user.Id : 0;
                int idAfter = (value != null) ? value.Id : 0;

                _user = value;

                if (idBefor != 0 && idBefor != idAfter)
                    onPropertyChanged("User");

                
            }
        }
        
        public Play(IActionManager bl, ISetting setting, Random rd)
        {
            this.rd = rd;
            Bl = bl;
            Setting = setting;
            NewWord = new Words();
            User = new User();
            Setup = new Setup();

            setting.init( User, Setup, rd);
        }
        
        public void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}