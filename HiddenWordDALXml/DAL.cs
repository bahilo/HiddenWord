
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HiddenWordCommon.classes;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using HiddenWordCommon.Enums;
using HiddenWordCommon.Interfaces.DAL;
using HiddenWordDALXml.Classes;
using HiddenWordDALXml.XmlManager;

namespace HiddenWordDALXml
{
    public class DAL : IDALActionManager
    {
        GenericMethodes genericMethode;

        public DAL()
        {
            genericMethode = new GenericMethodes();
        }

        //---------------[ The setup implementation ] --------------------

        public Setup GetSetupById(int id)
        {
            List<XSetups> paramList = new List<XSetups>();
            paramList.Add(genericMethode.getXmlDataByAttribute<XSetups>("XSetups", "ID", id.ToString()));

            List<Setup> setupsList = bindXmlDataToSetup(paramList);
            Setup result = new Setup();
            if (setupsList.Count != 0)
            {
                result = setupsList[0];
            }
            return result;
        }

        public List<Setup> GetSetupByMaxTry(int maxTry)
        {
            List<XSetups> resultList = genericMethode.getListXmlDataByValue<XSetups>("XSetups","maxtry", maxTry.ToString());
            return bindXmlDataToSetup(resultList);
        }

        public List<Setup> GetSetupByStatus(int status)
        {
            List<XSetups> resultList = genericMethode.getListXmlDataByValue<XSetups>("XSetups", "status", status.ToString());
            return bindXmlDataToSetup(resultList);
        }

        public Setup GetSetupActiveStatus()
        {
            List<XSetups> paramList = new List<XSetups>();
            paramList.Add(genericMethode.getXmlDataByValue<XSetups>("XSetups", ((int)ESetup.Active).ToString()));

            List<Setup> setupsList = bindXmlDataToSetup(paramList);
            Setup result = new Setup();
            if ( setupsList.Count != 0 )
            {
                result = setupsList[0];
            }
            return result;

        }

        public List<Setup> GetSetupData()
        {
            List<XSetups> resultList = genericMethode.getListXmlData<XSetups>("XSetups");
            return bindXmlDataToSetup(resultList);
        }

        public Setup InsertSetup(int maxTry, int status)
        {
            XSetups setup = new XSetups();
            setup.ID = genericMethode.autoIncrementXmlDataPrimaryKey("XSetups", "ID");
            setup.maxtry = maxTry;
            setup.status = status;

            HiddenWord hiddenWord = new HiddenWord();
            hiddenWord.Item = setup;

            List<XSetups> paramList = new List<XSetups>();
            paramList.Add(genericMethode.saveXmlData<XSetups>(hiddenWord, "XSetups", setup.ID.ToString()));

            List<Setup> setupsList = bindXmlDataToSetup(paramList);
            Setup result = new Setup();
            if (setupsList.Count != 0)
            {
                result = setupsList[0];
            }
            return result;
        }

        public Setup DeleteSetup(Setup setup)
        {
            return genericMethode.deleteXmlData<Setup>("XSetups", setup.Id.ToString());
        }

        public Setup UpdateSetup(Setup setup)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            
            //if (setup.Status == (int)ESetup.Active)
            {
                var xSetup = genericMethode.getXmlDataByValue<XSetups>("XSetups", ((int)ESetup.Active).ToString());
                param["ID"] = xSetup.ID.ToString();
                param["status"] = ((int)ESetup.NotActive).ToString();
                param["maxtry"] = xSetup.maxtry.ToString();
                genericMethode.updateXmlData<Setup>("XSetups", param);
            }

            param["ID"] = setup.Id.ToString();
            param["status"] = setup.Status.ToString();
            param["maxtry"] = setup.MaxTry.ToString();

            return genericMethode.updateXmlData<Setup>("XSetups", param);
        }

        private List<Setup> bindXmlDataToSetup(List<XSetups> resultList)
        {
            List<Setup> listSetup = new List<Setup>();
            foreach (XSetups result in resultList)
            {
                Setup setupTmp = new Setup();
                setupTmp.Id = result.ID;
                setupTmp.MaxTry = result.maxtry;
                setupTmp.Status = result.status;
                listSetup.Add(setupTmp);
            }
            return listSetup;
        }

        //---------------[ The statistic implementation ] --------------------

        public List<Statistic> GetStatisticByNbTry(int nbTry)
        {
            List<XStatistic> resultList = genericMethode.getListXmlDataByValueInnerJoin<XStatistic>("XStatistic", "nbtry", nbTry.ToString());
            return bindXmlObjectToStatistic(resultList);
        }

        public List<Statistic> GetStatisticBySetupId(int setupId)
        {
            List<XStatistic> resultList = genericMethode.getListXmlDataByValueInnerJoin<XStatistic>("XStatistic", "setupid", setupId.ToString());
            return bindXmlObjectToStatistic(resultList);
        }

        public List<Statistic> GetStatisticByUserId(int userId)
        {
            List<XStatistic> resultList = genericMethode.getListXmlDataByValueInnerJoin<XStatistic>("XStatistic", "userid", userId.ToString());
            return bindXmlObjectToStatistic(resultList);
        }

        public List<Statistic> GetStatisticByWordId(int wordId)
        {
            List<XStatistic> resultList = genericMethode.getListXmlDataByValueInnerJoin<XStatistic>("XStatistic", "userid", wordId.ToString());
            return bindXmlObjectToStatistic(resultList);
        }

        public List<Statistic> GetStatisticData()
        {
            List<XStatistic> resultList = genericMethode.getListXmlData<XStatistic>("XStatistic");
            return bindXmlObjectToStatistic(resultList);
        }
        

        public List<Statistic> getUsersStatistics()
        {            
            throw new NotImplementedException();
        }


        public Statistic insertStatistic(int userId, int wordId, int nbTry, int setupId)
        {
            XStatistic statistic = new XStatistic();
            statistic.ID = genericMethode.autoIncrementXmlDataPrimaryKey("XStatistic", "ID");
            statistic.userid = userId;
            statistic.wordid = wordId;
            statistic.setupid = setupId;
            statistic.nbtry = nbTry;

            HiddenWord hiddenWord = new HiddenWord();
            hiddenWord.Item = statistic;

            List<XStatistic> paramList = new List<XStatistic>();
            paramList.Add(genericMethode.saveXmlData<XStatistic>(hiddenWord, "XStatistic", statistic.ID.ToString()));

            List<Statistic> statisticList = bindXmlObjectToStatistic(paramList);
            Statistic result = new Statistic();
            if (statisticList.Count != 0)
            {
                result = statisticList[0];
            }

            return result;


        }

        public Statistic DeleteStatistic(Statistic statistic)
        {
            return genericMethode.deleteXmlData<Statistic>("XStatistic", statistic.Id.ToString());
        }

        public Statistic UpdateStatistic(Statistic statistic)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["ID"] = statistic.Id.ToString();
            param["nbtry"] = statistic.NbTry.ToString();
            param["setupid"] = statistic.SetupId.ToString();
            param["wordid"] = statistic.WordId.ToString();
            param["userid"] = statistic.UserId.ToString();            

            return genericMethode.updateXmlData<Statistic>("XStatistic", param);
        }

        private List<Statistic> bindXmlObjectToStatistic(List<XStatistic> xmlOjectList )
        {
            List<Statistic> listStatistic = new List<Statistic>();
            foreach (XStatistic result in xmlOjectList)
            {
                Statistic statisticTmp = new Statistic();
                statisticTmp.Id = result.ID;
                statisticTmp.NbTry = result.nbtry;
                statisticTmp.SetupId = result.setupid;
                statisticTmp.WordId = result.wordid;
                statisticTmp.UserId = result.userid;
                listStatistic.Add(statisticTmp);
            }

            return listStatistic;
        }

        //---------------[ The User implementation ] --------------------

        public User GetUserById(int id)
        {
            List<XUsers> paramList = new List<XUsers>();
            paramList.Add(genericMethode.getXmlDataByAttribute<XUsers>("XUsers", "ID", id.ToString()));

            List<User> usersList = bindXmlDataToUser(paramList);
            User result = new User();
            if (usersList.Count != 0)
            {
                result = usersList[0];
            }
            return result;
        }

        public User GetUserByPseudo(string pseudo)
        {
            List<XUsers> paramList = new List<XUsers>();
            paramList.Add(genericMethode.getXmlDataByValue<XUsers>("XUsers", pseudo));

            List<User> usersList = bindXmlDataToUser(paramList);
            User result = new User();
            if ( usersList.Count != 0 )
            {
                result = usersList[0];
            }
            return result;
        }
               
        public List<User> GetUserData()
        {
            List<XUsers> resultList = genericMethode.getListXmlData<XUsers>("XUsers");
            return bindXmlDataToUser(resultList);
        }
        
        public User InsertUser(string pseudo)
        {
            XUsers user = new XUsers();
            user.ID = genericMethode.autoIncrementXmlDataPrimaryKey("XUsers", "ID");
            user.pseudo = pseudo;

            HiddenWord hiddenWord = new HiddenWord();
            hiddenWord.Item = user;
            
            List<XUsers> paramList = new List<XUsers>();
            paramList.Add(genericMethode.saveXmlData<XUsers>(hiddenWord, "XUsers", user.ID.ToString()));

            List<User> usersList = bindXmlDataToUser(paramList);
            User result = new User();
            if (usersList.Count != 0)
            {
                result = usersList[0];
            }
            return result;

        }

        public User DeleteUser(User user)
        {
            return genericMethode.deleteXmlData<User>("XUsers", user.Id.ToString());
        }

        public User UpdateUser(User user)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["ID"] = user.Id.ToString();
            param["pseudo"] = user.Pseudo.ToString();            

            return genericMethode.updateXmlData<User>("XUsers", param);
        }

        private List<User> bindXmlDataToUser(List<XUsers> resultList)
        {
            List<User> userFound = new List<User>();
            foreach (XUsers result in resultList)
            {
                User userTemp = new User();
                userTemp.Id = result.ID;
                userTemp.Pseudo = (string)result.pseudo;
                userFound.Add(userTemp);
            }
            return userFound;
        }
        //---------------[ The word implementation ] --------------------

        public Words GetWordsById(int id)
        {
            List<XWords> paramList = new List<XWords>();
            paramList.Add(genericMethode.getXmlDataByAttribute<XWords>("XWords", "ID", id.ToString()));

            List<Words> wordsList = bindXmlDataToWord(paramList);
            Words result = new Words();
            if (result != null)
            {
                result = wordsList[0];
            }
            return result;
            
        }

        public Words GetWordsByName(string name)
        {
            List<XWords> paramList = new List<XWords>();
            paramList.Add(genericMethode.getXmlDataByValue<XWords>("XWords", name));

            List<Words> wordsList = bindXmlDataToWord(paramList);
            Words result = new Words(); 
            if (wordsList.Count != 0)
            {
                result = wordsList[0];
            }
            return result;
        }

        public List<Words> GetWordsData()
        {
            List<XWords> resultList = genericMethode.getListXmlData<XWords>("XWords");
            return bindXmlDataToWord(resultList);
        }

        public Words InsertWord(string name)
        {
            XWords word = new XWords();
            word.ID = genericMethode.autoIncrementXmlDataPrimaryKey("XWords", "ID");
            word.name = name;
            
            HiddenWord hiddenWord = new HiddenWord();
            hiddenWord.Item = word;

            List<XWords> paramList = new List<XWords>();
            paramList.Add(genericMethode.saveXmlData<XWords>(hiddenWord, "XWords", word.ID.ToString()));

            List<Words> wordsList = bindXmlDataToWord(paramList);
            Words result = new Words();
            if ( wordsList.Count != 0 )
            {
                result = wordsList[0];
            }
            return result;

        }

        public Words DeleteWord(Words word)
        {
            return genericMethode.deleteXmlData<Words>("XWords", word.Id.ToString());
        }

        public Words UpdateWord(Words word)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["ID"] = word.Id.ToString();
            param["name"] = word.Name.ToString();            

            return genericMethode.updateXmlData<Words>("XWords", param);
        }

        private List<Words> bindXmlDataToWord(List<XWords> resultList)
        {
            List<Words> listWords = new List<Words>();
            foreach (XWords result in resultList)
            {
                Words wordTmp = new Words();
                wordTmp.Id = result.ID;
                wordTmp.Name = (string)result.name;
                listWords.Add(wordTmp);
            }

            return listWords;
        }


        



    }
}
