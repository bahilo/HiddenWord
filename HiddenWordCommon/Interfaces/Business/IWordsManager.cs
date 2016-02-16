using HiddenWordCommon.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordCommon.Interfaces.Business
{
    public interface IWordsManager
    {
        List<Words> GetWordsData();
        Words GetWordsById(int id);
        Words GetWordsByName(string name);
        void InsertWord(string name);
        Words getNewRandomWord(Random rd);
        Words DeleteWord(Words word);
        Words UpdateWord(Words word);
    }
}
