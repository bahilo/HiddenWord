using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
using HiddenWordCommon.Interfaces.Business;
using System;

namespace HiddenWordBusiness.Core
{
    public class BusinessDisplay : IDisplay
    {
        public IDisplay Display { get; set; }
        private IStatisticManager BlStat;
        private IWordsManager BlWord;
        private ISetupsManager BlSetup;
        private IUsersManager BlUser;

        public BusinessDisplay( IDisplay display,
                                IStatisticManager blStat,
                                IWordsManager blWord,
                                ISetupsManager blSetup,
                                IUsersManager blUser)
        {
            this.Display = display;
            this.BlStat = blStat;
            this.BlWord = blWord;
            this.BlSetup = blSetup;
            this.BlUser = blUser;
        }

        /*-------------------[ setupMenu ]--------------*/

        public string setupMenu()
        {
            return Display.setupMenu();
        }

        /*-------------------[ Setup New Word ]--------------*/

        public Words setupNewWord()
        {
            Words result = Display.setupNewWord();
            var checkWordRegistered = BlWord.GetWordsByName(result.Name);
            if ( !result.Name.Equals("") && checkWordRegistered.Name == null)
            {
                BlWord.InsertWord(result.Name);
                checkWordRegistered = BlWord.GetWordsByName(result.Name);
            }

            return checkWordRegistered;
        }

        /*-----------------[ Exit Game]--------------*/

        void IDisplay.exitGame()
        {
            Display.exitGame();
        }


        /*-----------------[ End Game]--------------*/

        public void endGame(string solution)
        {
            Display.endGame(solution);
        }


        /*-------------------[ display Startup Menu ]--------------*/

        public string displayStartupMenu()
        {
            return Display.displayStartupMenu();
        }


        /*-------------------[ Read user menu input ]--------------*/

        public string read(string message)
        {
            return Display.read(message);
        }


        /*-------------------[ display Welcome Screen ]--------------*/

        public void displayWelcomeScreen()
        {
            Display.displayWelcomeScreen();
        }


        /*-------------------[ display Prompt ]--------------*/

        public void displayPrompt(string pseudo)
        {
            Display.displayPrompt(pseudo);
        }


        /*-------------------[ Display Congratulation ]--------------*/

        public void DisplayCongratulation()
        {
            Display.DisplayCongratulation();
        }


        /*-------------------[ display Warning MaxTry ]--------------*/

        public void displayWarningMaxTry(int maxTry)
        {
            Display.displayWarningMaxTry(maxTry);
        }

        /*-------------------[ User Selection ]--------------*/

        public User SelectUser()
        {
            return getUser(Display.SelectUser());
        }

        public User CreateUser()
        {
            return getUser(Display.CreateUser());
        }

        private User getUser(User user)
        {
            var checkUserRegistered = BlUser.GetUserByPseudo(user.Pseudo);
            if ( !user.Pseudo.Equals("") && checkUserRegistered.Pseudo == null)
            {
                BlUser.InsertUser(user.Pseudo);
                checkUserRegistered = BlUser.GetUserByPseudo(user.Pseudo);
            }

            checkUserRegistered.UserStats = BlStat.GetStatisticByUserId(checkUserRegistered.Id);

            foreach (var stat in checkUserRegistered.UserStats)
            {
                checkUserRegistered.UserWordsStats.Add(BlWord.GetWordsById(stat.WordId));
                checkUserRegistered.UserSetupsStats.Add(BlSetup.GetSetupById(stat.SetupId));
            }

            return checkUserRegistered;
        }

        /*-------------------[ Setup Max Try ]--------------*/

        public Setup setupMaxTry()
        {
            Setup result = Display.setupMaxTry();
            if (result.MaxTry > 13)
                throw new ApplicationException("The maximun try cannot be greater than 13!");
            var checkSetupRegistered = BlSetup.GetSetupByMaxTry(result.MaxTry);
            if (result.MaxTry != 0 && checkSetupRegistered.Count == 0)
            {
                BlSetup.InsertSetup(result.MaxTry, (int)ESetup.Active);
                checkSetupRegistered = BlSetup.GetSetupByStatus((int)ESetup.Active);                
                return checkSetupRegistered[0];
            }
            else if( result.MaxTry != 0 )
            {
                checkSetupRegistered[0].Status = (int)ESetup.Active;
                BlSetup.UpdateSetup(checkSetupRegistered[0]);
                return checkSetupRegistered[0];
            }

            return null;
        }

        /*-------------------[ Display Message ]--------------*/

        public void displayMessage(string message, int nbEmptyLineBefore = 0, int nbEmptyLineAfter = 0, int nbTabulation = 0)
        {
            displayEmptyLine(nbEmptyLineBefore);
            displayTabulation(nbTabulation);
            Display.displayMessage(message);
            displayEmptyLine(nbEmptyLineAfter);
        }

        private void displayEmptyLine(int nbLine)
        {
            for (int i = 1; i <= nbLine; i++)
            {
                Display.displayMessage("\n");
            }
        }

        private void displayTabulation(int nbTab)
        {
            for (int i = 1; i <= nbTab; i++)
            {
                Display.displayMessage("\t");
            }
        }


        private void displayTabulation(int? nbTab)
        {
            for (int i = 1; i <= nbTab; i++)
            {
                Display.displayMessage("\t");
            }
        }

        /*-------------------[ Display table game ]--------------*/

        public void displayGame(string[][] gameTable, int indexLine, int indexCol, int indexCurrentLine, EPosition[][] TrackPosition)
        {
            Display.displayGame(gameTable, indexLine, indexCol, indexCurrentLine, TrackPosition);
        }

        /*-------------------[ Read user try ]--------------*/

        public string readResponse(string wordName)
        {
            string response = Display.readResponse("Response: ");
            if ( response == null || response.Length < wordName.Length )
                throw new ApplicationException("Your response must be at least " + wordName.Length + " characters!");
            return response;
        }

        /*-------------------[ Display User Statistics ]--------------*/

        public void DisplayStatisticByUser(User user)
        {
            Display.DisplayStatisticByUser(user);
        }
    }
}
