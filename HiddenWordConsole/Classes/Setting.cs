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
                if (response.Equals("0"))
                    return false;
                if (response.Equals("1"))
                {
                    if (isSetting)
                    {
                        Bl.BlDisplay.setupNewWord();
                    }
                }
                if (response.Equals("2"))
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

                if (response.Equals("0"))
                {
                    IsExitGame = true;
                }
                else if (response.Equals("1")) // Setting
                {
                    isSetting = true;
                }
                else if (response.Equals("2")) // Start game
                {
                    isSetting = false;
                }

                if (IsExitGame)
                    isSetup = true;
                else
                    isSetup = SetGame();
            } while (
                        !response.Equals("0")
                        && !response.Equals("1")
                        && !response.Equals("2")
                        || !isSetup
               );
            return true;
        }

        public bool SettingCheck()
        {
            /*if (NewWord == null || NewWord.Name == null || NewWord.Name == "")
            {
                Console.WriteLine("No Word Found In Database!");
                return false;
            }*/

            if (User == null || User.Pseudo == null || User.Pseudo == "")
            {
                Console.WriteLine("No User Selected!");
                return false;
            }

            if (Setup == null || Setup.MaxTry == 0)
            {
                Console.WriteLine("No Maximun Try Setup!");
                return false;
            }
            return true;
        }
    }
}
