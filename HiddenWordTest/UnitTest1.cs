using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HiddenWordCommon.Interfaces.Business;
using HiddenWord.Business;
using HiddenWordCommon.Interfaces.DAL;
using HiddenWordCommon.classes;
using System.Collections.Generic;
using HiddenWordBusiness.Core;
using HiddenWordBusiness.classes;
using HiddenWordCommon.Enums;

namespace HiddenWordTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetRandomWord()
        {
            IDisplay blDisplay = new BusinessDisplay(new Display(),
                                                        new BusinessStatistic(new FakeDAL()),
                                                        new BusinessWord(new FakeDAL()),
                                                        new BusinessSetup(new FakeDAL()),
                                                        new BusinessUser(new FakeDAL()));
            BL Bl = new BL(new BusinessStatistic(new FakeDAL()),
                            new BusinessWord(new FakeDAL()),
                            new BusinessSetup(new FakeDAL()),
                            new BusinessUser(new FakeDAL()),
                            new FakeDAL(),
                            blDisplay);
            Random rd = new Random();
            
            Assert.AreEqual(Bl.BlWord.getNewRandomWord(rd).Name, "dance");
        }

        [TestMethod]
        public void TestGetUserByPseudo()
        {
            IDisplay blDisplay = new BusinessDisplay(new Display(),
                                                        new BusinessStatistic(new FakeDAL()),
                                                        new BusinessWord(new FakeDAL()),
                                                        new BusinessSetup(new FakeDAL()),
                                                        new BusinessUser(new FakeDAL()));
            BL Bl = new BL(new BusinessStatistic(new FakeDAL()),
                            new BusinessWord(new FakeDAL()),
                            new BusinessSetup(new FakeDAL()),
                            new BusinessUser(new FakeDAL()),
                            new FakeDAL(),
                            blDisplay);
            Random rd = new Random();
            
            Assert.AreEqual(Bl.BlUser.GetUserByPseudo("bahilo").Pseudo, "bahilo");
        }

        [TestMethod]
        public void TestGetSetupByStatus()
        {
            IDisplay blDisplay = new BusinessDisplay(new Display(),
                                                        new BusinessStatistic(new FakeDAL()),
                                                        new BusinessWord(new FakeDAL()),
                                                        new BusinessSetup(new FakeDAL()),
                                                        new BusinessUser(new FakeDAL()));
            BL Bl = new BL(new BusinessStatistic(new FakeDAL()),
                            new BusinessWord(new FakeDAL()),
                            new BusinessSetup(new FakeDAL()),
                            new BusinessUser(new FakeDAL()),
                            new FakeDAL(),
                            blDisplay);
            Random rd = new Random();
            
            Assert.AreEqual(Bl.BlSetup.GetSetupByStatus((int)ESetup.Active)[0].Status, (int)ESetup.Active);
        }


    }

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
            return new Setup { Id = 1, MaxTry = 5, Status=1 };
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
            throw new NotImplementedException();
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

        public void InsertSetup(int maxTry, int status)
        {
            throw new NotImplementedException();
        }

        public void insertStatistic(int userId, int wordId, int nbTry, int setupId)
        {
            throw new NotImplementedException();
        }

        public void InsertUser(string pseudo)
        {
            throw new NotImplementedException();
        }

        public void InsertWord(string name)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
