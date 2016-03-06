using HiddenWord.Business;
using HiddenWordBusiness.classes;
using HiddenWordCommon.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordWpf.classes
{
    public class Player : Play
    {        
        public int NbTry { get; set; }
        public bool Win { get; set; }
       
        public Check CheckCharacter;
        public EndGame gameOver;


        public Player(IActionManager bl, Random rd)
            : base(bl, rd)
        {
            Win = false;
            NbTry = 0;
            CheckCharacter = new Check(bl);
            gameOver = new EndGame(bl);
            PropertyChanged += Play_PropertyChanged;
        }

        private void Play_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "User":
                    Bl.BlDisplay.displayPrompt(User.Pseudo);
                    break;
                case "Setup":
                    init();
                    break;
                case "NewWord":
                    break;
            }
        }

        public void init()
        {
            start();
        }

        /*-------------------[ start ]--------------*/

        private void start()
        {
            settings(false);
            launchGame();
        }

        private void launchGame()
        {
            CheckCharacter.Word = NewWord.Name;
            CheckCharacter.IndexLine = Setup.MaxTry;
            CheckCharacter.init();
            Bl.BlDisplay.displayPrompt(this.User.Pseudo);
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

        public bool isCorrectCharater(string v)
        {
            CheckCharacter.charaterPosition(v);            
            return this.CheckCharacter.isCorrectCharater();
        }

        public void displayGame()
        {
            this.CheckCharacter.displayGame();
        }

        public void exitGame()
        {
            this.gameOver.exitGame();
            Console.ReadKey();
        }

        public int getMaxTry()
        {
            return this.Setup.MaxTry;
        }

        public string play()
        {
            string response =  Bl.BlDisplay.readResponse("Response: ");
            if (response.Length < NewWord.Name.Length)
                throw new ApplicationException("Must be at least "+NewWord.Name.Length+" characteres!");
            
              return response;
        }

        public void selectNewUser()
        {
            User = Bl.BlDisplay.SelectUser();
        }

        internal void createUser()
        {
            User = Bl.BlDisplay.CreateUser();
        }

        internal void setupNewWord()
        {
            NewWord = Bl.BlDisplay.setupNewWord();
        }

        internal void setupMaxTry()
        {
            Setup = Bl.BlDisplay.setupMaxTry();
        }
    }
}
