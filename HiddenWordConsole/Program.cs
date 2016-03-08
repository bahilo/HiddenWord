using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HiddenWordBusiness.classes;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordBusiness.Core;
using HiddenWord.Business;
using HiddenWordConsole.Classes;

namespace HiddenWordConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IDisplay blDisplay = new BusinessDisplay(   new Display(),
                                                        new BusinessStatistic(new HiddenWordDALXml.DAL()),
                                                        new BusinessWord(new HiddenWordDALXml.DAL()),
                                                        new BusinessSetup(new HiddenWordDALXml.DAL()),
                                                        new BusinessUser(new HiddenWordDALXml.DAL()));
            BL Bl = new BL( new BusinessStatistic(new HiddenWordDALXml.DAL()),
                            new BusinessWord(new HiddenWordDALXml.DAL()),
                            new BusinessSetup(new HiddenWordDALXml.DAL()),
                            new BusinessUser(new HiddenWordDALXml.DAL()),
                            new HiddenWordDALXml.DAL(),
                            blDisplay);

            Bl.BlDisplay.displayWelcomeScreen();

            Game game = new Game(Bl);

            game.run();
        }

    }
}
