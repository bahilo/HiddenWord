
using HiddenWordCommon.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordCommon.Interfaces.DAL
{
    public interface IWordsManager
    {
        List<Words> GetWordsData();
        Words GetWordsById(int id);
        Words GetWordsByName(string name);
        Words InsertWord(string name);
        Words DeleteWord(Words word);
        Words UpdateWord(Words word);
    }
}
