using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordWpf.Classes;
using HiddenWordWpf.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HiddenWordWpf.classes
{
    public class Display : IDisplay
    {
        private MenuItem menuExit;
        private MenuItem menuStart;
        private MenuItem menuMaxTry;
        private MenuItem menuUserSelect;
        private MenuItem menuUserCreate;
        private MenuItem menuStatistic;

        public MainWindow MyWindow { get; set; }
        public Grid gvMain { get; set; }
        public StackPanel stackPanelTop { get; set; }
        public StackPanel MyStackPanel { get; set; }
        public TextBox inputPlayerBox { get; set; }
        public Chart MyChart { get; set; }
        public Grid gvCentral { get; set; }
        public string PageTitle { get; set; }
        public Label labelInputPlayerBox { get; set; }
        public TextBlock MyTextCentre { get; set; }

        public event BtnClickEventHandler btn_clickEvent;

        public Display(MainWindow wind, Grid gvcont, Grid gvCentr, StackPanel stackPanel, TextBox inputPlayer, Label inputLabel, Chart chart)
        {
            MyWindow = wind;
            gvMain = gvcont;
            MyChart = chart;
            gvCentral = gvCentr;
            stackPanelTop = stackPanel;
            inputPlayerBox = inputPlayer;
            PageTitle = "WELECOM INTO HIDDEN WORD!";
            labelInputPlayerBox = inputLabel;
                     
            MyTextCentre = new TextBlock();
            MyTextCentre.FontSize = 14;
            MyTextCentre.FontWeight = FontWeights.Bold;
            MyTextCentre.HorizontalAlignment = HorizontalAlignment.Center;
            MyTextCentre.VerticalAlignment = VerticalAlignment.Center;

            MyStackPanel = new StackPanel();
            //gvCentral
        }

        public Display(MainWindow mainWindow, Grid gvMain, Grid gvCentral, StackPanel titlePanel, TextBox inputGamer, Label response, Chart myChart, MenuItem menuExit, MenuItem menuStart, MenuItem menuMaxTry, MenuItem menuUserSelect, MenuItem menuUserCreate, MenuItem menuStatistic)
            :this(mainWindow, gvMain, gvCentral, titlePanel, inputGamer, response, myChart)
        {            
            this.menuExit = menuExit;
            this.menuStart = menuStart;
            this.menuMaxTry = menuMaxTry;
            this.menuUserSelect = menuUserSelect;
            this.menuUserCreate = menuUserCreate;
            this.menuStatistic = menuStatistic;
        }

        public User SelectUser()
        {
            User user = new User();
            user.Pseudo = read("Please enter your pseudo "); 
            return user;
        }

        public User CreateUser()
        {
            User user = new User();
            user.Pseudo = read("Please enter your new pseudo ");
            return user;
        }

        public User createUser()
        {
            User user = new User();
            user.Pseudo = read("Please enter a pseudo ");
            return user;
        }       

        
        /*-------------------[ Setup New Word ]--------------*/

        public Words setupNewWord()
        {
            string response = inputPlayerBox.Text;            
            Words word = new Words();
            word.Name = read("Please enter a new word: ");
            return word;
        }


        /*-------------------[ Setup Max Try ]--------------*/

        public Setup setupMaxTry()
        {
            Setup setup = new Setup();
            try
            {
                setup.MaxTry = int.Parse(read("Please enter the maximum of try:"));
            }
            catch (Exception)
            {                    
                setup.MaxTry = 0;
            }            
            setup.Status = (int)ESetup.Active;
            return setup;
        }

        /*-------------------[ Exit Game ]--------------*/

        public void exitGame()
        {
            int nbRow = 3, nbCol = 3, AxisX = 1, AxisY = 1;

            MyTextCentre.Text = "Bye!";
            MyTextCentre.Foreground = new SolidColorBrush(Colors.Black);
            MyStackPanel.Children.Clear();
            MyStackPanel.Children.Add(MyTextCentre);
            initCentralGrid(nbRow, nbCol, AxisX, AxisY, MyStackPanel, clearClildren: true);

            Button myBtn = new Button();
            myBtn.Width = gvCentral.Width / (nbCol * 2);
            myBtn.Height = gvCentral.Height / (nbRow * 2);
            myBtn.Content = "OK";
            myBtn.IsDefault = true;

            AxisX = 2; AxisY = 1;
            initCentralGrid(nbRow, nbCol, AxisX, AxisY, myBtn, clearClildren: false);

            myBtn.Click += btnExitGame_click;
        }

        private void btnExitGame_click(object sender, RoutedEventArgs e)
        {
            onBtnClick();
        }

        private void onBtnClick()
        {
            if (btn_clickEvent != null)
                btn_clickEvent(this, new EventArgs());
        }

        /*-------------------[ End Game ]--------------*/

        public void endGame(string solution)
        {
            int nbRow = 3, nbCol = 3, AxisX = 1, AxisY = 1;

            MyTextCentre.Text += " GAME OVER! \n"+"solution: " + solution;
            MyStackPanel.Children.Clear();
            MyStackPanel.Children.Add(MyTextCentre);            
            initCentralGrid(nbRow, nbCol, AxisX, AxisY, MyStackPanel, clearClildren: true);                        
        }


        /*-------------------[ Display startup Menu ]--------------*/

        public string displayStartupMenu()
        {
            return read("Please choose < 0. Exit, 1. Setting, 2. Start >");
        }


        /*-------------------[ Read player input ]--------------*/

        public string read(string message)
        {            
            string response = inputPlayerBox.Text;
            InputDialog dialoBox = new InputDialog(message);

            if (dialoBox.ShowDialog() == true)
                response = dialoBox.Answer;

            return response;
        }


        /*-------------------[ Welcome message ]--------------*/

        public void displayWelcomeScreen()
        {
            TextBlock myTextTop = new TextBlock();
            myTextTop.Text = PageTitle;
            myTextTop.FontSize = 14;
            myTextTop.HorizontalAlignment = HorizontalAlignment.Center;
            myTextTop.VerticalAlignment = VerticalAlignment.Center;

            stackPanelTop.Children.Clear();
            stackPanelTop.Children.Add(myTextTop);
        }


        /*-------------------[ Prompt diplaying ]--------------*/

        public void displayPrompt(string pseudo)
        {
            labelInputPlayerBox.Content = "[" + pseudo + "] - What is the hidden word?";
        }


        /*-------------------[ Congratulation ]--------------*/

        public void DisplayCongratulation()
        {
            MyTextCentre.Text = "CONGRATULATION";
            MyTextCentre.Foreground = new SolidColorBrush(Colors.Green);
            MyStackPanel.Children.Clear();
            MyStackPanel.Children.Add(MyTextCentre);
            
            initCentralGrid(3, 3, 1, 1, MyStackPanel, clearClildren: true);
        }

        private void initCentralGrid(int nbRow, int nbCol, int axisX, int axisY, UIElement children, int rowSpan = 1, int colSpan =1, bool clearClildren = false)
        {
            if (clearClildren)
                gvCentral.Children.Clear();

            gvCentral.RowDefinitions.Clear();
            gvCentral.ColumnDefinitions.Clear();

            for (int i = 0; i < nbRow; i++)
            {
                var rowdef = new RowDefinition();
                rowdef.Height = new GridLength(gvCentral.Height / nbRow);
                gvCentral.RowDefinitions.Add(rowdef);
            }

            for (int i = 0; i < nbCol; i++)
            {
                var colDef = new ColumnDefinition();
                colDef.Width = new GridLength(gvCentral.Width / nbCol);
                gvCentral.ColumnDefinitions.Add(colDef);
            }

            gvCentral.Visibility = Visibility.Visible;            
            gvCentral.Children.Add(children);
            if(colSpan == 1) Grid.SetColumn(children, axisY);
            Grid.SetColumnSpan(children, colSpan);
            if (rowSpan == 1) Grid.SetRow(children, axisX);
            Grid.SetRowSpan(children, rowSpan);
            gvCentral.Background = new SolidColorBrush(Colors.Ivory);

        }

        /*-------------------[ Warning handle ]--------------*/

        public void displayWarningMaxTry(int maxTry)
        {
            int nbRow = 3, nbCol = 3, AxisX = 1, AxisY = 1;
            
            MyTextCentre.Text = "WARNING maximun try = " + maxTry;
            MyTextCentre.Foreground = new SolidColorBrush(Colors.Orange);

            MyStackPanel.Children.Clear();
            MyStackPanel.Children.Add(MyTextCentre);
            initCentralGrid(nbRow, nbCol, AxisX, AxisY, MyStackPanel, clearClildren: true);

            Button myBtn = new Button();
            myBtn.Width = gvCentral.Width / (nbCol * 2);
            myBtn.Height = gvCentral.Height / (nbRow * 2);
            myBtn.Content = "OK";

            AxisX = 2; AxisY = 2;
            initCentralGrid(nbRow, nbCol, AxisX, AxisY, myBtn, clearClildren: false);

            myBtn.Click += btn_click;
        }

        
        /*-------------------[ Display setupMenu ]--------------*/

        public string setupMenu()
        {
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
                command.CanExecute += (s, e) => { e.CanExecute = true; };
                command.Executed += (s, e) => {
                    switch (customCommand.Name)
                    {
                        case "Exit":
                            menuExit.Command = customCommand;
                            MyWindow.menuExit_Click(this, new RoutedEventArgs());
                            break;
                        case "Start":
                            menuStart.Command = customCommand;
                            MyWindow.menuStart_Click(this, new RoutedEventArgs());
                            break;
                        case "MaxTry":
                            menuMaxTry.Command = customCommand;
                            MyWindow.menuMaxTry_Click(this, new RoutedEventArgs());
                            break;
                        case "UserSelect":
                            menuUserSelect.Command = customCommand;
                            MyWindow.menuUserSelect_Click(this, new RoutedEventArgs());
                            break;
                        case "UserCreate":
                            menuUserCreate.Command = customCommand;
                            MyWindow.menuUserCreate_Click(this, new RoutedEventArgs());
                            break;
                        case "Statistics":
                            menuStatistic.Command = customCommand;
                            MyWindow.menuStatistic_Click(this, new RoutedEventArgs());
                            break;
                    }
                };

                MyWindow.CommandBindings.Add(command);
            }

            return "";
        }






        public void displayMessage(string message, int nbEmptyLineBefore = 0, int nbEmptyLineAfter = 0, int nbTabulation = 0)
        {
            int nbRow = 3, nbCol = 3, AxisX = 1, AxisY = 1;

            MyTextCentre.Text = message;
            MyStackPanel.Children.Clear();
            MyStackPanel.Children.Add(MyTextCentre);
            initCentralGrid(nbRow, nbCol, AxisX, AxisY, MyStackPanel, clearClildren: true);

            Button myBtn = new Button();
            myBtn.Width = gvCentral.Width / ( nbCol * 2 ) ;
            myBtn.Height = gvCentral.Height / (nbRow * 2);
            myBtn.Content = "OK";

            AxisX = 2; AxisY = 2;
            initCentralGrid(nbRow, nbCol, AxisX, AxisY, myBtn, clearClildren: false);

            myBtn.Click += btn_click;
        }

        private void btn_click(object sender, RoutedEventArgs e)
        {
            gvCentral.Visibility = Visibility.Hidden;
        }

        public void displayGame(string[][] gameTable, int indexLine, int indexCol, int indexCurrentLine, EPosition[][] TrackPosition)
        {
            gvMain.Children.Clear();
            gvMain.RowDefinitions.Clear();
            gvMain.ColumnDefinitions.Clear();

            gvMain.HorizontalAlignment = HorizontalAlignment.Left;
            gvMain.VerticalAlignment = VerticalAlignment.Top;
            gvMain.Background = new SolidColorBrush(Colors.PaleVioletRed);
            //gvMain.ShowGridLines = true;

            for (int i = 0; i < indexLine; i++)
            {
                var rowdef = new RowDefinition();
                rowdef.Height = new GridLength(gvMain.Height / indexLine);
                gvMain.RowDefinitions.Add(rowdef);
            }
            for (int i = 0; i < indexCol; i++)
            {
                var coldef = new ColumnDefinition();
                coldef.Width = new GridLength(gvMain.Width / indexCol);
                gvMain.ColumnDefinitions.Add(coldef);
            }

            for (int i = 0, line = 0; i < indexLine; i++)
            {
                for (int y = 0, col = 0; y < indexCol; y++, col++)
                {
                    var btn = new Button();
                    btn.HorizontalContentAlignment = HorizontalAlignment.Center;
                    btn.VerticalContentAlignment = VerticalAlignment.Center;
                    btn.Name = "_" + i + "" + y;
                    btn.FontSize = 15;
                    btn.Content = gameTable[i][y];
                    Grid.SetColumn(btn, col);
                    Grid.SetRow(btn, line);

                    if (TrackPosition[i][y].Equals(EPosition.GoodPosition))
                    {
                        btn.Background = new SolidColorBrush(Colors.Green);
                        btn.ToolTip = "Good Position";
                    }
                    else if (TrackPosition[i][y].Equals(EPosition.BadPosition))
                    {
                        btn.Background = new SolidColorBrush(Colors.Orange);
                        btn.ToolTip = "Bad Position";
                    }
                    else if (indexCurrentLine > 0 && line <= indexCurrentLine - 1  && TrackPosition[i][y].Equals(EPosition.NotInWord))
                    {
                        btn.Background = new SolidColorBrush(Colors.Red);
                        btn.ToolTip = "Not In Word";
                    }
                    else
                    {
                        btn.Background = new SolidColorBrush(Colors.Ivory);
                    }

                    if (i == indexCurrentLine)
                    {
                        //MyWindow.RegisterName(btn.Name, btn);
                        Storyboard myStoryBord = new Storyboard();

                        DoubleAnimation myDoubleAnimationFadeIn = new DoubleAnimation();
                        myDoubleAnimationFadeIn.From = 0.0;
                        myDoubleAnimationFadeIn.To = 1.0;
                        myDoubleAnimationFadeIn.Duration = new Duration(TimeSpan.FromSeconds(3));
                        myDoubleAnimationFadeIn.AutoReverse = true;
                        myDoubleAnimationFadeIn.RepeatBehavior = RepeatBehavior.Forever;

                        DoubleAnimation myDoubleAnimationFadeOut = new DoubleAnimation();
                        myDoubleAnimationFadeOut.From = 1.0;
                        myDoubleAnimationFadeOut.To = 0.0;
                        myDoubleAnimationFadeOut.BeginTime = TimeSpan.FromSeconds(3);
                        //myDoubleAnimationFadeOut.Duration = new Duration(TimeSpan.FromSeconds(5));
                        myDoubleAnimationFadeOut.AutoReverse = true;
                        myDoubleAnimationFadeOut.RepeatBehavior = RepeatBehavior.Forever;

                        //btn.BeginAnimation(Button.OpacityProperty, myDoubleAnimationFadeIn);
                        
                        myStoryBord.Children.Add(myDoubleAnimationFadeIn);
                        Storyboard.SetTarget(myDoubleAnimationFadeIn, btn);
                        Storyboard.SetTargetProperty(myDoubleAnimationFadeIn, new PropertyPath("Opacity", 0.7));
                        myStoryBord.Begin(btn);

                        //btn.BeginAnimation(Button.OpacityProperty, myDoubleAnimation);
                        //Storyboard myStoryBord = new Storyboard();
                        myStoryBord.Children.Add(myDoubleAnimationFadeOut);
                        Storyboard.SetTargetName(myDoubleAnimationFadeOut, btn.Name);
                        Storyboard.SetTargetProperty(myDoubleAnimationFadeOut, new PropertyPath("Opacity", 0));

                    }

                    gvMain.Children.Add(btn);
                    
                    if (col == indexCol -1)
                    {
                        col = 0;
                        line++;
                    }

                    if (line == indexLine)
                    {
                        line = 0;
                    }
                }
            }
        }

        public string readResponse(string message)
        {
            string response = inputPlayerBox.Text;
            inputPlayerBox.Text = "";
             /*TextBlock myTextTop = new TextBlock();
            myTextTop.Text = message; // PageTitle;
            stackPanelTop.Children.Clear();
            stackPanelTop.Children.Add(myTextTop);
            //if ( reponse)
           InputDialog dialoBox = new InputDialog(message);

            if (dialoBox.ShowDialog() == true)
                response = dialoBox.Answer;*/

            return response;
        }

        public void DisplayStatisticByUser(User user)
        {
            //MyChart.Width = gvMain.Width;
            //MyChart.Height = gvMain.Height;
            MyChart.Title = "Statistics of "+user.Pseudo;
            KeyValuePair<string, int>[] keyValuePair = new KeyValuePair<string, int>[user.UserStats.Count()]; 
            for (int i=0; i< user.UserStats.Count; i++)
            {
                keyValuePair[i] = new KeyValuePair<string, int>(user.UserWordsStats[i].Name, user.UserStats[i].NbTry);
            }
            ((AreaSeries)MyChart.Series[0]).ItemsSource = keyValuePair;
        }
    }
}
