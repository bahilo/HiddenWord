using HiddenWord.Business;
using HiddenWordBusiness.classes;
using HiddenWordBusiness.Core;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordWpf.Classes;
using System;
using System.Windows;

namespace HiddenWordWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        classes.Display display;
        Player player;
        BL Bl;

        public MainWindow()
        {
            InitializeComponent();
            
            Init();
            DisplayUserStatistic();
            Bl.BlDisplay.setupMenu();

        }

        //===================================================================================================================
        //=======================================================[ Methods ]=================================================
        //===================================================================================================================

        /// <summary>
        /// Initialize the game
        /// </summary>
        private void Init()
        {
            display = new classes.Display(  this,           // The main Window
                                            gvMain,         // Main grid
                                            gvCentral,      // Central grid for dialogBox
                                            titlePanel,     // StackPanel top for title
                                            inputGamer,     // TextBlock user input 
                                            Response,       // Label user response input 
                                            MyChart,        // Chart object
                                            menuExit,       
                                            menuStart,
                                            menuMaxTry,
                                            menuUserSelect,
                                            menuUserCreate,
                                            menuStatistic
                                            );

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

            player = new Player(Bl, new Setting(Bl), new Random());
            display.btn_clickEvent += Display_btn_clickEvent;
            
        }


        /// <summary>
        /// Display user statistics
        /// </summary>
        public void DisplayUserStatistic()
        {
            inputGamer.Visibility = Visibility.Hidden;
            btnValidate.Visibility = Visibility.Hidden;
            Response.Visibility = Visibility.Hidden;
            titlePanel.Visibility = Visibility.Hidden;

            gvMain.Children.Clear();
            gvMain.RowDefinitions.Clear();
            gvMain.ColumnDefinitions.Clear();

            gvMain.Children.Add(MyChart);

            player.DisplayUserStatistic();
        }



        //===================================================================================================================
        //================================================[ Event Listener ]=================================================
        //===================================================================================================================

       
        /// <summary>
        /// Exit game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Display_btn_clickEvent(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Display Exit message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void menuExit_Click(object sender, RoutedEventArgs e)
        {
            btnValidate.IsDefault = false;
            player.exitGame();            
        }

        /// <summary>
        /// Set the max try
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void menuMaxTry_Click(object sender, RoutedEventArgs e)
        {
            player.setupMaxTry();
        }

        /// <summary>
        /// Select a user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void menuUserSelect_Click(object sender, RoutedEventArgs e)
        {
            player.selectNewUser();
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void menuUserCreate_Click(object sender, RoutedEventArgs e)
        {
            player.createUser();
        }

        /// <summary>
        /// Add new word in the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void menuWord_Click(object sender, RoutedEventArgs e)
        {
            player.setupNewWord();
        }

        /// <summary>
        /// Display statistic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void menuStatistic_Click(object sender, RoutedEventArgs e)
        {
            DisplayUserStatistic();
        }


        /// <summary>
        /// Method to start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void menuStart_Click(object sender, RoutedEventArgs e)
        {            
            player.NbTry = 0;   
            player.init();
            player.displayGame();
            gvCentral.Visibility = Visibility.Hidden;
            inputGamer.Focus();

            inputGamer.Visibility = Visibility.Visible;
            btnValidate.Visibility = Visibility.Visible;
            Response.Visibility = Visibility.Visible;
            titlePanel.Visibility = Visibility.Visible;
            display.displayWelcomeScreen();

            btnValidate.IsDefault = true;
            inputGamer.Focus();
        }

        /// <summary>
        /// Check user input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnValidate_Click(object sender, RoutedEventArgs e)
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
                    Bl.BlDisplay.DisplayCongratulation();
                    position = true;
                }
                player.NbTry++;
                player.displayGame();
            }
            else if (player.getMaxTry() <= 0)
            {
                Bl.BlDisplay.displayWarningMaxTry(player.getMaxTry());
            }

            if (player.NbTry >= player.getMaxTry() || position)
            {
                player.gameOver = new EndGame(Bl, player.User, player.NewWord, player.NbTry, player.Setup);
            }  

        }

    }
}
