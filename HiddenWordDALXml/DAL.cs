
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HiddenWordCommon.classes;
using System.Xml.Serialization;
using System.IO;
using Xml = HiddenWordXMLSchema;
using System.Xml.Linq;
using HiddenWordCommon.Enums;
using HiddenWordCommon.Interfaces.DAL;
using HiddenWordDALXml.Classes;

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
            List<Xml.XSetups> paramList = new List<HiddenWordXMLSchema.XSetups>();
            paramList.Add(genericMethode.getXmlDataByAttribute<Xml.XSetups>("XSetups", "ID", id.ToString()));

            List<Setup> setupsList = bindXmlDataToSetup(paramList);
            Setup result = new Setup();
            if (result != null)
            {
                result = setupsList[0];
            }
            return result;
        }

        public List<Setup> GetSetupByMaxTry(int maxTry)
        {
            List<Xml.XSetups> resultList = genericMethode.getListXmlDataByValue<Xml.XSetups>("XSetups", maxTry.ToString());
            return bindXmlDataToSetup(resultList);
        }

        public List<Setup> GetSetupByStatus(int status)
        {
            List<Xml.XSetups> resultList = genericMethode.getListXmlDataByValue<Xml.XSetups>("XSetups", status.ToString());
            return bindXmlDataToSetup(resultList);
        }

        public Setup GetSetupActiveStatus()
        {
            List<Xml.XSetups> paramList = new List<HiddenWordXMLSchema.XSetups>();
            paramList.Add(genericMethode.getXmlDataByValue<Xml.XSetups>("XSetups", ((int)ESetup.Active).ToString()));

            List<Setup> setupsList = bindXmlDataToSetup(paramList);
            Setup result = new Setup();
            if (result != null)
            {
                result = setupsList[0];
            }
            return result;

        }

        public List<Setup> GetSetupData()
        {
            List<Xml.XSetups> resultList = genericMethode.getListXmlData<Xml.XSetups>("XSetups");
            return bindXmlDataToSetup(resultList);
        }

        public void InsertSetup(int maxTry, int status)
        {
            Xml.XSetups setup = new Xml.XSetups();
            setup.ID = genericMethode.autoIncrementXmlDataPrimaryKey("XSetups", "ID");
            setup.maxtry = maxTry;
            setup.status = status;

            Xml.HiddenWord hiddenWord = new Xml.HiddenWord();
            hiddenWord.Item = setup;
            genericMethode.saveXmlData(hiddenWord);
        }

        public Setup DeleteSetup(Setup setup)
        {
            return genericMethode.deleteXmlData<Setup>("XSetups", setup.Id.ToString());
        }

        public Setup UpdateSetup(Setup setup)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["ID"] = setup.Id.ToString();
            param["status"] = setup.Status.ToString();
            param["maxtry"] = setup.MaxTry.ToString();            

            return genericMethode.updateXmlData<Setup>("XSetups", param);
        }

        private List<Setup> bindXmlDataToSetup(List<Xml.XSetups> resultList)
        {
            List<Setup> listSetup = new List<Setup>();
            foreach (Xml.XSetups result in resultList)
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
            List<Xml.XStatistic> resultList = genericMethode.getListXmlDataByValueInnerJoin<Xml.XStatistic>("XStatistic", "nbtry", nbTry.ToString());
            return bindXmlObjectToStatistic(resultList);
        }

        public List<Statistic> GetStatisticBySetupId(int setupId)
        {
            List<Xml.XStatistic> resultList = genericMethode.getListXmlDataByValueInnerJoin<Xml.XStatistic>("XStatistic", "setupid", setupId.ToString());
            return bindXmlObjectToStatistic(resultList);
        }

        public List<Statistic> GetStatisticByUserId(int userId)
        {
            List<Xml.XStatistic> resultList = genericMethode.getListXmlDataByValueInnerJoin<Xml.XStatistic>("XStatistic", "userid", userId.ToString());
            return bindXmlObjectToStatistic(resultList);
        }

        public List<Statistic> GetStatisticByWordId(int wordId)
        {
            List<Xml.XStatistic> resultList = genericMethode.getListXmlDataByValueInnerJoin<Xml.XStatistic>("XStatistic", "userid", wordId.ToString());
            return bindXmlObjectToStatistic(resultList);
        }

        public List<Statistic> GetStatisticData()
        {
            List<Xml.XStatistic> resultList = genericMethode.getListXmlData<Xml.XStatistic>("XStatistic");
            return bindXmlObjectToStatistic(resultList);
        }
        

        public List<Statistic> getUsersStatistics()
        {            
            throw new NotImplementedException();
        }


        public void insertStatistic(int userId, int wordId, int nbTry, int setupId)
        {
            Xml.XStatistic statistic = new Xml.XStatistic();
            statistic.ID = genericMethode.autoIncrementXmlDataPrimaryKey("XStatistic", "ID");
            statistic.userid = userId;
            statistic.wordid = wordId;
            statistic.setupid = setupId;
            statistic.nbtry = nbTry;

            Xml.HiddenWord hiddenWord = new Xml.HiddenWord();
            hiddenWord.Item = statistic;

            genericMethode.saveXmlData(hiddenWord);
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

        private List<Statistic> bindXmlObjectToStatistic(List<HiddenWordXMLSchema.XStatistic> xmlOjectList )
        {
            List<Statistic> listStatistic = new List<Statistic>();
            foreach (Xml.XStatistic result in xmlOjectList)
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
            List<Xml.XUsers> paramList = new List<HiddenWordXMLSchema.XUsers>();
            paramList.Add(genericMethode.getXmlDataByAttribute<Xml.XUsers>("XUsers", "ID", id.ToString()));

            List<User> usersList = bindXmlDataToUser(paramList);
            User result = new User();
            if (result != null)
            {
                result = usersList[0];
            }
            return result;
        }

        public User GetUserByPseudo(string pseudo)
        {
            List<Xml.XUsers> paramList = new List<HiddenWordXMLSchema.XUsers>();
            paramList.Add(genericMethode.getXmlDataByValue<Xml.XUsers>("XUsers", pseudo));

            List<User> usersList = bindXmlDataToUser(paramList);
            User result = new User();
            if (result != null)
            {
                result = usersList[0];
            }
            return result;
        }
               
        public List<User> GetUserData()
        {
            List<Xml.XUsers> resultList = genericMethode.getListXmlData<Xml.XUsers>("XUsers");
            return bindXmlDataToUser(resultList);
        }
        
        public void InsertUser(string pseudo)
        {
            Xml.XUsers user = new Xml.XUsers();
            user.ID = genericMethode.autoIncrementXmlDataPrimaryKey("XUsers", "ID");
            user.pseudo = pseudo;

            Xml.HiddenWord hiddenWord = new Xml.HiddenWord();
            hiddenWord.Item = user;

            genericMethode.saveXmlData(hiddenWord);

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

        private List<User> bindXmlDataToUser(List<Xml.XUsers> resultList)
        {
            List<User> userFound = new List<User>();
            foreach (Xml.XUsers result in resultList)
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
            List<Xml.XWords> paramList = new List<HiddenWordXMLSchema.XWords>();
            paramList.Add(genericMethode.getXmlDataByAttribute<Xml.XWords>("XWords", "ID", id.ToString()));

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
            List<Xml.XWords> paramList = new List<HiddenWordXMLSchema.XWords>();
            paramList.Add(genericMethode.getXmlDataByValue<Xml.XWords>("XWords", name));

            List<Words> wordsList = bindXmlDataToWord(paramList);
            Words result = new Words(); 
            if (result != null)
            {
                result = wordsList[0];
            }
            return result;
        }

        public List<Words> GetWordsData()
        {
            List<Xml.XWords> resultList = genericMethode.getListXmlData<Xml.XWords>("XWords");
            return bindXmlDataToWord(resultList);
        }

        public void InsertWord(string name)
        {
            Xml.XWords word = new Xml.XWords();
            word.ID = genericMethode.autoIncrementXmlDataPrimaryKey("XWords", "ID");
            word.name = name;
            
            Xml.HiddenWord hiddenWord = new Xml.HiddenWord();
            hiddenWord.Item = word;
            genericMethode.saveXmlData(hiddenWord);
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

        private List<Words> bindXmlDataToWord(List<Xml.XWords> resultList)
        {
            List<Words> listWords = new List<Words>();
            foreach (Xml.XWords result in resultList)
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
