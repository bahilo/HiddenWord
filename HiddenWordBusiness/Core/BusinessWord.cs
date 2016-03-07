using HiddenWordBusiness.Exceptions;
using HiddenWordCommon.classes;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordCommon.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordBusiness.Core
{
    public class BusinessWord : HiddenWordCommon.Interfaces.Business.IWordsManager
    {
        public IDALActionManager DALAccess { get; set; }
        public ExceptionGenerator exceptionGenerator { get; set; }

        public BusinessWord(IDALActionManager DAL )
        {
            DALAccess = DAL;
            exceptionGenerator = new ExceptionGenerator();
        }

        //---------------[ The word implementation ] --------------------

        public Words GetWordsById(int id)
        {
            Words result = DALAccess.GetWordsById(id);
            
            // Throwing exception for null value
            exceptionGenerator.exceptionFor<Words>(
                "read", 
                new StringBuilder().AppendFormat("Error occured while retiving a word from the database with id '{0}'!", id).ToString(),
                result,
                isExludeNullable: true);            

            return result;
        }

        public Words GetWordsByName(string name)
        {
            var result = DALAccess.GetWordsByName(name);
            
            // Throwing exception for null value
            exceptionGenerator.exceptionFor<Words>(
                "read",
                new StringBuilder().AppendFormat("Error occured while retiving this word from the database '{0}'!", name).ToString(),
                result,
                isExludeNullable: true);

            return result;
        }

        public List<Words> GetWordsData()
        {
            var result = DALAccess.GetWordsData();           

            // Throwing Value of 0 exception
            exceptionGenerator.exceptionFor<int>(
                "read",
                new StringBuilder().AppendFormat("No data found in the database!").ToString(),
                result.Count(),
                excludeValue: 0,
                isExludeNullable: false);

            return result;
        }

        public void InsertWord(string name)
        {
            exceptionGenerator.exceptionFor<string>(
                "read",
                new StringBuilder().AppendFormat("Word Error, Word's name cannot be null").ToString(),
                name);

            //Throwing exception for Value greater than 0 
            exceptionGenerator.exceptionFor<Words>(
                "insert",
                new StringBuilder().AppendFormat("Error occured while deleting the word '{0}'", name).ToString(),
                DALAccess.InsertWord(name),
                excludeValueGreaterThan: 0,
                isExludeNullable: false);


        }

        public Words getNewRandomWord(Random rd)
        {
            List<Words> ListResult = DALAccess.GetWordsData();

            // Throwing Value of 0 exception
            exceptionGenerator.exceptionFor<int>(
                "read",
                new StringBuilder().AppendFormat("No words found in Database!").ToString(),
                ListResult.Count(),
                excludeValue: 0,
                isExludeNullable: false);
            
            return ListResult[rd.Next(0, ListResult.Count - 1)];
        }

        public Words DeleteWord(Words word)
        {
            Words result = DALAccess.DeleteWord(word);
            
            // Throwing exception for null value
            exceptionGenerator.exceptionFor<Words>(
                "delete",
                new StringBuilder().AppendFormat("Error occured while deleting the word '{0}'", word.Name).ToString(),
                result,
                isExludeNullable: true);

            return result;
        }

        public Words UpdateWord(Words word)
        {
            var result = DALAccess.UpdateWord(word);

            // Throwing exception for null value
            exceptionGenerator.exceptionFor<Words>(
                "update",
                new StringBuilder().AppendFormat("Error occured while updating the new word '{0}'", word.Name).ToString(),
                result,
                isExludeNullable: true);

            return result;
        }
    }
}
