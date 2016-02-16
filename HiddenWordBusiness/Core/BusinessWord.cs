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

        public BusinessWord(IDALActionManager DAL )
        {
            DALAccess = DAL;
        }

        //---------------[ The word implementation ] --------------------

        public Words GetWordsById(int id)
        {
            return DALAccess.GetWordsById(id);
        }

        public Words GetWordsByName(string name)
        {
            return DALAccess.GetWordsByName(name);
        }

        public List<Words> GetWordsData()
        {
            return DALAccess.GetWordsData();
        }

        public void InsertWord(string name)
        {
            DALAccess.InsertWord(name);
        }

        public Words getNewRandomWord(Random rd)
        {
            List<Words> ListResult = DALAccess.GetWordsData();

            if (ListResult.Count == 0) 
                throw new ApplicationException("No word found in Database!");
            
            return ListResult[rd.Next(0, ListResult.Count - 1)];
        }

        public Words DeleteWord(Words word)
        {
            Words result = DALAccess.DeleteWord(word);
            if ( result != null )
                throw new ApplicationException(new StringBuilder().AppendFormat("Error occured while deleting the word '{0}'", word.Name).ToString());

            return result;
        }

        public Words UpdateWord(Words word)
        {
            return DALAccess.UpdateWord(word);
        }
    }
}
