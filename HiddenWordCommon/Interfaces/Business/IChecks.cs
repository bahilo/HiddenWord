using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordCommon.Interfaces.Business
{
    public interface ICheck
    {
        bool isCorrectCharater();
        void charaterPosition(string userTry);
        bool checkWin();
        void displayError();
    }
}
