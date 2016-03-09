using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
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
        string read(string message);
        void displayWelcomeScreen();
        void displayPrompt(string pseudo);
        void DisplayCongratulation();
        void displayWarningMaxTry(int maxTry);
        void displayMessage(string message, int nbEmptyLineBefore = 0, int nbEmptyLineAfter = 0, int nbTabulation = 0);
        void displayGame(string[][] gameTable, int indexLine, int indexCol, int  indexCurrentLine, EPosition[][] TrackPosition);
        string readResponse(string v);
        User CreateUser();
        void DisplayStatisticByUser(User user);
    }
}