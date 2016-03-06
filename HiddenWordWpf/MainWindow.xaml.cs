using HiddenWord.Business;
using HiddenWordBusiness.classes;
using HiddenWordBusiness.Core;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordWpf.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HiddenWordWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        classes.Display display;
        Game game;
        HiddenWordWpf.classes.Player player;
        BL Bl;

        public MainWindow()
        {
            InitializeComponent();

            display = new classes.Display(gvMain, gvCentral, titlePanel, inputGamer, Response);

            IDisplay blDisplay = new BusinessDisplay(display,
                                                        new BusinessStatistic(new HiddenWordDALXml.DAL()),
                                                        new BusinessWord(new HiddenWordDALXml.DAL()),
                                                        new BusinessSetup(new HiddenWordDALXml.DAL()),
                                                        new BusinessUser(new HiddenWordDALXml.DAL()));
            Bl = new BL(new BusinessStatistic(new HiddenWordDALXml.DAL()),
                            new BusinessWord(new HiddenWordDALXml.DAL()),
                            new BusinessSetup(new HiddenWordDALXml.DAL()),
                            new BusinessUser(new HiddenWordDALXml.DAL()),
                            new HiddenWordDALXml.DAL(),
                            blDisplay);

            

            //game = new Game(Bl);
            player = new HiddenWordWpf.classes.Player(Bl, new Random());
            player.NbTry = 0;


            Bl.BlDisplay.displayWelcomeScreen();            
            player.init();
            player.displayGame();
            inputGamer.Focus();
        }
        
        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        Popup pop = new Popup();
        private void menuMaxTry_Click(object sender, RoutedEventArgs e)
        {
            //var popup = new PopupWindow();
            //InputDialog dialoBox = new InputDialog("Is it working?","Here response");

            //if (dialoBox.ShowDialog() == true)
            //    Debug.WriteLine(dialoBox.Answer);

            //Debug.WriteLine(dialoBox.Answer);
            /*TextBox myTextBox = new TextBox();
            myTextBox.Width = pop.Width / 2;
            gvCentral.Background = new SolidColorBrush(Colors.Black);

            pop.Child = myTextBox;
            pop.IsOpen = true;*/

            player.setupMaxTry();
        }

        private void menuUserSelect_Click(object sender, RoutedEventArgs e)
        {
            player.selectNewUser();
        }

        private void menuUserCreate_Click(object sender, RoutedEventArgs e)
        {
            player.createUser();
        }

        private void menuWord_Click(object sender, RoutedEventArgs e)
        {
            player.setupNewWord();
        }

        private void menuStatistic_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuStartt_Click(object sender, RoutedEventArgs e)
        {
            Bl.BlDisplay.displayWelcomeScreen();            
            player.init();
            player.displayGame();
            //gvMain.Visibility = Visibility.Visible;
            gvCentral.Visibility = Visibility.Hidden;
        }

        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            bool position = false;            

            if (player.getMaxTry() > 0 && player.NbTry <= player.getMaxTry())
            {                
                try
                {
                    position = player.isCorrectCharater(player.play());
                }
                catch (ApplicationException ex)
                {
                    Bl.BlDisplay.displayMessage(ex.Message);
                }

                if (player.checkWin())
                {
                    //_player.displayGame();                    
                    Bl.BlDisplay.DisplayCongratulation();                    
                }
                /*else if (!position)
                {
                    player.displayError();
                }*/
                player.NbTry++;
                player.displayGame();
            }
            else if (player.getMaxTry() <= 0)
            {
                Bl.BlDisplay.displayWarningMaxTry(player.getMaxTry());
            }
            else
            {
                player.gameOver = new EndGame(Bl, player.User, player.NewWord, player.NbTry, player.Setup);
            }  

        }

        /*public void myFunction(Object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("click => "+((Button)sender).Content);
         
        }*/
    }
}
