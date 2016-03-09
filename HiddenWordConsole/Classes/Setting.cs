using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordCommon.Interfaces.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordConsole.Classes
{
    class Setting : ISetting
    {
        public IActionManager Bl { get; set; }
        public Setup Setup { get; set; }
        public User User { get; set; }
        public bool IsExitGame { get; set; }

        protected bool isSetting;

        public Setting(IActionManager bl)
        {
            Bl = bl;
        }

        public void init(User user, Setup setup, Random rd)
        {
            User = user;
            Setup = setup;
        }

        public bool SetGame()
        {
            string response = "";
            do
            {
                if (isSetting)
                    response = Bl.BlDisplay.setupMenu(); // SETTING MENU
                if (response.Equals(ESetupMenu.Exit))
                    return false;
                if (response.Equals(ESetupMenu.ChoiceOne))
                {
                    if (isSetting)
                    {
                        Bl.BlDisplay.setupNewWord();
                    }
                }
                if (response.Equals(ESetupMenu.ChoiceTwo))
                {
                    if (isSetting || this.Setup.MaxTry == 0)
                    {
                        try
                        {
                            this.Setup = Bl.BlDisplay.setupMaxTry(); // MAX TRY SETTING
                        }
                        catch (ApplicationException e)
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
            } while (!SettingCheck());

            return true;
        }



        public bool GameSetup()
        {
            bool isSetup;
            string response;
            do
            {
                response = Bl.BlDisplay.displayStartupMenu();

                if (response.Equals(ESetupMenu.Exit))
                {
                    IsExitGame = true;
                }
                else if (response.Equals(ESetupMenu.ChoiceOne)) // Setting
                {
                    isSetting = true;
                }
                else if (response.Equals(ESetupMenu.ChoiceTwo)) // Start game
                {
                    isSetting = false;
                }

                if (IsExitGame)
                    isSetup = true;
                else
                    isSetup = SetGame();
            } while (
                        !response.Equals(ESetupMenu.Exit)
                        && !response.Equals(ESetupMenu.ChoiceOne)
                        && !response.Equals(ESetupMenu.ChoiceTwo)
                        || !isSetup
               );
            return true;
        }

        public bool SettingCheck()
        {
            if (User == null || User.Pseudo == null || User.Pseudo == "")
            {
                Bl.BlDisplay.displayMessage("No User Selected!",nbEmptyLineBefore: 2, nbEmptyLineAfter: 2, nbTabulation: 2);
                return false;
            }

            if (Setup == null || Setup.MaxTry == 0)
            {
                Bl.BlDisplay.displayMessage("No Maximun Try Setup!", nbEmptyLineBefore: 2, nbEmptyLineAfter: 2, nbTabulation: 2);
                return false;
            }
            return true;
        }
    }
}
