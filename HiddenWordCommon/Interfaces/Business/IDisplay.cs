using HiddenWordCommon.classes;
using System.Collections.Generic;

namespace HiddenWordCommon.Interfaces.Business
{
    public interface IDisplay
    {
        User SelectUser();
        string setupMenu();
        Words setupNewWord();
        Setup setupMaxTry();
        void exitGame();
        void endGame(string solution);
        string displayStartupMenu();
        string readScreen(string message);
        void displayWelcomeScreen();
        void displayPrompt(string pseudo);
        void DisplayCongratulation();
        void displayWarningMaxTry(int maxTry);
        void displayEmptyLine(int? nbLine = 1);
        void displayMessage(string message, int? nbEmptyLineBefore = 0, int? nbEmptyLineAfter = 0, int? nbTabulation = 0);
        void displayTabulation(int? nbTab = 1);
        void displayGame(string[][] gameTable, int indexLine, int indexCol, int  indexCurrentLine);
    }
}