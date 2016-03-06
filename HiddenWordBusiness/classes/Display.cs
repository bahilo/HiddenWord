using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
using HiddenWordCommon.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordBusiness.classes
{
    public class Display : IDisplay
    {

        public User SelectUser()
        {
            string response = "";
            
            this.displayEmptyLine();
            this.displayTabulation(2);
            displayMessage("\n....... USER SELECTION ......\n");
            this.displayEmptyLine();
            do
            {
                this.displayTabulation(1);
                response = readScreen("Do you have a pseudo already register? (y/n): ");
                this.displayEmptyLine();
            } while (response != "y" && response != "n");

            if (response.Equals("y"))
            {
                response = readScreen("\tPlease enter your pseudo: ");
                this.displayEmptyLine();
            }
            else
            {                
                this.displayTabulation(2);
                displayMessage("....... SETTING NEW USER ......",nbEmptyLineAfter:1);
                //bool next = false;
                do
                {
                    response = readScreen("\tPlease enter your pseudo: ");
                    this.displayEmptyLine();
                } while ( response == "" ); 
            }
            this.displayEmptyLine();
            User user = new User();
            user.Pseudo = response;            
            return user;
        }


        /*-------------------[ setupMenu ]--------------*/

        public string setupMenu()
        {
            string response = "";

            this.displayEmptyLine();
            this.displayTabulation(2);
            displayMessage("....... SETUP ......");
            this.displayEmptyLine();
            do
            {
                writeMenuOption(1, "Quit", "New word", "Setting the maximun of try");
                this.displayEmptyLine();
                response = readScreen("Response: ");
                this.displayEmptyLine();
            } while (!response.Equals("0") && !response.Equals("1") && !response.Equals("2"));

            this.displayEmptyLine();
            return response;
        }

        /*-------------------[ Setup New Word ]--------------*/

        public Words setupNewWord()
        {
            string response = "";
            displayMessage("\t\t....... SETUP NEW WORD ......");
            this.displayEmptyLine();
            do
            {
                response = readScreen("\tPlease enter a new word: ");
                this.displayEmptyLine();
            } while (response == "");

            this.displayEmptyLine();

            Words word = new Words();
            word.Name = response;
            return word;
        } 


        /*-------------------[ Setup Max Try ]--------------*/

        public Setup setupMaxTry()
        {
            string response = "";
            Setup setup = new Setup();
            this.displayEmptyLine();
            displayMessage("\t\t....... SETTING THE MAX TRY ......");
            this.displayEmptyLine();
            do
            {
                response = readScreen("\tPlease enter the maximum of try: ");
                this.displayEmptyLine();
            } while (response == "" && int.Parse(response) == 0);

            this.displayEmptyLine();

            setup.MaxTry = int.Parse(response);
            setup.Status = (int)ESetup.Active;

            return setup;
        }

        public void exitGame()
        {
            this.displayEmptyLine(2);
            this.displayTabulation(2);
            displayMessage("Bye!");
            this.displayEmptyLine();
        }

        public void endGame(string solution)
        {
            this.displayTabulation(2);
            displayMessage("END OF THE GAME!");
            this.displayEmptyLine();
            this.displayTabulation(2);
            displayMessage("SOLUTION: " + solution);
            this.displayEmptyLine();
        }

        public string displayStartupMenu()
        {
            string response;
            writeMenuOption(1, "Quit","Settings","Sart game");
            response = readScreen("Response: ");
            this.displayEmptyLine(1);

            return response;
        }

        public string readScreen(string message)
        {
            displayMessage(message);
            return Console.ReadLine();
        }

        public void displayWelcomeScreen()
        {
            this.displayEmptyLine(2);
            this.displayTabulation(2);
            displayMessage("WELECOM TO OUR NEW GAME CALLED << HIDDEN WORD >>");
            this.displayEmptyLine(2);
        }

        public void displayPrompt(string pseudo)
        {
            displayMessage("[" + pseudo + "] - What is the hidden word?");
            this.displayEmptyLine();
        }

        public void DisplayCongratulation()
        {
            displayMessage("Congratulation you won the game");
            this.displayEmptyLine();
        }

        public void displayWarningMaxTry(int maxTry)
        {
            displayMessage("WARNING maximun try = " + maxTry);
            this.displayEmptyLine();
        }

        public void displayEmptyLine(int? nbLine = 1)
        {
            for (int i =1; i <= nbLine; i++)
            {
                Console.Write("\n");
            }
            
        }

        public void displayMessage(string message, int? nbEmptyLineBefore = 0, int? nbEmptyLineAfter = 0, int? nbTabulation = 0)
        {
            Console.Write(message);
        }

        public void displayTabulation(int? nbTab = 1)
        {
            for (int i = 1; i <= nbTab; i++)
            {
                Console.Write("\t");
            }
        }


        private void writeMenuOption(int nbtab, params string[] menuArgs)
        {
            for ( int i = 0 ; i < menuArgs.Count(); i++ )
            {
                displayTabulation(nbtab);
                displayMessage(i+ ". " +menuArgs[i]);
                this.displayEmptyLine();
            }
        }

        public void displayGame(string[][] gameTable, int indexLine, int indexCol, int indexCurrentLine, EPosition[][] TrackPosition)
        {
            for (int i = 0; i < indexLine; i++)
            {
                for (int y = 0, col = 0; y < indexCol; y++, col++)
                {
                    Console.Write(" "+gameTable[i][y]);
                }
                Console.WriteLine("\n\n");
            }
        }

        public string readResponse(string v)
        {
            throw new NotImplementedException();
        }

        public User CreateUser()
        {
            throw new NotImplementedException();
        }
    }
}
