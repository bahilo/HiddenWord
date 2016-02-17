
using HiddenWordCommon.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordCommon.Interfaces.DAL
{
    public interface ISetupsManager
    {
        List<Setup> GetSetupData();
        Setup GetSetupById(int id);
        List<Setup> GetSetupByMaxTry(int maxTry);
        List<Setup> GetSetupByStatus(int status);
        Setup GetSetupActiveStatus();
        Setup InsertSetup(int maxTry, int status);
        Setup DeleteSetup(Setup setup);
        Setup UpdateSetup(Setup setup);
    }
}
