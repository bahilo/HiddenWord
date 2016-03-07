using HiddenWordBusiness.Exceptions;
using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
using HiddenWordCommon.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiddenWordBusiness.Core
{
    public class BusinessSetup : HiddenWordCommon.Interfaces.Business.ISetupsManager
    {
        public IDALActionManager DALAccess { get; set; }
        public ExceptionGenerator exceptionGenerator { get; set; }

        public BusinessSetup(IDALActionManager DAL)
        {
            DALAccess = DAL;
            exceptionGenerator = new ExceptionGenerator();
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
            var result = DALAccess.GetSetupByStatus(status);
            // Throwing exception for null value
            exceptionGenerator.exceptionFor<int>(
                "read",
                new StringBuilder().AppendFormat("No data found in the database for max try!").ToString(),
                result.Count,
                excludeValue: 0,
                isExludeNullable: false);

            return result;
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
            exceptionGenerator.exceptionFor<int>(
                "read",
                new StringBuilder().AppendFormat("Setup Error, Max try cannot must be greater than 0!").ToString(),
                maxTry,
                excludeValue: 0,
                isExludeNullable: false);
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
