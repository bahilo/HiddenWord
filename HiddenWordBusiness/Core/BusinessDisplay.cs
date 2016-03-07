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

        public string displayStartupMenu()
        {
            return Display.displayStartupMenu();
        }

        public string readScreen(string message)
        {
            return Display.readScreen(message);
        }

        public void displayWelcomeScreen()
        {
            Display.displayWelcomeScreen();
        }

        public void displayPrompt(string pseudo)
        {
            Display.displayPrompt(pseudo);
        }

        public void DisplayCongratulation()
        {
            Display.DisplayCongratulation();
        }

        public void displayWarningMaxTry(int maxTry)
        {
            Display.displayWarningMaxTry(maxTry);
        }

        public void displayEmptyLine(int? nbLine = 1)
        {
            Display.displayEmptyLine(nbLine);
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

            return checkUserRegistered;
        }

        /*-------------------[ Setup Max Try ]--------------*/

        public Setup setupMaxTry()
        {
            Setup result = Display.setupMaxTry();
            if (result.MaxTry >= 20)
                throw new ApplicationException("The maximun try cannot be greater than 20!");
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

        public void displayMessage(string message, int? nbEmptyLineBefore = 0, int? nbEmptyLineAfter = 0, int? nbTabulation = 0)
        {
            Display.displayEmptyLine(nbEmptyLineBefore);
            Display.displayTabulation(nbTabulation);
            Display.displayMessage(message);
            Display.displayEmptyLine(nbEmptyLineAfter);
        }

        public void displayTabulation(int? nbTab)
        {
            Display.displayTabulation(nbTab);
        }

        public void displayGame(string[][] gameTable, int indexLine, int indexCol, int indexCurrentLine, EPosition[][] TrackPosition)
        {
            Display.displayGame(gameTable, indexLine, indexCol, indexCurrentLine, TrackPosition);
        }

        public string readResponse(string v)
        {
            return Display.readResponse(v);
        }

    }
}
