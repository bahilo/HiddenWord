using HiddenWordCommon.classes;
using HiddenWordCommon.Enums;
using HiddenWordCommon.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace HiddenWordWpf.classes
{
    public class Display : IDisplay
    {
        public Grid gvMain { get; set; }
        public StackPanel stackPanelTop { get; set; }
        public TextBox inputPlayerBox { get; set; }
        public Grid gvCentral { get; set; }
        public string PageTitle { get; set; }
        public Label labelInputPlayerBox { get; set; }

        public Display(Grid gvcont, Grid gvCentr, StackPanel stackPanel, TextBox inputPlayer, Label inputLabel)
        {
            gvMain = gvcont;
            gvCentral = gvCentr;
            stackPanelTop = stackPanel;
            inputPlayerBox = inputPlayer;
            PageTitle = "WELECOM TO OUR NEW GAME CALLED << HIDDEN WORD >>";
            labelInputPlayerBox = inputLabel;
        }

        public User SelectUser()
        {
            User user = new User();
            user.Pseudo = readScreen("Please enter your pseudo "); 
            return user;
        }

        public User CreateUser()
        {
            User user = new User();
            user.Pseudo = readScreen("Please enter your new pseudo ");
            return user;
        }

        public User createUser()
        {
            //PageTitle = "....... SETTING NEW USER ......";
            
            User user = new User();
            user.Pseudo = readScreen("Please enter a pseudo ");
            return user;
        }
        


        /*-------------------[ setupMenu ]--------------

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
            
            //PageTitle = "....... SETUP NEW WORD ......";

            string response = inputPlayerBox.Text; 

            /*displayMessage("\t\t....... SETUP NEW WORD ......");
            this.displayEmptyLine();
            do
            {
                response = readScreen("\tPlease enter a new word: ");
                this.displayEmptyLine();
            } while (response == "");

            this.displayEmptyLine();*/

            Words word = new Words();
            word.Name = readScreen("Please enter a new word: ");
            return word;
        }


        /*-------------------[ Setup Max Try ]--------------*/

        public Setup setupMaxTry()
        {
            //PageTitle = "....... SETTING THE MAX TRY ......";

            Setup setup = new Setup();
            /*this.displayEmptyLine();
            displayMessage("\t\t....... SETTING THE MAX TRY ......");
            this.displayEmptyLine();
            do
            {
                response = readScreen("\tPlease enter the maximum of try: ");
                this.displayEmptyLine();
            } while (response == "" && int.Parse(response) == 0);

            this.displayEmptyLine();*/
            do
            {
                string exMess = "";
                try
                {
                    setup.MaxTry = int.Parse(readScreen(exMess + "Please enter the maximum of try:"));
                    exMess = "";
                }
                catch (Exception)
                {                    
                    setup.MaxTry = 0;
                    exMess = "Max try = "+ setup.MaxTry +"\n";
                }
            } while ( setup.MaxTry == 0 );
            
            setup.Status = (int)ESetup.Active;

            return setup;
        }

        /*-------------------[ Exit Game ]--------------*/

        public void exitGame()
        {
            TextBlock myTextTop = new TextBlock();
            myTextTop.Text = "....... Exit Game ......";


            //TextBlock myTextCentre1 = new TextBlock();
            //myTextCentre1.Text = "Do you have a pseudo already register? ";

            TextBlock myTextCentre3 = new TextBlock();
            myTextCentre3.Text = "Bye!";

            StackPanel myStackPanel = new StackPanel();
            myStackPanel.Children.Add(myTextCentre3);

            stackPanelTop.Children.Clear();
            stackPanelTop.Children.Add(myTextTop);
            gvCentral.Children.Clear();
            gvCentral.Children.Add(myStackPanel);



            /*this.displayEmptyLine(2);
            this.displayTabulation(2);
            displayMessage("Bye!");
            this.displayEmptyLine();*/
        }

        /*-------------------[ End Game ]--------------*/

        public void endGame(string solution)
        {
            TextBlock myTextTop = new TextBlock();
            myTextTop.Text = "....... END OF THE GAME! ......";


            //TextBlock myTextCentre1 = new TextBlock();
            //myTextCentre1.Text = "Do you have a pseudo already register? ";

            TextBlock myTextCentre3 = new TextBlock();
            myTextCentre3.Text = "SOLUTION: " + solution;

            StackPanel myStackPanel = new StackPanel();
            myStackPanel.Children.Add(myTextCentre3);

            stackPanelTop.Children.Clear();
            stackPanelTop.Children.Add(myTextTop);
            gvCentral.Children.Clear();
            gvCentral.Children.Add(myStackPanel);


            /*this.displayTabulation(2);
            displayMessage("END OF THE GAME!");
            this.displayEmptyLine();
            this.displayTabulation(2);
            displayMessage("SOLUTION: " + solution);
            this.displayEmptyLine();*/
        }


        /*-------------------[ Display startup Menu ]--------------*/

        public string displayStartupMenu()
        {

            /*writeMenuOption(1, "Quit","Settings","Sart game");
            response = readScreen("Response: ");
            this.displayEmptyLine(1);*/

            return readScreen("Please choose < 0. Exit, 1. Setting, 2. Start >");
        }


        /*-------------------[ Read player input ]--------------*/

        public string readScreen(string message)
        {            
            string response = inputPlayerBox.Text;

            /*TextBlock myTextTop = new TextBlock();
            myTextTop.Text = message; // PageTitle;
            stackPanelTop.Children.Clear();
            stackPanelTop.Children.Add(myTextTop);*/
            //if ( reponse)
            InputDialog dialoBox = new InputDialog(message);

            if (dialoBox.ShowDialog() == true)
                response = dialoBox.Answer;

            return response;
        }


        /*-------------------[ Welcome message ]--------------*/

        public void displayWelcomeScreen()
        {
            TextBlock myTextTop = new TextBlock();
            myTextTop.Text = "WELECOM TO OUR NEW GAME CALLED << HIDDEN WORD >>";


            //TextBlock myTextCentre1 = new TextBlock();
            //myTextCentre1.Text = "Do you have a pseudo already register? ";

            TextBlock myTextCentre3 = new TextBlock();
            myTextCentre3.Text = "";

            StackPanel myStackPanel = new StackPanel();
            myStackPanel.Children.Add(myTextCentre3);

            stackPanelTop.Children.Clear();
            stackPanelTop.Children.Add(myTextTop);
            gvCentral.Children.Clear();
            gvCentral.Children.Add(myStackPanel);

            //this.displayEmptyLine(2);
            //this.displayTabulation(2);
            //displayMessage("WELECOM TO OUR NEW GAME CALLED << HIDDEN WORD >>");
            //this.displayEmptyLine(2);

        }


        /*-------------------[ Prompt diplaying ]--------------*/

        public void displayPrompt(string pseudo)
        {
            labelInputPlayerBox.Content = "[" + pseudo + "] - What is the hidden word?";
            //displayMessage();
            //this.displayEmptyLine();
        }


        /*-------------------[ Congratulation ]--------------*/

        public void DisplayCongratulation()
        {
            TextBlock myTextTop = new TextBlock();
            myTextTop.Text = PageTitle;


            //TextBlock myTextCentre1 = new TextBlock();
            //myTextCentre1.Text = "Do you have a pseudo already register? ";

            TextBlock myTextCentre3 = new TextBlock();
            myTextCentre3.Text = "Congratulation you won the game";

            StackPanel myStackPanel = new StackPanel();
            myStackPanel.Children.Add(myTextCentre3);

            stackPanelTop.Children.Clear();
            stackPanelTop.Children.Add(myTextTop);

            RowDefinition rowDef1 = new RowDefinition();
            rowDef1.Height = new GridLength(gvCentral.Height / 3);
            ColumnDefinition colDef1 = new ColumnDefinition();
            colDef1.Width = new GridLength(gvCentral.Width / 3);

            RowDefinition rowDef2 = new RowDefinition();
            rowDef2.Height = new GridLength(gvCentral.Height / 3);
            ColumnDefinition colDef2 = new ColumnDefinition();
            colDef2.Width = new GridLength(gvCentral.Width / 3);

            RowDefinition rowDef3 = new RowDefinition();
            rowDef3.Height = new GridLength(gvCentral.Height / 3);
            ColumnDefinition colDef3 = new ColumnDefinition();
            colDef3.Width = new GridLength(gvCentral.Width / 3);

            gvCentral.ColumnDefinitions.Add(colDef1);
            gvCentral.RowDefinitions.Add(rowDef1);

            gvCentral.ColumnDefinitions.Add(colDef2);
            gvCentral.RowDefinitions.Add(rowDef2);

            gvCentral.ColumnDefinitions.Add(colDef3);
            gvCentral.RowDefinitions.Add(rowDef3);

            //gvCentral.Children.Clear();
            //gvMain.Children.Add(myStackPanel);

            double gvMainHeight = gvMain.Height;
            double gvMainWidth = gvMain.Width;

            double gvCentralHeight = gvCentral.Height;
            double gvCentralWidth = gvCentral.Width;

            /*gvCentral.Height = gvMainHeight;
            gvCentral.Width = gvMainWidth;

            gvMain.Height = gvCentralHeight;
            gvMain.Width = gvCentralWidth;
            gvMain.Children.Clear();*/
            //gvMain.Visibility = Visibility.Hidden;
            gvCentral.Visibility = Visibility.Visible;

            gvCentral.Children.Clear();
            gvCentral.Children.Add(myStackPanel);
            Grid.SetColumn(myStackPanel, 1);
            Grid.SetRow(myStackPanel, 1);
            gvCentral.Background = new SolidColorBrush(Colors.Ivory);

            /*displayMessage("Congratulation you won the game");
            this.displayEmptyLine();*/
        }


        /*-------------------[ Warning handle ]--------------*/

        public void displayWarningMaxTry(int maxTry)
        {
            TextBlock myTextTop = new TextBlock();
            myTextTop.Text = PageTitle;


            //TextBlock myTextCentre1 = new TextBlock();
            //myTextCentre1.Text = "Do you have a pseudo already register? ";

            TextBlock myTextCentre3 = new TextBlock();
            myTextCentre3.Text = "WARNING maximun try = " + maxTry;

            StackPanel myStackPanel = new StackPanel();
            myStackPanel.Children.Add(myTextCentre3);

            stackPanelTop.Children.Clear();
            stackPanelTop.Children.Add(myTextTop);
            gvCentral.Children.Clear();
            gvCentral.Children.Add(myStackPanel);

            //displayMessage("WARNING maximun try = " + maxTry);
            //this.displayEmptyLine();
        }


        /*-------------------[ Display empty lines ]--------------*/

        public void displayEmptyLine(int? nbLine = 1)
        {
            for (int i =1; i <= nbLine; i++)
            {
                Console.Write("\n");
            }
            
        }


        /*-------------------[ Message ]--------------*/

        public void displayMessage(UIElement message, int? nbEmptyLineBefore = 0, int? nbEmptyLineAfter = 0, int? nbTabulation = 0)
        {
           
        }


        /*-------------------[ Display tabulation ]--------------*/

        public void displayTabulation(int? nbTab = 1)
        {
            
        }

        
        /*-------------------[ Menu Options ]--------------*/

        private void writeMenuOption(int nbtab, params string[] menuArgs)
        {
            
        }

        public string setupMenu()
        {
            throw new NotImplementedException();
        }

        public void displayMessage(string message, int? nbEmptyLineBefore = 0, int? nbEmptyLineAfter = 0, int? nbTabulation = 0)
        {
            TextBlock myTextTop = new TextBlock();
            myTextTop.Text = message;
            stackPanelTop.Children.Add(myTextTop);
        }

        public void displayGame(string[][] gameTable, int indexLine, int indexCol, int indexCurrentLine, EPosition[][] TrackPosition)
        {
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
                    btn.Content = gameTable[i][y];

                    if (TrackPosition[i][y].Equals(EPosition.GoodPosition))
                    {
                        btn.Background = new SolidColorBrush(Colors.Green);
                    }
                    else if (TrackPosition[i][y].Equals(EPosition.BadPosition))
                    {
                        btn.Background = new SolidColorBrush(Colors.Orange);
                    }
                    else if (indexCurrentLine > 0 && line == indexCurrentLine - 1 && TrackPosition[i][y].Equals(EPosition.NotInWord))
                    {
                        btn.Background = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        btn.Background = new SolidColorBrush(Colors.Ivory);
                    }

                    if (i == indexCurrentLine)
                    {
                        //Border border = new Border();
                        
                        //DrawingContext dc = new DrawingVisual().RenderOpen(); ;
                        //dc.DrawLine(new Pen(new SolidColorBrush(Colors.Ivory),3), new Point(0, gvMain.RowDefinitions[i].Offset), new Point(gvMain.Width / indexCol, gvMain.RowDefinitions[i].Offset));
                        //dc.Close();
                    }
                    gvMain.Children.Add(btn);
                    Grid.SetColumn(btn, col);
                    Grid.SetRow(btn, line);
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

    }
}
