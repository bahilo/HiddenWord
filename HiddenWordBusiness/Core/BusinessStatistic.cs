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

        public BusinessStatistic(IDALActionManager DAL)
        {
            DALAccess = DAL;
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
