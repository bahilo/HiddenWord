using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordCommon.Interfaces.Business
{
    public interface IGame : ICheck
    {
        //search for user charactere
        bool checkCharater(string userTry);
        



    }
}
