using HiddenWordCommon.Interfaces.Business;
using HiddenWordCommon.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HiddenWord.Business
{
    public class BL : IActionManager 
    {

        public IDALActionManager DALAccess { get; set; }
        public HiddenWordCommon.Interfaces.Business.IUsersManager BlUser { get; set; }
        public HiddenWordCommon.Interfaces.Business.ISetupsManager BlSetup { get; set; }
        public HiddenWordCommon.Interfaces.Business.IStatisticManager BlStat { get; set; }
        public HiddenWordCommon.Interfaces.Business.IWordsManager BlWord { get; set; }
        public IDisplay BlDisplay { get; set; }
        //public IDisplay DisplayGUI { get; set; }


        /*public BL()
        {
            DALAcess = new DAL();
            this.BlStat = blStat;
            this.BlWord = blWord;
            this.BlSetup = blSetup;
            this.BlUser = blUser;
        }*/

        public BL(  HiddenWordCommon.Interfaces.Business.IStatisticManager blStat,
                    HiddenWordCommon.Interfaces.Business.IWordsManager blWord,
                    HiddenWordCommon.Interfaces.Business.ISetupsManager blSetup,
                    HiddenWordCommon.Interfaces.Business.IUsersManager blUser,
                    IDALActionManager DAL,
                    IDisplay display)
        {
            DALAccess = DAL;
            this.BlStat = blStat;
            this.BlWord = blWord;
            this.BlSetup = blSetup;
            this.BlUser = blUser;
            BlDisplay = display;
        }

        /*private static IActionManager _instance;
        public static IActionManager instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BL(HiddenWordCommon.Interfaces.Business.IStatisticManager blStat,
                                        HiddenWordCommon.Interfaces.Business.IWordsManager blWord,
                                        HiddenWordCommon.Interfaces.Business.ISetupsManager blSetup,
                                        HiddenWordCommon.Interfaces.Business.IUsersManager blUser,
                                        IDALActionManager DAL);

                return _instance;
            }
            set { }
        }*/


    }
}
