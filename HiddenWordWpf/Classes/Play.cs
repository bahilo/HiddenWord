
using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
using HiddenWordCommon.Interfaces.Business;
using System;
using System.ComponentModel;

namespace HiddenWordBusiness.classes
{
    public class Play
    {
        public Words NewWord { get; set; }
        public Setup Setup { get; set; }
        public User User { get; set; }
        protected bool isSetting;
        public bool IsExitGame { get; set; }
        protected Random rd;
        protected IActionManager Bl;

        

        public Play(IActionManager bl, Random rd)
        {
            this.rd = rd;
            Bl = bl;
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
            User = Bl.BlDisplay.SelectUser();
            Setup = (Bl.BlSetup.GetSetupByStatus((int)ESetup.Active) == null ) ? bl.BlDisplay.setupMaxTry() : Bl.BlSetup.GetSetupByStatus((int)ESetup.Active)[0];

            //Setup.PropertyChanged += onSetupChange;

            
        }

        /*private void onSetupChange(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("Setup changed!!!!");
        }*/

        protected bool settings(bool isSetting)
        {
            string response = "";
            IsExitGame = false;

            do {                            
                if (isSetting)
                    response = Bl.BlDisplay.setupMenu(); // SETTING MENU
                if (response.Equals("0"))
                    return false;
                if (response.Equals("1"))
                {
                    if (isSetting)
                    {
                        this.NewWord = Bl.BlDisplay.setupNewWord();
                    }
                }                
                if (response.Equals("2"))
                {
                    if (isSetting && this.Setup.MaxTry == 0 )
                    {
                        try
                        {
                            this.Setup = Bl.BlDisplay.setupMaxTry(); // MAX TRY SETTING
                        }
                        catch (ApplicationException e )
                        {
                            Bl.BlDisplay.displayMessage(e.Message);
                            this.Setup = Bl.BlDisplay.setupMaxTry(); // MAX TRY SETTING
                        }                        
                    }
                }

                if (this.User == null || this.User.Pseudo == null || this.User.Pseudo == "")
                    this.User = Bl.BlDisplay.SelectUser();

                if (this.Setup == null || this.Setup.MaxTry == 0)
                {
                    var listSetup = Bl.BlSetup.GetSetupByStatus((int)ESetup.Active);
                    this.Setup = ( listSetup.Count > 0 ) ?  listSetup[0] : this.Setup;

                    if (this.Setup == null || this.Setup.MaxTry == 0)
                        this.Setup = Bl.BlDisplay.setupMaxTry();
                }

            } while (!finalSettingCheck() && !IsExitGame) ;

                return true;
        }        
            
        
        /*-------------------[ Final Check ]--------------*/

        public bool finalSettingCheck()
        {
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