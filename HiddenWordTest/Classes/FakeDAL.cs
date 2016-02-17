using HiddenWordCommon.classes;
using HiddenWordCommon.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordTest.Classes
{
    public class FakeDAL : IDALActionManager
    {
        public Setup DeleteSetup(Setup setup)
        {
            throw new NotImplementedException();
        }

        public Statistic DeleteStatistic(Statistic statistic)
        {
            throw new NotImplementedException();
        }

        public User DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public Words DeleteWord(Words word)
        {
            throw new NotImplementedException();
        }

        public Setup GetSetupActiveStatus()
        {
            return new Setup { Id = 1, MaxTry = 5, Status = 1 };
        }

        public Setup GetSetupById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Setup> GetSetupByMaxTry(int maxTry)
        {
            throw new NotImplementedException();
        }

        public List<Setup> GetSetupByStatus(int status)
        {
            return new List<Setup> { new Setup { Id = 1, MaxTry = 5, Status = 1 } };
        }

        public List<Setup> GetSetupData()
        {
            throw new NotImplementedException();
        }

        public List<Statistic> GetStatisticByNbTry(int nbTry)
        {
            throw new NotImplementedException();
        }

        public List<Statistic> GetStatisticBySetupId(int setupId)
        {
            throw new NotImplementedException();
        }

        public List<Statistic> GetStatisticByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public List<Statistic> GetStatisticByWordId(int wordId)
        {
            throw new NotImplementedException();
        }

        public List<Statistic> GetStatisticData()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByPseudo(string pseudo)
        {
            return new User { Id = 1, Pseudo = "bahilo" };
        }

        public List<User> GetUserData()
        {
            throw new NotImplementedException();
        }

        public List<Statistic> getUsersStatistics()
        {
            throw new NotImplementedException();
        }

        public Words GetWordsById(int id)
        {
            return new Words { Id = 1, Name = "dance" };
        }

        public Words GetWordsByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Words> GetWordsData()
        {
            return new List<Words> { new Words {
                                         Id=1,
                                         Name="dance"}};
        }

        public Setup InsertSetup(int maxTry, int status)
        {
            return new Setup();
        }

        public Statistic insertStatistic(int userId, int wordId, int nbTry, int setupId)
        {
            return new Statistic();
        }

        public User InsertUser(string pseudo)
        {
            return new User();
        }

        public Words InsertWord(string name)
        {
            return new Words();
        }

        public Setup UpdateSetup(Setup setup)
        {
            throw new NotImplementedException();
        }

        public Statistic UpdateStatistic(Statistic statistic)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Words UpdateWord(Words word)
        {
            return new Words { Id = 1, Name = "swim" };
        }
    }
}
