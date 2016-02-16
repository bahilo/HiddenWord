using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiddenWordCommon.classes;
using HiddenWordCommon.Interfaces.Business;

namespace HiddenWordBusiness.classes
{
    public class EndGame
    {
        public string EndMessage { get; set; }
        public IActionManager Bl;

        public EndGame(IActionManager bl)
        {
            this.Bl = bl;
        }
        public EndGame(IActionManager bl, User user, Words word, int nbTry, Setup setup)
        {
            this.Bl = bl;
            Bl.BlDisplay.endGame(word.Name);
            save(user, word, nbTry, setup);
        }

        public void save(User user, Words word, int nbTry, Setup setup)
        {
            Bl.BlStat.insertStatistic(user.Id, word.Id, nbTry, setup.Id);
        }

        public void exitGame()
        {
            Bl.BlDisplay.exitGame();
        }
    }
}
