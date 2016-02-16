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
        public IDALActionManager DALAcess { get; set; }

        public BusinessStatistic(IDALActionManager DAL)
        {
            DALAcess = DAL;
        }


        //---------------[ The statistic implementation ] --------------------

        public List<Statistic> GetStatisticByNbTry(int nbTry)
        {
            return DALAcess.GetStatisticByNbTry(nbTry);
        }

        public List<Statistic> GetStatisticBySetupId(int setupId)
        {
            return DALAcess.GetStatisticBySetupId(setupId);
        }

        public List<Statistic> GetStatisticByUserId(int userId)
        {
            return DALAcess.GetStatisticByUserId(userId);
        }

        public List<Statistic> GetStatisticByWordId(int wordId)
        {
            return DALAcess.GetStatisticByWordId(wordId);
        }

        public List<Statistic> GetStatisticData()
        {
            return DALAcess.GetStatisticData();
        }

        public List<Statistic> getUsersStatistics()
        {
            return DALAcess.getUsersStatistics();
        }


        public void insertStatistic(int userId, int wordId, int nbTry, int setupId)
        {
            DALAcess.insertStatistic(userId, wordId, nbTry, setupId);
        }

        public Statistic DeleteStatistic(Statistic statistic)
        {
            return DALAcess.DeleteStatistic(statistic);
        }

        public Statistic UpdateStatistic(Statistic statistic)
        {
            return DALAcess.UpdateStatistic(statistic);
        }





    }
}
