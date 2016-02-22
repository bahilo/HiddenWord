﻿using System;
using System.Linq;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
using System.Collections.Generic;

namespace HiddenWordBusiness.classes
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

        public Check(IActionManager bl, int manxTry)
        {
            Bl = bl;
            IndexLine = manxTry * 2;
        }

        public void init()
        {
            IndexColumn = Word.Length;
            Game = new string[IndexLine][];                       
            Game = convertStringToEmptyArrayOfUnderscore();
            Error = "";
        }

        public string[][] convertStringToEmptyArrayOfUnderscore()
        {
            string[][] StringTab = new string[IndexLine][];
            for (int i = 0; i <= IndexLine; i++)
            {
                for (int y = 0; y < IndexColumn; y++)
                {
                    StringTab[i][y] = "_";
                    TrackPosition[i][y] = EPosition.NotInWord;
                }
            }
            return StringTab;
        }

        public void charaterPosition(string userTry)
        {

            for (int i = 0; i < IndexColumn; i++ )
            {
                Game[IndexCurrentLine][i] = userTry[i].ToString();

                if (Word.Contains(userTry[i]) && Word[i].Equals(userTry[i].ToString()))
                {
                    TrackPosition[IndexCurrentLine][i] = EPosition.GoodPosition;
                }
                if (Word.Contains(userTry[i]))
                {
                    TrackPosition[IndexCurrentLine][i] = EPosition.BadPosition;
                }
                else
                {
                    TrackPosition[IndexCurrentLine][i] = EPosition.NotInWord;
                }
            }

            displayGame(Game[IndexCurrentLine]);

            IndexCurrentLine++;

            displayGame(keepCorrectCharacter());

        }

        public string[] keepCorrectCharacter()
        {
            for (int y = 0; y < IndexColumn; y++)
            {
                if ( TrackPosition[IndexCurrentLine -1][y] == EPosition.GoodPosition )
                {
                    Game[IndexCurrentLine][y] = Word[y].ToString();
                }
            }
            return Game[IndexCurrentLine];
        }


        private IEnumerable<string>  getCharacter(Words word)
        {
            for (int i = 0; i < Word.Count(); i++)
            {
                yield return Game[IndexCurrentLine][i];                
            }
            
        }

        public bool checkWin()
        {
            //string buf1, buf2;
            for (int i = 0 ; i < Word.Count(); i++ )
            {
                if( !Game[IndexCurrentLine][i].Equals(Word[i].ToString()) ){                    
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
            Bl.BlDisplay.displayEmptyLine();
            foreach (string charact in Game[IndexCurrentLine])
            {
                Bl.BlDisplay.displayMessage(charact + " ");
            }
            Bl.BlDisplay.displayEmptyLine();
        }


        public void displayError()
        {
            Bl.BlDisplay.displayMessage(Error,0,0);
        }

        public bool isCorrectCharater()
        {
            for (int y = 0; y < IndexColumn; y++)
            {
                if(TrackPosition[IndexCurrentLine][y] == EPosition.GoodPosition)
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
