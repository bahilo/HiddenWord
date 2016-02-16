using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
using HiddenWordCommon.Interfaces.DAL;
using System;
using System.Collections.Generic;

namespace HiddenWordBusiness.Core
{
    public class BusinessSetup : HiddenWordCommon.Interfaces.Business.ISetupsManager
    {
        public IDALActionManager DALAccess { get; set; }

        public BusinessSetup(IDALActionManager DAL)
        {
            DALAccess = DAL;
        }

        //--------------- [ The setup implementation ] --------------------

        public Setup GetSetupById(int id)
        {
            return DALAccess.GetSetupById(id);
        }

        public List<Setup> GetSetupByMaxTry(int maxTry)
        {
            return DALAccess.GetSetupByMaxTry(maxTry);
        }

        public List<Setup> GetSetupByStatus(int status)
        {
            return DALAccess.GetSetupByStatus(status);
        }

        public Setup GetSetupActiveStatus()
        {
            return DALAccess.GetSetupActiveStatus();
        }

        public List<Setup> GetSetupData()
        {
            return DALAccess.GetSetupData();
        }

        public void InsertSetup(int maxTry, int status)
        {
            List<Setup> setupList = DALAccess.GetSetupByStatus((int)ESetup.Active);
            foreach (Setup setup in setupList)
            {
                setup.Status = 0;
                DALAccess.UpdateSetup(setup);
            }
            DALAccess.InsertSetup(maxTry, status);
        }

        public Setup DeleteSetup(Setup setup)
        {
            return DALAccess.DeleteSetup(setup);
        }

        public Setup UpdateSetup(Setup setup)
        {
            return DALAccess.UpdateSetup(setup);
        }

        



    }
}
