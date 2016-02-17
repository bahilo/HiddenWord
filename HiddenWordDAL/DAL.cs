
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiddenWordDAL.DataModel.DsHiddenWordTableAdapters;
using System.Data;
using HiddenWordCommon.classes;
using HiddenWordCommon.Interfaces.DAL;

namespace HiddenWordDAL
{
    public class DAL : IDALActionManager
    {
        setupsTableAdapter tabadapSetup = new setupsTableAdapter();
        usersTableAdapter tabadapUser = new usersTableAdapter();
        wordsTableAdapter tabadapWord = new wordsTableAdapter();
        statisticTableAdapter tabadapStat = new statisticTableAdapter();
        

        //---------------[ The setup implementation ] --------------------

        public Setup GetSetupById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Setup> GetSetupByMaxTry(int maxTry)
        {
            
            var setups = tabadapSetup.GetSetupByMaxTry(maxTry);
            List<Setup> responseSetup = new List<Setup>();
            int i = 0;
            foreach (var row in setups)
            {
                responseSetup[i].Id = row.id;
                responseSetup[i].MaxTry = row.MaxTry;
                responseSetup[i].Status = row.Status;
                i++;
                //break;
            }

            return responseSetup;
        }

        public List<Setup> GetSetupByStatus(int status)
        {
            var setups = tabadapSetup.GetSetupByStatus(status); // 1 = current status
            return GetListSetupsFromTableAdapter(setups);
        }

        public Setup GetSetupActiveStatus()
        {
            var setups = tabadapSetup.GetSetupActiveStatus(); // 1 = current status
            return GetOneSetupFromTableAdapter(setups);
        }

        public List<Setup> GetSetupData()
        {
            throw new NotImplementedException();
        }

        public Setup InsertSetup(int maxTry, int status)
        {
            tabadapSetup.ResetSetupStatus();
            var results = tabadapSetup.InsertSetup(maxTry, status);
            return new Setup();
        }


        private Setup GetOneSetupFromTableAdapter(HiddenWordDAL.DataModel.DsHiddenWord.setupsDataTable source)
        {
            Setup setup = new Setup();

            foreach (var row in source)
            {
                setup.Id = row.id;
                setup.MaxTry = row.MaxTry;
                setup.Status = row.Status;

                break;
            }

            return setup;
        }

        private List<Setup> GetListSetupsFromTableAdapter(HiddenWordDAL.DataModel.DsHiddenWord.setupsDataTable source)
        {
            Setup setup = new Setup();
            List<Setup> listSetup = new List<Setup>();

            foreach (var row in source)
            {
                setup.Id = row.id;
                setup.MaxTry = row.MaxTry;
                setup.Status = row.Status;
                listSetup.Add(setup);
            }

            return listSetup;
        }

        //---------------[ The statistic implementation ] --------------------

        public List<Statistic> GetStatisticByNbTry(int nbTry)
        {
            throw new NotImplementedException();
        }

        public List<Statistic> GetStatisticBySetupId(int setupId)
        {
            statisticTableAdapter tabadapStat = new statisticTableAdapter();
            var statistics = tabadapStat.GetStatisticBySetupId(setupId);
            return GetListStatisticFromTableAdapter(statistics);
        }

        public List<Statistic> GetStatisticByUserId(int userId)
        {
            var statistics = tabadapStat.GetStatisticByUserId(userId);
            return GetListStatisticFromTableAdapter(statistics);
        }

        public List<Statistic> GetStatisticByWordId(int wordId)
        {
            statisticTableAdapter tabadapStat = new statisticTableAdapter();
            var statistics = tabadapStat.GetStatisticByWordId(wordId);
            return GetListStatisticFromTableAdapter(statistics);
        }

        public List<Statistic> GetStatisticData()
        {            
            var statistics = tabadapStat.GetData();
            return GetListStatisticFromTableAdapter(statistics);
        }

        public  Statistic getOneUserStatistics(int userId)
        {
            Statistic stat;
            Words word;
            Setup setup;
            User user;
            List<Statistic> statsList = new List<Statistic>();

            var stats = tabadapStat.GetStatisticByUserId(userId);
            stat = new Statistic();
            for (int i = 0; i < stats.Count(); i++)
            {
                word = new Words();
                setup = new Setup();
                user = new User();

                stat.WordList = new List<Words>();
                stat.UserList = new List<User>();
                stat.SetupList = new List<Setup>();
                for (int j = 0; j < stats.Rows[i].ItemArray.Count(); j++)
                {
                    user.Id = (int)stats.Rows[i].ItemArray[5];
                    user.Pseudo = (string)stats.Rows[i].ItemArray[6];

                    word.Id = (int)stats.Rows[i].ItemArray[7];
                    word.Name = (string)stats.Rows[i].ItemArray[8];

                    setup.Id = (int)stats.Rows[i].ItemArray[9];
                    setup.MaxTry = (int)stats.Rows[i].ItemArray[10];

                    /*stat.id = (int)stats.Rows[i].ItemArray[1];
                    stat.UserId = (int)stats.Rows[i].ItemArray[2];
                    stat.WordId = (int)stats.Rows[i].ItemArray[3];
                    stat.SetupId = (int)stats.Rows[i].ItemArray[4];
                    stat.NbTry = (int)stats.Rows[i].ItemArray[5];*/

                    stat.WordList.Add(word);
                    stat.SetupList.Add(setup);
                    stat.UserList.Add(user);
                }

            }  

            return stat;
        }


        public List<Statistic> getUsersStatistics()
        {
            //var tabadap = new usersTableAdapter();
            //var tabadapStat = new statisticTableAdapter();
            var users = tabadapUser.GetUsers();
            Statistic stat;
            Words word;
            Setup setup;
            List<User> userList = new List<User>();
            List<Statistic> statsList = new List<Statistic>();

            foreach (var row in users)
            {
                var stats = tabadapStat.GetStatisticByUserId(row.id);
                stat = new Statistic();
                stat.User.Id = row.id;
                stat.User.Pseudo = row.Pseudo;

                foreach (var rowStat in stats)
                {
                    word = new Words();
                    setup = new Setup();

                    word.Id = rowStat.wordsRow.id;
                    word.Name = rowStat.wordsRow.Name;
                    setup.Id = rowStat.setupsRow.id;
                    setup.MaxTry = rowStat.setupsRow.MaxTry;
                    stat.WordList.Add(word);
                    stat.SetupList.Add(setup);

                }

                statsList.Add(stat);

            }

            return statsList;
        }


        public Statistic insertStatistic(int userId, int wordId, int nbTry, int setupId)
        {
            tabadapStat.InsertStatistic(userId, wordId, nbTry, setupId);
            return new Statistic();
        }

        private List<Statistic> GetListStatisticFromTableAdapter(HiddenWordDAL.DataModel.DsHiddenWord.statisticDataTable source)
        {
            Statistic stats = new Statistic();
            List<Statistic> listStats = new List<Statistic>();

            foreach (var row in source)
            {
                stats.Id = row.id;
                stats.UserId = row.UserId;
                stats.SetupId = row.SetupId;
                stats.NbTry = row.NbTry;
                listStats.Add(stats);

            }

            return listStats;
        }

        //---------------[ The User implementation ] --------------------

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByPseudo(string pseudo)
        {            
            var users = tabadapUser.GetUserByPseudo(pseudo);
            return GetOneUserFromTableAdapter(users);
        }

        public List<User> GetUserData()
        {
            var users = tabadapUser.GetUsers();
            return GetListUsersFromTableAdapter(users);
        }

        public User InsertUser(string pseudo)
        {
            tabadapUser.InsertUser(pseudo);
            return new User();
        }


        private List<User> GetListUsersFromTableAdapter(HiddenWordDAL.DataModel.DsHiddenWord.usersDataTable source)
        {
            List<User> listUsers = new List<User>();
            User user;
            foreach (var row in source)
            {
                user = new User();
                user.Id = row.id;
                user.Pseudo = row.Pseudo;
                listUsers.Add(user);
            }

            return listUsers;
        }

        private User GetOneUserFromTableAdapter(HiddenWordDAL.DataModel.DsHiddenWord.usersDataTable source)
        {
            User user = new User();

            foreach (var row in source)
            {
                user.Pseudo = row.Pseudo;
                user.Id = row.id;
                break;
            }
            return user;
        }

        //---------------[ The word implementation ] --------------------

        public Words GetWordsById(int id)
        {
            throw new NotImplementedException();
        }

        public Words GetWordsByName(string name)
        {
            
            var words = tabadapWord.GetWordByName(name);
            Words responseWord = new Words();

            foreach (var row in words)
            {
                responseWord.Id = row.id;
                responseWord.Name = row.Name;

                break;
            }

            return responseWord;
        }

        public List<Words> GetWordsData()
        {
            var words = tabadapWord.GetWords();
            Words responseWord = new Words();
            List<Words> listWords = new List<Words>();
            Words word;
            foreach (var row in words)
            {
                word = new Words();
                word.Id = row.id;
                word.Name = row.Name;
                listWords.Add(word);
            }

            return listWords;
        }

        public Words InsertWord(string name)
        {
            tabadapWord.InsertWord(name);
            return new Words();
        }

        public Words DeleteWord(Words word)
        {
            throw new NotImplementedException();
        }

        public Words UpdateWord(Words word)
        {
            throw new NotImplementedException();
        }

        public User DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Statistic DeleteStatistic(Statistic statistic)
        {
            throw new NotImplementedException();
        }

        public Statistic UpdateStatistic(Statistic statistic)
        {
            throw new NotImplementedException();
        }

        public Setup DeleteSetup(Setup setup)
        {
            throw new NotImplementedException();
        }

        public Setup UpdateSetup(Setup setup)
        {
            throw new NotImplementedException();
        }
    }
}
