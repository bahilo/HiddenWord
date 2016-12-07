using HiddenWord.Business;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordCommon.Interfaces.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordBusiness.classes
{
    public class Player : Play
    {
        public int NbTry { get; set; }
        public Check CheckCharacter;
        public EndGame gameOver;

        public Player(IActionManager bl, ISetting setting, Random rd)
            : base(bl, setting, rd)
        {
            NbTry = 1;
            CheckCharacter = new Check(bl);
            gameOver = new EndGame(bl);
            PropertyChanged += Play_PropertyChanged;
        }

        private void Play_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "User":
                    resetGame();
                    displayGame();
                    break;
                case "Setup":
                    resetGame();
                    displayGame();
                    break;
            }
        }

        public void initialize()
        {
            start();
        }

        /*-------------------[ start ]--------------*/

        private void start()
        {
            if(Setting.GameSetup())
            {
                this.User = Setting.User;
                this.Setup = Setting.Setup;
            }
            launchGame();            
        }

        private void launchGame()
        {
            setRandomWord();
            CheckCharacter.Word = NewWord.Name;
            resetGame();
        }

        private void resetGame()
        {
            CheckCharacter.IndexLine = Setup.MaxTry;
            CheckCharacter.init();
            Bl.BlDisplay.displayPrompt(this.User.Pseudo);
        }

        public void setRandomWord()
        {
            try
            {
                NewWord = Bl.BlWord.getNewRandomWord(rd);
            }
            catch (ApplicationException e)
            {
                //Bl.BlDisplay.displayMessage(e.Message);
                Bl.BlDisplay.setupNewWord();
                NewWord = Bl.BlWord.getNewRandomWord(rd);
                throw;
            }
        }

        public string GetPseudo()
        {
            return this.User.Pseudo;
        }

        public void displayError()
        {
            this.CheckCharacter.displayError();
        }

        public bool checkWin()
        {
            return this.CheckCharacter.checkWin();
        }

        public bool isCorrectCharater(string response)
        {
            if (response.Length < NewWord.Name.Length)
                return false;

            CheckCharacter.charaterPosition(response);
            return this.CheckCharacter.isCorrectCharater();
        }

        public void displayGame()
        {
            this.CheckCharacter.displayGame();
        }

        public void DisplayUserStatistic()
        {
            var userFound = Bl.BlUser.GetUserData();
            if (userFound.Count != 0)
            {
                userFound[0].UserStats = Bl.BlStat.GetStatisticByUserId(userFound[0].Id);

                foreach (var stat in userFound[0].UserStats)
                {
                    userFound[0].UserWordsStats.Add(Bl.BlWord.GetWordsById(stat.WordId));
                    userFound[0].UserSetupsStats.Add(Bl.BlSetup.GetSetupById(stat.SetupId));
                }

                Bl.BlDisplay.DisplayStatisticByUser(userFound[0]);
            }
        }

        public void exitGame()
        {
            this.gameOver.exitGame();
        }

        public int getMaxTry()
        {
            return this.Setup.MaxTry;
        }

        public string play()
        {
            string response = "";
            try
            {
                response = Bl.BlDisplay.readResponse(NewWord.Name);
                NbTry++;
            }
            catch (ApplicationException)
            {
                throw;
            }

            return response;
        }

        public void selectNewUser()
        {
            var result = Bl.BlDisplay.SelectUser();
            User = (result != null && result.Pseudo != "" && result.Pseudo != null) ? result : User;
        }

        public void createUser()
        {
            var result = Bl.BlDisplay.CreateUser();
            User = (result != null && result.Pseudo != "" && result.Pseudo != null) ? result : User;
        }

        public void setupNewWord()
        {
            var result = Bl.BlDisplay.setupNewWord();
            if (result.Id != 0 && result.Name != null && result.Name != "")
                Bl.BlDisplay.displayMessage(@"The word '" + result.Name + "' have been saved successfully!");
        }

        public void setupMaxTry()
        {
            try
            {
                var result = Bl.BlDisplay.setupMaxTry();
                Setup = (result != null && result.MaxTry != 0) ? result : Setup;
            }
            catch (ApplicationException)
            {
                throw;
            }
        }
    }
}
