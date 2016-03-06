
using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
using HiddenWordCommon.Interfaces.Business;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace HiddenWordWpf.classes
{
    public class Play : INotifyPropertyChanged
    {
        protected bool isSetting;
        public bool IsExitGame { get; set; }
        protected Random rd;
        protected IActionManager Bl;

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
                _newWord = value;
                onPropertyChanged("NewWord");
            }
        }
        private Setup _setup;
        public Setup Setup {
            get
            {
                return _setup;
            }
            set
            {
                if (_setup != null && _setup.Id == value.Id) return;
                _setup = value;
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
                _user = value;
                onPropertyChanged("User");
            }
        }
       

        public Play(IActionManager bl, Random rd)
        {
            this.rd = rd;
            Bl = bl;
            
            NewWord = new Words();
            User = new User();
            Setup = new Setup();

            IsExitGame = false;

            /*Setup.PropertyChanged += onSetupChange;
            User.PropertyChanged += onUserChange;
            NewWord.PropertyChanged += onWordChange;*/
        }


        public void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /*private void onWordChange(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void onUserChange(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void onSetupChange(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void onSetupChange(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("Setup changed!!!!");
        }*/

        protected bool settings(bool isSetting)
        {
            do {                           
                
                if (this.User == null || this.User.Pseudo == null || this.User.Pseudo == "")
                    this.User = Bl.BlDisplay.SelectUser();

                if (this.Setup == null || this.Setup.MaxTry == 0)
                {
                    try
                    {
                        Setup = Bl.BlSetup.GetSetupByStatus((int)ESetup.Active)[0];
                    }
                    catch (ApplicationException e)
                    {
                        Bl.BlDisplay.displayMessage(e.Message);
                        Bl.BlDisplay.setupMaxTry();
                        Setup = Bl.BlSetup.GetSetupByStatus((int)ESetup.Active)[0];
                    }
                }

                if (NewWord == null || NewWord.Name == null || NewWord.Name == "")
                {
                    try
                    {
                        NewWord = Bl.BlWord.getNewRandomWord(rd);
                    }
                    catch (ApplicationException e)
                    {
                        Bl.BlDisplay.displayMessage(e.Message);
                        Bl.BlDisplay.setupNewWord();
                        NewWord = Bl.BlWord.getNewRandomWord(rd);
                    }
                }

            } while (!finalSettingCheck() && !IsExitGame) ;

                return true;
        }        
            
        
        /*-------------------[ Final Check ]--------------*/

        public bool finalSettingCheck()
        {
            if (this.NewWord == null || this.NewWord.Name == null || this.NewWord.Name == "")
            {
                Console.WriteLine("No Word In Database!");
                return false;
            }

            if (this.User == null ||  this.User.Pseudo == null || this.User.Pseudo == "")
            {
                Console.WriteLine("No user selected!");
                return false;
            }

            if (this.Setup == null || this.Setup.MaxTry == 0)
            {
                Console.WriteLine("No maximun try setup!");
                return false;
            }
            return true;
        }        
            
    }
}