using System;
using System.Linq;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
using System.Collections.Generic;

namespace HiddenWordWpf.classes
{
    public class Check : ICheck
    {
        public string Error { get; set; }
        public string[][] Game { get; set; }
        public String Word { get; set; }
        public IActionManager Bl { get; set; }
        public int IndexLine { get; set; }
        public int IndexColumn { get; set; }
        public int IndexCurrentLine { get; set; }
        public EPosition[][] TrackPosition { get; set; }

        public Check(IActionManager bl)
        {
            Bl = bl;

        }

        public void init()
        {
            IndexColumn = Word.Length;
            Game = new string[IndexLine][];
            TrackPosition = new EPosition[IndexLine][];
            Game = convertStringToEmptyArrayOfUnderscore();
            Error = "";
            IndexCurrentLine = 0;
        }

        public string[][] convertStringToEmptyArrayOfUnderscore()
        {
            string[][] StringTab = new string[IndexLine][];
            for (int i = 0; i < IndexLine; i++)
            {
                StringTab[i] = convertStringIntoTableOfString(Word);
                TrackPosition[i] = new EPosition[IndexColumn];
                for (int y = 0; y < IndexColumn; y++)
                {
                    if (y == 0)
                    {
                        StringTab[i][y] = Word[0].ToString();
                    }
                    else
                    {
                        StringTab[i][y] = "_";
                    }

                    TrackPosition[i][y] = EPosition.NotInWord;

                }
            }
            return StringTab;
        }

        private string[] convertStringIntoTableOfString(string source)
        {
            string[] StringTab = new string[source.Length];
            for (int y = 0; y < IndexColumn; y++)
            {
                StringTab[y] = source[y].ToString();
            }
            return StringTab;
        }

        /*private void createTableOfStateForEachCharact()
        {
            ;
            for (int y = 0; y < IndexColumn; y++)
            {
                StringTab[y] = Word[y].ToString();
            }
            
        }*/

        public void charaterPosition(string userTry)
        {

            for (int i = 0; i < IndexColumn; i++)
            {
                Game[IndexCurrentLine][i] = convertStringIntoTableOfString(userTry)[i];

                if (Word.Contains(userTry[i]) && Word[i].Equals(userTry[i]))
                {
                    TrackPosition[IndexCurrentLine][i] = EPosition.GoodPosition;
                }
                if (Word.Contains(userTry[i]) && !Word[i].Equals(userTry[i]))
                {
                    TrackPosition[IndexCurrentLine][i] = EPosition.BadPosition;
                }
            }

            //displayGame(Game[IndexCurrentLine]);

            IndexCurrentLine++;

            keepCorrectCharacter();
            //displayGame();

        }

        public string[] keepCorrectCharacter()
        {
            for (int y = 0; y < IndexColumn; y++)
            {
                if (TrackPosition[IndexCurrentLine - 1][y] == EPosition.GoodPosition)
                {
                    Game[IndexCurrentLine - 1][y] = Word[y].ToString();
                }
                else
                {
                    Game[IndexCurrentLine - 1][y] = "_";
                }
            }
            return Game[IndexCurrentLine - 1];
        }


        private IEnumerable<string> getCharacter(Words word)
        {
            for (int i = 0; i < Word.Count(); i++)
            {
                yield return Game[IndexCurrentLine][i];
            }

        }

        public bool checkWin()
        {
            //string buf1, buf2;
            for (int i = 0; i < Word.Count(); i++)
            {
                if (!Game[IndexCurrentLine - 1][i].Equals(Word[i].ToString()))
                {
                    return false;
                }
            }
            return true;
        }


        public void displayGame(string[] tabword)
        {
            Bl.BlDisplay.displayEmptyLine();
            foreach (string charact in tabword)
            {
                Bl.BlDisplay.displayMessage(charact + " ");
            }
            Bl.BlDisplay.displayEmptyLine();
        }

        public void displayGame()
        {
            Bl.BlDisplay.displayGame(Game, IndexLine, IndexColumn, IndexCurrentLine, TrackPosition);
        }

        /*public void displayGame()
        {
            Bl.BlDisplay.displayEmptyLine();
            foreach (string charact in Game[IndexCurrentLine])
            {
                Bl.BlDisplay.displayMessage(charact + " ");
            }
            Bl.BlDisplay.displayEmptyLine();
        }*/


        public void displayError()
        {
            Bl.BlDisplay.displayMessage(Error, 0, 0);
        }

        public bool isCorrectCharater()
        {
            for (int y = 0; y < IndexColumn; y++)
            {
                if (TrackPosition[IndexCurrentLine - 1][y] != EPosition.GoodPosition)
                    return false;
            }
            return true;
        }


        /*public bool startupRequirement(User user, Setup setup)
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
        }*/


    }
}
