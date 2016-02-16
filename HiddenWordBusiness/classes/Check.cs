using System;
using System.Linq;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordCommon.classes;

namespace HiddenWordBusiness.classes
{
    public class Check : ICheck
    {
        public string Error { get; set; }
        public string[] Game { get; set; }
        public String Word { get; set; }
        public IActionManager Bl { get; set; }

        public Check(IActionManager bl)
        {
            Bl = bl;
        }

        public void init()
        {
            Game = new string[Word.Count()];
            Game = convertStringToEmptyArrayOfTring(this.Word);
            Error = "";
        }

        public string[] convertStringToEmptyArrayOfTring(string source)
        {
            string[] StringTab = new string[source.Count()];
            for (int i = 0; i < source.Count(); i++)
            {
                StringTab[i] = "_";
            }

            return StringTab;
        }

        public bool isCorrectCharater(string userTry)
        {
            bool isCorrect = false;

            if (Word.Contains(userTry))
            {
                for (int i = 0; i < Game.Count(); i++ )
                {
                    if (userTry.Equals("" + Word[i])) { 
                        Game[i] = "" + Word[i];                        
                    }
                }
                isCorrect = true;
            }
            else { 
                Error +="###########\n" +
                        "##   X   ##\n" +
                        "###########";
            }
            return isCorrect;
        }

        public bool checkWin()
        {
            //string buf1, buf2;
            for (int i = 0 ; i < Word.Count(); i++ )
            {
                if(!Game[i].Equals(""+Word[i])){                    
                    return false;
                }
            }
            return true;
        }


        public void displayGame()
        {
            Bl.BlDisplay.displayEmptyLine();
            foreach (string charact in Game)
            {
                Bl.BlDisplay.displayMessage(charact + " ",0,0);
            }
            Bl.BlDisplay.displayEmptyLine();
        }

        public void displayError()
        {
            Bl.BlDisplay.displayMessage(Error,0,0);
        }


        public bool startupRequirement(User user, Setup setup)
        {
            if (user == null || user.Pseudo == null || user.Pseudo == "")
            {
                Bl.BlDisplay.displayMessage("No user selected!", 2, 2, 2);
                
                return false;
            }
            if (setup == null || setup.MaxTry == 0)
            {
                Bl.BlDisplay.displayMessage("No maximun try setup!",2,2, 2);
                return false;
            }
            return true;           
        }


    }
}
