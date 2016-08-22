using HiddenWordCommon.Interfaces.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiddenWordCommon.classes;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordCommon.Enums;

namespace HiddenWordWpf.Classes
{
    class Setting : ISetting
    {
        public Setup Setup { get; set; }
        public User User { get; set; }
        public IActionManager Bl { get; set; }
        public bool IsExitGame { get; set; }

        public Setting(IActionManager bl)
        {
            Bl = bl;
        }

        public bool GameSetup()
        {
            do
            {
                if (this.User == null || this.User.Pseudo == null || this.User.Pseudo == "")
                    this.User = Bl.BlDisplay.SelectUser();

                //if (this.Setup == null || this.Setup.MaxTry == 0)
                //{
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
                //}

            } while (!SettingCheck() );

            return true;
        }

        public void init(User user, Setup setup, Random rd)
        {
            User = user;
            Setup = setup;
        }

        public bool SetGame()
        {
            return GameSetup();
        }

        public bool SettingCheck()
        {
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
