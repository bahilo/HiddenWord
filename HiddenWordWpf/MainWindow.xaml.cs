using HiddenWord.Business;
using HiddenWordBusiness.classes;
using HiddenWordBusiness.Core;
using HiddenWordCommon.Interfaces.Business;
using HiddenWordWpf.classes;
using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();

            display = new classes.Display(gvContainer, gvCentral, titlePanel, inputGamer, Response);

            IDisplay blDisplay = new BusinessDisplay(display,
                                                        new BusinessStatistic(new HiddenWordDALXml.DAL()),
                                                        new BusinessWord(new HiddenWordDALXml.DAL()),
                                                        new BusinessSetup(new HiddenWordDALXml.DAL()),
                                                        new BusinessUser(new HiddenWordDALXml.DAL()));
            BL Bl = new BL(new BusinessStatistic(new HiddenWordDALXml.DAL()),
                            new BusinessWord(new HiddenWordDALXml.DAL()),
                            new BusinessSetup(new HiddenWordDALXml.DAL()),
                            new BusinessUser(new HiddenWordDALXml.DAL()),
                            new HiddenWordDALXml.DAL(),
                            blDisplay);

            Bl.BlDisplay.displayWelcomeScreen();

            Game game = new Game(Bl);

            //game.run();

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

            display.setupMaxTry();
        }

        private void menuUserSelect_Click(object sender, RoutedEventArgs e)
        {
            display.SelectUser();
        }

        private void menuUserCreate_Click(object sender, RoutedEventArgs e)
        {
            display.createUser();
        }

        private void menuWord_Click(object sender, RoutedEventArgs e)
        {
            display.setupNewWord();
        }

        private void menuStatistic_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuStartt_Click(object sender, RoutedEventArgs e)
        {

        }

        /*public void myFunction(Object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("click => "+((Button)sender).Content);
         
        }*/
    }
}
