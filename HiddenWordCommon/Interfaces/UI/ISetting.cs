using HiddenWordCommon.classes;
using HiddenWordCommon.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordCommon.Interfaces.UI
{
    public interface ISetting
    {
        bool GameSetup();
        bool SetGame();
        bool SettingCheck();
        void init(User user, Setup setup, Random rd);
        Setup Setup { get; set; }
        User User { get; set; }
        IActionManager Bl { get; set; }
        bool IsExitGame { get; set; }
    }
}
