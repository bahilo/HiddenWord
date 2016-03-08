using HiddenWord.Business;
using HiddenWordBusiness.classes;
using HiddenWordBusiness.Core;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordWpf.classes;
using HiddenWordWpf.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
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
        Player player;
        BL Bl;

        public MainWindow()
        {
            InitializeComponent();
            
            Init();
            DisplayUserStatistic();
            //SystemSounds.Question.Play();

            List<RoutedUICommand> commandeList = new List<RoutedUICommand>
                                                        {
                                                             CustomCommands.Exit,
                                                             CustomCommands.Start,
                                                             CustomCommands.MaxTry,
                                                             CustomCommands.UserSelect,
                                                             CustomCommands.UserCreate,
                                                             CustomCommands.Word,
                                                             CustomCommands.Statistics
                                                        };

            foreach (var customCommand in commandeList)
            {
                CommandBinding command = new CommandBinding();
                command.Command = customCommand;
                command.CanExecute += (s,e)=> { e.CanExecute = true; }; 
                command.Executed += (s, e) => {
                    switch (customCommand.Name)
                    {
                        case "Exit":
                            menuExit.Command = customCommand;
                            menuExit_Click(this, new RoutedEventArgs());
                            break;
                        case "Start":
                            menuStart.Command = customCommand;
                            menuStart_Click(this, new RoutedEventArgs());
                            break;
                        case "MaxTry":
                            menuMaxTry.Command = customCommand;
                            menuMaxTry_Click(this, new RoutedEventArgs());
                            break;
                        case "UserSelect":
                            menuUserSelect.Command = customCommand;
                            menuUserSelect_Click(this, new RoutedEventArgs());
                            break;
                        case "UserCreate":
                            menuUserCreate.Command = customCommand;
                            menuUserCreate_Click(this, new RoutedEventArgs());
                            break;                            
                        case "Statistics":
                            menuStatistic.Command = customCommand;
                            menuStatistic_Click(this, new RoutedEventArgs());
                            break;
                    }
                };
                
                this.CommandBindings.Add(command);
            }

            /*CommandBinding command = new CommandBinding();
            command.Command = CustomCommands.Start;
            command.CanExecute += Start_CanExecute;
            command.Executed += Start_Executed;

            this.CommandBindings.Add(command);*/

        }

        /*private void Start_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.menuExit_Click(this, new RoutedEventArgs());
        }

        private void Start_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }*/

        private void Init()
        {
            display = new classes.Display(this, gvMain, gvCentral, titlePanel, inputGamer, Response, MyChart);

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
            player = new Player(Bl, new Setting(Bl), new Random());
            display.btn_clickEvent += Display_btn_clickEvent;
            btnValidate.IsDefault = true;
        }

        private void DisplayUserStatistic()
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

        private void Display_btn_clickEvent(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            btnValidate.IsDefault = false;
            player.exitGame();            
        }

        Popup pop = new Popup();
        private void menuMaxTry_Click(object sender, RoutedEventArgs e)
        {
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
            DisplayUserStatistic();
        }

        private void menuStart_Click(object sender, RoutedEventArgs e)
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
                    position = true;
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

            if (player.NbTry >= player.getMaxTry() || position)
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
