using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordCommon.Interfaces.Business
{
    public interface ICheck
    {
        bool isCorrectCharater(string userTry);
        bool checkWin();
        void displayGame();
        void displayError();
    }
}
