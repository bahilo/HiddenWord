
using HiddenWordBusiness.classes;
using HiddenWordCommon.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordConsole.Classes
{
    public class Game
    {
        private IActionManager _bl;
        private Player _player;
        private Random _rd;


        public Game(IActionManager bl)
        {
            _bl = bl;
            _rd = new Random();
            _player = new Player(_bl, _rd);
        } 


        public void run()
        {            

            while (!_player.IsExitGame)
            {
                bool position = false;
                _player.init();

                if (!_player.IsExitGame)
                {
                    _player.NbTry = 0;
                    if (_player.getMaxTry() > 0)
                    {
                        while (_player.NbTry <= _player.getMaxTry())
                        {
                            _player.displayGame();
                            try
                            {
                                position = _player.isCorrectCharater(_player.play());
                            }
                            catch (ApplicationException e)
                            {
                                _bl.BlDisplay.displayMessage(e.Message);
                            }

                            if (_player.checkWin())
                            {
                                //_player.displayGame();
                                _bl.BlDisplay.DisplayCongratulation();
                                break;
                            }
                            else if (!position)
                            {
                                _player.displayError();
                            }
                            _player.NbTry++;
                        }
                        _player.gameOver = new EndGame(_bl, _player.User, _player.NewWord, _player.NbTry, _player.Setup);

                        _bl.BlDisplay.displayEmptyLine(3);
                    }
                    else
                    {
                        _bl.BlDisplay.displayWarningMaxTry(_player.getMaxTry());
                    }
                }
            }

            _player.exitGame();
            
        }





    }
}
