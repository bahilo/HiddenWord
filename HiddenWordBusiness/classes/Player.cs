using HiddenWord.Business;
using HiddenWordCommon.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordBusiness.classes
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
        }

        public void init()
        {
            start();
            //CheckCharacter.Word = NewWord.Name;
            //CheckCharacter.init();
        }

        /*-------------------[ start ]--------------*/

        private void start()
        {
            bool isSetup;
            string response;
            do
            {

                response = Bl.BlDisplay.displayStartupMenu();

                if (response.Equals("0"))
                {
                    IsExitGame = true;
                }
                else if (response.Equals("1")) // Setting
                {
                    isSetting = true;
                }
                else if (response.Equals("2")) // Start game
                {
                    isSetting = false;
                }

                if (IsExitGame)
                    isSetup = true;
                else
                    isSetup = settings(isSetting);
            } while (  
                        !response.Equals("0")
                        && !response.Equals("1") 
                        && !response.Equals("2") 
                        || !isSetup
               );

            launchGame();
        }

        private void launchGame()
        {
            //try
            //{
            //    NewWord = Bl.BlWord.getNewRandomWord(rd);
            //}catch(ApplicationException e)
            //{
            //    Bl.BlDisplay.displayMessage(e.Message);
            //    Bl.BlDisplay.setupNewWord();
            //    NewWord = Bl.BlWord.getNewRandomWord(rd);
            //}
            /*if(typeof(Console).IsInstanceOfType(Console))
            Console.Clear();*/
            CheckCharacter.IndexLine = Setup.MaxTry;
            CheckCharacter.Word = NewWord.Name;
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
            string response =  Bl.BlDisplay.readScreen("Response: ");
            if (response.Length < NewWord.Name.Length)
                throw new ApplicationException("Must be at least "+NewWord.Name.Length+" characteres!");
            
              return response;
        }



    }
}
