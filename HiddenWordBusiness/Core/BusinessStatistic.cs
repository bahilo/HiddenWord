using HiddenWordBusiness.Exceptions;
using HiddenWordCommon.classes;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordCommon.Interfaces.DAL;
using HiddenWordDALXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordBusiness.Core
{
    public class BusinessStatistic : HiddenWordCommon.Interfaces.Business.IStatisticManager
    {
        public IDALActionManager DALAccess { get; set; }
        public ExceptionGenerator exceptionGenerator { get; set; }

        public BusinessStatistic(IDALActionManager DAL)
        {
            DALAccess = DAL;
            exceptionGenerator = new ExceptionGenerator();
        }


        //---------------[ The statistic implementation ] --------------------

        public List<Statistic> GetStatisticByNbTry(int nbTry)
        {
            return DALAccess.GetStatisticByNbTry(nbTry);
        }

        public List<Statistic> GetStatisticBySetupId(int setupId)
        {
            return DALAccess.GetStatisticBySetupId(setupId);
        }

        public List<Statistic> GetStatisticByUserId(int userId)
        {
            return DALAccess.GetStatisticByUserId(userId);
        }

        public List<Statistic> GetStatisticByWordId(int wordId)
        {
            return DALAccess.GetStatisticByWordId(wordId);
        }

        public List<Statistic> GetStatisticData()
        {
            return DALAccess.GetStatisticData();
        }

        public List<Statistic> getUsersStatistics()
        {
            return DALAccess.getUsersStatistics();
        }


        public void insertStatistic(int userId, int wordId, int nbTry, int setupId)
        {
            //Throwing exception for Value greater than 0 
            exceptionGenerator.exceptionFor<int>(
                "read",
                new StringBuilder().AppendFormat("Statistic Error, User Id must be greater than 0 ").ToString(),
                userId,
                excludeValueLessOrEqual: 0);

            //Throwing exception for Value greater than 0 
            exceptionGenerator.exceptionFor<int>(
                "read",
                new StringBuilder().AppendFormat("Statistic Error, Word Id must be greater than 0 ").ToString(),
                wordId,
                excludeValueLessOrEqual: 0);

            //Throwing exception for Value greater than 0 
            exceptionGenerator.exceptionFor<int>(
                "read",
                new StringBuilder().AppendFormat("Statistic Error, Setup Id must be greater than 0 ").ToString(),
                setupId,
                excludeValueLessOrEqual: 0);

            DALAccess.insertStatistic(userId, wordId, nbTry, setupId);
        }

        public Statistic DeleteStatistic(Statistic statistic)
        {
            return DALAccess.DeleteStatistic(statistic);
        }

        public Statistic UpdateStatistic(Statistic statistic)
        {
            return DALAccess.UpdateStatistic(statistic);
        }





    }
}
