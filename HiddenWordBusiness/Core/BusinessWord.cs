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
        public IDALActionManager DALAcess { get; set; }

        public BusinessWord(IDALActionManager DAL )
        {
            DALAcess = DAL;
        }

        //---------------[ The word implementation ] --------------------

        public Words GetWordsById(int id)
        {
            return DALAcess.GetWordsById(id);
        }

        public Words GetWordsByName(string name)
        {
            return DALAcess.GetWordsByName(name);
        }

        public List<Words> GetWordsData()
        {
            return DALAcess.GetWordsData();
        }

        public void InsertWord(string name)
        {
            DALAcess.InsertWord(name);
        }

        public Words getNewRandomWord(Random rd)
        {
            List<Words> ListResult = DALAcess.GetWordsData();

            if (ListResult.Count == 0) 
                throw new ApplicationException("No word found in Database!");
            
            return ListResult[rd.Next(0, ListResult.Count - 1)];
        }

        public Words DeleteWord(Words word)
        {
            Words result = DALAcess.DeleteWord(word);
            if ( result != null )
                throw new ApplicationException(new StringBuilder().AppendFormat("Error occured while deleting the word '{0}'", word.Name).ToString());

            return result;
        }

        public Words UpdateWord(Words word)
        {
            return DALAcess.UpdateWord(word);
        }
    }
}
