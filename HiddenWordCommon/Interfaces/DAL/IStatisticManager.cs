
using HiddenWordCommon.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordCommon.Interfaces.DAL
{
    public interface IStatisticManager
    {
        List<Statistic> GetStatisticData();
        List<Statistic> GetStatisticByNbTry(int nbTry);
        List<Statistic> GetStatisticBySetupId(int setupId);
        List<Statistic> GetStatisticByUserId(int userId);
        List<Statistic> GetStatisticByWordId(int wordId);
        Statistic insertStatistic(int userId, int wordId, int nbTry, int setupId);
        Statistic DeleteStatistic(Statistic statistic);
        Statistic UpdateStatistic(Statistic statistic);
    }
}
