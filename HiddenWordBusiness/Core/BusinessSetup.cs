using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
using HiddenWordCommon.Interfaces.DAL;
using System;
using System.Collections.Generic;

namespace HiddenWordBusiness.Core
{
    public class BusinessSetup : HiddenWordCommon.Interfaces.Business.ISetupsManager
    {
        public IDALActionManager DALAcess { get; set; }

        public BusinessSetup(IDALActionManager DAL)
        {
            DALAcess = DAL;
        }

        //--------------- [ The setup implementation ] --------------------

        public Setup GetSetupById(int id)
        {
            return DALAcess.GetSetupById(id);
        }

        public List<Setup> GetSetupByMaxTry(int maxTry)
        {
            return DALAcess.GetSetupByMaxTry(maxTry);
        }

        public List<Setup> GetSetupByStatus(int status)
        {
            return DALAcess.GetSetupByStatus(status);
        }

        public Setup GetSetupActiveStatus()
        {
            return DALAcess.GetSetupActiveStatus();
        }

        public List<Setup> GetSetupData()
        {
            return DALAcess.GetSetupData();
        }

        public void InsertSetup(int maxTry, int status)
        {
            List<Setup> setupList = DALAcess.GetSetupByStatus((int)ESetup.Active);
            foreach (Setup setup in setupList)
            {
                setup.Status = 0;
                DALAcess.UpdateSetup(setup);
            }
            DALAcess.InsertSetup(maxTry, status);
        }

        public Setup DeleteSetup(Setup setup)
        {
            return DALAcess.DeleteSetup(setup);
        }

        public Setup UpdateSetup(Setup setup)
        {
            return DALAcess.UpdateSetup(setup);
        }

        



    }
}
