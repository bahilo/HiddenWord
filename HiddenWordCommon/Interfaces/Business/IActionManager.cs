
using HiddenWordCommon.Interfaces.DAL;
using System.Collections.Generic;

namespace HiddenWordCommon.Interfaces.Business
{
    public interface IActionManager
    {
        IDALActionManager DALAccess { get; set; }
        HiddenWordCommon.Interfaces.Business.IUsersManager BlUser { get; set; }
        HiddenWordCommon.Interfaces.Business.ISetupsManager BlSetup { get; set; }
        HiddenWordCommon.Interfaces.Business.IStatisticManager BlStat { get; set; }
        HiddenWordCommon.Interfaces.Business.IWordsManager BlWord { get; set; }
        IDisplay BlDisplay { get; set; }
    }
}